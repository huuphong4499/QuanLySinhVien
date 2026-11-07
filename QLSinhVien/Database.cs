using System;
using System.Data;
using System.IO;
using System.Text.Json;
using Microsoft.Data.SqlClient;

namespace QLSinhVien
{
    /// <summary>
    /// Lớp cấu hình cơ sở dữ liệu
    /// </summary>
    public class DbConfig
    {
        public string Server { get; set; } = "localhost";
        public string Database { get; set; } = "QLSinhVien";
        public bool IntegratedSecurity { get; set; } = true;
        public bool TrustServerCertificate { get; set; } = true;
        public string? UserId { get; set; }
        public string? Password { get; set; }
    }

    /// <summary>
    /// Lớp quản trị và cung cấp kết nối Cơ sở dữ liệu SQL Server tự động cho toàn hệ thống.
    /// Tự động dò tìm SQL Server instance khả dụng, tự động tạo Database và dữ liệu mẫu nếu chưa có.
    /// Giúp bất kỳ ai (sinh viên, giảng viên) tải project về đều có thể chạy được ngay!
    /// </summary>
    public static class Database
    {
        private static string activeServer = "localhost";
        private static string connectionString =
            @"Server=localhost;Database=QLSinhVien;Integrated Security=True;TrustServerCertificate=True;";

        private static readonly string ConfigFileName = "dbsettings.json";

        // Danh sách các Server instance thông dụng nhất trên các máy Windows
        private static readonly string[] CommonServers = new string[]
        {
            ".",
            "localhost",
            @".\SQLEXPRESS",
            @"localhost\SQLEXPRESS",
            @"(localdb)\MSSQLLocalDB",
            "127.0.0.1",
            @"(local)\SQLEXPRESS"
        };

        public static string ConnectionString
        {
            get => connectionString;
            set => connectionString = value;
        }

        public static string ActiveServer => activeServer;

        /// <summary>
        /// Khởi tạo và trả về đối tượng SqlConnection mới
        /// </summary>
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        /// <summary>
        /// Tự động dò tìm SQL Server và khởi tạo CSDL + bảng dữ liệu nếu chưa có.
        /// Hàm này được gọi tự động khi ứng dụng khởi chạy.
        /// </summary>
        public static void Initialize()
        {
            try
            {
                // 1. Thử đọc cấu hình từ file dbsettings.json nếu có
                string? configuredServer = LoadConfiguredServer();

                // 2. Kiểm tra server cấu hình hoặc tự động dò tìm server hoạt động
                string? workingServer = null;

                if (!string.IsNullOrWhiteSpace(configuredServer) && TestServerConnection(configuredServer))
                {
                    workingServer = configuredServer;
                }
                else
                {
                    workingServer = FindWorkingServer();
                }

                if (string.IsNullOrWhiteSpace(workingServer))
                {
                    // Nếu không dò được, giữ mặc định localhost
                    workingServer = "localhost";
                }

                activeServer = workingServer;
                connectionString = BuildConnectionString(activeServer, "QLSinhVien");

                // Lưu lại server hoạt động vào file cấu hình
                SaveConfiguredServer(activeServer);

                // 3. Tự động kiểm tra và khởi tạo Database + Dữ liệu mẫu nếu chưa có
                EnsureDatabaseAndTablesExist(activeServer);
            }
            catch (Exception ex)
            {
                // Ghi nhận lỗi nhưng không làm crash ứng dụng ngay, để người dùng nhận thông báo chi tiết
                System.Diagnostics.Debug.WriteLine("Database Initialize Warning: " + ex.Message);
            }
        }

        /// <summary>
        /// Dò tìm SQL Server instance đang chạy trên máy
        /// </summary>
        private static string? FindWorkingServer()
        {
            foreach (string server in CommonServers)
            {
                if (TestServerConnection(server))
                {
                    return server;
                }
            }
            return null;
        }

        /// <summary>
        /// Kiểm tra kết nối đến một server (với timeout ngắn 2 giây)
        /// </summary>
        private static bool TestServerConnection(string server)
        {
            try
            {
                string testConnStr = $"Server={server};Database=master;Integrated Security=True;TrustServerCertificate=True;Connect Timeout=2;";
                using (SqlConnection conn = new SqlConnection(testConnStr))
                {
                    conn.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Tự động tạo Database QLSinhVien và các bảng + dữ liệu mẫu nếu chưa tồn tại
        /// </summary>
        private static void EnsureDatabaseAndTablesExist(string server)
        {
            // Bước A: Kết nối master để tạo Database QLSinhVien nếu chưa có
            string masterConnStr = $"Server={server};Database=master;Integrated Security=True;TrustServerCertificate=True;";
            using (SqlConnection masterConn = new SqlConnection(masterConnStr))
            {
                masterConn.Open();
                string checkDbSql = @"
                    IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'QLSinhVien')
                    BEGIN
                        CREATE DATABASE QLSinhVien;
                    END";

                using (SqlCommand cmd = new SqlCommand(checkDbSql, masterConn))
                {
                    cmd.ExecuteNonQuery();
                }
            }

            // Bước B: Kết nối Database QLSinhVien để tạo các bảng và dữ liệu mẫu nếu chưa có
            string appConnStr = $"Server={server};Database=QLSinhVien;Integrated Security=True;TrustServerCertificate=True;";
            using (SqlConnection appConn = new SqlConnection(appConnStr))
            {
                appConn.Open();

                // 1. Bảng TAIKHOAN
                string initTaiKhoan = @"
                    IF OBJECT_ID('dbo.TAIKHOAN', 'U') IS NULL
                    BEGIN
                        CREATE TABLE TAIKHOAN
                        (
                            TenDangNhap NVARCHAR(50) PRIMARY KEY,
                            MatKhau NVARCHAR(100) NOT NULL
                        );

                        INSERT INTO TAIKHOAN (TenDangNhap, MatKhau) VALUES
                            (N'admin', N'123456'),
                            (N'khang', N'123456'),
                            (N'phong', N'123456'),
                            (N'an', N'123456'),
                            (N'long', N'123456');
                    END";
                using (SqlCommand cmd = new SqlCommand(initTaiKhoan, appConn)) { cmd.ExecuteNonQuery(); }

                // 2. Bảng KHOA
                string initKhoa = @"
                    IF OBJECT_ID('dbo.KHOA', 'U') IS NULL
                    BEGIN
                        CREATE TABLE KHOA
                        (
                            MaKhoa NVARCHAR(50) PRIMARY KEY,
                            TenKhoa NVARCHAR(100) NOT NULL
                        );

                        INSERT INTO KHOA (MaKhoa, TenKhoa) VALUES
                            (N'CNTT', N'Khoa Công nghệ thông tin'),
                            (N'DTVT', N'Khoa Điện tử viễn thông'),
                            (N'KTXD', N'Khoa Kỹ thuật xây dựng'),
                            (N'NN', N'Khoa Ngoại ngữ'),
                            (N'QTKD', N'Khoa Quản trị kinh doanh');
                    END";
                using (SqlCommand cmd = new SqlCommand(initKhoa, appConn)) { cmd.ExecuteNonQuery(); }

                // 3. Bảng LOP
                string initLop = @"
                    IF OBJECT_ID('dbo.LOP', 'U') IS NULL
                    BEGIN
                        CREATE TABLE LOP
                        (
                            MaLop NVARCHAR(50) PRIMARY KEY,
                            TenLop NVARCHAR(100) NOT NULL,
                            MaKhoa NVARCHAR(50) NOT NULL,
                            CONSTRAINT FK_LOP_KHOA FOREIGN KEY (MaKhoa) REFERENCES KHOA(MaKhoa)
                                ON UPDATE CASCADE ON DELETE NO ACTION
                        );

                        INSERT INTO LOP (MaLop, TenLop, MaKhoa) VALUES
                            (N'CNTT1', N'Công nghệ thông tin 1', N'CNTT'),
                            (N'CNTT2', N'Công nghệ thông tin 2', N'CNTT'),
                            (N'CNTT3', N'Công nghệ thông tin 3', N'CNTT'),
                            (N'DTVT1', N'Điện tử viễn thông 1', N'DTVT'),
                            (N'QTKD1', N'Quản trị kinh doanh 1', N'QTKD');
                    END";
                using (SqlCommand cmd = new SqlCommand(initLop, appConn)) { cmd.ExecuteNonQuery(); }

                // 4. Bảng SINHVIEN
                string initSinhVien = @"
                    IF OBJECT_ID('dbo.SINHVIEN', 'U') IS NULL
                    BEGIN
                        CREATE TABLE SINHVIEN
                        (
                            MaSV NVARCHAR(50) PRIMARY KEY,
                            HoTen NVARCHAR(100) NOT NULL,
                            NgaySinh DATE NOT NULL,
                            GioiTinh NVARCHAR(10) NOT NULL,
                            DiaChi NVARCHAR(200) NULL,
                            SoDienThoai NVARCHAR(20) NULL,
                            MaLop NVARCHAR(50) NOT NULL,
                            CONSTRAINT FK_SINHVIEN_LOP FOREIGN KEY (MaLop) REFERENCES LOP(MaLop)
                                ON UPDATE CASCADE ON DELETE NO ACTION
                        );

                        INSERT INTO SINHVIEN (MaSV, HoTen, NgaySinh, GioiTinh, DiaChi, SoDienThoai, MaLop) VALUES
                            (N'SV001', N'Nguyễn Văn An', '2004-01-15', N'Nam', N'Hà Nội', N'0912345678', N'CNTT1'),
                            (N'SV002', N'Trần Thị Bình', '2004-03-22', N'Nữ', N'Đà Nẵng', N'0987654321', N'CNTT1'),
                            (N'SV003', N'Lê Văn Cường', '2003-11-05', N'Nam', N'Hồ Chí Minh', N'0901234567', N'CNTT2'),
                            (N'SV004', N'Phạm Thị Dung', '2004-07-19', N'Nữ', N'Hải Phòng', N'0934567890', N'CNTT2'),
                            (N'SV005', N'Hoàng Văn Em', '2004-09-30', N'Nam', N'Cần Thơ', N'0945678901', N'CNTT3');
                    END";
                using (SqlCommand cmd = new SqlCommand(initSinhVien, appConn)) { cmd.ExecuteNonQuery(); }
            }
        }

        private static string BuildConnectionString(string server, string database)
        {
            return $"Server={server};Database={database};Integrated Security=True;TrustServerCertificate=True;";
        }

        private static string GetConfigPath()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            return Path.Combine(baseDir, ConfigFileName);
        }

        private static string? LoadConfiguredServer()
        {
            try
            {
                string path = GetConfigPath();
                if (File.Exists(path))
                {
                    string json = File.ReadAllText(path);
                    var config = JsonSerializer.Deserialize<DbConfig>(json);
                    return config?.Server;
                }
            }
            catch { }
            return null;
        }

        private static void SaveConfiguredServer(string server)
        {
            try
            {
                string path = GetConfigPath();
                var config = new DbConfig
                {
                    Server = server,
                    Database = "QLSinhVien",
                    IntegratedSecurity = true,
                    TrustServerCertificate = true
                };
                string json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(path, json);
            }
            catch { }
        }

        /// <summary>
        /// Thực thi câu lệnh SELECT trả về một DataTable
        /// </summary>
        public static DataTable ExecuteQuery(string query, SqlParameter[]? parameters = null)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null && parameters.Length > 0)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            return dataTable;
        }

        /// <summary>
        /// Thực thi câu lệnh INSERT, UPDATE, DELETE trả về số dòng bị ảnh hưởng
        /// </summary>
        public static int ExecuteNonQuery(string query, SqlParameter[]? parameters = null)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null && parameters.Length > 0)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Thực thi câu lệnh truy vấn trả về giá trị ô đầu tiên (ExecuteScalar)
        /// </summary>
        public static object? ExecuteScalar(string query, SqlParameter[]? parameters = null)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null && parameters.Length > 0)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    return cmd.ExecuteScalar();
                }
            }
        }
    }
}
