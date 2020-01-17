CREATE TABLE [dbo].[Almacen]
(
	[IdAlmacen] INT NOT NULL PRIMARY KEY IDENTITY, 
	[IdEmpresa] SMALLINT NOT NULL,
    [IdProduto] INT NOT NULL, 
    [Cantidad] DECIMAL(18, 4) NOT NULL, 
    [Ubicacion] VARCHAR(100) NOT NULL, 
    [FechaActualizacion] SMALLDATETIME NOT NULL, 
    [FechaRegistro] SMALLDATETIME NOT NULL DEFAULT GetDate()
)
