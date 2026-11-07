using System;
using System.Data;
using Microsoft.Data.SqlClient;
using QLSinhVien.DTO;

namespace QLSinhVien.DAL
{
    /// <summary>
    /// Lớp truy xuất dữ liệu Tài khoản
    /// Thành viên phụ trách: An Nguyễn Lâm Khang
    /// Chức năng: Thao tác cơ sở dữ liệu bảng TAIKHOAN
    /// </summary>
    public class TaiKhoanDAL
    {
        /// <summary>
        /// Kiểm tra thông tin đăng nhập từ cơ sở dữ liệu
        /// </summary>
        public bool KiemTraDangNhap(string tenDangNhap, string matKhau)
        {
            try
            {
                string sql = @"
                    SELECT COUNT(*) 
                    FROM TAIKHOAN 
                    WHERE TenDangNhap = @TenDangNhap 
                      AND MatKhau = @MatKhau";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@TenDangNhap", SqlDbType.NVarChar, 50) { Value = tenDangNhap },
                    new SqlParameter("@MatKhau", SqlDbType.NVarChar, 100) { Value = matKhau }
                };

                object? result = Database.ExecuteScalar(sql, parameters);
                int count = result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
                return count > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi truy vấn đăng nhập: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Lấy thông tin chi tiết một tài khoản theo tên đăng nhập
        /// </summary>
        public TaiKhoanDTO? LayThongTinTaiKhoan(string tenDangNhap)
        {
            try
            {
                string sql = @"
                    SELECT TOP 1 TenDangNhap, MatKhau
                    FROM TAIKHOAN 
                    WHERE TenDangNhap = @TenDangNhap";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@TenDangNhap", SqlDbType.NVarChar, 50) { Value = tenDangNhap }
                };

                DataTable dt = Database.ExecuteQuery(sql, parameters);
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    return new TaiKhoanDTO
                    {
                        TenDangNhap = row["TenDangNhap"]?.ToString() ?? "",
                        MatKhau = row["MatKhau"]?.ToString() ?? ""
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy thông tin tài khoản: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Kiểm tra tên đăng nhập đã tồn tại trong CSDL hay chưa
        /// </summary>
        public bool KiemTraTonTai(string tenDangNhap)
        {
            try
            {
                string sql = "SELECT COUNT(*) FROM TAIKHOAN WHERE TenDangNhap = @TenDangNhap";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@TenDangNhap", SqlDbType.NVarChar, 50) { Value = tenDangNhap }
                };

                object? result = Database.ExecuteScalar(sql, parameters);
                return (result != null && Convert.ToInt32(result) > 0);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi kiểm tra tên đăng nhập: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Thêm tài khoản mới vào cơ sở dữ liệu
        /// </summary>
        public bool ThemTaiKhoan(TaiKhoanDTO taiKhoan)
        {
            try
            {
                string sql = "INSERT INTO TAIKHOAN (TenDangNhap, MatKhau) VALUES (@TenDangNhap, @MatKhau)";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@TenDangNhap", SqlDbType.NVarChar, 50) { Value = taiKhoan.TenDangNhap },
                    new SqlParameter("@MatKhau", SqlDbType.NVarChar, 100) { Value = taiKhoan.MatKhau }
                };

                return Database.ExecuteNonQuery(sql, parameters) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi thêm tài khoản: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Đổi mật khẩu cho tài khoản
        /// </summary>
        public bool DoiMatKhau(string tenDangNhap, string matKhauMoi)
        {
            try
            {
                string sql = "UPDATE TAIKHOAN SET MatKhau = @MatKhauMoi WHERE TenDangNhap = @TenDangNhap";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@TenDangNhap", SqlDbType.NVarChar, 50) { Value = tenDangNhap },
                    new SqlParameter("@MatKhauMoi", SqlDbType.NVarChar, 100) { Value = matKhauMoi }
                };

                return Database.ExecuteNonQuery(sql, parameters) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi đổi mật khẩu: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Lấy toàn bộ danh sách tài khoản
        /// </summary>
        public DataTable LayTatCaTaiKhoan()
        {
            try
            {
                string sql = "SELECT TenDangNhap, MatKhau FROM TAIKHOAN";
                return Database.ExecuteQuery(sql);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy danh sách tài khoản: " + ex.Message, ex);
            }
        }
    }
}
