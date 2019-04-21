using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.MainModule
{
    public static class FechasFunciones
    {
        private static Dictionary<string, DateTime> DiasFestivos = new Dictionary<string, DateTime>()
        {
            { "AñoNuevo", Convert.ToDateTime("01/01/" + DateTime.Now.Year.ToString() ) },
            { "ConstitucionMexicana", Convert.ToDateTime("06/02/" + DateTime.Now.Year.ToString() ) },
            { "NatalicioBenitoJuarez", Convert.ToDateTime("21/03/" + DateTime.Now.Year.ToString() ) },
            { "DiaTrabajo", Convert.ToDateTime("01/05/" + DateTime.Now.Year.ToString() ) },
            { "BatallaPuebla", Convert.ToDateTime("05/05/" + DateTime.Now.Year.ToString() ) },
            { "DiaIndependencia", Convert.ToDateTime("16/09/" + DateTime.Now.Year.ToString() ) },
            { "DiaMuertos", Convert.ToDateTime("02/11/" + DateTime.Now.Year.ToString() ) },
            { "RevolucionMexicana", Convert.ToDateTime("20/11/" + DateTime.Now.Year.ToString() ) },
            { "Navidad", Convert.ToDateTime("25/12/" + DateTime.Now.Year.ToString() ) }
        };

        public static int diasHabilesEntreDosFechas(DateTime fechaMenor, DateTime fechaMayor)
        {
            int contador = 0;
            DateTime fechaContador = fechaMenor;
            do
            {
                if (!fechaContador.DayOfWeek.Equals(DayOfWeek.Saturday)
                    && !fechaContador.DayOfWeek.Equals(DayOfWeek.Sunday)
                    && !DiasFestivos.ContainsValue(fechaContador))
                    contador++;

                fechaContador = fechaContador.AddDays(1);
            } while (fechaContador.CompareTo(fechaMayor) < 0);

            return contador;
        }

        public static double ObtenerMinutosEntreDosFechas(DateTime fechaMenor, DateTime fechaMayor)
        {
            TimeSpan resultado = fechaMayor.Subtract(fechaMenor);
            return resultado.TotalMinutes;
        }
        public static bool validaFechaInicialFinal(DateTime Inicial, DateTime Fianl)
        {
            if (Inicial < Fianl) return true;
            return false;
        }
        public static bool validaFechaDentroDelMes(DateTime Fecha)
        {
            if (Fecha.Month.Equals(DateTime.Now.Month) && Fecha.Year.Equals(DateTime.Now.Year))
                return true;
            return false;
        }
    }
}
