using Application.MainModule.DTOs;
using Application.MainModule.Flujos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace DS.MainModule.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/facturacion")]
    public class FacturacionController: ApiController
    {
        private Facturacion _facturacion;

        [Route("registrar/factruas")]
        public HttpResponseMessage PostRegistrarCFDILst(List<CFDIDTO> list)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _facturacion.GenerarFactura(list));
        }
        [Route("registrar/factrua")]
        public HttpResponseMessage PostRegistrarCFDI(CFDIDTO dto)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _facturacion.GenerarFactura(dto));
        }        
        [Route("buscar/ticket/{numTicket}")]
        public HttpResponseMessage GetTicket(string numTicket)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _facturacion.BuscarPorTicket(numTicket));
        }
        [Route("buscar/tickets/{rfc}")]
        public HttpResponseMessage GetTickets(string rfc)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _facturacion.BuscarPorRFC(rfc));
        }
        [Route("buscar/ticketsporcliente/{id}")]
        public HttpResponseMessage GetTicketsPorCliete(int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _facturacion.BuscarPorNumCliente(id));
        }
    }
}