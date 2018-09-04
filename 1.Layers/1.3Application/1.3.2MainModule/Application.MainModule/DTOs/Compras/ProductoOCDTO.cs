using Exceptions.MainModule.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Compras
{
    [Serializable]
    public class ProductoOCDTO : Requisicion.RequisicionProductoAutorizacionDTO
    {
        public int IdProveedor { get; set; }
        public int IdCuentaContable { get; set; }
        public int IdOrdenCompra { get; set; }     
        public string ProductoServicioTipo { get; set; }
        public string Categoria { get; set; }
        public string Linea { get; set; }
        public string UnidadMedida { get; set; }
        public string UnidadMedida2 { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public decimal Descuento { get; set; }
        public decimal IVA { get; set; }
        public decimal IEPS { get; set; }
        public decimal Importe { get; set; }
        public bool EsActivoVenta { get; set; }
        public bool EsGas { get; set; }

    }
}
