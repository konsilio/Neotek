CREATE TABLE [dbo].[AlmacenSalidaProducto]
(
	[IdRequisicion] INT NOT NULL,
	[IdAlmacen] INT NOT NULL ,
	[IdProduto] INT NOT NULL,
	[IdUsuarioEntrega] INT NOT NULL,
	[Cantidad] DECIMAL(18, 4) NOT NULL,
    [UrlDocSalida] VARCHAR(350) NULL, 
    [PathDocSalida] VARCHAR(350) NULL,	
	[Observaciones ] VARCHAR(250) NULL, 
	[FechaEntrada] SMALLDATETIME NOT NULL ,     
    [FechaRegistro] SMALLDATETIME NOT NULL DEFAULT GetDate(),     
)
