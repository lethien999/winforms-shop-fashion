using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Windows.Forms;
using QRCoder;
using WinFormsFashionShop.Presentation.Helpers;
using WinFormsFashionShop.Presentation.Services;
using WinFormsFashionShop.Presentation.Models;

namespace WinFormsFashionShop.Presentation.Forms
{
    /// <summary>
    /// Dialog for displaying QR code payment via PayOS/VietQR.
    /// Single responsibility: only displays QR code payment UI.
    /// </summary>
    public partial class QRCodePaymentDialog : Form
    {
        private readonly PaymentApiClientWithRetry _apiClient;
        private readonly int _orderId;
        private readonly decimal _totalAmount;
        private readonly string _orderDescription;
        private PaymentData? _paymentLinkData;
        private System.Windows.Forms.Timer? _paymentCheckTimer;
        private bool _isPaymentConfirmed = false;

        public bool IsPaymentConfirmed => _isPaymentConfirmed;
        public PaymentData? PaymentData => _paymentLinkData;

        public QRCodePaymentDialog(int orderId, decimal totalAmount, string orderDescription)
        {
            _orderId = orderId;
            _totalAmount = totalAmount;
            _orderDescription = orderDescription;
            _apiClient = new PaymentApiClientWithRetry(ApiConfig.BaseUrl);
            InitializeComponent();
            InitializeControls();
            LoadPaymentQRCode();
        }

        /// <summary>
        /// Initializes event handlers and sets initial values.
        /// Single responsibility: only wires up event handlers and sets initial data.
        /// </summary>
        private void InitializeControls()
        {
            // Set initial order info
            _lblOrderCode!.Text = $"📋 Mã đơn: {_orderId}";
            _lblAmount!.Text = $"💰 Số tiền: {_totalAmount:N0} VNĐ";
            lblDescription!.Text = $"📝 {_orderDescription}";

            // Wire up event handlers
            _btnCheckPayment!.Click += BtnCheckPayment_Click;
            _btnCancel!.Click += BtnCancel_Click;
        }

        /// <summary>
        /// Handles cancel button click.
        /// Single responsibility: only closes the dialog.
        /// </summary>
        private void BtnCancel_Click(object? sender, EventArgs e)
        {
            StopPaymentCheckTimer();
            DialogResult = DialogResult.Cancel;
            Close();
        }


        /// <summary>
        /// Loads payment QR code from Backend API.
        /// Single responsibility: only loads QR code.
        /// </summary>
        private async void LoadPaymentQRCode()
        {
            try
            {
                if (_lblStatus == null) return;
                _lblStatus.Text = "Đang tạo link thanh toán...";
                _lblStatus.ForeColor = Color.Blue;

                // PayOS requires amount in VND (as integer, not decimal)
                // Example: 1,220 VNĐ = 1220 (not 1.22)
                // Validate amount >= 0.01 VND (minimum 1 VND)
                if (_totalAmount < 0.01m)
                {
                    throw new ArgumentException("Số tiền phải lớn hơn hoặc bằng 0.01 VNĐ");
                }

                // Convert decimal VND to integer (e.g., 1220.50 -> 1221, 1220.00 -> 1220)
                int amountInVND = (int)Math.Round(_totalAmount, MidpointRounding.AwayFromZero);
                
                // Ensure minimum amount is 1 VND (PayOS requirement)
                if (amountInVND < 1)
                {
                    amountInVND = 1;
                }

                // Tạo payment link qua Backend API
                var request = new CreatePaymentRequest
                {
                    OrderId = _orderId,
                    Amount = amountInVND,
                    Description = _orderDescription
                };

                var response = await _apiClient.CreatePaymentAsync(request);
                
                if (!response.Success || response.Data == null)
                {
                    throw new InvalidOperationException(response.Message ?? "Không thể tạo payment link");
                }

                _paymentLinkData = response.Data;

                // Ưu tiên sử dụng QR code chuẩn VietQR từ PayOS (có thể quét bằng app ngân hàng/Momo)
                if (!string.IsNullOrWhiteSpace(_paymentLinkData.QrCode))
                {
                    // Sử dụng QR code chuẩn VietQR từ PayOS
                    DisplayQRCode(_paymentLinkData.QrCode);
                    if (_lblStatus != null)
                    {
                        _lblStatus.Text = "📱 Quét mã QR bằng app ngân hàng hoặc Momo để thanh toán";
                        _lblStatus.ForeColor = Color.Green;
                    }
                    if (_btnCheckPayment != null)
                        _btnCheckPayment.Enabled = true;
                    
                    // Start auto-check timer (check every 2-3 seconds)
                    StartPaymentCheckTimer();
                    
                    // Thêm link web làm phương án dự phòng (nếu có checkoutUrl)
                    if (!string.IsNullOrWhiteSpace(_paymentLinkData.CheckoutUrl))
                    {
                        var lblOpenLink = new LinkLabel
                        {
                            Text = "🌐 Hoặc click để mở link thanh toán trên web",
                            Font = new Font("Arial", 9),
                            Dock = DockStyle.Top,
                            Height = 25,
                            TextAlign = ContentAlignment.MiddleCenter,
                            AutoSize = false,
                            LinkColor = Color.FromArgb(70, 130, 180),
                            ActiveLinkColor = Color.Blue
                        };
                        lblOpenLink.LinkClicked += (s, e) =>
                        {
                            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                            {
                                FileName = _paymentLinkData.CheckoutUrl,
                                UseShellExecute = true
                            });
                        };
                        pnlContent.Controls.Add(lblOpenLink);
                    }
                }
                else if (!string.IsNullOrWhiteSpace(_paymentLinkData.CheckoutUrl))
                {
                    // Fallback: Tạo QR code từ checkout URL (chỉ mở web, không quét trực tiếp được)
                    DisplayQRCodeFromUrl(_paymentLinkData.CheckoutUrl);
                    if (_lblStatus != null)
                    {
                        _lblStatus.Text = "⚠️ Quét mã QR sẽ mở link thanh toán trên web";
                        _lblStatus.ForeColor = Color.Orange;
                    }
                    if (_btnCheckPayment != null)
                        _btnCheckPayment.Enabled = true;

                    // Add link to open in browser
                    var lblOpenLink = new LinkLabel
                    {
                        Text = "🌐 Click để mở link thanh toán",
                        Font = new Font("Arial", 9),
                        Dock = DockStyle.Top,
                        Height = 25,
                        TextAlign = ContentAlignment.MiddleCenter,
                        AutoSize = false,
                        LinkColor = Color.FromArgb(70, 130, 180),
                        ActiveLinkColor = Color.Blue
                    };
                    lblOpenLink.LinkClicked += (s, e) =>
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = _paymentLinkData.CheckoutUrl,
                            UseShellExecute = true
                        });
                    };
                    pnlContent.Controls.Add(lblOpenLink);

                    // Start auto-check timer (check every 2-3 seconds)
                    StartPaymentCheckTimer();
                }
                else
                {
                    if (_lblStatus != null)
                    {
                        _lblStatus.Text = "❌ Không thể tạo mã QR. Vui lòng thử lại!";
                        _lblStatus.ForeColor = Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowError($"Không thể tạo mã QR thanh toán: {ex.Message}");
                if (_lblStatus != null)
                {
                    _lblStatus.Text = "Lỗi: " + ex.Message;
                    _lblStatus.ForeColor = Color.Red;
                }
            }
        }

        /// <summary>
        /// Generates and displays QR code from payment URL.
        /// Single responsibility: only generates and displays QR code from URL.
        /// </summary>
        private void DisplayQRCodeFromUrl(string paymentUrl)
        {
            try
            {
                using (var qrGenerator = new QRCodeGenerator())
                {
                    var qrCodeData = qrGenerator.CreateQrCode(paymentUrl, QRCodeGenerator.ECCLevel.Q);
                    using (var qrCode = new QRCode(qrCodeData))
                    {
                        var qrCodeImage = qrCode.GetGraphic(20);
                        _picQRCode!.Image = qrCodeImage;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowError($"Không thể tạo mã QR: {ex.Message}");
            }
        }

        /// <summary>
        /// Displays QR code from PayOS data (can be base64 image or EMV QR data string).
        /// Single responsibility: only displays QR code from PayOS data.
        /// </summary>
        private void DisplayQRCode(string qrCodeData)
        {
            try
            {
                // Kiểm tra xem đây có phải là base64 image không
                if (IsBase64Image(qrCodeData))
                {
                    // Xử lý base64 image
                    var base64String = qrCodeData;
                    if (base64String.Contains(","))
                    {
                        base64String = base64String.Split(',')[1];
                    }

                    var imageBytes = Convert.FromBase64String(base64String);
                    using (var ms = new System.IO.MemoryStream(imageBytes))
                    {
                        var image = Image.FromStream(ms);
                        // Dispose old image if exists
                        if (_picQRCode!.Image != null)
                        {
                            var oldImage = _picQRCode.Image;
                            _picQRCode.Image = null;
                            oldImage.Dispose();
                        }
                        _picQRCode.Image = (Image)image.Clone();
                    }
                }
                else
                {
                    // Nếu không phải base64 image, coi như là EMV QR data string và generate QR code
                    // Đây là chuẩn VietQR có thể quét được bằng app ngân hàng/Momo
                    using (var qrGenerator = new QRCodeGenerator())
                    {
                        var qrCodeDataObj = qrGenerator.CreateQrCode(qrCodeData, QRCodeGenerator.ECCLevel.Q);
                        using (var qrCode = new QRCode(qrCodeDataObj))
                        {
                            var qrCodeImage = qrCode.GetGraphic(20);
                            _picQRCode!.Image = qrCodeImage;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Thử fallback: generate QR code từ string data
                try
                {
                    using (var qrGenerator = new QRCodeGenerator())
                    {
                        var qrCodeDataObj = qrGenerator.CreateQrCode(qrCodeData, QRCodeGenerator.ECCLevel.Q);
                        using (var qrCode = new QRCode(qrCodeDataObj))
                        {
                            var qrCodeImage = qrCode.GetGraphic(20);
                            _picQRCode!.Image = qrCodeImage;
                        }
                    }
                }
                catch
                {
                    ErrorHandler.ShowError($"Không thể hiển thị mã QR: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Checks if a string is a valid base64 image.
        /// Single responsibility: only validates base64 image string.
        /// </summary>
        private bool IsBase64Image(string data)
        {
            try
            {
                // Kiểm tra nếu có data URI prefix (data:image/...)
                if (data.Contains("data:image/"))
                    return true;

                // Thử decode base64
                var cleanData = data.Contains(",") ? data.Split(',')[1] : data;
                var imageBytes = Convert.FromBase64String(cleanData);
                
                // Kiểm tra magic bytes của các format image phổ biến
                if (imageBytes.Length < 4) return false;
                
                // PNG: 89 50 4E 47
                if (imageBytes[0] == 0x89 && imageBytes[1] == 0x50 && 
                    imageBytes[2] == 0x4E && imageBytes[3] == 0x47)
                    return true;
                
                // JPEG: FF D8 FF
                if (imageBytes[0] == 0xFF && imageBytes[1] == 0xD8 && imageBytes[2] == 0xFF)
                    return true;
                
                // GIF: 47 49 46 38
                if (imageBytes[0] == 0x47 && imageBytes[1] == 0x49 && 
                    imageBytes[2] == 0x46 && imageBytes[3] == 0x38)
                    return true;
                
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Starts automatic payment status checking timer.
        /// Single responsibility: only starts timer.
        /// </summary>
        private void StartPaymentCheckTimer()
        {
            // Check every 2-3 seconds từ Backend API (nhanh hơn vì không gọi PayOS trực tiếp)
            _paymentCheckTimer = new System.Windows.Forms.Timer
            {
                Interval = 3000 // Check every 3 seconds
            };
            _paymentCheckTimer.Tick += async (s, e) => await CheckPaymentStatusAsync();
            
            // Start timer after 3 seconds delay để webhook có thời gian xử lý
            var delayTimer = new System.Windows.Forms.Timer
            {
                Interval = 3000 // 3 seconds delay before first check
            };
            delayTimer.Tick += (s, e) =>
            {
                delayTimer.Stop();
                delayTimer.Dispose();
                _paymentCheckTimer?.Start();
            };
            delayTimer.Start();
        }

        /// <summary>
        /// Stops payment status checking timer.
        /// Single responsibility: only stops timer.
        /// </summary>
        private void StopPaymentCheckTimer()
        {
            _paymentCheckTimer?.Stop();
            _paymentCheckTimer?.Dispose();
        }

        /// <summary>
        /// Handles check payment button click.
        /// Single responsibility: only triggers payment check.
        /// </summary>
        private async void BtnCheckPayment_Click(object? sender, EventArgs e)
        {
            if (_btnCheckPayment == null || _lblStatus == null) return;

            // Disable button temporarily to prevent multiple clicks
            _btnCheckPayment.Enabled = false;
            _btnCheckPayment.Text = "⏳ Đang kiểm tra...";
            
            try
            {
                await CheckPaymentStatusAsync(true); // Pass true to indicate manual check
            }
            finally
            {
                // Re-enable button if payment not confirmed
                if (!_isPaymentConfirmed && _btnCheckPayment != null)
                {
                    _btnCheckPayment.Enabled = true;
                    _btnCheckPayment.Text = "🔄 Kiểm tra thanh toán";
                }
            }
        }

        /// <summary>
        /// Checks payment status from Backend API (không gọi PayOS trực tiếp).
        /// Single responsibility: only checks payment status.
        /// </summary>
        /// <param name="isManualCheck">True if user manually clicked check button, false if auto-check from timer</param>
        private async Task CheckPaymentStatusAsync(bool isManualCheck = false)
        {
            if (_paymentLinkData == null)
            {
                if (_lblStatus != null && isManualCheck)
                {
                    _lblStatus.Text = "❌ Không có thông tin thanh toán để kiểm tra";
                    _lblStatus.ForeColor = Color.Red;
                }
                return;
            }

            try
            {
                if (_lblStatus != null && isManualCheck)
                {
                    _lblStatus.Text = "⏳ Đang kiểm tra trạng thái thanh toán...";
                    _lblStatus.ForeColor = Color.Blue;
                }

                // Gọi Backend API để kiểm tra trạng thái từ database (không gọi PayOS trực tiếp)
                var statusResponse = await _apiClient.GetPaymentStatusAsync(_orderId);

                if (!statusResponse.Success || statusResponse.Data == null)
                {
                    if (isManualCheck && _lblStatus != null)
                    {
                        _lblStatus.Text = $"❌ Lỗi: {statusResponse.Message ?? "Không thể lấy trạng thái"}";
                        _lblStatus.ForeColor = Color.Red;
                    }
                    return;
                }

                var status = statusResponse.Data.Status?.ToUpper() ?? "";
                
                // Kiểm tra trạng thái từ database
                if (status == "PAID")
                {
                    _isPaymentConfirmed = true;
                    if (_lblStatus != null)
                    {
                        _lblStatus.Text = "✓ Thanh toán thành công! Đang in hóa đơn...";
                        _lblStatus.ForeColor = Color.Green;
                    }
                    StopPaymentCheckTimer();
                    if (_btnCheckPayment != null)
                    {
                        _btnCheckPayment.Enabled = false;
                        _btnCheckPayment.Text = "✓ Đã thanh toán";
                    }

                    // Tự động in hóa đơn khi thanh toán thành công
                    PrintInvoiceAfterPayment();

                    // Auto close after 3 seconds (để có thời gian in)
                    var closeTimer = new System.Windows.Forms.Timer { Interval = 3000 };
                    closeTimer.Tick += (s, e) => 
                    { 
                        closeTimer.Stop();
                        closeTimer.Dispose();
                        DialogResult = DialogResult.OK; 
                        Close(); 
                    };
                    closeTimer.Start();
                }
                else if (status == "CANCELLED")
                {
                    if (_lblStatus != null)
                    {
                        _lblStatus.Text = "❌ Thanh toán đã bị hủy";
                        _lblStatus.ForeColor = Color.Red;
                    }
                    StopPaymentCheckTimer();
                    if (_btnCheckPayment != null)
                        _btnCheckPayment.Enabled = false;
                }
                else
                {
                    // Only update status message for manual checks or if payment is still pending
                    // Don't spam status updates for auto-checks
                    if (_lblStatus != null && isManualCheck)
                    {
                        _lblStatus.Text = "⏳ Đang chờ thanh toán... (Chưa nhận được thanh toán)";
                        _lblStatus.ForeColor = Color.Blue;
                    }
                }
            }
            catch (Exception ex)
            {
                // Kiểm tra xem có phải lỗi mạng không
                bool isNetworkError = ex.Message.Contains("kết nối") || 
                                     ex.Message.Contains("network") || 
                                     ex.Message.Contains("timeout") ||
                                     ex.Message.Contains("refused");

                if (isNetworkError && !isManualCheck)
                {
                    // Mất mạng: hiển thị thông báo nhưng vẫn tiếp tục retry
                    if (_lblStatus != null)
                    {
                        _lblStatus.Text = "⚠️ Mất kết nối mạng. Đang thử lại...";
                        _lblStatus.ForeColor = Color.Orange;
                    }
                    // Timer sẽ tự động retry ở lần check tiếp theo
                }
                else if (isManualCheck)
                {
                    // Show error to user when they manually check
                    if (_lblStatus != null)
                    {
                        _lblStatus.Text = $"❌ Lỗi kiểm tra: {ex.Message}";
                        _lblStatus.ForeColor = Color.Red;
                    }
                }
                else
                {
                    // Silent log for auto-check errors (don't disturb user)
                    System.Diagnostics.Debug.WriteLine($"Payment check error (auto-check): {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Tự động in hóa đơn sau khi thanh toán thành công
        /// </summary>
        private void PrintInvoiceAfterPayment()
        {
            try
            {
                // Lấy thông tin đơn hàng từ OrderService
                var services = WinFormsFashionShop.Business.Composition.ServicesComposition.Create();
                var order = services.OrderService.GetOrderById(_orderId);
                
                if (order != null)
                {
                    // In tự động (không hiển thị dialog)
                    var printed = WinFormsFashionShop.Presentation.Helpers.PrintHelper.PrintOrderAuto(order);
                    
                    if (!printed && _lblStatus != null)
                    {
                        _lblStatus.Text = "✓ Thanh toán thành công! (In hóa đơn thất bại)";
                    }
                }
            }
            catch (Exception ex)
            {
                // Log lỗi nhưng không ảnh hưởng đến flow thanh toán
                System.Diagnostics.Debug.WriteLine($"Lỗi in hóa đơn tự động: {ex.Message}");
                if (_lblStatus != null)
                {
                    _lblStatus.Text = "✓ Thanh toán thành công! (Không thể in tự động)";
                }
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            StopPaymentCheckTimer();
            _apiClient?.Dispose();
            base.OnFormClosing(e);
        }

    }
}

