# 🔴 VẤN ĐỀ DNS: Không thể kết nối PayOS API

## 📋 Tình trạng hiện tại

**Lỗi**: DNS không thể resolve `api.payos.vn`
- `nslookup api.payos.vn` → "No internal type for both IPv4 and IPv6 Addresses"
- `Test-NetConnection` → "Name resolution failed"
- API không thể check payment status từ PayOS

**Hệ quả**:
- Order 12 (PayOSOrderCode 12853) đã thanh toán trên PayOS Dashboard
- Nhưng WinForms vẫn hiển thị "Đang chờ thanh toán" vì không check được từ PayOS API

---

## ✅ GIẢI PHÁP TẠM THỜI: Manual Update

### Bước 1: Chạy SQL Script để update Order 12

```sql
-- Chạy file: Database/ManualUpdateOrder12.sql
```

Script này sẽ:
- ✅ Gọi `ProcessPayOSWebhook` stored procedure
- ✅ Update Order 12 → Status = "Paid"
- ✅ Ghi audit log
- ✅ Đảm bảo idempotency (không duplicate)

### Bước 2: Verify kết quả

Sau khi chạy script, kiểm tra:
```sql
SELECT OrderId, PayOSOrderCode, Status, PaidAt 
FROM Orders 
WHERE OrderId = 12;
```

---

## 🔧 GIẢI PHÁP LÂU DÀI: Fix DNS

### Option 1: Đổi DNS Server (Khuyến nghị)

**Windows:**
1. Mở **Settings** → **Network & Internet** → **Ethernet** (hoặc **Wi-Fi**)
2. Click **Change adapter options**
3. Right-click network adapter → **Properties**
4. Chọn **Internet Protocol Version 4 (TCP/IPv4)** → **Properties**
5. Chọn **Use the following DNS server addresses**:
   - **Preferred DNS server**: `8.8.8.8` (Google DNS)
   - **Alternate DNS server**: `1.1.1.1` (Cloudflare DNS)
6. Click **OK** → **OK**
7. **Restart máy** hoặc chạy:
   ```powershell
   ipconfig /flushdns
   ```

**Test lại:**
```powershell
nslookup api.payos.vn
Resolve-DnsName api.payos.vn
```

### Option 2: Thêm vào hosts file (Nếu biết IP của PayOS)

**⚠️ Không khuyến nghị** vì IP có thể thay đổi.

### Option 3: Dùng VPN/Proxy

Nếu DNS của ISP bị chặn, dùng VPN hoặc proxy.

---

## 🔄 GIẢI PHÁP THAY THẾ: Dùng Webhook

Nếu API có **public URL** (không phải localhost), có thể dùng webhook:

1. **Cấu hình Webhook URL trong PayOS Dashboard:**
   - Vào PayOS Merchant Dashboard
   - Settings → Webhook
   - Set URL: `https://your-public-api-url.com/api/payment/webhook`

2. **Webhook sẽ tự động update khi payment thành công**
   - Không cần check API
   - PayOS gửi POST request đến webhook endpoint
   - Code đã có sẵn xử lý webhook trong `PaymentController.HandleWebhook`

**⚠️ Lưu ý**: Nếu API chạy trên localhost, webhook không hoạt động vì PayOS không thể gửi request đến localhost.

---

## 📊 So sánh các giải pháp

| Giải pháp | Ưu điểm | Nhược điểm | Khuyến nghị |
|-----------|---------|------------|-------------|
| **Manual Update (SQL)** | ✅ Nhanh, không cần fix network | ❌ Phải làm thủ công mỗi lần | ✅ Tạm thời |
| **Fix DNS** | ✅ Tự động, lâu dài | ❌ Cần quyền admin | ✅ **Khuyến nghị** |
| **Webhook** | ✅ Tự động, real-time | ❌ Cần public URL | ✅ Nếu có public URL |
| **VPN/Proxy** | ✅ Không cần fix DNS | ❌ Phụ thuộc VPN | ⚠️ Tạm thời |

---

## 🧪 Test sau khi fix DNS

1. **Test DNS resolution:**
   ```powershell
   nslookup api.payos.vn
   Resolve-DnsName api.payos.vn
   ```

2. **Test API connection:**
   ```powershell
   Test-NetConnection -ComputerName api.payos.vn -Port 443
   ```

3. **Test trong code:**
   - Restart API server
   - Tạo order mới
   - Click "Kiểm tra thanh toán"
   - Xem log trong Visual Studio Output Window

---

## 📝 Checklist

- [ ] Chạy `Database/ManualUpdateOrder12.sql` để update Order 12
- [ ] Verify Order 12 đã chuyển sang "Paid"
- [ ] Đổi DNS server (8.8.8.8, 1.1.1.1)
- [ ] Flush DNS cache: `ipconfig /flushdns`
- [ ] Test DNS resolution: `nslookup api.payos.vn`
- [ ] Restart API server
- [ ] Test tạo order mới và check payment status

---

## 🆘 Nếu vẫn không được

1. **Kiểm tra Firewall:**
   - Windows Firewall có chặn outbound connection không?
   - Corporate firewall/proxy có chặn `api.payos.vn` không?

2. **Kiểm tra Network:**
   - Internet có hoạt động bình thường không?
   - Có thể ping được các domain khác không?

3. **Contact IT Support:**
   - Nếu dùng corporate network, liên hệ IT để whitelist `api.payos.vn`

4. **Dùng Manual Update:**
   - Nếu không fix được DNS, dùng SQL script để manual update mỗi khi có payment

