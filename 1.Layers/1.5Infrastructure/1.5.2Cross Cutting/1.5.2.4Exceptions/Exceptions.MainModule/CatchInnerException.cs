﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions.MainModule
{
    public static class CatchInnerException
    {
        public static List<string> Obtener(Exception ex)
        {
            var lista = new List<string>();
            var inn = ex.InnerException;

            while (inn != null)
            {
                lista.Add(inn.Message);
                inn = inn.InnerException;
            }          
            return lista;
        }
        public static string ObtenerString(Exception ex)
        {
            string lista = string.Empty;
            var inn = ex.InnerException;

            while (inn != null)
            {
                lista = string.Concat(lista, inn.Message);
                inn = inn.InnerException;
            }
            return lista;
        }
    }
}
