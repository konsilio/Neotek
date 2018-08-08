CREATE TABLE [dbo].[CEstacionCarburacion]
(
	[IdEstacionCarburacion] INT NOT NULL PRIMARY KEY IDENTITY,
	[IdEmpresa] SMALLINT NOT NULL, 
    [Numero] VARCHAR(50) NOT NULL, 
    [Nombre] VARCHAR(50) NULL, 
    [Activo] BIT NOT NULL DEFAULT 1, 
    [FechaRegistro] SMALLDATETIME NOT NULL DEFAULT Getdate(),
)
