CREATE TABLE [dbo].[OrdenCompraEstatus]
(
	[IdOrdenCompraEstatus] TINYINT NOT NULL PRIMARY KEY, 
    [Descripcion] VARCHAR(50) NOT NULL, 
    [Activo] BIT NOT NULL DEFAULT 1, 
    [FechaRegistro] SMALLDATETIME NOT NULL
)
