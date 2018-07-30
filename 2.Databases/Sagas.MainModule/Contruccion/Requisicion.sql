CREATE TABLE [dbo].[Requisicion]
(
	[IdRequisicion] INT NOT NULL PRIMARY KEY, 
	[IdUsuarioSolicitante] INT NOT NULL, 
	[IdEmpresa] SMALLINT NOT NULL, 
    [NumeroRequisicion] VARCHAR(15) NOT NULL,
    [MotivoRequisicion] VARCHAR(500) NOT NULL, 
    [RequeridoEn] VARCHAR(500) NOT NULL, 
    [Estatus] TINYINT NOT NULL, 
    [FechaRequerida] DATETIME NOT NULL,   
    [FechaRegistro] DATETIME NOT NULL DEFAULT Getdate()    
)
