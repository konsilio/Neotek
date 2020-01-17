using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Compras
{
    public static class CalcularPagoServicio
    {
        public static OrdenCompraPago CalcularPago(OrdenCompraPago pago, OrdenCompra oc)
        {
            var pagos = OrdenCompraPagoServicio.BuscarPagos(oc.IdOrdenCompra);
            if (pagos == null || pagos.Count.Equals(0))
            {
                pago.Orden = 1;
                pago.TotalImporte = oc.Total ?? 0;
                pago.SaldoInsoluto = CalculaSaldoInsoluto(oc.Total.Value, pago.MontoPagado);                
            }
            else
            {
                var ultimoPago = pagos.OrderByDescending(x => x.Orden).First();
                pago.Orden = ultimoPago.Orden++;
                pago.TotalImporte = ultimoPago.SaldoInsoluto;
                pago.SaldoInsoluto = CalculaSaldoInsoluto(ultimoPago.SaldoInsoluto, pago.MontoPagado);
            }
            pago.IdBanco = oc.Proveedor.IdBanco;
            return pago;
        }
        public static decimal CalculaSaldoInsoluto(decimal Total, decimal MontoPagado)
        {
            return Total - MontoPagado;
        }
    }
}
