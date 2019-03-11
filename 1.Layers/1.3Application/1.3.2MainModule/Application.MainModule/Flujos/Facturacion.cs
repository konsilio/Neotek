﻿using Application.MainModule.AdaptadoresDTO.Facturacion;
using Application.MainModule.com.admingest;
using Application.MainModule.DTOs;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.Facturacion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Flujos
{
    public class Facturacion
    {
        public CFDIDTO GenerarFactura(CFDIDTO dto)
        {
            var _comp = CFDIServicio.DatosComprobante(dto);
            _comp.Receptor = CFDIServicio.DatosReceptor(dto);
            _comp.Concepto = CFDIServicio.DatosConceptos(dto).ToArray();

            dto.Folio = Convert.ToInt32(_comp.Folio);
            dto.Serie = _comp.Serie;
            dto.UUID = string.Empty;

            dto.VersionCFDI = ConfigurationManager.AppSettings["VersionCFDI"];
            dto.RespuestaTimbrado = CFDIServicio.Crear(CFDIAdapter.FromDTO(dto));
            if (!dto.RespuestaTimbrado.Exito) return dto;
            return CFDIServicio.Timbrar(_comp, dto);
        }
        public List<CFDIDTO> GenerarFactura(List<CFDIDTO> dtos)
        {
            return dtos.Select(x => GenerarFactura(x)).ToList();
        }
    }
}
