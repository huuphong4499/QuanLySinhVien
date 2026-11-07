# BÁO CÁO PHÂN CÔNG ĐỒ ÁN KẾT THÚC HỌC PHẦN
## HỆ THỐNG QUẢN LÝ SINH VIÊN (QLSinhVien)
**Mô hình kiến trúc:** 3 Lớp Chuẩn (BLL - DAL - DTO)  
**Nền tảng:** .NET 8 (C# WinForms) + SQL Server  
**Nguyên tắc giao diện:** Giữ nguyên 100% UI, tách biệt logic nghiệp vụ hoàn chỉnh  

---

## 1. CẤU TRÚC THƯ MỤC HỆ THỐNG

### A. Bên trong Project `QLSinhVien/` (Code vận hành trực tiếp ứng dụng):
- `DTO/`:
  - `TaiKhoanDTO.cs` (Phụ trách: An Nguyễn Lâm Khang)
  - `KhoaDTO.cs` (Phụ trách: Chu Hữu Phong)
  - `LopDTO.cs` (Phụ trách: Phan Nhựt An)
  - `SinhVienDTO.cs` (Phụ trách: Trần Thành Long)
- `DAL/`:
  - `TaiKhoanDAL.cs` (Phụ trách: An Nguyễn Lâm Khang)
  - `KhoaDAL.cs` (Phụ trách: Chu Hữu Phong)
  - `LopDAL.cs` (Phụ trách: Phan Nhựt An)
  - `SinhVienDAL.cs` (Phụ trách: Trần Thành Long)
- `BLL/`:
  - `TaiKhoanBLL.cs` (Phụ trách: An Nguyễn Lâm Khang)
  - `KhoaBLL.cs` (Phụ trách: Chu Hữu Phong)
  - `LopBLL.cs` (Phụ trách: Phan Nhựt An)
  - `SinhVienBLL.cs` (Phụ trách: Trần Thành Long)

### B. 4 Folder độc lập của 4 thành viên (Đặt ở thư mục ngoài để nộp bài / báo cáo cá nhân):
1. **`An nguyễn lâm khang/`**:
   - `BLL/TaiKhoanBLL.cs`
   - `DAL/TaiKhoanDAL.cs`
   - `DTO/TaiKhoanDTO.cs`
   - `README.md`
2. **`chu hữu phong/`**:
   - `BLL/KhoaBLL.cs`
   - `DAL/KhoaDAL.cs`
   - `DTO/KhoaDTO.cs`
   - `README.md`
3. **`phan nhựt an/`**:
   - `BLL/LopBLL.cs`
   - `DAL/LopDAL.cs`
   - `DTO/LopDTO.cs`
   - `README.md`
4. **`trần thành long/`**:
   - `BLL/SinhVienBLL.cs`
   - `DAL/SinhVienDAL.cs`
   - `DTO/SinhVienDTO.cs`
   - `README.md`

---

## 2. BẢNG PHÂN CÔNG NHIỆM VỤ 4 THÀNH VIÊN

| STT | Họ và Tên | Module phụ trách | Các lớp thành phần | Mô tả công việc |
|:---:|:---|:---|:---|:---|
| 1 | **An Nguyễn Lâm Khang** | **Quản lý Tài khoản & Đăng nhập** | - `TaiKhoanDTO.cs`<br>- `TaiKhoanDAL.cs`<br>- `TaiKhoanBLL.cs` | - Thiết kế DTO tài khoản.<br>- Thao tác bảng `TAIKHOAN` an toàn bằng `SqlParameter`.<br>- Kiểm tra đăng nhập, xác thực dữ liệu, đổi mật khẩu. |
| 2 | **Chu Hữu Phong** | **Quản lý Khoa / Đơn vị Đào tạo** | - `KhoaDTO.cs`<br>- `KhoaDAL.cs`<br>- `KhoaBLL.cs` | - Thiết kế DTO Khoa.<br>- Thao tác bảng `KHOA`.<br>- Cung cấp danh mục Khoa cho Form Lớp.<br>- Kiểm tra ràng buộc FK: không cho xóa Khoa khi còn Lớp học. |
| 3 | **Phan Nhựt An** | **Quản lý Lớp học** | - `LopDTO.cs`<br>- `LopDAL.cs`<br>- `LopBLL.cs` | - Thiết kế DTO Lớp học.<br>- Thao tác bảng `LOP` (JOIN `KHOA`).<br>- Thêm/Sửa/Xóa lớp học.<br>- Kiểm tra ràng buộc FK: không cho xóa Lớp khi còn Sinh viên. |
| 4 | **Trần Thành Long** | **Quản lý Sinh viên & Tra cứu** | - `SinhVienDTO.cs`<br>- `SinhVienDAL.cs`<br>- `SinhVienBLL.cs` | - Thiết kế DTO Sinh viên.<br>- Thao tác bảng `SINHVIEN`.<br>- Validate toàn diện: mã SV, họ tên, tuổi (15-80), SĐT 10 số bằng Regex.<br>- Thêm, Sửa, Xóa, Tìm kiếm sinh viên. |

---

## 3. TÀI KHOẢN ĐĂNG NHẬP MẪU
- `admin` / `123456`
- `khang` / `123456`
- `phong` / `123456`
- `an` / `123456`
- `long` / `123456`
