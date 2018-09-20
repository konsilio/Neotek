using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Seguridad
{
   public class RolDto
    {
        public short IdRol { get; set; }
        public string Rol1 { get; set; }
        public string NombreRol { get; set; }
        /****************************************/

        public bool RequisicionVerRequisiciones { get; set; }
        public bool RequisicionGenerarNueva { get; set; }
        public bool RequisicionRevisarExistencia { get; set; }
        public bool RequisicionAutorizar { get; set; }
        public bool CompraVerOCompra { get; set; }
        public bool CompraGenerarOCompra { get; set; }
        public bool CompraAutorizarOCompra { get; set; }
        public bool CompraEntraProductoOCompra { get; set; }
        public bool CompraAtiendeServicioOCompra { get; set; }
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

        public short IdEmpresa { get; set; }
        public bool Activo { get; set; }
        public System.DateTime FechaRegistro { get; set; }
        public List<RolDto> ListaRoles { get; set; }

        public List<RolCat> ListaRolesCat { get; set; }
        public List<RolCompras> ListaRolesCom { get; set; }

    }
}
