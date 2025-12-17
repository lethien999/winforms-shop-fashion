# Thư mục Logo Thương hiệu

Thư mục này chứa logo và các tài sản thương hiệu của ứng dụng.

## Tên file logo được hỗ trợ

Ứng dụng sẽ tự động tìm logo với các tên sau (theo thứ tự ưu tiên):

1. `logo.png`
2. `logo.jpg`
3. `logo.jpeg`
4. `brand.png`
5. `brand.jpg`
6. `TF_logo.png`
7. `TF_logo.jpg`

## Cách sử dụng

1. Đặt file logo vào thư mục này với một trong các tên trên
2. Logo sẽ tự động được hiển thị trong các form của ứng dụng
3. Logo được tự động resize để phù hợp với kích thước hiển thị

## Kích thước khuyến nghị

- **Logo nhỏ**: 32x32px (cho menu, status bar)
- **Logo trung bình**: 48x48px (cho header form)
- **Logo lớn**: 64x64px (cho login form)
- **Logo rất lớn**: 96x96px (cho splash screen)

## Định dạng hỗ trợ

- PNG (khuyến nghị - hỗ trợ transparency)
- JPG/JPEG
- GIF
- BMP

## Lưu ý

- Logo nên có nền trong suốt (transparent) để hiển thị đẹp trên mọi nền
- Tỷ lệ khung hình vuông (1:1) được khuyến nghị
- File logo sẽ không được commit vào Git (theo .gitignore)

