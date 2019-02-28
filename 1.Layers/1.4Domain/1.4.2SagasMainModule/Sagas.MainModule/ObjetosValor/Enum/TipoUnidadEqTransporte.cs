using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagas.MainModule.ObjetosValor.Enum
{
    public static class TipoUnidadEqTransporteEnum
    {
        public static int Camioneta = (int)TipoUnidadEqTransporte.Camioneta;
        public static int Pipa = (int)TipoUnidadEqTransporte.Pipa;
        public static int Utilitario = (int)TipoUnidadEqTransporte.Utilitario;
    }
    public enum TipoUnidadEqTransporte
    {
        Camioneta = 1,
        Pipa = 2,
        Utilitario = 3,
    }
    public enum STipoUnidad { Camioneta, Pipa, Utilitario };   
}
