# 🚀 Quick Start - GitHub Actions CI/CD

Hướng dẫn nhanh để bắt đầu sử dụng CI/CD cho project.

## ✅ Đã sẵn sàng

Project đã được thiết lập CI/CD đầy đủ. Bạn chỉ cần:

### 1. Test CI Workflow (Build)

```bash
# Push code lên GitHub
git add .
git commit -m "test: Test CI workflow"
git push origin main
```

**Kết quả:**
- Vào GitHub → **Actions** tab
- Bạn sẽ thấy workflow **CI - Build and Test** đang chạy
- Đợi ~5-10 phút để hoàn thành
- Nếu thành công → ✅ Build artifacts sẽ được upload

### 2. Tạo Release đầu tiên

```bash
# 1. Tạo tag version
git tag -a v1.0.0 -m "First release"

# 2. Push tag lên GitHub
git push origin v1.0.0
```

**Kết quả:**
- Workflow **CD - Publish and Release** sẽ tự động chạy
- Sau ~15-20 phút, bạn sẽ có:
  - ✅ Release packages (ZIP files)
  - ✅ GitHub Release với download links
  - ✅ Release notes tự động

### 3. Xem Release

1. Vào GitHub repository
2. Click **Releases** (bên phải)
3. Bạn sẽ thấy release **v1.0.0** với:
   - Download links cho packages
   - Release notes
   - Changelog

---

## 📝 Các bước tiếp theo

### Thêm Test Project (Tùy chọn)

1. Tạo test project:
   ```bash
   dotnet new xunit -n Tests
   dotnet sln add Tests/Tests.csproj
   ```

2. Bật test job trong `.github/workflows/ci.yml`:
   ```yaml
   test:
     if: true  # Thay đổi từ false
   ```

### Thêm Code Quality Tools (Tùy chọn)

Có thể thêm:
- **dotnet format** - Code formatting
- **SonarCloud** - Code analysis
- **Security scanning** - Vulnerability detection

Xem chi tiết trong `.github/workflows/README.md`

---

## 🎯 Workflow Summary

| Workflow | Trigger | Thời gian | Kết quả |
|----------|---------|----------|---------|
| **CI** | Push/PR | ~5-10 phút | Build artifacts |
| **CD** | Tag v*.*.* | ~15-20 phút | Release packages + GitHub Release |

---

## ❓ Cần giúp đỡ?

- Xem chi tiết: [`.github/workflows/README.md`](.github/workflows/README.md)
- Xem hướng dẫn publish: [`PUBLISH_GUIDE.md`](../PUBLISH_GUIDE.md)

---

**Happy Coding! 🎉**

