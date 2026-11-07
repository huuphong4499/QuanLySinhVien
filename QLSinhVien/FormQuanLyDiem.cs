using System.Data;
using System.Globalization;
using System.Text;
using QLSinhVien.BLL;
using QLSinhVien.DTO;
namespace QLSinhVien;
public partial class FormQuanLyDiem : Form
{
    private readonly DiemBLL bll = new();
    private string? selectedSV, selectedMon;

    public FormQuanLyDiem()
    {
        InitializeComponent();
        numCC.ValueChanged += (_, _) => TinhTB();
        numGK.ValueChanged += (_, _) => TinhTB();
        numCK.ValueChanged += (_, _) => TinhTB();
        grid.SelectionChanged += (_, _) => ChonDong();
        btnThem.Click += (_, _) => ThaoTac(() => bll.Them(LayDiem()));
        btnSua.Click += (_, _) => ThaoTac(() => { KiemTraDaChon(); bll.Sua(LayDiem(true)); });
        btnXoa.Click += (_, _) => Xoa();
        btnLamMoi.Click += (_, _) => { selectedSV = null; selectedMon = null; Nap(); };
        btnTimKiem.Click += (_, _) => NapBang();
        btnXuat.Click += (_, _) => XuatCsv();
        Shown += (_, _) =>
        {
            try { Nap(); }
            catch (Exception ex)
            {
                MessageBox.Show("Không tải được dữ liệu. Hãy chạy script BoSungDiem_AnToan.sql trước.\n" + ex.Message);
            }
        };
        TinhTB();
    }

    private void TinhTB()=>lblTB.Text=(numCC.Value*.2m+numGK.Value*.2m+numCK.Value*.6m).ToString("0.00");
    private void Nap()
    {
        var sv=bll.SinhVien();cboSV.DataSource=sv;cboSV.DisplayMember="HienThi";cboSV.ValueMember="MaSV";
        var mon=bll.MonHoc();cboMon.DataSource=mon;cboMon.DisplayMember="HienThi";cboMon.ValueMember="MaMon";
        NapBang();
    }
    private void NapBang(){grid.DataSource=bll.DanhSach(txtTim.Text);grid.ClearSelection();}
    private void CapNhatChon(){ /* Thay đổi lựa chọn không tự thay đổi bản ghi đã chọn */ }
    private DiemDTO LayDiem(bool dungKhoaCu=false)=>new(){MaSV=dungKhoaCu?selectedSV??"":cboSV.SelectedValue?.ToString()??"",MaMon=dungKhoaCu?selectedMon??"":cboMon.SelectedValue?.ToString()??"",ChuyenCan=numCC.Value,GiuaKy=numGK.Value,CuoiKy=numCK.Value};
    private void KiemTraDaChon(){if(selectedSV==null||selectedMon==null)throw new InvalidOperationException("Hãy chọn một dòng trong danh sách.");}
    private void ChonDong()
    {
        if(grid.SelectedRows.Count==0 || grid.SelectedRows[0].DataBoundItem is not DataRowView r)return;
        selectedSV=r["MaSV"].ToString();selectedMon=r["MaMon"].ToString();
        cboSV.SelectedValue=selectedSV;cboMon.SelectedValue=selectedMon;
        numCC.Value=Convert.ToDecimal(r["ChuyenCan"]);numGK.Value=Convert.ToDecimal(r["GiuaKy"]);numCK.Value=Convert.ToDecimal(r["CuoiKy"]);
    }
    private void ThaoTac(Action action)
    {
        try{action();selectedSV=null;selectedMon=null;NapBang();MessageBox.Show("Thao tác thành công!");}
        catch(Exception ex){MessageBox.Show(ex.Message,"Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);}
    }
    private void Xoa()
    {
        try{KiemTraDaChon();if(MessageBox.Show("Xóa điểm đã chọn?","Xác nhận",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            ThaoTac(()=>bll.Xoa(selectedSV!,selectedMon!));}
        catch(Exception ex){MessageBox.Show(ex.Message);}
    }
    private static string Csv(string s)=>"\""+s.Replace("\"","\"\"")+"\"";
    private void XuatCsv()
    {
        try
        {
            using var dlg=new SaveFileDialog(){Filter="CSV cho Excel (*.csv)|*.csv",FileName="BangDiemSinhVien.csv"};
            if(dlg.ShowDialog(this)!=DialogResult.OK)return;
            var dt=bll.DanhSach(txtTim.Text);
            using var writer=new StreamWriter(dlg.FileName,false,new UTF8Encoding(true));
            writer.WriteLine(string.Join(",",dt.Columns.Cast<DataColumn>().Select(c=>Csv(c.ColumnName))));
            foreach(DataRow r in dt.Rows)writer.WriteLine(string.Join(",",r.ItemArray.Select(x=>Csv(Convert.ToString(x,CultureInfo.InvariantCulture)??""))));
            MessageBox.Show("Đã xuất CSV. Có thể mở bằng Excel.");
        }
        catch(Exception ex){MessageBox.Show(ex.Message);}
    }
}
