namespace QLSinhVien
{
    partial class FormLop
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
            label1 = new Label();
            lblMaLop = new Label();
            lblTenLop = new Label();
            txtMaLop = new TextBox();
            txtTenLop = new TextBox();
            lblMaKhoa = new Label();
            cboKhoa = new ComboBox();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            btnLamMoi = new Button();
            dgvLop = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvLop).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Top;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ImageAlign = ContentAlignment.TopCenter;
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(308, 41);
            label1.TabIndex = 0;
            label1.Text = "QUẢN LÝ SINH VIÊN";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblMaLop
            // 
            lblMaLop.AutoSize = true;
            lblMaLop.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblMaLop.Location = new Point(12, 74);
            lblMaLop.Name = "lblMaLop";
            lblMaLop.Size = new Size(74, 28);
            lblMaLop.TabIndex = 1;
            lblMaLop.Text = "Mã lớp";
            // 
            // lblTenLop
            // 
            lblTenLop.AutoSize = true;
            lblTenLop.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTenLop.Location = new Point(360, 74);
            lblTenLop.Name = "lblTenLop";
            lblTenLop.Size = new Size(75, 28);
            lblTenLop.TabIndex = 2;
            lblTenLop.Text = "Tên lớp";
            // 
            // txtMaLop
            // 
            txtMaLop.Location = new Point(12, 105);
            txtMaLop.Name = "txtMaLop";
            txtMaLop.Size = new Size(254, 27);
            txtMaLop.TabIndex = 3;
            // 
            // txtTenLop
            // 
            txtTenLop.Location = new Point(360, 105);
            txtTenLop.Name = "txtTenLop";
            txtTenLop.Size = new Size(254, 27);
            txtTenLop.TabIndex = 4;
            // 
            // lblMaKhoa
            // 
            lblMaKhoa.AutoSize = true;
            lblMaKhoa.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblMaKhoa.Location = new Point(12, 148);
            lblMaKhoa.Name = "lblMaKhoa";
            lblMaKhoa.Size = new Size(57, 28);
            lblMaKhoa.TabIndex = 5;
            lblMaKhoa.Text = "Khoa";
            // 
            // cboKhoa
            // 
            cboKhoa.FormattingEnabled = true;
            cboKhoa.Location = new Point(12, 191);
            cboKhoa.Name = "cboKhoa";
            cboKhoa.Size = new Size(602, 28);
            cboKhoa.TabIndex = 6;
            // 
            // btnThem
            // 
            btnThem.BackColor = Color.LightGreen;
            btnThem.Location = new Point(12, 245);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(84, 37);
            btnThem.TabIndex = 7;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = false;
            // 
            // btnSua
            // 
            btnSua.BackColor = Color.LightGray;
            btnSua.Location = new Point(102, 245);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(84, 37);
            btnSua.TabIndex = 8;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = false;
            // 
            // btnXoa
            // 
            btnXoa.BackColor = Color.Red;
            btnXoa.Location = new Point(192, 245);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(84, 37);
            btnXoa.TabIndex = 9;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = false;
            // 
            // btnLamMoi
            // 
            btnLamMoi.BackColor = Color.LightBlue;
            btnLamMoi.Location = new Point(282, 245);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(84, 37);
            btnLamMoi.TabIndex = 10;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = false;
            // 
            // dgvLop
            // 
            dgvLop.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLop.Dock = DockStyle.Bottom;
            dgvLop.Location = new Point(0, 327);
            dgvLop.Name = "dgvLop";
            dgvLop.RowHeadersWidth = 51;
            dgvLop.Size = new Size(800, 123);
            dgvLop.TabIndex = 11;
            // 
            // FormLop
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgvLop);
            Controls.Add(btnLamMoi);
            Controls.Add(btnXoa);
            Controls.Add(btnSua);
            Controls.Add(btnThem);
            Controls.Add(cboKhoa);
            Controls.Add(lblMaKhoa);
            Controls.Add(txtTenLop);
            Controls.Add(txtMaLop);
            Controls.Add(lblTenLop);
            Controls.Add(lblMaLop);
            Controls.Add(label1);
            Name = "FormLop";
            Text = "FormLop";
            Load += FormLop_Load;
            ((System.ComponentModel.ISupportInitialize)dgvLop).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label lblMaLop;
        private Label lblTenLop;
        private TextBox txtMaLop;
        private TextBox txtTenLop;
        private Label lblMaKhoa;
        private ComboBox cboKhoa;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLamMoi;
        private DataGridView dgvLop;
    }
}