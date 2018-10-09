using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagas.MainModule.ObjetosValor.Enum
{
    public static class CalibracionDestinoEnum
    {
        public static byte MismoTanque = (byte)CalibracionDestino.MismoTanque;
        public static byte TanquePortatil = (byte)CalibracionDestino.TanquePortatil;

        public enum CalibracionDestino : byte
        {
            MismoTanque = 1,
            TanquePortatil = 2,
        }
    }
}
