using System;
using System.Drawing;
using System.Windows.Forms;
using WinFormsFashionShop.Presentation.Helpers;

namespace WinFormsFashionShop.Presentation.Forms
{
    /// <summary>
    /// Dialog for processing payment with change calculation.
    /// Single responsibility: only handles payment processing UI.
    /// </summary>
    public partial class PaymentDialog : Form
    {
        private readonly decimal _totalAmount;

        public decimal CustomerPayment { get; private set; }
        public decimal Change { get; private set; }
        public bool IsConfirmed { get; private set; }

        public PaymentDialog(decimal totalAmount)
        {
            _totalAmount = totalAmount;
            InitializeComponent();
            InitializeControls();
        }

        private void InitializeControls()
        {
            // Set total amount
            _lblTotalAmount!.Text = $"{_totalAmount:N0} VNĐ";
            _txtCustomerPayment!.Text = _totalAmount.ToString("N0");
            
            // Wire up event handlers
            _txtCustomerPayment.TextChanged += TxtCustomerPayment_TextChanged;
            _txtCustomerPayment.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    _btnConfirm?.PerformClick();
                }
            };
            
            _btnConfirm!.Click += BtnConfirm_Click;
            
            // Focus on payment input
            _txtCustomerPayment.Focus();
            _txtCustomerPayment.SelectAll();
        }


        /// <summary>
        /// Handles customer payment input change to calculate change.
        /// Single responsibility: only calculates and displays change.
        /// </summary>
        private void TxtCustomerPayment_TextChanged(object? sender, EventArgs e)
        {
            if (_txtCustomerPayment == null || _lblChange == null) return;

            if (decimal.TryParse(_txtCustomerPayment.Text.Replace(",", "").Replace(".", ""), out var customerPayment))
            {
                CustomerPayment = customerPayment;
                Change = customerPayment - _totalAmount;

                if (Change >= 0)
                {
                    _lblChange.Text = $"{Change:N0} VNĐ";
                    _lblChange.ForeColor = Color.Green;
                    if (_btnConfirm != null)
                        _btnConfirm.Enabled = true;
                }
                else
                {
                    _lblChange.Text = $"Thiếu {Math.Abs(Change):N0} VNĐ";
                    _lblChange.ForeColor = Color.Red;
                    if (_btnConfirm != null)
                        _btnConfirm.Enabled = false;
                }
            }
            else
            {
                _lblChange.Text = "0 VNĐ";
                _lblChange.ForeColor = Color.Green;
                Change = 0;
            }
        }

        /// <summary>
        /// Handles confirm button click.
        /// Single responsibility: only validates and confirms payment.
        /// </summary>
        private void BtnConfirm_Click(object? sender, EventArgs e)
        {
            if (_txtCustomerPayment == null) return;

            if (!decimal.TryParse(_txtCustomerPayment.Text.Replace(",", "").Replace(".", ""), out var customerPayment))
            {
                ErrorHandler.ShowWarning("Vui lòng nhập số tiền hợp lệ!");
                DialogResult = DialogResult.None;
                return;
            }

            if (customerPayment < _totalAmount)
            {
                ErrorHandler.ShowWarning($"Số tiền khách đưa ({customerPayment:N0} VNĐ) nhỏ hơn tổng tiền ({_totalAmount:N0} VNĐ)!");
                DialogResult = DialogResult.None;
                return;
            }

            // Set payment values
            CustomerPayment = customerPayment;
            Change = customerPayment - _totalAmount;
            IsConfirmed = true;
            
            // CRITICAL: Set DialogResult to OK and close dialog
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}

