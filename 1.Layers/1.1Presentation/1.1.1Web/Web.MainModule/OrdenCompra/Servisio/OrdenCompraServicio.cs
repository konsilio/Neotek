using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.MainModule.OrdenCompra.Servisio
{
    public class OrdenCompraServicio
    {
        public Model.RequisicionOCDTO DatosRequisicion(int idReq, string Tkn)
        {
            Agente.AgenteServicios agente = new Agente.AgenteServicios();
            agente.BuscarRequisicioOC(idReq, Tkn);
            return agente._requisicionOrdenCompra;
        }
        public List<Model.ProveedorDTO> Proveedores(short idEmpresa, string Tkn)
        {
            Agente.AgenteServicios agente = new Agente.AgenteServicios();
            agente.BuscarProveedoresOC(idEmpresa, Tkn);
            return agente._listaProveedores;
        }
    }
}