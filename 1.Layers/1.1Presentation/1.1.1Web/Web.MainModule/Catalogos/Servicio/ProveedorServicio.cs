using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.MainModule.Agente;
using Web.MainModule.Catalogos.Model;

namespace Web.MainModule.Catalogos.Servicio
{
    public class ProveedorServicio
    {
        public List<ProveedorDTO> CargarProveedores(string Tkn)
        {
            var agente = new AgenteServicios();
            agente.BuscarProveedores(Tkn);
            return agente._listaProveedores;
        }        
    }
}