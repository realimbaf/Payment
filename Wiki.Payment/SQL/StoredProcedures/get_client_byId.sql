USE WIKI;
GO
CREATE PROCEDURE  [dbo].[get_client_byId] 
	@Id int
AS
BEGIN
	SET NOCOUNT ON;

SELECT [Id],[ClientCode],[Name],[Phone]
	FROM dbo.Client
	WHERE Id = @Id
END