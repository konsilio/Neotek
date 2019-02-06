using MVC.Presentacion.Agente;
using MVC.Presentacion.Models.Catalogos;
using MVC.Presentacion.Models.Pedidos;
using MVC.Presentacion.Models.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.App_Code
{
    public static class PedidosServicio
    {
        public static List<PedidoModel> ObtenerPedidos(short id, string tkn)
        {
            var agente = new AgenteServicio();
            agente.ListaPedidos(id, tkn);
            return agente._ListaPedidos;
        }

        public static List<PedidoModel> ObtenerPedidosFiltro(short id, string tkn, int? idpedido, string rfc = null, string tel1 = null)
        {
            var agente = new AgenteServicio();
            agente.ListaPedidosFiltro(id, idpedido.Value, rfc, tel1, tkn);
            return agente._ListaPedidos;
        }
        public static PedidoModel ObtenerIdPedido(int id,string tkn)
        {
            var agente = new AgenteServicio();
            agente.ObtenerPedidoId(id,tkn);
            return agente._Pedido;
        }
        public static List<EstatusPedidoModel> ObtenerEstatusPedidos(string tkn)
        {
            var agente = new AgenteServicio();
            agente.BuscarEstatusPedido(tkn);
            return agente._ListaEstatusP;
        }

        public static RespuestaDTO AltaNuevoPedido(PedidoModel model, string tkn)
        {
            var agente = new AgenteServicio();
            agente.GuardarNuevoPedido(model, tkn);
            return agente._RespuestaDTO;
        }
        public static RespuestaDTO AltaEncuestaPedido(List<EncuestaModel> model, string tkn)
        {
            var agente = new AgenteServicio();
            agente.GuardarEncuesta(model, tkn);
            return agente._RespuestaDTO;
        }
        public static RespuestaDTO ActualizarPedido(PedidoModel model, string tkn)
        {
            var agente = new AgenteServicio();
            agente.GuardarEdicionPedido(model, tkn);
            return agente._RespuestaDTO;
        }
        public static RespuestaDTO EliminarPedido(PedidoModel model, string tkn)
        {
            var agente = new AgenteServicio();
            agente.CancelarNuevoPedido(model, tkn);
            return agente._RespuestaDTO;
        }
        public static List<CamionetaModel> ObtenerCamionetas(short id, string tkn)
        {
            var agente = new AgenteServicio();
            agente.BuscarCamionetas(id, tkn);
            return agente._ListaCamionetas;
        }

        public static List<PipaModel> ObtenerPipas(short id, string tkn)
        {
            var agente = new AgenteServicio();
            agente.BuscarPipas(id, tkn);
            return agente._ListaPipas;
        }
    }
}