using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.MainModule.Agente;
using Web.MainModule.Catalogos.Model;
using Web.MainModule.OrdenCompra.Model;
using Web.MainModule.Seguridad.Model;

namespace Web.MainModule.OrdenCompra.Servicio
{
    public class OrdenCompraServicio
    {
        public RequisicionOCDTO DatosRequisicion(int idReq, string Tkn)
        {
            AgenteServicios agente = new AgenteServicios();
            agente.BuscarRequisicioOC(idReq, Tkn);
            return agente._requisicionOrdenCompra;
        }
        public List<ProveedorDTO> Proveedores(string Tkn)
        {
           var agente = new AgenteServicios();
            agente.BuscarProveedores(Tkn);
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
        public RespuestaDto CancelarOrdenCompra(OrdenCompraDTO dto, string Tkn)
        {
            AgenteServicios agente = new AgenteServicios();
            agente.CancelarOrdenCompra(dto, Tkn);
            return agente._respuestaDTO;
        }
        public RespuestaDto AutorizarOrdenCompra(OrdenCompraAutorizacionDTO dto, string Tkn)
        {
            AgenteServicios agente = new AgenteServicios();
            agente.AutorizarOrdenCompra(dto, Tkn);
            return agente._respuestaDTO;
        }
        public OrdenCompraCrearDTO OrdenCompraPorId(int dto, string Tkn)
        {
            AgenteServicios agente = new AgenteServicios();
            agente.BuscarOrdenCompra(dto, Tkn);
            return agente._ordenCompraCrearDTO;
        }
        public List<OrdenCompraEstatusDTO> ListaOCEstatus(string Tkn)
        {
            AgenteServicios agente = new AgenteServicios();
            agente.BuscarOrdenCompraEstatus(Tkn);
            return agente._listaOrdenCompraEstatus;
        }
    }
}