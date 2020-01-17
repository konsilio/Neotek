CREATE TABLE [dbo].[CEstadosRep]
(
	[IdEstadoRep] TINYINT NOT NULL PRIMARY KEY IDENTITY, 
    [Estado] VARCHAR(50) NOT NULL, 
    [Abreviatura] VARCHAR(10) NOT NULL, 
    [FechaRegistro] DATETIME NOT NULL DEFAULT getdate()
)
