--Ejecutar este escript despues de haber creado las tablas de la carpeta de Constructor

--USE [Sagas.MainModule]
--GO

--SET IDENTITY_INSERT [dbo].[CPais] ON 
--INSERT [dbo].[CPais] ([Pais], [FechaRegistro]) VALUES ( N'México', CAST(0x0000A923008A969C AS DateTime))
--SET IDENTITY_INSERT [dbo].[CPais] OFF
--SET IDENTITY_INSERT [dbo].[CEstadosRep] ON 
--INSERT [dbo].[CEstadosRep] ([Estado], [Abreviatura], [FechaRegistro]) VALUES (N'Aguascalientes', N'Ags.', CAST(0x0000A923008966F1 AS DateTime))
--SET IDENTITY_INSERT [dbo].[CEstadosRep] OFF

--SET IDENTITY_INSERT [dbo].[AdministracionCentral] ON 
--INSERT [dbo].[AdministracionCentral] ( [NombreComercial], [FechaRegistro]) VALUES ( N'Grupo Also', CAST(0x0000A923008902B4 AS DateTime))
--SET IDENTITY_INSERT [dbo].[AdministracionCentral] OFF

--INSERT [dbo].[AdministracionCentralDireccion] ([IdAdministracionCentral], [IdPais], [IdEstadoRep], [EstadoProvincia], [Municipio], [CodigoPostal], [Colonia], [Calle], [NumExt], [NumInt]) VALUES (1, 1, 1, NULL, N'Acapúlco', N'46561', N'Las trojes', N'lozano garza', N'201', NULL)
--INSERT [dbo].[AdministracionCetralContacto] ([IdAdministracionCentral], [Telefono1], [Telefono2], [Telefono3], [Celular1], [Celular2], [Celular3], [Email1], [Email2], [Email3], [SitioWeb1], [SitioWeb2], [SitioWeb3]) VALUES (1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
--INSERT [dbo].[AdministracionCentralFiscal] ([IdAdministracionCentral], [Rfc], [RazonSocial]) VALUES (1, N'SAHK8504092M5', N'Grupo Also S.A. de C.V.')
--INSERT [dbo].[AdministracionCentralImagen] ([IdAdministracionCentral], [UrlLogotipoMenu], [UrlLogotipoLogin]) VALUES (1, NULL, NULL)

--SET IDENTITY_INSERT [dbo].[Rol] ON 
--INSERT [dbo].[Rol] ([IdRol], [Rol], [NombreRol], [Activo], [FechaRegistro]) VALUES (1, N'Super Usuario', N'Super Usuario', 1, CAST(0x0000A923008BE991 AS DateTime))
--SET IDENTITY_INSERT [dbo].[Rol] OFF

--SET IDENTITY_INSERT [dbo].[UsuarioAC] ON 
--INSERT [dbo].[UsuarioAC] ([IdUsuarioAC], [IdAdministracionCentral], [IdRol], [Nombre], [Apellido1], [Apellido2], [SuperAdmin], [Activo], [FechaRegistro]) VALUES (1, 1, 1, N'Super Usuario', N'Super Usuario', N'Super Usuario', 1, 1, CAST(0x0000A923008C14B1 AS DateTime))
--SET IDENTITY_INSERT [dbo].[UsuarioAC] OFF

--INSERT [dbo].[UsuarioACCredencial] ([IdUsuarioAC], [Usuario], [Password]) VALUES (1, N'sa', N'saadmin')
--INSERT [dbo].[UsuarioACDireccion] ([IdUsuarioAC], [IdPais], [IdEstadoRep], [EstadoProvincia], [Municipio], [CodigoPostal], [Colonia], [Calle], [NumExt], [NumInt]) VALUES (1, 1, 1, NULL, N'Acapúlco', N'46543', N'Las Trojes', N'Lazano garza', N'205', NULL)
--INSERT [dbo].[UsuarioACContacto] ([IdUsuarioAC], [Telefono1], [Telefono2], [Telefono3], [Celular1], [Celular2], [Celular3], [Email1], [Email2], [Email3], [SitioWeb1], [SitioWeb2], [SitioWeb3]) VALUES (1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
