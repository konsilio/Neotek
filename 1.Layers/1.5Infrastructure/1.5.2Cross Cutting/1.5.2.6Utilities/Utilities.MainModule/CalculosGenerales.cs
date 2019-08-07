using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.MainModule
{
    public static class CalculosGenerales
    {
        public static int DiferenciaEntreDosNumero(int cantidadMayor, int cantidadMenor)
        {
            if (cantidadMayor < cantidadMenor)
                return cantidadMenor - cantidadMayor;
            return cantidadMayor - cantidadMenor;
        }
        public static decimal DiferenciaEntreDosNumero(decimal cantidadMayor, decimal cantidadMenor)
        {
            if (cantidadMayor < cantidadMenor)
                return cantidadMenor - cantidadMayor;
            return cantidadMayor - cantidadMenor;
        }
        public static double DiferenciaEntreDosNumero(double cantidadMayor, double cantidadMenor)
        {
            if (cantidadMayor < cantidadMenor)
                return cantidadMenor - cantidadMayor;
            return cantidadMayor - cantidadMenor;
        }
        public static decimal Truncar(decimal cantidad, decimal digitos)
        {
            decimal decimales = 1;
            for (int i = 0; i < digitos; i++)            
                decimales = decimales * 10;

            return Math.Truncate(cantidad * decimales) / decimales;
        }
        public static int Promediar(int total, int cantidad)
        {
            return total / cantidad;
        }
        public static decimal Promediar(decimal total, int cantidad)
        {
            return total / cantidad;
        }
        public static decimal Promediar(decimal total, decimal cantidad)
        {
            return total / cantidad;
        }
    }
}
