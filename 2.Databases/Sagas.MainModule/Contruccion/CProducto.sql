CREATE TABLE [dbo].[CProducto]
(
	[IdProducto] INT NOT NULL PRIMARY KEY IDENTITY, 
	[IdEmpresa] SMALLINT NOT NULL,
	[IdProductoServicioTipo] SMALLINT NOT NULL,
	[IdCategoria] SMALLINT NOT NULL,
	[IdProductoLinea] SMALLINT NOT NULL,
	[IdUnidadMedida] SMALLINT NOT NULL,
	[IdUnidadMedida2] SMALLINT NULL, 
    [Descripcion] VARCHAR(500) NOT NULL, 
    [Minimos] DECIMAL(18, 4) NULL, 
    [Maximo] DECIMAL(18, 4) NULL, 
    [UrlImagen] VARCHAR(350) NULL, 
    [RutaFisicaImagen] VARCHAR(350) NULL, 
    [Activo] BIT NOT NULL DEFAULT 1, 
    [FechaRegistro] SMALLDATETIME NULL DEFAULT getdate(),
)
