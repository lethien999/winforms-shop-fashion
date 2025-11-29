using System;
using System.Linq;
using System.Windows.Forms;
using WinFormsFashionShop.Business.Services;

namespace WinFormsFashionShop.Presentation.Forms
{
    public partial class ReportForm : Form
    {
        private readonly IReportService _reportService;

        public ReportForm(IReportService reportService)
        {
            _reportService = reportService ?? throw new ArgumentNullException(nameof(reportService));
            InitializeComponent();
            InitializeControls();
            LoadDefaultData();
        }

        private void InitializeControls()
        {
            // Set default values
            dtpFrom.Value = DateTime.Now.AddDays(-30);
            dtpTo.Value = DateTime.Now;
            
            // Wire up event handlers
            btnLoadRevenue.Click += (s, e) => LoadRevenueReport();
            btnLoadInventory.Click += (s, e) => LoadInventoryReport();
            btnLoadCustomers.Click += (s, e) => LoadCustomerReport();
        }

        private void LoadDefaultData()
        {
            LoadInventoryReport();
            LoadCustomerReport();
        }

        private void LoadRevenueReport()
        {
            try
            {
                var report = _reportService.GetRevenueReport(dtpFrom.Value.Date, dtpTo.Value.Date);
                
                gridRevenue.DataSource = report.Orders.Select(o => new
                {
                    o.Id,
                    o.OrderCode,
                    o.OrderDate,
                    o.CustomerId,
                    o.TotalAmount,
                    o.Status,
                    SốLượngSP = o.Items?.Count ?? 0
                }).ToList();

                lblRevenueTotal.Text = $"Tổng doanh thu: {report.TotalRevenue:N0} VNĐ ({report.TotalOrders} đơn hàng)";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải báo cáo doanh thu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadInventoryReport()
        {
            try
            {
                var threshold = (int)numLowStockThreshold.Value;
                var report = _reportService.GetInventoryReport(threshold);
                
                gridInventory.DataSource = report.Items.Select(i => new
                {
                    i.ProductCode,
                    i.ProductName,
                    i.QuantityInStock,
                    i.IsLowStock,
                    TrạngThái = i.IsLowStock ? "Tồn thấp" : "Bình thường"
                }).ToList();

                // Highlight low stock rows
                gridInventory.CellFormatting += (s, e) =>
                {
                    if (e.ColumnIndex == gridInventory.Columns["TrạngThái"]?.Index && 
                        e.RowIndex >= 0 && 
                        e.RowIndex < gridInventory.Rows.Count &&
                        gridInventory.Columns["IsLowStock"] != null &&
                        e.CellStyle != null)
                    {
                        var cellValue = gridInventory.Rows[e.RowIndex].Cells["IsLowStock"]?.Value;
                        if (cellValue != null && Convert.ToBoolean(cellValue))
                        {
                            e.CellStyle.BackColor = System.Drawing.Color.LightCoral;
                            e.CellStyle.ForeColor = System.Drawing.Color.Black;
                        }
                    }
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải báo cáo tồn kho: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCustomerReport()
        {
            try
            {
                var topN = (int)numTopCustomers.Value;
                var customers = _reportService.GetTopCustomers(topN);
                
                gridCustomers.DataSource = customers.Select(c => new
                {
                    c.CustomerId,
                    c.CustomerName,
                    c.OrderCount,
                    c.TotalSpent
                }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải báo cáo khách hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
