CREATE TABLE [dbo].[EmpresaConfiguracion]
(
	[IdEmpresa] SMALLINT NOT NULL,
    [FactorLitrosAKilos] DECIMAL(8, 4) NOT NULL DEFAULT 0.54, 
    [CierreInventario] SMALLDATETIME NOT NULL DEFAULT getdate(), 
    [InventarioSano] TINYINT NOT NULL DEFAULT 50, 
    [InventarioCrítico] TINYINT NOT NULL DEFAULT 35, 
    [MaxRemaGaseraMensual] DECIMAL(18, 4) NOT NULL DEFAULT 300000, 
    CONSTRAINT [PK_Configuracion] PRIMARY KEY ([IdEmpresa])
)
