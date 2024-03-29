﻿using Application.MainModule.DTOs;
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
                Numero = getNum(ec.Descripcion),
                Nombre = getStr(ec.Descripcion),
                Activo = ec.Activo,
                EsForaneo = ec.EsForaneo,
                FechaRegistro = DateTime.Now,
            };
        }
        public static Pipa FromEntity(Pipa ec)
        {
            return new Pipa()
            {
                //IdCamioneta = ec.IdCamioneta,
                IdEmpresa = ec.IdEmpresa,
                Numero = ec.Numero,
                Nombre = ec.Nombre,
                Activo = ec.Activo,
                EsForaneo = ec.EsForaneo,
                FechaRegistro = ec.FechaRegistro,
                Serie = ec.Serie,
                Folio = ec.Folio,
            };
        }
        public static string getNum(string cadena)
        {
            string resultString = Regex.Match(cadena, @"\d+").Value;
            return resultString;
        }
        public static string getStr(string cadena)
        {
            return string.Format("Pipa No.{0}", cadena);
         
        }
    }
}
