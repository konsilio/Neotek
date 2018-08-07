CREATE TABLE [dbo].[CAlmacenGasTipoMedidor]
(
	[IdTipoMedidor] SMALLINT NOT NULL PRIMARY KEY IDENTITY, 
    [Medidor] VARCHAR(50) NOT NULL, 
	[NumeroFotografias] TINYINT NOT NULL DEFAULT 1,
    [Activo] BIT NOT NULL DEFAULT 1, 
    [FechaRegistro] SMALLDATETIME NOT NULL DEFAULT Getdate()     
)
