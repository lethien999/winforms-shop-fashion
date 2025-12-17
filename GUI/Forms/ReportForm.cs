using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WinFormsFashionShop.Business.Services;
using WinFormsFashionShop.Data.Entities;
using WinFormsFashionShop.Presentation.Helpers;

namespace WinFormsFashionShop.Presentation.Forms
{
    public partial class ReportForm : Form
    {
        private readonly IReportService _reportService;
        private readonly IErrorHandler _errorHandler;

        public ReportForm(IReportService reportService, IErrorHandler errorHandler)
        {
            _reportService = reportService ?? throw new ArgumentNullException(nameof(reportService));
            _errorHandler = errorHandler ?? throw new ArgumentNullException(nameof(errorHandler));
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
                if (gridRevenue == null || dtpFrom == null || dtpTo == null)
                {
                    _errorHandler.ShowError("Các control chưa được khởi tạo!");
                    return;
                }

                var report = _reportService.GetRevenueReport(dtpFrom.Value.Date, dtpTo.Value.Date);
                if (report == null)
                {
                    _errorHandler.ShowWarning("Không thể tải báo cáo doanh thu. Dịch vụ trả về null.");
                    gridRevenue.DataSource = null;
                    if (lblRevenueTotal != null)
                        lblRevenueTotal.Text = "Tổng doanh thu: 0 VNĐ (0 đơn hàng)";
                    return;
                }
                
                var orders = report.Orders?.ToList() ?? new List<Order>();
                var orderList = orders.Where(o => o != null).Select(o => new
                {
                    Id = o.Id,
                    OrderCode = o.OrderCode ?? "",
                    OrderDate = o.OrderDate,
                    CustomerId = o.CustomerId,
                    TotalAmount = o.TotalAmount,
                    Status = o.Status,
                    SốLượngSP = o.Items?.Count ?? 0
                }).ToList();
                gridRevenue.DataSource = orderList;

                if (lblRevenueTotal != null)
                lblRevenueTotal.Text = $"Tổng doanh thu: {report.TotalRevenue:N0} VNĐ ({report.TotalOrders} đơn hàng)";
            }
            catch (Exception ex)
            {
                _errorHandler.ShowError($"Lỗi khi tải báo cáo doanh thu: {ex.Message}");
                if (gridRevenue != null)
                    gridRevenue.DataSource = null;
            }
        }

        private void LoadInventoryReport()
        {
            try
            {
                if (gridInventory == null || numLowStockThreshold == null)
                {
                    _errorHandler.ShowError("Các control chưa được khởi tạo!");
                    return;
                }

                var threshold = (int)numLowStockThreshold.Value;
                var report = _reportService.GetInventoryReport(threshold);
                if (report == null)
                {
                    _errorHandler.ShowWarning("Không thể tải báo cáo tồn kho. Dịch vụ trả về null.");
                    gridInventory.DataSource = null;
                    return;
                }
                
                var items = report.Items?.ToList() ?? new List<InventoryItem>();
                var itemList = items.Where(i => i != null).Select(i => new
                {
                    ProductCode = i.ProductCode ?? "",
                    ProductName = i.ProductName ?? "",
                    QuantityInStock = i.QuantityInStock,
                    IsLowStock = i.IsLowStock,
                    TrạngThái = i.IsLowStock ? "Tồn thấp" : "Bình thường"
                }).ToList();
                gridInventory.DataSource = itemList;

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
                _errorHandler.ShowError($"Lỗi khi tải báo cáo tồn kho: {ex.Message}");
                if (gridInventory != null)
                    gridInventory.DataSource = null;
            }
        }

        private void LoadCustomerReport()
        {
            try
            {
                if (gridCustomers == null || numTopCustomers == null)
                {
                    _errorHandler.ShowError("Các control chưa được khởi tạo!");
                    return;
                }

                var topN = (int)numTopCustomers.Value;
                var customers = _reportService.GetTopCustomers(topN);
                if (customers == null)
                {
                    _errorHandler.ShowWarning("Không thể tải báo cáo khách hàng. Dịch vụ trả về null.");
                    gridCustomers.DataSource = null;
                    return;
                }
                
                gridCustomers.DataSource = customers.Where(c => c != null).Select(c => new
                {
                    c.CustomerId,
                    c.CustomerName,
                    c.OrderCount,
                    c.TotalSpent
                }).ToList();
            }
            catch (Exception ex)
            {
                _errorHandler.ShowError($"Lỗi khi tải báo cáo khách hàng: {ex.Message}");
                if (gridCustomers != null)
                    gridCustomers.DataSource = null;
            }
        }
    }
}
