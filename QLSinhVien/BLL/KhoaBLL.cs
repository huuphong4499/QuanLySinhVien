using System;
using System.Collections.Generic;
using System.Data;
using QLSinhVien.DAL;
using QLSinhVien.DTO;

namespace QLSinhVien.BLL
{
    /// <summary>
    /// Lớp xử lý nghiệp vụ Khoa
    /// Thành viên phụ trách: Chu Hữu Phong
    /// Chức năng: Kiểm tra quy tắc nghiệp vụ, tính toàn vẹn dữ liệu cho phân hệ Khoa
    /// </summary>
    public class KhoaBLL
    {
        private readonly KhoaDAL _khoaDAL;

        public KhoaBLL()
        {
            _khoaDAL = new KhoaDAL();
        }

        /// <summary>
        /// Lấy bảng danh sách khoa phục vụ binding lên ComboBox trên Form Lớp
        /// </summary>
        public DataTable LayDanhSachKhoaChoComboBox()
        {
            return _khoaDAL.LayTatCaKhoa();
        }

        /// <summary>
        /// Lấy danh sách các đối tượng Khoa
        /// </summary>
        public List<KhoaDTO> LayDanhSachKhoa()
        {
            return _khoaDAL.LayDanhSachKhoa();
        }

        /// <summary>
        /// Thêm khoa mới với các bước xác thực nghiệp vụ
        /// </summary>
        public (bool ThanhCong, string ThongBao) ThemKhoa(KhoaDTO khoa)
        {
            if (khoa == null)
                return (false, "Dữ liệu khoa không hợp lệ!");

            if (string.IsNullOrWhiteSpace(khoa.MaKhoa))
                return (false, "Mã khoa không được để trống!");

            if (string.IsNullOrWhiteSpace(khoa.TenKhoa))
                return (false, "Tên khoa không được để trống!");

            khoa.MaKhoa = khoa.MaKhoa.Trim().ToUpper();
            khoa.TenKhoa = khoa.TenKhoa.Trim();

            if (khoa.MaKhoa.Length > 20)
                return (false, "Mã khoa không được vượt quá 20 ký tự!");

            try
            {
                if (_khoaDAL.KiemTraTonTai(khoa.MaKhoa))
                {
                    return (false, $"Mã khoa '{khoa.MaKhoa}' đã tồn tại trong hệ thống!");
                }

                bool ketQua = _khoaDAL.ThemKhoa(khoa);
                return ketQua
                    ? (true, "Thêm khoa mới thành công!")
                    : (false, "Thêm khoa thất bại, vui lòng thử lại!");
            }
            catch (Exception ex)
            {
                return (false, "Lỗi nghiệp vụ khoa: " + ex.Message);
            }
        }

        /// <summary>
        /// Cập nhật thông tin khoa
        /// </summary>
        public (bool ThanhCong, string ThongBao) CapNhatKhoa(KhoaDTO khoa)
        {
            if (khoa == null)
                return (false, "Dữ liệu khoa không hợp lệ!");

            if (string.IsNullOrWhiteSpace(khoa.MaKhoa))
                return (false, "Mã khoa không được để trống!");

            if (string.IsNullOrWhiteSpace(khoa.TenKhoa))
                return (false, "Tên khoa không được để trống!");

            khoa.MaKhoa = khoa.MaKhoa.Trim().ToUpper();
            khoa.TenKhoa = khoa.TenKhoa.Trim();

            try
            {
                if (!_khoaDAL.KiemTraTonTai(khoa.MaKhoa))
                {
                    return (false, $"Không tìm thấy khoa có mã '{khoa.MaKhoa}' để cập nhật!");
                }

                bool ketQua = _khoaDAL.CapNhatKhoa(khoa);
                return ketQua
                    ? (true, "Cập nhật thông tin khoa thành công!")
                    : (false, "Cập nhật khoa thất bại!");
            }
            catch (Exception ex)
            {
                return (false, "Lỗi nghiệp vụ khi sửa khoa: " + ex.Message);
            }
        }

        /// <summary>
        /// Xóa khoa có kiểm tra ràng buộc toàn vẹn dữ liệu
        /// </summary>
        public (bool ThanhCong, string ThongBao) XoaKhoa(string maKhoa)
        {
            if (string.IsNullOrWhiteSpace(maKhoa))
                return (false, "Vui lòng chọn khoa cần xóa!");

            maKhoa = maKhoa.Trim();

            try
            {
                if (!_khoaDAL.KiemTraTonTai(maKhoa))
                {
                    return (false, "Khoa cần xóa không tồn tại!");
                }

                int soLop = _khoaDAL.DemSoLopThuocKhoa(maKhoa);
                if (soLop > 0)
                {
                    return (false, $"Không thể xóa khoa này vì đang có {soLop} lớp thuộc khoa!");
                }

                bool ketQua = _khoaDAL.XoaKhoa(maKhoa);
                return ketQua
                    ? (true, "Xóa khoa thành công!")
                    : (false, "Xóa khoa không thành công!");
            }
            catch (Exception ex)
            {
                return (false, "Lỗi nghiệp vụ khi xóa khoa: " + ex.Message);
            }
        }
    }
}
