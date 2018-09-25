using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Requisicion
{
    [Serializable]
    public class RequisicionProductoDTO
    {
        public int IdRequisicion { get; set; }
        public int IdProducto { get; set; }
        public short Orden { get; set; }
        public string Producto { get; set; }
        public int IdTipoProducto { get; set; }
        public string TipoProducto { get; set; }
        public int IdCentroCosto { get; set; }
        public string CentroCosto { get; set; }
        public int IdUnidad { get; set; }
        public string Unidad { get; set; }
        public decimal Cantidad { get; set; }
        public string Aplicacion { get; set; }
        public Nullable<bool> RevisionFisica { get; set; }
        public Nullable<decimal> CantidadAlmacenActual { get; set; }
        public Nullable<decimal> CantidadAComprar { get; set; }
        public Nullable<bool> AutorizaEntrega { get; set; }
        public Nullable<bool> AutorizaCompra { get; set; }
    }
}