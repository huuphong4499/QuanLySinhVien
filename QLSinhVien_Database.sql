-- ========================================================
-- DỰ ÁN KẾT THÚC HỌC PHẦN: HỆ THỐNG QUẢN LÝ SINH VIÊN
-- CƠ SỞ DỮ LIỆU: QLSinhVien
-- KIẾN TRÚC: 3 Lớp Chuẩn (BLL - DAL - DTO)
-- PHÂN CÔNG 4 THÀNH VIÊN:
--   1. An Nguyễn Lâm Khang: Quản lý Tài khoản & Xác thực Đăng nhập
--   2. Chu Hữu Phong: Quản lý Khoa / Đơn vị
--   3. Phan Nhựt An: Quản lý Lớp học
--   4. Trần Thành Long: Quản lý Sinh viên & Tra cứu hồ sơ
-- ========================================================

-- 1. Tạo cơ sở dữ liệu (Nếu chưa có)
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'QLSinhVien')
BEGIN
    CREATE DATABASE QLSinhVien;
END
GO

USE QLSinhVien;
GO

-- 2. Bảng TAIKHOAN (Phụ trách: An Nguyễn Lâm Khang)
IF OBJECT_ID('dbo.TAIKHOAN', 'U') IS NOT NULL
    DROP TABLE dbo.TAIKHOAN;
GO

CREATE TABLE TAIKHOAN
(
    TenDangNhap NVARCHAR(50) PRIMARY KEY,
    MatKhau NVARCHAR(100) NOT NULL
);
GO

-- 3. Bảng KHOA (Phụ trách: Chu Hữu Phong)
IF OBJECT_ID('dbo.SINHVIEN', 'U') IS NOT NULL
    DROP TABLE dbo.SINHVIEN;
GO

IF OBJECT_ID('dbo.LOP', 'U') IS NOT NULL
    DROP TABLE dbo.LOP;
GO

IF OBJECT_ID('dbo.KHOA', 'U') IS NOT NULL
    DROP TABLE dbo.KHOA;
GO

CREATE TABLE KHOA
(
    MaKhoa NVARCHAR(50) PRIMARY KEY,
    TenKhoa NVARCHAR(100) NOT NULL
);
GO

-- 4. Bảng LOP (Phụ trách: Phan Nhựt An)
CREATE TABLE LOP
(
    MaLop NVARCHAR(50) PRIMARY KEY,
    TenLop NVARCHAR(100) NOT NULL,
    MaKhoa NVARCHAR(50) NOT NULL,
    CONSTRAINT FK_LOP_KHOA FOREIGN KEY (MaKhoa) REFERENCES KHOA(MaKhoa)
        ON UPDATE CASCADE
        ON DELETE NO ACTION
);
GO

-- 5. Bảng SINHVIEN (Phụ trách: Trần Thành Long)
CREATE TABLE SINHVIEN
(
    MaSV NVARCHAR(50) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    NgaySinh DATE NOT NULL,
    GioiTinh NVARCHAR(10) NOT NULL,
    DiaChi NVARCHAR(200) NULL,
    SoDienThoai NVARCHAR(20) NULL,
    MaLop NVARCHAR(50) NOT NULL,
    CONSTRAINT FK_SINHVIEN_LOP FOREIGN KEY (MaLop) REFERENCES LOP(MaLop)
        ON UPDATE CASCADE
        ON DELETE NO ACTION
);
GO

-- ========================================================
-- CHÈN DỮ LIỆU MẪU ĐỂ CHẠY THỬ NGHIỆM
-- ========================================================

-- Dữ liệu mẫu Tài khoản (An Nguyễn Lâm Khang)
INSERT INTO TAIKHOAN (TenDangNhap, MatKhau)
VALUES 
    (N'admin', N'123456'),
    (N'khang', N'123456'),
    (N'phong', N'123456'),
    (N'an', N'123456'),
    (N'long', N'123456');
GO

-- Dữ liệu mẫu Khoa (Chu Hữu Phong)
INSERT INTO KHOA (MaKhoa, TenKhoa)
VALUES 
    (N'CNTT', N'Khoa Công nghệ thông tin'),
    (N'DTVT', N'Khoa Điện tử viễn thông'),
    (N'KTXD', N'Khoa Kỹ thuật xây dựng'),
    (N'QTKD', N'Khoa Quản trị kinh doanh'),
    (N'NN', N'Khoa Ngoại ngữ');
GO

-- Dữ liệu mẫu Lớp (Phan Nhựt An)
INSERT INTO LOP (MaLop, TenLop, MaKhoa)
VALUES 
    (N'CNTT1', N'Công nghệ thông tin 1', N'CNTT'),
    (N'CNTT2', N'Công nghệ thông tin 2', N'CNTT'),
    (N'CNTT3', N'Công nghệ thông tin 3', N'CNTT'),
    (N'DTVT1', N'Điện tử viễn thông 1', N'DTVT'),
    (N'QTKD1', N'Quản trị kinh doanh 1', N'QTKD');
GO

-- Dữ liệu mẫu Sinh viên (Trần Thành Long)
INSERT INTO SINHVIEN (MaSV, HoTen, NgaySinh, GioiTinh, DiaChi, SoDienThoai, MaLop)
VALUES 
    (N'SV001', N'Nguyễn Văn An', '2004-01-15', N'Nam', N'Hà Nội', N'0912345678', N'CNTT1'),
    (N'SV002', N'Trần Thị Bình', '2004-03-22', N'Nữ', N'Đà Nẵng', N'0987654321', N'CNTT1'),
    (N'SV003', N'Lê Văn Cường', '2003-11-05', N'Nam', N'Hồ Chí Minh', N'0901234567', N'CNTT2'),
    (N'SV004', N'Phạm Thị Dung', '2004-07-19', N'Nữ', N'Hải Phòng', N'0934567890', N'CNTT2'),
    (N'SV005', N'Hoàng Văn Em', '2004-09-30', N'Nam', N'Cần Thơ', N'0945678901', N'CNTT3');
GO

-- KIỂM TRA TRUY VẤN
SELECT * FROM TAIKHOAN;
SELECT * FROM KHOA;
SELECT L.MaLop, L.TenLop, L.MaKhoa, K.TenKhoa FROM LOP L LEFT JOIN KHOA K ON L.MaKhoa = K.MaKhoa;
SELECT S.MaSV, S.HoTen, S.NgaySinh, S.GioiTinh, S.DiaChi, S.SoDienThoai, S.MaLop, L.TenLop FROM SINHVIEN S LEFT JOIN LOP L ON S.MaLop = L.MaLop;
GO
