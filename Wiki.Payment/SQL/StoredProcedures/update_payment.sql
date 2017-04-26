USE [Wiki]
GO
/****** Object:  StoredProcedure [dbo].[update_payment]    Script Date: 09.02.2017 11:45:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE  [dbo].[update_payment] 
	@Id int,
	@Type tinyint = NULL,
	@OperatorId int = NULL,
	@ClientId int = NULL,
	@Price numeric(19,2) = NULL,
	@DocCode int = NULL

AS
BEGIN
	SET NOCOUNT ON;

UPDATE dbo.Payment
    SET [Type]=ISNULL(@Type,[Type]), 
        OperatorId=ISNULL(@OperatorId,OperatorId), 
        ClientId=ISNULL(@ClientId, ClientId),
		Price=ISNULL(@Price,Price),
		DocCode=ISNULL(@DocCode,DocCode),
		[Time] = GETDATE()
    WHERE Id=@Id
	SELECT Id,[Time] FROM Payment WHERE id = SCOPE_IDENTITY();
END

