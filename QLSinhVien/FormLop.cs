using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QLSinhVien.BLL;
using QLSinhVien.DTO;

namespace QLSinhVien
{
    public partial class FormLop : Form
    {
        // Phân hệ Quản lý Khoa (Phụ trách: Chu Hữu Phong)
        private readonly KhoaBLL _khoaBLL;

        // Phân hệ Quản lý Lớp (Phụ trách: Phan Nhựt An)
        private readonly LopBLL _lopBLL;

        // Các khung giao diện
        private Panel? pnlHeader;
        private Panel? pnlThongTin;
        private Panel? pnlDanhSach;

        // Các tiêu đề
        private Label? lblTieuDe;
        private Label? lblMoTa;
        private Label? lblThongTin;
        private Label? lblDanhSach;
        private Label? lblKhoa;

        public FormLop()
        {
            InitializeComponent();

            _khoaBLL = new KhoaBLL();
            _lopBLL = new LopBLL();

            // Gắn sự kiện cho các nút
            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnLamMoi.Click += btnLamMoi_Click;

            // Gắn sự kiện chọn dòng
            dgvLop.CellClick += dgvLop_CellClick;

            // Tải dữ liệu khi mở Form
            Load += FormLop_Load;
        }

        // =========================================
        // 1. THIẾT KẾ GIAO DIỆN (GIỮ NGUYÊN 100%)
        // =========================================

        private void ThietKeGiaoDien()
        {
            // Cấu hình Form
            this.Text = "Quản lý lớp";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(1200, 800);
            this.MinimumSize = new Size(950, 650);
            this.BackColor = Color.FromArgb(242, 246, 252);
            this.Font = new Font("Segoe UI", 10);

            // Tạo thanh tiêu đề
            pnlHeader = new Panel();
            pnlHeader.BackColor = Color.FromArgb(20, 65, 130);
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Size = new Size(this.ClientSize.Width, 105);
            pnlHeader.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            this.Controls.Add(pnlHeader);

            // Tiêu đề chính
            lblTieuDe = new Label();
            lblTieuDe.Text = "QUẢN LÝ LỚP";
            lblTieuDe.Font = new Font("Segoe UI", 23, FontStyle.Bold);
            lblTieuDe.ForeColor = Color.White;
            lblTieuDe.AutoSize = true;
            lblTieuDe.Location = new Point(35, 18);

            pnlHeader.Controls.Add(lblTieuDe);

            // Dòng mô tả
            lblMoTa = new Label();
            lblMoTa.Text = "HỆ THỐNG QUẢN LÝ SINH VIÊN";
            lblMoTa.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            lblMoTa.ForeColor = Color.FromArgb(210, 225, 245);
            lblMoTa.AutoSize = true;
            lblMoTa.Location = new Point(39, 65);

            pnlHeader.Controls.Add(lblMoTa);

            // =====================================
            // KHUNG THÔNG TIN LỚP
            // =====================================

            pnlThongTin = new Panel();
            pnlThongTin.BackColor = Color.White;
            pnlThongTin.Location = new Point(25, 125);
            pnlThongTin.Size = new Size(1130, 205);
            pnlThongTin.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            this.Controls.Add(pnlThongTin);

            // Tiêu đề khung thông tin
            lblThongTin = new Label();
            lblThongTin.Text = "THÔNG TIN LỚP";
            lblThongTin.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            lblThongTin.ForeColor = Color.FromArgb(25, 85, 160);
            lblThongTin.Location = new Point(25, 15);
            lblThongTin.AutoSize = true;

            pnlThongTin.Controls.Add(lblThongTin);

            // Nhãn Mã lớp
            lblMaLop.Visible = true;
            lblMaLop.Text = "Mã lớp";
            lblMaLop.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblMaLop.ForeColor = Color.FromArgb(50, 60, 80);
            lblMaLop.Location = new Point(35, 65);
            lblMaLop.Size = new Size(300, 25);
            lblMaLop.Parent = pnlThongTin;

            // Ô nhập Mã lớp
            txtMaLop.Parent = pnlThongTin;
            txtMaLop.Location = new Point(35, 95);
            txtMaLop.Size = new Size(300, 35);
            txtMaLop.Font = new Font("Segoe UI", 11);
            txtMaLop.BorderStyle = BorderStyle.FixedSingle;

            // Nhãn Tên lớp
            lblTenLop.Visible = true;
            lblTenLop.Text = "Tên lớp";
            lblTenLop.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblTenLop.ForeColor = Color.FromArgb(50, 60, 80);
            lblTenLop.Location = new Point(390, 65);
            lblTenLop.Size = new Size(350, 25);
            lblTenLop.Parent = pnlThongTin;

            // Ô nhập Tên lớp
            txtTenLop.Parent = pnlThongTin;
            txtTenLop.Location = new Point(390, 95);
            txtTenLop.Size = new Size(350, 35);
            txtTenLop.Font = new Font("Segoe UI", 11);
            txtTenLop.BorderStyle = BorderStyle.FixedSingle;

            // Nhãn Khoa
            lblKhoa = new Label();
            lblKhoa.Text = "Khoa";
            lblKhoa.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblKhoa.ForeColor = Color.FromArgb(50, 60, 80);
            lblKhoa.Location = new Point(35, 145);
            lblKhoa.AutoSize = true;

            pnlThongTin.Controls.Add(lblKhoa);

            // ComboBox Khoa
            cboKhoa.Parent = pnlThongTin;
            cboKhoa.Location = new Point(100, 140);
            cboKhoa.Size = new Size(640, 35);
            cboKhoa.Font = new Font("Segoe UI", 10);
            cboKhoa.DropDownStyle = ComboBoxStyle.DropDownList;

            // =====================================
            // CÁC NÚT CHỨC NĂNG
            // =====================================

            CauHinhButton(btnThem, "✚  THÊM", Color.FromArgb(37, 99, 235));
            CauHinhButton(btnSua, "✎  SỬA", Color.FromArgb(16, 160, 130));
            CauHinhButton(btnXoa, "✖  XÓA", Color.FromArgb(235, 65, 75));
            CauHinhButton(btnLamMoi, "⟳  LÀM MỚI", Color.FromArgb(120, 75, 210));

            // Đưa các nút ra Form chính
            btnThem.Parent = this;
            btnSua.Parent = this;
            btnXoa.Parent = this;
            btnLamMoi.Parent = this;

            // =====================================
            // KHUNG DANH SÁCH LỚP
            // =====================================

            pnlDanhSach = new Panel();
            pnlDanhSach.BackColor = Color.White;
            pnlDanhSach.Location = new Point(25, 420);
            pnlDanhSach.Size = new Size(1130, 300);
            pnlDanhSach.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            this.Controls.Add(pnlDanhSach);

            // Tiêu đề danh sách
            lblDanhSach = new Label();
            lblDanhSach.Text = "DANH SÁCH LỚP";
            lblDanhSach.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            lblDanhSach.ForeColor = Color.FromArgb(25, 85, 160);
            lblDanhSach.Location = new Point(20, 12);
            lblDanhSach.AutoSize = true;

            pnlDanhSach.Controls.Add(lblDanhSach);

            // Đưa DataGridView vào khung danh sách
            dgvLop.Parent = pnlDanhSach;
            dgvLop.Location = new Point(15, 50);
            dgvLop.Size = new Size(1100, 235);
            dgvLop.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            // Cấu hình bảng
            CauHinhBang();

            // Đưa các khung lên phía trước
            pnlHeader.BringToFront();
            pnlThongTin.BringToFront();
            pnlDanhSach.BringToFront();

            // Đưa các nút lên phía trước
            btnThem.BringToFront();
            btnSua.BringToFront();
            btnXoa.BringToFront();
            btnLamMoi.BringToFront();

            // Sắp xếp vị trí
            CanChinhGiaoDien();

            // Căn chỉnh khi thay đổi kích thước
            this.Resize += (s, e) =>
            {
                CanChinhGiaoDien();
            };
        }

        // Cấu hình giao diện Button
        private void CauHinhButton(Button btn, string noiDung, Color mauNen)
        {
            btn.Text = noiDung;
            btn.Size = new Size(145, 48);
            btn.BackColor = mauNen;
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Cursor = Cursors.Hand;
            btn.TextAlign = ContentAlignment.MiddleCenter;
        }

        // Căn chỉnh vị trí giao diện
        private void CanChinhGiaoDien()
        {
            int w = this.ClientSize.Width;
            int h = this.ClientSize.Height;

            if (pnlHeader == null || pnlThongTin == null || pnlDanhSach == null)
                return;

            pnlHeader.Width = w;
            pnlThongTin.Width = w - 50;
            pnlDanhSach.Width = w - 50;

            // Vị trí các nút
            int y = 350;
            int x = 25;
            int gap = 15;

            btnThem.Location = new Point(x, y);
            btnSua.Location = new Point(x + 145 + gap, y);
            btnXoa.Location = new Point(x + (145 + gap) * 2, y);
            btnLamMoi.Location = new Point(x + (145 + gap) * 3, y);

            // Khung danh sách tự mở rộng
            pnlDanhSach.Location = new Point(25, 420);
            pnlDanhSach.Height = Math.Max(180, h - 445);
        }

        // Cấu hình DataGridView
        private void CauHinhBang()
        {
            dgvLop.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLop.MultiSelect = false;
            dgvLop.ReadOnly = true;
            dgvLop.AllowUserToAddRows = false;
            dgvLop.AllowUserToDeleteRows = false;
            dgvLop.AllowUserToResizeRows = false;
            dgvLop.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLop.BackgroundColor = Color.White;
            dgvLop.BorderStyle = BorderStyle.None;
            dgvLop.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvLop.RowHeadersVisible = false;
            dgvLop.EnableHeadersVisualStyles = false;

            // Tiêu đề cột
            dgvLop.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(25, 85, 160);
            dgvLop.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvLop.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvLop.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvLop.ColumnHeadersHeight = 45;

            // Dữ liệu
            dgvLop.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvLop.DefaultCellStyle.ForeColor = Color.FromArgb(45, 55, 70);
            dgvLop.DefaultCellStyle.BackColor = Color.White;
            dgvLop.DefaultCellStyle.SelectionBackColor = Color.FromArgb(45, 115, 205);
            dgvLop.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvLop.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 246, 255);
            dgvLop.RowTemplate.Height = 38;
        }

        // =========================================
        // 2. KHI MỞ FORM
        // =========================================

        private void FormLop_Load(object? sender, EventArgs e)
        {
            ThietKeGiaoDien();
            LoadKhoa();
            LoadLop();
        }

        // =========================================
        // 3. TẢI DANH SÁCH KHOA (CHU HỮU PHONG - KHOABLL)
        // =========================================

        private void LoadKhoa()
        {
            try
            {
                DataTable dt = _khoaBLL.LayDanhSachKhoaChoComboBox();
                cboKhoa.DataSource = dt;
                cboKhoa.DisplayMember = "TenKhoa";
                cboKhoa.ValueMember = "MaKhoa";
                cboKhoa.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách khoa: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =========================================
        // 4. TẢI DANH SÁCH LỚP (PHAN NHỰT AN - LOPBLL)
        // =========================================

        private void LoadLop()
        {
            try
            {
                DataTable dt = _lopBLL.LayDanhSachLop();
                dgvLop.DataSource = dt;

                // Đặt tên hiển thị cho các cột
                if (dgvLop.Columns.Contains("MaLop"))
                    dgvLop.Columns["MaLop"].HeaderText = "Mã lớp";

                if (dgvLop.Columns.Contains("TenLop"))
                    dgvLop.Columns["TenLop"].HeaderText = "Tên lớp";

                if (dgvLop.Columns.Contains("MaKhoa"))
                    dgvLop.Columns["MaKhoa"].HeaderText = "Mã khoa";

                if (dgvLop.Columns.Contains("TenKhoa"))
                    dgvLop.Columns["TenKhoa"].HeaderText = "Tên khoa";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách lớp: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =========================================
        // 5. NÚT THÊM (PHAN NHỰT AN - LOPBLL)
        // =========================================

        private void btnThem_Click(object? sender, EventArgs e)
        {
            string maKhoa = cboKhoa.SelectedValue?.ToString() ?? "";
            LopDTO lop = new LopDTO
            {
                MaLop = txtMaLop.Text.Trim(),
                TenLop = txtTenLop.Text.Trim(),
                MaKhoa = maKhoa
            };

            var ketQua = _lopBLL.ThemLop(lop);
            if (ketQua.ThanhCong)
            {
                MessageBox.Show(ketQua.ThongBao, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadLop();
                LamMoi();
            }
            else
            {
                MessageBox.Show(ketQua.ThongBao, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // =========================================
        // 6. NÚT SỬA (PHAN NHỰT AN - LOPBLL)
        // =========================================

        private void btnSua_Click(object? sender, EventArgs e)
        {
            string maKhoa = cboKhoa.SelectedValue?.ToString() ?? "";
            LopDTO lop = new LopDTO
            {
                MaLop = txtMaLop.Text.Trim(),
                TenLop = txtTenLop.Text.Trim(),
                MaKhoa = maKhoa
            };

            var ketQua = _lopBLL.CapNhatLop(lop);
            if (ketQua.ThanhCong)
            {
                MessageBox.Show(ketQua.ThongBao, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadLop();
                LamMoi();
            }
            else
            {
                MessageBox.Show(ketQua.ThongBao, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // =========================================
        // 7. NÚT XÓA (PHAN NHỰT AN - LOPBLL)
        // =========================================

        private void btnXoa_Click(object? sender, EventArgs e)
        {
            string maLop = txtMaLop.Text.Trim();
            if (string.IsNullOrWhiteSpace(maLop))
            {
                MessageBox.Show("Vui lòng chọn lớp cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult xacNhan = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa lớp '{maLop}' không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (xacNhan != DialogResult.Yes)
                return;

            var ketQua = _lopBLL.XoaLop(maLop);
            if (ketQua.ThanhCong)
            {
                MessageBox.Show(ketQua.ThongBao, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadLop();
                LamMoi();
            }
            else
            {
                MessageBox.Show(ketQua.ThongBao, "Không thể xóa lớp", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =========================================
        // 8. CHỌN DÒNG TRONG BẢNG
        // =========================================

        private void dgvLop_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgvLop.Rows[e.RowIndex];

            txtMaLop.Text = row.Cells["MaLop"].Value?.ToString() ?? "";
            txtTenLop.Text = row.Cells["TenLop"].Value?.ToString() ?? "";

            object? maKhoa = row.Cells["MaKhoa"].Value;
            if (maKhoa != null && maKhoa != DBNull.Value)
            {
                cboKhoa.SelectedValue = maKhoa.ToString() ?? "";
            }
            else
            {
                cboKhoa.SelectedIndex = -1;
            }
        }

        // =========================================
        // 9. NÚT LÀM MỚI
        // =========================================

        private void btnLamMoi_Click(object? sender, EventArgs e)
        {
            LamMoi();
            LoadLop();
        }

        private void LamMoi()
        {
            txtMaLop.Clear();
            txtTenLop.Clear();
            cboKhoa.SelectedIndex = -1;
            txtMaLop.Focus();
        }
    }
}
