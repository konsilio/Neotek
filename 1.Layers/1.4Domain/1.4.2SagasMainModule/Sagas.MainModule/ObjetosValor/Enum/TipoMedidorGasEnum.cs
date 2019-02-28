using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagas.MainModule.ObjetosValor.Enum
{
    public class TipoMedidorGasEnum
    {
        public static short Magnetel = (short)tipoMovimiento.Entrada;
        public static short Rotogate = (short)tipoMovimiento.Salida;
    }
    enum tipoMedidor : short
    {
        Magnetel = 1,
        Rotogate = 2,      
    }
    public enum stringMedidor { Magnetel, Rotogate };
}
