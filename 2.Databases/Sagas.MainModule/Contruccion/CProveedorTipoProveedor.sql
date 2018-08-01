CREATE TABLE [dbo].[CPorveedorTipoProveedor]
(
	[idTipoProveedor] BIT NOT NULL , 
    [Acreeedor] BIT NOT NULL, 
    [Proveedor] INT NOT NULL, 
    CONSTRAINT [PK_PorveedorTipoProveedor] PRIMARY KEY ([idTipoProveedor]) 
)
