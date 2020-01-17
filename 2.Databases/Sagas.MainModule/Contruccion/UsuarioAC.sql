CREATE TABLE [dbo].[UsuarioAC]
(
	[IdUsuarioAC] INT NOT NULL PRIMARY KEY IDENTITY, 
	[IdAdministracionCentral] TINYINT NOT NULL,
	[IdRol] SMALLINT NOT NULL,
    [Nombre] VARCHAR(100) NOT NULL, 
    [Apellido1] VARCHAR(80) NOT NULL, 
    [Apellido2] VARCHAR(80) NULL, 
	[SuperAdmin] BIT NOT NULL DEFAULT 0,
	[Activo] BIT NOT NULL DEFAULT 1,
    [FechaRegistro] DATETIME NOT NULL DEFAULT getDate()
    
)
