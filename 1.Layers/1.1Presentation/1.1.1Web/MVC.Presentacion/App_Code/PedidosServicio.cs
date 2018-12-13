using MVC.Presentacion.Agente;
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
        public static List<PedidoModel> ObtenerPedidos(string tkn)
        {
            var agente = new AgenteServicio();
            agente.ListaPedidos(tkn);
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
        public static RespuestaDTO ActualizarPedido(PedidoModel model, string tkn)
        {
            var agente = new AgenteServicio();
            agente.GuardarEdicionPedido(model, tkn);
            return agente._RespuestaDTO;
        }

        
    }
}