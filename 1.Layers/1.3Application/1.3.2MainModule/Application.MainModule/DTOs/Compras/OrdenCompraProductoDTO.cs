using System;

namespace Application.MainModule.DTOs
{
    [Serializable]
    public class OrdenCompraProductoDTO 
    {
        public int IdOrdenCompra { get; set; }
        public int IdProducto { get; set; }
        public int IdCentroCosto { get; set; }
        public string ProductoServicioTipo { get; set; }
        public string Producto { get; set; }
        public string Categoria { get; set; }
        public string Linea { get; set; }
        public string UnidadMedida { get; set; }
        public string UnidadMedida2 { get; set; }
        public string Descripcion { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Descuento { get; set; }
        public decimal IVA { get; set; }
        public decimal IEPS { get; set; }
        public decimal Importe { get; set; }
        public bool EsActivoVenta { get; set; }
        public bool EsGas { get; set; }

      
        public string Aplicacion { get; set; }
        public int IdTipoProducto { get; set; }
        public string TipoProducto { get; set; }       
        public string CentroCosto { get; set; }
        public int IdUnidad { get; set; }    
        public decimal CantidadAComprar { get; set; }
        public int IdProdveedor { get; set; }
        public int IdCuentaContable { get; set; }
        public string CuentaContable { get; set; }      

    }
}
