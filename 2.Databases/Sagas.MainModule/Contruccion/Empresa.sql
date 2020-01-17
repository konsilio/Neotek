CREATE TABLE [dbo].[Empresa]
(
	[IdEmpresa] SMALLINT NOT NULL PRIMARY KEY IDENTITY,
	[IdAdministracionCentral] TINYINT NOT NULL, 
    [NombreComercial] VARCHAR(100) NOT NULL, 
    [FechaRegistro] DATETIME NOT NULL DEFAULT getdate()
    
)
