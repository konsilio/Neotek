CREATE TABLE [dbo].[CProveedor]
(
	[IdProveedor] INT NOT NULL PRIMARY KEY, 
	[NombreComercial] VARCHAR(100) NOT NULL, 
    [FechaRegistro] DATETIME NOT NULL DEFAULT getdate(), 
    [ProdutoPrinicpal] BIT NOT NULL, 
    [Transportista] BIT NOT NULL
)
