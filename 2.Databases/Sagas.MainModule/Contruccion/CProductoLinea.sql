CREATE TABLE [dbo].[CProductoLinea]
(
	[IdProductoLinea] SMALLINT NOT NULL PRIMARY KEY IDENTITY, 
	[IdEmpresa] SMALLINT NOT NULL, 
    [Linea] VARCHAR(50) NOT NULL, 
    [Descripcion] VARCHAR(250) NULL,     
    [Activo] BIT NOT NULL DEFAULT 1, 
    [FechaRegistro] SMALLDATETIME NOT NULL DEFAULT getdate()
)
