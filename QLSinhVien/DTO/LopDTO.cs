using System;

namespace QLSinhVien.DTO
{
    /// <summary>
    /// Đối tượng truyền tải dữ liệu Lớp học
    /// Thành viên phụ trách: Phan Nhựt An
    /// Chức năng: Quản lý thông tin Lớp học
    /// </summary>
    public class LopDTO
    {
        public string MaLop { get; set; } = string.Empty;
        public string TenLop { get; set; } = string.Empty;
        public string MaKhoa { get; set; } = string.Empty;
        public string? TenKhoa { get; set; }

        public LopDTO()
        {
        }

        public LopDTO(string maLop, string tenLop, string maKhoa, string? tenKhoa = null)
        {
            MaLop = maLop;
            TenLop = tenLop;
            MaKhoa = maKhoa;
            TenKhoa = tenKhoa;
        }

        public override string ToString()
        {
            return TenLop;
        }
    }
}
