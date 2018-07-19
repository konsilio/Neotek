CREATE TABLE [dbo].[UsuarioCredencial]
(
	[IdUsuario] INT NOT NULL, 
    [Usuario] VARCHAR(350) NOT NULL, 
    [Password] VARCHAR(250) NOT NULL, 
    CONSTRAINT [PK_UsuarioCredencial] PRIMARY KEY ([IdUsuario]) 
)
