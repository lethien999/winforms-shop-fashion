/*
================================================================================
    HỆ THỐNG QUẢN LÝ BÁN HÀNG CHO SHOP THỜI TRANG - WINFORMS
    Script tạo Database và các bảng
================================================================================
    
    HƯỚNG DẪN SỬ DỤNG:
    1. Mở SQL Server Management Studio (SSMS)
    2. Kết nối với SQL Server (Server: . hoặc localhost)
    3. Copy toàn bộ nội dung file này
    4. Paste vào cửa sổ Query trong SSMS
    5. Nhấn F5 hoặc click Execute để chạy
    
    THÔNG TIN ĐĂNG NHẬP MẶC ĐỊNH SAU KHI CHẠY SCRIPT:
    - Username: admin
    - Password: admin123
    - Role: Admin
    
================================================================================
*/

USE master;
GO

-- ============================================================================
-- BƯỚC 1: TẠO DATABASE
-- ============================================================================
PRINT '========================================';
PRINT 'BƯỚC 1: Kiểm tra và tạo Database';
PRINT '========================================';

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'WinFormsFashionShopDb')
BEGIN
    CREATE DATABASE WinFormsFashionShopDb;
    PRINT '✓ Database WinFormsFashionShopDb đã được tạo thành công!';
END
ELSE
BEGIN
    PRINT '✓ Database WinFormsFashionShopDb đã tồn tại.';
END
GO

USE WinFormsFashionShopDb;
GO

-- ============================================================================
-- BƯỚC 2: TẠO BẢNG USERS (Người dùng hệ thống)
-- ============================================================================
PRINT '';
PRINT '========================================';
PRINT 'BƯỚC 2: Tạo bảng Users';
PRINT '========================================';

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Users')
BEGIN
    CREATE TABLE Users (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Username NVARCHAR(50) NOT NULL UNIQUE,
        PasswordHash NVARCHAR(255) NOT NULL,
        FullName NVARCHAR(100) NOT NULL,
        Role NVARCHAR(20) NOT NULL,                    -- Admin, Staff
        IsActive BIT NOT NULL DEFAULT 1,
        CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
        UpdatedAt DATETIME NULL
    );
    PRINT '✓ Bảng Users đã được tạo thành công!';
END
ELSE
BEGIN
    PRINT '✓ Bảng Users đã tồn tại.';
END
GO

-- ============================================================================
-- BƯỚC 3: TẠO BẢNG CATEGORIES (Danh mục sản phẩm)
-- ============================================================================
PRINT '';
PRINT '========================================';
PRINT 'BƯỚC 3: Tạo bảng Categories';
PRINT '========================================';

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Categories')
BEGIN
    CREATE TABLE Categories (
        Id INT PRIMARY KEY IDENTITY(1,1),
        CategoryName NVARCHAR(100) NOT NULL,            -- Tên danh mục (Áo, Quần, Phụ kiện...)
        Description NVARCHAR(255) NULL,                 -- Mô tả
        IsActive BIT NOT NULL DEFAULT 1
    );
    PRINT '✓ Bảng Categories đã được tạo thành công!';
END
ELSE
BEGIN
    PRINT '✓ Bảng Categories đã tồn tại.';
END
GO

-- ============================================================================
-- BƯỚC 4: TẠO BẢNG PRODUCTS (Sản phẩm)
-- ============================================================================
PRINT '';
PRINT '========================================';
PRINT 'BƯỚC 4: Tạo bảng Products';
PRINT '========================================';

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Products')
BEGIN
    CREATE TABLE Products (
        Id INT PRIMARY KEY IDENTITY(1,1),
        ProductCode NVARCHAR(50) NOT NULL UNIQUE,       -- Mã sản phẩm (SKU)
        Name NVARCHAR(200) NOT NULL,                    -- Tên sản phẩm
        CategoryId INT NOT NULL,                        -- FK -> Categories.Id
        UnitPrice DECIMAL(18,2) NOT NULL,               -- Giá bán
        Unit NVARCHAR(20) NOT NULL DEFAULT 'cái',       -- Đơn vị tính (cái, bộ, chiếc...)
        ImagePath NVARCHAR(500) NULL,                    -- Đường dẫn ảnh sản phẩm
        IsActive BIT NOT NULL DEFAULT 1,
        CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
        UpdatedAt DATETIME NULL,
        FOREIGN KEY (CategoryId) REFERENCES Categories(Id)
    );
    PRINT '✓ Bảng Products đã được tạo thành công!';
END
ELSE
BEGIN
    PRINT '✓ Bảng Products đã tồn tại.';
END
GO

-- ============================================================================
-- BƯỚC 5: TẠO BẢNG CUSTOMERS (Khách hàng)
-- ============================================================================
PRINT '';
PRINT '========================================';
PRINT 'BƯỚC 5: Tạo bảng Customers';
PRINT '========================================';

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Customers')
BEGIN
    CREATE TABLE Customers (
        Id INT PRIMARY KEY IDENTITY(1,1),
        CustomerName NVARCHAR(150) NOT NULL,            -- Tên khách hàng
        Phone NVARCHAR(20) NULL,                        -- Số điện thoại
        Email NVARCHAR(100) NULL,                        -- Email
        Address NVARCHAR(255) NULL,                      -- Địa chỉ
        IsActive BIT NOT NULL DEFAULT 1,
        CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
        UpdatedAt DATETIME NULL
    );
    PRINT '✓ Bảng Customers đã được tạo thành công!';
END
ELSE
BEGIN
    PRINT '✓ Bảng Customers đã tồn tại.';
END
GO

-- ============================================================================
-- BƯỚC 6: TẠO BẢNG ORDERS (Đơn hàng/Hóa đơn)
-- ============================================================================
PRINT '';
PRINT '========================================';
PRINT 'BƯỚC 6: Tạo bảng Orders';
PRINT '========================================';

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Orders')
BEGIN
    CREATE TABLE Orders (
        Id INT PRIMARY KEY IDENTITY(1,1),
        OrderCode NVARCHAR(50) NOT NULL UNIQUE,          -- Mã hóa đơn (ORD202412010001)
        OrderDate DATETIME NOT NULL DEFAULT GETDATE(),  -- Ngày lập hóa đơn
        CustomerId INT NULL,                            -- FK -> Customers.Id (nullable - cho phép hóa đơn không gắn khách)
        UserId INT NOT NULL,                             -- FK -> Users.Id (Nhân viên lập hóa đơn)
        TotalAmount DECIMAL(18,2) NOT NULL,              -- Tổng tiền
        PaymentMethod NVARCHAR(50) NULL,                 -- Phương thức thanh toán (Tiền mặt, Thẻ, Chuyển khoản...)
        Notes NVARCHAR(255) NULL,                        -- Ghi chú
        Status NVARCHAR(20) NOT NULL DEFAULT 'Paid',     -- Trạng thái: Paid, Cancelled
        FOREIGN KEY (CustomerId) REFERENCES Customers(Id),
        FOREIGN KEY (UserId) REFERENCES Users(Id)
    );
    PRINT '✓ Bảng Orders đã được tạo thành công!';
END
ELSE
BEGIN
    PRINT '✓ Bảng Orders đã tồn tại.';
END
GO

-- ============================================================================
-- BƯỚC 7: TẠO BẢNG ORDERITEMS (Chi tiết đơn hàng)
-- ============================================================================
PRINT '';
PRINT '========================================';
PRINT 'BƯỚC 7: Tạo bảng OrderItems';
PRINT '========================================';

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'OrderItems')
BEGIN
    CREATE TABLE OrderItems (
        Id INT PRIMARY KEY IDENTITY(1,1),
        OrderId INT NOT NULL,                           -- FK -> Orders.Id
        ProductId INT NOT NULL,                          -- FK -> Products.Id
        Quantity INT NOT NULL,                           -- Số lượng
        UnitPrice DECIMAL(18,2) NOT NULL,                 -- Đơn giá tại thời điểm bán
        LineTotal DECIMAL(18,2) NOT NULL,                -- Thành tiền = Quantity * UnitPrice
        FOREIGN KEY (OrderId) REFERENCES Orders(Id) ON DELETE CASCADE,
        FOREIGN KEY (ProductId) REFERENCES Products(Id)
    );
    PRINT '✓ Bảng OrderItems đã được tạo thành công!';
END
ELSE
BEGIN
    PRINT '✓ Bảng OrderItems đã tồn tại.';
END
GO

-- ============================================================================
-- BƯỚC 8: TẠO BẢNG INVENTORY (Tồn kho)
-- ============================================================================
PRINT '';
PRINT '========================================';
PRINT 'BƯỚC 8: Tạo bảng Inventory';
PRINT '========================================';

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Inventory')
BEGIN
    CREATE TABLE Inventory (
        Id INT PRIMARY KEY IDENTITY(1,1),
        ProductId INT NOT NULL UNIQUE,                   -- FK -> Products.Id (1-1 relationship)
        QuantityInStock INT NOT NULL DEFAULT 0,          -- Số lượng tồn kho hiện tại
        LastUpdated DATETIME NOT NULL DEFAULT GETDATE(), -- Lần cập nhật gần nhất
        FOREIGN KEY (ProductId) REFERENCES Products(Id)
    );
    PRINT '✓ Bảng Inventory đã được tạo thành công!';
END
ELSE
BEGIN
    PRINT '✓ Bảng Inventory đã tồn tại.';
END
GO

-- ============================================================================
-- BƯỚC 9: TẠO CÁC INDEX ĐỂ TĂNG HIỆU SUẤT TRUY VẤN
-- ============================================================================
PRINT '';
PRINT '========================================';
PRINT 'BƯỚC 9: Tạo các Index';
PRINT '========================================';

-- Index cho Orders.OrderDate (truy vấn theo ngày)
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Orders_OrderDate')
BEGIN
    CREATE INDEX IX_Orders_OrderDate ON Orders(OrderDate);
    PRINT '✓ Index IX_Orders_OrderDate đã được tạo.';
END

-- Index cho Orders.CustomerId (truy vấn đơn hàng theo khách)
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Orders_CustomerId')
BEGIN
    CREATE INDEX IX_Orders_CustomerId ON Orders(CustomerId);
    PRINT '✓ Index IX_Orders_CustomerId đã được tạo.';
END

-- Index cho Orders.UserId (truy vấn đơn hàng theo nhân viên)
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Orders_UserId')
BEGIN
    CREATE INDEX IX_Orders_UserId ON Orders(UserId);
    PRINT '✓ Index IX_Orders_UserId đã được tạo.';
END

-- Index cho Products.ProductCode (tìm kiếm sản phẩm theo mã)
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Products_ProductCode')
BEGIN
    CREATE INDEX IX_Products_ProductCode ON Products(ProductCode);
    PRINT '✓ Index IX_Products_ProductCode đã được tạo.';
END

-- Index cho Users.Username (đăng nhập)
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Users_Username')
BEGIN
    CREATE INDEX IX_Users_Username ON Users(Username);
    PRINT '✓ Index IX_Users_Username đã được tạo.';
END

-- Index cho OrderItems.OrderId (truy vấn chi tiết đơn hàng)
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_OrderItems_OrderId')
BEGIN
    CREATE INDEX IX_OrderItems_OrderId ON OrderItems(OrderId);
    PRINT '✓ Index IX_OrderItems_OrderId đã được tạo.';
END

-- Index cho OrderItems.ProductId (thống kê sản phẩm bán chạy)
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_OrderItems_ProductId')
BEGIN
    CREATE INDEX IX_OrderItems_ProductId ON OrderItems(ProductId);
    PRINT '✓ Index IX_OrderItems_ProductId đã được tạo.';
END

-- Index cho Products.CategoryId (lọc sản phẩm theo danh mục)
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Products_CategoryId')
BEGIN
    CREATE INDEX IX_Products_CategoryId ON Products(CategoryId);
    PRINT '✓ Index IX_Products_CategoryId đã được tạo.';
END

GO

-- ============================================================================
-- BƯỚC 10: CHÈN DỮ LIỆU MẪU
-- ============================================================================
PRINT '';
PRINT '========================================';
PRINT 'BƯỚC 10: Chèn dữ liệu mẫu';
PRINT '========================================';

-- Tạo user Admin mặc định
IF NOT EXISTS (SELECT * FROM Users WHERE Username = 'admin')
BEGIN
    -- Password: admin123 (đã hash bằng BCrypt với workFactor = 10)
    -- BCrypt hash tự động bao gồm salt, mỗi lần hash sẽ khác nhau nhưng verify được
    -- Hash này được tạo bằng: BCrypt.HashPassword("admin123", workFactor: 10)
    -- 
    -- QUAN TRỌNG: Hash này là hash mẫu. Để tạo hash mới:
    -- 1. Sử dụng online tool: https://bcrypt-generator.com/ (password: admin123, rounds: 10)
    -- 2. Hoặc chạy tool C#: Database/GenerateBCryptHash.cs
    -- 3. Copy hash được tạo và thay thế hash dưới đây
    --
    -- Hash mẫu (có thể không hoạt động, cần thay thế bằng hash thật):
    INSERT INTO Users (Username, PasswordHash, FullName, Role, IsActive, CreatedAt)
    VALUES ('admin', '$2a$10$2BO5J.fLXKIfGsdMm.g/pOjrwVwBueeDHbpdK368SArJrelBnnFpm', 'Administrator', 'Admin', 1, GETDATE());
    PRINT '✓ Đã tạo user Admin mặc định.';
    PRINT '  - Username: admin';
    PRINT '  - Password: admin123';
    PRINT '  - Hash: BCrypt (workFactor = 10)';
END
ELSE
BEGIN
    PRINT '✓ User admin đã tồn tại.';
    PRINT '  - Lưu ý: Ứng dụng sẽ tự động migrate password hash sang BCrypt khi đăng nhập.';
END

-- Tạo user Staff mẫu
IF NOT EXISTS (SELECT * FROM Users WHERE Username = 'staff')
BEGIN
    -- Password: admin123 (cùng password với admin để dễ test)
    -- BCrypt hash với workFactor = 10
    -- Hash này được tạo bằng: BCrypt.HashPassword("admin123", workFactor: 10)
    INSERT INTO Users (Username, PasswordHash, FullName, Role, IsActive, CreatedAt)
    VALUES ('staff', '$2a$10$2BO5J.fLXKIfGsdMm.g/pOjrwVwBueeDHbpdK368SArJrelBnnFpm', 'Nhân viên bán hàng', 'Staff', 1, GETDATE());
    PRINT '✓ Đã tạo user Staff mẫu.';
    PRINT '  - Username: staff';
    PRINT '  - Password: admin123';
    PRINT '  - Hash: BCrypt (workFactor = 10)';
END
ELSE
BEGIN
    PRINT '✓ User staff đã tồn tại.';
    PRINT '  - Lưu ý: Ứng dụng sẽ tự động migrate password hash sang BCrypt khi đăng nhập.';
END

-- Tạo một số danh mục mẫu
IF NOT EXISTS (SELECT * FROM Categories WHERE CategoryName = 'Áo')
BEGIN
    INSERT INTO Categories (CategoryName, Description, IsActive)
    VALUES ('Áo', 'Các loại áo thời trang', 1);
    PRINT '✓ Đã tạo danh mục: Áo';
END

IF NOT EXISTS (SELECT * FROM Categories WHERE CategoryName = 'Quần')
BEGIN
    INSERT INTO Categories (CategoryName, Description, IsActive)
    VALUES ('Quần', 'Các loại quần thời trang', 1);
    PRINT '✓ Đã tạo danh mục: Quần';
END

IF NOT EXISTS (SELECT * FROM Categories WHERE CategoryName = 'Phụ kiện')
BEGIN
    INSERT INTO Categories (CategoryName, Description, IsActive)
    VALUES ('Phụ kiện', 'Túi xách, giày dép, mũ nón...', 1);
    PRINT '✓ Đã tạo danh mục: Phụ kiện';
END

GO

-- ============================================================================
-- HOÀN TẤT
-- ============================================================================
PRINT '';
PRINT '========================================';
PRINT 'HOÀN TẤT TẠO DATABASE';
PRINT '========================================';
PRINT '';
PRINT 'Database: WinFormsFashionShopDb';
PRINT 'Các bảng đã tạo:';
PRINT '  ✓ Users (Người dùng)';
PRINT '  ✓ Categories (Danh mục)';
PRINT '  ✓ Products (Sản phẩm)';
PRINT '  ✓ Customers (Khách hàng)';
PRINT '  ✓ Orders (Đơn hàng)';
PRINT '  ✓ OrderItems (Chi tiết đơn hàng)';
PRINT '  ✓ Inventory (Tồn kho)';
PRINT '';
PRINT 'THÔNG TIN ĐĂNG NHẬP MẶC ĐỊNH:';
PRINT '  Username: admin';
PRINT '  Password: admin123';
PRINT '  Role: Admin';
PRINT '';
PRINT 'Bạn có thể bắt đầu sử dụng ứng dụng!';
PRINT '========================================';
GO

