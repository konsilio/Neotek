CREATE TABLE [dbo].[CProveedorFiscal]
(
	[IdProveedor] INT NOT NULL, 
	[IdTipoPersona] NCHAR(10) NOT NULL,
	[IdRegimenFiscal] INT NOT NULL,
    [Rfc] VARCHAR(13) NOT NULL, 
    [RazonSocial] VARCHAR(350) NOT NULL, 
    CONSTRAINT [PK_CProveedorFiscal] PRIMARY KEY ([IdProveedor])         
)
