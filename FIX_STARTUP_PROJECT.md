# 🔧 Hướng dẫn sửa lỗi Startup Project

## ❌ Lỗi hiện tại
```
A project with an Output Type of Class Library cannot be started directly.
```

## ✅ Cách khắc phục

### Bước 1: Đóng Visual Studio (nếu đang mở)

### Bước 2: Xóa file .suo (đã được xóa tự động)

### Bước 3: Mở lại Visual Studio và Solution

### Bước 4: Set Startup Project

**Cách 1: Right-click trong Solution Explorer**
1. Mở **Solution Explorer** (View → Solution Explorer hoặc **Ctrl+Alt+L**)
2. **Right-click** vào project **`WinFormsFashionShop.Presentation`** (project GUI)
3. Chọn **"Set as Startup Project"**
4. Project này sẽ được **in đậm** trong Solution Explorer

**Cách 2: Chọn từ Toolbar**
1. Nhìn vào **toolbar** phía trên (gần nút Play/Debug)
2. Có một **dropdown** hiển thị project hiện tại
3. **Click vào dropdown** đó
4. Chọn **"WinFormsFashionShop.Presentation"**

**Cách 3: Properties Panel**
1. Click vào **Solution** (node đầu tiên) trong Solution Explorer
2. Mở **Properties** panel (View → Properties Window hoặc **F4**)
3. Tìm property **"Startup project"**
4. Chọn **"WinFormsFashionShop.Presentation"**

### Bước 5: Kiểm tra

Sau khi set startup project, bạn sẽ thấy:
- ✅ Trong **Solution Explorer**: Project `WinFormsFashionShop.Presentation` được **in đậm**
- ✅ Trong **Toolbar**: Dropdown hiển thị "WinFormsFashionShop.Presentation"
- ✅ Trong **Properties** (khi click vào Solution): "Startup project: WinFormsFashionShop.Presentation"

### Bước 6: Chạy ứng dụng

Nhấn **Ctrl+F5** (Run without debugging) hoặc **F5** (Run with debugging)

---

## 🚀 Hoặc chạy từ Command Line (không cần Visual Studio)

Mở **Terminal** hoặc **PowerShell** và chạy:

```powershell
dotnet run --project GUI
```

Hoặc:

```powershell
cd GUI
dotnet run
```

---

## 📝 Lưu ý

- **WinFormsFashionShop.Presentation** (GUI) = **WinExe** → ✅ Có thể chạy
- **WinFormsFashionShop.Data** (DAO) = **Class Library** → ❌ Không thể chạy
- **WinFormsFashionShop.Business** (BUS) = **Class Library** → ❌ Không thể chạy
- **DTO** = **Class Library** → ❌ Không thể chạy

**Chỉ có project GUI mới có thể chạy được!**

