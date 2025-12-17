# Thư mục lưu trữ ảnh

Thư mục này chứa tất cả các ảnh được sử dụng trong ứng dụng.

## Cấu trúc thư mục

```
Images/
├── Products/          # Ảnh sản phẩm
│   └── [ProductId]_[Timestamp].[ext]
├── Categories/         # Ảnh danh mục (nếu có)
│   └── [CategoryId]_[Timestamp].[ext]
├── Users/             # Avatar người dùng (nếu có)
│   └── [UserId]_[Timestamp].[ext]
├── Customers/         # Ảnh khách hàng (nếu có)
│   └── [CustomerId]_[Timestamp].[ext]
├── Orders/            # Ảnh liên quan đến đơn hàng (nếu có)
│   └── [OrderId]_[Timestamp].[ext]
├── Invoices/          # Ảnh hóa đơn/giấy tờ (nếu có)
│   └── [InvoiceId]_[Timestamp].[ext]
└── Temp/              # Ảnh tạm thời (sẽ được xóa tự động)
    └── [Temp files]
```

## Quy tắc đặt tên file

- Format: `[EntityId]_[Timestamp].[extension]`
- Ví dụ: `1_20241201143025.jpg`
- Timestamp format: `yyyyMMddHHmmss`

## Định dạng hỗ trợ

- JPG/JPEG
- PNG
- GIF
- BMP
- WEBP

## Kích thước tối đa

- 5MB per file

## Lưu ý

- Tất cả đường dẫn ảnh trong database là relative path (bắt đầu từ `Images/`)
- Thư mục sẽ được tạo tự động khi lưu ảnh lần đầu
- Ảnh được lưu trong thư mục ứng dụng (AppDomain.CurrentDomain.BaseDirectory)

