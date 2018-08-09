CREATE TABLE [dbo].[Rol]
(
	[IdRol] SMALLINT NOT NULL PRIMARY KEY IDENTITY, 
	[IdEmpresa] SMALLINT NOT NULL,
    [Rol] VARCHAR(50) NOT NULL, 
	[NombreRol] VARCHAR(50) NOT NULL, 
	[Activo] BIT NOT NULL DEFAULT 1,
    [FechaRegistro] DATETIME NOT NULL DEFAULT getdate(),     
)
