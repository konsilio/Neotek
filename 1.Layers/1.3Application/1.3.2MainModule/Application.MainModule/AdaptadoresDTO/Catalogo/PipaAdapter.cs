using Application.MainModule.DTOs;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO.Catalogo
{
   public class PipaAdapter
    {
        public static Pipa FromDTO(EquipoTransporteDTO ec)
        {
            return new Pipa()
            {
                //IdCamioneta = ec.IdCamioneta,
                IdEmpresa = ec.IdEmpresa,
                Numero = getNum(ec.AliasUnidad),
                Nombre = getStr(ec.AliasUnidad),
                Activo = ec.Activo,
                FechaRegistro = DateTime.Now,
            };
        }

        public static string getNum(string cadena)
        {
            string resultString = Regex.Match(cadena, @"\d+").Value;
            return resultString;
        }
        public static string getStr(string cadena)
        {
            string resultString = cadena.Split(' ')[0];
            return resultString;
        }
    }
}
