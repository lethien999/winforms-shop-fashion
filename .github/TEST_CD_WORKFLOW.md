# Hướng dẫn Test CD Workflow

## Cách 1: Test bằng Tag (Tự động)

### Tạo và Push Tag

```bash
# Tạo tag version mới
git tag -a v1.0.1 -m "Test release"

# Push tag lên GitHub
git push origin v1.0.1
```

**Kết quả:**
- CD workflow sẽ tự động trigger
- Vào Actions tab để xem workflow đang chạy
- Đợi ~15-20 phút để hoàn thành

### Xóa Tag (nếu cần test lại)

```bash
# Xóa tag local
git tag -d v1.0.1

# Xóa tag trên GitHub
git push origin :refs/tags/v1.0.1
```

---

## Cách 2: Test bằng Manual Trigger

### Bước 1: Vào GitHub Actions

1. Mở repository trên GitHub
2. Click tab **Actions**

### Bước 2: Chọn Workflow

1. Trong sidebar bên trái, click **"CD - Publish and Release"**
2. Hoặc tìm workflow trong danh sách

### Bước 3: Trigger Manual

1. Click nút **"Run workflow"** (ở góc trên bên phải)
2. Chọn branch: `main`
3. Nhập **Version**: `1.0.1` (hoặc version khác)
4. **Create GitHub Release**: `true` (hoặc `false` nếu chỉ muốn test build)
5. Click **"Run workflow"**

### Bước 4: Theo dõi

1. Workflow sẽ xuất hiện trong danh sách runs
2. Click vào để xem chi tiết
3. Xem từng job:
   - **Check Trigger**: Should pass
   - **Determine Version**: Should extract version
   - **Build and Publish**: Build và publish app
   - **Create GitHub Release**: Tạo release (nếu enable)

---

## Kiểm tra Kết quả

### 1. Workflow Status

- Vào **Actions** tab
- Tìm workflow run mới nhất
- Kiểm tra status: ✅ Success hoặc ❌ Failure

### 2. Artifacts

- Click vào workflow run
- Scroll xuống phần **Artifacts**
- Sẽ thấy:
  - `winforms-release-v1.0.1`
  - Chứa ZIP packages

### 3. GitHub Release

- Vào tab **Releases**
- Sẽ thấy release **"Release v1.0.1"**
- Có download links cho packages
- Có release notes tự động

---

## Expected Results

### ✅ Success Case

1. **Check Trigger**: ✅ Pass
   - Log: "✅ Tag push detected" hoặc "✅ Manual trigger detected"

2. **Determine Version**: ✅ Pass
   - Log: "Determined version: 1.0.1 (prerelease: false)"

3. **Build and Publish**: ✅ Pass
   - Build solution thành công
   - Publish WinForms app (2 versions)
   - Publish API
   - Tạo release packages

4. **Create GitHub Release**: ✅ Pass
   - Tạo release với download links
   - Generate release notes

### ❌ Failure Cases

#### Case 1: Check Trigger Fail

**Triệu chứng:**
- Job "Check Trigger" fail
- Log: "❌ Invalid trigger"

**Nguyên nhân:**
- Workflow trigger trên regular push (không phải tag)
- Đây là expected behavior - workflow sẽ skip

**Giải pháp:**
- Không cần làm gì, đây là bảo vệ
- Chỉ test với tag hoặc manual trigger

#### Case 2: Version Detection Fail

**Triệu chứng:**
- Job "Determine Version" fail
- Log: "Error: Version cannot be empty"

**Nguyên nhân:**
- Tag format không đúng
- Manual trigger không nhập version

**Giải pháp:**
- Kiểm tra tag format: phải là `v*.*.*`
- Manual trigger: nhập version đầy đủ

#### Case 3: Build Fail

**Triệu chứng:**
- Job "Build and Publish" fail
- Log: Build errors

**Nguyên nhân:**
- Code có lỗi compile
- Dependencies thiếu

**Giải pháp:**
- Xem logs để biết lỗi cụ thể
- Fix lỗi trong code
- Test lại

---

## Debug Tips

### 1. Xem Logs Chi tiết

1. Vào workflow run
2. Click vào job bị lỗi
3. Click vào step để xem logs
4. Tìm error messages

### 2. Enable Debug Logging

Thêm vào workflow file (tạm thời):
```yaml
env:
  ACTIONS_RUNNER_DEBUG: true
  ACTIONS_STEP_DEBUG: true
```

### 3. Test từng Job

- Có thể comment out các jobs không cần thiết
- Chỉ test job cần debug

---

## Quick Test Commands

```bash
# Test với tag mới
git tag -a v1.0.2 -m "Quick test"
git push origin v1.0.2

# Xem tags
git tag -l

# Xóa tag (nếu cần)
git tag -d v1.0.2
git push origin :refs/tags/v1.0.2
```

---

**Lưu ý:** Mỗi lần test sẽ tốn GitHub Actions minutes. Hãy test cẩn thận!

