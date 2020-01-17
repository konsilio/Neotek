CREATE TABLE [dbo].[RequisicionEstatus]
(
	[IdRequisicionEstatus] TINYINT NOT NULL IDENTITY, 
    [Estatus] VARCHAR(50) NULL, 
    CONSTRAINT [PK_RequisicionEstatus] PRIMARY KEY ([IdRequisicionEstatus]) 
)
