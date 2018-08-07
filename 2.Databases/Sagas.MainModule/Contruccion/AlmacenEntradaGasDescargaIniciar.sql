CREATE TABLE [dbo].[AlmacenEntradaGasDescargaIniciar]
(
	[IdAlmacenEntradaGasDescarga] INT NOT NULL PRIMARY KEY,	 
    [TanquePrestado] BIT NULL, 
    [PorcenMagnatelOcularTractorINI] DECIMAL(10, 2) NULL,
	[PorcenMagnatelOcularAlmacenINI] DECIMAL(10, 2) NULL, 
    [FechaInicioDescarga] DATETIME NULL,
)
