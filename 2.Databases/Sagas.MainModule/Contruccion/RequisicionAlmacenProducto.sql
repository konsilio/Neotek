CREATE TABLE [dbo].[RequisicionAlmacenProducto]
(
	[IdRequisicion] INT NOT NULL , 
    [IdProducto] INT NOT NULL, 
    [RevisionFisica] BIT NULL, 
    [CantidadAlmacenActual] DECIMAL(18, 2) NULL, 
    [CantidadAComprar] DECIMAL(18, 2) NULL, 
    PRIMARY KEY ([IdRequisicion], [IdProducto])
)
