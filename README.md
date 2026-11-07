# HỆ THỐNG QUẢN LÝ SINH VIÊN (QLSinhVien)

Dự án đồ án kết thúc học phần môn Lập trình Windows / C#.NET WinForms.
Ứng dụng kiến trúc **3 lớp chuẩn (BLL - DAL - DTO)** kết nối cơ sở dữ liệu **SQL Server**.

---

## 🚀 TÍNH NĂNG ĐẶC BIỆT: TỰ ĐỘNG KHỞI TẠO CSDL CHO MỌI NGƯỜI
Hệ thống được tích hợp cơ chế **tự động dò tìm SQL Server instance và tự động tạo Database + dữ liệu mẫu**:
- **Bất kỳ ai (thành viên trong nhóm hay giảng viên chấm bài)** khi clone project về:
  1. Chỉ cần mở solution bằng **Visual Studio** (hoặc Rider / VS Code).
  2. Bấm **Run (F5)** hoặc chạy chương trình.
  3. Hệ thống sẽ **tự động dò tìm SQL Server đang chạy trên máy** (hỗ trợ `localhost`, `.`, `.\SQLEXPRESS`, `(localdb)\MSSQLLocalDB`,...);
  4. Nếu cơ sở dữ liệu `QLSinhVien` chưa có, chương trình **tự động tạo Database và chèn sẵn dữ liệu mẫu** (tài khoản, khoa, lớp, sinh viên) mà **không cần chạy lệnh SQL thủ công**!

---

## 🔑 TÀI KHOẢN ĐĂNG NHẬP MẶT ĐỊNH
Sau khi khởi chạy ứng dụng, bạn có thể đăng nhập bằng một trong các tài khoản sau:

| Tên đăng nhập | Mật khẩu | Phân quyền | Ghi chú |
|:---:|:---:|:---:|:---|
| **`admin`** | **`123456`** | Quản trị viên | Tài khoản quản trị toàn quyền |
| **`khang`** | **`123456`** | Người dùng | Tài khoản bạn An Nguyễn Lâm Khang |
| **`phong`** | **`123456`** | Người dùng | Tài khoản bạn Chu Hữu Phong |
| **`an`** | **`123456`** | Người dùng | Tài khoản bạn Phan Nhựt An |
| **`long`** | **`123456`** | Người dùng | Tài khoản bạn Trần Thành Long |

---

## ⚙️ CẤU HÌNH TÙY CHỌN (NẾU CẦN)
Nếu máy bạn sử dụng SQL Server có tài khoản hoặc cấu hình riêng, bạn có thể mở file `dbsettings.json` trong thư mục chạy để tùy chỉnh:
```json
{
  "Server": "localhost",
  "Database": "QLSinhVien",
  "IntegratedSecurity": true,
  "TrustServerCertificate": true
}
```
*(Nếu muốn tạo thủ công qua SQL Server Management Studio, file script đã có sẵn tại `QLSinhVien_Database.sql`)*.

---

## 👥 PHÂN CÔNG NHIỆM VỤ 4 THÀNH VIÊN
Hệ thống được xây dựng theo mô hình 3 lớp chuẩn (`BLL` - `DAL` - `DTO`) với 4 phân hệ độc lập:
1. **An Nguyễn Lâm Khang**: Phân hệ **Quản lý Tài khoản & Đăng nhập** (`TaiKhoanDTO`, `TaiKhoanDAL`, `TaiKhoanBLL`).
2. **Chu Hữu Phong**: Phân hệ **Quản lý Khoa / Đơn vị** (`KhoaDTO`, `KhoaDAL`, `KhoaBLL`).
3. **Phan Nhựt An**: Phân hệ **Quản lý Lớp học** (`LopDTO`, `LopDAL`, `LopBLL`).
4. **Trần Thành Long**: Phân hệ **Quản lý Sinh viên & Tra cứu hồ sơ** (`SinhVienDTO`, `SinhVienDAL`, `SinhVienBLL`).
