using Application.MainModule.DTOs.Pedidos;
using Application.MainModule.Flujos;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DS.MainModule.Controllers
{
    [Authorize]
    [RoutePrefix("api/pedidos")]
    public class PedidosController : ApiController
    {
        private Pedidos _pedidos;
        public PedidosController()
        {
            _pedidos = new Pedidos();
        }

        [Route("buscar/listapedidos")]
        public HttpResponseMessage GetListaPedidos()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _pedidos.ListaPedidos());
        }
        [Route("buscar/pedidos/{id}")]
        public HttpResponseMessage GetPedidosId(int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _pedidos.PedidoId(id));
        }

        [Route("buscar/listaestatuspedidos")]
        public HttpResponseMessage GetEstatusPedidos()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _pedidos.ListaEstatusPedidos());
        }

        [Route("modificar/pedido")]
        public HttpResponseMessage PutModificarPedidos(PedidoModelDto _model)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _pedidos.Modifica(_model));
        }

        [Route("registrar/pedido")]
        public HttpResponseMessage PostRegistrarPedidos(PedidoModelDto _model)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _pedidos.Registra(_model));
        }

        [Route("cancelar/pedido")]
        public HttpResponseMessage PutCancelarPedidos(PedidoModelDto _model)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _pedidos.Elimina(_model));
        }
    }
}