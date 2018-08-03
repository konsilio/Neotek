CREATE TABLE [dbo].[CAlmacenGasTipo]
(
	[IdTipoAlmacenGas] TINYINT NOT NULL PRIMARY KEY IDENTITY, 
    [Descripcion] VARCHAR(50) NOT NULL, 
    [Activo] BIT NOT NULL DEFAULT 1, 
    [FechaRegistro] SMALLDATETIME NOT NULL DEFAULT getDate()
)
