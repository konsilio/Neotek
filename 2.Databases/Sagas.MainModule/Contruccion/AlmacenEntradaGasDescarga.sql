CREATE TABLE [dbo].[AlmacenEntradaGasDescarga]
(
	[IdAlmacenEntradaGasDescarga] INT NOT NULL PRIMARY KEY IDENTITY,
	[IdAlmacenGas] SMALLINT NULL,
	[IdRequisicion] INT NULL,
    [IdOrdenCompraExpedidor] INT NULL,
	[IdOrdenCompraPorteador] INT NULL,
	[IdCAlmacenGas] SMALLINT NULL,
	[IdTipoMedidorTractor] SMALLINT NULL,
	[IdTipoMedidorAlmacen] SMALLINT NULL,
    [DatosProcesados] BIT NULL DEFAULT 0,
	[FechaRegistro] DATETIME NOT NULL DEFAULT Getdate()
)
