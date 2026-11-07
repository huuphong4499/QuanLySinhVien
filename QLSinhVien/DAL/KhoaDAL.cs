using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using QLSinhVien.DTO;

namespace QLSinhVien.DAL
{
    /// <summary>
    /// Lớp truy xuất dữ liệu Khoa
    /// Thành viên phụ trách: Chu Hữu Phong
    /// Chức năng: Thao tác cơ sở dữ liệu bảng KHOA
    /// </summary>
    public class KhoaDAL
    {
        /// <summary>
        /// Lấy toàn bộ danh sách khoa dưới dạng DataTable để đổ vào ComboBox hoặc DataGridView
        /// </summary>
        public DataTable LayTatCaKhoa()
        {
            try
            {
                string sql = "SELECT MaKhoa, TenKhoa FROM KHOA ORDER BY TenKhoa ASC";
                return Database.ExecuteQuery(sql);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi truy vấn danh sách khoa: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Lấy danh sách các đối tượng KhoaDTO
        /// </summary>
        public List<KhoaDTO> LayDanhSachKhoa()
        {
            List<KhoaDTO> list = new List<KhoaDTO>();
            try
            {
                string sql = "SELECT MaKhoa, TenKhoa FROM KHOA ORDER BY TenKhoa ASC";
                DataTable dt = Database.ExecuteQuery(sql);
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new KhoaDTO
                    {
                        MaKhoa = row["MaKhoa"]?.ToString() ?? "",
                        TenKhoa = row["TenKhoa"]?.ToString() ?? ""
                    });
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi đọc danh sách đối tượng khoa: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Lấy thông tin một khoa dựa trên Mã khoa
        /// </summary>
        public KhoaDTO? LayKhoaTheoMa(string maKhoa)
        {
            try
            {
                string sql = "SELECT TOP 1 MaKhoa, TenKhoa FROM KHOA WHERE MaKhoa = @MaKhoa";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaKhoa", SqlDbType.NVarChar, 50) { Value = maKhoa }
                };

                DataTable dt = Database.ExecuteQuery(sql, parameters);
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    return new KhoaDTO
                    {
                        MaKhoa = row["MaKhoa"]?.ToString() ?? "",
                        TenKhoa = row["TenKhoa"]?.ToString() ?? ""
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy thông tin khoa theo mã: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Kiểm tra mã khoa đã tồn tại hay chưa
        /// </summary>
        public bool KiemTraTonTai(string maKhoa)
        {
            try
            {
                string sql = "SELECT COUNT(*) FROM KHOA WHERE MaKhoa = @MaKhoa";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaKhoa", SqlDbType.NVarChar, 50) { Value = maKhoa }
                };

                object? result = Database.ExecuteScalar(sql, parameters);
                return (result != null && Convert.ToInt32(result) > 0);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi kiểm tra mã khoa tồn tại: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Đếm số lớp thuộc về một khoa để kiểm tra ràng buộc trước khi xóa
        /// </summary>
        public int DemSoLopThuocKhoa(string maKhoa)
        {
            try
            {
                string sql = "SELECT COUNT(*) FROM LOP WHERE MaKhoa = @MaKhoa";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaKhoa", SqlDbType.NVarChar, 50) { Value = maKhoa }
                };

                object? result = Database.ExecuteScalar(sql, parameters);
                return (result != null && result != DBNull.Value) ? Convert.ToInt32(result) : 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi đếm số lớp thuộc khoa: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Thêm một khoa mới
        /// </summary>
        public bool ThemKhoa(KhoaDTO khoa)
        {
            try
            {
                string sql = "INSERT INTO KHOA (MaKhoa, TenKhoa) VALUES (@MaKhoa, @TenKhoa)";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaKhoa", SqlDbType.NVarChar, 50) { Value = khoa.MaKhoa },
                    new SqlParameter("@TenKhoa", SqlDbType.NVarChar, 100) { Value = khoa.TenKhoa }
                };

                return Database.ExecuteNonQuery(sql, parameters) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi thêm khoa: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Cập nhật thông tin khoa
        /// </summary>
        public bool CapNhatKhoa(KhoaDTO khoa)
        {
            try
            {
                string sql = "UPDATE KHOA SET TenKhoa = @TenKhoa WHERE MaKhoa = @MaKhoa";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaKhoa", SqlDbType.NVarChar, 50) { Value = khoa.MaKhoa },
                    new SqlParameter("@TenKhoa", SqlDbType.NVarChar, 100) { Value = khoa.TenKhoa }
                };

                return Database.ExecuteNonQuery(sql, parameters) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi sửa khoa: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Xóa khoa
        /// </summary>
        public bool XoaKhoa(string maKhoa)
        {
            try
            {
                string sql = "DELETE FROM KHOA WHERE MaKhoa = @MaKhoa";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaKhoa", SqlDbType.NVarChar, 50) { Value = maKhoa }
                };

                return Database.ExecuteNonQuery(sql, parameters) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi xóa khoa: " + ex.Message, ex);
            }
        }
    }
}
