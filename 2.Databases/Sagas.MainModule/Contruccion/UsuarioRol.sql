CREATE TABLE [dbo].[UsuarioRol]
(
	[IdUsuario] INT NOT NULL , 
    [IdRol] SMALLINT NOT NULL, 
    CONSTRAINT [PK_UsuarioRol] PRIMARY KEY ([IdUsuario], [IdRol])
)
