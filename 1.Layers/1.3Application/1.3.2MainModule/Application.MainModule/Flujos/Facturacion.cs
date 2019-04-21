using Application.MainModule.AdaptadoresDTO.Facturacion;
using Application.MainModule.AdaptadoresDTO.Ventas;
using Application.MainModule.com.admingest;
using Application.MainModule.DTOs;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.DTOs.Ventas;
using Application.MainModule.Servicios.Catalogos;
using Application.MainModule.Servicios.Facturacion;
using Sagas.MainModule.Entidades;
using Sagas.MainModule.ObjetosValor.Constantes;
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
        public RespuestaDto GenerarFacturaGlobal(CFDIDTO dto)
        {
            var _comp = CFDIServicio.DatosComprobante(dto);
            _comp.Receptor = CFDIServicio.DatosReceptor(dto);
            _comp.Concepto = CFDIServicio.DatosConceptos(dto).ToArray();

            return CFDIServicio.Timbrar(_comp, dto).RespuestaTimbrado;
        }
        public CFDIDTO GenerarFactura(CFDIDTO dto)
        {
            var _comp = CFDIServicio.DatosComprobante(dto);
            _comp.Receptor = CFDIServicio.DatosReceptor(dto);
            _comp.Concepto = CFDIServicio.DatosConceptos(dto).ToArray();

            dto.Id_MetodoPago = Convert.ToInt32(_comp.MetodoPago.Equals(MetodoPagoConst.Pago_en_parcialidades_o_diferido) ? MetodoPagoConst.IDPPD : MetodoPagoConst.IDPUE);
            dto.Folio = Convert.ToInt32(_comp.Folio);
            dto.Serie = _comp.Serie;
            dto.UUID = string.Empty;
            dto.VersionCFDI = ConfigurationManager.AppSettings["VersionCFDI"];
            dto.RespuestaTimbrado = CFDIServicio.Crear(CFDIAdapter.FromDTO(dto));

            if (!dto.RespuestaTimbrado.Exito) return dto;
            else dto.Id_RelTF = dto.RespuestaTimbrado.Id;
            return CFDIServicio.Timbrar(_comp, dto);
        }
        public RespuestaDto GenerarFactura(List<CFDIDTO> dtos)
        {
            var respuestas = dtos.Select(x => GenerarFactura(x)).ToList();
            return CFDIServicio.DatosRespuesta(respuestas);
        }
        public List<CFDIDTO> BuscarFacturasPorRFC(string RFC)
        {
            var cfdis = CFDIServicio.BuscarPorRFC(RFC);
            return CFDIAdapter.ToDTO(cfdis);
        }
        public List<CFDIDTO> BuscarFacturasPorRFC()
        {
            //Validar Permiso

            var cfdis = CFDIServicio.BuscarPorRFC("XAXX010101000");
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
            //Validar permisos

            //Descomentar en produccion 
            //var validacion = CFDIServicio.ValidarRangoBusqueda(model);
            //if (!validacion.Exito) return new List<VentaPuntoVentaDTO>(); //validacion;  

            var ventas = PuntoVentaServicio.ObtenerVentasPorCliente(model.IdCliente, model.FechaIni, model.FechaFinal);
            return CajaGeneralAdapter.ToDTOP(ventas);
        }
    }
}
