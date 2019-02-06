using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagas.MainModule.ObjetosValor.Enum
{
    public static class TipoUnidadEqTransporteEnum
    {
        public static short Camioneta = (short)TipoUnidadEqTransporte.Camioneta;
        public static short Pipa = (short)TipoUnidadEqTransporte.Pipa;
        public static short Utilitario = (short)TipoUnidadEqTransporte.Utilitario;
    }
    public enum TipoUnidadEqTransporte
    {
        Camioneta = 1,
        Pipa = 2,
        Utilitario = 3,
    }
    public enum STipoUnidad { Camioneta, Pipa, Utilitario };   
}
