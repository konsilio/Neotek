CREATE TABLE [dbo].[RolAccion]
(
	[IdRol] SMALLINT NOT NULL, 
	[RequisicionVerRequisiciones] BIT NOT NULL DEFAULT 0, 
    [RequisicionGenerarNueva] BIT NOT NULL DEFAULT 0, 
    [RequisicionRevisarExistencia] BIT NOT NULL DEFAULT 0, 
    [RequisicionAutorizar] BIT NOT NULL DEFAULT 0, 
    [CompraVerOCompra] BIT NOT NULL DEFAULT 0, 
	[CompraGenerarOCompra] BIT NOT NULL DEFAULT 0, 
	[CompraAutorizarOCompra] BIT NOT NULL DEFAULT 0, 
	[AppCompraVerOCompra] BIT NOT NULL DEFAULT 0, 
	[CompraEntraProductoOCompra] BIT NOT NULL DEFAULT 0, 
	[CompraAtiendeServicioOCompra] BIT NOT NULL DEFAULT 0, 
	[AppCompraEntraGas] BIT NOT NULL DEFAULT 0, 
	[AppCompraGasIniciarDescarga] BIT NOT NULL DEFAULT 0, 
	[AppCompraGasFinalizarDescarga] BIT NOT NULL DEFAULT 0, 	
	CONSTRAINT [PK_RolAccion] PRIMARY KEY ([IdRol]) 
)
