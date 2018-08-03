CREATE TABLE [dbo].[CProveedor](
	[IdProveedor] [int] NOT NULL,
	[IdEmpresa] [smallint] NOT NULL,
	[IdTipoProveedor] [tinyint] NOT NULL,
	[IdCuentaContable] [int] NULL,
	[NombreComercial] [varchar](100) NOT NULL,
	[ProdutoPrinicpal] [bit] NOT NULL,
	[TransportistaProdutoPrinicpal] [bit] NOT NULL,
	[Vende] [varchar](250) NULL,
	[Activo] [bit] NOT NULL,
	[FechaRegistro] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdProveedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]