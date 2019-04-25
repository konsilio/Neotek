using Application.MainModule.DTOs.Respuesta;
using Exceptions.MainModule.Validaciones;
using Sagas.MainModule.Entidades;
using Application.MainModule.Servicios.Historico;
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

        public static RespuestaDto PuedeConsultarUsuario()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CatConsultarUsuario).ToList();

            return EvaluarPermiso(roles, Error.P0001, "Usuario");
        }

        public static RespuestaDto PuedeRegistrarUsuario()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CatInsertarUsuario).ToList();

            return EvaluarPermiso(roles, Error.P0001, "Usuario");
        }

        public static RespuestaDto PuedeModificarUsuario()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CatModificarUsuario).ToList();

            return EvaluarPermiso(roles, Error.P0002, "Usuario");
        }

        public static RespuestaDto PuedeEliminarUsuario()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CatEliminarUsuario).ToList();

            return EvaluarPermiso(roles, Error.P0003, "Usuario");
        } 
        #endregion

        #region Empresa
        public static RespuestaDto PuedeConsultarEmpresa()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CatConsultarUsuario).ToList();

            return EvaluarPermiso(roles, Error.P0001, "Empresa");
        }
        public static RespuestaDto PuedeRegistrarEmpresa()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CatInsertarEmpresa).ToList();

            return EvaluarPermiso(roles, Error.P0001, "Empresa");
        }

        public static RespuestaDto PuedeModificarEmpresa()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CatModificarEmpresa).ToList();

            return EvaluarPermiso(roles, Error.P0002, "Empresa");
        }

        public static RespuestaDto PuedeEliminarEmpresa()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CatEliminarEmpresa).ToList();

            return EvaluarPermiso(roles, Error.P0003, "Empresa");
        }
        #endregion

        #region Rol
        public static RespuestaDto PuedeConsultarRol()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CatConsultarUsuario).ToList();

            return EvaluarPermiso(roles, Error.P0001, "Rol");
        }

        public static RespuestaDto PuedeRegistrarRol()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CatInsertarRol).ToList();

            return EvaluarPermiso(roles, Error.P0001, "Rol");
        }

        public static RespuestaDto PuedeModificarRol()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CatModificarRol).ToList();

            return EvaluarPermiso(roles, Error.P0002, "Rol");
        }

        public static RespuestaDto PuedeEliminarRol()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CatEliminarRol).ToList();

            return EvaluarPermiso(roles, Error.P0003, "Rol");
        }
        #endregion

        #region Clientes
        public static RespuestaDto PuedeConsultarCliente()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CatConsultarCliente).ToList();

            return EvaluarPermiso(roles, Error.P0001, "Rol");
        }
        public static RespuestaDto PuedeRegistrarCliente()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CatInsertarCliente).ToList();

            return EvaluarPermiso(roles, Error.P0001, "Cliente");
        }

        public static RespuestaDto PuedeModificarCliente()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CatModificarCliente).ToList();

            return EvaluarPermiso(roles, Error.P0002, "Cliente");
        }

        public static RespuestaDto PuedeEliminarCliente()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CatEliminarCliente).ToList();

            return EvaluarPermiso(roles, Error.P0003, "Cliente");
        }
        #endregion

        #region Puntos de Venta
        public static RespuestaDto PuedeConsultarPuntoVenta()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CatConsultarPuntoVenta).ToList();

            return EvaluarPermiso(roles, Error.P0001, "PuntoVenta");
        }

        public static RespuestaDto PuedeEliminarPuntoVenta()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CatEliminarPuntoVenta).ToList();

            return EvaluarPermiso(roles, Error.P0003, "PuntoVenta");
        }

        #endregion

        #region Precio Venta

        public static RespuestaDto PuedeConsultarPrecioVentaGas()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CatConsultarPrecioVentaGas).ToList();

            return EvaluarPermiso(roles, Error.P0001, "PrecioVentaGas");
        }
        public static RespuestaDto PuedeRegistrarPrecioVentaGas()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CatInsertarPrecioVentaGas).ToList();

            return EvaluarPermiso(roles, Error.P0001, "PrecioVentaGas");
        }

        public static RespuestaDto PuedeModificarPrecioVentaGas()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CatModificarPrecioVentaGas).ToList();

            return EvaluarPermiso(roles, Error.P0002, "PrecioVentaGas");
        }

        public static RespuestaDto PuedeEliminarPrecioVentaGas()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CatEliminarPrecioVentaGas).ToList();

            return EvaluarPermiso(roles, Error.P0003, "PrecioVentaGas");
        }

        #endregion

        #region CajaGeneral

        public static RespuestaDto PuedeConsultarCajaGeneral()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CatConsultarCliente).ToList();

            return EvaluarPermiso(roles, Error.P0001, "CajaGeneral");
        }
        public static RespuestaDto PuedeRegistrarCajaGeneral()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CatInsertarUsuario).ToList();

            return EvaluarPermiso(roles, Error.P0001, "CajaGeneral");
        }

        public static RespuestaDto PuedeModificarCajaGeneral()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CatModificarUsuario).ToList();

            return EvaluarPermiso(roles, Error.P0002, "CajaGeneral");
        }

        public static RespuestaDto PuedeEliminarCajaGeneral()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CatEliminarUsuario).ToList();

            return EvaluarPermiso(roles, Error.P0003, "CajaGeneral");
        }

        #endregion

        #region Proveedor

        public static RespuestaDto PuedeRegistrarProveedor()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CatInsertarProveedor).ToList();

            return EvaluarPermiso(roles, Error.P0001, "Proveedores");
        }

        public static RespuestaDto PuedeModificarProveedor()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CatModificarProveedor).ToList();

            return EvaluarPermiso(roles, Error.P0002, "Proveedores");
        }

        public static RespuestaDto PuedeEliminarProveedor()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CatEliminarProveedor).ToList();

            return EvaluarPermiso(roles, Error.P0003, "Proveedores");
        }

        public static RespuestaDto PuedeConsultarProveedor()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CatConsultarProveedor).ToList();

            return EvaluarPermiso(roles, Error.P0004, "Proveedores");
        }
        #endregion

        #region Producto
        public static RespuestaDto PuedeRegistrarProducto()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CatInsertarProducto).ToList();

            return EvaluarPermiso(roles, Error.P0001, "productos y; sus categorías, líneas y unidades de medida");
        }

        public static RespuestaDto PuedeModificarProducto()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CatModificarProducto).ToList();

            return EvaluarPermiso(roles, Error.P0002, "productos y; sus categorías, líneas y unidades de medida");
        }

        public static RespuestaDto PuedeEliminarProducto()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CatEliminarProducto).ToList();

            return EvaluarPermiso(roles, Error.P0003, "productos y; sus categorías, líneas y unidades de medida");
        }

        public static RespuestaDto PuedeConsultarProducto()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CatConsultarProducto).ToList();

            return EvaluarPermiso(roles, Error.P0004, "productos y; sus categorías, líneas y unidades de medida");
        }
        #endregion

        #region Centro Costo
        public static RespuestaDto PuedeRegistrarCentroCosto()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CatInsertarCentroCosto).ToList();

            return EvaluarPermiso(roles, Error.P0001, "un centro de costo");
        }

        public static RespuestaDto PuedeModificarCentroCosto()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CatModificarCentroCosto).ToList();

            return EvaluarPermiso(roles, Error.P0002, "un centro de costo");
        }

        public static RespuestaDto PuedeEliminarCentroCosto()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CatEliminarCentroCosto).ToList();

            return EvaluarPermiso(roles, Error.P0003, "un centro de costo");
        }

        public static RespuestaDto PuedeConsultarCentroCosto()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CatConsultarCentroCosto).ToList();

            return EvaluarPermiso(roles, Error.P0004, "los centros de costo");
        }
        #endregion

        #region Cuenta Contable
        public static RespuestaDto PuedeRegistrarCuentaContable()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CatInsertarCuentaContable).ToList();

            return EvaluarPermiso(roles, Error.P0001, "una cuenta contable");
        }

        public static RespuestaDto PuedeModificarCuentaContable()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CatModificarCuentaContable).ToList();

            return EvaluarPermiso(roles, Error.P0002, "una cuenta contable");
        }

        public static RespuestaDto PuedeEliminarCuentaContable()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CatEliminarCuentaContable).ToList();

            return EvaluarPermiso(roles, Error.P0003, "una cuenta contable");
        }

        public static RespuestaDto PuedeConsultarCuentaContable()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CatConsultarCuentaContable).ToList();

            return EvaluarPermiso(roles, Error.P0004, "las cuentas contables");
        }
        #endregion

        #region Orden de Compra
        public static RespuestaDto PuedeRegistrarOrdenCompra()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CompraGenerarOCompra).ToList();

            return EvaluarPermiso(roles, Error.P0001, "Orden de Compra");
        }
        public static RespuestaDto PuedeAutorizarOrdenCompra()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CompraAutorizarOCompra).ToList();

            return EvaluarPermiso(roles, Error.P0002, "Orden de Compra");
        }
        public static RespuestaDto PuedeConsultarOrdenCompra()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CompraVerOCompra).ToList();

            return EvaluarPermiso(roles, Error.P0004, "Orden de Compra");
        }

        //public static RespuestaDto PuedeEliminarOrdenCompra()
        //{
        //    var usuario = UsuarioAplicacionServicio.Obtener();
        //    var rol = usuario.Roles.FirstOrDefault(x => x.Compra);

        //    return EvaluarPermiso(rol, Error.P0003, "Proveedores");
        //}

        #endregion

        #region Pedido
        public static RespuestaDto PuedeRegistrarPedido()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.PedidoGenerarPedido).ToList();

            return EvaluarPermiso(roles, Error.P0001, "Pedidos");
        }
        public static RespuestaDto PuedeConsultarPedido()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.PedidoVerPedidos).ToList();

            return EvaluarPermiso(roles, Error.P0001, "Pedidos");
        }

        public static RespuestaDto PuedeModificarPedido()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.PedidoModificarPedido).ToList();

            return EvaluarPermiso(roles, Error.P0001, "Pedidos");
        }
        public static RespuestaDto PuedeEliminarPedido()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.PedidoEliminarPedido).ToList();

            return EvaluarPermiso(roles, Error.P0001, "Pedidos");
        }
        #endregion

        #region Abonos
        public static RespuestaDto PuedeRegistrarAbonos()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CobranzaGenerarAbonos).ToList();

            return EvaluarPermiso(roles, Error.P0001, "Abonos");
        }
        public static RespuestaDto PuedeConsultarCarteraVencida()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CobranzaVerCartera).ToList();

            return EvaluarPermiso(roles, Error.P0001, "Cartera Vencida");
        }
        public static RespuestaDto PuedeConsultarAbonos()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CobranzaVerAbonos).ToList();

            return EvaluarPermiso(roles, Error.P0001, "Abonos");
        }
        public static RespuestaDto PuedeConsultarCreditoRecuperado()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CobranzaVerCreditoRecuperado).ToList();

            return EvaluarPermiso(roles, Error.P0001, "C Recuperado");
        }

        #endregion

        #region EquipoTransporte
        public static RespuestaDto PuedeRegistrarParqueVehicular()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CatInsertarEmpresa).ToList();

            return EvaluarPermiso(roles, Error.P0001, "EquipoTransporte");
        }
        public static RespuestaDto PuedeConsultarParqueVehicular()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CatConsultarEmpresa).ToList();

            return EvaluarPermiso(roles, Error.P0001, "EquipoTransporte");
        }
        public static RespuestaDto PuedeAsignarVehiculo()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CatInsertarEmpresa).ToList();

            return EvaluarPermiso(roles, Error.P0001, "Asignacion Vehicular");
        }
        public static RespuestaDto PuedeConsultarAsignacionVehicular()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CatConsultarEmpresa).ToList();

            return EvaluarPermiso(roles, Error.P0001, "Asignacion Vehicular");
        }
        public static RespuestaDto PuedeBorrarAsignacionVehicular()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CatConsultarEmpresa).ToList();

            return EvaluarPermiso(roles, Error.P0001, "Asignacion Vehicular");
        }

        public static RespuestaDto PuedeRegistrarMantenimiento()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CatConsultarEmpresa).ToList();

            return EvaluarPermiso(roles, Error.P0001, "Mantenimiento Vehicular");
        }
        public static RespuestaDto PuedeBorrarMantenimiento()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CatConsultarEmpresa).ToList();

            return EvaluarPermiso(roles, Error.P0001, "Mantenimiento Vehicular");
        }
        public static RespuestaDto PuedeRegistrarRecargaCombustible()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CatConsultarEmpresa).ToList();

            return EvaluarPermiso(roles, Error.P0001, "Recarga de combustible Vehicular");
        }
        public static RespuestaDto PuedeBorrarRecargaCombustible()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.CatConsultarEmpresa).ToList();

            return EvaluarPermiso(roles, Error.P0001, "Recarga de combustible Vehicular");
        }
        #endregion

        #region HistoricoVentas

        public static RespuestaDto HistoricoVentas()
        {
            var usuario = UsuarioAplicacionServicio.Obtener();
            var roles = usuario.UsuarioRoles.Where(x => x.Role.HistoricoVentas).ToList();

            return EvaluarPermiso(roles, Error.P0001, "Usuario");
        }

        #endregion

        private static RespuestaDto EvaluarPermiso(List<UsuarioRol> roles, string error, string format = "")
        {
            if (roles == null & roles.Count <= 0)
            {
                string mensaje = string.Format(error, format);
                return new RespuestaDto()
                {
                    Mensaje = mensaje,
                    MensajesError = new List<string>() { mensaje }
                };
            }
            return new RespuestaDto() { Exito = true };
        }
    }
}
