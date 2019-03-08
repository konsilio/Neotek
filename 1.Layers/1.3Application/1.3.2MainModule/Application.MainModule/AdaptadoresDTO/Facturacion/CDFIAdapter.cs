using Application.MainModule.DTOs;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO.Facturacion
{
    public static class CFDIAdapter
    {
        public static CFDIDTO ToDTO(CFDI entidad)
        {
            return new CFDIDTO()
            {
               
            };
        }
        public static List<CFDIDTO> ToDTO(List<CFDI> entidad)
        {
            return entidad.Select(x => ToDTO(x)).ToList();
        }
        public static CFDI FromDTO(CFDIDTO dto)
        {
            return new CFDI()
            {
               
            };
        }
        public static List<CFDI> FromDTO(List<CFDIDTO> entidad)
        {
            return entidad.Select(x => FromDTO(x)).ToList();
        }
        public static CFDI FormEmtity(CFDI entidad)
        {
            return new CFDI()
            {
           
            };
        }
    }
}
