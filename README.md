# WinForms Fashion Shop Management System

Hệ thống quản lý bán hàng cho shop thời trang sử dụng WinForms và SQL Server.

## 🚀 Yêu cầu hệ thống

- .NET 8.0 SDK hoặc cao hơn
- SQL Server 2019 hoặc cao hơn (hoặc SQL Server Express)
- Visual Studio 2022 hoặc Visual Studio Code

## 📋 Cài đặt

### 1. Clone repository

```bash
git clone <repository-url>
cd winforms-shop-fashion
```

### 2. Cấu hình Database

1. Mở SQL Server Management Studio (SSMS) hoặc sử dụng `sqlcmd`
2. Chạy script tạo database: `Database/CreateDatabase.sql`
3. Cập nhật connection string trong `WinFormsFashionShop.Data/DatabaseConfig.cs`:

```csharp
public static string ConnectionString { get; set; } = 
    "Data Source=YOUR_SERVER;Initial Catalog=WinFormsFashionShopDb;User ID=YOUR_USER;Password=YOUR_PASSWORD;Trust Server Certificate=True";
```

**⚠️ QUAN TRỌNG:** Không commit file `DatabaseConfig.cs` với thông tin đăng nhập thật lên GitHub!

### 3. Restore NuGet packages

```bash
dotnet restore
```

### 4. Build project

```bash
dotnet build
```

### 5. Chạy ứng dụng

```bash
dotnet run --project WinFormsFashionShop.Presentation
```

Hoặc mở solution trong Visual Studio và nhấn F5.

## 🔐 Thông tin đăng nhập mặc định

Sau khi chạy script tạo database, bạn có thể đăng nhập với:

- **Username:** `admin`
- **Password:** `admin123`
- **Role:** Admin

**⚠️ Lưu ý:** Đổi mật khẩu ngay sau lần đăng nhập đầu tiên!

## 📁 Cấu trúc project

```
WinFormsFashionShop/
├── WinFormsFashionShop.Data/          # Data Access Layer (DAL)
│   ├── Entities/                      # Entity classes
│   ├── Repositories/                  # Repository implementations
│   └── DatabaseConfig.cs              # Database connection configuration
├── WinFormsFashionShop.Business/      # Business Logic Layer (BLL)
│   ├── Services/                      # Business services
│   └── Composition/                   # Dependency injection setup
├── WinFormsFashionShop.Presentation/  # Presentation Layer (UI)
│   └── Forms/                         # WinForms
└── Database/                          # SQL scripts
    └── CreateDatabase.sql             # Database creation script
```

## 🎯 Chức năng chính

### Quản lý sản phẩm
- Thêm, sửa, xóa sản phẩm
- Phân loại theo danh mục
- Quản lý giá bán và đơn vị tính

### Quản lý khách hàng
- Lưu thông tin khách hàng
- Xem lịch sử mua hàng

### Bán hàng
- Lập hóa đơn bán hàng
- Tự động trừ tồn kho
- Tính tổng tiền và giảm giá

### Quản lý tồn kho
- Nhập hàng tăng tồn kho
- Kiểm tra số lượng tồn kho
- Cảnh báo hàng sắp hết

### Báo cáo
- Báo cáo doanh thu theo ngày/tháng
- Báo cáo tồn kho
- Top khách hàng

## 🔒 Bảo mật

- Mật khẩu được hash bằng BCrypt
- Phân quyền theo role (Admin, Staff)
- Connection string không được commit lên Git

## 🛠️ Công nghệ sử dụng

- **.NET 8.0** - Framework
- **WinForms** - UI Framework
- **SQL Server** - Database
- **BCrypt.Net-Next** - Password hashing
- **Microsoft.Data.SqlClient** - SQL Server client

## 📝 Ghi chú

- File `.gitignore` đã được cấu hình để bỏ qua các file build output và user-specific files
- Không commit file `DatabaseConfig.cs` với thông tin đăng nhập thật
- Sử dụng `DatabaseConfig.example.cs` làm template

## 📄 License

[Thêm license của bạn ở đây]

