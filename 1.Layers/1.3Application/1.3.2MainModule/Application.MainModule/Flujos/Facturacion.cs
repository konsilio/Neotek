using Application.MainModule.AdaptadoresDTO.Facturacion;
using Application.MainModule.AdaptadoresDTO.Ventas;
using Application.MainModule.com.admingest;
using Application.MainModule.DTOs;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.DTOs.Ventas;
using Application.MainModule.Servicios.Catalogos;
using Application.MainModule.Servicios.Facturacion;
using Sagas.MainModule.Entidades;
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

        public List<CFDIDTO> BuscarFacturasPorRFC(string RFC)
        {
            var cfdis = CFDIServicio.BuscarPorRFC(RFC);
            return CFDIAdapter.ToDTO(cfdis);
        }
        public List<CFDIDTO> BuscarFacturasPorCliente(int id)
        {
            var cfdis = CFDIServicio.BuscarPorCliente(id);
            return CFDIAdapter.ToDTO(cfdis);
        }
        public CFDIDTO BuscarFacturasPorTicket(string folio)
        {
            var cfdis = CFDIServicio.Buscar(folio);
            return CFDIAdapter.ToDTO(cfdis);
        }
        public List<VentaPuntoVentaDTO> BuscarPorRFC(string RFC)
        {
            var ventas = PuntoVentaServicio.ObtenerVentasPorRFC(RFC);
            var _cfdi = BuscarFacturasPorRFC(RFC);
            ventas = CFDIServicio.DescartarFacturados(ventas, _cfdi);
            return CajaGeneralAdapter.ToDTOP(ventas);
        }
        public List<VentaPuntoVentaDTO> BuscarPorNumCliente(int Id)
        {
            var ventas = PuntoVentaServicio.ObtenerVentasPorCliente(Id);
            var _cfdi = BuscarFacturasPorCliente(Id);
            ventas = CFDIServicio.DescartarFacturados(ventas, _cfdi);
            return CajaGeneralAdapter.ToDTOP(ventas);
        }
        public VentaPuntoVentaDTO BuscarPorTicket(string ticket)
        {
            var venta = PuntoVentaServicio.Obtener(ticket);
            var cfdi = BuscarFacturasPorTicket(ticket);
            if (cfdi == null)
                return CajaGeneralAdapter.ToDTOP(venta);
            else
                return null;
        }
        public List<VentaPuntoVentaDTO> Buscar(FacturacionDTO model)
        {
            List<VentaPuntoVentaDTO> _list = new List<VentaPuntoVentaDTO>();
            if (model.IdCliente > 0)
                _list.AddRange(BuscarPorNumCliente(model.IdCliente));
            if (!model.RFC.Equals(string.Empty))
                _list.AddRange(BuscarPorRFC(model.RFC));

            return _list.Distinct(new VentaPuntoVentaComparer()).ToList();
        }
    }
}
