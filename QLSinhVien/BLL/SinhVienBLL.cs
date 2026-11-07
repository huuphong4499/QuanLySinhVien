using System;
using System.Data;
using System.Text.RegularExpressions;
using QLSinhVien.DAL;
using QLSinhVien.DTO;

namespace QLSinhVien.BLL
{
    /// <summary>
    /// Lớp xử lý nghiệp vụ Quản lý Sinh viên
    /// Thành viên phụ trách: Trần Thành Long
    /// Chức năng: Kiểm tra tính hợp lệ dữ liệu hồ sơ sinh viên, thực thi nghiệp vụ Thêm, Sửa, Xóa, Tìm kiếm
    /// </summary>
    public class SinhVienBLL
    {
        private readonly SinhVienDAL _sinhVienDAL;

        public SinhVienBLL()
        {
            _sinhVienDAL = new SinhVienDAL();
        }

        /// <summary>
        /// Lấy toàn bộ danh sách sinh viên để hiển thị lên bảng
        /// </summary>
        public DataTable LayDanhSachSinhVien()
        {
            return _sinhVienDAL.LayTatCaSinhVien();
        }

        /// <summary>
        /// Tìm kiếm sinh viên theo từ khóa mã hoặc họ tên
        /// </summary>
        public DataTable TimKiemSinhVien(string tuKhoa)
        {
            tuKhoa = tuKhoa?.Trim() ?? string.Empty;
            if (string.IsNullOrEmpty(tuKhoa))
            {
                return _sinhVienDAL.LayTatCaSinhVien();
            }
            return _sinhVienDAL.TimKiemSinhVien(tuKhoa);
        }

        /// <summary>
        /// Thêm sinh viên mới với kiểm tra toàn diện quy tắc nghiệp vụ
        /// </summary>
        public (bool ThanhCong, string ThongBao) ThemSinhVien(SinhVienDTO sv)
        {
            var validate = KiemTraDuLieu(sv, isUpdate: false);
            if (!validate.HopLe)
            {
                return (false, validate.ThongBao);
            }

            try
            {
                if (_sinhVienDAL.KiemTraTonTai(sv.MaSV))
                {
                    return (false, $"Mã sinh viên '{sv.MaSV}' đã tồn tại trong hệ thống! Vui lòng chọn mã khác.");
                }

                bool kq = _sinhVienDAL.ThemSinhVien(sv);
                return kq
                    ? (true, "Thêm sinh viên thành công!")
                    : (false, "Thêm sinh viên thất bại, vui lòng thử lại!");
            }
            catch (Exception ex)
            {
                return (false, "Lỗi nghiệp vụ thêm sinh viên: " + ex.Message);
            }
        }

        /// <summary>
        /// Cập nhật thông tin sinh viên
        /// </summary>
        public (bool ThanhCong, string ThongBao) CapNhatSinhVien(SinhVienDTO sv)
        {
            var validate = KiemTraDuLieu(sv, isUpdate: true);
            if (!validate.HopLe)
            {
                return (false, validate.ThongBao);
            }

            try
            {
                if (!_sinhVienDAL.KiemTraTonTai(sv.MaSV))
                {
                    return (false, $"Không tìm thấy sinh viên có mã '{sv.MaSV}' để sửa!");
                }

                bool kq = _sinhVienDAL.CapNhatSinhVien(sv);
                return kq
                    ? (true, "Cập nhật thông tin sinh viên thành công!")
                    : (false, "Cập nhật sinh viên thất bại!");
            }
            catch (Exception ex)
            {
                return (false, "Lỗi nghiệp vụ sửa sinh viên: " + ex.Message);
            }
        }

        /// <summary>
        /// Xóa sinh viên khỏi hệ thống
        /// </summary>
        public (bool ThanhCong, string ThongBao) XoaSinhVien(string maSV)
        {
            if (string.IsNullOrWhiteSpace(maSV))
            {
                return (false, "Vui lòng chọn sinh viên cần xóa!");
            }

            maSV = maSV.Trim().ToUpper();

            try
            {
                if (!_sinhVienDAL.KiemTraTonTai(maSV))
                {
                    return (false, $"Không tìm thấy sinh viên có mã '{maSV}' để xóa!");
                }

                bool kq = _sinhVienDAL.XoaSinhVien(maSV);
                return kq
                    ? (true, "Xóa sinh viên thành công!")
                    : (false, "Xóa sinh viên không thành công!");
            }
            catch (Exception ex)
            {
                return (false, "Lỗi nghiệp vụ xóa sinh viên: " + ex.Message);
            }
        }

        /// <summary>
        /// Kiểm tra tính hợp lệ nghiệp vụ của hồ sơ sinh viên
        /// </summary>
        private (bool HopLe, string ThongBao) KiemTraDuLieu(SinhVienDTO sv, bool isUpdate)
        {
            if (sv == null)
                return (false, "Dữ liệu sinh viên không hợp lệ!");

            // 1. Kiểm tra mã sinh viên
            if (string.IsNullOrWhiteSpace(sv.MaSV))
                return (false, "Vui lòng nhập mã sinh viên!");

            sv.MaSV = sv.MaSV.Trim().ToUpper();
            if (sv.MaSV.Length < 3 || sv.MaSV.Length > 20)
                return (false, "Mã sinh viên phải từ 3 đến 20 ký tự!");

            if (sv.MaSV.Contains(" "))
                return (false, "Mã sinh viên không được chứa khoảng trắng!");

            // 2. Kiểm tra họ tên
            if (string.IsNullOrWhiteSpace(sv.HoTen))
                return (false, "Vui lòng nhập họ tên sinh viên!");

            sv.HoTen = sv.HoTen.Trim();
            if (sv.HoTen.Length < 2 || sv.HoTen.Length > 100)
                return (false, "Họ và tên sinh viên phải từ 2 đến 100 ký tự!");

            // 3. Kiểm tra ngày sinh / độ tuổi
            int tuoi = DateTime.Today.Year - sv.NgaySinh.Year;
            if (sv.NgaySinh > DateTime.Today.AddYears(-tuoi)) tuoi--;

            if (tuoi < 15 || tuoi > 80)
                return (false, $"Ngày sinh không hợp lý (Tuổi hiện tại: {tuoi}). Độ tuổi sinh viên hợp lệ từ 15 đến 80 tuổi!");

            // 4. Kiểm tra giới tính
            if (string.IsNullOrWhiteSpace(sv.GioiTinh))
                return (false, "Vui lòng chọn giới tính sinh viên!");

            // 5. Kiểm tra lớp học
            if (string.IsNullOrWhiteSpace(sv.MaLop))
                return (false, "Vui lòng chọn lớp cho sinh viên!");

            sv.MaLop = sv.MaLop.Trim();

            // 6. Kiểm tra số điện thoại (nếu có nhập)
            if (!string.IsNullOrWhiteSpace(sv.SoDienThoai))
            {
                sv.SoDienThoai = sv.SoDienThoai.Trim();
                if (!Regex.IsMatch(sv.SoDienThoai, @"^0[0-9]{9}$"))
                {
                    return (false, "Số điện thoại không hợp lệ! Vui lòng nhập số điện thoại gồm 10 chữ số và bắt đầu bằng số 0.");
                }
            }
            else
            {
                sv.SoDienThoai = null;
            }

            // Chuẩn hóa địa chỉ
            if (!string.IsNullOrWhiteSpace(sv.DiaChi))
            {
                sv.DiaChi = sv.DiaChi.Trim();
            }
            else
            {
                sv.DiaChi = null;
            }

            return (true, "Dữ liệu hợp lệ!");
        }
    }
}
