CREATE TABLE [dbo].[CompraRequisicion]
(
	[IdRequisicion] VARCHAR(15) NOT NULL PRIMARY KEY, 
    [IdCentroCosto] INT NOT NULL, 
    [FechaRequerida] DATETIME NOT NULL, 
    [Solicitante] NCHAR(10) NOT NULL, 
    [MotivoCompra] VARCHAR(500) NOT NULL, 
    [RequeridoEn] VARCHAR(500) NOT NULL, 
    [Estatus] TINYINT NOT NULL
)
