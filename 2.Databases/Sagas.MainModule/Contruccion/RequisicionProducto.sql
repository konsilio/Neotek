CREATE TABLE [dbo].[RequisicionProducto]
(
	[IdRequisicion] INT NOT NULL , 
	[IdProducto] INT NOT NULL, 
    [IdTipoProducto] INT NOT NULL,     
	[IdCentroCosto] INT NOT NULL,
    [Cantidad] DECIMAL(18, 2) NOT NULL, 
    [Aplicacion] VARCHAR(500) NOT NULL, 
    CONSTRAINT [PK_RequisicionProducto] PRIMARY KEY ([IdRequisicion], [IdProducto]) 
)
