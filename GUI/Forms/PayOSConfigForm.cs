using System;
using System.Windows.Forms;
using WinFormsFashionShop.Presentation.Helpers;

namespace WinFormsFashionShop.Presentation.Forms
{
    /// <summary>
    /// Form for configuring PayOS API credentials.
    /// Single responsibility: only handles PayOS configuration UI.
    /// </summary>
    public partial class PayOSConfigForm : Form
    {

        public PayOSConfigForm()
        {
            InitializeComponent();
            InitializeControls();
            LoadCurrentConfig();
        }

        /// <summary>
        /// Initializes event handlers for controls.
        /// Single responsibility: only wires up event handlers.
        /// </summary>
        private void InitializeControls()
        {
            _btnTest!.Click += BtnTest_Click;
            _btnSave!.Click += BtnSave_Click;
            _txtClientId!.Focus();
        }

        /// <summary>
        /// Loads current PayOS configuration.
        /// Single responsibility: only loads configuration.
        /// </summary>
        private void LoadCurrentConfig()
        {
            _txtClientId!.Text = PayOSConfig.ClientId;
            _txtApiKey!.Text = PayOSConfig.ApiKey;
            _txtChecksumKey!.Text = PayOSConfig.ChecksumKey;
        }

        /// <summary>
        /// Handles test connection button click.
        /// Single responsibility: only tests PayOS connection.
        /// </summary>
        private async void BtnTest_Click(object? sender, EventArgs e)
        {
            if (!ValidateInputs())
                return;

            try
            {
                // Temporarily set config for testing
                var originalClientId = PayOSConfig.ClientId;
                var originalApiKey = PayOSConfig.ApiKey;
                var originalChecksumKey = PayOSConfig.ChecksumKey;

                PayOSConfig.ClientId = _txtClientId!.Text.Trim();
                PayOSConfig.ApiKey = _txtApiKey!.Text.Trim();
                PayOSConfig.ChecksumKey = _txtChecksumKey!.Text.Trim();

                // Test by creating a small payment link with unique orderCode
                var payOSService = new Services.PayOSService();
                // Generate unique orderCode based on timestamp to avoid "order already exists" error
                var testOrderId = (int)(DateTimeOffset.UtcNow.ToUnixTimeSeconds() % int.MaxValue);
                var testItems = new System.Collections.Generic.List<Net.payOS.Types.ItemData>
                {
                    new Net.payOS.Types.ItemData("Test", 1, 1000)
                };

                var result = await payOSService.CreatePaymentLinkAsync(
                    orderId: testOrderId,
                    amount: 1000,
                    description: "Test connection",
                    items: testItems
                );

                // Cancel the test payment link immediately after successful creation
                if (result != null && !string.IsNullOrEmpty(result.checkoutUrl))
                {
                    try
                    {
                        await payOSService.CancelPaymentLinkAsync(testOrderId, "Test connection - auto cancel");
                    }
                    catch { /* Ignore cancel errors */ }
                }

                // Restore original config
                PayOSConfig.ClientId = originalClientId;
                PayOSConfig.ApiKey = originalApiKey;
                PayOSConfig.ChecksumKey = originalChecksumKey;

                ErrorHandler.ShowSuccess("Kết nối PayOS thành công!");
            }
            catch (Exception ex)
            {
                ErrorHandler.ShowError($"Không thể kết nối PayOS: {ex.Message}\n\nVui lòng kiểm tra lại thông tin!");
            }
        }

        /// <summary>
        /// Handles save button click.
        /// Single responsibility: only saves configuration.
        /// </summary>
        private void BtnSave_Click(object? sender, EventArgs e)
        {
            if (!ValidateInputs())
            {
                DialogResult = DialogResult.None;
                return;
            }

            PayOSConfig.ClientId = _txtClientId!.Text.Trim();
            PayOSConfig.ApiKey = _txtApiKey!.Text.Trim();
            PayOSConfig.ChecksumKey = _txtChecksumKey!.Text.Trim();

            // Reload to ensure values are saved
            Helpers.PayOSConfig.Reload();

            ErrorHandler.ShowSuccess("Đã lưu cấu hình PayOS thành công!");
        }

        /// <summary>
        /// Validates input fields.
        /// Single responsibility: only validates inputs.
        /// </summary>
        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(_txtClientId?.Text))
            {
                ErrorHandler.ShowWarning("Vui lòng nhập Client ID!");
                _txtClientId?.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(_txtApiKey?.Text))
            {
                ErrorHandler.ShowWarning("Vui lòng nhập API Key!");
                _txtApiKey?.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(_txtChecksumKey?.Text))
            {
                ErrorHandler.ShowWarning("Vui lòng nhập Checksum Key!");
                _txtChecksumKey?.Focus();
                return false;
            }

            return true;
        }
    }
}

