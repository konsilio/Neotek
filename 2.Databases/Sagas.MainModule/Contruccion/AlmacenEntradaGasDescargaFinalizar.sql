CREATE TABLE [dbo].[AlmacenEntradaGasDescargaFinalizar]
(
	[IdAlmacenEntradaGasDescarga] INT NOT NULL PRIMARY KEY,
    [PorcenMagnatelOcularTractorFIN] DECIMAL(10, 2) NULL,
	[PorcenMagnatelOcularAlmacenFIN] DECIMAL(10, 2) NULL, 
    [FechaFinDescarga] DATETIME NULL,
)
