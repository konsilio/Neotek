using Application.MainModule.DTOs.Catalogo;
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
    }
}
