using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace QLSinhVien
{
    /// <summary>
    /// Lớp quản trị và cung cấp kết nối Cơ sở dữ liệu SQL Server tập trung cho toàn hệ thống
    /// </summary>
    public static class Database
    {
        // Chuỗi kết nối SQL Server chuẩn
        private static string connectionString =
            @"Server=localhost;Database=QLSinhVien;Integrated Security=True;TrustServerCertificate=True;";

        /// <summary>
        /// Cho phép thiết lập hoặc cập nhật chuỗi kết nối linh hoạt
        /// </summary>
        public static string ConnectionString
        {
            get => connectionString;
            set => connectionString = value;
        }

        /// <summary>
        /// Khởi tạo và trả về đối tượng SqlConnection mới
        /// </summary>
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
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
