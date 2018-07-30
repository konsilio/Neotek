CREATE TABLE [dbo].[RequisicionPorducto]
(
	[IdRequisicion] INT NOT NULL , 
	[IdProducto] INT NOT NULL, 
    [IdTipoProducto] INT NOT NULL,     
	[IdCentroCosto] INT NOT NULL,
    [Cantidad] DECIMAL(18, 2) NOT NULL, 
    [Aplicacion] VARCHAR(500) NOT NULL, 
    CONSTRAINT [PK_RequisicionPorducto] PRIMARY KEY ([IdRequisicion], [IdProducto]) 
)
