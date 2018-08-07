CREATE TABLE [dbo].[CFormaPago]
(
	[IdFormaPago] TINYINT NOT NULL PRIMARY KEY, 
    [FormaPago] VARCHAR(100) NOT NULL, 
    [Activo] BIT NOT NULL DEFAULT 1, 
    [FechaRegistro] SMALLDATETIME NOT NULL DEFAULT GetDate()
)
