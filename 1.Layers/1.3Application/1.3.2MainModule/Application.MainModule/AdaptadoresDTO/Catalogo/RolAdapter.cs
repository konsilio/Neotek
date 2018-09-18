using Application.MainModule.DTOs.Catalogo;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO.Catalogo
{
    public class RolAdapter
    {
        public static RolDto ToDTO(Rol rol)
        {
            // return new RolDto()
            RolDto r = new RolDto()
            {
                IdEmpresa = rol.IdEmpresa,
                IdRol = rol.IdRol,
                Rol1 = rol.Rol1,
                NombreRol = rol.NombreRol,
                Activo = rol.Activo,
                FechaRegistro = rol.FechaRegistro,
                RequisicionVerRequisiciones = rol.RequisicionVerRequisiciones,
                RequisicionGenerarNueva = rol.RequisicionGenerarNueva,
                RequisicionRevisarExistencia = rol.RequisicionRevisarExistencia,
                RequisicionAutorizar = rol.RequisicionAutorizar,
                CompraVerOCompra = rol.CompraVerOCompra,
                CompraGenerarOCompra = rol.CompraGenerarOCompra,
                CompraAutorizarOCompra = rol.CompraAutorizarOCompra,
                CompraEntraProductoOCompra = rol.CompraEntraProductoOCompra,
                CompraAtiendeServicioOCompra = rol.CompraAtiendeServicioOCompra,
                CatInsertarUsuario = rol.CatInsertarUsuario,
                CatModificarUsuario = rol.CatModificarUsuario,
                CatEliminarUsuario = rol.CatEliminarUsuario,
                CatConsultarUsuario = rol.CatConsultarUsuario,
                CatInsertarProveedor = rol.CatInsertarProveedor,
                CatModificarProveedor = rol.CatModificarProveedor,
                CatEliminarProveedor = rol.CatEliminarProveedor,
                CatConsultarProveedor = rol.CatConsultarProveedor,
                CatInsertarProducto = rol.CatInsertarProducto,
                CatModificarProducto = rol.CatModificarProducto,
                CatEliminarProducto = rol.CatEliminarProducto,
                CatConsultarProducto = rol.CatConsultarProducto,
                CatInsertarCentroCosto = rol.CatInsertarCentroCosto,
                CatModificarCentroCosto = rol.CatModificarCentroCosto,
                CatEliminarCentroCosto = rol.CatEliminarCentroCosto,
                CatConsultarCentroCosto = rol.CatConsultarCentroCosto,
                CatInsertarCuentaContable = rol.CatInsertarCuentaContable,
                CatModificarCuentaContable = rol.CatModificarCuentaContable,
                CatEliminarCuentaContable = rol.CatEliminarCuentaContable,
                CatConsultarCuentaContable = rol.CatConsultarCuentaContable,
                CatInsertarCliente = rol.CatInsertarCliente,
                CatModificarCliente = rol.CatModificarCliente,
                CatEliminarCliente = rol.CatEliminarCliente,
                CatConsultarCliente = rol.CatConsultarCliente,
                CatAsignarChoferPuntoVenta = rol.CatAsignarChoferPuntoVenta,
                CatEliminarPuntoVenta = rol.CatEliminarPuntoVenta,
                CatConsultarPuntoVenta = rol.CatConsultarPuntoVenta,
                CatAsignarEquipoTransporte = rol.CatAsignarEquipoTransporte,
                CatModificarEquipoTransporte = rol.CatModificarEquipoTransporte,
                CatEliminarEquipoTransporte = rol.CatEliminarEquipoTransporte,
                CatConsultarEquipoTransporte = rol.CatConsultarEquipoTransporte,
                AppCompraVerOCompra = rol.AppCompraVerOCompra,
                AppCompraEntraGas = rol.AppCompraEntraGas,
                AppCompraGasIniciarDescarga = rol.AppCompraGasIniciarDescarga,
                AppCompraGasFinalizarDescarga = rol.AppCompraGasFinalizarDescarga,
                AppAutoconsumoInventarioGral = rol.AppAutoconsumoInventarioGral,
                AppAutoconsumoEstacionCarb = rol.AppAutoconsumoEstacionCarb,
                AppAutoconsumoPipa = rol.AppAutoconsumoPipa,
                AppCalibracionEstacionCarb = rol.AppCalibracionEstacionCarb,
                AppCalibracionPipa = rol.AppCalibracionPipa,
                AppCalibracionCamionetaCilindro = rol.AppCalibracionCamionetaCilindro,
                AppRecargaEstacionCarb = rol.AppRecargaEstacionCarb,
                AppRecargaPipa = rol.AppRecargaPipa,
                AppRecargaCamionetaCilindro = rol.AppRecargaCamionetaCilindro,
                AppTomaLecturaAlmacenPral = rol.AppTomaLecturaAlmacenPral,
                AppTomaLecturaEstacionCarb = rol.AppTomaLecturaEstacionCarb,
                AppTomaLecturaPipa = rol.AppTomaLecturaPipa,
                AppTomaLecturaCamionetaCilindro = rol.AppTomaLecturaCamionetaCilindro,
                AppTomaLecturaReporteDelDia = rol.AppTomaLecturaReporteDelDia,
                AppTraspasoEstacionCarb = rol.AppTraspasoEstacionCarb,
                AppTraspasoPipa = rol.AppTraspasoPipa,
            };

            return r;
        }


        public static List<RolDto> ToDTO(List<Rol> roles)
        {
            return roles.ToList().Select(x => ToDTO(x)).ToList();
        }

      
        //get all roles
        public static RolDto ToDTORol(Rol us)
        {
            RolDto rolDTO = new RolDto()
            {                
                IdRol = us.IdRol,
                Rol1 = us.Rol1,
                NombreRol = us.NombreRol,
                Activo = us.Activo,
                FechaRegistro = us.FechaRegistro,
                IdEmpresa = us.IdEmpresa,

            };

            return rolDTO;
        }

        public static List<RolDto> ToDTORoles(List<Rol> lu)
        {
            List<RolDto> luDTO = lu.ToList().Select(x => ToDTO(x)).ToList();
            return luDTO;
        }


        public static Rol FromDto(RolDto rolDTO)
        {
            return new Rol()
            {
                IdEmpresa = rolDTO.IdEmpresa,
                NombreRol = rolDTO.NombreRol,
                Rol1 = rolDTO.NombreRol,
                Activo = true,
                FechaRegistro = DateTime.Now,
                RequisicionVerRequisiciones = rolDTO.RequisicionVerRequisiciones,
                RequisicionGenerarNueva = rolDTO.RequisicionGenerarNueva,
                RequisicionRevisarExistencia = rolDTO.RequisicionRevisarExistencia,
                RequisicionAutorizar = rolDTO.RequisicionAutorizar,
                CompraVerOCompra = rolDTO.CompraVerOCompra,
                CompraGenerarOCompra = rolDTO.CompraGenerarOCompra,
                CompraAutorizarOCompra = rolDTO.CompraAutorizarOCompra,
                CompraEntraProductoOCompra = rolDTO.CompraEntraProductoOCompra,
                CompraAtiendeServicioOCompra = rolDTO.CompraAtiendeServicioOCompra,
                CatInsertarUsuario = rolDTO.CatInsertarUsuario,
                CatModificarUsuario = rolDTO.CatModificarUsuario,
                CatEliminarUsuario = rolDTO.CatEliminarUsuario,
                CatConsultarUsuario = rolDTO.CatConsultarUsuario,
                CatInsertarProveedor = rolDTO.CatInsertarProveedor,
                CatModificarProveedor = rolDTO.CatModificarProveedor,
                CatEliminarProveedor = rolDTO.CatEliminarProveedor,
                CatConsultarProveedor = rolDTO.CatConsultarProveedor,
                CatInsertarProducto = rolDTO.CatInsertarProducto,
                CatModificarProducto = rolDTO.CatModificarProducto,
                CatEliminarProducto = rolDTO.CatEliminarProducto,
                CatConsultarProducto = rolDTO.CatConsultarProducto,
                CatInsertarCentroCosto = rolDTO.CatInsertarCentroCosto,
                CatModificarCentroCosto = rolDTO.CatModificarCentroCosto,
                CatEliminarCentroCosto = rolDTO.CatEliminarCentroCosto,
                CatConsultarCentroCosto = rolDTO.CatConsultarCentroCosto,
                CatInsertarCuentaContable = rolDTO.CatInsertarCuentaContable,
                CatModificarCuentaContable = rolDTO.CatModificarCuentaContable,
                CatEliminarCuentaContable = rolDTO.CatEliminarCuentaContable,
                CatConsultarCuentaContable = rolDTO.CatConsultarCuentaContable,
                CatInsertarCliente = rolDTO.CatInsertarCliente,
                CatModificarCliente = rolDTO.CatModificarCliente,
                CatEliminarCliente = rolDTO.CatEliminarCliente,
                CatConsultarCliente = rolDTO.CatConsultarCliente,
                CatAsignarChoferPuntoVenta = rolDTO.CatAsignarChoferPuntoVenta,
                CatEliminarPuntoVenta = rolDTO.CatEliminarPuntoVenta,
                CatConsultarPuntoVenta = rolDTO.CatConsultarPuntoVenta,
                CatAsignarEquipoTransporte = rolDTO.CatAsignarEquipoTransporte,
                CatModificarEquipoTransporte = rolDTO.CatModificarEquipoTransporte,
                CatEliminarEquipoTransporte = rolDTO.CatEliminarEquipoTransporte,
                CatConsultarEquipoTransporte = rolDTO.CatConsultarEquipoTransporte,
                AppCompraVerOCompra = rolDTO.AppCompraVerOCompra,
                AppCompraEntraGas = rolDTO.AppCompraEntraGas,
                AppCompraGasIniciarDescarga = rolDTO.AppCompraGasIniciarDescarga,
                AppCompraGasFinalizarDescarga = rolDTO.AppCompraGasFinalizarDescarga,
                AppAutoconsumoInventarioGral = rolDTO.AppAutoconsumoInventarioGral,
                AppAutoconsumoEstacionCarb = rolDTO.AppAutoconsumoEstacionCarb,
                AppAutoconsumoPipa = rolDTO.AppAutoconsumoPipa,
                AppCalibracionEstacionCarb = rolDTO.AppCalibracionEstacionCarb,
                AppCalibracionPipa = rolDTO.AppCalibracionPipa,
                AppCalibracionCamionetaCilindro = rolDTO.AppCalibracionCamionetaCilindro,
                AppRecargaEstacionCarb = rolDTO.AppRecargaEstacionCarb,
                AppRecargaPipa = rolDTO.AppRecargaPipa,
                AppRecargaCamionetaCilindro = rolDTO.AppRecargaCamionetaCilindro,
                AppTomaLecturaAlmacenPral = rolDTO.AppTomaLecturaAlmacenPral,
                AppTomaLecturaEstacionCarb = rolDTO.AppTomaLecturaEstacionCarb,
                AppTomaLecturaPipa = rolDTO.AppTomaLecturaPipa,
                AppTomaLecturaCamionetaCilindro = rolDTO.AppTomaLecturaCamionetaCilindro,
                AppTomaLecturaReporteDelDia = rolDTO.AppTomaLecturaReporteDelDia,
                AppTraspasoEstacionCarb = rolDTO.AppTraspasoEstacionCarb,
                AppTraspasoPipa = rolDTO.AppTraspasoPipa,
            };
        }

        public static Rol FromDtoPermiso(RolDto rolDTO)
        {
            return new Rol()
            {
                IdEmpresa = rolDTO.IdEmpresa,
                NombreRol = rolDTO.NombreRol,
                Rol1 = rolDTO.NombreRol,
                Activo = true,
                FechaRegistro = DateTime.Now,
                IdRol = rolDTO.IdRol,
                RequisicionVerRequisiciones = rolDTO.RequisicionVerRequisiciones,
                RequisicionGenerarNueva = rolDTO.RequisicionGenerarNueva,
                RequisicionRevisarExistencia = rolDTO.RequisicionRevisarExistencia,
                RequisicionAutorizar = rolDTO.RequisicionAutorizar,
                CompraVerOCompra = rolDTO.CompraVerOCompra,
                CompraGenerarOCompra = rolDTO.CompraGenerarOCompra,
                CompraAutorizarOCompra = rolDTO.CompraAutorizarOCompra,
                CompraEntraProductoOCompra = rolDTO.CompraEntraProductoOCompra,
                CompraAtiendeServicioOCompra = rolDTO.CompraAtiendeServicioOCompra,
                CatInsertarUsuario = rolDTO.CatInsertarUsuario,
                CatModificarUsuario = rolDTO.CatModificarUsuario,
                CatEliminarUsuario = rolDTO.CatEliminarUsuario,
                CatConsultarUsuario = rolDTO.CatConsultarUsuario,
                CatInsertarProveedor = rolDTO.CatInsertarProveedor,
                CatModificarProveedor = rolDTO.CatModificarProveedor,
                CatEliminarProveedor = rolDTO.CatEliminarProveedor,
                CatConsultarProveedor = rolDTO.CatConsultarProveedor,
                CatInsertarProducto = rolDTO.CatInsertarProducto,
                CatModificarProducto = rolDTO.CatModificarProducto,
                CatEliminarProducto = rolDTO.CatEliminarProducto,
                CatConsultarProducto = rolDTO.CatConsultarProducto,
                CatInsertarCentroCosto = rolDTO.CatInsertarCentroCosto,
                CatModificarCentroCosto = rolDTO.CatModificarCentroCosto,
                CatEliminarCentroCosto = rolDTO.CatEliminarCentroCosto,
                CatConsultarCentroCosto = rolDTO.CatConsultarCentroCosto,
                CatInsertarCuentaContable = rolDTO.CatInsertarCuentaContable,
                CatModificarCuentaContable = rolDTO.CatModificarCuentaContable,
                CatEliminarCuentaContable = rolDTO.CatEliminarCuentaContable,
                CatConsultarCuentaContable = rolDTO.CatConsultarCuentaContable,
                CatInsertarCliente = rolDTO.CatInsertarCliente,
                CatModificarCliente = rolDTO.CatModificarCliente,
                CatEliminarCliente = rolDTO.CatEliminarCliente,
                CatConsultarCliente = rolDTO.CatConsultarCliente,
                CatAsignarChoferPuntoVenta = rolDTO.CatAsignarChoferPuntoVenta,
                CatEliminarPuntoVenta = rolDTO.CatEliminarPuntoVenta,
                CatConsultarPuntoVenta = rolDTO.CatConsultarPuntoVenta,
                CatAsignarEquipoTransporte = rolDTO.CatAsignarEquipoTransporte,
                CatModificarEquipoTransporte = rolDTO.CatModificarEquipoTransporte,
                CatEliminarEquipoTransporte = rolDTO.CatEliminarEquipoTransporte,
                CatConsultarEquipoTransporte = rolDTO.CatConsultarEquipoTransporte,
                AppCompraVerOCompra = rolDTO.AppCompraVerOCompra,
                AppCompraEntraGas = rolDTO.AppCompraEntraGas,
                AppCompraGasIniciarDescarga = rolDTO.AppCompraGasIniciarDescarga,
                AppCompraGasFinalizarDescarga = rolDTO.AppCompraGasFinalizarDescarga,
                AppAutoconsumoInventarioGral = rolDTO.AppAutoconsumoInventarioGral,
                AppAutoconsumoEstacionCarb = rolDTO.AppAutoconsumoEstacionCarb,
                AppAutoconsumoPipa = rolDTO.AppAutoconsumoPipa,
                AppCalibracionEstacionCarb = rolDTO.AppCalibracionEstacionCarb,
                AppCalibracionPipa = rolDTO.AppCalibracionPipa,
                AppCalibracionCamionetaCilindro = rolDTO.AppCalibracionCamionetaCilindro,
                AppRecargaEstacionCarb = rolDTO.AppRecargaEstacionCarb,
                AppRecargaPipa = rolDTO.AppRecargaPipa,
                AppRecargaCamionetaCilindro = rolDTO.AppRecargaCamionetaCilindro,
                AppTomaLecturaAlmacenPral = rolDTO.AppTomaLecturaAlmacenPral,
                AppTomaLecturaEstacionCarb = rolDTO.AppTomaLecturaEstacionCarb,
                AppTomaLecturaPipa = rolDTO.AppTomaLecturaPipa,
                AppTomaLecturaCamionetaCilindro = rolDTO.AppTomaLecturaCamionetaCilindro,
                AppTomaLecturaReporteDelDia = rolDTO.AppTomaLecturaReporteDelDia,
                AppTraspasoEstacionCarb = rolDTO.AppTraspasoEstacionCarb,
                AppTraspasoPipa = rolDTO.AppTraspasoPipa,
            };
        }
        public static Rol FromDtoNomRol(RolDto rolDTO, Rol rol)
        {
            var catRol = FromEntity(rol);
            if (rolDTO.NombreRol != null) { catRol.NombreRol = rolDTO.NombreRol; } else { catRol.NombreRol = catRol.NombreRol; }
            
            return catRol;           
        }
        public static List<Rol> FromDto(List<RolDto> rolDTO)
        {
            return  rolDTO.ToList().Select(x => FromDtoPermiso(x)).ToList();
        }

   
        public static Rol FromEntity(Rol rol)
        {
            return new Rol()
            {
                IdEmpresa = rol.IdEmpresa,
                IdRol = rol.IdRol,
                Rol1 = rol.Rol1,
                NombreRol = rol.NombreRol,
                Activo = rol.Activo,
                FechaRegistro = rol.FechaRegistro,
                RequisicionVerRequisiciones = rol.RequisicionVerRequisiciones,
                RequisicionGenerarNueva = rol.RequisicionGenerarNueva,
                RequisicionRevisarExistencia = rol.RequisicionRevisarExistencia,
                RequisicionAutorizar = rol.RequisicionAutorizar,
                CompraVerOCompra = rol.CompraVerOCompra,
                CompraGenerarOCompra = rol.CompraGenerarOCompra,
                CompraAutorizarOCompra = rol.CompraAutorizarOCompra,
                CompraEntraProductoOCompra = rol.CompraEntraProductoOCompra,
                CompraAtiendeServicioOCompra = rol.CompraAtiendeServicioOCompra,
                CatInsertarUsuario = rol.CatInsertarUsuario,
                CatModificarUsuario = rol.CatModificarUsuario,
                CatEliminarUsuario = rol.CatEliminarUsuario,
                CatConsultarUsuario = rol.CatConsultarUsuario,
                CatInsertarProveedor = rol.CatInsertarProveedor,
                CatModificarProveedor = rol.CatModificarProveedor,
                CatEliminarProveedor = rol.CatEliminarProveedor,
                CatConsultarProveedor = rol.CatConsultarProveedor,
                CatInsertarProducto = rol.CatInsertarProducto,
                CatModificarProducto = rol.CatModificarProducto,
                CatEliminarProducto = rol.CatEliminarProducto,
                CatConsultarProducto = rol.CatConsultarProducto,
                CatInsertarCentroCosto = rol.CatInsertarCentroCosto,
                CatModificarCentroCosto = rol.CatModificarCentroCosto,
                CatEliminarCentroCosto = rol.CatEliminarCentroCosto,
                CatConsultarCentroCosto = rol.CatConsultarCentroCosto,
                CatInsertarCuentaContable = rol.CatInsertarCuentaContable,
                CatModificarCuentaContable = rol.CatModificarCuentaContable,
                CatEliminarCuentaContable = rol.CatEliminarCuentaContable,
                CatConsultarCuentaContable = rol.CatConsultarCuentaContable,
                CatInsertarCliente = rol.CatInsertarCliente,
                CatModificarCliente = rol.CatModificarCliente,
                CatEliminarCliente = rol.CatEliminarCliente,
                CatConsultarCliente = rol.CatConsultarCliente,
                CatAsignarChoferPuntoVenta = rol.CatAsignarChoferPuntoVenta,
                CatEliminarPuntoVenta = rol.CatEliminarPuntoVenta,
                CatConsultarPuntoVenta = rol.CatConsultarPuntoVenta,
                CatAsignarEquipoTransporte = rol.CatAsignarEquipoTransporte,
                CatModificarEquipoTransporte = rol.CatModificarEquipoTransporte,
                CatEliminarEquipoTransporte = rol.CatEliminarEquipoTransporte,
                CatConsultarEquipoTransporte = rol.CatConsultarEquipoTransporte,
                AppCompraVerOCompra = rol.AppCompraVerOCompra,
                AppCompraEntraGas = rol.AppCompraEntraGas,
                AppCompraGasIniciarDescarga = rol.AppCompraGasIniciarDescarga,
                AppCompraGasFinalizarDescarga = rol.AppCompraGasFinalizarDescarga,
                AppAutoconsumoInventarioGral = rol.AppAutoconsumoInventarioGral,
                AppAutoconsumoEstacionCarb = rol.AppAutoconsumoEstacionCarb,
                AppAutoconsumoPipa = rol.AppAutoconsumoPipa,
                AppCalibracionEstacionCarb = rol.AppCalibracionEstacionCarb,
                AppCalibracionPipa = rol.AppCalibracionPipa,
                AppCalibracionCamionetaCilindro = rol.AppCalibracionCamionetaCilindro,
                AppRecargaEstacionCarb = rol.AppRecargaEstacionCarb,
                AppRecargaPipa = rol.AppRecargaPipa,
                AppRecargaCamionetaCilindro = rol.AppRecargaCamionetaCilindro,
                AppTomaLecturaAlmacenPral = rol.AppTomaLecturaAlmacenPral,
                AppTomaLecturaEstacionCarb = rol.AppTomaLecturaEstacionCarb,
                AppTomaLecturaPipa = rol.AppTomaLecturaPipa,
                AppTomaLecturaCamionetaCilindro = rol.AppTomaLecturaCamionetaCilindro,
                AppTomaLecturaReporteDelDia = rol.AppTomaLecturaReporteDelDia,
                AppTraspasoEstacionCarb = rol.AppTraspasoEstacionCarb,
                AppTraspasoPipa = rol.AppTraspasoPipa,
                
            };
        }

    }

}
