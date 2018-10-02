using Application.MainModule.DTOs.Compras;
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
                Orden = 1,
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
                IdFormaPago = pago.IdFormaPago,
                CuentaBancaria = pago.CuentaBancaria,
                FechaRegistro = Convert.ToDateTime(DateTime.Today.ToShortDateString()),
                MontoPagado = pago.MontoPagado,
                Orden = 1,
                PhysicalPathCapturaPantalla = pago.PhysicalPathCapturaPantalla,
                UrlPathCapturaPantalla = pago.UrlPathCapturaPantalla
            };
        }
        public static List<OrdenCompraPagoDTO> ToDTO(List<OrdenCompraPago> pago)
        {
            return pago.Select(x => ToDTO(x)).ToList();
        }
    }
}
