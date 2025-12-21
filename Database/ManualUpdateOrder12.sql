-- Manual update Order 12 (PayOSOrderCode 12853) to Paid status
-- Vì đã thanh toán trên PayOS Dashboard nhưng DNS không resolve được api.payos.vn

USE WinFormsFashionShopDb
GO

DECLARE @OrderId INT = 12;
DECLARE @PayOSOrderCode INT = 12853;
DECLARE @WebhookId NVARCHAR(255);
SET @WebhookId = 'MANUAL_UPDATE_' + CAST(@PayOSOrderCode AS NVARCHAR(50)) + '_' + 
    REPLACE(REPLACE(REPLACE(CONVERT(NVARCHAR(50), GETDATE(), 120), '-', ''), ' ', ''), ':', '');
DECLARE @Code NVARCHAR(10) = '00'; -- PayOS code "00" = thành công
DECLARE @Amount INT;
DECLARE @RawData NVARCHAR(MAX);

-- Lấy amount từ order
SELECT @Amount = TotalAmount FROM Orders WHERE Id = @OrderId;

IF @Amount IS NULL
BEGIN
    PRINT 'ERROR: Không tìm thấy Order ' + CAST(@OrderId AS NVARCHAR(50));
    RETURN;
END

PRINT '=== MANUAL UPDATE ORDER ' + CAST(@OrderId AS NVARCHAR(50)) + ' ===';
PRINT 'PayOSOrderCode: ' + CAST(@PayOSOrderCode AS NVARCHAR(50));
PRINT 'Amount: ' + CAST(@Amount AS NVARCHAR(50));
PRINT 'WebhookId: ' + @WebhookId;
PRINT '';

-- Kiểm tra trạng thái hiện tại
DECLARE @CurrentStatus NVARCHAR(50);
SELECT @CurrentStatus = Status FROM Orders WHERE Id = @OrderId;
PRINT 'Current Status: ' + ISNULL(@CurrentStatus, 'NULL');
PRINT '';

-- Tạo RawData JSON
SET @RawData = N'{"source":"manual_update","reason":"dns_resolution_failed","orderId":' + CAST(@OrderId AS NVARCHAR(50)) + '}';

-- Gọi stored procedure để update
EXEC ProcessPayOSWebhook
    @WebhookId = @WebhookId,
    @PayOSOrderCode = @PayOSOrderCode,
    @Code = @Code,
    @Amount = @Amount,
    @Reference = NULL,
    @PaymentLinkId = NULL,
    @RawData = @RawData,
    @IPAddress = 'ManualUpdate',
    @UserAgent = 'SQLScript';

PRINT '=== RESULT ===';
PRINT 'Stored procedure executed successfully';
PRINT '';

-- Verify
SELECT 
    Id AS OrderId,
    PayOSOrderCode,
    Status,
    PaidAt,
    TransactionId
FROM Orders 
WHERE Id = @OrderId;

PRINT '';
PRINT '=== PAYMENT WEBHOOKS ===';
SELECT TOP 5
    WebhookId,
    PayOSOrderCode,
    Code,
    Amount,
    ProcessedAt,
    ProcessedSuccessfully,
    ErrorMessage
FROM PaymentWebhooks
WHERE PayOSOrderCode = @PayOSOrderCode
ORDER BY ProcessedAt DESC;

PRINT '';
PRINT '=== AUDIT LOG ===';
SELECT TOP 5
    Id AS LogId,
    OrderId,
    Action,
    OldStatus,
    NewStatus,
    PerformedAt AS LoggedAt
FROM PaymentAuditLog
WHERE OrderId = @OrderId
ORDER BY PerformedAt DESC;
