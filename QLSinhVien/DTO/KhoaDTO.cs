using System;

namespace QLSinhVien.DTO
{
    /// <summary>
    /// Đối tượng truyền tải dữ liệu Khoa
    /// Thành viên phụ trách: Chu Hữu Phong
    /// Chức năng: Quản lý Khoa / Đơn vị đào tạo
    /// </summary>
    public class KhoaDTO
    {
        public string MaKhoa { get; set; } = string.Empty;
        public string TenKhoa { get; set; } = string.Empty;
        public string? GhiChu { get; set; }

        public KhoaDTO()
        {
        }

        public KhoaDTO(string maKhoa, string tenKhoa, string? ghiChu = null)
        {
            MaKhoa = maKhoa;
            TenKhoa = tenKhoa;
            GhiChu = ghiChu;
        }

        public override string ToString()
        {
            return TenKhoa;
        }
    }
}
