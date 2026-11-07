using System;
using System.Drawing;
using System.Windows.Forms;
using QLSinhVien.BLL;

namespace QLSinhVien
{
    public partial class FormDangNhap : Form
    {
        // Phân hệ Quản lý Tài khoản & Đăng nhập (Phụ trách: An Nguyễn Lâm Khang)
        private readonly TaiKhoanBLL _taiKhoanBLL;

        public FormDangNhap()
        {
            InitializeComponent();
            _taiKhoanBLL = new TaiKhoanBLL();
        }

        // THIẾT KẾ GIAO DIỆN ĐĂNG NHẬP (GIỮ NGUYÊN GIAO DIỆN 100%)
        private void FormDangNhap_Load(object? sender, EventArgs e)
        {
            // Thiết lập cửa sổ
            this.Text = "Đăng nhập hệ thống";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(1000, 650);
            this.MinimumSize = new Size(850, 550);
            this.BackColor = Color.FromArgb(240, 245, 252);

            // Ẩn các Label cũ trên Form
            foreach (Control control in this.Controls)
            {
                if (control is Label)
                {
                    control.Visible = false;
                }
            }

            // Tạo khung đăng nhập
            Panel pnlLogin = new Panel();
            pnlLogin.Name = "pnlLogin";
            pnlLogin.Size = new Size(460, 470);
            pnlLogin.BackColor = Color.White;

            this.Controls.Add(pnlLogin);

            // Tiêu đề
            Label lblTitle = new Label();
            lblTitle.Text = "QUẢN LÝ SINH VIÊN";
            lblTitle.Font = new Font("Segoe UI", 19, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(30, 64, 120);
            lblTitle.Size = new Size(420, 45);
            lblTitle.Location = new Point(20, 35);
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            pnlLogin.Controls.Add(lblTitle);

            // Dòng mô tả
            Label lblSubTitle = new Label();
            lblSubTitle.Text = "Vui lòng đăng nhập để tiếp tục";
            lblSubTitle.Font = new Font("Segoe UI", 10);
            lblSubTitle.ForeColor = Color.Gray;
            lblSubTitle.Size = new Size(420, 30);
            lblSubTitle.Location = new Point(20, 80);
            lblSubTitle.TextAlign = ContentAlignment.MiddleCenter;

            pnlLogin.Controls.Add(lblSubTitle);

            // Nhãn tên đăng nhập
            Label lblUsername = new Label();
            lblUsername.Text = "TÊN ĐĂNG NHẬP";
            lblUsername.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblUsername.ForeColor = Color.FromArgb(50, 60, 80);
            lblUsername.Location = new Point(55, 145);
            lblUsername.Size = new Size(350, 25);

            pnlLogin.Controls.Add(lblUsername);

            // Ô nhập tên đăng nhập
            txtTenDangNhap.Parent = pnlLogin;
            txtTenDangNhap.Location = new Point(55, 175);
            txtTenDangNhap.Size = new Size(350, 38);
            txtTenDangNhap.Font = new Font("Segoe UI", 11);
            txtTenDangNhap.BorderStyle = BorderStyle.FixedSingle;

            // Nhãn mật khẩu
            Label lblPassword = new Label();
            lblPassword.Text = "MẬT KHẨU";
            lblPassword.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblPassword.ForeColor = Color.FromArgb(50, 60, 80);
            lblPassword.Location = new Point(55, 230);
            lblPassword.Size = new Size(350, 25);

            pnlLogin.Controls.Add(lblPassword);

            // Ô nhập mật khẩu
            txtMatKhau.Parent = pnlLogin;
            txtMatKhau.Location = new Point(55, 260);
            txtMatKhau.Size = new Size(350, 38);
            txtMatKhau.Font = new Font("Segoe UI", 11);
            txtMatKhau.BorderStyle = BorderStyle.FixedSingle;
            txtMatKhau.UseSystemPasswordChar = true;

            // Nút đăng nhập
            btnDangNhap.Parent = pnlLogin;
            btnDangNhap.Text = "ĐĂNG NHẬP";
            btnDangNhap.Location = new Point(55, 335);
            btnDangNhap.Size = new Size(350, 48);
            btnDangNhap.BackColor = Color.FromArgb(37, 99, 235);
            btnDangNhap.ForeColor = Color.White;
            btnDangNhap.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnDangNhap.FlatStyle = FlatStyle.Flat;
            btnDangNhap.FlatAppearance.BorderSize = 0;
            btnDangNhap.Cursor = Cursors.Hand;

            // Nút thoát
            btnThoat.Parent = pnlLogin;
            btnThoat.Text = "THOÁT";
            btnThoat.Location = new Point(55, 395);
            btnThoat.Size = new Size(350, 42);
            btnThoat.BackColor = Color.FromArgb(230, 235, 242);
            btnThoat.ForeColor = Color.FromArgb(60, 70, 85);
            btnThoat.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnThoat.FlatStyle = FlatStyle.Flat;
            btnThoat.FlatAppearance.BorderSize = 0;
            btnThoat.Cursor = Cursors.Hand;

            // Đặt Enter để đăng nhập
            this.AcceptButton = btnDangNhap;

            // Căn giữa khung đăng nhập
            void CanGiuaKhung()
            {
                pnlLogin.Location = new Point(
                    (this.ClientSize.Width - pnlLogin.Width) / 2,
                    (this.ClientSize.Height - pnlLogin.Height) / 2
                );
            }

            CanGiuaKhung();

            this.Resize += (s, args) =>
            {
                CanGiuaKhung();
            };
        }

        // CODE NÚT ĐĂNG NHẬP - GỌI BLL (AN NGUYỄN LÂM KHANG)
        private void btnDangNhap_Click(object? sender, EventArgs e)
        {
            string tenDangNhap = txtTenDangNhap.Text;
            string matKhau = txtMatKhau.Text;

            // Tránh xử lý nhiều lần khi nhấn liên tục
            btnDangNhap.Enabled = false;

            try
            {
                var ketQua = _taiKhoanBLL.DangNhap(tenDangNhap, matKhau);

                if (ketQua.ThanhCong)
                {
                    // Mở Menu chính ngay khi đăng nhập đúng
                    FormMenu menu = new FormMenu();

                    // Đóng đăng nhập khi Menu đóng
                    menu.FormClosed += (s, args) => this.Close();

                    // Ẩn Form đăng nhập
                    this.Hide();

                    // Hiển thị Menu chính
                    menu.Show();
                }
                else
                {
                    MessageBox.Show(
                        ketQua.ThongBao,
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtMatKhau.Clear();
                    txtMatKhau.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi hệ thống: " + ex.Message,
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                if (!this.IsDisposed)
                {
                    btnDangNhap.Enabled = true;
                }
            }
        }

        // CODE NÚT THOÁT
        private void btnThoat_Click(object? sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn thoát không?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
