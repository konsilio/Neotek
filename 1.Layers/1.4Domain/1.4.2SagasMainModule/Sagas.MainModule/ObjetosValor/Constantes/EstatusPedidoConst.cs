using Sagas.MainModule.ObjetosValor.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagas.MainModule.ObjetosValor.Constantes
{
    public static class EstatusPedidoConst
    {
        public static string PedidoCreado = "Pedido Creado";
        public static string EnRuta = "En Ruta";
        public static string Surtido = "Surtido";
        public static string Cancelado = "Cancelado";
        public static string Solollamada = "Solo llamada";

        public static string ObtenerString(short id)
        {
            if (id.Equals(EstatusPedidoEnum.PedidoCreado))
                return PedidoCreado;
            if (id.Equals(EstatusPedidoEnum.EnRuta))
                return EnRuta;
            if (id.Equals(EstatusPedidoEnum.Surtido))
                return Surtido;
            if (id.Equals(EstatusPedidoEnum.Cancelado))
                return Cancelado;
            return Solollamada;
        }        
    }
}
