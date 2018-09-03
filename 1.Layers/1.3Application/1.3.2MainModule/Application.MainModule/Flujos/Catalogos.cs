using Application.MainModule.AdaptadoresDTO.Catalogo;
using Application.MainModule.DTOs.Catalogo;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.Servicios.Catalogos;
using Application.MainModule.Servicios.Seguridad;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Flujos
{
    public class Catalogos
    {
        #region Empresas
        public List<EmpresaDTO> ListaEmpresasLogin()
        {
            return EmpresaServicio.BuscarEmpresasLogin();
        }
        public List<EmpresaDTO> ListaEmpresas()
        {
            if (TokenServicio.ObtenerEsAdministracionCentral())
                return EmpresaServicio.BuscarEmpresas();
            else
                return EmpresaServicio.BuscarEmpresas().ToList().Where(x => x.IdEmpresa.Equals(TokenServicio.ObtenerIdEmpresa())).ToList();
        }
        public List<EmpresaDTO> ListaEmpresas(bool conAC)
        {
            return EmpresaServicio.BuscarEmpresas(conAC);
        }
        #endregion
        #region Usuarios
        public List<UsuarioDTO> ListaUsuarios(short idEmpresa)
        {
            if (TokenServicio.ObtenerEsAdministracionCentral())
                return UsuarioServicio.ListaUsuarios().Where(x => x.IdEmpresa.Equals(idEmpresa)).ToList();
            else
                return UsuarioServicio.ListaUsuarios().Where(x => x.IdEmpresa.Equals(TokenServicio.ObtenerIdEmpresa())).ToList();
        }
        #endregion
        #region Productos
        public List<ProductoDTO> ListaProductos(short idEmpresa)
        {
            return ProductoServicios.ListaProductos(idEmpresa);
        }
        public List<ProductoDTO> ListaPorductosAsociados(int idProdcuto)
        {
            return ProductoServicios.ListaProductoAsociados(ProductoServicios.ListaProductoAsociados(idProdcuto));
        }
        #endregion
        #region CentroCosto
        public RespuestaDto RegistraCentroCosto(CentroCostoCrearDto ccDto)
        {
            var resp = PermisosServicio.PuedeRegistrarCentroCosto();
            if (!resp.Exito) return resp;

            resp = ValidarCatalogoServicio.CentroCosto(ccDto);
            if (!resp.Exito) return resp;

            return CentroCostoServicio.RegistrarCentroCosto(CentroCostoAdapter.FromDto(ccDto));
        }

        public RespuestaDto ModificaCentroCosto(CentroCostoModificarDto ccDto)
        {
            var resp = PermisosServicio.PuedeModificarCentroCosto();
            if (!resp.Exito) return resp;

            resp = ValidarCatalogoServicio.CentroCosto(ccDto);
            if (!resp.Exito) return resp;

            var centro = CentroCostoServicio.Obtener(ccDto.IdCentroCosto);
            if (centro == null) return CentroCostoServicio.NoExiste();

            var CentroCosto = CentroCostoAdapter.FromDto(ccDto);
            CentroCosto.FechaRegistro = centro.FechaRegistro;
            return CentroCostoServicio.ModificarCentroCosto(CentroCosto);
        }

        public RespuestaDto EliminaCentroCosto(CentroCostoEliminarDto ccDto)
        {
            var resp = PermisosServicio.PuedeEliminarCentroCosto();
            if (!resp.Exito) return resp;

            var centro = CentroCostoServicio.Obtener(ccDto.IdCentroCosto);
            if (centro == null) return CentroCostoServicio.NoExiste();

            centro = CentroCostoAdapter.FromEntity(centro);
            centro.Activo = false;
            return CentroCostoServicio.ModificarCentroCosto(centro);
        }

        public List<CentroCostoDTO> ListaCentrosCostos()
        {
            return CentroCostoAdapter.ToDTO(CentroCostoServicio.Obtener());
        }
        public CentroCostoDTO ConsultaCentroCosto(int idCentroCosto)
        {
            var resp = PermisosServicio.PuedeConsultarCentroCosto();
            if (!resp.Exito) return null;

            return CentroCostoAdapter.ToDTO(CentroCostoServicio.Obtener(idCentroCosto));
        }
        #endregion
        #region Proveedor
        public RespuestaDto RegistraProveedor(ProveedorCrearDto provDto)
        {
            var resp = PermisosServicio.PuedeRegistrarProveedor();
            if (!resp.Exito) return resp;

            return ProveedorServicio.RegistrarProveedor(ProveedorAdapter.FromDto(provDto));
        }

        public RespuestaDto ModificaProveedor(ProveedorModificarDto provDto)
        {
            var resp = PermisosServicio.PuedeModificarProveedor();
            if (!resp.Exito) return resp;

            var provee = ProveedorServicio.Obtener(provDto.IdProveedor);
            if (provee == null) return ProveedorServicio.NoExiste();

            var proveedor = ProveedorAdapter.FromDto(provDto);
            proveedor.FechaRegistro = provee.FechaRegistro;
            return ProveedorServicio.ModificarProveedor(proveedor);
        }

        public RespuestaDto EliminaProveedor(ProveedorEliminarDto provDto)
        {
            var resp = PermisosServicio.PuedeEliminarProveedor();
            if (!resp.Exito) return resp;

            var provee = ProveedorServicio.Obtener(provDto.IdProveedor);
            if (provee == null) return ProveedorServicio.NoExiste();

            provee = ProveedorAdapter.FromEntity(provee);
            provee.Activo = false;
            return ProveedorServicio.ModificarProveedor(provee);
        }

        public List<ProveedorDto> ConsultaProveedores()
        {
            var resp = PermisosServicio.PuedeConsultarProveedor();
            if (!resp.Exito) return new List<ProveedorDto>();

            return ProveedorAdapter.ToDto(ProveedorServicio.Obtener());
        }

        public ProveedorDto ConsultaProveedor(int idProveedor)
        {
            var resp = PermisosServicio.PuedeConsultarProveedor();
            if (!resp.Exito) return null;

            return ProveedorAdapter.ToDto(ProveedorServicio.Obtener(idProveedor));
        }
        #endregion
        #region Cuentas Contables
        public RespuestaDto RegistraCuentaContable(CuentaContableCrearDto ccDto)
        {
            var resp = PermisosServicio.PuedeRegistrarCuentaContable();
            if (!resp.Exito) return resp;

            return CuentaContableServicio.RegistrarCuentaContable(CuentaContableAdapter.FromDto(ccDto));
        }
        public RespuestaDto ModificaCuentaContable(CuentaContableModificarDto ccDto)
        {
            var resp = PermisosServicio.PuedeModificarCuentaContable();
            if (!resp.Exito) return resp;

            var ctactble = CuentaContableServicio.Obtener(ccDto.IdCuenta);
            if (ctactble == null) return CuentaContableServicio.NoExiste();

            //var CuentaContable = CuentaContableAdapter.FromDto(ccDto);
            var ctactbleEmptity = CuentaContableAdapter.FromEntity(ctactble);
            ctactbleEmptity.Numero = ccDto.Numero;
            ctactbleEmptity.Descripcion = ccDto.Descripcion;
            return CuentaContableServicio.ModificarCuentaContable(ctactbleEmptity);
        }
        public RespuestaDto EliminaCuentaContable(CuentaContableEliminarDto ccDto)
        {
            var resp = PermisosServicio.PuedeEliminarCuentaContable();
            if (!resp.Exito) return resp;

            var ctactble = CuentaContableServicio.Obtener(ccDto.IdCuenta);
            if (ctactble == null) return CuentaContableServicio.NoExiste();

            var ctactbleEmptity = CuentaContableAdapter.FromEmtyte(ctactble);
            ctactbleEmptity.Activo = false;
            return CuentaContableServicio.ModificarCuentaContable(ctactbleEmptity);
        }
        public List<CuentaContableDto> ConsultaCuentasContables(short idEmpresa)
        {
            var resp = PermisosServicio.PuedeConsultarCuentaContable();
            if (!resp.Exito) return new List<CuentaContableDto>();

            return CuentaContableAdapter.ToDto(CuentaContableServicio.Obtener().Where(x => x.IdEmpresa.Equals(idEmpresa)).ToList());
        }
        public CuentaContableDto ConsultaCuentaContable(int idCuentaContable)
        {
            var resp = PermisosServicio.PuedeConsultarCuentaContable();
            if (!resp.Exito) return null;

            return CuentaContableAdapter.ToDto(CuentaContableServicio.Obtener(idCuentaContable));
        }
        #endregion
        #region CuentaContable
        //public List<CuentaContableDto> BuscarCuentaContable(int idEmpresa)
        //{
        //    var listaCuentasContables = new CuentaContableDataAccess().BuscarCuentasContables(idEmpresa);
        //    return CuentaContableAdapter.FromDto(listaCuentasContables);
        //}
        //public RespuestaDto BorrarCuentaContable(int idCuentaContable)
        //{//Borrado logico    
        //    var ctaCtble = CuentaContableServicio.ObtenerCuentaContable(idCuentaContable);
        //    ctaCtble = CuentaContableAdapter.FromEmtyte(ctaCtble);

        //    ctaCtble.Activo = false;
        //    return CuentaContableServicio.ModificarCuentaContable(ctaCtble);
        //}
        //public RespuestaDto EditarCuentaContable(CuentaContableDto cc)
        //{
        //    var ctaCtble = CuentaContableServicio.ObtenerCuentaContable(cc.IdCuentaContable);
        //    ctaCtble = CuentaContableAdapter.FromEmtyte(ctaCtble);
        //    ctaCtble.Numero = cc.Numero;
        //    ctaCtble.Descripcion = cc.Descripcion;

        //    return CuentaContableServicio.ModificarCuentaContable(ctaCtble);
        //}
        //public RespuestaDto CrearCuentaContable(CuentaContableDto cc)
        //{
        //    CuentaContable ctaCtble = CuentaContableAdapter.ToDTO(cc);
        //    return new CuentaContableDataAccess().InsertarCuentaContable(ctaCtble);
        //}
        #endregion
    }
}
