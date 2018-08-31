using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.MainModule.OrdenCompra.Model
{
    public class OrdenCompraProductoCrearDTO
    {
        public int IdProducto { get; set; }
        public int IdCentroCosto { get; set; }
        public string CentroCosto { get; set; }
        public string ProductoServicioTipo { get; set; }
        public string Producto { get; set; }
        public string TipoProducto { get; set; }
        public int IdProveedor { get; set; }
        public int IdCuentaContable { get; set; }
        public string CuentaContable { get; set; }
        public string Categoria { get; set; }
        public string Linea { get; set; }
        public string UnidadMedida { get; set; }
        public string UnidadMedida2 { get; set; }
        public string Descripcion { get; set; }
        public decimal Cantidad { get; set; }
        public decimal CantidadRequerida { get; set; }
        public decimal Precio { get; set; }
        public decimal Descuento { get; set; }
        public decimal IVA { get; set; }
        public decimal IEPS { get; set; }
        public decimal Importe { get; set; }
        public bool EsActivoVenta { get; set; }
        public bool EsGas { get; set; }
    }
}