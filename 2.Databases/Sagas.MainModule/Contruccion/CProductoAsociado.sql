CREATE TABLE [dbo].[CProductoAsociado]
(
	[IdProducto] INT NOT NULL , 
    [IdProductoAsociado] INT NOT NULL, 
    [Activo] BIT NOT NULL DEFAULT 1, 
    [FechaRegistro] SMALLDATETIME NOT NULL DEFAULT getDate()
)
