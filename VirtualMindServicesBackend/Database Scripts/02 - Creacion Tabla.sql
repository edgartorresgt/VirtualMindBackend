USE [VirtualMindTest]
GO

/****** Object:  Table [dbo].[Transaccion]    Script Date: 6/9/2020 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Transaccion](
	[IdTransaccion] [uniqueidentifier] NOT NULL,
	[IdUsuario] [nvarchar](20) NULL,
	[MontoPesosArgentinos] [decimal](18, 2) NOT NULL,
	[MonedaCompra] [nvarchar](10) NULL,
	[MontoCambioDia] [decimal](18, 2) NOT NULL,
	[MontoCompra] [decimal](18, 2) NOT NULL,
	[FechaTransaccion] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Transaccion] PRIMARY KEY CLUSTERED 
(
	[IdTransaccion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


