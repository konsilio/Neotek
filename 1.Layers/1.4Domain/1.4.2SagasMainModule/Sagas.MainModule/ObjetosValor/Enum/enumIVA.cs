using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sagas.MainModule.ObjetosValor.Enum
{
    public static class IvaEnum
    {
        public static decimal p16 = (decimal)((int)enumIVA.iva16 / 100);
        public static decimal p4 = (decimal)((int)enumIVA.iva4 / 100);
        public static decimal p0 = (decimal)((int)enumIVA.iva0 / 100);
    }

    public enum enumIVA
    {
        iva16 = 16,
        iva4 = 4,
        iva0 = 0
    }
}