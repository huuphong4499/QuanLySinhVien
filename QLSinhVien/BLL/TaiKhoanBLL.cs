using System;
using System.Data;
using QLSinhVien.DAL;
using QLSinhVien.DTO;

namespace QLSinhVien.BLL
{
    /// <summary>
    /// Lớp xử lý nghiệp vụ Tài khoản & Đăng nhập
    /// Thành viên phụ trách: An Nguyễn Lâm Khang
    /// Chức năng: Kiểm tra tính hợp lệ dữ liệu, điều phối nghiệp vụ xác thực người dùng
    /// </summary>
    public class TaiKhoanBLL
    {
        private readonly TaiKhoanDAL _taiKhoanDAL;

        public TaiKhoanBLL()
        {
            _taiKhoanDAL = new TaiKhoanDAL();
        }

        /// <summary>
        /// Xử lý nghiệp vụ đăng nhập hệ thống
        /// </summary>
        public (bool ThanhCong, string ThongBao, TaiKhoanDTO? TaiKhoan) DangNhap(string tenDangNhap, string matKhau)
        {
            if (string.IsNullOrWhiteSpace(tenDangNhap) || string.IsNullOrWhiteSpace(matKhau))
            {
                return (false, "Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu!", null);
            }

            tenDangNhap = tenDangNhap.Trim();

            try
            {
                bool hopLe = _taiKhoanDAL.KiemTraDangNhap(tenDangNhap, matKhau);
                if (hopLe)
                {
                    TaiKhoanDTO? tk = _taiKhoanDAL.LayThongTinTaiKhoan(tenDangNhap);
                    return (true, "Đăng nhập thành công!", tk);
                }
                else
                {
                    return (false, "Tên đăng nhập hoặc mật khẩu không đúng!", null);
                }
            }
            catch (Exception ex)
            {
                return (false, "Lỗi kết nối cơ sở dữ liệu khi đăng nhập: " + ex.Message, null);
            }
        }

        /// <summary>
        /// Xử lý nghiệp vụ tạo tài khoản mới
        /// </summary>
        public (bool ThanhCong, string ThongBao) DangKyTaiKhoan(TaiKhoanDTO taiKhoan)
        {
            if (taiKhoan == null)
                return (false, "Dữ liệu tài khoản không hợp lệ!");

            if (string.IsNullOrWhiteSpace(taiKhoan.TenDangNhap))
                return (false, "Tên đăng nhập không được để trống!");

            if (taiKhoan.TenDangNhap.Trim().Length < 3)
                return (false, "Tên đăng nhập phải có ít nhất 3 ký tự!");

            if (string.IsNullOrWhiteSpace(taiKhoan.MatKhau))
                return (false, "Mật khẩu không được để trống!");

            if (taiKhoan.MatKhau.Length < 3)
                return (false, "Mật khẩu phải có ít nhất 3 ký tự!");

            try
            {
                taiKhoan.TenDangNhap = taiKhoan.TenDangNhap.Trim();
                if (_taiKhoanDAL.KiemTraTonTai(taiKhoan.TenDangNhap))
                {
                    return (false, "Tên đăng nhập đã tồn tại trong hệ thống!");
                }

                bool kq = _taiKhoanDAL.ThemTaiKhoan(taiKhoan);
                return kq ? (true, "Đăng ký tài khoản thành công!") : (false, "Đăng ký tài khoản thất bại!");
            }
            catch (Exception ex)
            {
                return (false, "Lỗi nghiệp vụ tài khoản: " + ex.Message);
            }
        }

        /// <summary>
        /// Xử lý nghiệp vụ đổi mật khẩu
        /// </summary>
        public (bool ThanhCong, string ThongBao) DoiMatKhau(string tenDangNhap, string matKhauCu, string matKhauMoi, string xacNhanMatKhau)
        {
            if (string.IsNullOrWhiteSpace(tenDangNhap) || string.IsNullOrWhiteSpace(matKhauCu))
                return (false, "Vui lòng nhập đầy đủ thông tin tài khoản!");

            if (string.IsNullOrWhiteSpace(matKhauMoi))
                return (false, "Vui lòng nhập mật khẩu mới!");

            if (matKhauMoi.Length < 3)
                return (false, "Mật khẩu mới phải có ít nhất 3 ký tự!");

            if (matKhauMoi != xacNhanMatKhau)
                return (false, "Mật khẩu mới và xác nhận mật khẩu không trùng khớp!");

            try
            {
                if (!_taiKhoanDAL.KiemTraDangNhap(tenDangNhap, matKhauCu))
                {
                    return (false, "Mật khẩu cũ không chính xác!");
                }

                bool kq = _taiKhoanDAL.DoiMatKhau(tenDangNhap, matKhauMoi);
                return kq ? (true, "Đổi mật khẩu thành công!") : (false, "Đổi mật khẩu thất bại!");
            }
            catch (Exception ex)
            {
                return (false, "Lỗi khi đổi mật khẩu: " + ex.Message);
            }
        }

        /// <summary>
        /// Lấy toàn bộ danh sách tài khoản
        /// </summary>
        public DataTable LayDanhSachTaiKhoan()
        {
            return _taiKhoanDAL.LayTatCaTaiKhoan();
        }
    }
}
