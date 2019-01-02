using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagas.MainModule.ObjetosValor.Enum
{
    public class EstatusPedidoEnum
    {
        public static short PedidoCreado = (short)EstatusPedido.PedidoCreado;
        public static short EnRuta = (short)EstatusPedido.EnRuta;
        public static short Surtido = (short)EstatusPedido.Surtido;
        public static short Cancelado = (short)EstatusPedido.Cancelado;
        public static short Solollamada = (short)EstatusPedido.Solollamada;
    }
    enum EstatusPedido : short
    {
        PedidoCreado = 1,
        EnRuta = 2,
        Surtido = 3,
        Cancelado = 4,
        Solollamada = 5,
    }

    public enum stringEstatus { PedidoCreado, EnRuta, Surtido, Cancelado, Solollamada };
        
}
