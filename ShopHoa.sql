CREATE DATABASE ShopHoa;
USE ShopHoa;


CREATE TABLE DangKy (
    MaDangKy INT IDENTITY(1,1) PRIMARY KEY,
    TenDangNhap NVARCHAR(255) NOT NULL UNIQUE,
    MatKhau NVARCHAR(255) NOT NULL,
    VaiTroNguoiDung NVARCHAR(20),
);

INSERT INTO DangKy(TenDangNhap, MatKhau, VaiTroNguoiDung) VALUES
('trantrachnguyen', 'nguyen123','Admin'),
('phamthientan', 'tan123','Admin'),
('vodaithanh', 'thanh123','Admin')

CREATE TABLE KhachHang (
    MaKhachHang INT IDENTITY(1,1) PRIMARY KEY,
    MaDangKy INT UNIQUE,
    TenKhachHang NVARCHAR(255) NOT NULL,
    DiaChi NVARCHAR(255),
    SoDienThoai NVARCHAR(20),
    Email NVARCHAR(255),    
    FOREIGN KEY (MaDangKy) REFERENCES DangKy(MaDangKy)
);

CREATE TABLE LoaiHoa (
    MaLoaiHoa INT IDENTITY(1,1) PRIMARY KEY,
    TenLoaiHoa NVARCHAR(255) NOT NULL,
    MoTaLoaiHoa NVARCHAR(255)
);
CREATE TABLE MauSac (
    MaMau INT IDENTITY(1,1) PRIMARY KEY,
    TenMau NVARCHAR(255) NOT NULL
);
CREATE TABLE Hoa (
    MaHoa INT IDENTITY(1,1) PRIMARY KEY,
    TenHoa NVARCHAR(255) NOT NULL,
    MoTaHoa NVARCHAR(255),
	GiaBan DECIMAL(10, 3) NOT NULL,
    SoLuongTonKho INT NOT NULL,
    MaLoaiHoa INT,
    MaMau INT, 
    AnhHoa NVARCHAR(255),
    FOREIGN KEY (MaLoaiHoa) REFERENCES LoaiHoa(MaLoaiHoa),
    FOREIGN KEY (MaMau) REFERENCES MauSac(MaMau)
);


CREATE TABLE DonDatHang (
    MaDonHang INT IDENTITY(1,1) PRIMARY KEY,
    MaKhachHang INT,
    NgayDatHang DATE,
    TenKhachHang NVARCHAR(255),
    DiaChiGiaoHang NVARCHAR(255),
    SoDienThoai NVARCHAR(20),
	SoLuong INT,
    TrangThaiDonHang NVARCHAR(20),
    TongGiaTriDonHang DECIMAL(10, 2),
    FOREIGN KEY (MaKhachHang) REFERENCES KhachHang(MaKhachHang)
);
CREATE TABLE GioHang (
    MaGioHang INT IDENTITY(1,1) PRIMARY KEY,
    MaKhachHang INT,
    MaHoa INT,
    SoLuong INT,
    FOREIGN KEY (MaKhachHang) REFERENCES KhachHang(MaKhachHang),
    FOREIGN KEY (MaHoa) REFERENCES Hoa(MaHoa)
);
CREATE TABLE DoanhThu (
    MaDoanhThu INT IDENTITY(1,1) PRIMARY KEY,
    MaDonHang INT,
	NgayGiaoHang DATE,
    TongDoanhThu DECIMAL(10, 2),
    FOREIGN KEY (MaDonHang) REFERENCES DonDatHang(MaDonHang)
);

INSERT INTO LoaiHoa (TenLoaiHoa, MoTaLoaiHoa)
VALUES
(N'Hoa Hồng Các Loại', N'Các loại hoa hồng đẹp và thơm nức'),
(N'Hoa Cúc Các Loại', N'Hoa cúc tinh khôi với nhiều màu sắc đẹp'),
(N'Lan Hồ Điệp', N'Lan hồ điệp quý phái và sang trọng'),
(N'Bó Hoa Baby ', N'Các loại hoa nhỏ xinh thích hợp làm quà tặng'),
(N'Hoa tươi', N'Hoa chưa bị tàn phai, chưa mất đi sự tươi mới và đẹp đẽ của nó.');

-- Thêm dữ liệu vào bảng MauSac
INSERT INTO MauSac (TenMau)
VALUES
(N'Đỏ'),
(N'Trắng'),
(N'Vàng'),
(N'Hồng'),
(N'Xanh'),
(N'Tím')

-- Thêm dữ liệu vào bảng Hoa
INSERT INTO Hoa (TenHoa, MoTaHoa, GiaBan, SoLuongTonKho, MaLoaiHoa, MaMau,  AnhHoa)
VALUES
(N'Hoa Đỏ', N'Hoa đẹp màu đỏ tươi', 250.000, 100, 1, 1,'hh1.jpg'),
(N'Hoa Hồng Trắng', N'Hoa hồng trắng tinh khôi', 300.000, 80, 1, 2, 'hh2.jpg'),
(N'Hoa Lan Vàng', N'Hoa lan màu vàng nổi bật', 350.000, 120, 2, 3, 'hh3.jpg'),
(N'Hoa Cẩm Chướng', N'Hoa cẩm chướng hương thơm dễ chịu', 400.000, 90, 2, 1, 'hh4.jpg'),
(N'Hoa Tulip Hồng', N'Hoa tulip màu hồng nhẹ nhàng', 280.000, 110, 3, 4, 'hh5.jpg'),
(N'Hoa Đồng Tiền', N'Hoa đồng tiền màu vàng óng ả', 220.000, 150, 3, 3, 'hh6.jpg'),
(N'Hoa Cúc Trắng', N'Hoa cúc trắng tinh khôi', 320.000, 95, 1, 2, 'hh7.jpg'),
(N'Hoa Violet', N'Hoa violet màu tím quyến rũ', 270.000, 105, 3, 6, 'hh8.jpg'),
(N'Hoa Đào Hồng', N'Hoa đào hồng mềm mại', 310.000, 88, 1, 4, 'hh9.jpg'),
(N'Hoa Đỏ Phấn', N'Hoa đỏ phấn đẹp lạ mắt', 280.000, 120, 2, 1, 'hh10.jpg'),
(N'Hoa Sen Hồng', N'Hoa sen màu hồng dịu dàng', 330.000, 85, 3, 4, 'hh11.jpg'),
(N'Hoa Đào Đỏ', N'Hoa đào màu đỏ rực rỡ', 360.000, 95, 1, 1, 'hh12.jpg'),
(N'Hoa Cúc Vàng', N'Hoa cúc màu vàng tinh khôi', 300.000, 110, 2, 3, 'hh13.jpg'),
(N'Hoa Phượng Đỏ', N'Hoa phượng màu đỏ rực', 400.000, 75, 1, 1, 'hh14.jpg'),
(N'Cát Tường Trắng', N'Hoa cát tường trắng màu trắng tinh khôi', 650.000, 130, 5, 2, 'hh15.jpg'),
(N'Hoa Hướng Dương', N'Hoa hướng dương màu vàng tươi', 320.000, 100, 1, 3, 'hh16.jpg'),
(N'Hoa Lan Đỏ', N'Hoa lan màu đỏ quyến rũ', 350.000, 80, 2, 1, 'hh17.jpg'),
(N'Hoa Đỗ Quyên', N'Hoa đỗ quyên màu hồng dịu dàng', 280.000, 105, 3, 1, 'hh18.jpg'),
(N'Hoa Lavender', N'Hoa lavender màu tím nhẹ nhàng', 300.000, 95, 1, 6, 'hh19.jpg'),
(N'Hoa Đỏ Đen Cao Cấp', N'Hoa đỏ đen cao cấp với hương thơm đặc trưng', 800.000, 8, 1, 1, 'hh20.jpg');

