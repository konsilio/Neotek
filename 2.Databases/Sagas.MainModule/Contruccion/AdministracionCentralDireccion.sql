CREATE TABLE [dbo].[AdministracionCentralDireccion]
(
	[IdAdministracionCentral] TINYINT NOT NULL , 
    [IdPais] TINYINT NOT NULL, 
    [IdEstadoRep] TINYINT NULL, 
	[EstadoProvincia] VARCHAR(150) NULL,
    [Municipio] VARCHAR(100) NOT NULL, 
    [CodigoPostal] VARCHAR(20) NOT NULL, 
    [Colonia] VARCHAR(100) NOT NULL, 
    [Calle] VARCHAR(250) NOT NULL, 
    [NumExt] VARCHAR(10) NOT NULL, 
    [NumInt] VARCHAR(10) NULL, 
    CONSTRAINT [PK_AdministracionCentralDireccion] PRIMARY KEY ([IdAdministracionCentral]),    
)
