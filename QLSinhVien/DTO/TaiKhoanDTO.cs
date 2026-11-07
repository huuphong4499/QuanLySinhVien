using System;

namespace QLSinhVien.DTO
{
    /// <summary>
    /// Đối tượng truyền tải dữ liệu Tài khoản
    /// Thành viên phụ trách: An Nguyễn Lâm Khang
    /// Chức năng: Quản lý Tài khoản & Đăng nhập
    /// </summary>
    public class TaiKhoanDTO
    {
        public string TenDangNhap { get; set; } = string.Empty;
        public string MatKhau { get; set; } = string.Empty;
        public string HoTen { get; set; } = string.Empty;
        public string Quyen { get; set; } = "User";
        public bool TrangThai { get; set; } = true;

        public TaiKhoanDTO()
        {
        }

        public TaiKhoanDTO(string tenDangNhap, string matKhau)
        {
            TenDangNhap = tenDangNhap;
            MatKhau = matKhau;
        }

        public TaiKhoanDTO(string tenDangNhap, string matKhau, string hoTen, string quyen, bool trangThai = true)
        {
            TenDangNhap = tenDangNhap;
            MatKhau = matKhau;
            HoTen = hoTen;
            Quyen = quyen;
            TrangThai = trangThai;
        }
    }
}
