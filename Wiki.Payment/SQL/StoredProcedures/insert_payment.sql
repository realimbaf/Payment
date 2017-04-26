USE [Wiki]
GO
/****** Object:  StoredProcedure [dbo].[insert_payment]    Script Date: 09.02.2017 11:43:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE  [dbo].[insert_payment] 
	@Type tinyint,   
    @OperatorId int,
	@ClientId int,
	@Price numeric(19,2)
AS
BEGIN
	SET NOCOUNT ON;

INSERT INTO [dbo].[Payment]
           ([Type]
           ,[OperatorId]
           ,[ClientId]
		   ,[Price]
		   ,[Time]
		   )
     VALUES(
           @Type ,   
		   @OperatorId,
           @ClientId,
		   @Price,
		   GETDATE()
		   )
		   SELECT Id,[Time] FROM Payment WHERE id = SCOPE_IDENTITY();
END