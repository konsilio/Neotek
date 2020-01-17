CREATE TABLE [dbo].[Usuario]
(
	[IdUsuario] INT NOT NULL PRIMARY KEY IDENTITY, 
	[IdEmpresa] SMALLINT NOT NULL,
    [Nombre] VARCHAR(100) NOT NULL, 
    [Apellido1] VARCHAR(80) NOT NULL, 
    [Apellido2] VARCHAR(80) NULL, 
	[Activo] BIT NOT NULL DEFAULT 1,
    [FechaRegistro] DATETIME NOT NULL DEFAULT getDate()
    
)
