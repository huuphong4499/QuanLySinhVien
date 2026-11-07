using System;

namespace QLSinhVien.DTO
{
    /// <summary>
    /// Đối tượng truyền tải dữ liệu Sinh viên
    /// Thành viên phụ trách: Trần Thành Long
    /// Chức năng: Quản lý thông tin hồ sơ Sinh viên
    /// </summary>
    public class SinhVienDTO
    {
        public string MaSV { get; set; } = string.Empty;
        public string HoTen { get; set; } = string.Empty;
        public DateTime NgaySinh { get; set; } = DateTime.Today;
        public string GioiTinh { get; set; } = "Nam";
        public string? DiaChi { get; set; }
        public string? SoDienThoai { get; set; }
        public string MaLop { get; set; } = string.Empty;
        public string? TenLop { get; set; }

        public SinhVienDTO()
        {
        }

        public SinhVienDTO(
            string maSV,
            string hoTen,
            DateTime ngaySinh,
            string gioiTinh,
            string? diaChi,
            string? soDienThoai,
            string maLop,
            string? tenLop = null)
        {
            MaSV = maSV;
            HoTen = hoTen;
            NgaySinh = ngaySinh;
            GioiTinh = gioiTinh;
            DiaChi = diaChi;
            SoDienThoai = soDienThoai;
            MaLop = maLop;
            TenLop = tenLop;
        }

        public override string ToString()
        {
            return $"{MaSV} - {HoTen}";
        }
    }
}
