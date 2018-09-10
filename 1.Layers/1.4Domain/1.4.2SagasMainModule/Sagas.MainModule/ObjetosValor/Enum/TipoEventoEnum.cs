using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagas.MainModule.ObjetosValor.Enum
{
    public static class TipoEventoEnum
    {
       public static byte Inicial = (byte)tipoEvento.Inicial;
       public static byte Final = (byte)tipoEvento.Final;

        enum tipoEvento : byte
        {
            Inicial = 1,
            Final = 2,
        }
    }
}
