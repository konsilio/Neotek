using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagas.MainModule.ObjetosValor.Enum
{
    public class TipoMovimientoEnum
    {
        public static byte Entrada = (byte)tipoMovimiento.Entrada;
        public static byte Salida = (byte)tipoMovimiento.Salida;
    }

    enum tipoMovimiento : byte
    {
        Entrada = 1,
        Salida = 2,        
    }

    public enum stringMovimiento { Entrada, Salida };

    public static class SMovimiento
    {
        public static string E = stringMovimiento.Entrada.ToString();
        public static string S = stringMovimiento.Salida.ToString();
      


    }
}
