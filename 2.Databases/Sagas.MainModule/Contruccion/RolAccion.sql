CREATE TABLE [dbo].[RolAccion]
(
	[IdRol] SMALLINT NOT NULL, 
    [CompraCapRequisicion] BIT NOT NULL DEFAULT 0, 
    [CompraRevRequisicion] BIT NOT NULL DEFAULT 0, 
    [CompraGenOCompra] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [PK_RolAccion] PRIMARY KEY ([IdRol]) 
)
