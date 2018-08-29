using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.MainModule.Agente;
using Web.MainModule.Catalogos.Model;
using Web.MainModule.OrdenCompra.Model;

namespace Web.MainModule.OrdenCompra.Servicio
{
    public class OrdenCompraServicio
    {
        public Model.RequisicionOCDTO DatosRequisicion(int idReq, string Tkn)
        {
            AgenteServicios agente = new AgenteServicios();
            agente.BuscarRequisicioOC(idReq, Tkn);
            return agente._requisicionOrdenCompra;
        }
        public List<ProveedorDTO> Proveedores(string Tkn)
        {
            Agente.AgenteServicios agente = new Agente.AgenteServicios();
            agente.BuscarProveedoresOC(Tkn);
            return agente._listaProveedores;
        }
        public List<CuentaContableDTO> ListaCuentasContables(short idEmpresa, string Tkn)
        {
            AgenteServicios agente = new AgenteServicios();
            agente.BuscarCuentasContables(idEmpresa, Tkn);
            return agente._listaCuentasContable;
        }
        public List<OrdenCompraRespuestaDTO> GenerarOrdenesCompra(OrdenCompraCrearDTO ocDTO, string Tkn)
        {
            AgenteServicios agente = new AgenteServicios();
            agente.GuardarOrdenesCompra(ocDTO, Tkn);
            return agente._listaOrdenesCompraRespuesta;
        }
        public List<OrdenCompraDTO> ObtenerOrdenesCompra(short idEmpresa, string Tkn)
        {
            AgenteServicios agente = new AgenteServicios();
            agente.BuscarOrdenesCompra(idEmpresa, Tkn);
            return agente._listaOrdenCompraDTO;
        }
    }
}