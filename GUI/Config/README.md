# Thư mục cấu hình

Thư mục này chứa các file cấu hình cho ứng dụng.

## PayOS Configuration

File `payos.config.json` chứa thông tin cấu hình PayOS API:
- Client ID
- API Key  
- Checksum Key

**⚠️ LƯU Ý BẢO MẬT:**
- File này chứa thông tin nhạy cảm
- Không commit file này lên Git (đã được thêm vào .gitignore)
- Chỉ chia sẻ với người có quyền truy cập

## Cấu trúc file

```json
{
  "PayOS": {
    "ClientId": "your-client-id",
    "ApiKey": "your-api-key",
    "ChecksumKey": "your-checksum-key"
  }
}
```

## Cách cập nhật

1. Mở ứng dụng
2. Vào menu "Quản trị" → "Cấu hình PayOS"
3. Nhập thông tin mới và click "Lưu"

Hoặc chỉnh sửa trực tiếp file `payos.config.json` trong thư mục này.

