using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using WinFormsFashionShop.DTO;
using WinFormsFashionShop.Business.Composition;

namespace WinFormsFashionShop.Presentation.Helpers
{
    /// <summary>
    /// Helper class for printing orders.
    /// Single responsibility: only handles printing functionality.
    /// </summary>
    public static class PrintHelper
    {
        private static OrderDTO? _orderToPrint;

        /// <summary>
        /// Prints an order using PrintDocument.
        /// Kiểm tra và đánh dấu đã in để tránh in trùng hóa đơn.
        /// </summary>
        public static void PrintOrder(OrderDTO order, bool checkPrinted = true)
        {
            if (order == null) throw new ArgumentNullException(nameof(order));

            // Kiểm tra nếu đã in rồi (nếu có thông tin PrintedAt từ DTO)
            // Note: Cần cập nhật OrderDTO để có PrintedAt nếu muốn check từ DTO
            // Tạm thời, check sẽ được thực hiện trong OrderService

            _orderToPrint = order;

            using var printDoc = new PrintDocument();
            printDoc.PrintPage += PrintDocument_PrintPage;

            using var printDialog = new PrintDialog
            {
                Document = printDoc,
                AllowSomePages = false,
                ShowHelp = false
            };

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDoc.Print();
                
                // Đánh dấu đã in sau khi in thành công
                if (checkPrinted)
                {
                    try
                    {
                        var services = ServicesComposition.Create();
                        services.OrderService.MarkOrderAsPrinted(order.Id);
                    }
                    catch (Exception ex)
                    {
                        // Log lỗi nhưng không throw để không ảnh hưởng đến việc in
                        System.Diagnostics.Debug.WriteLine($"Lỗi đánh dấu đã in: {ex.Message}");
                    }
                }
            }
        }

        /// <summary>
        /// In hóa đơn tự động (không hiển thị dialog) - dùng cho tự động in sau khi thanh toán
        /// </summary>
        public static bool PrintOrderAuto(OrderDTO order)
        {
            if (order == null) throw new ArgumentNullException(nameof(order));

            try
            {
                _orderToPrint = order;

                using var printDoc = new PrintDocument();
                printDoc.PrintPage += PrintDocument_PrintPage;

                // In trực tiếp không cần dialog (dùng máy in mặc định)
                printDoc.Print();

                // Đánh dấu đã in
                try
                {
                    var services = ServicesComposition.Create();
                    services.OrderService.MarkOrderAsPrinted(order.Id);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Lỗi đánh dấu đã in: {ex.Message}");
                }

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi in tự động: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Handles the PrintPage event to draw order content.
        /// Single responsibility: only draws order content on print page.
        /// </summary>
        private static void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (_orderToPrint == null || e?.Graphics == null) return;

            var graphics = e.Graphics;
            var font = new Font("Arial", 12);
            var boldFont = new Font("Arial", 12, FontStyle.Bold);
            var titleFont = new Font("Arial", 16, FontStyle.Bold);
            var smallFont = new Font("Arial", 10);

            float yPos = 0;
            float leftMargin = e.MarginBounds.Left;
            float topMargin = e.MarginBounds.Top;
            float rightMargin = e.MarginBounds.Right;

            // Header with background
            var headerRect = new RectangleF(leftMargin, yPos, rightMargin - leftMargin, 80);
            using (var brush = new SolidBrush(Color.FromArgb(240, 240, 240)))
            {
                graphics.FillRectangle(brush, headerRect);
            }
            var roundedRect = Rectangle.Round(headerRect);
            graphics.DrawRectangle(Pens.Gray, roundedRect);

            // Title
            var titleRect = new RectangleF(leftMargin + 10, yPos + 10, rightMargin - leftMargin - 20, 30);
            graphics.DrawString("HÓA ĐƠN BÁN HÀNG", titleFont, Brushes.Black, titleRect);
            yPos += 40;

            graphics.DrawString("WinForms Fashion Shop", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, leftMargin + 10, yPos);
            yPos += font.GetHeight() + 5;

            graphics.DrawString("Địa chỉ: 123 Đường ABC, Quận XYZ, TP.HCM", smallFont, Brushes.Black, leftMargin + 10, yPos);
            yPos += smallFont.GetHeight() + 3;

            graphics.DrawString($"ĐT: 0123 456 789 | Email: shop@fashion.com", smallFont, Brushes.Black, leftMargin + 10, yPos);
            yPos += smallFont.GetHeight() + 15;

            // Order info
            graphics.DrawString($"Mã đơn: {_orderToPrint.OrderCode}", font, Brushes.Black, leftMargin, yPos);
            yPos += font.GetHeight() + 5;

            graphics.DrawString($"Ngày: {_orderToPrint.OrderDate:dd/MM/yyyy HH:mm}", font, Brushes.Black, leftMargin, yPos);
            yPos += font.GetHeight() + 5;

            graphics.DrawString($"Khách hàng: {(_orderToPrint.CustomerName ?? "Khách lẻ")}", font, Brushes.Black, leftMargin, yPos);
            yPos += font.GetHeight() + 5;

            graphics.DrawString($"Nhân viên: {(_orderToPrint.UserName ?? "")}", font, Brushes.Black, leftMargin, yPos);
            yPos += font.GetHeight() + 5;

            graphics.DrawString($"PTTT: {(_orderToPrint.PaymentMethod ?? "")}", font, Brushes.Black, leftMargin, yPos);
            yPos += font.GetHeight() + 10;

            // Line
            graphics.DrawLine(Pens.Black, leftMargin, yPos, rightMargin, yPos);
            yPos += 10;

            // Items table header with background
            var headerTableRect = new RectangleF(leftMargin, yPos, rightMargin - leftMargin, 25);
            graphics.FillRectangle(new SolidBrush(Color.FromArgb(70, 130, 180)), headerTableRect);
            graphics.DrawRectangle(Pens.Black, Rectangle.Round(headerTableRect));

            var headerFont = new Font("Arial", 10, FontStyle.Bold);
            graphics.DrawString("STT", headerFont, Brushes.White, leftMargin + 5, yPos + 3);
            graphics.DrawString("Tên sản phẩm", headerFont, Brushes.White, leftMargin + 50, yPos + 3);
            graphics.DrawString("SL", headerFont, Brushes.White, leftMargin + 300, yPos + 3);
            graphics.DrawString("Đơn giá", headerFont, Brushes.White, leftMargin + 350, yPos + 3);
            graphics.DrawString("Thành tiền", headerFont, Brushes.White, leftMargin + 450, yPos + 3);
            yPos += 25;

            // Items with borders
            int stt = 1;
            foreach (var item in _orderToPrint.Items ?? Enumerable.Empty<OrderItemDTO>())
            {
                if (yPos > e.MarginBounds.Bottom - 100)
                {
                    e.HasMorePages = true;
                    return;
                }

                var rowRect = new RectangleF(leftMargin, yPos, rightMargin - leftMargin, 20);
                graphics.DrawRectangle(Pens.LightGray, Rectangle.Round(rowRect));

                graphics.DrawString(stt.ToString(), smallFont, Brushes.Black, leftMargin + 5, yPos + 2);
                graphics.DrawString(item.ProductName ?? "", smallFont, Brushes.Black, leftMargin + 50, yPos + 2);
                graphics.DrawString(item.Quantity.ToString(), smallFont, Brushes.Black, leftMargin + 300, yPos + 2);
                graphics.DrawString($"{item.UnitPrice:N0}", smallFont, Brushes.Black, leftMargin + 350, yPos + 2);
                graphics.DrawString($"{item.LineTotal:N0}", smallFont, Brushes.Black, leftMargin + 450, yPos + 2);
                yPos += 20;
                stt++;
            }

            yPos += 10;
            graphics.DrawLine(Pens.Black, leftMargin, yPos, rightMargin, yPos);
            yPos += 10;

            // Total with background
            var totalRect = new RectangleF(leftMargin, yPos, rightMargin - leftMargin, 35);
            graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 240, 240)), totalRect);
            graphics.DrawRectangle(Pens.Black, Rectangle.Round(totalRect));

            var totalFont = new Font("Arial", 12, FontStyle.Bold);
            graphics.DrawString("TỔNG TIỀN:", totalFont, Brushes.Black, leftMargin + 10, yPos + 8);
            graphics.DrawString($"{_orderToPrint.TotalAmount:N0} VNĐ", totalFont, Brushes.Red, rightMargin - 200, yPos + 8);
            yPos += 40;

            // Footer
            graphics.DrawString("Cảm ơn quý khách!", font, Brushes.Black, leftMargin, yPos);
            yPos += font.GetHeight() + 5;

            graphics.DrawString("Hẹn gặp lại!", smallFont, Brushes.Black, leftMargin, yPos);
        }
    }
}

