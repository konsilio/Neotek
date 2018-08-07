CREATE TABLE [dbo].[CCuentaContable]
(	
	[IdCuentaContable] INT NOT NULL PRIMARY KEY IDENTITY, 
	[IdEmpresa] SMALLINT NOT NULL,
    [Numero] VARCHAR(50) NOT NULL, 
    [Descripcion] VARCHAR(250) NOT NULL,     	
	[Activo] BIT NOT NULL DEFAULT 1,
	[FechaRegistro] DATETIME NOT NULL DEFAULT getdate()
)
