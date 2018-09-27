using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagas.MainModule.ObjetosValor.Enum
{
    public class EstatusPrecioVentaEnum
    {
        public static byte Programado = (byte)EstatusPrecioVenta.Programado;
        public static byte Vigente = (byte)EstatusPrecioVenta.Vigente;
        public static byte Vencido = (byte)EstatusPrecioVenta.Vencido;
    }

    enum EstatusPrecioVenta : byte
    {
        Programado = 1,
        Vigente = 2,
        Vencido = 3,
    }
}
