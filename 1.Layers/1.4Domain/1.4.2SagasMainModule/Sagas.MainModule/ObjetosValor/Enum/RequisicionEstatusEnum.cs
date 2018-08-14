using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagas.MainModule.ObjetosValor.Enum
{
    public static class RequisicionEstatusEnum
    {
        public static byte Creado = (byte)ReqEstatusEnum.Creado;
        public static byte Modificado = (byte)ReqEstatusEnum.Modificado;
        public static byte Eliminado = (byte)ReqEstatusEnum.Eliminado;
    }

    enum ReqEstatusEnum
    {
        Creado = 3,
        Modificado = 4,
        Eliminado = 5
    }
}
