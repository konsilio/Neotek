CREATE TABLE [dbo].[AlmacenMercancia]
(
	[IdAlmacen] INT NOT NULL PRIMARY KEY, 
    [IdProduto] INT NOT NULL, 
    [Cantidad] DECIMAL(18, 2) NOT NULL, 
    [Ubicacion] VARCHAR(100) NOT NULL, 
    [FechaActualizacion ] DATETIME NOT NULL
)
