namespace QLSinhVien.DTO;
public class DiemDTO
{
    public string MaSV { get; set; } = "";
    public string MaMon { get; set; } = "";
    public decimal ChuyenCan { get; set; }
    public decimal GiuaKy { get; set; }
    public decimal CuoiKy { get; set; }
    public decimal DiemTB => Math.Round(ChuyenCan * .2m + GiuaKy * .2m + CuoiKy * .6m, 2);
}
