using Application.MainModule.DTOs;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Catalogos
{
    public static class ReporteServicio
    {
        public static List<RepCuentaPorPagarDTO> CalcularCuentasPorPagar(List<Requisicion> ListRequisiciones)
        {
            List<RepCuentaPorPagarDTO> respuesta = new List<RepCuentaPorPagarDTO>();
            foreach (var req in ListRequisiciones)
            {
                var idcuenta = 1;
                if (ValidarOrdenesDeCompra(req.OrdenesCompra.ToList()))
                {
                    foreach (var oc in req.OrdenesCompra)
                    {
                        RepCuentaPorPagarDTO cpp = new RepCuentaPorPagarDTO();
                        cpp.IdCuenta = idcuenta;
                        cpp.CuentaContable = CuentaContableServicio.Obtener(oc.IdCuentaContable).Descripcion;
                        cpp.SaldoPasivo = ObtenerSaldoPasivo(oc);
                        cpp.SaldoPagado = ObtenerSaldoPagado(oc);
                        cpp.SaldoInsoluto = ObtenerSaldoInsoluto(oc);
                        idcuenta++;
                    }
                }
            }

            return respuesta;
        }
        private static bool ValidarOrdenesDeCompra(List<OrdenCompra> oc)
        {
            if (oc == null)
                return false;
            if (oc.Count <= 0)
                return false;
            return true;
        }
        private static double ObtenerSaldoPasivo(OrdenCompra oc)
        {
            return Convert.ToDouble(oc.Total.Value);
        }
        private static double ObtenerSaldoPagado(OrdenCompra oc)
        {
            return oc.OrdenCompraPago != null ? Convert.ToDouble(oc.OrdenCompraPago.Sum(x => x.TotalImporte)) : 0;
        }
        private static double ObtenerSaldoInsoluto(OrdenCompra oc)
        {
            return oc.OrdenCompraPago != null ? Convert.ToDouble(oc.OrdenCompraPago.OrderBy(x => x.FechaConfirmacion).FirstOrDefault().SaldoInsoluto) : Convert.ToDouble(oc.Total.Value);
        }
    }
}
