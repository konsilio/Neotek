using Application.MainModule.DTOs.Cobranza;
using Application.MainModule.Flujos;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DS.MainModule.Controllers
{
    [Authorize]
    [RoutePrefix("api/cobranza")]
    public class CobranzaController : ApiController
    {
        private Cobranza _cobranza;
        public CobranzaController()
        {
            _cobranza = new Cobranza();
        }

        [Route("buscar/listacargos/{id}")]
        public HttpResponseMessage GetListaCargosEmpresa(short id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _cobranza.ListaCargos(id));
        }
        [Route("buscar/listacrecuperada/{id}")]
        public HttpResponseMessage GetListaCRecuperada(short id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _cobranza.ListaCRecuperada(id));
        }
        [Route("buscar/carterarecuperada")]
        public HttpResponseMessage PutCarteraRecuperada(CargosDTO dto)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _cobranza.CarteraRecuperada(dto.IdCliente,  dto.IdEmpresa,dto.FechaRango1.Date, dto.FechaRango2.Date,dto.Ticket));
        }
       
        [Route("buscar/cargos/{id}")]
        public HttpResponseMessage GetPedidosId(int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _cobranza.CargoId(id));
        }
        [Route("buscar/carteravencida")]//{idCliente}/{fecha}/{idEmpresa}
        public HttpResponseMessage PutCarteraVencida(CargosDTO dto)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _cobranza.ListaCargos(dto.IdCliente, dto.FechaRango1.Date, dto.IdEmpresa));
        }
        [Route("registrar/abono")]
        public HttpResponseMessage PostRegistrarAbonos(CargosDTO _model)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _cobranza.Registra(_model.Abono));
        }
        [Route("registrar/abonos")]
        public HttpResponseMessage PostRegistrarAbonosLst(List<AbonosDTO> _model)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _cobranza.Registra(_model));
        }

    }
}