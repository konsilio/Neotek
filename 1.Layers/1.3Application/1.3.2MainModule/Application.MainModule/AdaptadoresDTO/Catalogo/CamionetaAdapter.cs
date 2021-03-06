﻿using Application.MainModule.DTOs;
using Application.MainModule.DTOs.Catalogo;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO.Catalogo
{
  public class CamionetaAdapter
    {
        public static Camioneta FromDTO(CamionetaDTO ec)
        {
            return new Camioneta()
            {
                IdCamioneta = ec.IdCamioneta,
                IdEmpresa = ec.IdEmpresa,               
                Numero = ec.Numero,
                Nombre = ec.Nombre,
                Activo = true,
                EsForaneo = ec.EsForaneo,
                FechaRegistro = DateTime.Now,
            };
        }
        public static Camioneta FromEntity(Camioneta ec)
        {
            return new Camioneta()
            {
                IdCamioneta = ec.IdCamioneta,
                IdEmpresa = ec.IdEmpresa,
                Numero = ec.Numero,
                Nombre = ec.Nombre,
                Activo = true,
                EsForaneo = ec.EsForaneo,
                FechaRegistro = ec.FechaRegistro,
                Serie = ec.Serie,
                Folio = ec.Folio,
            };
        }
        public static Camioneta FromDTO(EquipoTransporteDTO ec)
        {
            return new Camioneta()
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

        public static string getNum(string cadena)
        {
            string resultString = Regex.Match(cadena, @"\d+").Value;
            return resultString;
        }
        public static string getStr(string cadena)
        {
            return string.Format("Camioneta No. {0}", cadena);            
        }
    }
}
