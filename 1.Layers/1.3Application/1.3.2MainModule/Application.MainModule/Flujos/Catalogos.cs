using Application.MainModule.AdaptadoresDTO.Catalogo;
using Application.MainModule.DTOs.Catalogo;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.Catalogos;
using Application.MainModule.Servicios.Seguridad;
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
        public List<CentroCostoDTO> ListaCentrosCostos()
        {
            return CentroCostoServicio.ObtenerCentrosCostos();
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

        public List<ProveedorDto> ConsultaProveedores(short idEmpresa)
        {
            var resp = PermisosServicio.PuedeConsultarProveedor();
            if (!resp.Exito) return new List<ProveedorDto>();

            return ProveedorAdapter.ToDto(ProveedorServicio.Obtener(idEmpresa));
        }

        public ProveedorDto ConsultaProveedor(int idProveedor)
        {
            var resp = PermisosServicio.PuedeConsultarProveedor();
            if (!resp.Exito) return null;

            return ProveedorAdapter.ToDto(ProveedorServicio.Obtener(idProveedor));
        }
        #endregion
    }
}
