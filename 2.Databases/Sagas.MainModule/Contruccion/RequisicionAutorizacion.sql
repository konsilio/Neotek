CREATE TABLE [dbo].[RequisicionAutorizacion]
(
	[IdRequisicion] INT NOT NULL PRIMARY KEY, 
    [IdUsuarioAutorizacion] SMALLINT NULL, 
    [FechaAutorizacion] DATETIME NULL
)
