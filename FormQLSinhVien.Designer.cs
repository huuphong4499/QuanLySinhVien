namespace QLSinhVien
{
    partial class FormQLSinhVien
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblTieuDe = new Label();
            lblMaSV = new Label();
            lblHoTen = new Label();
            lblNgaySinh = new Label();
            lblGioiTinh = new Label();
            lblDiaChi = new Label();
            lblSoDienThoai = new Label();
            lblLop = new Label();
            lblTimKiem = new Label();
            txtMaSV = new TextBox();
            txtHoTen = new TextBox();
            txtDiaChi = new TextBox();
            txtSoDienThoai = new TextBox();
            txtTimKiem = new TextBox();
            dtpNgaySinh = new DateTimePicker();
            cboGioiTinh = new ComboBox();
            cboMaLop = new ComboBox();
            dgvSinhVien = new DataGridView();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            btnLamMoi = new Button();
            btnTimKiem = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvSinhVien).BeginInit();
            SuspendLayout();
            // 
            // lblTieuDe
            // 
            lblTieuDe.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblTieuDe.AutoSize = true;
            lblTieuDe.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTieuDe.Location = new Point(232, 26);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(308, 41);
            lblTieuDe.TabIndex = 0;
            lblTieuDe.Text = "QUẢN LÝ SINH VIÊN";
            lblTieuDe.TextAlign = ContentAlignment.TopCenter;
            // 
            // lblMaSV
            // 
            lblMaSV.AutoSize = true;
            lblMaSV.Location = new Point(23, 94);
            lblMaSV.Name = "lblMaSV";
            lblMaSV.Size = new Size(91, 20);
            lblMaSV.TabIndex = 1;
            lblMaSV.Text = "Mã sinh viên";
            // 
            // lblHoTen
            // 
            lblHoTen.AutoSize = true;
            lblHoTen.Location = new Point(334, 94);
            lblHoTen.Name = "lblHoTen";
            lblHoTen.Size = new Size(73, 20);
            lblHoTen.TabIndex = 2;
            lblHoTen.Text = "Họ và tên";
            // 
            // lblNgaySinh
            // 
            lblNgaySinh.AutoSize = true;
            lblNgaySinh.Location = new Point(23, 157);
            lblNgaySinh.Name = "lblNgaySinh";
            lblNgaySinh.Size = new Size(74, 20);
            lblNgaySinh.TabIndex = 3;
            lblNgaySinh.Text = "Ngày sinh";
            // 
            // lblGioiTinh
            // 
            lblGioiTinh.AutoSize = true;
            lblGioiTinh.Location = new Point(334, 157);
            lblGioiTinh.Name = "lblGioiTinh";
            lblGioiTinh.Size = new Size(65, 20);
            lblGioiTinh.TabIndex = 4;
            lblGioiTinh.Text = "Giới tính";
            // 
            // lblDiaChi
            // 
            lblDiaChi.AutoSize = true;
            lblDiaChi.Location = new Point(23, 226);
            lblDiaChi.Name = "lblDiaChi";
            lblDiaChi.Size = new Size(55, 20);
            lblDiaChi.TabIndex = 5;
            lblDiaChi.Text = "Địa chỉ";
            // 
            // lblSoDienThoai
            // 
            lblSoDienThoai.AutoSize = true;
            lblSoDienThoai.Location = new Point(334, 226);
            lblSoDienThoai.Name = "lblSoDienThoai";
            lblSoDienThoai.Size = new Size(36, 20);
            lblSoDienThoai.TabIndex = 6;
            lblSoDienThoai.Text = "SĐT";
            // 
            // lblLop
            // 
            lblLop.AutoSize = true;
            lblLop.Location = new Point(28, 289);
            lblLop.Name = "lblLop";
            lblLop.Size = new Size(34, 20);
            lblLop.TabIndex = 7;
            lblLop.Text = "Lớp";
            // 
            // lblTimKiem
            // 
            lblTimKiem.AutoSize = true;
            lblTimKiem.Location = new Point(28, 375);
            lblTimKiem.Name = "lblTimKiem";
            lblTimKiem.Size = new Size(70, 20);
            lblTimKiem.TabIndex = 8;
            lblTimKiem.Text = "Tìm kiếm";
            // 
            // txtMaSV
            // 
            txtMaSV.Location = new Point(23, 117);
            txtMaSV.Name = "txtMaSV";
            txtMaSV.Size = new Size(250, 27);
            txtMaSV.TabIndex = 9;
            // 
            // txtHoTen
            // 
            txtHoTen.Location = new Point(334, 117);
            txtHoTen.Name = "txtHoTen";
            txtHoTen.Size = new Size(245, 27);
            txtHoTen.TabIndex = 10;
            // 
            // txtDiaChi
            // 
            txtDiaChi.Location = new Point(23, 249);
            txtDiaChi.Name = "txtDiaChi";
            txtDiaChi.Size = new Size(250, 27);
            txtDiaChi.TabIndex = 13;
            // 
            // txtSoDienThoai
            // 
            txtSoDienThoai.Location = new Point(334, 249);
            txtSoDienThoai.Name = "txtSoDienThoai";
            txtSoDienThoai.Size = new Size(245, 27);
            txtSoDienThoai.TabIndex = 14;
            // 
            // txtTimKiem
            // 
            txtTimKiem.Location = new Point(104, 368);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.Size = new Size(245, 27);
            txtTimKiem.TabIndex = 16;
            // 
            // dtpNgaySinh
            // 
            dtpNgaySinh.Location = new Point(23, 180);
            dtpNgaySinh.Name = "dtpNgaySinh";
            dtpNgaySinh.Size = new Size(250, 27);
            dtpNgaySinh.TabIndex = 17;
            // 
            // cboGioiTinh
            // 
            cboGioiTinh.FormattingEnabled = true;
            cboGioiTinh.Location = new Point(334, 180);
            cboGioiTinh.Name = "cboGioiTinh";
            cboGioiTinh.Size = new Size(245, 28);
            cboGioiTinh.TabIndex = 18;
            // 
            // cboMaLop
            // 
            cboMaLop.FormattingEnabled = true;
            cboMaLop.Location = new Point(28, 312);
            cboMaLop.Name = "cboMaLop";
            cboMaLop.Size = new Size(245, 28);
            cboMaLop.TabIndex = 19;
            // 
            // dgvSinhVien
            // 
            dgvSinhVien.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSinhVien.Dock = DockStyle.Bottom;
            dgvSinhVien.Location = new Point(0, 505);
            dgvSinhVien.Name = "dgvSinhVien";
            dgvSinhVien.RowHeadersWidth = 51;
            dgvSinhVien.Size = new Size(1182, 48);
            dgvSinhVien.TabIndex = 20;
            // 
            // btnThem
            // 
            btnThem.BackColor = Color.Lime;
            btnThem.Location = new Point(28, 429);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(86, 44);
            btnThem.TabIndex = 21;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = false;
            // 
            // btnSua
            // 
            btnSua.BackColor = Color.Yellow;
            btnSua.Location = new Point(135, 429);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(86, 44);
            btnSua.TabIndex = 22;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = false;
            // 
            // btnXoa
            // 
            btnXoa.BackColor = Color.Red;
            btnXoa.Location = new Point(247, 429);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(86, 44);
            btnXoa.TabIndex = 23;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = false;
            // 
            // btnLamMoi
            // 
            btnLamMoi.BackColor = Color.DarkGray;
            btnLamMoi.Location = new Point(358, 429);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(86, 44);
            btnLamMoi.TabIndex = 24;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = false;
            // 
            // btnTimKiem
            // 
            btnTimKiem.BackColor = Color.DeepSkyBlue;
            btnTimKiem.Location = new Point(470, 429);
            btnTimKiem.Name = "btnTimKiem";
            btnTimKiem.Size = new Size(86, 44);
            btnTimKiem.TabIndex = 25;
            btnTimKiem.Text = "Tìm kiếm";
            btnTimKiem.UseVisualStyleBackColor = false;
            // 
            // FormQLSinhVien
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1182, 553);
            Controls.Add(btnTimKiem);
            Controls.Add(btnLamMoi);
            Controls.Add(btnXoa);
            Controls.Add(btnSua);
            Controls.Add(btnThem);
            Controls.Add(dgvSinhVien);
            Controls.Add(cboMaLop);
            Controls.Add(cboGioiTinh);
            Controls.Add(dtpNgaySinh);
            Controls.Add(txtTimKiem);
            Controls.Add(txtSoDienThoai);
            Controls.Add(txtDiaChi);
            Controls.Add(txtHoTen);
            Controls.Add(txtMaSV);
            Controls.Add(lblTimKiem);
            Controls.Add(lblLop);
            Controls.Add(lblSoDienThoai);
            Controls.Add(lblDiaChi);
            Controls.Add(lblGioiTinh);
            Controls.Add(lblNgaySinh);
            Controls.Add(lblHoTen);
            Controls.Add(lblMaSV);
            Controls.Add(lblTieuDe);
            Name = "FormQLSinhVien";
            Text = "FormQLSinhVien";
            Load += FormQLSinhVien_Load;
            ((System.ComponentModel.ISupportInitialize)dgvSinhVien).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTieuDe;
        private Label lblMaSV;
        private Label lblHoTen;
        private Label lblNgaySinh;
        private Label lblGioiTinh;
        private Label lblDiaChi;
        private Label lblSoDienThoai;
        private Label lblLop;
        private Label lblTimKiem;
        private TextBox txtMaSV;
        private TextBox txtHoTen;
        private TextBox txtDiaChi;
        private TextBox txtSoDienThoai;
        private TextBox txtTimKiem;
        private DateTimePicker dtpNgaySinh;
        private ComboBox cboGioiTinh;
        private ComboBox cboMaLop;
        private DataGridView dgvSinhVien;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLamMoi;
        private Button btnTimKiem;
    }
}