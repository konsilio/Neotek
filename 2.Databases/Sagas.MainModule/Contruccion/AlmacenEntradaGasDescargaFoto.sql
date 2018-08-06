CREATE TABLE [dbo].[AlmacenEntradaGasDescargaFoto]
(
	[IdAlmacenEntradaGasDescarga] INT NOT NULL, 
    [UrlImagen] VARCHAR(350) NULL, 
    [PathImagen] VARCHAR(250) NULL, 
    [ImagenDe] VARCHAR(50) NULL, 
    CONSTRAINT [PK_AlmacenEntradaGasDescargaFoto] PRIMARY KEY ([IdAlmacenEntradaGasDescarga]),

)
