﻿CREATE TABLE [dbo].[CPais]
(
	[IdPais] TINYINT NOT NULL PRIMARY KEY IDENTITY, 
    [Pais] VARCHAR(100) NULL, 
    [FechaRegistro] DATETIME NOT NULL DEFAULT getDate()
)
