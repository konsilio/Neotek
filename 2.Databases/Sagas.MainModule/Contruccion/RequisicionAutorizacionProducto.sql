CREATE TABLE [dbo].[RequisicionAutorizacionProducto]
(
	[IdRequisicion] INT NOT NULL , 
    [IdProducto] INT NOT NULL, 
    [AutorizaEntrega] BIT NULL, 
    [AutorizaCompra] BIT NULL, 
    PRIMARY KEY ([IdRequisicion], [IdProducto])
)
