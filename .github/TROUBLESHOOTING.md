# Troubleshooting CI/CD Workflows

## Vấn đề thường gặp và cách khắc phục

### 1. CD Workflow chạy khi push code (không phải tag)

**Triệu chứng:**
- CD workflow chạy mỗi khi push code lên branch
- Workflow fail với lỗi "Invalid trigger"

**Nguyên nhân:**
- GitHub Actions có thể trigger workflow trên regular push trong một số trường hợp
- Workflow file đã được thay đổi và trigger lại

**Giải pháp:**
- Workflow đã có điều kiện `if` ở job level để chỉ chạy khi có tag
- Job `check-trigger` sẽ fail nhanh nếu không phải tag hoặc manual trigger
- Các jobs khác sẽ không chạy nếu `check-trigger` fail

**Kiểm tra:**
- Vào Actions tab → Xem workflow run
- Nếu thấy "Check Trigger" job fail → Đây là expected behavior
- Workflow sẽ skip các jobs khác

---

### 2. CD Workflow không chạy khi push tag

**Triệu chứng:**
- Push tag nhưng workflow không chạy

**Nguyên nhân có thể:**
- Tag format không đúng (phải là `v*.*.*`)
- Workflow file có lỗi syntax

**Giải pháp:**
1. Kiểm tra tag format:
   ```bash
   git tag -l  # Xem tất cả tags
   ```
   Tag phải bắt đầu bằng `v` và theo format `v1.0.0`, `v1.2.3`, etc.

2. Kiểm tra workflow syntax:
   - Vào Actions tab → Workflows
   - Click vào workflow → Xem "Workflow file"
   - Nếu có lỗi syntax, sẽ hiển thị warning

3. Test với manual trigger:
   - Vào Actions tab → Chọn workflow "CD - Publish and Release"
   - Click "Run workflow"
   - Nhập version (ví dụ: `1.0.0`)
   - Click "Run workflow"

---

### 3. Build artifacts không tìm thấy

**Triệu chứng:**
- Warning: "No files were found with the provided path"

**Nguyên nhân:**
- Build output path không đúng
- Files chưa được build

**Giải pháp:**
- Workflow đã có step "List build output directories" để debug
- Kiểm tra logs để xem path nào đúng
- Artifacts sẽ có `if-no-files-found: warn` để không fail build

---

### 4. Code Quality job exit code 1

**Triệu chứng:**
- Code Quality job có exit code 1 nhưng workflow vẫn success

**Giải pháp:**
- Đây là expected behavior
- Job có `continue-on-error: true`
- Chỉ là warning, không fail build

---

### 5. Version detection fail

**Triệu chứng:**
- CD workflow fail ở job "Determine Version"

**Nguyên nhân:**
- Tag format không đúng
- Script bash có lỗi

**Giải pháp:**
1. Kiểm tra tag format:
   ```bash
   git tag -l
   ```
   Phải là `v1.0.0`, không phải `1.0.0`

2. Xem logs trong "Determine Version" job để biết lỗi cụ thể

---

## Debug Workflows

### Enable Debug Logging

Thêm vào workflow file:
```yaml
env:
  ACTIONS_RUNNER_DEBUG: true
  ACTIONS_STEP_DEBUG: true
```

### Xem Logs

1. Vào GitHub → Actions tab
2. Click vào workflow run
3. Click vào job bị lỗi
4. Xem logs từng step

### Test Locally

Có thể dùng `act` để test workflow locally:
```bash
# Cài đặt act (cần Docker)
# https://github.com/nektos/act

# Test workflow
act push --eventpath event.json
```

---

## Best Practices

1. **Luôn test workflow trước khi merge:**
   - Tạo branch riêng
   - Test workflow trên branch đó
   - Merge khi đã chắc chắn

2. **Sử dụng manual trigger để test:**
   - Vào Actions tab
   - Chọn workflow
   - Click "Run workflow"
   - Test với inputs khác nhau

3. **Kiểm tra logs kỹ:**
   - Đọc logs từng step
   - Tìm error messages
   - Check environment variables

4. **Version tags:**
   - Luôn dùng format `v*.*.*`
   - Ví dụ: `v1.0.0`, `v1.2.3`, `v2.0.0-beta.1`

---

## Liên hệ

Nếu vẫn gặp vấn đề, xem:
- [GitHub Actions Documentation](https://docs.github.com/en/actions)
- [Workflow Syntax](https://docs.github.com/en/actions/using-workflows/workflow-syntax-for-github-actions)
- [Troubleshooting](https://docs.github.com/en/actions/how-tos/troubleshooting-workflows)

