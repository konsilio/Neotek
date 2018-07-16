CREATE TABLE [dbo].[Usuario]
(
	[IdUsuario] SMALLINT NOT NULL PRIMARY KEY, 
	[IdEmpresa] SMALLINT NOT NULL,
    [Nombre] VARCHAR(100) NOT NULL, 
    [Apellido1] VARCHAR(80) NOT NULL, 
    [Apellido2] VARCHAR(80) NOT NULL, 
	[Email] VARCHAR(200) NOT NULL,
    [FechaRegistro] DATETIME NOT NULL DEFAULT getDate()
    
)
