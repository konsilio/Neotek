CREATE TABLE [dbo].[CProveedorBancario]
(
	[IdProveedor] INT NOT NULL PRIMARY KEY, 
    [IdFormaDePago] TINYINT NOT NULL, 
    [IdBanco] TINYINT NOT NULL, 
    [Cuenta] VARCHAR(150) NOT NULL, 
    [DiasCredito] DECIMAL(18, 4) NOT NULL
)
