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
            return ProveedorServicio.RegistrarProveedor(provDto);
        }

        public List<ProveedorDto> ConsultaProveedores()
        {
            return ProveedorServicio.Obtener();
        }

        public ProveedorDto ConsultaProveedor(int idProveedor)
        {
            return ProveedorServicio.Obtener(idProveedor);
        }
        #endregion
    }
}
