using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.MainModule
{
    public static class FilterFunciones
    {
        public static List<string> ObtenerFields(string fields, char separador = '|')
        {
            return fields.Split(separador).ToList();
        }
        public static string ConcatenarLista(List<String> lstrng, char separador = '|')
        {
            string cadenaFinal = string.Empty;
            lstrng.ForEach(x => string.Concat(cadenaFinal, x, separador));
            return cadenaFinal.Substring(0, cadenaFinal.Length - 1);
        }
        public static Dictionary<string, string> ObtenerFilters(string filters)
        {
            Dictionary<string, string> filtros = new Dictionary<string, string>();
            List<string> listaFiltros = ObtenerFields(filters);

            string[] separador = new string[] { "::" };

            foreach (string filtro in listaFiltros)
            {
                string[] filtroDividido = filtro.Split(separador, StringSplitOptions.None);

                filtroDividido[0] = string.IsNullOrEmpty(filtroDividido[0]) ? string.Empty : filtroDividido[0];
                filtroDividido[1] = string.IsNullOrEmpty(filtroDividido[1]) ? string.Empty : filtroDividido[1];

                filtros.Add(filtroDividido[0], filtroDividido[1]);
            }

            return filtros;
        }

        public static string ObtenerSort(string sorts)
        {
            return sorts.Replace('|', ',').Replace("-", "DESC ").Replace("+", "ASC ");
        }
    }
}
