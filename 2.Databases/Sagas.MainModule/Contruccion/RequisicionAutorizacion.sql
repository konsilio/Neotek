CREATE TABLE [dbo].[RequisicionAutorizacion]
(
	[IdRequisicion] INT NOT NULL , 
    [IdUsuarioAutorizacion] INT NULL, 
    [FechaAutorizacion] DATETIME NULL, 
    CONSTRAINT [PK_RequisicionAutorizacion] PRIMARY KEY ([IdRequisicion])
)
