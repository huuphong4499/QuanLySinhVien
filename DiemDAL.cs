using System.Data;
using Microsoft.Data.SqlClient;
using QLSinhVien.DTO;
namespace QLSinhVien.DAL;
public class DiemDAL
{
    public DataTable DanhSach(string keyword = "") => Database.ExecuteQuery(@"
SELECT d.MaSV, s.HoTen, d.MaMon, m.TenMon, d.ChuyenCan, d.GiuaKy, d.CuoiKy, d.DiemTB
FROM dbo.DIEM d JOIN dbo.SINHVIEN s ON s.MaSV=d.MaSV JOIN dbo.MONHOC m ON m.MaMon=d.MaMon
WHERE @TuKhoa = N'' OR s.MaSV LIKE N'%' + @TuKhoa + N'%' OR s.HoTen LIKE N'%' + @TuKhoa + N'%' OR m.TenMon LIKE N'%' + @TuKhoa + N'%'
ORDER BY s.MaSV, m.MaMon", new[] { new SqlParameter("@TuKhoa", SqlDbType.NVarChar, 100) { Value = keyword } });
    public DataTable SinhVien() => Database.ExecuteQuery("SELECT MaSV, MaSV + N' - ' + HoTen AS HienThi FROM dbo.SINHVIEN ORDER BY MaSV");
    public DataTable MonHoc() => Database.ExecuteQuery("SELECT MaMon, MaMon + N' - ' + TenMon AS HienThi FROM dbo.MONHOC ORDER BY MaMon");
    private static SqlParameter[] ThamSo(DiemDTO d) => new[] {
        new SqlParameter("@MaSV", SqlDbType.NVarChar,50){Value=d.MaSV},
        new SqlParameter("@MaMon", SqlDbType.NVarChar,50){Value=d.MaMon},
        new SqlParameter("@CC", SqlDbType.Decimal){Precision=4,Scale=2,Value=d.ChuyenCan},
        new SqlParameter("@GK", SqlDbType.Decimal){Precision=4,Scale=2,Value=d.GiuaKy},
        new SqlParameter("@CK", SqlDbType.Decimal){Precision=4,Scale=2,Value=d.CuoiKy}
    };
    public int Them(DiemDTO d) => Database.ExecuteNonQuery("INSERT INTO dbo.DIEM(MaSV,MaMon,ChuyenCan,GiuaKy,CuoiKy) VALUES(@MaSV,@MaMon,@CC,@GK,@CK)", ThamSo(d));
    public int Sua(DiemDTO d) => Database.ExecuteNonQuery("UPDATE dbo.DIEM SET ChuyenCan=@CC,GiuaKy=@GK,CuoiKy=@CK WHERE MaSV=@MaSV AND MaMon=@MaMon", ThamSo(d));
    public int Xoa(string maSV,string maMon) => Database.ExecuteNonQuery("DELETE FROM dbo.DIEM WHERE MaSV=@MaSV AND MaMon=@MaMon", new[]{new SqlParameter("@MaSV",SqlDbType.NVarChar,50){Value=maSV},new SqlParameter("@MaMon",SqlDbType.NVarChar,50){Value=maMon}});
}
