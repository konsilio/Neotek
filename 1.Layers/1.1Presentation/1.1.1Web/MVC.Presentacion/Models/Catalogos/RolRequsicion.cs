using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Catalogos
{
    public class RolRequsicion
    {
        public short IdRol { get; set; }
        public string Rol1 { get; set; }
        public string NombreRol { get; set; }

        /**************************/
        public bool RequisicionVerRequisiciones { get; set; }
        public bool RequisicionGenerarNueva { get; set; }
        public bool RequisicionRevisarExistencia { get; set; }
        public bool RequisicionAutorizar { get; set; }
        /**************************/
        public bool CompraVerOCompra { get; set; }
        public bool CompraGenerarOCompra { get; set; }
        public bool CompraAutorizarOCompra { get; set; }
        public bool CompraEntraProductoOCompra { get; set; }
        public bool CompraAtiendeServicioOCompra { get; set; }
        public bool CompraCancelaOCompra { get; set; }
        public bool AlmacenActualizaExistencias { get; set; }
        public bool AlmacenVerExistencias { get; set; }
        public bool AlmacenVerMovimientos { get; set; }
        public bool CatInsertarUsuario { get; set; }
        public bool CatModificarUsuario { get; set; }
        public bool CatEliminarUsuario { get; set; }
        public bool CatConsultarUsuario { get; set; }
        public bool CatInsertarProveedor { get; set; }
        public bool CatModificarProveedor { get; set; }
        public bool CatEliminarProveedor { get; set; }
        public bool CatConsultarProveedor { get; set; }
        public bool CatInsertarProducto { get; set; }
        public bool CatModificarProducto { get; set; }
        public bool CatEliminarProducto { get; set; }
        public bool CatConsultarProducto { get; set; }
        public bool CatInsertarCentroCosto { get; set; }
        public bool CatModificarCentroCosto { get; set; }
        public bool CatEliminarCentroCosto { get; set; }
        public bool CatConsultarCentroCosto { get; set; }
        public bool CatInsertarCuentaContable { get; set; }
        public bool CatModificarCuentaContable { get; set; }
        public bool CatEliminarCuentaContable { get; set; }
        public bool CatConsultarCuentaContable { get; set; }
        public bool CatInsertarCliente { get; set; }
        public bool CatModificarCliente { get; set; }
        public bool CatEliminarCliente { get; set; }
        public bool CatConsultarCliente { get; set; }
        public bool CatAsignarChoferPuntoVenta { get; set; }
        public bool CatEliminarPuntoVenta { get; set; }
        public bool CatConsultarPuntoVenta { get; set; }
        public bool CatAsignarEquipoTransporte { get; set; }
        public bool CatModificarEquipoTransporte { get; set; }
        public bool CatEliminarEquipoTransporte { get; set; }
        public bool CatConsultarEquipoTransporte { get; set; }
        public bool CatInsertarEmpresa { get; set; }
        public bool CatModificarEmpresa { get; set; }
        public bool CatEliminarEmpresa { get; set; }
        public bool CatConsultarEmpresa { get; set; }
        public bool CatInsertarRol { get; set; }
        public bool CatModificarRol { get; set; }
        public bool CatEliminarRol { get; set; }
        public bool CatConsultarRol { get; set; }
        public bool CatInsertarPrecioVentaGas { get; set; }
        public bool CatModificarPrecioVentaGas { get; set; }
        public bool CatEliminarPrecioVentaGas { get; set; }
        public bool CatConsultarPrecioVentaGas { get; set; }
        public bool CatInsertarPrecioVenta { get; set; }
        public bool CatModificarPrecioVenta { get; set; }
        public bool CatEliminarPrecioVenta { get; set; }
        public bool CatConsultarPrecioVenta { get; set; }
        public bool AppCompraVerOCompra { get; set; }
        public bool AppCompraEntraGas { get; set; }
        public bool AppCompraGasIniciarDescarga { get; set; }
        public bool AppCompraGasFinalizarDescarga { get; set; }
        public bool AppAutoconsumoInventarioGral { get; set; }
        public bool AppAutoconsumoEstacionCarb { get; set; }
        public bool AppAutoconsumoPipa { get; set; }
        public bool AppCalibracionEstacionCarb { get; set; }
        public bool AppCalibracionPipa { get; set; }
        public bool AppCalibracionCamionetaCilindro { get; set; }
        public bool AppRecargaEstacionCarb { get; set; }
        public bool AppRecargaPipa { get; set; }
        public bool AppRecargaCamionetaCilindro { get; set; }
        public bool AppTomaLecturaAlmacenPral { get; set; }
        public bool AppTomaLecturaEstacionCarb { get; set; }
        public bool AppTomaLecturaPipa { get; set; }
        public bool AppTomaLecturaCamionetaCilindro { get; set; }
        public bool AppTomaLecturaReporteDelDia { get; set; }
        public bool AppTraspasoEstacionCarb { get; set; }
        public bool AppTraspasoPipa { get; set; }
        public bool AppDisposicionEfectivo { get; set; }
        public bool AppCamionetaPuntoVenta { get; set; }
        public bool AppEstacionCarbPuntoVenta { get; set; }
        public bool AppPipaPuntoVenta { get; set; }
        public short IdEmpresa { get; set; }
        public bool Activo { get; set; }
        public System.DateTime FechaRegistro { get; set; }  
    }
}