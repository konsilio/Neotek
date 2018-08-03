CREATE TABLE [dbo].[CAlmacenGasTotal]
(
	[IdCAlmacenGasTotal] SMALLINT NOT NULL PRIMARY KEY IDENTITY, 
    [IdEmpresa] SMALLINT NOT NULL, 
    [CapacidadTotalLt] DECIMAL(18, 4) NOT NULL DEFAULT 0, 
    [CapacidadTotalKg] DECIMAL(18, 4) NOT NULL DEFAULT 0, 
    [CantidadActualLt] DECIMAL(18, 4) NOT NULL DEFAULT 0, 
    [CantidadActualKg] DECIMAL(18, 4) NOT NULL DEFAULT 0, 
    [PorcentajeActual] DECIMAL(18, 4) NOT NULL DEFAULT 0, 
    [Activo] BIT NOT NULL DEFAULT 1, 
    [FechaRegistro] SMALLDATETIME NOT NULL DEFAULT getDate()
)
