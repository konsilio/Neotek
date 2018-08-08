CREATE TABLE [dbo].[AlmacenEntradaGasDescargaPuerta]
(
	[IdAlmacenEntradaGasDescarga] INT NOT NULL PRIMARY KEY,
	[IdProveedorExpedidor] INT NULL, 
    [IdProveedorPorteador] INT NULL, 
    [FechaPapeleta] SMALLDATETIME NULL, 
    [FechaEmbarque] SMALLDATETIME NULL, 
    [NumeroEmbarque] VARCHAR(50) NULL,     
    [NombreOperador] VARCHAR(250) NULL, 
    [PlacasTractor] VARCHAR(25) NULL, 
    [NumTanquePG] VARCHAR(25) NULL, 
    [CapacidadTanqueLt] DECIMAL(18, 4) NULL, 
    [CapacidadTanqueKg] DECIMAL(18, 4) NULL, 
    [PorcenMagnatelPapeleta] DECIMAL(10, 2) NULL, 
    [PresionTanque] DECIMAL(10, 2) NULL, 
    [Sello] VARCHAR(25) NULL, 
    [ValorCarga] DECIMAL(18, 4) NULL, 
    [NombreResponsable] VARCHAR(250) NULL,      
    [PorcenMagnatelOcular] DECIMAL(10, 2) NULL, 
    [FechaEntraGas] DATETIME NULL 

)
