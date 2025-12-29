# GitHub Actions Workflows

Tài liệu hướng dẫn sử dụng các GitHub Actions workflows cho project.

## 📋 Tổng quan

Project có 3 workflows:

| Workflow | File | Mục đích |
|----------|------|----------|
| **Build/Release** ⭐ | `build.yml` | Build trên mọi push, release khi có tag (Recommended) |
| **CI** | `ci.yml` | Build và quality checks |
| **CD** | `cd.yml` | Publish và release (legacy) |

---

## 🚀 Build/Release Workflow (Recommended)

**File:** `.github/workflows/build.yml`

Workflow này được thiết kế theo mô hình của [electron-builder](https://github.com/OpenBuilds/action-electron-build):
- **Build trên MỌI push** - Tự động build và kiểm tra code
- **Release khi có tag** - Tự động tạo GitHub Release khi push tag `v*.*.*`
- **Draft release** - Release được tạo ở chế độ draft để review trước khi publish

### 📦 Cách tạo Release mới

```bash
# 1. Commit changes
git add .
git commit -m "Release v1.0.0"

# 2. Create version tag
git tag v1.0.0

# 3. Push code and tag
git push origin main
git push --tags
```

GitHub Actions sẽ tự động:
1. Build solution
2. Tạo 3 packages:
   - `WinFormsFashionShop-v1.0.0-full-win64.zip` - Full package với API
   - `WinFormsFashionShop-v1.0.0-portable-win64.zip` - Single executable
   - `WinFormsFashionShop-PaymentAPI-v1.0.0-win64.zip` - API server only
3. Tạo **draft release** trên GitHub

### 📝 Publish Release

Sau khi workflow chạy xong:

1. Vào **Releases** page trên GitHub
2. Tìm draft release mới tạo
3. Review và chỉnh sửa release notes nếu cần
4. Click **Publish release**

### 🏷️ Version Tag Format

| Tag | Type | Description |
|-----|------|-------------|
| `v1.0.0` | Stable | Production release |
| `v1.0.0-alpha` | Pre-release | Alpha testing |
| `v1.0.0-beta` | Pre-release | Beta testing |
| `v1.0.0-rc.1` | Pre-release | Release candidate |

### ⚙️ Manual Trigger

Bạn cũng có thể trigger workflow thủ công:

1. Vào **Actions** tab
2. Chọn **Build/Release** workflow
3. Click **Run workflow**
4. Chọn options:
   - `create_release`: true/false
   - `version`: version number (e.g., 1.0.0)

---

## 🔄 CI Workflow - Build and Test

**File:** `.github/workflows/ci.yml`

### Trigger
- Tự động chạy khi:
  - Push code lên branch `main` hoặc `develop`
  - Tạo Pull Request vào `main` hoặc `develop`
- Có thể trigger thủ công từ GitHub Actions tab

### Jobs

#### 1. Build Solution
- Build toàn bộ solution với configuration `Release`
- Upload build artifacts
- Thời gian: ~5-10 phút

#### 2. Code Quality (Tùy chọn)
- Kiểm tra TODO/FIXME comments
- Có thể thêm: code formatting, SonarCloud, security scanning

#### 3. Run Tests (Tùy chọn)
- Chạy unit tests (nếu có test project)
- Upload test results và code coverage
- Hiện tại đang tắt vì chưa có test project

### Xem kết quả
1. Vào GitHub repository
2. Click tab **Actions**
3. Chọn workflow run muốn xem
4. Click vào job để xem chi tiết logs

---

## � CD Workflow - Publish and Release (Legacy)

**File:** `.github/workflows/cd.yml`

> ⚠️ **Note:** Workflow này được giữ lại cho backward compatibility. Khuyến khích sử dụng **Build/Release** workflow mới.

### Trigger
- Tự động chạy khi:
  - Tạo tag version (ví dụ: `v1.0.0`, `v1.2.3`)
- Có thể trigger thủ công với inputs:
  - `version`: Version number (required)
  - `create_release`: Có tạo GitHub Release không (optional, default: true)

### Jobs

#### 1. Determine Version
- Xác định version từ tag hoặc input
- Phân biệt prerelease (alpha, beta, rc) và release chính thức

#### 2. Build and Publish
- Build solution
- Publish WinForms app (2 versions):
  - **Self-contained**: Tất cả files trong folder
  - **Single file**: 1 file .exe duy nhất
- Publish API
- Tạo release package (ZIP)
- Upload artifacts

#### 3. Create GitHub Release
- Tạo GitHub Release với:
  - Release notes tự động
  - Download links cho packages
  - Changelog từ commits

### Cách sử dụng

#### Tạo Release từ Tag

```bash
# 1. Tạo tag version
git tag -a v1.0.0 -m "Release version 1.0.0"

# 2. Push tag lên GitHub
git push origin v1.0.0
```

GitHub Actions sẽ tự động:
- Build và publish application
- Tạo release packages
- Tạo GitHub Release với download links

#### Tạo Release thủ công

1. Vào GitHub repository → **Actions** tab
2. Chọn workflow **CD - Publish and Release**
3. Click **Run workflow**
4. Nhập:
   - **Version**: `1.0.0` (hoặc version khác)
   - **Create GitHub Release**: `true` (hoặc `false`)
5. Click **Run workflow**

---

## 📦 Release Packages

Sau khi publish, bạn sẽ có 2 packages:

### 1. Full Package (`WinFormsFashionShop-v1.0.0-YYYYMMDD.zip`)
- Chứa tất cả files cần thiết
- Folder structure rõ ràng
- Dễ debug và maintain

**Cấu trúc:**
```
WinFormsFashionShop-v1.0.0-20241222.zip
├── GUI.exe                    # Main executable
├── *.dll                      # Dependencies
├── Config/                    # Configuration folder
├── API/                       # API server
│   └── API.exe
├── CreateDatabase.sql         # Database script
├── README.md                  # Documentation
└── VERSION.txt               # Version info
```

### 2. Single File Package (`*-singlefile.zip`)
- Chỉ có 1 file .exe
- Kích thước nhỏ hơn
- Khởi động nhanh hơn

**Cấu trúc:**
```
WinFormsFashionShop-v1.0.0-20241222-singlefile.zip
├── GUI.exe                    # Single executable
├── CreateDatabase.sql
├── README.md
└── VERSION.txt
```

---

## 🔧 Cấu hình và Tùy chỉnh

### Thay đổi .NET Version

Sửa trong workflow files:
```yaml
env:
  DOTNET_VERSION: '8.0.x'  # Thay đổi version ở đây
```

### Thay đổi Runtime

Sửa trong `cd-publish.yml`:
```yaml
env:
  RUNTIME: 'win-x64'  # Hoặc win-x86, win-arm64
```

### Thêm Code Quality Checks

Thêm vào `ci.yml`:
```yaml
- name: Run SonarCloud
  uses: SonarSource/sonarcloud-github-action@master
  # ... config
```

### Thêm Test Project

1. Tạo test project (xUnit, NUnit, MSTest)
2. Thêm vào solution
3. Bật job `test` trong `ci.yml`:
   ```yaml
   test:
     if: true  # Thay đổi từ false thành true
   ```

---

## 🐛 Troubleshooting

### Workflow không chạy

**Kiểm tra:**
- File workflow có đúng format YAML không?
- Trigger conditions có đúng không?
- Branch name có khớp với trigger không?

### Build failed

**Kiểm tra:**
- Xem logs trong GitHub Actions
- Kiểm tra .NET version có đúng không
- Kiểm tra dependencies có đầy đủ không

### Release không được tạo

**Kiểm tra:**
- Tag format có đúng không? (phải là `v*.*.*`)
- `GITHUB_TOKEN` có quyền tạo release không?
- `create_release` input có được set đúng không?

### Package quá lớn

**Giải pháp:**
- Dùng single file version
- Enable compression
- Trim unused code (cẩn thận, có thể gây lỗi)

---

## 📚 Best Practices

1. **Versioning:**
   - Sử dụng Semantic Versioning (v1.0.0, v1.1.0, v2.0.0)
   - Tag mỗi release

2. **Testing:**
   - Luôn test workflow trên branch riêng trước
   - Test trên máy local trước khi push

3. **Security:**
   - Không commit secrets vào workflow files
   - Sử dụng GitHub Secrets cho sensitive data

4. **Performance:**
   - Sử dụng caching cho NuGet packages
   - Chỉ build những gì cần thiết

5. **Documentation:**
   - Cập nhật release notes mỗi release
   - Ghi rõ breaking changes

---

## 🔗 Tài liệu tham khảo

- [GitHub Actions Documentation](https://docs.github.com/en/actions)
- [.NET Publish Documentation](https://learn.microsoft.com/en-us/dotnet/core/deploying/)
- [OpenBuilds/action-electron-build](https://github.com/OpenBuilds/action-electron-build) - Inspiration for build.yml

---

## 📊 So sánh Workflows

| Feature | build.yml ⭐ | ci.yml | cd.yml |
|---------|-------------|--------|--------|
| Build on push | ✅ | ✅ | ❌ |
| Build on PR | ✅ | ✅ | ❌ |
| Release on tag | ✅ | ❌ | ✅ |
| Draft release | ✅ | ❌ | ❌ |
| Code quality | ❌ | ✅ | ❌ |
| ClickOnce | ❌ | ❌ | ✅ |
| Recommended | ⭐ Yes | For CI only | Legacy |

---

**Tài liệu được cập nhật:** 2025-12-29

