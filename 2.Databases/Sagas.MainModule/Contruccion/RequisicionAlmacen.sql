CREATE TABLE [dbo].[RequisicionAlmacen]
(
	[IdRequisicion] INT NOT NULL PRIMARY KEY, 
    [IdUsuarioRevision] SMALLINT NULL, 
    [OpinionAlmacen] VARCHAR(500) NULL, 
    [FechaRevision] DATETIME NULL , 
    [MotivoCancelacion] VARCHAR(500) NULL
)
