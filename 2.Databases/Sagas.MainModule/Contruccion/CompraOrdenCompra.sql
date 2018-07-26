CREATE TABLE [dbo].[CompraOrdenCompra]
(
	[IdOrdenCompra] VARCHAR(15) NOT NULL PRIMARY KEY, 
    [IdRequisicion] VARCHAR(15) NULL, 
    [IdPorveedor] INT NULL, 
    [idCentroCosto] NCHAR(10) NULL
)
