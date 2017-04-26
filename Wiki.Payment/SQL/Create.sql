USE [Wiki]
GO

/****** Object:  Table [dbo].[Payment]    Script Date: 06.02.2017 12:23:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Payment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [tinyint] NOT NULL,
	[OperatorId] [int] NOT NULL,
	[ClientId] [int] NOT NULL,
	[Price] [numeric](19, 4) NOT NULL,
	[Time] [datetime] NOT NULL,
 CONSTRAINT [PK_Payment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


