using Application.MainModule.DTOs.Seguridad;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.AccesoADatos;
using Exceptions.MainModule.Validaciones;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Seguridad
{
    public static class RolServicio
    {
        public static List<Rol> ObtenerRoles(short idEmpresa)
        {
            return new RolDataAccess().Buscar(idEmpresa);
        }

        public static List<Rol> ObtenerRoles(Empresa empresa)
        {
            if (empresa != null)
                if (empresa.Roles != null && empresa.Roles.Count > 0)
                    return empresa.Roles.ToList();

            return ObtenerRoles(empresa.IdEmpresa);
        }

        public static Rol Obtener(UsuarioRol usrRol)
        {
            if (usrRol != null)
                if (usrRol.Role != null)
                    return usrRol.Role;

            return Obtener(usrRol.IdRol);
        }

        public static List<RolDto> ListaAllRoles(short idEmpresa)
        {
            List<RolDto> lRoles = AdaptadoresDTO.Seguridad.RolAdapter.ToDTORoles(new RolDataAccess().Buscar(idEmpresa));
            return lRoles;
        }

        public static RespuestaDto AltaRol(Rol rol)
        {
            return new RolDataAccess().Insertar(rol);
        }

        public static Rol Obtener(short idRol)
        {
            return new RolDataAccess().BuscarIdRol(idRol);
        }

        public static UsuarioRol Obtener(short idRol, int us)
        {
            return new RolDataAccess().Buscar(idRol, us);
        }
        public static RespuestaDto ExisteRol(Usuario usuario, Rol rol, string type)
        {
            var userrol = RolServicio.Obtener(rol.IdRol, usuario.IdUsuario);

            if (userrol != null)//(usuario.UsuarioRoles.Contains(userRol))//(usuario.Roles.Contains(rol))               
            {
                if (type == "alta")
                {
                    string mensaje = string.Format(Error.ContieneRol, "El usuario", rol.NombreRol);

                    return new RespuestaDto()
                    {
                        Exito = false,
                        Mensaje = mensaje,
                        MensajesError = new List<string>() { mensaje },
                    };
                }
                else
                {
                    string mensaje = "Operacion exitosa";
                    return new RespuestaDto()
                    {
                        Exito = true,
                        Mensaje = mensaje,
                        Id = rol.IdRol,
                        MensajesError = new List<string>() { mensaje },
                    };
                }
            }
            else
            {
                return new RespuestaDto()
                {
                    Exito = true,
                };
            }
            //return new RespuestaDto();
        }

        public static RespuestaDto Actualizar(Rol rol)
        {
            return new RolDataAccess().Actualizar(rol);
        }

        public static RespuestaDto Actualizar(List<Rol> rol)
        {
            return new RolDataAccess().Actualizar(rol);
        }

        public static RespuestaDto NoExiste()
        {
            string mensaje = string.Format(Error.NoExiste, "El Rol");

            return new RespuestaDto()
            {
                ModeloValido = true,
                Mensaje = mensaje,
                MensajesError = new List<string>() { mensaje },
            };
        }

        public static MenuDto CrearMenu(int idUsuario)
        {
            //List<MenuDto> lista = new List<MenuDto>();
            MenuDto lista = new MenuDto();
            var usuario = new UsuarioDataAccess().Buscar(idUsuario);
            if (usuario.EsSuperAdmin)
            {
                lista = setTrue();
            }
            else
            {
                foreach (var rol in usuario.Roles)
                {
                    if (rol.FacturasFacturar || rol.FacturasVerFacturas)
                    {
                        lista.Facturacion = true;
                    }
                    if (rol.RequisicionVerRequisiciones || rol.RequisicionAutorizar || rol.RequisicionGenerarNueva || rol.RequisicionRevisarExistencia)
                    {
                        lista.Requisicion = true;
                    }
                    if (rol.CobranzaVerAbonos || rol.CobranzaVerCreditoRecuperado || rol.CobranzaVerCartera || rol.CobranzaVerCreditoRecuperado || rol.CobranzaConsultarFactura || rol.CobranzaFacturar)
                    {
                        lista.CreditoCobranza = true;
                        if (rol.CobranzaVerAbonos)
                            lista.CreditoCobranza = true;

                        if (rol.CobranzaVerCreditoRecuperado)
                            lista.CCRecuperado = true;

                        if (rol.CobranzaVerCartera)
                            lista.CCVencida = true;

                        if (rol.CobranzaVerCreditoRecuperado)
                            lista.CCRecuperado = true;

                        if (rol.CobranzaConsultarFactura || rol.CobranzaFacturar)
                            lista.CCFacturaGlobal = true;

                    }
                    if (rol.PedidoVerPedidos || rol.PedidoGenerarPedido || rol.PedidoEliminarPedido || rol.PedidoModificarPedido)
                    {
                        lista.CallCenter = true;

                        if (rol.PedidoVerPedidos)
                            lista.CCVencida = true;

                        if (rol.PedidoGenerarPedido)
                            lista.CCRecuperado = true;

                        if (rol.PedidoEliminarPedido)
                            lista.CCFacturaGlobal = true;

                        if (rol.PedidoModificarPedido)
                            lista.CCRecuperado = true;
                    }
                    if (rol.CatInsertarCuentaContable || rol.CatConsultarPuntoVenta || rol.CatConsultarPrecioVentaGas || rol.PedidoModificarPedido
                        || rol.RequisicionGenerarNueva || rol.CompraGenerarOCompra || rol.ETRegistrarParqueVehicular || rol.CatLiquidarCajaGeneral || rol.CatConsultarCajaGeneral)
                    {
                        lista.Reportes = true;

                        if (rol.CatInsertarCuentaContable)
                            lista.ReporteCuentasXPagar = true;

                        if (rol.CatConsultarPuntoVenta)
                            lista.ReportePuntoVenta = true;

                        if (rol.CatConsultarPrecioVentaGas)
                            lista.ReportePrecioVenta = true;

                        if (rol.PedidoModificarPedido)
                            lista.ReporteCallCenter = true;

                        if (rol.RequisicionGenerarNueva)
                            lista.ReporteRequisicion = true;

                        if (rol.CompraGenerarOCompra)
                            lista.ReporteOrdenCompra = true;

                        if (rol.ETRegistrarParqueVehicular)
                            lista.ReporteRendimientoVehicular = true;

                        if (rol.CatLiquidarCajaGeneral || rol.CatConsultarCajaGeneral)
                            lista.ReporteCorteCaja = true;
                    }
                    if (rol.ETRegistrarParqueVehicular || rol.ETConsultarParqueVehicular || rol.ETAsignarVehiculo || rol.ETConsultarAsignarVehiculo
                        || rol.ETBorrarAsignacionVehicular || rol.ETRegistrarMantenimiento || rol.ETBorrarMantenimiento || rol.ETRegistrarRecargaCombustible)
                    {
                        lista.EquipooTransporte = true;
                        if (rol.ETRegistrarParqueVehicular || rol.ETConsultarParqueVehicular)
                            lista.ETParqueVehicular = true;

                        if (rol.ETAsignarVehiculo || rol.ETConsultarAsignarVehiculo || rol.ETBorrarAsignacionVehicular)
                            lista.ETAsignacion = true;

                        if (rol.ETRegistrarMantenimiento || rol.ETBorrarMantenimiento)
                            lista.ETMantenimiento = true;

                        if (rol.ETRegistrarRecargaCombustible)
                            lista.ETRecargaC = true;
                    }


                    if (rol.CompraGenerarOCompra || rol.CompraAutorizarOCompra || rol.CompraVerOCompra)
                    {
                        lista.CompraOrdenCompra = true;
                    }

                    if (rol.ConsultarRemanenteGeneral)
                    {
                        lista.Remanente = true;
                    }
                    if (rol.AlmacenActualizaExistencias || rol.AlmacenRegistrarAlmacen || rol.AlmacenVerProductos)
                    {
                        lista.Almacen = true;
                    }
                    if (rol.CatConsultarCajaGeneral || rol.CatLiquidarCajaGeneral)
                    {
                        lista.VentasCajaGeneral = true;
                    }

                    if (rol.HistoricoVentas || rol.HVCargaInformacion)
                    {
                        lista.Ventas = true;
                    }
                    ///CAtalogos
                    if (rol.CatInsertarProducto || rol.CatModificarProducto || rol.CatEliminarProducto || rol.CatConsultarProducto  //Productos
                      || rol.CatInsertarCentroCosto || rol.CatModificarCentroCosto || rol.CatEliminarCentroCosto || rol.CatConsultarCentroCosto //CentroCosto
                      || rol.CatInsertarCuentaContable || rol.CatModificarCuentaContable || rol.CatEliminarCuentaContable || rol.CatConsultarCuentaContable //CuentaContable
                      || rol.CatInsertarProveedor || rol.CatModificarProveedor || rol.CatEliminarProveedor || rol.CatConsultarProveedor //Proveedor
                      || rol.CatInsertarEmpresa || rol.CatModificarEmpresa || rol.CatEliminarEmpresa //Gaseras
                      || rol.CatInsertarUsuario || rol.CatModificarUsuario || rol.CatEliminarUsuario //Usuario
                      || rol.CatInsertarRol || rol.CatModificarRol || rol.CatEliminarRol  //Rol
                      || rol.CatConsultarCliente || rol.CatInsertarCliente || rol.CatModificarCliente || rol.CatEliminarCliente //Cliente
                      || rol.CatConsultarPuntoVenta || rol.CatEliminarPuntoVenta //PuntoVenta 
                      || rol.CatConsultarPrecioVentaGas || rol.CatInsertarPrecioVentaGas || rol.CatModificarPrecioVentaGas || rol.CatEliminarPrecioVentaGas//PuntoVenta
                    )
                    {
                        lista.Catalogos = true;

                        if (rol.CatInsertarProducto || rol.CatModificarProducto || rol.CatEliminarProducto || rol.CatConsultarProducto)
                            lista.CatProducto = true;

                        if (rol.CatInsertarCentroCosto || rol.CatModificarCentroCosto || rol.CatEliminarCentroCosto || rol.CatConsultarCentroCosto)
                            lista.CatCentroCosto = true;

                        if (rol.CatInsertarCuentaContable || rol.CatModificarCuentaContable || rol.CatEliminarCuentaContable || rol.CatConsultarCuentaContable)
                            lista.CatCuentaContable = true;

                        if (rol.CatInsertarProveedor || rol.CatModificarProveedor || rol.CatEliminarProveedor || rol.CatConsultarProveedor)
                            lista.CatProveedor = true;

                        if (rol.CatInsertarEmpresa || rol.CatModificarEmpresa || rol.CatEliminarEmpresa)
                            lista.CatGaseras = true;

                        if (rol.CatInsertarUsuario || rol.CatModificarUsuario || rol.CatEliminarUsuario)
                            lista.CatUsuarios = true;

                        if (rol.CatInsertarRol || rol.CatModificarRol || rol.CatEliminarRol)
                            lista.CatRoles = true;

                        if (rol.CatConsultarCliente || rol.CatInsertarCliente || rol.CatModificarCliente || rol.CatEliminarCliente)
                            lista.CatClientes = true;

                        if (rol.CatConsultarPuntoVenta || rol.CatEliminarPuntoVenta)
                            lista.CatPuntosVenta = true;

                        if (rol.CatConsultarPrecioVentaGas || rol.CatInsertarPrecioVentaGas || rol.CatModificarPrecioVentaGas || rol.CatEliminarPrecioVentaGas)
                            lista.CatPrecioVenta = true;
                    }
                }
            }
            return lista;
        }

        private static MenuDto setTrue()
        {
            MenuDto lista = new MenuDto();

            lista.Facturacion = true;
            lista.Requisicion = true;
            lista.CCAbonos = true;
            lista.CreditoCobranza = true;
            lista.CCRecuperado = true;
            lista.CCVencida = true;
            lista.CCFacturaGlobal = true;
            lista.CallCenter = true;
            lista.Reportes = true;
            lista.ReporteCuentasXPagar = true;
            lista.ReportePuntoVenta = true;
            lista.ReportePrecioVenta = true;
            lista.ReporteCallCenter = true;
            lista.ReporteRequisicion = true;
            lista.ReporteOrdenCompra = true;
            lista.ReporteRendimientoVehicular = true;
            lista.ReporteInvConcepto = true;
            lista.ReporteHistVentas = true;
            lista.ReporteCorteCaja = true;
            lista.ReporteGastoVehiculo = true;
            lista.EquipooTransporte = true;
            lista.ETParqueVehicular = true;
            lista.ETRecargaC = true;
            lista.ETAsignacion = true;
            lista.ETMantenimiento = true;
            lista.CompraOrdenCompra = true;
            lista.Remanente = true;
            lista.Almacen = true;
            lista.AlmacenExistencia = true;
            lista.AlmacenES = true;
            lista.AlmacenSalidaMercancia = true;
            lista.Ventas = true;
            lista.VentasCajaGeneral = true;
            lista.VentasCajaGeneralEst = true;
            lista.HVCargaInfo = true;
            lista.HistoricoVentas = true;
            lista.Catalogos = true;
            lista.CatProducto = true;
            lista.CatCentroCosto = true;
            lista.CatCuentaContable = true;
            lista.CatProveedor = true;
            lista.CatGaseras = true;
            lista.CatEgresos = true;
            lista.CatUsuarios = true;
            lista.CatRoles = true;
            lista.CatClientes = true;
            lista.CatCombustibles = true;
            lista.CatPuntosVenta = true;
            lista.CatPrecioVenta = true;
            lista.CatPrecioVentaO = true;

            return lista;
        }
    }
}
