using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagas.MainModule.ObjetosValor.Enum
{
    public class TipoMovimientoEnum
    {
        public static byte Entrada = (byte)tipoMovimiento.Entrada;
        public static byte Salida = (byte)tipoMovimiento.Salida;
        public static byte LectInicial = (byte)tipoMovimiento.LectInicial;
        public static byte LectFinal = (byte)tipoMovimiento.LectFinal;
    }

    enum tipoMovimiento : byte
    {
        Entrada = 1,
        Salida = 2,
        LectInicial = 3,
        LectFinal = 4,
    }
}
