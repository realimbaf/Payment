USE [Wiki]
GO
/****** Object:  StoredProcedure [dbo].[get_payment_byId]    Script Date: 08.02.2017 17:38:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE  [dbo].[get_payment_byId] 
	@Id int
AS
BEGIN
	SET NOCOUNT ON;

Select p.[Id]
      ,p.[Type]
      ,p.[OperatorId]
	  ,o.Name as OperatorName
	  ,c.ClientCode
      ,p.[ClientId]
      ,p.[Price]
      ,p.[Time]
      ,p.[DocCode] 
	  FROM dbo.Payment as p
	  Join dbo.Client as o
	  ON p.OperatorId = o.Id
	  Join dbo.Client as c
	  ON p.ClientId = c.Id
	WHERE p.Id = @Id
END

