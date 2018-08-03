CREATE TABLE [dbo].[CProveedorTipoProveedor]
(
	[IdTipoProveedor] TINYINT NOT NULL IDENTITY , 
    [Tipo] VARCHAR(50) NOT NULL, 
	[Activo] BIT NOT NULL DEFAULT 1,
	[FechaRegistro] DATETIME NOT NULL DEFAULT getdate()
    CONSTRAINT [PK_PorveedorTipoProveedor] PRIMARY KEY ([idTipoProveedor]) 
)
