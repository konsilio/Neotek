﻿using Application.MainModule.DTOs.Respuesta;
using Exceptions.MainModule.Validaciones;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Seguridad
{
    public static class PermisosServicio
    {
        #region Usuario
        public static RespuestaDto PuedeRegistrarUsuario()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var rol = usuario.Roles.FirstOrDefault(x => x.CatInsertarUsuario);

            return EvaluarPermiso(rol, Error.P0001, "Usuario");
        }

        public static RespuestaDto PuedeModificarUsuario()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var rol = usuario.Roles.FirstOrDefault(x => x.CatModificarUsuario);

            return EvaluarPermiso(rol, Error.P0002, "Usuario");
        }

        public static RespuestaDto PuedeEliminarUsuario()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var rol = usuario.Roles.FirstOrDefault(x => x.CatEliminarUsuario);

            return EvaluarPermiso(rol, Error.P0003, "Usuario");
        }
        #endregion

        #region Empresa
        public static RespuestaDto PuedeRegistrarEmpresa()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var rol = usuario.Roles.FirstOrDefault(x => x.CatInsertarUsuario);//CatInsertarEmpresa

            return EvaluarPermiso(rol, Error.P0001, "Empresa");
        }

        public static RespuestaDto PuedeModificarEmpresa()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var rol = usuario.Roles.FirstOrDefault(x => x.CatModificarUsuario);//CatModificarEmpresa

            return EvaluarPermiso(rol, Error.P0002, "Empresa");
        }

        public static RespuestaDto PuedeEliminarEmpresa()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var rol = usuario.Roles.FirstOrDefault(x => x.CatEliminarUsuario);//CatEliminarEmpresa

            return EvaluarPermiso(rol, Error.P0003, "Empresa");
        }
        #endregion

        #region Proveedor
        public static RespuestaDto PuedeRegistrarProveedor()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var rol = usuario.Roles.FirstOrDefault(x => x.CatInsertarProveedor);

            return EvaluarPermiso(rol, Error.P0001, "Proveedores");
        }

        public static RespuestaDto PuedeModificarProveedor()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var rol = usuario.Roles.FirstOrDefault(x => x.CatModificarProveedor);

            return EvaluarPermiso(rol, Error.P0002, "Proveedores");
        }

        public static RespuestaDto PuedeEliminarProveedor()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var rol = usuario.Roles.FirstOrDefault(x => x.CatEliminarProveedor);

            return EvaluarPermiso(rol, Error.P0003, "Proveedores");
        }

        public static RespuestaDto PuedeConsultarProveedor()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var rol = usuario.Roles.FirstOrDefault(x => x.CatConsultarProveedor);

            return EvaluarPermiso(rol, Error.P0004, "Proveedores");
        }
        #endregion

        #region Producto
        public static RespuestaDto PuedeRegistrarProducto()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var rol = usuario.Roles.FirstOrDefault(x => x.CatInsertarProducto);

            return EvaluarPermiso(rol, Error.P0001, "productos y; sus categorías, líneas y unidades de medida");
        }

        public static RespuestaDto PuedeModificarProducto()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var rol = usuario.Roles.FirstOrDefault(x => x.CatModificarProducto);

            return EvaluarPermiso(rol, Error.P0002, "productos y; sus categorías, líneas y unidades de medida");
        }

        public static RespuestaDto PuedeEliminarProducto()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var rol = usuario.Roles.FirstOrDefault(x => x.CatEliminarProducto);

            return EvaluarPermiso(rol, Error.P0003, "productos y; sus categorías, líneas y unidades de medida");
        }

        public static RespuestaDto PuedeConsultarProducto()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var rol = usuario.Roles.FirstOrDefault(x => x.CatConsultarProducto);

            return EvaluarPermiso(rol, Error.P0004, "productos y; sus categorías, líneas y unidades de medida");
        }
        #endregion

        #region Centro Costo
        public static RespuestaDto PuedeRegistrarCentroCosto()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var rol = usuario.Roles.FirstOrDefault(x => x.CatInsertarCentroCosto);

            return EvaluarPermiso(rol, Error.P0001, "un centro de costo");
        }

        public static RespuestaDto PuedeModificarCentroCosto()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var rol = usuario.Roles.FirstOrDefault(x => x.CatModificarCentroCosto);

            return EvaluarPermiso(rol, Error.P0002, "un centro de costo");
        }

        public static RespuestaDto PuedeEliminarCentroCosto()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var rol = usuario.Roles.FirstOrDefault(x => x.CatEliminarCentroCosto);

            return EvaluarPermiso(rol, Error.P0003, "un centro de costo");
        }

        public static RespuestaDto PuedeConsultarCentroCosto()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var rol = usuario.Roles.FirstOrDefault(x => x.CatConsultarCentroCosto);

            return EvaluarPermiso(rol, Error.P0004, "los centros de costo");
        }
        #endregion

        #region Cuenta Contable
        public static RespuestaDto PuedeRegistrarCuentaContable()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var rol = usuario.Roles.FirstOrDefault(x => x.CatInsertarCuentaContable);

            return EvaluarPermiso(rol, Error.P0001, "una cuenta contable");
        }

        public static RespuestaDto PuedeModificarCuentaContable()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var rol = usuario.Roles.FirstOrDefault(x => x.CatModificarCuentaContable);

            return EvaluarPermiso(rol, Error.P0002, "una cuenta contable");
        }

        public static RespuestaDto PuedeEliminarCuentaContable()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var rol = usuario.Roles.FirstOrDefault(x => x.CatEliminarCuentaContable);

            return EvaluarPermiso(rol, Error.P0003, "una cuenta contable");
        }

        public static RespuestaDto PuedeConsultarCuentaContable()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var rol = usuario.Roles.FirstOrDefault(x => x.CatConsultarCuentaContable);

            return EvaluarPermiso(rol, Error.P0004, "las cuentas contables");
        }
        #endregion

        #region Orden de Compra
        public static RespuestaDto PuedeRegistrarOrdenCompra()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var rol = usuario.Roles.FirstOrDefault(x => x.CompraGenerarOCompra);

            return EvaluarPermiso(rol, Error.P0001, "Orden de Compra");
        }
        public static RespuestaDto PuedeAutorizarOrdenCompra()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var rol = usuario.Roles.FirstOrDefault(x => x.CompraAutorizarOCompra);

            return EvaluarPermiso(rol, Error.P0002, "Orden de Compra");
        }
        public static RespuestaDto PuedeConsultarOrdenCompra()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var rol = usuario.Roles.FirstOrDefault(x => x.CompraVerOCompra);

            return EvaluarPermiso(rol, Error.P0004, "Orden de Compra");
        }

        //public static RespuestaDto PuedeEliminarOrdenCompra()
        //{
        //    var usuario = UsuarioAplicacionServicio.Obtener();
        //    var rol = usuario.Roles.FirstOrDefault(x => x.Compra);

        //    return EvaluarPermiso(rol, Error.P0003, "Proveedores");
        //}

        #endregion

        private static RespuestaDto EvaluarPermiso(Rol rol, string error, string format = "")
        {
            if (rol == null)
            {
                string mensaje = string.Format(error, format);
                return new RespuestaDto()
                {
                    Mensaje = mensaje,
                    MensajesError = new List<string>() { mensaje }
                };
            }
            return new RespuestaDto() {Exito = true };
        }
    }
}
