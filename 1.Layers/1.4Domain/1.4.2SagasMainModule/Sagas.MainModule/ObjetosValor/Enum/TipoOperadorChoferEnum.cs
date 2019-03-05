using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagas.MainModule.ObjetosValor.Enum
{
    public class TipoOperadorChoferEnum
    {
        public static byte Chofer = (byte)tipoOperadorChoferEnum.Chofer;
        public static byte Ayudante = (byte)tipoOperadorChoferEnum.Ayudante;
        public static byte Operador = (byte)tipoOperadorChoferEnum.Operador;
    }
    enum tipoOperadorChoferEnum : byte
    {
        Chofer = 1,
        Ayudante = 2,
        Operador = 3,
    }
}
