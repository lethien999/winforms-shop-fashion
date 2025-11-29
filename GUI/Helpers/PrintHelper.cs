using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using WinFormsFashionShop.DTO;

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
        /// </summary>
        public static void PrintOrder(OrderDTO order)
        {
            if (order == null) throw new ArgumentNullException(nameof(order));

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
            }
        }

        /// <summary>
        /// Handles the PrintPage event to draw order content.
        /// Single responsibility: only draws order content on print page.
        /// </summary>
        private static void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (_orderToPrint == null) return;

            var graphics = e.Graphics;
            var font = new Font("Arial", 12);
            var boldFont = new Font("Arial", 12, FontStyle.Bold);
            var titleFont = new Font("Arial", 16, FontStyle.Bold);
            var smallFont = new Font("Arial", 10);

            float yPos = 0;
            float leftMargin = e.MarginBounds.Left;
            float topMargin = e.MarginBounds.Top;
            float rightMargin = e.MarginBounds.Right;

            // Header
            graphics.DrawString("HÓA ĐƠN BÁN HÀNG", titleFont, Brushes.Black, leftMargin, yPos);
            yPos += titleFont.GetHeight() + 10;

            graphics.DrawString("WinForms Fashion Shop", font, Brushes.Black, leftMargin, yPos);
            yPos += font.GetHeight() + 5;

            graphics.DrawString("Địa chỉ: 123 Đường ABC, Quận XYZ, TP.HCM", smallFont, Brushes.Black, leftMargin, yPos);
            yPos += smallFont.GetHeight() + 5;

            graphics.DrawString($"ĐT: 0123 456 789", smallFont, Brushes.Black, leftMargin, yPos);
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

            // Items header
            graphics.DrawString("STT", smallFont, Brushes.Black, leftMargin, yPos);
            graphics.DrawString("Tên sản phẩm", smallFont, Brushes.Black, leftMargin + 50, yPos);
            graphics.DrawString("SL", smallFont, Brushes.Black, leftMargin + 300, yPos);
            graphics.DrawString("Đơn giá", smallFont, Brushes.Black, leftMargin + 350, yPos);
            graphics.DrawString("Thành tiền", smallFont, Brushes.Black, leftMargin + 450, yPos);
            yPos += smallFont.GetHeight() + 5;

            graphics.DrawLine(Pens.Black, leftMargin, yPos, rightMargin, yPos);
            yPos += 10;

            // Items
            int stt = 1;
            foreach (var item in _orderToPrint.Items ?? Enumerable.Empty<OrderItemDTO>())
            {
                if (yPos > e.MarginBounds.Bottom - 100)
                {
                    e.HasMorePages = true;
                    return;
                }

                graphics.DrawString(stt.ToString(), smallFont, Brushes.Black, leftMargin, yPos);
                graphics.DrawString(item.ProductName ?? "", smallFont, Brushes.Black, leftMargin + 50, yPos);
                graphics.DrawString(item.Quantity.ToString(), smallFont, Brushes.Black, leftMargin + 300, yPos);
                graphics.DrawString($"{item.UnitPrice:N0}", smallFont, Brushes.Black, leftMargin + 350, yPos);
                graphics.DrawString($"{item.LineTotal:N0}", smallFont, Brushes.Black, leftMargin + 450, yPos);
                yPos += smallFont.GetHeight() + 5;
                stt++;
            }

            yPos += 10;
            graphics.DrawLine(Pens.Black, leftMargin, yPos, rightMargin, yPos);
            yPos += 10;

            // Total
            graphics.DrawString("TỔNG TIỀN:", boldFont, Brushes.Black, leftMargin + 350, yPos);
            graphics.DrawString($"{_orderToPrint.TotalAmount:N0} VNĐ", boldFont, Brushes.Black, leftMargin + 450, yPos);
            yPos += boldFont.GetHeight() + 20;

            // Footer
            graphics.DrawString("Cảm ơn quý khách!", font, Brushes.Black, leftMargin, yPos);
            yPos += font.GetHeight() + 5;

            graphics.DrawString("Hẹn gặp lại!", smallFont, Brushes.Black, leftMargin, yPos);
        }
    }
}

