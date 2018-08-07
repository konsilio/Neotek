CREATE TABLE [dbo].[CProductoUnidadMedida]
(
	[IdUnidadMedida] SMALLINT NOT NULL PRIMARY KEY IDENTITY, 
	[IdEmpresa] SMALLINT NOT NULL, 
    [Nombre] VARCHAR(50) NOT NULL, 
    [Acronimo] VARCHAR(10) NOT NULL, 
    [Descripcion] VARCHAR(250) NULL, 
    [Activo] BIT NOT NULL DEFAULT 1, 
    [FechaRegistro] SMALLDATETIME NOT NULL DEFAULT getdate() 
)
