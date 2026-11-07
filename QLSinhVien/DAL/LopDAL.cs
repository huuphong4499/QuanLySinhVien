using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using QLSinhVien.DTO;

namespace QLSinhVien.DAL
{
    /// <summary>
    /// Lớp truy xuất dữ liệu Lớp học
    /// Thành viên phụ trách: Phan Nhựt An
    /// Chức năng: Thao tác cơ sở dữ liệu bảng LOP
    /// </summary>
    public class LopDAL
    {
        /// <summary>
        /// Lấy toàn bộ danh sách lớp kèm thông tin tên khoa dưới dạng DataTable để hiển thị lên DataGridView
        /// </summary>
        public DataTable LayTatCaLop()
        {
            try
            {
                string sql = @"
                    SELECT L.MaLop, L.TenLop, L.MaKhoa, K.TenKhoa
                    FROM LOP L
                    LEFT JOIN KHOA K ON L.MaKhoa = K.MaKhoa
                    ORDER BY L.MaLop ASC";

                return Database.ExecuteQuery(sql);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi truy vấn danh sách lớp: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Lấy danh sách lớp đơn giản để đổ dữ liệu vào ComboBox chọn lớp
        /// </summary>
        public DataTable LayDanhSachLopChoComboBox()
        {
            try
            {
                string sql = "SELECT MaLop, TenLop FROM LOP ORDER BY TenLop ASC";
                return Database.ExecuteQuery(sql);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi truy vấn danh sách lớp cho ComboBox: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Lấy danh sách lớp dưới dạng List DTO
        /// </summary>
        public List<LopDTO> LayDanhSachLopDTO()
        {
            List<LopDTO> list = new List<LopDTO>();
            try
            {
                string sql = @"
                    SELECT L.MaLop, L.TenLop, L.MaKhoa, K.TenKhoa
                    FROM LOP L
                    LEFT JOIN KHOA K ON L.MaKhoa = K.MaKhoa
                    ORDER BY L.MaLop ASC";

                DataTable dt = Database.ExecuteQuery(sql);
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new LopDTO
                    {
                        MaLop = row["MaLop"]?.ToString() ?? "",
                        TenLop = row["TenLop"]?.ToString() ?? "",
                        MaKhoa = row["MaKhoa"]?.ToString() ?? "",
                        TenKhoa = row["TenKhoa"]?.ToString()
                    });
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi đọc danh sách đối tượng lớp: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Lấy thông tin lớp học theo mã lớp
        /// </summary>
        public LopDTO? LayLopTheoMa(string maLop)
        {
            try
            {
                string sql = @"
                    SELECT TOP 1 L.MaLop, L.TenLop, L.MaKhoa, K.TenKhoa
                    FROM LOP L
                    LEFT JOIN KHOA K ON L.MaKhoa = K.MaKhoa
                    WHERE L.MaLop = @MaLop";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaLop", SqlDbType.NVarChar, 50) { Value = maLop }
                };

                DataTable dt = Database.ExecuteQuery(sql, parameters);
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    return new LopDTO
                    {
                        MaLop = row["MaLop"]?.ToString() ?? "",
                        TenLop = row["TenLop"]?.ToString() ?? "",
                        MaKhoa = row["MaKhoa"]?.ToString() ?? "",
                        TenKhoa = row["TenKhoa"]?.ToString()
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy thông tin lớp theo mã: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Kiểm tra mã lớp đã tồn tại trong CSDL hay chưa
        /// </summary>
        public bool KiemTraTonTai(string maLop)
        {
            try
            {
                string sql = "SELECT COUNT(*) FROM LOP WHERE MaLop = @MaLop";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaLop", SqlDbType.NVarChar, 50) { Value = maLop }
                };

                object? result = Database.ExecuteScalar(sql, parameters);
                return (result != null && Convert.ToInt32(result) > 0);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi kiểm tra mã lớp: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Đếm số sinh viên đang thuộc lớp để bảo vệ ràng buộc toàn vẹn khi xóa
        /// </summary>
        public int DemSoSinhVienTrongLop(string maLop)
        {
            try
            {
                string sql = "SELECT COUNT(*) FROM SINHVIEN WHERE MaLop = @MaLop";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaLop", SqlDbType.NVarChar, 50) { Value = maLop }
                };

                object? result = Database.ExecuteScalar(sql, parameters);
                return (result != null && result != DBNull.Value) ? Convert.ToInt32(result) : 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi đếm số sinh viên trong lớp: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Thêm lớp mới
        /// </summary>
        public bool ThemLop(LopDTO lop)
        {
            try
            {
                string sql = "INSERT INTO LOP (MaLop, TenLop, MaKhoa) VALUES (@MaLop, @TenLop, @MaKhoa)";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaLop", SqlDbType.NVarChar, 50) { Value = lop.MaLop },
                    new SqlParameter("@TenLop", SqlDbType.NVarChar, 100) { Value = lop.TenLop },
                    new SqlParameter("@MaKhoa", SqlDbType.NVarChar, 50) { Value = lop.MaKhoa }
                };

                return Database.ExecuteNonQuery(sql, parameters) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi thêm lớp: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Cập nhật thông tin lớp
        /// </summary>
        public bool CapNhatLop(LopDTO lop)
        {
            try
            {
                string sql = "UPDATE LOP SET TenLop = @TenLop, MaKhoa = @MaKhoa WHERE MaLop = @MaLop";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaLop", SqlDbType.NVarChar, 50) { Value = lop.MaLop },
                    new SqlParameter("@TenLop", SqlDbType.NVarChar, 100) { Value = lop.TenLop },
                    new SqlParameter("@MaKhoa", SqlDbType.NVarChar, 50) { Value = lop.MaKhoa }
                };

                return Database.ExecuteNonQuery(sql, parameters) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi sửa thông tin lớp: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Xóa lớp khỏi cơ sở dữ liệu
        /// </summary>
        public bool XoaLop(string maLop)
        {
            try
            {
                string sql = "DELETE FROM LOP WHERE MaLop = @MaLop";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaLop", SqlDbType.NVarChar, 50) { Value = maLop }
                };

                return Database.ExecuteNonQuery(sql, parameters) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi xóa lớp: " + ex.Message, ex);
            }
        }
    }
}
