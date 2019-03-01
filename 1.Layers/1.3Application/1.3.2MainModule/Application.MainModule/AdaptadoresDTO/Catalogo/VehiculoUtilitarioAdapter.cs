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
   public class VehiculoUtilitarioAdapter
    {
        public static CUtilitario FromDTO(EquipoTransporteDTO ec)
        {
            return new CUtilitario()
            {
                //IdCamioneta = ec.IdCamioneta,
                IdEmpresa = ec.IdEmpresa,
                Numero = getNum(ec.Descripcion),
                Nombre = getStr(ec.Descripcion),
                Activo = ec.Activo,
                EsForaneo = ec.EsForaneo,
                FechaRegistro = DateTime.Now,
            };
        }
        public static CUtilitario FromEntity(CUtilitario ec)
        {
            return new CUtilitario()
            {
                //IdCamioneta = ec.IdCamioneta,
                IdEmpresa = ec.IdEmpresa,
                Numero = ec.Numero,
                Nombre = ec.Nombre,
                Activo = ec.Activo,
                EsForaneo = ec.EsForaneo,
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
            return string.Format("Utilitario No. {0}", cadena);
        }
    }
}
