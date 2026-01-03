IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'ProcessPayOSWebhook')
    DROP PROCEDURE ProcessPayOSWebhook;
GO

SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

CREATE PROCEDURE ProcessPayOSWebhook
    @WebhookId NVARCHAR(100),
    @PayOSOrderCode INT,
    @Code NVARCHAR(10),
    @Amount INT,
    @Reference NVARCHAR(100) = NULL,
    @PaymentLinkId NVARCHAR(100) = NULL,
    @RawData NVARCHAR(MAX) = NULL,
    @IPAddress NVARCHAR(50) = NULL,
    @UserAgent NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @OrderId INT = NULL;
    DECLARE @OldStatus NVARCHAR(20) = NULL;
    DECLARE @NewStatus NVARCHAR(20) = NULL;
    DECLARE @OldAmount DECIMAL(18,2) = NULL;
    DECLARE @TransactionId NVARCHAR(100) = NULL;
    DECLARE @ErrorMessage NVARCHAR(500) = NULL;
    
    BEGIN TRANSACTION;
    
    BEGIN TRY
        IF @Code != '00'
        BEGIN
            COMMIT TRANSACTION;
            SELECT 'Failed' AS Result, 'Code khong phai 00' AS Message, NULL AS OrderId, NULL AS CurrentStatus;
            RETURN -1;
        END;
        
        SELECT @OrderId = Id, @OldStatus = Status, @OldAmount = TotalAmount
        FROM Orders WHERE PayOSOrderCode = @PayOSOrderCode;
        
        IF @OrderId IS NULL
        BEGIN
            COMMIT TRANSACTION;
            SELECT 'Failed' AS Result, 'Khong tim thay don hang' AS Message, NULL AS OrderId, NULL AS CurrentStatus;
            RETURN -2;
        END;
        
        IF @Amount != CAST(@OldAmount AS INT)
        BEGIN
            COMMIT TRANSACTION;
            SELECT 'Failed' AS Result, 'So tien khong khop' AS Message, @OrderId AS OrderId, @OldStatus AS CurrentStatus;
            RETURN -3;
        END;
        
        IF @OldStatus = 'Paid' AND EXISTS (SELECT 1 FROM Orders WHERE Id = @OrderId AND PaidAt IS NOT NULL)
        BEGIN
            COMMIT TRANSACTION;
            SELECT 'Success' AS Result, 'Don hang da thanh toan truoc do' AS Message, @OrderId AS OrderId, @OldStatus AS CurrentStatus;
            RETURN 0;
        END;
        
        SET @NewStatus = 'Paid';
        SET @TransactionId = COALESCE(@Reference, @PaymentLinkId, @WebhookId);
        
        UPDATE Orders SET 
            Status = @NewStatus, 
            PaidAt = GETDATE(), 
            TransactionId = @TransactionId 
        WHERE Id = @OrderId;
        
        UPDATE PaymentTransactions SET 
            Status = 'PAID', 
            PaidAt = GETDATE(), 
            Reference = @Reference, 
            TransactionDateTime = CONVERT(NVARCHAR(50), GETDATE(), 120), 
            WebhookId = @WebhookId, 
            RawData = COALESCE(@RawData, RawData), 
            UpdatedAt = GETDATE() 
        WHERE PayOSOrderCode = @PayOSOrderCode;
        
        COMMIT TRANSACTION;
        SELECT 'Success' AS Result, 'Thanh toan thanh cong' AS Message, @OrderId AS OrderId, @NewStatus AS CurrentStatus;
        RETURN 0;
        
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        SET @ErrorMessage = ERROR_MESSAGE();
        SELECT 'Error' AS Result, @ErrorMessage AS Message, @OrderId AS OrderId, @OldStatus AS CurrentStatus;
        RETURN -99;
    END CATCH;
END;
GO
