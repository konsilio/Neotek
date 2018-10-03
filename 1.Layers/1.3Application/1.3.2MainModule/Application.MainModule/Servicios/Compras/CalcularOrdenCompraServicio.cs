using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.Servicios.Almacen;
using Application.MainModule.Servicios.Catalogos;
using Sagas.MainModule.Entidades;
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
        /// <summary>
        /// Calcula los totales de la orden de compra sumando los importes de los productos
        /// </summary>
        /// <param name="ocs"></param>
        /// <returns></returns>
        public static List<OrdenCompra> CalcularTotales(List<OrdenCompra> ocs)
        {
            foreach (var oc in ocs)
            {
                foreach (var prod in oc.Productos)
                {
                    //Se validan valores nulos para inicialicar
                    if (oc.Iva == null) oc.Iva = 0;
                    if (oc.Ieps == null) oc.Ieps = 0;
                    if (oc.SubtotalSinIeps == null) oc.SubtotalSinIeps = 0;
                    if (oc.SubtotalSinIva == null) oc.SubtotalSinIva = 0;
                    if (oc.Total == null) oc.Total = 0;
                    oc.Iva += (prod.Precio * (prod.IVA / 100));
                    oc.Ieps += (prod.Precio * (prod.IEPS / 100));
                    oc.SubtotalSinIeps = prod.Importe - oc.Ieps;
                    oc.SubtotalSinIva = prod.Importe - oc.Iva;
                    oc.Total += prod.Importe;
                    if (prod.EsGas) oc.EsGas = true;
                    if (prod.EsActivoVenta) oc.EsActivoVenta = true;
                }
            }
            return ocs;
        }
        public static Sagas.MainModule.Entidades.Requisicion CalcularAlmacenProcutos(Sagas.MainModule.Entidades.Requisicion _requisicion)
        {
            int x = 0;
            foreach (RequisicionProducto _prod in _requisicion.Productos)
            {
                var prod = ProductoServicio.ObtenerProducto(_prod.IdProducto);
                if (prod.EsGas)
                {
                    _requisicion.Productos.ElementAt(x).CantidadAlmacenActual = AlmacenGasServicio.ObtenerCantidadActualAlmacenGeneral(_requisicion.IdEmpresa);
                    _requisicion.Productos.ElementAt(x).CantidadAComprar = _prod.Cantidad;
                }
                else
                {
                    var almacen = new AlmacenDataAccess().ProductoAlmacen(_prod.IdProducto, _requisicion.IdEmpresa);
                    _requisicion.Productos.ElementAt(x).CantidadAlmacenActual = almacen != null ? almacen.Cantidad : 0;
                    if (_prod.CantidadAlmacenActual.Value > _prod.Cantidad)
                        _requisicion.Productos.ElementAt(x).CantidadAComprar = 0;
                    else
                        _requisicion.Productos.ElementAt(x).CantidadAComprar = Math.Abs(_prod.CantidadAlmacenActual.Value - _prod.Cantidad);
                }
                _requisicion.Productos.ElementAt(x).EsActivoVenta = prod.EsActivoVenta;
                _requisicion.Productos.ElementAt(x).EsGas = prod.EsGas;
                _requisicion.Productos.ElementAt(x).EsTransporteGas = prod.EsTransporteGas;

                x++;
            }
            return _requisicion;
        }
    }
}
