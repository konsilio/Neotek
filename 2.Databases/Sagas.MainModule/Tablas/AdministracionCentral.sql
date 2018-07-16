CREATE TABLE [dbo].[AdministracionCentral]
(
	[IdAdministracionCentral] TINYINT NOT NULL PRIMARY KEY IDENTITY, 
    [RazonSocial] VARCHAR(100) NOT NULL, 
    [FechaRegistro] DATETIME NOT NULL DEFAULT getDate()
)
