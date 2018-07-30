CREATE TABLE [dbo].[CompraOrdeCompraProducto]
(
	[IdOrdenCompra] VARCHAR(15) NOT NULL, 
    [TipoPoducto] TINYINT NULL, 
    [IdPorducto] INT NULL, 
    [NombreProducto] VARCHAR(50) NULL, 
    [Unidad] VARCHAR(3) NULL, 
    [Comentario] VARCHAR(500) NULL, 
    [IdCuentaContable] INT NULL, 
    [Cantidad] INT NULL, 
    [Precio] DECIMAL(18, 2) NULL, 
    [Descuento] DECIMAL(18, 2) NULL, 
    [IVA] DECIMAL(18, 2) NULL, 
    [IEPS] DECIMAL(18, 2) NULL 
)
