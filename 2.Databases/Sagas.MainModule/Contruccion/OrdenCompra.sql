CREATE TABLE [dbo].[OrdenCompra]
(
	[IdOrdenCompra] INT NOT NULL PRIMARY KEY IDENTITY, 
	[IdEmpresa] SMALLINT NOT NULL,
	[IdOrdenCompraEstatus] TINYINT NOT NULL,
    [IdRequisicion] INT NOT NULL, 
    [IdProveedor] INT NOT NULL, 
    [IdCentroCosto] INT NOT NULL, 
    [IdCuentaContable] INT NOT NULL, 
    [NumOrdenCompra] VARBINARY(25) NOT NULL, 
    [Activo] BIT NOT NULL DEFAULT 1, 
    [FechaRegistro] SMALLDATETIME NOT NULL DEFAULT getdate(), 
)
