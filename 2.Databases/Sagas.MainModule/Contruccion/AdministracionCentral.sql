CREATE TABLE [dbo].[AdministracionCentral]
(
	[IdAdministracionCentral] TINYINT NOT NULL PRIMARY KEY IDENTITY, 
    [NombreComercial] VARCHAR(100) NOT NULL, 
    [FechaRegistro] DATETIME NOT NULL DEFAULT getDate()
)
