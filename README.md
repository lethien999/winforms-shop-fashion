# WinForms Fashion Shop Management System

## 1. Giới thiệu

### Project là gì?
Hệ thống quản lý bán hàng (POS - Point of Sale) cho shop thời trang được xây dựng bằng **Windows Forms (.NET 8.0)** và **SQL Server**. Hệ thống hỗ trợ quản lý toàn bộ quy trình bán hàng từ quản lý sản phẩm, khách hàng, đơn hàng đến thanh toán trực tuyến qua PayOS (VietQR).

### Giải quyết vấn đề gì?
- **Quản lý sản phẩm**: Lưu trữ thông tin sản phẩm, danh mục, giá bán, hình ảnh
- **Quản lý tồn kho**: Theo dõi số lượng tồn kho, cảnh báo hàng sắp hết
- **Bán hàng**: Lập hóa đơn, tính tiền tự động, in hóa đơn
- **Thanh toán**: Hỗ trợ thanh toán tiền mặt, thẻ, và VietQR qua PayOS
- **Quản lý khách hàng**: Lưu thông tin khách hàng, xem lịch sử mua hàng
- **Báo cáo**: Báo cáo doanh thu, tồn kho, top sản phẩm bán chạy
- **Phân quyền**: Quản lý người dùng với 2 role: Admin và Staff

### Dành cho ai?
- **Chủ shop thời trang**: Quản lý cửa hàng, theo dõi doanh thu
- **Nhân viên bán hàng**: Lập hóa đơn, xử lý thanh toán
- **Quản lý kho**: Điều chỉnh tồn kho, nhập hàng

---

## 2. Công nghệ sử dụng

### Ngôn ngữ & Framework
- **.NET 8.0** - Framework chính
- **C#** - Ngôn ngữ lập trình
- **Windows Forms** - UI Framework cho desktop application

### Database
- **SQL Server 2019+** (hoặc SQL Server Express) - Database chính
- **Entity Framework Core** - ORM (chỉ dùng cho migrations, không dùng cho data access)
- **ADO.NET** - Data access layer (Repository pattern)

### Thư viện chính
- **BCrypt.Net-Next** - Hash mật khẩu
- **Microsoft.Data.SqlClient** - SQL Server client
- **payOS** (v1.0.2) - Tích hợp thanh toán PayOS/VietQR
- **QRCoder** (v1.7.0) - Tạo QR code cho thanh toán
- **Swashbuckle.AspNetCore** (v6.5.0) - Swagger/OpenAPI cho API

### Service bên thứ 3
- **PayOS API** - Dịch vụ thanh toán VietQR
  - Endpoint: `https://api.payos.vn/v2/`
  - Webhook: Nhận thông báo thanh toán thành công

### Kiến trúc
- **3-Layer Architecture**:
  - **DAO (Data Access Layer)**: Repositories, Entities, Database config
  - **BUS (Business Logic Layer)**: Services, Business rules, Validation
  - **GUI (Presentation Layer)**: WinForms UI, Controllers, Helpers
- **API Layer**: ASP.NET Core Web API cho xử lý thanh toán PayOS
- **DTO Layer**: Data Transfer Objects giữa các layer

---

## 3. Cấu trúc thư mục

```
winforms-shop-fashion/
├── API/                          # ASP.NET Core Web API
│   ├── Controllers/
│   │   └── PaymentController.cs  # API endpoints cho PayOS
│   ├── Models/                    # Request/Response models
│   ├── Services/
│   │   ├── PaymentService.cs     # Xử lý logic thanh toán PayOS
│   │   └── IPaymentService.cs
│   ├── Program.cs                 # Entry point của API
│   └── appsettings.json           # Config API (PayOS credentials)
│
├── BUS/                           # Business Logic Layer
│   ├── Composition/
│   │   └── ServicesComposition.cs # Dependency injection setup
│   ├── Constants/
│   │   └── ApplicationConstants.cs # OrderStatus, PaymentMethod, etc.
│   ├── Mappers/                   # Entity <-> DTO mappers
│   └── Services/                  # Business services
│       ├── OrderService.cs         # Logic xử lý đơn hàng
│       ├── ProductService.cs       # Logic quản lý sản phẩm
│       ├── InventoryService.cs     # Logic quản lý tồn kho
│       ├── AuthService.cs          # Xác thực người dùng
│       └── ...
│
├── DAO/                           # Data Access Layer
│   ├── Entities/                  # Database entities
│   │   ├── Order.cs
│   │   ├── Product.cs
│   │   ├── Customer.cs
│   │   └── ...
│   ├── Repositories/              # Repository implementations
│   │   ├── OrderRepository.cs
│   │   ├── ProductRepository.cs
│   │   └── ...
│   ├── ApplicationDbContext.cs    # EF Core DbContext (chỉ dùng migrations)
│   └── DatabaseConfig.cs          # Connection string (KHÔNG commit)
│
├── DTO/                           # Data Transfer Objects
│   ├── OrderDTO.cs
│   ├── ProductDTO.cs
│   └── ...
│
├── GUI/                           # Presentation Layer (WinForms)
│   ├── Forms/                     # WinForms UI
│   │   ├── MainForm.cs            # Form chính
│   │   ├── LoginForm.cs           # Form đăng nhập
│   │   ├── OrderForm.cs           # Form lập hóa đơn
│   │   ├── ProductManagementForm.cs
│   │   ├── QRCodePaymentDialog.cs # Dialog hiển thị QR code
│   │   └── ...
│   ├── Controllers/               # UI Controllers
│   ├── Helpers/                   # Helper classes
│   │   ├── PayOSConfig.cs         # Cấu hình PayOS
│   │   └── ...
│   └── Services/                  # UI Services
│       └── PaymentApiClientWithRetry.cs # Client gọi API
│
├── Database/
│   ├── CreateDatabase.sql         # Script tạo database và tables
│   └── GenerateBCryptHash.cs     # Tool tạo BCrypt hash
│
├── Images/                        # Thư mục lưu hình ảnh
│   ├── Products/                  # Hình sản phẩm
│   ├── Categories/                # Hình danh mục
│   ├── Customers/                 # Hình khách hàng
│   └── ...
│
└── WinFormsFashionShop.sln        # Solution file
```

### Vai trò từng folder quan trọng:

- **API/**: Chạy độc lập như Web API server, xử lý webhook từ PayOS và cung cấp endpoints cho WinForms app gọi
- **GUI/**: Ứng dụng WinForms chính, entry point của hệ thống
- **BUS/**: Chứa toàn bộ business logic, validation, rules
- **DAO/**: Truy cập database, không chứa business logic
- **Database/**: SQL scripts để setup database
- **Images/**: Lưu trữ hình ảnh sản phẩm, khách hàng (không commit lên Git)

---

## 4. Yêu cầu hệ thống

### Hệ điều hành
- **Windows 10/11** (64-bit)
- **Windows Server 2019+** (nếu deploy server)

### Runtime & SDK
- **.NET 8.0 SDK** hoặc cao hơn
  - Download: https://dotnet.microsoft.com/download/dotnet/8.0
  - Verify: `dotnet --version` (phải >= 8.0.0)

### Database
- **SQL Server 2019+** hoặc **SQL Server Express**
  - Download: https://www.microsoft.com/sql-server/sql-server-downloads
  - Hoặc dùng **SQL Server LocalDB** (đi kèm Visual Studio)

### Development Tools (tùy chọn)
- **Visual Studio 2022** (Community/Professional/Enterprise)
  - Download: https://visualstudio.microsoft.com/
  - Workload: ".NET desktop development"
- **Visual Studio Code** với extension "C# Dev Kit"
- **SQL Server Management Studio (SSMS)** - Quản lý database
  - Download: https://docs.microsoft.com/sql/ssms/download-sql-server-management-studio-ssms

### Internet Connection
- Cần kết nối internet để:
  - Tích hợp PayOS API (thanh toán VietQR)
  - Download NuGet packages khi build lần đầu

---

## 5. Hướng dẫn cài đặt

### Bước 1: Clone repository

```bash
git clone <repository-url>
cd winforms-shop-fashion
```

### Bước 2: Cài đặt .NET 8.0 SDK

1. Download và cài đặt .NET 8.0 SDK từ: https://dotnet.microsoft.com/download/dotnet/8.0
2. Verify installation:
```bash
dotnet --version
# Output: 8.0.x hoặc cao hơn
```

### Bước 3: Cài đặt SQL Server

1. Download và cài đặt SQL Server 2019+ hoặc SQL Server Express
2. Ghi nhớ:
   - **Server name**: Thường là `localhost` hoặc `.\SQLEXPRESS` (cho Express)
   - **Authentication mode**: Windows Authentication hoặc SQL Authentication
   - Nếu dùng SQL Authentication, ghi nhớ **username** và **password**

### Bước 4: Tạo Database

1. Mở **SQL Server Management Studio (SSMS)** hoặc dùng `sqlcmd`
2. Kết nối với SQL Server
3. Mở file `Database/CreateDatabase.sql`
4. Chạy toàn bộ script (F5 hoặc Execute)
5. Verify: Database `WinFormsFashionShopDb` đã được tạo với các bảng:
   - Users, Categories, Products, Customers, Orders, OrderItems, Inventory

**Thông tin đăng nhập mặc định sau khi chạy script:**
- Username: `admin`
- Password: `admin123`
- Role: `Admin`

### Bước 5: Cấu hình Database Connection

1. Copy file `DAO/DatabaseConfig.example.cs` thành `DAO/DatabaseConfig.cs`:
```bash
# Windows PowerShell
Copy-Item DAO/DatabaseConfig.example.cs DAO/DatabaseConfig.cs
```

2. Mở `DAO/DatabaseConfig.cs` và cập nhật connection string:

```csharp
public static string ConnectionString { get; set; } = 
    "Data Source=localhost;Initial Catalog=WinFormsFashionShopDb;User ID=sa;Password=YourPassword;Trust Server Certificate=True";
```

**Lưu ý quan trọng:**
- ⚠️ **KHÔNG commit** file `DatabaseConfig.cs` lên Git (đã có trong `.gitignore`)
- Nếu dùng Windows Authentication, connection string:
```csharp
"Data Source=localhost;Initial Catalog=WinFormsFashionShopDb;Integrated Security=True;Trust Server Certificate=True"
```

### Bước 6: Cấu hình PayOS (Tùy chọn - chỉ cần nếu dùng thanh toán VietQR)

1. Đăng ký tài khoản PayOS tại: https://pay.payos.vn/
2. Lấy thông tin:
   - **Client ID**
   - **API Key**
   - **Checksum Key**

3. **Cấu hình cho WinForms app:**
   - Chạy WinForms app lần đầu
   - Vào menu: **Quản trị** → **Cấu hình PayOS**
   - Nhập Client ID, API Key, Checksum Key
   - Config được lưu tại: `GUI/bin/Debug/net8.0-windows/Config/payos.config.json`

4. **Cấu hình cho API:**
   - Copy `API/appsettings.example.json` thành `API/appsettings.json`
   - Mở `API/appsettings.json` và cập nhật:
```json
{
  "PayOS": {
    "ClientId": "your-client-id",
    "ApiKey": "your-api-key",
    "ChecksumKey": "your-checksum-key"
  }
}
```

### Bước 7: Restore NuGet Packages

```bash
dotnet restore
```

Hoặc trong Visual Studio: Right-click Solution → **Restore NuGet Packages**

### Bước 8: Build Project

```bash
dotnet build
```

Verify: Không có lỗi build, output: `Build succeeded.`

---

## 6. Hướng dẫn chạy project

### Cách 1: Chạy từ Command Line

#### Chạy WinForms Application (Ứng dụng chính):

```bash
cd GUI
dotnet run
```

Hoặc từ root:
```bash
dotnet run --project GUI/GUI.csproj
```

#### Chạy API Server (Cần thiết cho thanh toán PayOS):

Mở terminal mới:
```bash
cd API
dotnet run
```

API sẽ chạy tại: `https://localhost:5001` hoặc `http://localhost:5000`

**Lưu ý:** 
- API phải chạy trước khi WinForms app sử dụng tính năng thanh toán VietQR
- Nếu không chạy API, thanh toán VietQR sẽ không hoạt động

### Cách 2: Chạy từ Visual Studio

1. Mở `WinFormsFashionShop.sln` trong Visual Studio 2022
2. Set **Multiple Startup Projects**:
   - Right-click Solution → **Properties** → **Startup Project**
   - Chọn **Multiple startup projects**
   - Set **GUI** và **API** đều là **Start**
3. Nhấn **F5** hoặc click **Start**

### Kiểm tra project đã chạy thành công

1. **WinForms App:**
   - Cửa sổ đăng nhập xuất hiện
   - Đăng nhập với: `admin` / `admin123`
   - Form chính (MainForm) hiển thị dashboard

2. **API Server:**
   - Terminal hiển thị: `Now listening on: https://localhost:5001`
   - Mở browser: `https://localhost:5001/swagger` → Thấy Swagger UI

---

## 7. Hướng dẫn sử dụng

### 7.1. Đăng nhập

1. Mở ứng dụng → Form đăng nhập xuất hiện
2. Nhập:
   - **Username**: `admin` (hoặc username khác)
   - **Password**: `admin123` (hoặc password đã đổi)
3. Click **Đăng nhập**
4. Nếu đúng → Vào form chính
5. Nếu sai → Hiển thị thông báo lỗi

**⚠️ Lưu ý:** Đổi mật khẩu ngay sau lần đăng nhập đầu tiên!

### 7.2. Dashboard

Sau khi đăng nhập, bạn thấy:
- **Doanh thu tháng này**: Tổng doanh thu tháng hiện tại
- **Thao tác nhanh**: Nút "Lập hóa đơn"
- **Đơn hàng gần đây**: Danh sách các đơn hàng mới nhất
- **Menu bar**: Các chức năng chính

### 7.3. Quản lý Sản phẩm

**Vào:** Menu **Quản lý** → **Sản phẩm**

**Chức năng:**
- **Thêm sản phẩm**: Click nút "Thêm mới" → Nhập thông tin (Mã SP, Tên, Danh mục, Giá, Đơn vị, Hình ảnh)
- **Sửa sản phẩm**: Chọn sản phẩm → Click "Sửa" → Cập nhật thông tin
- **Xóa sản phẩm**: Chọn sản phẩm → Click "Xóa" → Xác nhận
- **Tìm kiếm**: Nhập từ khóa vào ô tìm kiếm → Enter

**Input:**
- Mã sản phẩm: Tự động generate hoặc nhập thủ công (phải unique)
- Tên sản phẩm: Bắt buộc
- Danh mục: Chọn từ dropdown
- Giá bán: Số >= 0
- Đơn vị: "cái", "bộ", "chiếc", etc.
- Hình ảnh: Click "Chọn ảnh" → Chọn file ảnh

**Output:**
- Danh sách sản phẩm hiển thị trong DataGridView
- Thông báo thành công/lỗi sau mỗi thao tác

### 7.4. Quản lý Danh mục

**Vào:** Menu **Quản lý** → **Danh mục**

**Chức năng:**
- Thêm, sửa, xóa danh mục
- Mỗi danh mục có: Tên, Mô tả, Hình ảnh

### 7.5. Quản lý Khách hàng

**Vào:** Menu **Quản lý** → **Khách hàng**

**Chức năng:**
- Thêm, sửa, xóa khách hàng
- Thông tin: Tên, SĐT, Email, Địa chỉ
- Xem lịch sử mua hàng của khách hàng

### 7.6. Lập Hóa đơn (Bán hàng)

**Vào:** Menu **Bán hàng** → **Lập hóa đơn** hoặc click nút "Lập hóa đơn" trên dashboard

**Quy trình:**

1. **Chọn khách hàng** (hoặc để trống = "Khách lẻ")
   - Click "Chọn khách hàng" → Chọn từ danh sách hoặc "Thêm mới"

2. **Thêm sản phẩm vào đơn:**
   - Click "Thêm sản phẩm" → Chọn sản phẩm → Nhập số lượng
   - Sản phẩm hiển thị trong bảng với: Mã SP, Tên, SL, Đơn giá, Thành tiền
   - Có thể xóa sản phẩm khỏi đơn

3. **Chọn phương thức thanh toán:**
   - **Tiền mặt**: Thanh toán trực tiếp
   - **Thẻ**: Thanh toán bằng thẻ
   - **VietQR (PayOS)**: Thanh toán qua QR code (cần API chạy)
   - **Chuyển khoản**: Ghi chú thông tin chuyển khoản
   - **Khác**: Phương thức khác

4. **Xác nhận thanh toán:**
   - Xem tổng tiền
   - Click "Xác nhận thanh toán"
   - **Nếu Tiền mặt/Thẻ**: → Dialog xác nhận → Click "Xác nhận" → Đơn được tạo với Status = "Paid" → Tự động trừ tồn kho
   - **Nếu VietQR**: → Dialog hiển thị QR code → Quét QR và thanh toán → Hệ thống tự động cập nhật khi nhận webhook từ PayOS → Tự động trừ tồn kho khi Status = "Paid"

5. **In hóa đơn** (sau khi thanh toán thành công):
   - Click "In hóa đơn" → Dialog xem trước → Click "In"

**Lưu ý:**
- Tồn kho chỉ được trừ khi đơn có Status = "Paid"
- Đơn VietQR ban đầu có Status = "Pending" → Chờ webhook từ PayOS → Status = "Paid" → Mới trừ tồn kho

### 7.7. Quản lý Đơn hàng

**Vào:** Menu **Quản lý** → **Đơn hàng**

**Chức năng:**
- Xem danh sách tất cả đơn hàng
- **Xem chi tiết**: Chọn đơn → Click "Xem chi tiết"
- **In hóa đơn**: Chọn đơn đã thanh toán → Click "In hóa đơn"
- **Hủy đơn**: Chọn đơn Pending → Click "Hủy đơn" → Xác nhận → Tự động hoàn trả tồn kho nếu đơn đã Paid
- **Thanh toán VietQR** (cho đơn Pending): Click "Thanh toán VietQR" → Hiển thị QR code
- **Kiểm tra thanh toán**: Click "Kiểm tra thanh toán" → Gọi API recheck status từ PayOS

**Trạng thái đơn hàng:**
- **Pending**: Đơn chưa thanh toán (VietQR)
- **Processing**: Đang xử lý thanh toán (webhook đã đến)
- **Paid**: Đã thanh toán thành công
- **Failed**: Thanh toán thất bại
- **Cancelled**: Đã hủy

### 7.8. Quản lý Tồn kho

**Vào:** Menu **Quản lý** → **Tồn kho**

**Chức năng:**
- Xem danh sách sản phẩm và số lượng tồn kho
- **Điều chỉnh tồn kho**: Chọn sản phẩm → Nhập số lượng mới → Click "Cập nhật"
- **Cảnh báo**: Sản phẩm có tồn kho < 10 được highlight màu đỏ

### 7.9. Báo cáo

**Vào:** Menu **Báo cáo**

**Chức năng:**
- **Báo cáo doanh thu**: Chọn khoảng thời gian → Xem doanh thu theo ngày/tháng
- **Báo cáo tồn kho**: Xem tồn kho hiện tại, sản phẩm sắp hết
- **Top sản phẩm bán chạy**: Sản phẩm bán nhiều nhất
- **Top khách hàng**: Khách hàng mua nhiều nhất

### 7.10. Quản lý Người dùng (Chỉ Admin)

**Vào:** Menu **Quản trị** → **Người dùng**

**Chức năng:**
- Thêm, sửa, xóa người dùng
- Phân quyền: Admin hoặc Staff
- Đổi mật khẩu người dùng

**Phân quyền:**
- **Admin**: Toàn quyền (quản lý người dùng, hủy đơn hàng, v.v.)
- **Staff**: Chỉ bán hàng, quản lý sản phẩm, không được hủy đơn hàng

### 7.11. Cấu hình PayOS (Chỉ Admin)

**Vào:** Menu **Quản trị** → **Cấu hình PayOS**

**Chức năng:**
- Nhập Client ID, API Key, Checksum Key từ PayOS dashboard
- Lưu cấu hình → Sử dụng cho thanh toán VietQR

---

## 8. Luồng nghiệp vụ chính

### 8.1. Luồng Bán hàng (Tiền mặt/Thẻ)

```
1. Nhân viên mở "Lập hóa đơn"
2. Chọn khách hàng (hoặc để trống)
3. Thêm sản phẩm vào đơn → Nhập số lượng
4. Chọn phương thức thanh toán: "Tiền mặt" hoặc "Thẻ"
5. Click "Xác nhận thanh toán"
6. Dialog xác nhận → Click "Xác nhận"
7. Hệ thống:
   - Tạo Order với Status = "Paid"
   - Tạo OrderItems
   - Tự động TRỪ tồn kho (vì Status = "Paid")
   - Tính tổng tiền
8. Hiển thị thông báo "Thanh toán thành công"
9. Có thể in hóa đơn ngay
```

### 8.2. Luồng Bán hàng (VietQR - PayOS)

```
1. Nhân viên mở "Lập hóa đơn"
2. Chọn khách hàng, thêm sản phẩm
3. Chọn phương thức thanh toán: "VietQR (PayOS)"
4. Click "Xác nhận thanh toán"
5. Hệ thống:
   - Tạo Order với Status = "Pending" (CHƯA trừ tồn kho)
   - Gọi API: POST /api/payment/create
   - API tạo payment link từ PayOS → Trả về QR code
6. Dialog hiển thị QR code
7. Khách hàng quét QR và thanh toán trên app ngân hàng
8. PayOS gửi webhook đến: POST /api/payment/webhook
9. API xử lý webhook:
   - Verify signature (HMAC SHA256)
   - Gọi stored procedure ProcessPayOSWebhook
   - Update Order: Status = "Paid", PaidAt = now
   - Gọi OrderService.DecreaseInventoryForPaidOrder() → TRỪ tồn kho
10. WinForms app có thể:
    - Tự động refresh (nếu đang mở OrderDetailDialog)
    - Hoặc click "Kiểm tra thanh toán" → Gọi GET /api/payment/recheck/{orderId}
11. Khi Status = "Paid" → Có thể in hóa đơn
```

**Lưu ý quan trọng:**
- **1 Invoice = 1 PayOSOrderCode duy nhất**: Nếu đơn đã có PayOSOrderCode, hệ thống KHÔNG tạo payment link mới, chỉ check status
- Tồn kho chỉ được trừ khi Status = "Paid" (đảm bảo an toàn)

### 8.3. Luồng Hủy đơn hàng

```
1. Admin mở "Quản lý đơn hàng"
2. Chọn đơn có Status = "Pending" hoặc "Paid"
3. Click "Hủy đơn" → Xác nhận
4. Hệ thống:
   - Update Order: Status = "Cancelled"
   - Nếu đơn đã Paid → Gọi InventoryService.IncreaseStock() → HOÀN TRẢ tồn kho
   - Nếu đơn Pending → Không cần hoàn trả (chưa trừ tồn kho)
5. Thông báo "Đã hủy đơn hàng thành công"
```

### 8.4. Luồng Nhập hàng (Điều chỉnh tồn kho)

```
1. Quản lý kho mở "Quản lý tồn kho"
2. Chọn sản phẩm cần nhập
3. Nhập số lượng mới (hoặc số lượng tăng)
4. Click "Cập nhật"
5. Hệ thống:
   - Update Inventory.QuantityInStock
   - Update Inventory.LastUpdated = now
6. Thông báo "Cập nhật tồn kho thành công"
```

---

## 9. Lỗi thường gặp & Cách khắc phục

### 9.1. Lỗi Config

#### Lỗi: "Cannot connect to database"
**Nguyên nhân:** Connection string sai hoặc SQL Server chưa chạy

**Cách khắc phục:**
1. Kiểm tra SQL Server đang chạy:
   - Mở **Services** (services.msc) → Tìm "SQL Server (MSSQLSERVER)" → Phải là "Running"
2. Kiểm tra connection string trong `DAO/DatabaseConfig.cs`:
   - Server name đúng chưa? (localhost, .\SQLEXPRESS, etc.)
   - Database name đúng chưa? (WinFormsFashionShopDb)
   - Username/Password đúng chưa?
3. Test connection bằng SSMS trước

#### Lỗi: "PayOS chưa được cấu hình"
**Nguyên nhân:** Chưa cấu hình PayOS credentials

**Cách khắc phục:**
1. Vào menu **Quản trị** → **Cấu hình PayOS**
2. Nhập đầy đủ: Client ID, API Key, Checksum Key
3. Verify: File `GUI/bin/Debug/net8.0-windows/Config/payos.config.json` đã được tạo

### 9.2. Lỗi Runtime

#### Lỗi: "Insufficient stock for product"
**Nguyên nhân:** Số lượng tồn kho không đủ

**Cách khắc phục:**
1. Kiểm tra tồn kho: Menu **Quản lý** → **Tồn kho**
2. Nếu thiếu → Nhập thêm hàng
3. Nếu đủ nhưng vẫn báo lỗi → Kiểm tra database: `SELECT * FROM Inventory WHERE ProductId = X`

#### Lỗi: "Order not found" khi thanh toán VietQR
**Nguyên nhân:** OrderId không tồn tại hoặc PayOSOrderCode bị NULL

**Cách khắc phục:**
1. Kiểm tra database: `SELECT * FROM Orders WHERE Id = X`
2. Nếu PayOSOrderCode = NULL → Có thể sync lại:
   - Gọi API: `POST /api/payment/sync-payos-codes?orderId=X`
   - Hoặc update thủ công qua stored procedure

#### Lỗi: "Webhook signature verification failed"
**Nguyên nhân:** ChecksumKey sai hoặc webhook bị tamper

**Cách khắc phục:**
1. Kiểm tra ChecksumKey trong `API/appsettings.json` đúng chưa
2. Verify: ChecksumKey phải khớp với PayOS dashboard
3. Nếu vẫn lỗi → Kiểm tra log trong Debug Output của API

### 9.3. Lỗi Tích hợp API

#### Lỗi: "Cannot connect to API" khi thanh toán VietQR
**Nguyên nhân:** API server chưa chạy hoặc URL sai

**Cách khắc phục:**
1. Kiểm tra API đang chạy:
   - Mở browser: `https://localhost:5001/swagger` → Phải thấy Swagger UI
2. Kiểm tra URL trong `GUI/Services/PaymentApiClientWithRetry.cs`:
   - BaseUrl phải là: `https://localhost:5001` (hoặc port API đang chạy)
3. Restart API server nếu cần

#### Lỗi: "PayOS API timeout" hoặc "Cannot resolve api.payos.vn"
**Nguyên nhân:** Mạng internet, firewall, hoặc DNS

**Cách khắc phục:**
1. Kiểm tra kết nối internet
2. Kiểm tra firewall không chặn `api.payos.vn`
3. Flush DNS: `ipconfig /flushdns` (Windows)
4. Nếu đã thanh toán trên PayOS web nhưng hệ thống chưa cập nhật:
   - Click "Kiểm tra thanh toán" trong OrderDetailDialog
   - Hoặc gọi API: `GET /api/payment/recheck/{orderId}`

#### Lỗi: "CORS policy" khi WinForms gọi API
**Nguyên nhân:** API chưa cấu hình CORS

**Cách khắc phục:**
1. Kiểm tra `API/Program.cs` có:
```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowWinForms", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});
```
2. Verify: `app.UseCors("AllowWinForms");` được gọi trước `app.MapControllers()`

### 9.4. Lỗi Database

#### Lỗi: "Invalid column name" hoặc "Table doesn't exist"
**Nguyên nhân:** Database schema chưa được tạo hoặc không khớp với code

**Cách khắc phục:**
1. Chạy lại script `Database/CreateDatabase.sql`
2. Verify các bảng đã được tạo:
```sql
USE WinFormsFashionShopDb;
SELECT * FROM INFORMATION_SCHEMA.TABLES;
```
3. Kiểm tra schema khớp với code (xem `DAO/ApplicationDbContext.cs`)

#### Lỗi: "Violation of UNIQUE KEY constraint" cho PayOSOrderCode
**Nguyên nhân:** Cố gắng tạo payment link mới cho đơn đã có PayOSOrderCode

**Cách khắc phục:**
- Đây là lỗi logic, không nên xảy ra. Hệ thống đã có check:
  - `CreatePaymentLinkAsync` kiểm tra nếu đã có PayOSOrderCode → Không tạo mới, chỉ check status
- Nếu vẫn xảy ra → Kiểm tra code `API/Services/PaymentService.cs`

---

## 10. Ghi chú & Lưu ý

### 10.1. Bảo mật

- ⚠️ **KHÔNG commit** file `DAO/DatabaseConfig.cs` lên Git (đã có trong `.gitignore`)
- ⚠️ **KHÔNG commit** file `API/appsettings.json` với PayOS credentials thật
- ⚠️ Mật khẩu được hash bằng BCrypt (workFactor = 10)
- ⚠️ Đổi mật khẩu mặc định ngay sau lần đăng nhập đầu tiên

### 10.2. Database

- **1 Invoice = 1 PayOSOrderCode duy nhất**: Đảm bảo không tạo payment link mới cho đơn đã có PayOSOrderCode
- **Tồn kho chỉ trừ khi Status = "Paid"**: Đảm bảo an toàn, tránh trừ tồn kho cho đơn chưa thanh toán
- **Unique index** trên `PayOSOrderCode` với filter `IS NOT NULL`: Cho phép nhiều NULL nhưng chỉ 1 giá trị không NULL

### 10.3. PayOS Integration

- **Webhook phải trả về HTTP 200 trong < 5 giây**: API xử lý webhook trong background để đảm bảo response nhanh
- **Signature verification**: Tất cả webhook đều được verify bằng HMAC SHA256
- **Idempotency**: Stored procedure `ProcessPayOSWebhook` đảm bảo không xử lý webhook trùng lặp

### 10.4. Performance

- **API chạy độc lập**: Có thể deploy API lên server riêng, WinForms app gọi qua HTTPS
- **Logs**: Lỗi được log vào `GUI/Logs/error-YYYYMMDD.log`
- **Retry logic**: `PaymentApiClientWithRetry` tự động retry khi gọi API thất bại

### 10.5. Deployment

- **WinForms app**: Build → Copy folder `GUI/bin/Release/net8.0-windows/` → Deploy lên máy client
- **API**: Build → Deploy lên IIS hoặc chạy như Windows Service
- **Database**: Backup thường xuyên, đặc biệt trước khi update schema

### 10.6. Maintenance

- **Backup database** định kỳ (hàng ngày/tuần)
- **Kiểm tra logs** thường xuyên: `GUI/Logs/`
- **Update NuGet packages** định kỳ: `dotnet list package --outdated`
- **Monitor PayOS webhook**: Kiểm tra `PaymentWebhooks` table trong database

### 10.7. Troubleshooting Tips

- **Nếu đơn VietQR bị "Pending" mãi**: 
  1. Kiểm tra API có nhận được webhook không (xem logs)
  2. Kiểm tra PayOS dashboard: Đơn đã thanh toán chưa?
  3. Click "Kiểm tra thanh toán" để force recheck từ PayOS API
  4. Nếu vẫn không được → Có thể update thủ công: `POST /api/payment/force-update-paid/{orderId}`

- **Nếu tồn kho không khớp**:
  1. Kiểm tra tất cả đơn hàng có Status = "Paid" đã trừ tồn kho chưa
  2. Kiểm tra có đơn nào bị hủy nhưng chưa hoàn trả tồn kho không
  3. Điều chỉnh thủ công qua "Quản lý tồn kho"

---

## 11. API Endpoints

### Payment API (Chạy tại: `https://localhost:5001`)

#### POST `/api/payment/create`
Tạo payment link từ PayOS cho đơn hàng mới.

**Request:**
```json
{
  "orderId": 123,
  "amount": 100000,
  "description": "DH ORD202412220001"
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "paymentLink": "https://pay.payos.vn/web/...",
    "qrCode": "data:image/png;base64,...",
    "payOSOrderCode": 6169
  }
}
```

#### GET `/api/payment/status/{orderId}`
Lấy trạng thái thanh toán từ database.

#### GET `/api/payment/recheck/{orderId}`
Recheck trạng thái từ PayOS API (không tạo payment link mới).

#### POST `/api/payment/webhook`
Nhận webhook từ PayOS khi thanh toán thành công. (Internal use)

#### POST `/api/payment/force-update-paid/{orderId}`
Force update order status = "Paid" (dùng khi PayOS API không kết nối được).

#### GET `/api/payment/debug/{payOSOrderCode}`
Debug endpoint để kiểm tra trạng thái PayOS trực tiếp.

**Swagger UI:** `https://localhost:5001/swagger`

---

## 12. Database Schema

### Các bảng chính:

- **Users**: Người dùng hệ thống (Admin, Staff)
- **Categories**: Danh mục sản phẩm
- **Products**: Sản phẩm
- **Customers**: Khách hàng
- **Orders**: Đơn hàng/Hóa đơn
- **OrderItems**: Chi tiết đơn hàng
- **Inventory**: Tồn kho

### Stored Procedures:

- **ProcessPayOSWebhook**: Xử lý webhook từ PayOS (atomic transaction, idempotency)

Xem chi tiết schema trong `Database/CreateDatabase.sql`

---

## 13. CI/CD và Đóng gói Ứng dụng

### 13.1. CI/CD Pipeline

Project đã được thiết lập **GitHub Actions** cho CI/CD:

#### Continuous Integration (CI)
- **Workflow:** `.github/workflows/ci.yml`
- **Trigger:** Tự động chạy khi push code hoặc tạo Pull Request
- **Chức năng:**
  - Build solution với .NET 8.0
  - Chạy tests (nếu có)
  - Upload build artifacts

#### Continuous Deployment (CD)
- **Workflow:** `.github/workflows/cd-publish.yml`
- **Trigger:** 
  - Khi tạo tag version (ví dụ: `v1.0.0`)
  - Hoặc trigger thủ công từ GitHub Actions tab
- **Chức năng:**
  - Publish WinForms app (self-contained)
  - Tạo release package (ZIP)
  - Tạo GitHub Release với download link

### 13.2. Tạo Release

#### Cách 1: Tạo Release từ Tag

```bash
# 1. Tạo tag version
git tag -a v1.0.0 -m "Release version 1.0.0"

# 2. Push tag lên GitHub
git push origin v1.0.0
```

GitHub Actions sẽ tự động build và tạo release package.

#### Cách 2: Trigger thủ công

1. Vào GitHub repository → **Actions** tab
2. Chọn workflow **CD - Publish WinForms App**
3. Click **Run workflow**
4. Nhập version number → **Run workflow**

### 13.3. Đóng gói Ứng dụng

Xem chi tiết trong file **[PUBLISH_GUIDE.md](./PUBLISH_GUIDE.md)** để biết:

- Các cách publish WinForms app (self-contained, framework-dependent, single file)
- Tạo installer (MSI với WiX, EXE với Inno Setup)
- Tối ưu hóa package size
- Hướng dẫn deploy cho end user
- Troubleshooting

#### Publish nhanh từ Command Line:

```powershell
# Publish self-contained (khuyến nghị)
dotnet publish GUI/GUI.csproj `
  --configuration Release `
  --output ./publish/GUI `
  --self-contained true `
  --runtime win-x64

# Publish single file
dotnet publish GUI/GUI.csproj `
  --configuration Release `
  --output ./publish/GUI-SingleFile `
  --self-contained true `
  --runtime win-x64 `
  -p:PublishSingleFile=true
```

---

## 14. License

[Thêm license của bạn ở đây]

---

## 15. Support & Contact

Nếu gặp vấn đề, vui lòng:
1. Kiểm tra phần "Lỗi thường gặp" ở trên
2. Xem logs trong `GUI/Logs/`
3. Kiểm tra Debug Output trong Visual Studio
4. Xem [PUBLISH_GUIDE.md](./PUBLISH_GUIDE.md) cho hướng dẫn đóng gói
5. Tạo issue trên repository (nếu có)

---

**Tài liệu được cập nhật lần cuối:** 2024-12-22
