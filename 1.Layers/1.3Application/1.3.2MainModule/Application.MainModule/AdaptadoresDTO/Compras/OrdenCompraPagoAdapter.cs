using Application.MainModule.DTOs.Compras;
using Application.MainModule.Servicios.Catalogos;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO.Compras
{
    public static class OrdenCompraPagoAdapter
    {
        public static OrdenCompraPago FromDTO(OrdenCompraPagoDTO dto)
        {
            return new OrdenCompraPago()
            {
                IdOrdenCompra = dto.IdOrdenCompra,
                IdBanco = dto.IdBanco,
                IdFormaPago = dto.IdFormaPago,
                CuentaBancaria = dto.CuentaBancaria,
                FechaRegistro = Convert.ToDateTime(DateTime.Today.ToShortDateString()),
                MontoPagado = dto.MontoPagado,
                PhysicalPathCapturaPantalla = dto.PhysicalPathCapturaPantalla,
                UrlPathCapturaPantalla = dto.UrlPathCapturaPantalla
            };
        }
        public static List<OrdenCompraPago> FromDTO(List<OrdenCompraPagoDTO> pago)
        {
            return pago.Select(x => FromDTO(x)).ToList();
        }
        public static OrdenCompraPagoDTO ToDTO(OrdenCompraPago pago)
        {
            return new OrdenCompraPagoDTO()
            {
                IdOrdenCompra = pago.IdOrdenCompra,
                IdBanco = pago.IdBanco,
                IdFormaPago = pago.IdFormaPago ?? 0,
                formaPago = FormaPagoServicio.Obtener(pago.IdFormaPago ?? 99).Descripcion,
                CuentaBancaria = pago.CuentaBancaria,
                FechaRegistro = Convert.ToDateTime(DateTime.Today.ToShortDateString()).Date,
                //FechaConfirmacion = pago.FechaConfirmacion != null ? Convert.ToDateTime(pago.FechaConfirmacion.Value.ToShortDateString()).Date : null,
                TotalImporte = pago.TotalImporte,
                MontoPagado = pago.MontoPagado,
                Orden = pago.Orden,
                PhysicalPathCapturaPantalla = pago.PhysicalPathCapturaPantalla ?? "",
                UrlPathCapturaPantalla = pago.UrlPathCapturaPantalla ?? ""
            };
        }
        public static List<OrdenCompraPagoDTO> ToDTO(List<OrdenCompraPago> pago)
        {
            return pago.Select(x => ToDTO(x)).ToList();
        }
        public static OrdenCompraPago FromEntity(OrdenCompraPago oc)
        {
            return new OrdenCompraPago()
            {
                IdOrdenCompra = oc.IdOrdenCompra,
                Orden = oc.Orden,
                IdBanco = oc.IdBanco,
                IdFormaPago = oc.IdFormaPago,
                CuentaBancaria = oc.CuentaBancaria,
                FechaRegistro = Convert.ToDateTime(DateTime.Today.ToShortDateString()),
                MontoPagado = oc.MontoPagado,
                PhysicalPathCapturaPantalla = oc.PhysicalPathCapturaPantalla,
                UrlPathCapturaPantalla = oc.UrlPathCapturaPantalla
            };
        }
    }
}
