using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Compras
{
    public static class CalcularOrdenCompraServicio
    {
        public static decimal ComplementoPrecioPorGalon(decimal montBelvieuDlls, decimal tarifaServicioPorGalonDlls, decimal tipoCambio)
        {
            return (montBelvieuDlls + tarifaServicioPorGalonDlls) * tipoCambio;
        }

        public static decimal ComplementoImporteEnLitros(decimal precioPorGalon, decimal factorGalonALitros)
        {
            return precioPorGalon / factorGalonALitros;
        }

        public static decimal ComplementoPVPMKg(decimal importeEnLitros, decimal factorCompraLitrosAKilos)
        {
            return importeEnLitros / factorCompraLitrosAKilos;
        }

        public static decimal Subtotal(decimal precio, decimal cantidad)
        {
            return precio * cantidad;
        }

        public static decimal Subtotal(List<decimal> montosASumar)
        {
            return montosASumar.Sum();
        }        

        public static decimal Iva(decimal subtotal, decimal porcentIva)
        {
            return subtotal * porcentIva;
        }

        public static decimal Ieps(decimal subtotal, decimal porcentIeps)
        {
            return subtotal * porcentIeps;
        }

        public static decimal Total(decimal subtotal, decimal iva, decimal ieps)
        {
            decimal SubtotalConIva = subtotal * iva;
            decimal SubtotalConIeps = subtotal * ieps;           

            return subtotal + SubtotalConIva + SubtotalConIeps;
        }

        public static decimal ComplementoPrecioTransporteDeGas(decimal masaKg, decimal factorConvTransporte)
        {
            return masaKg * factorConvTransporte;
        }
    }
}
