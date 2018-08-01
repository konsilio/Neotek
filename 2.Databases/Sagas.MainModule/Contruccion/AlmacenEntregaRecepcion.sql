CREATE TABLE [dbo].[AlmacenEntradaSalida
]
(
	[IdRT] INT NOT NULL PRIMARY KEY, 
	[IdRequisicion] INT NOT NULL,
    [IdOrdenCompra] INT NULL, 
    [IdTipoEntradaSalida] TINYINT NOT NULL, 
    [UrlDoc] VARCHAR(200) NULL, 
    [PathDoc] VARCHAR(200) NULL,	
	[FechaEntradaSalida] DATETIME NOT NULL DEFAULT GetDate(), 
    [Observaciones ] VARCHAR(150) NULL, 
    [IdUsuarioEntrega] INT NOT NULL
)
