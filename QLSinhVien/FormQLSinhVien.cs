using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QLSinhVien.BLL;
using QLSinhVien.DTO;

namespace QLSinhVien
{
    public partial class FormQLSinhVien : Form
    {
        // Phân hệ Quản lý Lớp (Phụ trách: Phan Nhựt An)
        private readonly LopBLL _lopBLL;

        // Phân hệ Quản lý Sinh viên (Phụ trách: Trần Thành Long)
        private readonly SinhVienBLL _sinhVienBLL;

        // Các khu vực giao diện
        private Panel? panelHeader;
        private Panel? panelThongTin;
        private Panel? panelChucNang;
        private Panel? panelDanhSach;

        private Label? lblMoTa;
        private Label? lblThongTin;
        private Label? lblTieuDeBang;

        public FormQLSinhVien()
        {
            InitializeComponent();

            _lopBLL = new LopBLL();
            _sinhVienBLL = new SinhVienBLL();

            // Thiết kế giao diện
            CaiThienGiaoDien();

            // Đăng ký sự kiện
            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnLamMoi.Click += btnLamMoi_Click;
            btnTimKiem.Click += btnTimKiem_Click;

            dgvSinhVien.CellClick += dgvSinhVien_CellClick;
        }

        // ==========================================
        // 1. THIẾT KẾ GIAO DIỆN (GIỮ NGUYÊN 100%)
        // ==========================================

        private void CaiThienGiaoDien()
        {
            Color navy = Color.FromArgb(30, 58, 95);
            Color blue = Color.FromArgb(37, 99, 235);
            Color teal = Color.FromArgb(13, 148, 136);
            Color red = Color.FromArgb(220, 38, 38);
            Color text = Color.FromArgb(51, 65, 85);
            Color nen = Color.FromArgb(245, 247, 251);

            this.Text = "QUẢN LÝ SINH VIÊN";
            this.BackColor = nen;
            this.Font = new Font("Segoe UI", 10F);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new Size(1380, 850);
            this.MinimumSize = new Size(1100, 750);

            // Tạo các Panel
            panelHeader = new Panel();
            panelThongTin = new Panel();
            panelChucNang = new Panel();
            panelDanhSach = new Panel();

            panelHeader.BackColor = navy;
            panelThongTin.BackColor = Color.White;
            panelChucNang.BackColor = Color.White;
            panelDanhSach.BackColor = Color.White;

            // Tiêu đề phụ
            lblMoTa = new Label();
            lblMoTa.Text = "Quản lý hồ sơ và thông tin sinh viên";
            lblMoTa.ForeColor = Color.FromArgb(219, 234, 254);
            lblMoTa.Font = new Font("Segoe UI", 10F);
            lblMoTa.AutoSize = true;
            lblMoTa.Location = new Point(32, 55);

            lblThongTin = new Label();
            lblThongTin.Text = "THÔNG TIN SINH VIÊN";
            lblThongTin.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblThongTin.ForeColor = navy;
            lblThongTin.AutoSize = true;
            lblThongTin.Location = new Point(24, 14);

            lblTieuDeBang = new Label();
            lblTieuDeBang.Text = "DANH SÁCH SINH VIÊN";
            lblTieuDeBang.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTieuDeBang.ForeColor = navy;
            lblTieuDeBang.AutoSize = true;
            lblTieuDeBang.Location = new Point(20, 12);

            // Đưa tiêu đề vào Header
            Controls.Remove(lblTieuDe);
            panelHeader.Controls.Add(lblTieuDe);
            panelHeader.Controls.Add(lblMoTa);

            lblTieuDe.Location = new Point(28, 12);
            lblTieuDe.AutoSize = true;
            lblTieuDe.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTieuDe.ForeColor = Color.White;

            // Đưa các control thông tin vào Panel
            Control[] controlsThongTin =
            {
                lblThongTin,
                lblMaSV, txtMaSV,
                lblHoTen, txtHoTen,
                lblNgaySinh, dtpNgaySinh,
                lblGioiTinh, cboGioiTinh,
                lblDiaChi, txtDiaChi,
                lblSoDienThoai, txtSoDienThoai,
                lblLop, cboMaLop
            };

            foreach (Control c in controlsThongTin)
            {
                if (c.Parent != null)
                    c.Parent.Controls.Remove(c);

                panelThongTin.Controls.Add(c);
            }

            // Đưa các control chức năng vào Panel
            Control[] controlsChucNang =
            {
                lblTimKiem,
                txtTimKiem,
                btnThem,
                btnSua,
                btnXoa,
                btnLamMoi,
                btnTimKiem
            };

            foreach (Control c in controlsChucNang)
            {
                if (c.Parent != null)
                    c.Parent.Controls.Remove(c);

                panelChucNang.Controls.Add(c);
            }

            // Đưa bảng dữ liệu vào Panel
            if (dgvSinhVien.Parent != null)
                dgvSinhVien.Parent.Controls.Remove(dgvSinhVien);

            panelDanhSach.Controls.Add(lblTieuDeBang);
            panelDanhSach.Controls.Add(dgvSinhVien);

            // Thêm Panel vào Form
            Controls.Add(panelHeader);
            Controls.Add(panelThongTin);
            Controls.Add(panelChucNang);
            Controls.Add(panelDanhSach);

            // Thiết kế các nhãn
            Label[] labels =
            {
                lblMaSV, lblHoTen, lblNgaySinh,
                lblGioiTinh, lblDiaChi,
                lblSoDienThoai, lblLop, lblTimKiem
            };

            foreach (Label lbl in labels)
            {
                lbl.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                lbl.ForeColor = text;
                lbl.AutoSize = true;
            }

            // Thiết kế TextBox
            TextBox[] textBoxes =
            {
                txtMaSV, txtHoTen, txtDiaChi,
                txtSoDienThoai, txtTimKiem
            };

            foreach (TextBox txt in textBoxes)
            {
                txt.Font = new Font("Segoe UI", 10F);
                txt.ForeColor = text;
                txt.BackColor = Color.White;
                txt.BorderStyle = BorderStyle.FixedSingle;
            }

            // Thiết kế ComboBox
            ComboBox[] combos =
            {
                cboGioiTinh, cboMaLop
            };

            foreach (ComboBox cbo in combos)
            {
                cbo.Font = new Font("Segoe UI", 10F);
                cbo.ForeColor = text;
                cbo.BackColor = Color.White;
                cbo.FlatStyle = FlatStyle.Flat;
            }

            // Ngày sinh
            dtpNgaySinh.Font = new Font("Segoe UI", 10F);
            dtpNgaySinh.Format = DateTimePickerFormat.Custom;
            dtpNgaySinh.CustomFormat = "dd/MM/yyyy";

            // Thiết kế nút
            ThietKeNut(btnThem, blue);
            ThietKeNut(btnSua, teal);
            ThietKeNut(btnXoa, red);
            ThietKeNut(btnLamMoi, Color.FromArgb(100, 116, 139));
            ThietKeNut(btnTimKiem, navy);

            btnThem.Text = "+  Thêm";
            btnSua.Text = "Sửa";
            btnXoa.Text = "Xóa";
            btnLamMoi.Text = "Làm mới";
            btnTimKiem.Text = "Tìm kiếm";

            // Thiết kế bảng
            dgvSinhVien.BackgroundColor = Color.White;
            dgvSinhVien.BorderStyle = BorderStyle.None;
            dgvSinhVien.EnableHeadersVisualStyles = false;
            dgvSinhVien.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvSinhVien.ColumnHeadersDefaultCellStyle.BackColor = navy;
            dgvSinhVien.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvSinhVien.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvSinhVien.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvSinhVien.ColumnHeadersHeight = 45;

            dgvSinhVien.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgvSinhVien.DefaultCellStyle.ForeColor = text;
            dgvSinhVien.DefaultCellStyle.BackColor = Color.White;
            dgvSinhVien.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
            dgvSinhVien.DefaultCellStyle.SelectionForeColor = navy;
            dgvSinhVien.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgvSinhVien.GridColor = Color.FromArgb(226, 232, 240);
            dgvSinhVien.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            dgvSinhVien.RowHeadersVisible = false;
            dgvSinhVien.RowTemplate.Height = 38;
            dgvSinhVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSinhVien.MultiSelect = false;
            dgvSinhVien.ReadOnly = true;
            dgvSinhVien.AllowUserToAddRows = false;
            dgvSinhVien.AllowUserToResizeRows = false;
            dgvSinhVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Định dạng bảng sau khi tải dữ liệu
            dgvSinhVien.DataBindingComplete += dgvSinhVien_DataBindingComplete;

            // Bố trí giao diện
            BoTriGiaoDien();

            // Tự thay đổi kích thước khi phóng to
            this.Resize += (s, e) =>
            {
                BoTriGiaoDien();
            };
        }

        // ==========================================
        // 2. BỐ TRÍ GIAO DIỆN TỰ ĐỘNG
        // ==========================================

        private void BoTriGiaoDien()
        {
            if (panelHeader == null || panelThongTin == null || panelChucNang == null || panelDanhSach == null)
                return;

            int width = this.ClientSize.Width;
            int height = this.ClientSize.Height;

            // Header
            panelHeader.SetBounds(0, 0, width, 90);

            // Panel thông tin
            panelThongTin.SetBounds(20, 105, width - 40, 260);

            // Panel chức năng
            panelChucNang.SetBounds(20, 380, width - 40, 65);

            // Panel danh sách
            panelDanhSach.SetBounds(20, 460, width - 40, height - 480);

            // Chia thông tin thành 3 cột
            int colWidth = (panelThongTin.Width - 100) / 3;

            int x1 = 25;
            int x2 = 50 + colWidth;
            int x3 = 75 + colWidth * 2;

            int inputWidth = colWidth - 20;

            // Hàng 1
            lblMaSV.Location = new Point(x1, 53);
            txtMaSV.SetBounds(x1, 76, inputWidth, 30);

            lblHoTen.Location = new Point(x2, 53);
            txtHoTen.SetBounds(x2, 76, inputWidth, 30);

            lblNgaySinh.Location = new Point(x3, 53);
            dtpNgaySinh.SetBounds(x3, 76, inputWidth, 30);

            // Hàng 2
            lblGioiTinh.Location = new Point(x1, 125);
            cboGioiTinh.SetBounds(x1, 148, inputWidth, 30);

            lblDiaChi.Location = new Point(x2, 125);
            txtDiaChi.SetBounds(x2, 148, inputWidth, 30);

            lblSoDienThoai.Location = new Point(x3, 125);
            txtSoDienThoai.SetBounds(x3, 148, inputWidth, 30);

            // Hàng 3
            lblLop.Location = new Point(x1, 197);
            cboMaLop.SetBounds(x1, 220, inputWidth, 30);

            // Nút chức năng
            int buttonY = 12;

            btnThem.SetBounds(20, buttonY, 105, 40);
            btnSua.SetBounds(140, buttonY, 105, 40);
            btnXoa.SetBounds(260, buttonY, 105, 40);
            btnLamMoi.SetBounds(380, buttonY, 115, 40);

            // Tìm kiếm bên phải
            int searchButtonX = panelChucNang.Width - 145;

            btnTimKiem.SetBounds(searchButtonX, buttonY, 125, 40);
            txtTimKiem.SetBounds(searchButtonX - 290, 18, 275, 30);
            lblTimKiem.Location = new Point(searchButtonX - 365, 24);

            // Bảng danh sách
            dgvSinhVien.SetBounds(20, 48, panelDanhSach.Width - 40, panelDanhSach.Height - 62);
        }

        // ==========================================
        // 3. THIẾT KẾ NÚT BẤM
        // ==========================================

        private void ThietKeNut(Button btn, Color mauNen)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = ControlPaint.Dark(mauNen, 0.1F);
            btn.FlatAppearance.MouseDownBackColor = ControlPaint.Dark(mauNen, 0.2F);
            btn.BackColor = mauNen;
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
            btn.Height = 40;
        }

        // ==========================================
        // 4. ĐỊNH DẠNG BẢNG SINH VIÊN
        // ==========================================

        private void dgvSinhVien_DataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
        {
            string[] headers =
            {
                "Mã sinh viên",
                "Họ và tên",
                "Ngày sinh",
                "Giới tính",
                "Địa chỉ",
                "Số điện thoại",
                "Mã lớp"
            };

            for (int i = 0; i < dgvSinhVien.Columns.Count && i < headers.Length; i++)
            {
                dgvSinhVien.Columns[i].HeaderText = headers[i];
            }

            if (dgvSinhVien.Columns.Contains("NgaySinh"))
            {
                dgvSinhVien.Columns["NgaySinh"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
        }

        // ==========================================
        // 5. KHỞI TẠO FORM
        // ==========================================

        private void FormQLSinhVien_Load(object? sender, EventArgs e)
        {
            cboGioiTinh.Items.Clear();
            cboGioiTinh.Items.Add("Nam");
            cboGioiTinh.Items.Add("Nữ");

            cboGioiTinh.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMaLop.DropDownStyle = ComboBoxStyle.DropDownList;

            LoadLop();
            LoadSinhVien();
        }

        // ==========================================
        // 6. TẢI DANH SÁCH LỚP (PHAN NHỰT AN - LOPBLL)
        // ==========================================

        private void LoadLop()
        {
            try
            {
                DataTable dt = _lopBLL.LayDanhSachLopChoComboBox();
                cboMaLop.DataSource = null;
                cboMaLop.DataSource = dt;
                cboMaLop.DisplayMember = "TenLop";
                cboMaLop.ValueMember = "MaLop";
                cboMaLop.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách lớp:\n" + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==========================================
        // 7. TẢI DANH SÁCH SINH VIÊN (TRẦN THÀNH LONG - SINHVIENBLL)
        // ==========================================

        private void LoadSinhVien()
        {
            try
            {
                DataTable dt = _sinhVienBLL.LayDanhSachSinhVien();
                dgvSinhVien.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải sinh viên:\n" + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==========================================
        // 8. TẠO ĐỐI TƯỢNG SINHVIENDTO TỪ FORM
        // ==========================================

        private SinhVienDTO LayDuLieuTuForm()
        {
            string maLop = cboMaLop.SelectedValue?.ToString() ?? "";
            return new SinhVienDTO
            {
                MaSV = txtMaSV.Text.Trim(),
                HoTen = txtHoTen.Text.Trim(),
                NgaySinh = dtpNgaySinh.Value.Date,
                GioiTinh = cboGioiTinh.Text.Trim(),
                DiaChi = string.IsNullOrWhiteSpace(txtDiaChi.Text) ? null : txtDiaChi.Text.Trim(),
                SoDienThoai = string.IsNullOrWhiteSpace(txtSoDienThoai.Text) ? null : txtSoDienThoai.Text.Trim(),
                MaLop = maLop
            };
        }

        // ==========================================
        // 9. THÊM SINH VIÊN (TRẦN THÀNH LONG - SINHVIENBLL)
        // ==========================================

        private void btnThem_Click(object? sender, EventArgs e)
        {
            SinhVienDTO sv = LayDuLieuTuForm();
            var ketQua = _sinhVienBLL.ThemSinhVien(sv);

            if (ketQua.ThanhCong)
            {
                MessageBox.Show(ketQua.ThongBao, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadSinhVien();
                LamMoi();
            }
            else
            {
                MessageBox.Show(ketQua.ThongBao, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // ==========================================
        // 10. SỬA SINH VIÊN (TRẦN THÀNH LONG - SINHVIENBLL)
        // ==========================================

        private void btnSua_Click(object? sender, EventArgs e)
        {
            SinhVienDTO sv = LayDuLieuTuForm();
            var ketQua = _sinhVienBLL.CapNhatSinhVien(sv);

            if (ketQua.ThanhCong)
            {
                MessageBox.Show(ketQua.ThongBao, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadSinhVien();
                LamMoi();
            }
            else
            {
                MessageBox.Show(ketQua.ThongBao, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // ==========================================
        // 11. XÓA SINH VIÊN (TRẦN THÀNH LONG - SINHVIENBLL)
        // ==========================================

        private void btnXoa_Click(object? sender, EventArgs e)
        {
            string maSV = txtMaSV.Text.Trim();
            if (string.IsNullOrWhiteSpace(maSV))
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult xacNhan = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa sinh viên '{maSV}' không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (xacNhan != DialogResult.Yes)
                return;

            var ketQua = _sinhVienBLL.XoaSinhVien(maSV);
            if (ketQua.ThanhCong)
            {
                MessageBox.Show(ketQua.ThongBao, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadSinhVien();
                LamMoi();
            }
            else
            {
                MessageBox.Show(ketQua.ThongBao, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==========================================
        // 12. TÌM KIẾM SINH VIÊN (TRẦN THÀNH LONG - SINHVIENBLL)
        // ==========================================

        private void btnTimKiem_Click(object? sender, EventArgs e)
        {
            try
            {
                string tuKhoa = txtTimKiem.Text.Trim();
                DataTable dt = _sinhVienBLL.TimKiemSinhVien(tuKhoa);
                dgvSinhVien.DataSource = dt;

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy sinh viên phù hợp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm:\n" + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==========================================
        // 13. CHỌN SINH VIÊN TRÊN BẢNG
        // ==========================================

        private void dgvSinhVien_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgvSinhVien.Rows[e.RowIndex];

            txtMaSV.Text = row.Cells["MaSV"].Value?.ToString() ?? "";
            txtHoTen.Text = row.Cells["HoTen"].Value?.ToString() ?? "";

            object? ngaySinh = row.Cells["NgaySinh"].Value;
            if (ngaySinh != null && ngaySinh != DBNull.Value)
            {
                DateTime ngay = Convert.ToDateTime(ngaySinh);
                if (ngay >= dtpNgaySinh.MinDate && ngay <= dtpNgaySinh.MaxDate)
                {
                    dtpNgaySinh.Value = ngay;
                }
            }

            cboGioiTinh.Text = row.Cells["GioiTinh"].Value?.ToString() ?? "";

            object? diaChi = row.Cells["DiaChi"].Value;
            txtDiaChi.Text = (diaChi == null || diaChi == DBNull.Value) ? "" : diaChi.ToString();

            object? sdt = row.Cells["SoDienThoai"].Value;
            txtSoDienThoai.Text = (sdt == null || sdt == DBNull.Value) ? "" : sdt.ToString();

            object? maLop = row.Cells["MaLop"].Value;
            if (maLop != null && maLop != DBNull.Value)
            {
                cboMaLop.SelectedValue = maLop.ToString() ?? "";
            }
        }

        // ==========================================
        // 14. LÀM MỚI DỮ LIỆU
        // ==========================================

        private void btnLamMoi_Click(object? sender, EventArgs e)
        {
            LamMoi();
            LoadSinhVien();
        }

        private void LamMoi()
        {
            txtMaSV.Clear();
            txtHoTen.Clear();
            txtDiaChi.Clear();
            txtSoDienThoai.Clear();
            txtTimKiem.Clear();

            dtpNgaySinh.Value = DateTime.Today;

            cboGioiTinh.SelectedIndex = -1;
            cboMaLop.SelectedIndex = -1;

            dgvSinhVien.ClearSelection();
            txtMaSV.Focus();
        }
    }
}
