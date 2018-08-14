using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.DTOs.Catalogo;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.AdaptadoresDTO.Catalogo;

namespace Application.MainModule.Servicios.Catalogos
{
    public static class EmpresaServicio
    {
        public static List<EmpresaDTO> BuscarEmpresas()
        {
            List<EmpresaDTO> lEmpresas = new List<EmpresaDTO>(EmpresaAdapter.ToDTO(new EmpresaDataAccess().BuscarTodos()));
            return lEmpresas;
        }
        public static List<EmpresaDTO> BuscarEmpresas(bool conAC)
        {
            List<EmpresaDTO> lEmpresas = new List<EmpresaDTO>(EmpresaAdapter.ToDTO(new EmpresaDataAccess().BuscarTodos(conAC)));
            return lEmpresas;
        }
        public static List<EmpresaDTO> BuscarEmpresasLogin()
        {            
            return BuscarEmpresas();
        }
    }
}
