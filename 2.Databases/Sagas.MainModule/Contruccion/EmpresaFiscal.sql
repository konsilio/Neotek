CREATE TABLE [dbo].[EmpresaFiscal]
(
	[IdEmpresa] SMALLINT NOT NULL, 
    [Rfc] VARCHAR(13) NOT NULL, 
    [RazonSocial] VARCHAR(350) NOT NULL, 
    CONSTRAINT [PK_EmpresaFiscal] PRIMARY KEY ([IdEmpresa]) 
)
