using System.Collections.Generic;
using Web.MainModule.Agente;
using Web.MainModule.Seguridad.Model;

namespace Web.MainModule.Catalogos.Servicio
{
    public class EmpresaServicio
    {
        public List<EmpresaDTO> Empresas(string tkn)
        {
            var agente = new AgenteServicios();
            agente.ListaEmpresas(tkn);
            return agente._listaEmpresas;
        }
    }
}