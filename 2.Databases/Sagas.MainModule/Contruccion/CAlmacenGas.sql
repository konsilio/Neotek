CREATE TABLE [dbo].[CAlmacenGas]
(
	[IdCAlmacenGas] SMALLINT NOT NULL PRIMARY KEY IDENTITY, 
    [IdEmpresa] SMALLINT NOT NULL, 
    [IdTipoAlmacen] TINYINT NOT NULL,
	[IdTipoMedidor] SMALLINT NOT NULL, 
    [IdEstacionCarburacion] INT NULL, 
    [IdCamioneta] INT NULL, 
    [IdPipa] INT NULL, 
    [EsGeneral] BIT NOT NULL DEFAULT 0, 
    [CapacidadTanqueLt] DECIMAL(18, 4) NULL, 
    [CapacidadTanqueKg] DECIMAL(18, 4) NULL, 
    [CantidadActualLt] DECIMAL(18, 4) NOT NULL, 
    [CantidadActualKg] DECIMAL(18, 4) NOT NULL, 
    [PorcentajeActual] DECIMAL(18, 4) NOT NULL, 
    [P5000Actual] DECIMAL(18, 4) NULL, 
    [Activo] BIT NOT NULL DEFAULT 1, 
    [FechaRegistro] SMALLDATETIME NOT NULL DEFAULT getDate()
)
