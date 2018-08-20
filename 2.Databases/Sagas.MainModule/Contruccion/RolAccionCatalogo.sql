CREATE TABLE [dbo].[RolAccionCatalogo]
(
	[IdRol] SMALLINT NOT NULL PRIMARY KEY, 
    [CatInsertarUsuario] BIT NOT NULL DEFAULT 0, 
    [CatModificarUsuario] BIT NOT NULL DEFAULT 0, 
    [CatEliminarUsuario] BIT NOT NULL DEFAULT 0, 
    [CatConsultarUsuario] BIT NOT NULL DEFAULT 0,
	[CatInsertarProveedor] BIT NOT NULL DEFAULT 0, 
    [CatModificarProveedor] BIT NOT NULL DEFAULT 0, 
    [CatEliminarProveedor] BIT NOT NULL DEFAULT 0, 
    [CatConsultarProveedor] BIT NOT NULL DEFAULT 0,
	[CatInsertarProducto] BIT NOT NULL DEFAULT 0, 
    [CatModificarProducto] BIT NOT NULL DEFAULT 0, 
    [CatEliminarProducto] BIT NOT NULL DEFAULT 0, 
    [CatConsultarProducto] BIT NOT NULL DEFAULT 0,
	[CatInsertarCuentaContable] BIT NOT NULL DEFAULT 0, 
    [CatModificarCuentaContable] BIT NOT NULL DEFAULT 0, 
    [CatEliminarCuentaContable] BIT NOT NULL DEFAULT 0, 
    [CatConsultarCuentaContable] BIT NOT NULL DEFAULT 0
)
