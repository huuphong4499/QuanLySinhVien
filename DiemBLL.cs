using System.Data;
using QLSinhVien.DAL;
using QLSinhVien.DTO;
namespace QLSinhVien.BLL;
public class DiemBLL
{
    private readonly DiemDAL dal = new();
    public DataTable DanhSach(string tim = "") => dal.DanhSach(tim.Trim());
    public DataTable SinhVien() => dal.SinhVien();
    public DataTable MonHoc() => dal.MonHoc();
    private static void KiemTra(DiemDTO d)
    {
        if (string.IsNullOrWhiteSpace(d.MaSV) || string.IsNullOrWhiteSpace(d.MaMon)) throw new ArgumentException("Chọn sinh viên và môn học.");
        if (new[]{d.ChuyenCan,d.GiuaKy,d.CuoiKy}.Any(x=>x<0 || x>10)) throw new ArgumentException("Điểm phải nằm trong khoảng 0–10.");
    }
    public void Them(DiemDTO d){KiemTra(d);dal.Them(d);}
    public void Sua(DiemDTO d){KiemTra(d);if(dal.Sua(d)==0)throw new InvalidOperationException("Không tìm thấy bảng điểm cần sửa.");}
    public void Xoa(string sv,string mon){if(dal.Xoa(sv,mon)==0)throw new InvalidOperationException("Không tìm thấy bảng điểm cần xóa.");}
}
