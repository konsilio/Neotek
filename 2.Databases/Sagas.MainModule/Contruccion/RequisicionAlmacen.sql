CREATE TABLE [dbo].[RequisicionAlmacen]
(
	[IdRequisicion] INT NOT NULL , 
    [IdUsuarioRevision] INT NULL, 
    [OpinionAlmacen] VARCHAR(500) NULL, 
    [FechaRevision] DATETIME NULL , 
    [MotivoCancelacion] VARCHAR(500) NULL, 
    CONSTRAINT [PK_RequisicionAlmacen] PRIMARY KEY ([IdRequisicion])
)
