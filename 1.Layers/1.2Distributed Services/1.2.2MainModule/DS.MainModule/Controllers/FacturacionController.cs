﻿using Application.MainModule.DTOs;
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

        public FacturacionController()
        {
            _facturacion = new Facturacion();
        }

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
        [Route("buscar/tickets/cliente/{id}")]
        public HttpResponseMessage GetTicketsByCliente(int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _facturacion.BuscarPorNumCliente(id));
        }
        [Route("buscar/tickets/rfc/{rfc}")]
        public HttpResponseMessage GetTicketsByRFC(string rfc)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _facturacion.BuscarPorRFC(rfc));
        }

        [Route("buscar/cfdi/rfc/{rfc}")]
        public HttpResponseMessage GetCFDIByRFC(string rfc)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _facturacion.BuscarFacturasPorRFC(rfc));
        }
        [Route("buscar/cfdi/cliente/{id}")]
        public HttpResponseMessage GetCFDIByCliente(int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _facturacion.BuscarFacturasPorCliente(id));
        }
        [Route("buscar/cfdi/{numTicket}")]
        public HttpResponseMessage GetCFDI(string numTicket)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _facturacion.BuscarFacturasPorTicket(numTicket));
        }
    }
}   