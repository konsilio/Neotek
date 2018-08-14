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
        #endregion

        #region Proveedor
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
