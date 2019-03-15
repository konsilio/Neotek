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
                Id_RelTF = entidad.Id_RelTF,
                Id_FolioVenta = entidad.Id_FolioVenta,
                Id_FormaPago = entidad.Id_FormaPago,
                Id_MetodoPago = entidad.Id_MetodoPago,
                //UsoCFDI = entidad. ,
                VersionCFDI = entidad.VersionCFDI,
                FechaTimbrado = entidad.FechaTimbrado,
                UUID = entidad.UUID,
                Folio = entidad.Folio,
                Serie = entidad.Serie,
                URLPdf = entidad.URLPdf,
                URLXml = entidad.URLXml,
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
                Id_RelTF = dto.Id_RelTF,
                Id_FolioVenta = dto.Id_FolioVenta,
                Id_FormaPago = dto.Id_FormaPago,
                Id_MetodoPago = dto.Id_MetodoPago,
                //UsoCFDI = entidad. ,
                VersionCFDI = dto.VersionCFDI,
                FechaTimbrado = dto.FechaTimbrado,
                UUID = dto.UUID,
                Folio = dto.Folio,
                Serie = dto.Serie,
                URLPdf = dto.URLPdf,
                URLXml = dto.URLXml,
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
                Id_RelTF = entidad.Id_RelTF,
                Id_FolioVenta = entidad.Id_FolioVenta,
                Id_FormaPago = entidad.Id_FormaPago,
                Id_MetodoPago = entidad.Id_MetodoPago,
                VersionCFDI = entidad.VersionCFDI,
                FechaTimbrado = entidad.FechaTimbrado,
                UUID = entidad.UUID,
                Folio = entidad.Folio,
                Serie = entidad.Serie,
                URLPdf = entidad.URLPdf,
                URLXml = entidad.URLXml,
            };
        }      
    }
}
