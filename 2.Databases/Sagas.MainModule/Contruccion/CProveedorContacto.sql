CREATE TABLE [dbo].[CProveedorContacto]
(
	[IdProveedor] SMALLINT NOT NULL, 
    [Telefono1] VARCHAR(50) NULL, 
    [Telefono2] VARCHAR(50) NULL, 
    [Telefono3] VARCHAR(50) NULL, 
	[Celular1] VARCHAR(50) NULL, 
    [Celular2] VARCHAR(50) NULL, 
    [Celular3] VARCHAR(50) NULL,
    [Email1] VARCHAR(200) NULL, 
    [Email2] VARCHAR(200) NULL, 
    [Email3] VARCHAR(200) NULL, 
    [SitioWeb1] VARCHAR(150) NULL, 
    [SitioWeb2] VARCHAR(150) NULL, 
    [SitioWeb3] VARCHAR(150) NULL, 
    CONSTRAINT [PK_CProveedorContacto] PRIMARY KEY ([IdProveedor]),
)
