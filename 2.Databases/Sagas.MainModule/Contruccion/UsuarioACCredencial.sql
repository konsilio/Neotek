CREATE TABLE [dbo].[UsuarioACCredencial]
(
	[IdUsuarioAC] INT NOT NULL, 
    [Usuario] VARCHAR(350) NOT NULL, 
    [Password] VARCHAR(250) NOT NULL, 
    CONSTRAINT [PK_UsuarioACCredencial] PRIMARY KEY ([IdUsuarioAC]) 
)
