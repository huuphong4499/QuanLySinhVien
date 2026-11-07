using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace QLSinhVien
{
    public partial class FormMenu : Form
    {
        public FormMenu()
        {
            InitializeComponent();

            // Đăng ký sự kiện nút
            btnQuanLyLop.Click += btnQuanLyLop_Click;
            btnQuanLySinhVien.Click += btnQuanLySinhVien_Click;
            btnDangXuat.Click += btnDangXuat_Click;

            // Tự điều chỉnh giao diện khi thay đổi kích thước
            this.Resize += FormMenu_Resize;
            this.Load += FormMenu_Load;
        }

        // Thiết lập giao diện Menu
        private void FormMenu_Load(object? sender, EventArgs e)
        {
            this.Text = "HỆ THỐNG QUẢN LÝ SINH VIÊN";
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.MinimumSize = new Size(800, 500);

            // Thêm biểu tượng và nội dung cho các nút
            btnQuanLyLop.Text = "▦  QUẢN LÝ LỚP";
            btnQuanLySinhVien.Text = "♙  QUẢN LÝ SINH VIÊN";
            btnDangXuat.Text = "↪  ĐĂNG XUẤT";

            // Màu nền
            btnQuanLyLop.BackColor = Color.FromArgb(37, 99, 235);
            btnQuanLySinhVien.BackColor = Color.FromArgb(16, 150, 120);
            btnDangXuat.BackColor = Color.FromArgb(220, 53, 69);

            // Màu chữ
            btnQuanLyLop.ForeColor = Color.White;
            btnQuanLySinhVien.ForeColor = Color.White;
            btnDangXuat.ForeColor = Color.White;

            // Kiểu nút
            btnQuanLyLop.FlatStyle = FlatStyle.Flat;
            btnQuanLySinhVien.FlatStyle = FlatStyle.Flat;
            btnDangXuat.FlatStyle = FlatStyle.Flat;

            // Xóa viền nút
            btnQuanLyLop.FlatAppearance.BorderSize = 0;
            btnQuanLySinhVien.FlatAppearance.BorderSize = 0;
            btnDangXuat.FlatAppearance.BorderSize = 0;

            // Font chữ
            btnQuanLyLop.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            btnQuanLySinhVien.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            btnDangXuat.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            // Căn giữa nội dung
            btnQuanLyLop.TextAlign = ContentAlignment.MiddleCenter;
            btnQuanLySinhVien.TextAlign = ContentAlignment.MiddleCenter;
            btnDangXuat.TextAlign = ContentAlignment.MiddleCenter;

            // Hiển thị con trỏ bàn tay
            btnQuanLyLop.Cursor = Cursors.Hand;
            btnQuanLySinhVien.Cursor = Cursors.Hand;
            btnDangXuat.Cursor = Cursors.Hand;

            // Căn chỉnh giao diện lần đầu
            FormMenu_Resize(this, EventArgs.Empty);
        }

        // Điều chỉnh vị trí và kích thước nút
        private void FormMenu_Resize(object? sender, EventArgs e)
        {
            int w = this.ClientSize.Width;
            int h = this.ClientSize.Height;

            int buttonWidth = Math.Min(320, w / 3);
            int buttonHeight = 110;
            int gap = 40;

            int totalWidth = buttonWidth * 2 + gap;
            int startX = (w - totalWidth) / 2;
            int centerY = h / 2;

            // Nút quản lý lớp
            btnQuanLyLop.Size = new Size(buttonWidth, buttonHeight);
            btnQuanLyLop.Location = new Point(
                startX,
                centerY - buttonHeight / 2
            );

            // Nút quản lý sinh viên
            btnQuanLySinhVien.Size = new Size(buttonWidth, buttonHeight);
            btnQuanLySinhVien.Location = new Point(
                startX + buttonWidth + gap,
                centerY - buttonHeight / 2
            );

            // Nút đăng xuất
            btnDangXuat.Size = new Size(180, 50);
            btnDangXuat.Location = new Point(
                (w - btnDangXuat.Width) / 2,
                centerY + buttonHeight + 55
            );

            // BO TRÒN 4 GÓC CÁC NÚT
            BoTronButton(btnQuanLyLop, 20);
            BoTronButton(btnQuanLySinhVien, 20);
            BoTronButton(btnDangXuat, 15);
        }

        // Hàm bo tròn góc Button
        private void BoTronButton(Button btn, int banKinh)
        {
            int d = banKinh * 2;

            using (GraphicsPath path = new GraphicsPath())
            {
                path.StartFigure();

                // Góc trên bên trái
                path.AddArc(0, 0, d, d, 180, 90);

                // Góc trên bên phải
                path.AddArc(btn.Width - d, 0, d, d, 270, 90);

                // Góc dưới bên phải
                path.AddArc(
                    btn.Width - d,
                    btn.Height - d,
                    d, d, 0, 90
                );

                // Góc dưới bên trái
                path.AddArc(
                    0,
                    btn.Height - d,
                    d, d, 90, 90
                );

                path.CloseFigure();

                btn.Region?.Dispose();
                btn.Region = new Region(path);
            }
        }

        // Mở form quản lý lớp
        private void btnQuanLyLop_Click(object? sender, EventArgs e)
        {
            using (FormLop form = new FormLop())
            {
                form.ShowDialog(this);
            }
        }

        // Mở form quản lý sinh viên
        private void btnQuanLySinhVien_Click(object? sender, EventArgs e)
        {
            using (FormQLSinhVien form = new FormQLSinhVien())
            {
                form.ShowDialog(this);
            }
        }

        // Đăng xuất
        private void btnDangXuat_Click(object? sender, EventArgs e)
        {
            DialogResult ketQua = MessageBox.Show(
                "Bạn có chắc chắn muốn đăng xuất không?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (ketQua == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
