CREATE TABLE [dbo].[AlmacenEntradaProducto]
(	
	[IdRequisicion] INT NOT NULL,
    [IdOrdenCompra] INT NOT NULL,
	[IdAlmacen] INT NOT NULL ,
	[IdProduto] INT NOT NULL,
	[IdUsuarioRecibe] INT NOT NULL,
	[Cantidad] DECIMAL(18, 4) NOT NULL,
    [UrlDocEntrada] VARCHAR(350) NULL, 
    [PathDocEntrada] VARCHAR(350) NULL,	
	[Observaciones ] VARCHAR(250) NULL, 
	[FechaEntrada] SMALLDATETIME NOT NULL ,     
    [FechaRegistro] SMALLDATETIME NOT NULL DEFAULT GetDate(),     
)
