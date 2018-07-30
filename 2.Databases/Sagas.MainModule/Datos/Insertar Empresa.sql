--USE [Sagas.MainModule]
--GO

--INSERT INTO [dbo].[Empresa]
--           ([IdAdministracionCentral]
--           ,[NombreComercial]
--           )
--     VALUES
--           (1           ,'Gas Mundial de Guerrero')
--GO


--USE [Sagas.MainModule]
--GO

--INSERT INTO [dbo].[EmpresaConfiguracion]
--           ([IdEmpresa]
--           ,[FactorLitrosAKilos]
--           ,[CierreInventario]
--           ,[InventarioSano]
--           ,[InventarioCrítico]
--           ,[MaxRemaGaseraMensual])
--     VALUES
--           (1
--           ,0.54
--           ,GETDATe()
--           ,50
--           ,35
--           ,300000)
--GO

--USE [Sagas.MainModule]
--GO

--INSERT INTO [dbo].[EmpresaContacto]
--           ([IdEmpresa])
--     VALUES
--           (1)
--           GO

--		   USE [Sagas.MainModule]
--GO

--INSERT INTO [dbo].[EmpresaDireccion]
--           ([IdEmpresa]
--           ,[IdPais]
--           ,[IdEstadoRep]         
--           ,[Municipio]
--           ,[CodigoPostal]
--           ,[Colonia]
--           ,[Calle]
--           ,[NumExt]
--           ,[NumInt])
--     VALUES
--           (1
--           ,1
--           ,1           
--           ,'Ags'
--           ,'20000'
--           ,'Centro'
--           ,'Zaragosa'
--           ,'1'
--           ,'N/A')
--GO

--USE [Sagas.MainModule]
--GO

--INSERT INTO [dbo].[EmpresaFiscal]
--           ([IdEmpresa]
--           ,[Rfc]
--           ,[RazonSocial])
--     VALUES
--           (1
--           ,'RZGMG1212'
--           ,'Gas Mundial de Guerrero ')
--GO

--USE [Sagas.MainModule]
--GO

--INSERT INTO [dbo].[EmpresaImagen]
--           ([IdEmpresa])
--     VALUES
--           (1)

--GO




