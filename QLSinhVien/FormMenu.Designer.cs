namespace QLSinhVien
{
    partial class FormMenu
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
            btnQuanLyLop = new Button();
            btnQuanLySinhVien = new Button();
            btnDangXuat = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // btnQuanLyLop
            // 
            btnQuanLyLop.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnQuanLyLop.BackColor = Color.Chartreuse;
            btnQuanLyLop.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnQuanLyLop.ImageAlign = ContentAlignment.TopCenter;
            btnQuanLyLop.Location = new Point(121, 132);
            btnQuanLyLop.Name = "btnQuanLyLop";
            btnQuanLyLop.Size = new Size(247, 67);
            btnQuanLyLop.TabIndex = 0;
            btnQuanLyLop.Text = "Quản lý lớp";
            btnQuanLyLop.UseVisualStyleBackColor = false;
            // 
            // btnQuanLySinhVien
            // 
            btnQuanLySinhVien.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnQuanLySinhVien.BackColor = Color.Yellow;
            btnQuanLySinhVien.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnQuanLySinhVien.Location = new Point(424, 132);
            btnQuanLySinhVien.Name = "btnQuanLySinhVien";
            btnQuanLySinhVien.Size = new Size(247, 67);
            btnQuanLySinhVien.TabIndex = 1;
            btnQuanLySinhVien.Text = "Quản lý sinh viên";
            btnQuanLySinhVien.UseVisualStyleBackColor = false;
            // 
            // btnDangXuat
            // 
            btnDangXuat.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnDangXuat.BackColor = Color.Red;
            btnDangXuat.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDangXuat.ImageAlign = ContentAlignment.BottomCenter;
            btnDangXuat.Location = new Point(298, 245);
            btnDangXuat.Name = "btnDangXuat";
            btnDangXuat.Size = new Size(207, 51);
            btnDangXuat.TabIndex = 2;
            btnDangXuat.Text = "Đăng xuất";
            btnDangXuat.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(18, 29);
            label1.Name = "label1";
            label1.Size = new Size(233, 31);
            label1.TabIndex = 3;
            label1.Text = "QUẢN LÝ SINH VIÊN";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // FormMenu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label1);
            Controls.Add(btnDangXuat);
            Controls.Add(btnQuanLySinhVien);
            Controls.Add(btnQuanLyLop);
            Name = "FormMenu";
            Text = "FormMenu";
            Load += FormMenu_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnQuanLyLop;
        private Button btnQuanLySinhVien;
        private Button btnDangXuat;
        private Label label1;
    }
}