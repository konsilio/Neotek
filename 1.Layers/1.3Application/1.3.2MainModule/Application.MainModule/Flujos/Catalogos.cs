using Application.MainModule.DTOs.Catalogo;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.Catalogos;
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
            return EmpresaServicio.BuscarEmpresas();
        }
        public List<EmpresaDTO> ListaEmpresas(bool conAC)
        {
            return EmpresaServicio.BuscarEmpresas(conAC);
        }
        #endregion
        #region Usuarios
        public List<UsuarioDTO> ListaUsuarios(short idEmpresa)
        {
            return UsuarioServicio.ListaUsuarios(idEmpresa);
        }
        #endregion
        #region Productos
        public List<ProductoDTO> ListaProductos(short idEmpresa)
        {
            return ProductoServicios.ListaProductos(idEmpresa);
        }
        #endregion

        #region Proveedor
        public RespuestaDto RegistraProveedor(ProveedorCrearDto provDto)
        {
            return ProveedorServicio.RegistrarProveedor(provDto);
        }

        public List<ProveedorDto> ConsultaProveedores(short idEmpresa)
        {
            return ProveedorServicio.Obtener(idEmpresa);
        }

        public ProveedorDto ConsultaProveedor(int idProveedor)
        {
            return ProveedorServicio.Obtener(idProveedor);
        }
        #endregion
    }
}
