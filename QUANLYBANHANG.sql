﻿USE MASTER
GO
CREATE DATABASE QUANLYBANHANG
GO
USE QUANLYBANHANG
GO

CREATE TABLE ACCOUNT
(
USERNAME CHAR(50) NOT NULL,
PASSWORD CHAR(1000) NOT NULL,
TYPE INT NOT NULL DEFAULT 0 ---1: ADMIN && 0: STAFF,
PRIMARY KEY(USERNAME)

)
GO

CREATE TABLE KHACHHANG
(
MAKH INT IDENTITY(1,1) ,
TENKH NVARCHAR(50),
SDTKH CHAR(11),
DIACHI NVARCHAR(50) DEFAULT N'HỒ CHÍ MINH',
TONGTIEN INT,
PRIMARY KEY(MAKH)
)
GO

CREATE TABLE LOAISP
(
MALOAI INT IDENTITY(1,1) NOT NULL,
TENLOAI NVARCHAR (50)
PRIMARY KEY(MALOAI)
)
GO

CREATE TABLE SANPHAM
(
MASP INT IDENTITY(1,1) NOT NULL,
TENSP NVARCHAR(100),
LOAISP CHAR(50),
SOLUONGTON INT,
DONGIA INT,
PRIMARY KEY(MASP),
FOREIGN KEY(MASP) REFERENCES LOAISP(MALOAI)
)
GO

CREATE TABLE HOADON
(
MAHD INT IDENTITY(1,1) NOT NULL,
MAKH INT NOT NULL,
NVBAN NVARCHAR DEFAULT N'NHÂN VIÊN 1',
TGMUA DATE NOT NULL DEFAULT GETDATE(),
TONGTIEN INT,
PRIMARY KEY(MAHD),
FOREIGN KEY(MAKH) REFERENCES KHACHHANG(MAKH)
)
GO

CREATE TABLE CHITIETHOADON
(
ID INT IDENTITY PRIMARY KEY,
MAHD INT,
MASP INT,
COUNT INT,	
FOREIGN KEY (MAHD) REFERENCES HOADON(MAHD),
FOREIGN KEY (MASP) REFERENCES SANPHAM(MASP)
)