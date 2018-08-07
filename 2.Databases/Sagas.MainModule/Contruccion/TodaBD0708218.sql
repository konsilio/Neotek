USE [Sagas.MainModule]
GO
/****** Object:  Table [dbo].[AdministracionCentral]    Script Date: 07/08/2018 04:37:16 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AdministracionCentral](
	[IdAdministracionCentral] [tinyint] IDENTITY(1,1) NOT NULL,
	[NombreComercial] [varchar](100) NOT NULL,
	[FechaRegistro] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdAdministracionCentral] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AdministracionCentralDireccion]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AdministracionCentralDireccion](
	[IdAdministracionCentral] [tinyint] NOT NULL,
	[IdPais] [tinyint] NOT NULL,
	[IdEstadoRep] [tinyint] NULL,
	[EstadoProvincia] [varchar](150) NULL,
	[Municipio] [varchar](100) NOT NULL,
	[CodigoPostal] [varchar](20) NOT NULL,
	[Colonia] [varchar](100) NOT NULL,
	[Calle] [varchar](250) NOT NULL,
	[NumExt] [varchar](10) NOT NULL,
	[NumInt] [varchar](10) NULL,
 CONSTRAINT [PK_AdministracionCentralDireccion] PRIMARY KEY CLUSTERED 
(
	[IdAdministracionCentral] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AdministracionCentralFiscal]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AdministracionCentralFiscal](
	[IdAdministracionCentral] [tinyint] NOT NULL,
	[Rfc] [varchar](13) NOT NULL,
	[RazonSocial] [varchar](350) NOT NULL,
 CONSTRAINT [PK_AdministracionCentralFiscal] PRIMARY KEY CLUSTERED 
(
	[IdAdministracionCentral] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AdministracionCentralImagen]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AdministracionCentralImagen](
	[IdAdministracionCentral] [tinyint] NOT NULL,
	[UrlLogotipoMenu] [varchar](350) NULL,
	[UrlLogotipoLogin] [varchar](350) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdAdministracionCentral] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AdministracionCetralContacto]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AdministracionCetralContacto](
	[IdAdministracionCentral] [tinyint] NOT NULL,
	[Telefono1] [varchar](50) NULL,
	[Telefono2] [varchar](50) NULL,
	[Telefono3] [varchar](50) NULL,
	[Celular1] [varchar](50) NULL,
	[Celular2] [varchar](50) NULL,
	[Celular3] [varchar](50) NULL,
	[Email1] [varchar](200) NULL,
	[Email2] [varchar](200) NULL,
	[Email3] [varchar](200) NULL,
	[SitioWeb1] [varchar](150) NULL,
	[SitioWeb2] [varchar](150) NULL,
	[SitioWeb3] [varchar](150) NULL,
 CONSTRAINT [PK_AdministracionCetralContacto] PRIMARY KEY CLUSTERED 
(
	[IdAdministracionCentral] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Almacen]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Almacen](
	[IdAlmacen] [int] IDENTITY(1,1) NOT NULL,
	[IdEmpresa] [smallint] NOT NULL,
	[IdProduto] [int] NOT NULL,
	[Cantidad] [decimal](18, 4) NOT NULL,
	[Ubicacion] [varchar](100) NOT NULL,
	[FechaActualizacion] [smalldatetime] NOT NULL,
	[FechaRegistro] [smalldatetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdAlmacen] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AlmacenEntradaGasDescarga]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AlmacenEntradaGasDescarga](
	[IdAlmacenEntradaGasDescarga] [int] IDENTITY(1,1) NOT NULL,
	[IdAlmacenGas] [smallint] NULL,
	[IdRequisicion] [int] NULL,
	[IdOrdenCompraExpedidor] [int] NULL,
	[IdOrdenCompraPorteador] [int] NULL,
	[IdCAlmacenGas] [smallint] NULL,
	[IdTipoMedidorTractor] [smallint] NULL,
	[IdTipoMedidorAlmacen] [smallint] NULL,
	[DatosProcesados] [bit] NULL,
	[FechaRegistro] [datetime] NOT NULL,
	[fgh] [nchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdAlmacenEntradaGasDescarga] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AlmacenEntradaGasDescargaFinalizar]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AlmacenEntradaGasDescargaFinalizar](
	[IdAlmacenEntradaGasDescarga] [int] NOT NULL,
	[PorcenMagnatelOcularTractorFIN] [decimal](10, 2) NULL,
	[PorcenMagnatelOcularAlmacenFIN] [decimal](10, 2) NULL,
	[FechaFinDescarga] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdAlmacenEntradaGasDescarga] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AlmacenEntradaGasDescargaFoto]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AlmacenEntradaGasDescargaFoto](
	[IdAlmacenEntradaGasDescarga] [int] NOT NULL,
	[UrlImagen] [varchar](350) NOT NULL,
	[PathImagen] [varchar](250) NOT NULL,
	[IdImagenDe] [smallint] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AlmacenEntradaGasDescargaIniciar]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AlmacenEntradaGasDescargaIniciar](
	[IdAlmacenEntradaGasDescarga] [int] NOT NULL,
	[TanquePrestado] [bit] NULL,
	[PorcenMagnatelOcularTractorINI] [decimal](10, 2) NULL,
	[PorcenMagnatelOcularAlmacenINI] [decimal](10, 2) NULL,
	[FechaInicioDescarga] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdAlmacenEntradaGasDescarga] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AlmacenEntradaGasDescargaPuerta]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AlmacenEntradaGasDescargaPuerta](
	[IdAlmacenEntradaGasDescarga] [int] NOT NULL,
	[IdProveedorExpedidor] [int] NULL,
	[IdProveedorPorteador] [int] NULL,
	[FechaPapeleta] [smalldatetime] NULL,
	[FechaEmbarque] [smalldatetime] NULL,
	[NumeroEmbarque] [varchar](50) NULL,
	[NombreOperador] [varchar](250) NULL,
	[PlacasTractor] [varchar](25) NULL,
	[NumTanquePG] [varchar](25) NULL,
	[CapacidadTanqueLt] [decimal](18, 4) NULL,
	[CapacidadTanqueKg] [decimal](18, 4) NULL,
	[PorcenMagnatelPapeleta] [decimal](10, 2) NULL,
	[PresionTanque] [decimal](10, 2) NULL,
	[Sello] [varchar](25) NULL,
	[ValorCarga] [decimal](18, 4) NULL,
	[NombreResponsable] [varchar](250) NULL,
	[PorcenMagnatelOcular] [decimal](10, 2) NULL,
	[FechaEntraGas] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdAlmacenEntradaGasDescarga] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AlmacenEntradaProducto]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AlmacenEntradaProducto](
	[IdRequisicion] [int] NOT NULL,
	[IdOrdenCompra] [int] NOT NULL,
	[IdAlmacen] [int] NOT NULL,
	[IdProduto] [int] NOT NULL,
	[IdUsuarioRecibe] [int] NOT NULL,
	[Cantidad] [decimal](18, 4) NOT NULL,
	[UrlDocEntrada] [varchar](350) NULL,
	[PathDocEntrada] [varchar](350) NULL,
	[Observaciones ] [varchar](250) NULL,
	[FechaEntrada] [smalldatetime] NOT NULL,
	[FechaRegistro] [smalldatetime] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AlmacenGas]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AlmacenGas](
	[IdAlmacenGas] [smallint] IDENTITY(1,1) NOT NULL,
	[IdEmpresa] [smallint] NOT NULL,
	[CapacidadTotalLt] [decimal](18, 4) NOT NULL,
	[CapacidadTotalKg] [decimal](18, 4) NOT NULL,
	[CantidadActualLt] [decimal](18, 4) NOT NULL,
	[CantidadActualKg] [decimal](18, 4) NOT NULL,
	[PorcentajeActual] [decimal](18, 4) NOT NULL,
	[Activo] [bit] NOT NULL,
	[FechaRegistro] [smalldatetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdAlmacenGas] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AlmacenSalidaProducto]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AlmacenSalidaProducto](
	[IdRequisicion] [int] NOT NULL,
	[IdAlmacen] [int] NOT NULL,
	[IdProduto] [int] NOT NULL,
	[IdUsuarioEntrega] [int] NOT NULL,
	[Cantidad] [decimal](18, 4) NOT NULL,
	[UrlDocSalida] [varchar](350) NULL,
	[PathDocSalida] [varchar](350) NULL,
	[Observaciones ] [varchar](250) NULL,
	[FechaEntrada] [smalldatetime] NOT NULL,
	[FechaRegistro] [smalldatetime] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CAlmacenGas]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CAlmacenGas](
	[IdCAlmacenGas] [smallint] IDENTITY(1,1) NOT NULL,
	[IdEmpresa] [smallint] NOT NULL,
	[IdTipoAlmacen] [tinyint] NOT NULL,
	[IdTipoMedidor] [smallint] NOT NULL,
	[IdEstacionCarburacion] [int] NULL,
	[IdCamioneta] [int] NULL,
	[IdPipa] [int] NULL,
	[EsGeneral] [bit] NOT NULL,
	[CapacidadTanqueLt] [decimal](18, 4) NULL,
	[CapacidadTanqueKg] [decimal](18, 4) NULL,
	[CantidadActualLt] [decimal](18, 4) NOT NULL,
	[CantidadActualKg] [decimal](18, 4) NOT NULL,
	[PorcentajeActual] [decimal](18, 4) NOT NULL,
	[P5000Actual] [decimal](18, 4) NULL,
	[Activo] [bit] NOT NULL,
	[FechaRegistro] [smalldatetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdCAlmacenGas] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CAlmacenGasCalibracion]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CAlmacenGasCalibracion](
	[IdCAlmacenGas] [smallint] NOT NULL,
	[PorcentajeCalibracionPlaneada] [decimal](18, 4) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdCAlmacenGas] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CAlmacenGasCilindro]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CAlmacenGasCilindro](
	[IdCilindro] [int] IDENTITY(1,1) NOT NULL,
	[Cantidad] [decimal](18, 2) NOT NULL,
	[CapacidadLt] [decimal](18, 4) NOT NULL,
	[CapacidadKg] [decimal](18, 4) NOT NULL,
	[Precio] [money] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdCilindro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CAlmacenGasTipo]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CAlmacenGasTipo](
	[IdTipoAlmacenGas] [tinyint] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](50) NOT NULL,
	[Activo] [bit] NOT NULL,
	[FechaRegistro] [smalldatetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdTipoAlmacenGas] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CAlmacenGasTipoMedidor]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CAlmacenGasTipoMedidor](
	[IdTipoMedidor] [smallint] IDENTITY(1,1) NOT NULL,
	[Medidor] [varchar](50) NOT NULL,
	[NumeroFotografias] [tinyint] NOT NULL,
	[Activo] [bit] NOT NULL,
	[FechaRegistro] [smalldatetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdTipoMedidor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CCamioneta]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CCamioneta](
	[IdCamioneta] [int] IDENTITY(1,1) NOT NULL,
	[IdEmpresa] [smallint] NOT NULL,
	[Numero] [varchar](50) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Activo] [bit] NOT NULL,
	[FechaRegistro] [smalldatetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdCamioneta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CCamionetaCilindro]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CCamionetaCilindro](
	[IdEmpresa] [smallint] NOT NULL,
	[IdCamioneta] [int] NOT NULL,
	[IdCilindro] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CCentroCosto]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CCentroCosto](
	[IdCentroCosto] [int] IDENTITY(1,1) NOT NULL,
	[IdEmpresa] [smallint] NOT NULL,
	[IdTipoCentroCosto] [tinyint] NOT NULL,
	[IdEquipoTransporte] [int] NULL,
	[Numero] [varchar](50) NOT NULL,
	[Descripcion] [varchar](250) NOT NULL,
	[Activo] [bit] NOT NULL,
	[FechaRegistro] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdCentroCosto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CCuentaContable]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CCuentaContable](
	[IdCuentaContable] [int] IDENTITY(1,1) NOT NULL,
	[IdEmpresa] [smallint] NOT NULL,
	[Numero] [varchar](50) NOT NULL,
	[Descripcion] [varchar](250) NOT NULL,
	[Activo] [bit] NOT NULL,
	[FechaRegistro] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdCuentaContable] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CEstacionCarburacion]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CEstacionCarburacion](
	[IdEstacionCarburacion] [int] IDENTITY(1,1) NOT NULL,
	[IdEmpresa] [smallint] NOT NULL,
	[Numero] [varchar](50) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Activo] [bit] NOT NULL,
	[FechaRegistro] [smalldatetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdEstacionCarburacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CEstadosRep]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CEstadosRep](
	[IdEstadoRep] [tinyint] IDENTITY(1,1) NOT NULL,
	[Estado] [varchar](50) NOT NULL,
	[Abreviatura] [varchar](10) NOT NULL,
	[FechaRegistro] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdEstadoRep] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CFormaPago]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CFormaPago](
	[IdFormaPago] [tinyint] NOT NULL,
	[FormaPago] [varchar](100) NOT NULL,
	[Activo] [bit] NOT NULL,
	[FechaRegistro] [smalldatetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdFormaPago] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CPais]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CPais](
	[IdPais] [tinyint] IDENTITY(1,1) NOT NULL,
	[Pais] [varchar](100) NULL,
	[FechaRegistro] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdPais] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CPipa]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CPipa](
	[IdPipa] [int] IDENTITY(1,1) NOT NULL,
	[IdEmpresa] [smallint] NOT NULL,
	[Numero] [varchar](50) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Activo] [bit] NOT NULL,
	[FechaRegistro] [smalldatetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdPipa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CProducto]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CProducto](
	[IdProducto] [int] IDENTITY(1,1) NOT NULL,
	[IdEmpresa] [smallint] NOT NULL,
	[IdProductoServicioTipo] [smallint] NOT NULL,
	[IdCategoria] [smallint] NOT NULL,
	[IdProductoLinea] [smallint] NOT NULL,
	[IdUnidadMedida] [smallint] NOT NULL,
	[IdUnidadMedida2] [smallint] NULL,
	[Descripcion] [varchar](500) NOT NULL,
	[Minimos] [decimal](18, 4) NULL,
	[Maximo] [decimal](18, 4) NULL,
	[UrlImagen] [varchar](350) NULL,
	[PathImagen] [varchar](350) NULL,
	[Activo] [bit] NOT NULL,
	[FechaRegistro] [smalldatetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CProductoAsociado]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CProductoAsociado](
	[IdProducto] [int] NOT NULL,
	[IdProductoAsociado] [int] NOT NULL,
	[Activo] [bit] NOT NULL,
	[FechaRegistro] [smalldatetime] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CProductoCategoria]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CProductoCategoria](
	[IdCategoria] [smallint] IDENTITY(1,1) NOT NULL,
	[IdEmpresa] [smallint] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Descripcion] [varchar](250) NULL,
	[Activo] [bit] NOT NULL,
	[FechaRegistro] [smalldatetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdCategoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CProductoLinea]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CProductoLinea](
	[IdProductoLinea] [smallint] IDENTITY(1,1) NOT NULL,
	[IdEmpresa] [smallint] NOT NULL,
	[Linea] [varchar](50) NOT NULL,
	[Descripcion] [varchar](250) NULL,
	[Activo] [bit] NOT NULL,
	[FechaRegistro] [smalldatetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdProductoLinea] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CProductoServicioTipo]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CProductoServicioTipo](
	[IdProductoServicioTipo] [smallint] IDENTITY(1,1) NOT NULL,
	[IdEmpresa] [smallint] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Descripcion] [varchar](250) NULL,
	[Activo] [bit] NOT NULL,
	[FechaRegistro] [smalldatetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdProductoServicioTipo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CProductoUnidadMedida]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CProductoUnidadMedida](
	[IdUnidadMedida] [smallint] IDENTITY(1,1) NOT NULL,
	[IdEmpresa] [smallint] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Acronimo] [varchar](10) NOT NULL,
	[Descripcion] [varchar](250) NULL,
	[Activo] [bit] NOT NULL,
	[FechaRegistro] [smalldatetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdUnidadMedida] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CProveedor]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CProveedorBancario]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CProveedorBancario](
	[IdProveedor] [int] NOT NULL,
	[IdFormaDePago] [tinyint] NOT NULL,
	[IdBanco] [tinyint] NOT NULL,
	[Cuenta] [varchar](150) NOT NULL,
	[DiasCredito] [decimal](18, 4) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdProveedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CProveedorContacto]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CProveedorContacto](
	[IdProveedor] [int] NOT NULL,
	[Persona1] [varchar](150) NULL,
	[Persona2] [varchar](150) NULL,
	[Persona3] [varchar](150) NULL,
	[Telefono1] [varchar](50) NULL,
	[Telefono2] [varchar](50) NULL,
	[Telefono3] [varchar](50) NULL,
	[Celular1] [varchar](50) NULL,
	[Celular2] [varchar](50) NULL,
	[Celular3] [varchar](50) NULL,
	[Email1] [varchar](200) NULL,
	[Email2] [varchar](200) NULL,
	[Email3] [varchar](200) NULL,
	[SitioWeb1] [varchar](150) NULL,
	[SitioWeb2] [varchar](150) NULL,
	[SitioWeb3] [varchar](150) NULL,
 CONSTRAINT [PK_CProveedorContacto] PRIMARY KEY CLUSTERED 
(
	[IdProveedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CProveedorDireccion]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CProveedorDireccion](
	[IdProveedor] [int] NOT NULL,
	[IdPais] [tinyint] NOT NULL,
	[IdEstadoRep] [tinyint] NULL,
	[EstadoProvincia] [varchar](150) NULL,
	[Municipio] [varchar](100) NOT NULL,
	[CodigoPostal] [varchar](20) NOT NULL,
	[Colonia] [varchar](100) NOT NULL,
	[Calle] [varchar](250) NOT NULL,
	[NumExt] [varchar](10) NOT NULL,
	[NumInt] [varchar](10) NULL,
 CONSTRAINT [PK_CProveedorDireccion] PRIMARY KEY CLUSTERED 
(
	[IdProveedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CProveedorFiscal]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CProveedorFiscal](
	[IdProveedor] [int] NOT NULL,
	[IdTipoPersona] [nchar](10) NOT NULL,
	[IdRegimenFiscal] [int] NOT NULL,
	[Rfc] [varchar](13) NOT NULL,
	[RazonSocial] [varchar](350) NOT NULL,
 CONSTRAINT [PK_CProveedorFiscal] PRIMARY KEY CLUSTERED 
(
	[IdProveedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CProveedorTipoProveedor]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CProveedorTipoProveedor](
	[IdTipoProveedor] [tinyint] IDENTITY(1,1) NOT NULL,
	[Tipo] [varchar](50) NOT NULL,
	[Activo] [bit] NOT NULL,
	[FechaRegistro] [datetime] NOT NULL,
 CONSTRAINT [PK_PorveedorTipoProveedor] PRIMARY KEY CLUSTERED 
(
	[IdTipoProveedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Empresa]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Empresa](
	[IdEmpresa] [smallint] IDENTITY(1,1) NOT NULL,
	[IdAdministracionCentral] [tinyint] NOT NULL,
	[NombreComercial] [varchar](100) NOT NULL,
	[FechaRegistro] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdEmpresa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EmpresaConfiguracion]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmpresaConfiguracion](
	[IdEmpresa] [smallint] NOT NULL,
	[FactorLitrosAKilos] [decimal](8, 4) NOT NULL,
	[CierreInventario] [smalldatetime] NOT NULL,
	[InventarioSano] [tinyint] NOT NULL,
	[InventarioCrítico] [tinyint] NOT NULL,
	[MaxRemaGaseraMensual] [decimal](18, 4) NOT NULL,
 CONSTRAINT [PK_EmpresaConfiguracion] PRIMARY KEY CLUSTERED 
(
	[IdEmpresa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EmpresaContacto]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EmpresaContacto](
	[IdEmpresa] [smallint] NOT NULL,
	[Telefono1] [varchar](50) NULL,
	[Telefono2] [varchar](50) NULL,
	[Telefono3] [varchar](50) NULL,
	[Celular1] [varchar](50) NULL,
	[Celular2] [varchar](50) NULL,
	[Celular3] [varchar](50) NULL,
	[Email1] [varchar](200) NULL,
	[Email2] [varchar](200) NULL,
	[Email3] [varchar](200) NULL,
	[SitioWeb1] [varchar](150) NULL,
	[SitioWeb2] [varchar](150) NULL,
	[SitioWeb3] [varchar](150) NULL,
 CONSTRAINT [PK_EmpresaContacto] PRIMARY KEY CLUSTERED 
(
	[IdEmpresa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EmpresaDireccion]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EmpresaDireccion](
	[IdEmpresa] [smallint] NOT NULL,
	[IdPais] [tinyint] NOT NULL,
	[IdEstadoRep] [tinyint] NULL,
	[EstadoProvincia] [varchar](150) NULL,
	[Municipio] [varchar](100) NOT NULL,
	[CodigoPostal] [varchar](20) NOT NULL,
	[Colonia] [varchar](100) NOT NULL,
	[Calle] [varchar](250) NOT NULL,
	[NumExt] [varchar](10) NOT NULL,
	[NumInt] [varchar](10) NULL,
 CONSTRAINT [PK_EmpresaDireccion] PRIMARY KEY CLUSTERED 
(
	[IdEmpresa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EmpresaFiscal]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EmpresaFiscal](
	[IdEmpresa] [smallint] NOT NULL,
	[Rfc] [varchar](13) NOT NULL,
	[RazonSocial] [varchar](350) NOT NULL,
 CONSTRAINT [PK_EmpresaFiscal] PRIMARY KEY CLUSTERED 
(
	[IdEmpresa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EmpresaImagen]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EmpresaImagen](
	[IdEmpresa] [smallint] NOT NULL,
	[UrlLogotipoMenu] [varchar](350) NULL,
	[UrlLogotipoLogin] [varchar](350) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdEmpresa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ImagenDe]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ImagenDe](
	[IdImagenDe] [smallint] IDENTITY(1,1) NOT NULL,
	[ImagenDe] [varchar](50) NOT NULL,
	[Activo] [bit] NOT NULL,
	[FechaRegistro] [smalldatetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdImagenDe] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[OrdenCompra]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[OrdenCompra](
	[IdOrdenCompra] [int] IDENTITY(1,1) NOT NULL,
	[IdEmpresa] [smallint] NOT NULL,
	[IdOrdenCompraEstatus] [tinyint] NOT NULL,
	[IdRequisicion] [int] NOT NULL,
	[IdProveedor] [int] NOT NULL,
	[IdCentroCosto] [int] NOT NULL,
	[IdCuentaContable] [int] NOT NULL,
	[NumOrdenCompra] [varbinary](25) NOT NULL,
	[Activo] [bit] NOT NULL,
	[FechaRegistro] [smalldatetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdOrdenCompra] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[OrdenCompraEstatus]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[OrdenCompraEstatus](
	[IdOrdenCompraEstatus] [tinyint] NOT NULL,
	[Descripcion] [varchar](50) NOT NULL,
	[Activo] [bit] NOT NULL,
	[FechaRegistro] [smalldatetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdOrdenCompraEstatus] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[OrdenCompraImporte]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrdenCompraImporte](
	[IdOrdenCompra] [int] NOT NULL,
	[SubtotalSinIva] [decimal](18, 4) NULL,
	[SubtotalSinIeps] [decimal](18, 4) NULL,
	[Iva] [decimal](18, 4) NULL,
	[Ieps] [decimal](18, 4) NULL,
	[Total] [decimal](18, 4) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdOrdenCompra] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OrdenCompraProducto]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[OrdenCompraProducto](
	[IdOrdenCompra] [int] NOT NULL,
	[ProductoServicioTipo] [varchar](50) NOT NULL,
	[Producto] [varchar](50) NOT NULL,
	[Categoria] [varchar](50) NULL,
	[Linea] [varchar](50) NULL,
	[UnidadMedida] [varchar](50) NOT NULL,
	[UnidadMedida2] [varchar](50) NULL,
	[Descripcion] [varchar](500) NULL,
	[Cantidad] [int] NOT NULL,
	[Precio] [decimal](18, 2) NOT NULL,
	[Descuento] [decimal](18, 2) NOT NULL,
	[IVA] [decimal](18, 2) NOT NULL,
	[IEPS] [decimal](18, 2) NOT NULL,
	[Importe] [decimal](18, 2) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Requisicion]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Requisicion](
	[IdRequisicion] [int] IDENTITY(1,1) NOT NULL,
	[IdUsuarioSolicitante] [int] NOT NULL,
	[IdEmpresa] [smallint] NOT NULL,
	[NumeroRequisicion] [varchar](15) NOT NULL,
	[MotivoRequisicion] [varchar](500) NOT NULL,
	[RequeridoEn] [varchar](500) NOT NULL,
	[IdRequisicionEstatus] [tinyint] NOT NULL,
	[FechaRequerida] [datetime] NOT NULL,
	[FechaRegistro] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdRequisicion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RequisicionAlmacen]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RequisicionAlmacen](
	[IdRequisicion] [int] NOT NULL,
	[IdUsuarioRevision] [int] NULL,
	[OpinionAlmacen] [varchar](500) NULL,
	[FechaRevision] [datetime] NULL,
	[MotivoCancelacion] [varchar](500) NULL,
 CONSTRAINT [PK_RequisicionAlmacen] PRIMARY KEY CLUSTERED 
(
	[IdRequisicion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RequisicionAlmacenProducto]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RequisicionAlmacenProducto](
	[IdRequisicion] [int] NOT NULL,
	[IdProducto] [int] NOT NULL,
	[RevisionFisica] [bit] NULL,
	[CantidadAlmacenActual] [decimal](18, 2) NULL,
	[CantidadAComprar] [decimal](18, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdRequisicion] ASC,
	[IdProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RequisicionAutorizacion]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RequisicionAutorizacion](
	[IdRequisicion] [int] NOT NULL,
	[IdUsuarioAutorizacion] [int] NULL,
	[FechaAutorizacion] [datetime] NULL,
 CONSTRAINT [PK_RequisicionAutorizacion] PRIMARY KEY CLUSTERED 
(
	[IdRequisicion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RequisicionAutorizacionProducto]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RequisicionAutorizacionProducto](
	[IdRequisicion] [int] NOT NULL,
	[IdProducto] [int] NOT NULL,
	[AutorizaEntrega] [bit] NULL,
	[AutorizaCompra] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdRequisicion] ASC,
	[IdProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RequisicionEstatus]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RequisicionEstatus](
	[IdRequisicionEstatus] [tinyint] IDENTITY(1,1) NOT NULL,
	[Estatus] [varchar](50) NULL,
 CONSTRAINT [PK_RequisicionEstatus] PRIMARY KEY CLUSTERED 
(
	[IdRequisicionEstatus] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RequisicionProducto]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RequisicionProducto](
	[IdRequisicion] [int] NOT NULL,
	[IdProducto] [int] NOT NULL,
	[IdTipoProducto] [int] NOT NULL,
	[IdCentroCosto] [int] NOT NULL,
	[Cantidad] [decimal](18, 2) NOT NULL,
	[Aplicacion] [varchar](500) NOT NULL,
 CONSTRAINT [PK_RequisicionProducto] PRIMARY KEY CLUSTERED 
(
	[IdRequisicion] ASC,
	[IdProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Rol]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Rol](
	[IdRol] [smallint] IDENTITY(1,1) NOT NULL,
	[Rol] [varchar](50) NOT NULL,
	[NombreRol] [varchar](50) NOT NULL,
	[Activo] [bit] NOT NULL,
	[FechaRegistro] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RolAccion]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RolAccion](
	[IdRol] [smallint] NOT NULL,
	[CompraCapRequisicion] [bit] NOT NULL,
	[CompraRevRequisicion] [bit] NOT NULL,
	[CompraGenOCompra] [bit] NOT NULL,
 CONSTRAINT [PK_RolAccion] PRIMARY KEY CLUSTERED 
(
	[IdRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Usuario](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[IdEmpresa] [smallint] NOT NULL,
	[IdRol] [smallint] NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Apellido1] [varchar](80) NOT NULL,
	[Apellido2] [varchar](80) NULL,
	[Activo] [bit] NOT NULL,
	[FechaRegistro] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UsuarioAC]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UsuarioAC](
	[IdUsuarioAC] [int] IDENTITY(1,1) NOT NULL,
	[IdAdministracionCentral] [tinyint] NOT NULL,
	[IdRol] [smallint] NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Apellido1] [varchar](80) NOT NULL,
	[Apellido2] [varchar](80) NULL,
	[SuperAdmin] [bit] NOT NULL,
	[Activo] [bit] NOT NULL,
	[FechaRegistro] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdUsuarioAC] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UsuarioACContacto]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UsuarioACContacto](
	[IdUsuarioAC] [int] NOT NULL,
	[Telefono1] [varchar](50) NULL,
	[Telefono2] [varchar](50) NULL,
	[Telefono3] [varchar](50) NULL,
	[Celular1] [varchar](50) NULL,
	[Celular2] [varchar](50) NULL,
	[Celular3] [varchar](50) NULL,
	[Email1] [varchar](200) NULL,
	[Email2] [varchar](200) NULL,
	[Email3] [varchar](200) NULL,
	[SitioWeb1] [varchar](150) NULL,
	[SitioWeb2] [varchar](150) NULL,
	[SitioWeb3] [varchar](150) NULL,
 CONSTRAINT [PK_UsuarioACContacto] PRIMARY KEY CLUSTERED 
(
	[IdUsuarioAC] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UsuarioACCredencial]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UsuarioACCredencial](
	[IdUsuarioAC] [int] NOT NULL,
	[Usuario] [varchar](350) NOT NULL,
	[Password] [varchar](250) NOT NULL,
 CONSTRAINT [PK_UsuarioACCredencial] PRIMARY KEY CLUSTERED 
(
	[IdUsuarioAC] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UsuarioACDireccion]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UsuarioACDireccion](
	[IdUsuarioAC] [int] NOT NULL,
	[IdPais] [tinyint] NOT NULL,
	[IdEstadoRep] [tinyint] NULL,
	[EstadoProvincia] [varchar](150) NULL,
	[Municipio] [varchar](100) NOT NULL,
	[CodigoPostal] [varchar](20) NOT NULL,
	[Colonia] [varchar](100) NOT NULL,
	[Calle] [varchar](250) NOT NULL,
	[NumExt] [varchar](10) NOT NULL,
	[NumInt] [varchar](10) NULL,
 CONSTRAINT [PK_UsuarioACDireccion] PRIMARY KEY CLUSTERED 
(
	[IdUsuarioAC] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UsuarioContacto]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UsuarioContacto](
	[IdUsuario] [int] NOT NULL,
	[Telefono1] [varchar](50) NULL,
	[Telefono2] [varchar](50) NULL,
	[Telefono3] [varchar](50) NULL,
	[Celular1] [varchar](50) NULL,
	[Celular2] [varchar](50) NULL,
	[Celular3] [varchar](50) NULL,
	[Email1] [varchar](200) NULL,
	[Email2] [varchar](200) NULL,
	[Email3] [varchar](200) NULL,
	[SitioWeb1] [varchar](150) NULL,
	[SitioWeb2] [varchar](150) NULL,
	[SitioWeb3] [varchar](150) NULL,
 CONSTRAINT [PK_UsuarioContacto] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UsuarioCredencial]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UsuarioCredencial](
	[IdUsuario] [int] NOT NULL,
	[Usuario] [varchar](350) NOT NULL,
	[Password] [varchar](250) NOT NULL,
 CONSTRAINT [PK_UsuarioCredencial] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UsuarioDireccion]    Script Date: 07/08/2018 04:37:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UsuarioDireccion](
	[IdUsuario] [int] NOT NULL,
	[IdPais] [tinyint] NOT NULL,
	[IdEstadoRep] [tinyint] NULL,
	[EstadoProvincia] [varchar](150) NULL,
	[Municipio] [varchar](100) NOT NULL,
	[CodigoPostal] [varchar](20) NOT NULL,
	[Colonia] [varchar](100) NOT NULL,
	[Calle] [varchar](250) NOT NULL,
	[NumExt] [varchar](10) NOT NULL,
	[NumInt] [varchar](10) NULL,
 CONSTRAINT [PK_UsuarioDireccion] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[AdministracionCentral] ON 

INSERT [dbo].[AdministracionCentral] ([IdAdministracionCentral], [NombreComercial], [FechaRegistro]) VALUES (1, N'Grupo Also', CAST(0x0000A923008902B4 AS DateTime))
SET IDENTITY_INSERT [dbo].[AdministracionCentral] OFF
INSERT [dbo].[AdministracionCentralDireccion] ([IdAdministracionCentral], [IdPais], [IdEstadoRep], [EstadoProvincia], [Municipio], [CodigoPostal], [Colonia], [Calle], [NumExt], [NumInt]) VALUES (1, 1, 1, NULL, N'Acapúlco', N'46561', N'Las trojes', N'lozano garza', N'201', NULL)
INSERT [dbo].[AdministracionCentralFiscal] ([IdAdministracionCentral], [Rfc], [RazonSocial]) VALUES (1, N'SAHK8504092M5', N'Grupo Also S.A. de C.V.')
INSERT [dbo].[AdministracionCentralImagen] ([IdAdministracionCentral], [UrlLogotipoMenu], [UrlLogotipoLogin]) VALUES (1, NULL, NULL)
INSERT [dbo].[AdministracionCetralContacto] ([IdAdministracionCentral], [Telefono1], [Telefono2], [Telefono3], [Celular1], [Celular2], [Celular3], [Email1], [Email2], [Email3], [SitioWeb1], [SitioWeb2], [SitioWeb3]) VALUES (1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[CEstadosRep] ON 

INSERT [dbo].[CEstadosRep] ([IdEstadoRep], [Estado], [Abreviatura], [FechaRegistro]) VALUES (1, N'Aguascalientes', N'Ags.', CAST(0x0000A923008966F1 AS DateTime))
SET IDENTITY_INSERT [dbo].[CEstadosRep] OFF
SET IDENTITY_INSERT [dbo].[CPais] ON 

INSERT [dbo].[CPais] ([IdPais], [Pais], [FechaRegistro]) VALUES (1, N'México', CAST(0x0000A923008A969C AS DateTime))
SET IDENTITY_INSERT [dbo].[CPais] OFF
SET IDENTITY_INSERT [dbo].[Empresa] ON 

INSERT [dbo].[Empresa] ([IdEmpresa], [IdAdministracionCentral], [NombreComercial], [FechaRegistro]) VALUES (1, 1, N'Gas Mundial de Guerrero', CAST(0x0000A92E00D0932E AS DateTime))
SET IDENTITY_INSERT [dbo].[Empresa] OFF
INSERT [dbo].[EmpresaConfiguracion] ([IdEmpresa], [FactorLitrosAKilos], [CierreInventario], [InventarioSano], [InventarioCrítico], [MaxRemaGaseraMensual]) VALUES (1, CAST(0.5400 AS Decimal(8, 4)), CAST(0xA92E02F7 AS SmallDateTime), 50, 35, CAST(300000.0000 AS Decimal(18, 4)))
INSERT [dbo].[EmpresaContacto] ([IdEmpresa], [Telefono1], [Telefono2], [Telefono3], [Celular1], [Celular2], [Celular3], [Email1], [Email2], [Email3], [SitioWeb1], [SitioWeb2], [SitioWeb3]) VALUES (1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[EmpresaDireccion] ([IdEmpresa], [IdPais], [IdEstadoRep], [EstadoProvincia], [Municipio], [CodigoPostal], [Colonia], [Calle], [NumExt], [NumInt]) VALUES (1, 1, 1, NULL, N'Ags', N'20000', N'Centro', N'Zaragosa', N'1', N'N/A')
INSERT [dbo].[EmpresaFiscal] ([IdEmpresa], [Rfc], [RazonSocial]) VALUES (1, N'RZGMG1212', N'Gas Mundial de Guerrero ')
INSERT [dbo].[EmpresaImagen] ([IdEmpresa], [UrlLogotipoMenu], [UrlLogotipoLogin]) VALUES (1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Rol] ON 

INSERT [dbo].[Rol] ([IdRol], [Rol], [NombreRol], [Activo], [FechaRegistro]) VALUES (1, N'Super Usuario', N'Super Usuario', 1, CAST(0x0000A923008BE991 AS DateTime))
SET IDENTITY_INSERT [dbo].[Rol] OFF
SET IDENTITY_INSERT [dbo].[UsuarioAC] ON 

INSERT [dbo].[UsuarioAC] ([IdUsuarioAC], [IdAdministracionCentral], [IdRol], [Nombre], [Apellido1], [Apellido2], [SuperAdmin], [Activo], [FechaRegistro]) VALUES (1, 1, 1, N'Super Usuario', N'Super Usuario', N'Super Usuario', 1, 1, CAST(0x0000A923008C14B1 AS DateTime))
SET IDENTITY_INSERT [dbo].[UsuarioAC] OFF
INSERT [dbo].[UsuarioACContacto] ([IdUsuarioAC], [Telefono1], [Telefono2], [Telefono3], [Celular1], [Celular2], [Celular3], [Email1], [Email2], [Email3], [SitioWeb1], [SitioWeb2], [SitioWeb3]) VALUES (1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[UsuarioACCredencial] ([IdUsuarioAC], [Usuario], [Password]) VALUES (1, N'sa', N'2506D5B0FB4AC6CA2F6C0E46EEA0AA2A4097F0D651947E34907709CFAF4B416C')
INSERT [dbo].[UsuarioACDireccion] ([IdUsuarioAC], [IdPais], [IdEstadoRep], [EstadoProvincia], [Municipio], [CodigoPostal], [Colonia], [Calle], [NumExt], [NumInt]) VALUES (1, 1, 1, NULL, N'Acapúlco', N'46543', N'Las Trojes', N'Lazano garza', N'205', NULL)
ALTER TABLE [dbo].[AdministracionCentral] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[Almacen] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[AlmacenEntradaGasDescarga] ADD  DEFAULT ((0)) FOR [DatosProcesados]
GO
ALTER TABLE [dbo].[AlmacenEntradaGasDescarga] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[AlmacenEntradaProducto] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[AlmacenGas] ADD  DEFAULT ((0)) FOR [CapacidadTotalLt]
GO
ALTER TABLE [dbo].[AlmacenGas] ADD  DEFAULT ((0)) FOR [CapacidadTotalKg]
GO
ALTER TABLE [dbo].[AlmacenGas] ADD  DEFAULT ((0)) FOR [CantidadActualLt]
GO
ALTER TABLE [dbo].[AlmacenGas] ADD  DEFAULT ((0)) FOR [CantidadActualKg]
GO
ALTER TABLE [dbo].[AlmacenGas] ADD  DEFAULT ((0)) FOR [PorcentajeActual]
GO
ALTER TABLE [dbo].[AlmacenGas] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[AlmacenGas] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[AlmacenSalidaProducto] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[CAlmacenGas] ADD  DEFAULT ((0)) FOR [EsGeneral]
GO
ALTER TABLE [dbo].[CAlmacenGas] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[CAlmacenGas] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[CAlmacenGasCilindro] ADD  DEFAULT ((0)) FOR [Cantidad]
GO
ALTER TABLE [dbo].[CAlmacenGasCilindro] ADD  DEFAULT ((0)) FOR [CapacidadLt]
GO
ALTER TABLE [dbo].[CAlmacenGasCilindro] ADD  DEFAULT ((0)) FOR [CapacidadKg]
GO
ALTER TABLE [dbo].[CAlmacenGasCilindro] ADD  DEFAULT ((0)) FOR [Precio]
GO
ALTER TABLE [dbo].[CAlmacenGasTipo] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[CAlmacenGasTipo] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[CAlmacenGasTipoMedidor] ADD  DEFAULT ((1)) FOR [NumeroFotografias]
GO
ALTER TABLE [dbo].[CAlmacenGasTipoMedidor] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[CAlmacenGasTipoMedidor] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[CCamioneta] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[CCamioneta] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[CCentroCosto] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[CCentroCosto] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[CCuentaContable] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[CCuentaContable] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[CEstacionCarburacion] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[CEstacionCarburacion] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[CEstadosRep] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[CFormaPago] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[CFormaPago] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[CPais] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[CPipa] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[CPipa] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[CProducto] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[CProducto] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[CProductoAsociado] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[CProductoAsociado] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[CProductoCategoria] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[CProductoCategoria] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[CProductoLinea] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[CProductoLinea] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[CProductoServicioTipo] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[CProductoServicioTipo] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[CProductoUnidadMedida] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[CProductoUnidadMedida] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[CProveedor] ADD  DEFAULT ((0)) FOR [ProdutoPrinicpal]
GO
ALTER TABLE [dbo].[CProveedor] ADD  DEFAULT ((0)) FOR [TransportistaProdutoPrinicpal]
GO
ALTER TABLE [dbo].[CProveedor] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[CProveedor] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[CProveedorTipoProveedor] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[CProveedorTipoProveedor] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[Empresa] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[EmpresaConfiguracion] ADD  DEFAULT ((0.54)) FOR [FactorLitrosAKilos]
GO
ALTER TABLE [dbo].[EmpresaConfiguracion] ADD  DEFAULT (getdate()) FOR [CierreInventario]
GO
ALTER TABLE [dbo].[EmpresaConfiguracion] ADD  DEFAULT ((50)) FOR [InventarioSano]
GO
ALTER TABLE [dbo].[EmpresaConfiguracion] ADD  DEFAULT ((35)) FOR [InventarioCrítico]
GO
ALTER TABLE [dbo].[EmpresaConfiguracion] ADD  DEFAULT ((300000)) FOR [MaxRemaGaseraMensual]
GO
ALTER TABLE [dbo].[ImagenDe] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[ImagenDe] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[OrdenCompra] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[OrdenCompra] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[OrdenCompraEstatus] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[Requisicion] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[Rol] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[Rol] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[RolAccion] ADD  DEFAULT ((0)) FOR [CompraCapRequisicion]
GO
ALTER TABLE [dbo].[RolAccion] ADD  DEFAULT ((0)) FOR [CompraRevRequisicion]
GO
ALTER TABLE [dbo].[RolAccion] ADD  DEFAULT ((0)) FOR [CompraGenOCompra]
GO
ALTER TABLE [dbo].[Usuario] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[Usuario] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[UsuarioAC] ADD  DEFAULT ((0)) FOR [SuperAdmin]
GO
ALTER TABLE [dbo].[UsuarioAC] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[UsuarioAC] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[AdministracionCentralDireccion]  WITH CHECK ADD  CONSTRAINT [FK_AdministracionCentralDireccion_AdministracionCentral] FOREIGN KEY([IdAdministracionCentral])
REFERENCES [dbo].[AdministracionCentral] ([IdAdministracionCentral])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AdministracionCentralDireccion] CHECK CONSTRAINT [FK_AdministracionCentralDireccion_AdministracionCentral]
GO
ALTER TABLE [dbo].[AdministracionCentralDireccion]  WITH CHECK ADD  CONSTRAINT [FK_AdministracionCentralDireccion_CEstadosRep] FOREIGN KEY([IdEstadoRep])
REFERENCES [dbo].[CEstadosRep] ([IdEstadoRep])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[AdministracionCentralDireccion] CHECK CONSTRAINT [FK_AdministracionCentralDireccion_CEstadosRep]
GO
ALTER TABLE [dbo].[AdministracionCentralDireccion]  WITH CHECK ADD  CONSTRAINT [FK_AdministracionCentralDireccion_CPais] FOREIGN KEY([IdPais])
REFERENCES [dbo].[CPais] ([IdPais])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[AdministracionCentralDireccion] CHECK CONSTRAINT [FK_AdministracionCentralDireccion_CPais]
GO
ALTER TABLE [dbo].[AdministracionCentralFiscal]  WITH CHECK ADD  CONSTRAINT [FK_AdministracionCentralFiscal_AdministracionCentral] FOREIGN KEY([IdAdministracionCentral])
REFERENCES [dbo].[AdministracionCentral] ([IdAdministracionCentral])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AdministracionCentralFiscal] CHECK CONSTRAINT [FK_AdministracionCentralFiscal_AdministracionCentral]
GO
ALTER TABLE [dbo].[AdministracionCentralImagen]  WITH CHECK ADD  CONSTRAINT [FK_AdministracionCentralImagen_AdministracionCentral] FOREIGN KEY([IdAdministracionCentral])
REFERENCES [dbo].[AdministracionCentral] ([IdAdministracionCentral])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AdministracionCentralImagen] CHECK CONSTRAINT [FK_AdministracionCentralImagen_AdministracionCentral]
GO
ALTER TABLE [dbo].[AdministracionCetralContacto]  WITH CHECK ADD  CONSTRAINT [FK_AdministracionCetralContacto_AdministracionCentral] FOREIGN KEY([IdAdministracionCentral])
REFERENCES [dbo].[AdministracionCentral] ([IdAdministracionCentral])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AdministracionCetralContacto] CHECK CONSTRAINT [FK_AdministracionCetralContacto_AdministracionCentral]
GO
ALTER TABLE [dbo].[Almacen]  WITH CHECK ADD  CONSTRAINT [FK_Almacen_CProducto] FOREIGN KEY([IdProduto])
REFERENCES [dbo].[CProducto] ([IdProducto])
GO
ALTER TABLE [dbo].[Almacen] CHECK CONSTRAINT [FK_Almacen_CProducto]
GO
ALTER TABLE [dbo].[Almacen]  WITH CHECK ADD  CONSTRAINT [FK_Almacen_Empresa] FOREIGN KEY([IdEmpresa])
REFERENCES [dbo].[Empresa] ([IdEmpresa])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Almacen] CHECK CONSTRAINT [FK_Almacen_Empresa]
GO
ALTER TABLE [dbo].[AlmacenEntradaGasDescarga]  WITH CHECK ADD  CONSTRAINT [FK_AlmacenEntradaGasDescarga_AlmacenGas] FOREIGN KEY([IdAlmacenGas])
REFERENCES [dbo].[AlmacenGas] ([IdAlmacenGas])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[AlmacenEntradaGasDescarga] CHECK CONSTRAINT [FK_AlmacenEntradaGasDescarga_AlmacenGas]
GO
ALTER TABLE [dbo].[AlmacenEntradaGasDescarga]  WITH CHECK ADD  CONSTRAINT [FK_AlmacenEntradaGasDescarga_CAlmacenGas] FOREIGN KEY([IdCAlmacenGas])
REFERENCES [dbo].[CAlmacenGas] ([IdCAlmacenGas])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[AlmacenEntradaGasDescarga] CHECK CONSTRAINT [FK_AlmacenEntradaGasDescarga_CAlmacenGas]
GO
ALTER TABLE [dbo].[AlmacenEntradaGasDescarga]  WITH CHECK ADD  CONSTRAINT [FK_AlmacenEntradaGasDescarga_CAlmacenGasTipoMedidor_Almacen] FOREIGN KEY([IdTipoMedidorAlmacen])
REFERENCES [dbo].[CAlmacenGasTipoMedidor] ([IdTipoMedidor])
GO
ALTER TABLE [dbo].[AlmacenEntradaGasDescarga] CHECK CONSTRAINT [FK_AlmacenEntradaGasDescarga_CAlmacenGasTipoMedidor_Almacen]
GO
ALTER TABLE [dbo].[AlmacenEntradaGasDescarga]  WITH CHECK ADD  CONSTRAINT [FK_AlmacenEntradaGasDescarga_CAlmacenGasTipoMedidor_Tractor] FOREIGN KEY([IdTipoMedidorTractor])
REFERENCES [dbo].[CAlmacenGasTipoMedidor] ([IdTipoMedidor])
GO
ALTER TABLE [dbo].[AlmacenEntradaGasDescarga] CHECK CONSTRAINT [FK_AlmacenEntradaGasDescarga_CAlmacenGasTipoMedidor_Tractor]
GO
ALTER TABLE [dbo].[AlmacenEntradaGasDescarga]  WITH CHECK ADD  CONSTRAINT [FK_AlmacenEntradaGasDescarga_OrdenCompra_Expedidor] FOREIGN KEY([IdOrdenCompraExpedidor])
REFERENCES [dbo].[OrdenCompra] ([IdOrdenCompra])
GO
ALTER TABLE [dbo].[AlmacenEntradaGasDescarga] CHECK CONSTRAINT [FK_AlmacenEntradaGasDescarga_OrdenCompra_Expedidor]
GO
ALTER TABLE [dbo].[AlmacenEntradaGasDescarga]  WITH CHECK ADD  CONSTRAINT [FK_AlmacenEntradaGasDescarga_OrdenCompra_Porteador] FOREIGN KEY([IdOrdenCompraPorteador])
REFERENCES [dbo].[OrdenCompra] ([IdOrdenCompra])
GO
ALTER TABLE [dbo].[AlmacenEntradaGasDescarga] CHECK CONSTRAINT [FK_AlmacenEntradaGasDescarga_OrdenCompra_Porteador]
GO
ALTER TABLE [dbo].[AlmacenEntradaGasDescarga]  WITH CHECK ADD  CONSTRAINT [FK_AlmacenEntradaGasDescarga_Requisicion] FOREIGN KEY([IdRequisicion])
REFERENCES [dbo].[Requisicion] ([IdRequisicion])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[AlmacenEntradaGasDescarga] CHECK CONSTRAINT [FK_AlmacenEntradaGasDescarga_Requisicion]
GO
ALTER TABLE [dbo].[AlmacenEntradaGasDescargaFinalizar]  WITH CHECK ADD  CONSTRAINT [FK_AlmacenEntradaGasDescargaFinalizar_AlmacenEntradaGasDescarga] FOREIGN KEY([IdAlmacenEntradaGasDescarga])
REFERENCES [dbo].[AlmacenEntradaGasDescarga] ([IdAlmacenEntradaGasDescarga])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AlmacenEntradaGasDescargaFinalizar] CHECK CONSTRAINT [FK_AlmacenEntradaGasDescargaFinalizar_AlmacenEntradaGasDescarga]
GO
ALTER TABLE [dbo].[AlmacenEntradaGasDescargaFoto]  WITH CHECK ADD  CONSTRAINT [FK_AlmacenEntradaGasDescargaFoto_AlmacenEntradaGasDescarga] FOREIGN KEY([IdAlmacenEntradaGasDescarga])
REFERENCES [dbo].[AlmacenEntradaGasDescarga] ([IdAlmacenEntradaGasDescarga])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AlmacenEntradaGasDescargaFoto] CHECK CONSTRAINT [FK_AlmacenEntradaGasDescargaFoto_AlmacenEntradaGasDescarga]
GO
ALTER TABLE [dbo].[AlmacenEntradaGasDescargaFoto]  WITH CHECK ADD  CONSTRAINT [FK_AlmacenEntradaGasDescargaFoto_ImagenDe] FOREIGN KEY([IdImagenDe])
REFERENCES [dbo].[ImagenDe] ([IdImagenDe])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[AlmacenEntradaGasDescargaFoto] CHECK CONSTRAINT [FK_AlmacenEntradaGasDescargaFoto_ImagenDe]
GO
ALTER TABLE [dbo].[AlmacenEntradaGasDescargaIniciar]  WITH CHECK ADD  CONSTRAINT [FK_AlmacenEntradaGasDescargaIniciar_AlmacenEntradaGasDescarga] FOREIGN KEY([IdAlmacenEntradaGasDescarga])
REFERENCES [dbo].[AlmacenEntradaGasDescarga] ([IdAlmacenEntradaGasDescarga])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AlmacenEntradaGasDescargaIniciar] CHECK CONSTRAINT [FK_AlmacenEntradaGasDescargaIniciar_AlmacenEntradaGasDescarga]
GO
ALTER TABLE [dbo].[AlmacenEntradaGasDescargaPuerta]  WITH CHECK ADD  CONSTRAINT [FK_AlmacenEntradaGasDescargaPuerta_AlmacenEntradaGasDescarga] FOREIGN KEY([IdAlmacenEntradaGasDescarga])
REFERENCES [dbo].[AlmacenEntradaGasDescarga] ([IdAlmacenEntradaGasDescarga])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AlmacenEntradaGasDescargaPuerta] CHECK CONSTRAINT [FK_AlmacenEntradaGasDescargaPuerta_AlmacenEntradaGasDescarga]
GO
ALTER TABLE [dbo].[AlmacenEntradaProducto]  WITH CHECK ADD  CONSTRAINT [FK_AlmacenEntradaProducto_Almacen] FOREIGN KEY([IdAlmacen])
REFERENCES [dbo].[Almacen] ([IdAlmacen])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[AlmacenEntradaProducto] CHECK CONSTRAINT [FK_AlmacenEntradaProducto_Almacen]
GO
ALTER TABLE [dbo].[AlmacenEntradaProducto]  WITH CHECK ADD  CONSTRAINT [FK_AlmacenEntradaProducto_CProducto] FOREIGN KEY([IdProduto])
REFERENCES [dbo].[CProducto] ([IdProducto])
GO
ALTER TABLE [dbo].[AlmacenEntradaProducto] CHECK CONSTRAINT [FK_AlmacenEntradaProducto_CProducto]
GO
ALTER TABLE [dbo].[AlmacenEntradaProducto]  WITH CHECK ADD  CONSTRAINT [FK_AlmacenEntradaProducto_OrdenCompra] FOREIGN KEY([IdOrdenCompra])
REFERENCES [dbo].[OrdenCompra] ([IdOrdenCompra])
GO
ALTER TABLE [dbo].[AlmacenEntradaProducto] CHECK CONSTRAINT [FK_AlmacenEntradaProducto_OrdenCompra]
GO
ALTER TABLE [dbo].[AlmacenEntradaProducto]  WITH CHECK ADD  CONSTRAINT [FK_AlmacenEntradaProducto_Requisicion] FOREIGN KEY([IdRequisicion])
REFERENCES [dbo].[Requisicion] ([IdRequisicion])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[AlmacenEntradaProducto] CHECK CONSTRAINT [FK_AlmacenEntradaProducto_Requisicion]
GO
ALTER TABLE [dbo].[AlmacenEntradaProducto]  WITH CHECK ADD  CONSTRAINT [FK_AlmacenEntradaProducto_Usuario] FOREIGN KEY([IdUsuarioRecibe])
REFERENCES [dbo].[Usuario] ([IdUsuario])
GO
ALTER TABLE [dbo].[AlmacenEntradaProducto] CHECK CONSTRAINT [FK_AlmacenEntradaProducto_Usuario]
GO
ALTER TABLE [dbo].[AlmacenGas]  WITH CHECK ADD  CONSTRAINT [FK_AlmacenGas_Empresa] FOREIGN KEY([IdEmpresa])
REFERENCES [dbo].[Empresa] ([IdEmpresa])
GO
ALTER TABLE [dbo].[AlmacenGas] CHECK CONSTRAINT [FK_AlmacenGas_Empresa]
GO
ALTER TABLE [dbo].[AlmacenSalidaProducto]  WITH CHECK ADD  CONSTRAINT [FK_AlmacenSalidaProducto_Almacen] FOREIGN KEY([IdAlmacen])
REFERENCES [dbo].[Almacen] ([IdAlmacen])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[AlmacenSalidaProducto] CHECK CONSTRAINT [FK_AlmacenSalidaProducto_Almacen]
GO
ALTER TABLE [dbo].[AlmacenSalidaProducto]  WITH CHECK ADD  CONSTRAINT [FK_AlmacenSalidaProducto_CProducto] FOREIGN KEY([IdProduto])
REFERENCES [dbo].[CProducto] ([IdProducto])
GO
ALTER TABLE [dbo].[AlmacenSalidaProducto] CHECK CONSTRAINT [FK_AlmacenSalidaProducto_CProducto]
GO
ALTER TABLE [dbo].[AlmacenSalidaProducto]  WITH CHECK ADD  CONSTRAINT [FK_AlmacenSalidaProducto_Requisicion] FOREIGN KEY([IdRequisicion])
REFERENCES [dbo].[Requisicion] ([IdRequisicion])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[AlmacenSalidaProducto] CHECK CONSTRAINT [FK_AlmacenSalidaProducto_Requisicion]
GO
ALTER TABLE [dbo].[AlmacenSalidaProducto]  WITH CHECK ADD  CONSTRAINT [FK_AlmacenSalidaProducto_Usuario] FOREIGN KEY([IdUsuarioEntrega])
REFERENCES [dbo].[Usuario] ([IdUsuario])
GO
ALTER TABLE [dbo].[AlmacenSalidaProducto] CHECK CONSTRAINT [FK_AlmacenSalidaProducto_Usuario]
GO
ALTER TABLE [dbo].[CAlmacenGas]  WITH CHECK ADD  CONSTRAINT [FK_CAlmacenGas_CAlmacenGasTipo] FOREIGN KEY([IdTipoAlmacen])
REFERENCES [dbo].[CAlmacenGasTipo] ([IdTipoAlmacenGas])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[CAlmacenGas] CHECK CONSTRAINT [FK_CAlmacenGas_CAlmacenGasTipo]
GO
ALTER TABLE [dbo].[CAlmacenGas]  WITH CHECK ADD  CONSTRAINT [FK_CAlmacenGas_CAlmacenGasTipoMedidor] FOREIGN KEY([IdTipoMedidor])
REFERENCES [dbo].[CAlmacenGasTipoMedidor] ([IdTipoMedidor])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[CAlmacenGas] CHECK CONSTRAINT [FK_CAlmacenGas_CAlmacenGasTipoMedidor]
GO
ALTER TABLE [dbo].[CAlmacenGas]  WITH CHECK ADD  CONSTRAINT [FK_CAlmacenGas_CCamioneta] FOREIGN KEY([IdCamioneta])
REFERENCES [dbo].[CCamioneta] ([IdCamioneta])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[CAlmacenGas] CHECK CONSTRAINT [FK_CAlmacenGas_CCamioneta]
GO
ALTER TABLE [dbo].[CAlmacenGas]  WITH CHECK ADD  CONSTRAINT [FK_CAlmacenGas_CEstacionCarburacion] FOREIGN KEY([IdEstacionCarburacion])
REFERENCES [dbo].[CEstacionCarburacion] ([IdEstacionCarburacion])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[CAlmacenGas] CHECK CONSTRAINT [FK_CAlmacenGas_CEstacionCarburacion]
GO
ALTER TABLE [dbo].[CAlmacenGas]  WITH CHECK ADD  CONSTRAINT [FK_CAlmacenGas_CPipa] FOREIGN KEY([IdPipa])
REFERENCES [dbo].[CPipa] ([IdPipa])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[CAlmacenGas] CHECK CONSTRAINT [FK_CAlmacenGas_CPipa]
GO
ALTER TABLE [dbo].[CAlmacenGas]  WITH CHECK ADD  CONSTRAINT [FK_CAlmacenGas_Empresa] FOREIGN KEY([IdEmpresa])
REFERENCES [dbo].[Empresa] ([IdEmpresa])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[CAlmacenGas] CHECK CONSTRAINT [FK_CAlmacenGas_Empresa]
GO
ALTER TABLE [dbo].[CAlmacenGasCalibracion]  WITH CHECK ADD  CONSTRAINT [FK_CAlmacenGasCalibracion_CAlmacenGas] FOREIGN KEY([IdCAlmacenGas])
REFERENCES [dbo].[CAlmacenGas] ([IdCAlmacenGas])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CAlmacenGasCalibracion] CHECK CONSTRAINT [FK_CAlmacenGasCalibracion_CAlmacenGas]
GO
ALTER TABLE [dbo].[CCamioneta]  WITH CHECK ADD  CONSTRAINT [FK_CCamioneta_Empresa] FOREIGN KEY([IdEmpresa])
REFERENCES [dbo].[Empresa] ([IdEmpresa])
GO
ALTER TABLE [dbo].[CCamioneta] CHECK CONSTRAINT [FK_CCamioneta_Empresa]
GO
ALTER TABLE [dbo].[CCamionetaCilindro]  WITH CHECK ADD  CONSTRAINT [FK_CCamionetaCilindro_CAlmacenGasCilindro] FOREIGN KEY([IdCilindro])
REFERENCES [dbo].[CAlmacenGasCilindro] ([IdCilindro])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[CCamionetaCilindro] CHECK CONSTRAINT [FK_CCamionetaCilindro_CAlmacenGasCilindro]
GO
ALTER TABLE [dbo].[CCamionetaCilindro]  WITH CHECK ADD  CONSTRAINT [FK_CCamionetaCilindro_CCamioneta] FOREIGN KEY([IdCamioneta])
REFERENCES [dbo].[CCamioneta] ([IdCamioneta])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[CCamionetaCilindro] CHECK CONSTRAINT [FK_CCamionetaCilindro_CCamioneta]
GO
ALTER TABLE [dbo].[CCamionetaCilindro]  WITH CHECK ADD  CONSTRAINT [FK_CCamionetaCilindro_Empresa] FOREIGN KEY([IdEmpresa])
REFERENCES [dbo].[Empresa] ([IdEmpresa])
GO
ALTER TABLE [dbo].[CCamionetaCilindro] CHECK CONSTRAINT [FK_CCamionetaCilindro_Empresa]
GO
ALTER TABLE [dbo].[CEstacionCarburacion]  WITH CHECK ADD  CONSTRAINT [FK_CEstacionCarburacion_Empresa] FOREIGN KEY([IdEmpresa])
REFERENCES [dbo].[Empresa] ([IdEmpresa])
GO
ALTER TABLE [dbo].[CEstacionCarburacion] CHECK CONSTRAINT [FK_CEstacionCarburacion_Empresa]
GO
ALTER TABLE [dbo].[CPipa]  WITH CHECK ADD  CONSTRAINT [FK_CPipa_Empresa] FOREIGN KEY([IdEmpresa])
REFERENCES [dbo].[Empresa] ([IdEmpresa])
GO
ALTER TABLE [dbo].[CPipa] CHECK CONSTRAINT [FK_CPipa_Empresa]
GO
ALTER TABLE [dbo].[CProducto]  WITH CHECK ADD  CONSTRAINT [FK_CProducto_CProductoCategoria] FOREIGN KEY([IdCategoria])
REFERENCES [dbo].[CProductoCategoria] ([IdCategoria])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[CProducto] CHECK CONSTRAINT [FK_CProducto_CProductoCategoria]
GO
ALTER TABLE [dbo].[CProducto]  WITH CHECK ADD  CONSTRAINT [FK_CProducto_CProductoLinea] FOREIGN KEY([IdProductoLinea])
REFERENCES [dbo].[CProductoLinea] ([IdProductoLinea])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[CProducto] CHECK CONSTRAINT [FK_CProducto_CProductoLinea]
GO
ALTER TABLE [dbo].[CProducto]  WITH CHECK ADD  CONSTRAINT [FK_CProducto_CProductoServicioTipo] FOREIGN KEY([IdProductoServicioTipo])
REFERENCES [dbo].[CProductoServicioTipo] ([IdProductoServicioTipo])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[CProducto] CHECK CONSTRAINT [FK_CProducto_CProductoServicioTipo]
GO
ALTER TABLE [dbo].[CProducto]  WITH CHECK ADD  CONSTRAINT [FK_CProducto_CProductoUnidadMedida] FOREIGN KEY([IdUnidadMedida])
REFERENCES [dbo].[CProductoUnidadMedida] ([IdUnidadMedida])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[CProducto] CHECK CONSTRAINT [FK_CProducto_CProductoUnidadMedida]
GO
ALTER TABLE [dbo].[CProducto]  WITH CHECK ADD  CONSTRAINT [FK_CProducto_CProductoUnidadMedida1] FOREIGN KEY([IdUnidadMedida2])
REFERENCES [dbo].[CProductoUnidadMedida] ([IdUnidadMedida])
GO
ALTER TABLE [dbo].[CProducto] CHECK CONSTRAINT [FK_CProducto_CProductoUnidadMedida1]
GO
ALTER TABLE [dbo].[CProducto]  WITH CHECK ADD  CONSTRAINT [FK_CProducto_Empresa] FOREIGN KEY([IdEmpresa])
REFERENCES [dbo].[Empresa] ([IdEmpresa])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[CProducto] CHECK CONSTRAINT [FK_CProducto_Empresa]
GO
ALTER TABLE [dbo].[CProductoAsociado]  WITH CHECK ADD  CONSTRAINT [FK_CProductoAsociado_CProducto] FOREIGN KEY([IdProducto])
REFERENCES [dbo].[CProducto] ([IdProducto])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[CProductoAsociado] CHECK CONSTRAINT [FK_CProductoAsociado_CProducto]
GO
ALTER TABLE [dbo].[CProductoAsociado]  WITH CHECK ADD  CONSTRAINT [FK_CProductoAsociado_CProducto1] FOREIGN KEY([IdProductoAsociado])
REFERENCES [dbo].[CProducto] ([IdProducto])
GO
ALTER TABLE [dbo].[CProductoAsociado] CHECK CONSTRAINT [FK_CProductoAsociado_CProducto1]
GO
ALTER TABLE [dbo].[CProductoCategoria]  WITH CHECK ADD  CONSTRAINT [FK_CProductoCategoria_Empresa] FOREIGN KEY([IdEmpresa])
REFERENCES [dbo].[Empresa] ([IdEmpresa])
GO
ALTER TABLE [dbo].[CProductoCategoria] CHECK CONSTRAINT [FK_CProductoCategoria_Empresa]
GO
ALTER TABLE [dbo].[CProductoLinea]  WITH CHECK ADD  CONSTRAINT [FK_CProductoLinea_Empresa] FOREIGN KEY([IdEmpresa])
REFERENCES [dbo].[Empresa] ([IdEmpresa])
GO
ALTER TABLE [dbo].[CProductoLinea] CHECK CONSTRAINT [FK_CProductoLinea_Empresa]
GO
ALTER TABLE [dbo].[CProductoServicioTipo]  WITH CHECK ADD  CONSTRAINT [FK_CProductoServicioTipo_Empresa] FOREIGN KEY([IdEmpresa])
REFERENCES [dbo].[Empresa] ([IdEmpresa])
GO
ALTER TABLE [dbo].[CProductoServicioTipo] CHECK CONSTRAINT [FK_CProductoServicioTipo_Empresa]
GO
ALTER TABLE [dbo].[CProductoUnidadMedida]  WITH CHECK ADD  CONSTRAINT [FK_CProductoUnidadMedida_Empresa] FOREIGN KEY([IdEmpresa])
REFERENCES [dbo].[Empresa] ([IdEmpresa])
GO
ALTER TABLE [dbo].[CProductoUnidadMedida] CHECK CONSTRAINT [FK_CProductoUnidadMedida_Empresa]
GO
ALTER TABLE [dbo].[CProveedor]  WITH CHECK ADD  CONSTRAINT [FK_CProveedor_CProveedorTipoProveedor] FOREIGN KEY([IdTipoProveedor])
REFERENCES [dbo].[CProveedorTipoProveedor] ([IdTipoProveedor])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[CProveedor] CHECK CONSTRAINT [FK_CProveedor_CProveedorTipoProveedor]
GO
ALTER TABLE [dbo].[CProveedor]  WITH CHECK ADD  CONSTRAINT [FK_CProveedor_Empresa] FOREIGN KEY([IdEmpresa])
REFERENCES [dbo].[Empresa] ([IdEmpresa])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[CProveedor] CHECK CONSTRAINT [FK_CProveedor_Empresa]
GO
ALTER TABLE [dbo].[CProveedorBancario]  WITH CHECK ADD  CONSTRAINT [FK_CProveedorBancario_CProveedor] FOREIGN KEY([IdProveedor])
REFERENCES [dbo].[CProveedor] ([IdProveedor])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CProveedorBancario] CHECK CONSTRAINT [FK_CProveedorBancario_CProveedor]
GO
ALTER TABLE [dbo].[CProveedorContacto]  WITH CHECK ADD  CONSTRAINT [FK_CProveedorContacto_CProveedor] FOREIGN KEY([IdProveedor])
REFERENCES [dbo].[CProveedor] ([IdProveedor])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CProveedorContacto] CHECK CONSTRAINT [FK_CProveedorContacto_CProveedor]
GO
ALTER TABLE [dbo].[CProveedorDireccion]  WITH CHECK ADD  CONSTRAINT [FK_CProveedorDireccion_CProveedor] FOREIGN KEY([IdProveedor])
REFERENCES [dbo].[CProveedor] ([IdProveedor])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CProveedorDireccion] CHECK CONSTRAINT [FK_CProveedorDireccion_CProveedor]
GO
ALTER TABLE [dbo].[CProveedorFiscal]  WITH CHECK ADD  CONSTRAINT [FK_CProveedorFiscal_CProveedor] FOREIGN KEY([IdProveedor])
REFERENCES [dbo].[CProveedor] ([IdProveedor])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CProveedorFiscal] CHECK CONSTRAINT [FK_CProveedorFiscal_CProveedor]
GO
ALTER TABLE [dbo].[Empresa]  WITH CHECK ADD  CONSTRAINT [FK_Empresa_AdministracionCentral] FOREIGN KEY([IdAdministracionCentral])
REFERENCES [dbo].[AdministracionCentral] ([IdAdministracionCentral])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Empresa] CHECK CONSTRAINT [FK_Empresa_AdministracionCentral]
GO
ALTER TABLE [dbo].[EmpresaConfiguracion]  WITH CHECK ADD  CONSTRAINT [FK_EmpresaConfiguracion_Empresa] FOREIGN KEY([IdEmpresa])
REFERENCES [dbo].[Empresa] ([IdEmpresa])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[EmpresaConfiguracion] CHECK CONSTRAINT [FK_EmpresaConfiguracion_Empresa]
GO
ALTER TABLE [dbo].[EmpresaContacto]  WITH CHECK ADD  CONSTRAINT [FK_EmpresaContacto_Empresa] FOREIGN KEY([IdEmpresa])
REFERENCES [dbo].[Empresa] ([IdEmpresa])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[EmpresaContacto] CHECK CONSTRAINT [FK_EmpresaContacto_Empresa]
GO
ALTER TABLE [dbo].[EmpresaDireccion]  WITH CHECK ADD  CONSTRAINT [FK_EmpresaDireccion_CEstadosRep] FOREIGN KEY([IdEstadoRep])
REFERENCES [dbo].[CEstadosRep] ([IdEstadoRep])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[EmpresaDireccion] CHECK CONSTRAINT [FK_EmpresaDireccion_CEstadosRep]
GO
ALTER TABLE [dbo].[EmpresaDireccion]  WITH CHECK ADD  CONSTRAINT [FK_EmpresaDireccion_CPais] FOREIGN KEY([IdPais])
REFERENCES [dbo].[CPais] ([IdPais])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[EmpresaDireccion] CHECK CONSTRAINT [FK_EmpresaDireccion_CPais]
GO
ALTER TABLE [dbo].[EmpresaDireccion]  WITH CHECK ADD  CONSTRAINT [FK_EmpresaDireccion_Empresa] FOREIGN KEY([IdEmpresa])
REFERENCES [dbo].[Empresa] ([IdEmpresa])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[EmpresaDireccion] CHECK CONSTRAINT [FK_EmpresaDireccion_Empresa]
GO
ALTER TABLE [dbo].[EmpresaFiscal]  WITH CHECK ADD  CONSTRAINT [FK_EmpresaFiscal_Empresa] FOREIGN KEY([IdEmpresa])
REFERENCES [dbo].[Empresa] ([IdEmpresa])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[EmpresaFiscal] CHECK CONSTRAINT [FK_EmpresaFiscal_Empresa]
GO
ALTER TABLE [dbo].[EmpresaImagen]  WITH CHECK ADD  CONSTRAINT [FK_EmpresaImagen_Empresa] FOREIGN KEY([IdEmpresa])
REFERENCES [dbo].[Empresa] ([IdEmpresa])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[EmpresaImagen] CHECK CONSTRAINT [FK_EmpresaImagen_Empresa]
GO
ALTER TABLE [dbo].[OrdenCompra]  WITH CHECK ADD  CONSTRAINT [FK_OrdenCompra_CProveedor] FOREIGN KEY([IdProveedor])
REFERENCES [dbo].[CProveedor] ([IdProveedor])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[OrdenCompra] CHECK CONSTRAINT [FK_OrdenCompra_CProveedor]
GO
ALTER TABLE [dbo].[OrdenCompra]  WITH CHECK ADD  CONSTRAINT [FK_OrdenCompra_Empresa] FOREIGN KEY([IdEmpresa])
REFERENCES [dbo].[Empresa] ([IdEmpresa])
GO
ALTER TABLE [dbo].[OrdenCompra] CHECK CONSTRAINT [FK_OrdenCompra_Empresa]
GO
ALTER TABLE [dbo].[OrdenCompra]  WITH CHECK ADD  CONSTRAINT [FK_OrdenCompra_OrdenCompraEstatus] FOREIGN KEY([IdOrdenCompraEstatus])
REFERENCES [dbo].[OrdenCompraEstatus] ([IdOrdenCompraEstatus])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[OrdenCompra] CHECK CONSTRAINT [FK_OrdenCompra_OrdenCompraEstatus]
GO
ALTER TABLE [dbo].[OrdenCompra]  WITH CHECK ADD  CONSTRAINT [FK_OrdenCompra_Requisicion] FOREIGN KEY([IdRequisicion])
REFERENCES [dbo].[Requisicion] ([IdRequisicion])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[OrdenCompra] CHECK CONSTRAINT [FK_OrdenCompra_Requisicion]
GO
ALTER TABLE [dbo].[OrdenCompraImporte]  WITH CHECK ADD  CONSTRAINT [FK_OrdenCompraImporte_OrdenCompra] FOREIGN KEY([IdOrdenCompra])
REFERENCES [dbo].[OrdenCompra] ([IdOrdenCompra])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrdenCompraImporte] CHECK CONSTRAINT [FK_OrdenCompraImporte_OrdenCompra]
GO
ALTER TABLE [dbo].[OrdenCompraProducto]  WITH CHECK ADD  CONSTRAINT [FK_OrdenCompraProducto_OrdenCompra] FOREIGN KEY([IdOrdenCompra])
REFERENCES [dbo].[OrdenCompra] ([IdOrdenCompra])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrdenCompraProducto] CHECK CONSTRAINT [FK_OrdenCompraProducto_OrdenCompra]
GO
ALTER TABLE [dbo].[Requisicion]  WITH CHECK ADD  CONSTRAINT [FK_Requisicion_Empresa] FOREIGN KEY([IdEmpresa])
REFERENCES [dbo].[Empresa] ([IdEmpresa])
GO
ALTER TABLE [dbo].[Requisicion] CHECK CONSTRAINT [FK_Requisicion_Empresa]
GO
ALTER TABLE [dbo].[Requisicion]  WITH CHECK ADD  CONSTRAINT [FK_Requisicion_RequisicionEstatus] FOREIGN KEY([IdRequisicionEstatus])
REFERENCES [dbo].[RequisicionEstatus] ([IdRequisicionEstatus])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Requisicion] CHECK CONSTRAINT [FK_Requisicion_RequisicionEstatus]
GO
ALTER TABLE [dbo].[Requisicion]  WITH CHECK ADD  CONSTRAINT [FK_Requisicion_Usuario] FOREIGN KEY([IdUsuarioSolicitante])
REFERENCES [dbo].[Usuario] ([IdUsuario])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Requisicion] CHECK CONSTRAINT [FK_Requisicion_Usuario]
GO
ALTER TABLE [dbo].[RequisicionAlmacen]  WITH CHECK ADD  CONSTRAINT [FK_RequisicionAlmacen_Requisicion] FOREIGN KEY([IdRequisicion])
REFERENCES [dbo].[Requisicion] ([IdRequisicion])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RequisicionAlmacen] CHECK CONSTRAINT [FK_RequisicionAlmacen_Requisicion]
GO
ALTER TABLE [dbo].[RequisicionAlmacen]  WITH CHECK ADD  CONSTRAINT [FK_RequisicionAlmacen_Usuario] FOREIGN KEY([IdUsuarioRevision])
REFERENCES [dbo].[Usuario] ([IdUsuario])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[RequisicionAlmacen] CHECK CONSTRAINT [FK_RequisicionAlmacen_Usuario]
GO
ALTER TABLE [dbo].[RequisicionAlmacenProducto]  WITH CHECK ADD  CONSTRAINT [FK_RequisicionAlmacenProducto_RequisicionProducto] FOREIGN KEY([IdRequisicion], [IdProducto])
REFERENCES [dbo].[RequisicionProducto] ([IdRequisicion], [IdProducto])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RequisicionAlmacenProducto] CHECK CONSTRAINT [FK_RequisicionAlmacenProducto_RequisicionProducto]
GO
ALTER TABLE [dbo].[RequisicionAutorizacion]  WITH CHECK ADD  CONSTRAINT [FK_RequisicionAutorizacion_Requisicion] FOREIGN KEY([IdRequisicion])
REFERENCES [dbo].[Requisicion] ([IdRequisicion])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RequisicionAutorizacion] CHECK CONSTRAINT [FK_RequisicionAutorizacion_Requisicion]
GO
ALTER TABLE [dbo].[RequisicionAutorizacion]  WITH CHECK ADD  CONSTRAINT [FK_RequisicionAutorizacion_Usuario] FOREIGN KEY([IdUsuarioAutorizacion])
REFERENCES [dbo].[Usuario] ([IdUsuario])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[RequisicionAutorizacion] CHECK CONSTRAINT [FK_RequisicionAutorizacion_Usuario]
GO
ALTER TABLE [dbo].[RequisicionAutorizacionProducto]  WITH CHECK ADD  CONSTRAINT [FK_RequisicionAutorizacionProducto_RequisicionProducto] FOREIGN KEY([IdRequisicion], [IdProducto])
REFERENCES [dbo].[RequisicionProducto] ([IdRequisicion], [IdProducto])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RequisicionAutorizacionProducto] CHECK CONSTRAINT [FK_RequisicionAutorizacionProducto_RequisicionProducto]
GO
ALTER TABLE [dbo].[RequisicionProducto]  WITH CHECK ADD  CONSTRAINT [FK_RequisicionProducto_CProducto] FOREIGN KEY([IdProducto])
REFERENCES [dbo].[CProducto] ([IdProducto])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[RequisicionProducto] CHECK CONSTRAINT [FK_RequisicionProducto_CProducto]
GO
ALTER TABLE [dbo].[RequisicionProducto]  WITH CHECK ADD  CONSTRAINT [FK_RequisicionProducto_Requisicion] FOREIGN KEY([IdRequisicion])
REFERENCES [dbo].[Requisicion] ([IdRequisicion])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RequisicionProducto] CHECK CONSTRAINT [FK_RequisicionProducto_Requisicion]
GO
ALTER TABLE [dbo].[RolAccion]  WITH CHECK ADD  CONSTRAINT [FK_RolAccion_Rol] FOREIGN KEY([IdRol])
REFERENCES [dbo].[Rol] ([IdRol])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RolAccion] CHECK CONSTRAINT [FK_RolAccion_Rol]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Empresa] FOREIGN KEY([IdEmpresa])
REFERENCES [dbo].[Empresa] ([IdEmpresa])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_Empresa]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Rol] FOREIGN KEY([IdRol])
REFERENCES [dbo].[Rol] ([IdRol])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_Rol]
GO
ALTER TABLE [dbo].[UsuarioAC]  WITH CHECK ADD  CONSTRAINT [FK_UsuarioAC_AdministracionCentral] FOREIGN KEY([IdAdministracionCentral])
REFERENCES [dbo].[AdministracionCentral] ([IdAdministracionCentral])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UsuarioAC] CHECK CONSTRAINT [FK_UsuarioAC_AdministracionCentral]
GO
ALTER TABLE [dbo].[UsuarioAC]  WITH CHECK ADD  CONSTRAINT [FK_UsuarioAC_Rol] FOREIGN KEY([IdRol])
REFERENCES [dbo].[Rol] ([IdRol])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[UsuarioAC] CHECK CONSTRAINT [FK_UsuarioAC_Rol]
GO
ALTER TABLE [dbo].[UsuarioACContacto]  WITH CHECK ADD  CONSTRAINT [FK_UsuarioACContacto_UsuarioAC] FOREIGN KEY([IdUsuarioAC])
REFERENCES [dbo].[UsuarioAC] ([IdUsuarioAC])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UsuarioACContacto] CHECK CONSTRAINT [FK_UsuarioACContacto_UsuarioAC]
GO
ALTER TABLE [dbo].[UsuarioACCredencial]  WITH CHECK ADD  CONSTRAINT [FK_UsuarioACCredencial_UsuarioAC] FOREIGN KEY([IdUsuarioAC])
REFERENCES [dbo].[UsuarioAC] ([IdUsuarioAC])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UsuarioACCredencial] CHECK CONSTRAINT [FK_UsuarioACCredencial_UsuarioAC]
GO
ALTER TABLE [dbo].[UsuarioACDireccion]  WITH CHECK ADD  CONSTRAINT [FK_UsuarioACDireccion_CEstadosRep] FOREIGN KEY([IdEstadoRep])
REFERENCES [dbo].[CEstadosRep] ([IdEstadoRep])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[UsuarioACDireccion] CHECK CONSTRAINT [FK_UsuarioACDireccion_CEstadosRep]
GO
ALTER TABLE [dbo].[UsuarioACDireccion]  WITH CHECK ADD  CONSTRAINT [FK_UsuarioACDireccion_CPais] FOREIGN KEY([IdPais])
REFERENCES [dbo].[CPais] ([IdPais])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[UsuarioACDireccion] CHECK CONSTRAINT [FK_UsuarioACDireccion_CPais]
GO
ALTER TABLE [dbo].[UsuarioACDireccion]  WITH CHECK ADD  CONSTRAINT [FK_UsuarioACDireccion_UsuarioAC] FOREIGN KEY([IdUsuarioAC])
REFERENCES [dbo].[UsuarioAC] ([IdUsuarioAC])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UsuarioACDireccion] CHECK CONSTRAINT [FK_UsuarioACDireccion_UsuarioAC]
GO
ALTER TABLE [dbo].[UsuarioContacto]  WITH CHECK ADD  CONSTRAINT [FK_UsuarioContacto_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([IdUsuario])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UsuarioContacto] CHECK CONSTRAINT [FK_UsuarioContacto_Usuario]
GO
ALTER TABLE [dbo].[UsuarioCredencial]  WITH CHECK ADD  CONSTRAINT [FK_UsuarioCredencial_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([IdUsuario])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UsuarioCredencial] CHECK CONSTRAINT [FK_UsuarioCredencial_Usuario]
GO
ALTER TABLE [dbo].[UsuarioDireccion]  WITH CHECK ADD  CONSTRAINT [FK_UsuarioDireccion_CEstadosRep] FOREIGN KEY([IdEstadoRep])
REFERENCES [dbo].[CEstadosRep] ([IdEstadoRep])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[UsuarioDireccion] CHECK CONSTRAINT [FK_UsuarioDireccion_CEstadosRep]
GO
ALTER TABLE [dbo].[UsuarioDireccion]  WITH CHECK ADD  CONSTRAINT [FK_UsuarioDireccion_CPais] FOREIGN KEY([IdPais])
REFERENCES [dbo].[CPais] ([IdPais])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[UsuarioDireccion] CHECK CONSTRAINT [FK_UsuarioDireccion_CPais]
GO
ALTER TABLE [dbo].[UsuarioDireccion]  WITH CHECK ADD  CONSTRAINT [FK_UsuarioDireccion_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([IdUsuario])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UsuarioDireccion] CHECK CONSTRAINT [FK_UsuarioDireccion_Usuario]
GO
