using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagas.MainModule.ObjetosValor.Enum
{
    public static class TipoUnidadAlmacenGasEnum
    {
        public static byte Movil = (byte)TipoUnidadAlmacenGas.Movil;
        public static byte Fijo = (byte)TipoUnidadAlmacenGas.Fijo;        
    }

    public enum TipoUnidadAlmacenGas
    {
        Movil = 1,
        Fijo = 2,
    }
}
