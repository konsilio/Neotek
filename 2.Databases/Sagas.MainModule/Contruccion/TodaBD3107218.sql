USE [master]
GO
/****** Object:  Database [Sagas.MainModule]    Script Date: 31/07/2018 16:35:25 ******/
CREATE DATABASE [Sagas.MainModule]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Sagas.MainModule', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\Sagas.MainModule.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Sagas.MainModule_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\Sagas.MainModule_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Sagas.MainModule] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Sagas.MainModule].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Sagas.MainModule] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Sagas.MainModule] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Sagas.MainModule] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Sagas.MainModule] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Sagas.MainModule] SET ARITHABORT OFF 
GO
ALTER DATABASE [Sagas.MainModule] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Sagas.MainModule] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [Sagas.MainModule] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Sagas.MainModule] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Sagas.MainModule] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Sagas.MainModule] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Sagas.MainModule] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Sagas.MainModule] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Sagas.MainModule] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Sagas.MainModule] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Sagas.MainModule] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Sagas.MainModule] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Sagas.MainModule] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Sagas.MainModule] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Sagas.MainModule] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Sagas.MainModule] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Sagas.MainModule] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Sagas.MainModule] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Sagas.MainModule] SET RECOVERY FULL 
GO
ALTER DATABASE [Sagas.MainModule] SET  MULTI_USER 
GO
ALTER DATABASE [Sagas.MainModule] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Sagas.MainModule] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Sagas.MainModule] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Sagas.MainModule] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [Sagas.MainModule]
GO
/****** Object:  Table [dbo].[AdministracionCentral]    Script Date: 31/07/2018 16:35:25 ******/
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
/****** Object:  Table [dbo].[AdministracionCentralDireccion]    Script Date: 31/07/2018 16:35:25 ******/
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
/****** Object:  Table [dbo].[AdministracionCentralFiscal]    Script Date: 31/07/2018 16:35:25 ******/
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
/****** Object:  Table [dbo].[AdministracionCentralImagen]    Script Date: 31/07/2018 16:35:25 ******/
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
/****** Object:  Table [dbo].[AdministracionCetralContacto]    Script Date: 31/07/2018 16:35:25 ******/
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
/****** Object:  Table [dbo].[CEstadosRep]    Script Date: 31/07/2018 16:35:25 ******/
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
/****** Object:  Table [dbo].[CPais]    Script Date: 31/07/2018 16:35:25 ******/
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
/****** Object:  Table [dbo].[Empresa]    Script Date: 31/07/2018 16:35:25 ******/
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
/****** Object:  Table [dbo].[EmpresaConfiguracion]    Script Date: 31/07/2018 16:35:25 ******/
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
/****** Object:  Table [dbo].[EmpresaContacto]    Script Date: 31/07/2018 16:35:25 ******/
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
/****** Object:  Table [dbo].[EmpresaDireccion]    Script Date: 31/07/2018 16:35:25 ******/
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
/****** Object:  Table [dbo].[EmpresaFiscal]    Script Date: 31/07/2018 16:35:25 ******/
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
/****** Object:  Table [dbo].[EmpresaImagen]    Script Date: 31/07/2018 16:35:25 ******/
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
/****** Object:  Table [dbo].[Requisicion]    Script Date: 31/07/2018 16:35:25 ******/
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
/****** Object:  Table [dbo].[RequisicionAlmacen]    Script Date: 31/07/2018 16:35:25 ******/
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
/****** Object:  Table [dbo].[RequisicionAlmacenProducto]    Script Date: 31/07/2018 16:35:25 ******/
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
/****** Object:  Table [dbo].[RequisicionAutorizacion]    Script Date: 31/07/2018 16:35:25 ******/
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
/****** Object:  Table [dbo].[RequisicionAutorizacionProducto]    Script Date: 31/07/2018 16:35:25 ******/
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
/****** Object:  Table [dbo].[RequisicionEstatus]    Script Date: 31/07/2018 16:35:25 ******/
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
/****** Object:  Table [dbo].[RequisicionProducto]    Script Date: 31/07/2018 16:35:25 ******/
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
/****** Object:  Table [dbo].[Rol]    Script Date: 31/07/2018 16:35:25 ******/
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
/****** Object:  Table [dbo].[RolAccion]    Script Date: 31/07/2018 16:35:25 ******/
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
/****** Object:  Table [dbo].[Usuario]    Script Date: 31/07/2018 16:35:25 ******/
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
/****** Object:  Table [dbo].[UsuarioAC]    Script Date: 31/07/2018 16:35:25 ******/
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
/****** Object:  Table [dbo].[UsuarioACContacto]    Script Date: 31/07/2018 16:35:25 ******/
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
/****** Object:  Table [dbo].[UsuarioACCredencial]    Script Date: 31/07/2018 16:35:25 ******/
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
/****** Object:  Table [dbo].[UsuarioACDireccion]    Script Date: 31/07/2018 16:35:25 ******/
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
/****** Object:  Table [dbo].[UsuarioContacto]    Script Date: 31/07/2018 16:35:25 ******/
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
/****** Object:  Table [dbo].[UsuarioCredencial]    Script Date: 31/07/2018 16:35:25 ******/
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
/****** Object:  Table [dbo].[UsuarioDireccion]    Script Date: 31/07/2018 16:35:25 ******/
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
ALTER TABLE [dbo].[CEstadosRep] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[CPais] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
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
USE [master]
GO
ALTER DATABASE [Sagas.MainModule] SET  READ_WRITE 
GO
