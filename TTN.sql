-- USE master
-- alter database ElectricShop set single_user with rollback immediate
-- drop database ElectricShop

CREATE DATABASE ElectricShop
GO

USE ElectricShop
GO

CREATE TABLE Category
(
    CategoryID INT PRIMARY KEY IDENTITY(1,1),
    CategoryName NVARCHAR(250) NOT NULL,
    CategoryURL NVARCHAR(50),

    CreatedDate DATETIME DEFAULT GETDATE()
)

CREATE TABLE Product
(
    ProductID INT PRIMARY KEY IDENTITY(1,1),
    ProductName NVARCHAR(250) NOT NULL,
    ProductPrice DECIMAL(18, 0) NOT NULL,
    ProductDescription NVARCHAR(MAX),
    ProductImage NVARCHAR(MAX),
    ProductStock INT,
    ProductStatus BIT DEFAULT 1,
    ProductURL NVARCHAR(250),
    ViewCount INT,

    CreatedDate DATETIME DEFAULT GETDATE(),
    CategoryID INT REFERENCES Category(CategoryID)
)

CREATE TABLE Customer
(
    CustomerID INT PRIMARY KEY IDENTITY(1,1),
    CustomerUsername NVARCHAR(250) UNIQUE NOT NULL,
    CustomerPassword NVARCHAR(250) NOT NULL,
    CustomerEmail NVARCHAR(250),
    CustomerName NVARCHAR(250) NOT NULL,
    CustomerPhone NVARCHAR(20),
    CustomerAddress NVARCHAR(1000),

    CreatedDate DATETIME DEFAULT GETDATE(),
)

CREATE TABLE OrderStatus
(
    StatusID INT PRIMARY KEY,
    StatusName NVARCHAR(250) NOT NULL,

    CreatedDate DATETIME DEFAULT GETDATE(),
)
GO

CREATE TABLE [Order]
(
    OrderID INT PRIMARY KEY IDENTITY(1, 1),
    Total DECIMAL(18, 0),
    OrderDate DATETIME DEFAULT GETDATE(),
    CustomerName NVARCHAR(255) NOT NULL,
    CustomerPhone NVARCHAR(20) NOT NULL,
    CustomerAddress NVARCHAR(255) NOT NULL,

    OrderStatusID INT REFERENCES OrderStatus(StatusID),
    StatusChangeDate DATETIME DEFAULT NULL,
    CustomerID INT REFERENCES Customer(CustomerID)
)
GO

CREATE TABLE OrderDetail
(
    DetailID INT PRIMARY KEY IDENTITY(1, 1),

    Quantity INT NOT NULL,
    OrderID INT REFERENCES [Order](OrderID),
    ProductID INT REFERENCES Product(ProductID)
)
GO

CREATE TABLE [Admin]
(
    AdminID INT PRIMARY KEY IDENTITY(1, 1),
    AdminUsername NVARCHAR(250) UNIQUE NOT NULL,
    AdminPassword NVARCHAR(250) NOT NULL,
    AdminName NVARCHAR(250),

    CreatedDate DATETIME DEFAULT GETDATE(),
)
GO

INSERT INTO Category
VALUES
    (N'Ti vi', N'ti-vi', GETDATE()),
    (N'Điều hòa', N'dieu-hoa', GETDATE()),
    (N'Tủ lạnh', N'tu-lanh', GETDATE())
GO

INSERT INTO OrderStatus
VALUES
    (1, N'Đang xử lý', GETDATE()),
    (2, N'Đang giao hàng', GETDATE()),
    (3, N'Đã giao hàng', GETDATE()),
    (4, N'Hàng có lỗi', GETDATE()),
    (5, N'Đã hủy', GETDATE())
GO

--Admin
INSERT INTO Admin
VALUES(N'hoang', N'4297f44b13955235245b2497399d7a93', N'Nguyễn Hoàng', GETDATE())
GO
--products

INSERT INTO dbo.Product
VALUES
    ( N'SmartTV',
        10000000,
        N'',
        N'https://cdn.tgdd.vn/Products/Images/1942/210506/tcl-l32s6300-12-550x340.jpg',
        20,
        1,
        N'smarttv',
        199,
        GETDATE(),
        1
)

INSERT INTO dbo.Product
VALUES
    ( N'AndroidTV' , -- ProductName - nvarchar(250)
        13500000 , -- ProductPrice - decimal
        N'' , -- Description - nvarchar(max)
        N'https://cdn.tgdd.vn/Products/Images/1942/217954/casper-32hg5000-10-550x340.jpg' , -- ProductImage - nvarchar(max)
        15, -- ProductStock - int
        1 , -- ProductStatus - bit
        N'androidtv' , -- ProductURL - nvarchar(250)
        89,
        GETDATE() , -- CreatedDate - datetime
        1  -- CategoryID - int
)
INSERT INTO dbo.Product
VALUES
    ( N'InternetTV' , -- ProductName - nvarchar(250)
        11250000 , -- ProductPrice - decimal
        N'' , -- Description - nvarchar(max)
        N'https://cdn.tgdd.vn/Products/Images/1942/188214/tcl-40s6500-550x340.jpg' , -- ProductImage - nvarchar(max)
        10 , -- ProductStock - int
        1 , -- ProductStatus - bit
        N'internettv' , -- ProductURL - nvarchar(250)
        320,
        GETDATE() , -- CreatedDate - datetime
        1  -- CategoryID - int
)
INSERT INTO dbo.Product
VALUES
    ( N'Inverter Aqua AQA-KCRV9VKS' , -- ProductName - nvarchar(250)
        6250000 , -- ProductPrice - decimal
        N'' , -- Description - nvarchar(max)
        N'https://cdn.tgdd.vn/Products/Images/2002/217794/sharp-ah-x9xew-1-550x160.jpg' , -- ProductImage - nvarchar(max)
        20 , -- ProductStock - int
        1 , -- ProductStatus - bit
        N'inverter-aqua-aqa-kcrv9vks' , -- ProductURL - nvarchar(250)
        28,
        GETDATE() , -- CreatedDate - datetime
        2  -- CategoryID - int
)
INSERT INTO dbo.Product
VALUES
    ( N'Daikin FTKQ35SAVMV' , -- ProductName - nvarchar(250)
        7500000 , -- ProductPrice - decimal
        N'' , -- Description - nvarchar(max)
        N'https://cdn.tgdd.vn/Products/Images/2002/200017/daikin-atkq25tavmv-1-550x160.jpg' , -- ProductImage - nvarchar(max)
        18 , -- ProductStock - int
        1 , -- ProductStatus - bit
        N'daikin-ftq35savmv' , -- ProductURL - nvarchar(250)
        111,
        GETDATE() , -- CreatedDate - datetime
        2  -- CategoryID - int
)
INSERT INTO dbo.Product
VALUES
    ( N'Midea  MS11D1A' , -- ProductName - nvarchar(250)
        4600000 , -- ProductPrice - decimal
        N'' , -- Description - nvarchar(max)
        N'https://cdn.tgdd.vn/Products/Images/2002/198377/may-lanh-lg-v13enh-1-550x160.jpg' , -- ProductImage - nvarchar(max)
        25 , -- ProductStock - int
        1 , -- ProductStatus - bit
        N'media-ms11d1a' , -- ProductURL - nvarchar(250)
        85,
        GETDATE() , -- CreatedDate - datetime
        2  -- CategoryID - int
)
INSERT INTO dbo.Product
VALUES
    ( N'Samsung 255 Lít RT25M4033S8' , -- ProductName - nvarchar(250)
        19750000 , -- ProductPrice - decimal
        N'' , -- Description - nvarchar(max)
        N'https://cdn.tgdd.vn/Products/Images/1943/156181/samsung-rt22m4032dx-sv-14-300x300.jpg' , -- ProductImage - nvarchar(max)
        16 , -- ProductStock - int
        1 , -- ProductStatus - bit
        N'samsung-255-lít-rt25m4033s8' , -- ProductURL - nvarchar(250)
        123,
        GETDATE() , -- CreatedDate - datetime
        3  -- CategoryID - int
)
INSERT INTO dbo.Product
VALUES
    ( N'Sharp 196 Lít SJ-X201E-DS Inverter' , -- ProductName - nvarchar(250)
        15850000 , -- ProductPrice - decimal
        N'' , -- Description - nvarchar(max)
        N'https://cdn.tgdd.vn/Products/Images/1943/158471/tu-lanh-samsung-rt38k5982bs-sv--300x300.jpg' , -- ProductImage - nvarchar(max)
        22 , -- ProductStock - int
        1 , -- ProductStatus - bit
        N'sharp196-lit-sj-x201e-ds-inverter' , -- ProductURL - nvarchar(250)
        54,
        GETDATE() , -- CreatedDate - datetime
        3  -- CategoryID - int
)
INSERT INTO dbo.Product
VALUES
    ( N'Panasonic NR-BL267VSV1 Inverter 234L' , -- ProductName - nvarchar(250)
        16250000 , -- ProductPrice - decimal
        N'' , -- Description - nvarchar(max)
        N'https://cdn.tgdd.vn/Products/Images/1943/158471/tu-lanh-samsung-rt38k5982bs-sv--300x300.jpg' , -- ProductImage - nvarchar(max)
        17 , -- ProductStock - int
        1 , -- ProductStatus - bit
        N'panasonic-nr-bl267vsv1-inverter-234l' , -- ProductURL - nvarchar(250)
        32,
        GETDATE() , -- CreatedDate - datetime
        3  -- CategoryID - int
)
INSERT INTO dbo.Product
VALUES
    ( N'LG Inverter 613 Lít GR-B247JDS' , -- ProductName - nvarchar(250)
        21550000 , -- ProductPrice - decimal
        N'' , -- Description - nvarchar(max)
        N'https://cdn.tgdd.vn/Products/Images/1943/74706/sharp-sj-fx630v-st--300x300.jpg' , -- ProductImage - nvarchar(max)
        16 , -- ProductStock - int
        1 , -- ProductStatus - bit
        N'lg-inverter-613-lit-gr-b247jds' , -- ProductURL - nvarchar(250)
        420,
        GETDATE() , -- CreatedDate - datetime
        3  -- CategoryID - int
)

-----------------------------------------
-- public void FixEfProviderServicesProblem()
--         {
--             var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
--         }
