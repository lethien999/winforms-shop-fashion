# Hướng dẫn Đóng gói và Phân phối WinForms Application

## 1. Tổng quan về CI/CD

Project đã được thiết lập CI/CD với GitHub Actions theo best practices:

### CI (Continuous Integration)
- **File:** `.github/workflows/ci.yml`
- **Trigger:** 
  - Tự động khi push code hoặc tạo Pull Request
  - Có thể trigger thủ công
- **Chức năng:**
  - Build solution với caching
  - Code quality checks (TODO/FIXME detection)
  - Run tests (có thể bật khi có test project)
  - Upload build artifacts

### CD (Continuous Deployment)
- **File:** `.github/workflows/cd-publish.yml`
- **Trigger:** 
  - Tự động khi tạo tag version (ví dụ: `v1.0.0`)
  - Trigger thủ công với inputs (version, create_release)
- **Chức năng:**
  - Publish WinForms app (2 versions: full và single file)
  - Publish API
  - Tạo release packages (ZIP)
  - Tạo GitHub Release với release notes tự động

**Xem chi tiết:** [`.github/workflows/README.md`](.github/workflows/README.md)

---

## 2. Các cách đóng gói WinForms App

### 2.1. ClickOnce Deployment (Khuyến nghị cho Production)

**Ưu điểm:**
- ✅ Tự động update khi có phiên bản mới
- ✅ Dễ cài đặt (chỉ cần click)
- ✅ Không cần quyền admin
- ✅ Tích hợp với Windows
- ✅ Rollback về phiên bản cũ nếu cần

**Cách sử dụng:**

#### Option A: Publish Local (File Share hoặc USB)

```powershell
# Chạy script publish ClickOnce
.\Scripts\Publish-ClickOnce-Local.ps1 -Version "1.0.0.0"
```

**Kết quả:**
- Files được publish vào `publish/ClickOnce-Local/`
- Có file `setup.exe` và `GUI.application`
- Copy folder này lên file share hoặc USB
- Users chạy `setup.exe` để cài đặt

#### Option B: Publish Web (Cần web server)

```powershell
# Publish với URL web server
.\Scripts\Publish-ClickOnce.ps1 -Version "1.0.0.0" -PublishUrl "https://your-server.com/clickonce/"
```

**Yêu cầu:**
- Web server (IIS, Apache, Nginx)
- Cấu hình MIME types:
  - `.application` → `application/x-ms-application`
  - `.manifest` → `application/manifest`
  - `.deploy` → `application/octet-stream`

**Cài đặt:**
- Users truy cập URL và click vào `GUI.application`
- App sẽ tự động cài đặt và update

#### Option C: Publish từ Visual Studio

1. Right-click vào project `GUI`
2. Chọn **Publish**
3. Chọn **ClickOnce**
4. Cấu hình:
   - **Where to publish:** Chọn folder hoặc URL
   - **Install mode:** Online/Offline
   - **Update settings:** Cấu hình auto-update
5. Click **Publish**

**Lưu ý:**
- ClickOnce yêu cầu MSBuild (có trong Visual Studio)
- Nếu không có Visual Studio, dùng script PowerShell
- Version sẽ tự động increment mỗi lần publish

---

### 2.2. Publish Self-Contained (Khuyến nghị cho Standalone)

**Ưu điểm:**
- Không cần cài .NET Runtime trên máy client
- Tất cả dependencies đã được bundle
- Dễ deploy

**Cách làm:**

#### Option A: Publish từ Command Line

```powershell
# Publish với tất cả files (không single file)
dotnet publish GUI/GUI.csproj `
  --configuration Release `
  --output ./publish/GUI `
  --self-contained true `
  --runtime win-x64 `
  -p:PublishSingleFile=false

# Publish single file (tất cả trong 1 file .exe)
dotnet publish GUI/GUI.csproj `
  --configuration Release `
  --output ./publish/GUI-SingleFile `
  --self-contained true `
  --runtime win-x64 `
  -p:PublishSingleFile=true `
  -p:IncludeNativeLibrariesForSelfExtract=true `
  -p:EnableCompressionInSingleFile=true
```

#### Option B: Publish từ Visual Studio

1. Right-click project **GUI** → **Publish**
2. Chọn **Folder** → **Next**
3. Chọn **Folder location** → **Finish**
4. Click **Show all settings**
5. Cấu hình:
   - **Deployment mode:** Self-contained
   - **Target runtime:** win-x64
   - **Publish single file:** (tùy chọn)
6. Click **Publish**

**Output:**
- Folder `publish/GUI/` chứa tất cả files cần thiết
- Copy toàn bộ folder này lên máy client
- Chạy `GUI.exe` để khởi động ứng dụng

---

### 2.2. Publish Framework-Dependent

**Ưu điểm:**
- Kích thước nhỏ hơn
- Cần .NET 8.0 Runtime đã cài trên máy client

**Cách làm:**

```powershell
dotnet publish GUI/GUI.csproj `
  --configuration Release `
  --output ./publish/GUI `
  --self-contained false `
  --runtime win-x64
```

---

### 2.3. Tạo Installer (MSI/EXE)

#### Sử dụng WiX Toolset (MSI Installer)

1. **Cài đặt WiX Toolset:**
   - Download: https://wixtoolset.org/
   - Hoặc: `winget install WiXToolset.WiXToolset`

2. **Tạo WiX project:**

```xml
<!-- Installer.wxs -->
<?xml version="1.0" encoding="UTF-8"?>
<Wix xmlns="http://schemas.microsoft.com/wix/2006/wi">
  <Product Id="*" Name="WinForms Fashion Shop" Language="1033" Version="1.0.0" 
           Manufacturer="Your Company" UpgradeCode="YOUR-GUID-HERE">
    <Package InstallerVersion="200" Compressed="yes" InstallScope="perMachine" />
    
    <MajorUpgrade DowngradeErrorMessage="A newer version is already installed." />
    <MediaTemplate />
    
    <Feature Id="ProductFeature" Title="WinForms Fashion Shop" Level="1">
      <ComponentGroupRef Id="ProductComponents" />
    </Feature>
  </Product>
  
  <Fragment>
    <Directory Id="TARGETDIR" Name="SourceDir">
      <Directory Id="ProgramFilesFolder">
        <Directory Id="INSTALLFOLDER" Name="WinFormsFashionShop" />
      </Directory>
    </Directory>
  </Fragment>
  
  <Fragment>
    <ComponentGroup Id="ProductComponents" Directory="INSTALLFOLDER">
      <Component Id="GUI.exe">
        <File Source="$(var.GUI.TargetPath)" />
      </Component>
      <!-- Add other files -->
    </ComponentGroup>
  </Fragment>
</Wix>
```

3. **Build installer:**

```powershell
# Build application first
dotnet publish GUI/GUI.csproj --configuration Release --output ./publish

# Build installer
candle Installer.wxs
light Installer.wixobj -ext WixUIExtension
```

#### Sử dụng Inno Setup (EXE Installer) - Dễ hơn

1. **Download Inno Setup:** https://jrsoftware.org/isdl.php

2. **Tạo script `installer.iss`:**

```iss
[Setup]
AppName=WinForms Fashion Shop
AppVersion=1.0.0
DefaultDirName={pf}\WinFormsFashionShop
DefaultGroupName=WinForms Fashion Shop
OutputDir=installer
OutputBaseFilename=WinFormsFashionShop-Setup-v1.0.0
Compression=lzma
SolidCompression=yes
PrivilegesRequired=admin

[Files]
Source: "publish\GUI\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs

[Icons]
Name: "{group}\WinForms Fashion Shop"; Filename: "{app}\GUI.exe"
Name: "{commondesktop}\WinForms Fashion Shop"; Filename: "{app}\GUI.exe"

[Run]
Filename: "{app}\GUI.exe"; Description: "Launch WinForms Fashion Shop"; Flags: nowait postinstall skipifsilent
```

3. **Build installer:**
   - Mở Inno Setup Compiler
   - Load file `installer.iss`
   - Click **Build** → **Compile**

---

## 3. Quy trình Release với GitHub Actions

### 3.1. Tạo Release từ Tag

```bash
# 1. Tạo tag version
git tag -a v1.0.0 -m "Release version 1.0.0"

# 2. Push tag lên GitHub
git push origin v1.0.0
```

GitHub Actions sẽ tự động:
- Build và publish application
- Tạo release package (ZIP)
- Tạo GitHub Release với download link

### 3.2. Tạo Release thủ công

1. Vào GitHub repository
2. Click **Actions** tab
3. Chọn workflow **CD - Publish WinForms App**
4. Click **Run workflow**
5. Nhập version number (ví dụ: `1.0.0`)
6. Click **Run workflow**

---

## 4. Cấu trúc Package Release

Sau khi publish, package sẽ có cấu trúc:

```
WinFormsFashionShop-v1.0.0-20241222.zip
├── GUI.exe                    # Main executable
├── *.dll                      # Dependencies
├── Config/                    # Configuration folder
│   └── payos.config.json     # (User cần tự cấu hình)
├── Logs/                      # Log folder (tự tạo khi chạy)
├── CreateDatabase.sql         # Database script
├── README.md                  # Documentation
├── VERSION.txt               # Version info
└── API/                      # API server (nếu cần)
    └── API.exe
```

---

## 5. Hướng dẫn Deploy cho End User

### 5.1. Yêu cầu hệ thống

- **Windows 10/11** (64-bit)
- **SQL Server 2019+** (hoặc SQL Server Express)
- **.NET 8.0 Runtime** (nếu publish framework-dependent)

### 5.2. Các bước cài đặt

1. **Extract package:**
   - Giải nén file ZIP vào thư mục (ví dụ: `C:\Program Files\WinFormsFashionShop`)

2. **Setup Database:**
   - Mở SQL Server Management Studio
   - Chạy script `CreateDatabase.sql`

3. **Cấu hình Database:**
   - Copy `DAO/DatabaseConfig.example.cs` thành `DAO/DatabaseConfig.cs`
   - Cập nhật connection string trong `DatabaseConfig.cs`

4. **Cấu hình PayOS (nếu dùng):**
   - Chạy ứng dụng lần đầu
   - Vào menu **Quản trị** → **Cấu hình PayOS**
   - Nhập PayOS credentials

5. **Chạy ứng dụng:**
   - Double-click `GUI.exe`
   - Đăng nhập với: `admin` / `admin123`

---

## 6. Tối ưu hóa Package Size

### 6.1. Trim unused code

```powershell
dotnet publish GUI/GUI.csproj `
  --configuration Release `
  --self-contained true `
  --runtime win-x64 `
  -p:PublishTrimmed=true `
  -p:TrimMode=link
```

**Lưu ý:** Cần test kỹ vì trimming có thể gây lỗi runtime.

### 6.2. Enable compression

```powershell
-p:EnableCompressionInSingleFile=true
```

### 6.3. Exclude unused files

Thêm vào `.csproj`:

```xml
<PropertyGroup>
  <PublishReadyToRun>true</PublishReadyToRun>
  <PublishSingleFile>true</PublishSingleFile>
  <IncludeNativeLibrariesForSelfExtract>true</IncludeNativeLibrariesForSelfExtract>
</PropertyGroup>
```

---

## 7. Troubleshooting

### Lỗi: "Application failed to start"

**Nguyên nhân:** Thiếu .NET Runtime hoặc dependencies

**Giải pháp:**
- Publish với `--self-contained true`
- Hoặc cài .NET 8.0 Runtime: https://dotnet.microsoft.com/download

### Lỗi: "Cannot find database"

**Nguyên nhân:** Connection string chưa được cấu hình

**Giải pháp:**
- Kiểm tra `DatabaseConfig.cs` có connection string đúng chưa
- Verify SQL Server đang chạy

### Package quá lớn

**Giải pháp:**
- Dùng `PublishSingleFile=true` để giảm số lượng files
- Enable compression
- Consider framework-dependent nếu có thể

---

## 8. Best Practices

1. **Versioning:**
   - Sử dụng Semantic Versioning (v1.0.0, v1.1.0, v2.0.0)
   - Tag mỗi release

2. **Testing:**
   - Test trên máy sạch (không có .NET SDK)
   - Test trên Windows 10 và Windows 11

3. **Documentation:**
   - Luôn kèm README.md trong package
   - Ghi rõ yêu cầu hệ thống

4. **Security:**
   - Không commit `DatabaseConfig.cs` với credentials thật
   - Không commit `appsettings.json` với API keys thật

5. **Updates:**
   - Cung cấp update mechanism (nếu cần)
   - Hoặc hướng dẫn user download version mới

---

## 9. Tài liệu tham khảo

- [.NET Publish Documentation](https://learn.microsoft.com/en-us/dotnet/core/deploying/)
- [GitHub Actions Documentation](https://docs.github.com/en/actions)
- [WiX Toolset Documentation](https://wixtoolset.org/documentation/)
- [Inno Setup Documentation](https://jrsoftware.org/ishelp/)

---

**Tài liệu được cập nhật:** 2024-12-22

