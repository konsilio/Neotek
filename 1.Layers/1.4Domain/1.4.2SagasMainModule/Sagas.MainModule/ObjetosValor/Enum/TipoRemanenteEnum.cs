using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagas.MainModule.ObjetosValor.Enum
{
    public class TipoRemanenteEnum
    {
        public static int General = (int)tipoRemanenten.general;
        public static int PorPuntoVenta = (int)tipoRemanenten.porPuntoVenta;
    }
    enum tipoRemanenten : int
    {
        general = 1,
        porPuntoVenta = 2,
    }
}
