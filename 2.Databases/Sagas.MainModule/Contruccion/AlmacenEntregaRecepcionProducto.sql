CREATE TABLE [dbo].[AlmacenEntradSalidaProducto]
(
	[IdEnrtadaSalidaProducto] INT NOT NULL,
	[idEntradaSalida] INT NOT NULL,
	[IdProducto] INT NOT NULL,
	[Cantidad] DECIMAL(18, 2) NOT NULL 
)
