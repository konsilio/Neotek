CREATE TABLE [dbo].[CAlmacenGasCilindro]
(
	[IdCilindro] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Cantidad] DECIMAL(18, 2) NOT NULL DEFAULT 0, 
    [CapacidadLt] DECIMAL(18, 4) NOT NULL DEFAULT 0, 
    [CapacidadKg] DECIMAL(18, 4) NOT NULL DEFAULT 0, 
    [Precio] MONEY NOT NULL DEFAULT 0
)
