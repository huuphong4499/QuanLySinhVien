using System;
using System.Collections.Generic;
using System.Data;
using QLSinhVien.DAL;
using QLSinhVien.DTO;

namespace QLSinhVien.BLL
{
    /// <summary>
    /// Lớp xử lý nghiệp vụ Lớp học
    /// Thành viên phụ trách: Phan Nhựt An
    /// Chức năng: Kiểm tra quy tắc nghiệp vụ, điều phối dữ liệu phân hệ Lớp học
    /// </summary>
    public class LopBLL
    {
        private readonly LopDAL _lopDAL;

        public LopBLL()
        {
            _lopDAL = new LopDAL();
        }

        /// <summary>
        /// Lấy toàn bộ danh sách lớp kèm tên khoa hiển thị lên DataGridView
        /// </summary>
        public DataTable LayDanhSachLop()
        {
            return _lopDAL.LayTatCaLop();
        }

        /// <summary>
        /// Lấy danh sách lớp đơn giản để đổ dữ liệu cho ComboBox trên Form Quản lý Sinh viên
        /// </summary>
        public DataTable LayDanhSachLopChoComboBox()
        {
            return _lopDAL.LayDanhSachLopChoComboBox();
        }

        /// <summary>
        /// Thêm lớp học mới với các bước kiểm tra nghiệp vụ chặt chẽ
        /// </summary>
        public (bool ThanhCong, string ThongBao) ThemLop(LopDTO lop)
        {
            if (lop == null)
                return (false, "Dữ liệu lớp học không hợp lệ!");

            if (string.IsNullOrWhiteSpace(lop.MaLop))
                return (false, "Mã lớp không được để trống!");

            if (string.IsNullOrWhiteSpace(lop.TenLop))
                return (false, "Tên lớp không được để trống!");

            if (string.IsNullOrWhiteSpace(lop.MaKhoa))
                return (false, "Vui lòng chọn khoa trực thuộc!");

            lop.MaLop = lop.MaLop.Trim().ToUpper();
            lop.TenLop = lop.TenLop.Trim();
            lop.MaKhoa = lop.MaKhoa.Trim();

            if (lop.MaLop.Length > 20)
                return (false, "Mã lớp không được dài quá 20 ký tự!");

            try
            {
                if (_lopDAL.KiemTraTonTai(lop.MaLop))
                {
                    return (false, $"Mã lớp '{lop.MaLop}' đã tồn tại! Vui lòng chọn mã khác.");
                }

                bool kq = _lopDAL.ThemLop(lop);
                return kq
                    ? (true, "Thêm lớp học mới thành công!")
                    : (false, "Không thể thêm lớp học, vui lòng thử lại!");
            }
            catch (Exception ex)
            {
                return (false, "Lỗi nghiệp vụ thêm lớp: " + ex.Message);
            }
        }

        /// <summary>
        /// Cập nhật thông tin lớp học
        /// </summary>
        public (bool ThanhCong, string ThongBao) CapNhatLop(LopDTO lop)
        {
            if (lop == null)
                return (false, "Dữ liệu lớp học không hợp lệ!");

            if (string.IsNullOrWhiteSpace(lop.MaLop))
                return (false, "Vui lòng chọn hoặc nhập mã lớp cần sửa!");

            if (string.IsNullOrWhiteSpace(lop.TenLop))
                return (false, "Tên lớp không được để trống!");

            if (string.IsNullOrWhiteSpace(lop.MaKhoa))
                return (false, "Vui lòng chọn khoa trực thuộc!");

            lop.MaLop = lop.MaLop.Trim().ToUpper();
            lop.TenLop = lop.TenLop.Trim();
            lop.MaKhoa = lop.MaKhoa.Trim();

            try
            {
                if (!_lopDAL.KiemTraTonTai(lop.MaLop))
                {
                    return (false, $"Không tìm thấy lớp học có mã '{lop.MaLop}' để sửa!");
                }

                bool kq = _lopDAL.CapNhatLop(lop);
                return kq
                    ? (true, "Cập nhật thông tin lớp thành công!")
                    : (false, "Cập nhật lớp thất bại!");
            }
            catch (Exception ex)
            {
                return (false, "Lỗi nghiệp vụ sửa lớp: " + ex.Message);
            }
        }

        /// <summary>
        /// Xóa lớp học có kiểm tra ràng buộc sinh viên
        /// </summary>
        public (bool ThanhCong, string ThongBao) XoaLop(string maLop)
        {
            if (string.IsNullOrWhiteSpace(maLop))
                return (false, "Vui lòng chọn lớp cần xóa!");

            maLop = maLop.Trim().ToUpper();

            try
            {
                if (!_lopDAL.KiemTraTonTai(maLop))
                {
                    return (false, $"Không tìm thấy lớp học có mã '{maLop}' cần xóa!");
                }

                int soSinhVien = _lopDAL.DemSoSinhVienTrongLop(maLop);
                if (soSinhVien > 0)
                {
                    return (false, $"Không thể xóa lớp '{maLop}' vì đang có {soSinhVien} sinh viên thuộc lớp này!\nVui lòng chuyển hoặc xóa sinh viên trước.");
                }

                bool kq = _lopDAL.XoaLop(maLop);
                return kq
                    ? (true, "Xóa lớp học thành công!")
                    : (false, "Xóa lớp không thành công!");
            }
            catch (Exception ex)
            {
                return (false, "Lỗi nghiệp vụ xóa lớp: " + ex.Message);
            }
        }
    }
}
