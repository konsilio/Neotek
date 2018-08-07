CREATE TABLE [dbo].[OrdenCompraImporte]
(
	[IdOrdenCompra] INT NOT NULL PRIMARY KEY, 
    [SubtotalSinIva] DECIMAL(18, 4) NULL, 
    [SubtotalSinIeps] DECIMAL(18, 4) NULL, 
    [Iva] DECIMAL(18, 4) NULL, 
    [Ieps] DECIMAL(18, 4) NULL, 
    [Total] DECIMAL(18, 4) NULL
)
