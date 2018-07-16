CREATE TABLE [dbo].[Empresa]
(
	[IdEmpresa] SMALLINT NOT NULL PRIMARY KEY,
	[IdAdministracionCentral] TINYINT NOT NULL, 
    [RazonSocial] VARCHAR(100) NOT NULL, 
    [FechaRegistro] DATETIME NOT NULL DEFAULT getdate()
    
)
