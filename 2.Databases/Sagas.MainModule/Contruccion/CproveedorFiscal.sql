CREATE TABLE [dbo].[CproveedorFiscal]
(
		[IdProveedor] SMALLINT NOT NULL, 
    [Rfc] VARCHAR(13) NOT NULL, 
    [RazonSocial] VARCHAR(350) NOT NULL, 
    [TipoPersona] NCHAR(10) NOT NULL, 
    [IdRegimenFiscal] INT NOT NULL, 
)
