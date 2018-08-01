CREATE TABLE [dbo].[CProveedor]
(
	[IdProveedor] INT NOT NULL PRIMARY KEY, 
	[IdEmpresa] SMALLINT NOT NULL,
	[NombreComercial] VARCHAR(100) NOT NULL,      
    [ProdutoPrinicpal] BIT NOT NULL DEFAULT 0, 
    [TransportistaProdutoPrinicpal] BIT NOT NULL DEFAULT 0,
	[Activo] BIT NOT NULL DEFAULT 1,
	[FechaRegistro] DATETIME NOT NULL DEFAULT getdate()
)
