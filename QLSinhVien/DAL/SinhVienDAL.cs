using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using QLSinhVien.DTO;

namespace QLSinhVien.DAL
{
    /// <summary>
    /// Lớp truy xuất dữ liệu Sinh viên
    /// Thành viên phụ trách: Trần Thành Long
    /// Chức năng: Thao tác cơ sở dữ liệu bảng SINHVIEN
    /// </summary>
    public class SinhVienDAL
    {
        /// <summary>
        /// Lấy toàn bộ danh sách sinh viên dưới dạng DataTable để hiển thị lên DataGridView
        /// </summary>
        public DataTable LayTatCaSinhVien()
        {
            try
            {
                string sql = @"
                    SELECT MaSV, HoTen, NgaySinh, GioiTinh, DiaChi, SoDienThoai, MaLop
                    FROM SINHVIEN
                    ORDER BY MaSV ASC";

                return Database.ExecuteQuery(sql);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi truy vấn danh sách sinh viên: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Tìm kiếm sinh viên theo từ khóa (Mã SV hoặc Họ tên)
        /// </summary>
        public DataTable TimKiemSinhVien(string tuKhoa)
        {
            try
            {
                string sql = @"
                    SELECT MaSV, HoTen, NgaySinh, GioiTinh, DiaChi, SoDienThoai, MaLop
                    FROM SINHVIEN
                    WHERE MaSV LIKE @TuKhoa OR HoTen LIKE @TuKhoa
                    ORDER BY MaSV ASC";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@TuKhoa", SqlDbType.NVarChar, 100) { Value = "%" + tuKhoa + "%" }
                };

                return Database.ExecuteQuery(sql, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi tìm kiếm sinh viên: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Lấy danh sách sinh viên theo mã lớp
        /// </summary>
        public DataTable LaySinhVienTheoLop(string maLop)
        {
            try
            {
                string sql = @"
                    SELECT MaSV, HoTen, NgaySinh, GioiTinh, DiaChi, SoDienThoai, MaLop
                    FROM SINHVIEN
                    WHERE MaLop = @MaLop
                    ORDER BY MaSV ASC";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaLop", SqlDbType.NVarChar, 50) { Value = maLop }
                };

                return Database.ExecuteQuery(sql, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lọc sinh viên theo lớp: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Lấy thông tin một sinh viên theo mã sinh viên
        /// </summary>
        public SinhVienDTO? LaySinhVienTheoMa(string maSV)
        {
            try
            {
                string sql = @"
                    SELECT TOP 1 MaSV, HoTen, NgaySinh, GioiTinh, DiaChi, SoDienThoai, MaLop
                    FROM SINHVIEN
                    WHERE MaSV = @MaSV";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaSV", SqlDbType.NVarChar, 50) { Value = maSV }
                };

                DataTable dt = Database.ExecuteQuery(sql, parameters);
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    return new SinhVienDTO
                    {
                        MaSV = row["MaSV"]?.ToString() ?? "",
                        HoTen = row["HoTen"]?.ToString() ?? "",
                        NgaySinh = row["NgaySinh"] != DBNull.Value ? Convert.ToDateTime(row["NgaySinh"]) : DateTime.Today,
                        GioiTinh = row["GioiTinh"]?.ToString() ?? "Nam",
                        DiaChi = row["DiaChi"] != DBNull.Value ? row["DiaChi"].ToString() : null,
                        SoDienThoai = row["SoDienThoai"] != DBNull.Value ? row["SoDienThoai"].ToString() : null,
                        MaLop = row["MaLop"]?.ToString() ?? ""
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy thông tin sinh viên theo mã: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Kiểm tra mã sinh viên đã tồn tại trong CSDL hay chưa
        /// </summary>
        public bool KiemTraTonTai(string maSV)
        {
            try
            {
                string sql = "SELECT COUNT(*) FROM SINHVIEN WHERE MaSV = @MaSV";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaSV", SqlDbType.NVarChar, 50) { Value = maSV }
                };

                object? result = Database.ExecuteScalar(sql, parameters);
                return (result != null && Convert.ToInt32(result) > 0);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi kiểm tra mã sinh viên: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Thêm sinh viên mới vào cơ sở dữ liệu
        /// </summary>
        public bool ThemSinhVien(SinhVienDTO sv)
        {
            try
            {
                string sql = @"
                    INSERT INTO SINHVIEN (MaSV, HoTen, NgaySinh, GioiTinh, DiaChi, SoDienThoai, MaLop)
                    VALUES (@MaSV, @HoTen, @NgaySinh, @GioiTinh, @DiaChi, @SoDienThoai, @MaLop)";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaSV", SqlDbType.NVarChar, 50) { Value = sv.MaSV },
                    new SqlParameter("@HoTen", SqlDbType.NVarChar, 100) { Value = sv.HoTen },
                    new SqlParameter("@NgaySinh", SqlDbType.Date) { Value = sv.NgaySinh.Date },
                    new SqlParameter("@GioiTinh", SqlDbType.NVarChar, 10) { Value = sv.GioiTinh },
                    new SqlParameter("@DiaChi", SqlDbType.NVarChar, 200) { Value = (object?)sv.DiaChi ?? DBNull.Value },
                    new SqlParameter("@SoDienThoai", SqlDbType.NVarChar, 20) { Value = (object?)sv.SoDienThoai ?? DBNull.Value },
                    new SqlParameter("@MaLop", SqlDbType.NVarChar, 50) { Value = sv.MaLop }
                };

                return Database.ExecuteNonQuery(sql, parameters) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi thêm sinh viên: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Cập nhật thông tin sinh viên
        /// </summary>
        public bool CapNhatSinhVien(SinhVienDTO sv)
        {
            try
            {
                string sql = @"
                    UPDATE SINHVIEN
                    SET HoTen = @HoTen,
                        NgaySinh = @NgaySinh,
                        GioiTinh = @GioiTinh,
                        DiaChi = @DiaChi,
                        SoDienThoai = @SoDienThoai,
                        MaLop = @MaLop
                    WHERE MaSV = @MaSV";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaSV", SqlDbType.NVarChar, 50) { Value = sv.MaSV },
                    new SqlParameter("@HoTen", SqlDbType.NVarChar, 100) { Value = sv.HoTen },
                    new SqlParameter("@NgaySinh", SqlDbType.Date) { Value = sv.NgaySinh.Date },
                    new SqlParameter("@GioiTinh", SqlDbType.NVarChar, 10) { Value = sv.GioiTinh },
                    new SqlParameter("@DiaChi", SqlDbType.NVarChar, 200) { Value = (object?)sv.DiaChi ?? DBNull.Value },
                    new SqlParameter("@SoDienThoai", SqlDbType.NVarChar, 20) { Value = (object?)sv.SoDienThoai ?? DBNull.Value },
                    new SqlParameter("@MaLop", SqlDbType.NVarChar, 50) { Value = sv.MaLop }
                };

                return Database.ExecuteNonQuery(sql, parameters) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi cập nhật sinh viên: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Xóa sinh viên khỏi cơ sở dữ liệu
        /// </summary>
        public bool XoaSinhVien(string maSV)
        {
            try
            {
                string sql = "DELETE FROM SINHVIEN WHERE MaSV = @MaSV";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaSV", SqlDbType.NVarChar, 50) { Value = maSV }
                };

                return Database.ExecuteNonQuery(sql, parameters) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi xóa sinh viên: " + ex.Message, ex);
            }
        }
    }
}
