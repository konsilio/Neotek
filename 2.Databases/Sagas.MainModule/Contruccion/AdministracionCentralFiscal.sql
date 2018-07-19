CREATE TABLE [dbo].[AdministracionCentralFiscal]
(
	[IdAdministracionCentral] TINYINT NOT NULL, 
    [Rfc] VARCHAR(13) NOT NULL, 
    [RazonSocial] VARCHAR(350) NOT NULL, 
    CONSTRAINT [PK_AdministracionCentralFiscal] PRIMARY KEY ([IdAdministracionCentral]) 
)
