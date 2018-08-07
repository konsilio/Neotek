CREATE TABLE [dbo].[OrdenCompraProducto]
(
	[IdOrdenCompra] INT NOT NULL, 
	[ProductoServicioTipo] VARCHAR(50) NOT NULL,
    [Producto] VARCHAR(50) NOT NULL,     
    [Categoria] VARCHAR(50) NULL, 
    [Linea] VARCHAR(50) NULL, 
    [UnidadMedida] VARCHAR(50) NOT NULL, 
    [UnidadMedida2] VARCHAR(50) NULL, 
    [Descripcion] VARCHAR(500) NULL,     
	[Cantidad] INT NOT NULL, 
    [Precio] DECIMAL(18, 2) NOT NULL, 
    [Descuento] DECIMAL(18, 2) NOT NULL, 
    [IVA] DECIMAL(18, 2) NOT NULL, 
    [IEPS] DECIMAL(18, 2) NOT NULL,
	[Importe] DECIMAL(18, 2) NOT NULL, 
)
