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
        public bool RevisionFisica { get; set; }
        public decimal CantidadAlmacenActual { get; set; }
        public decimal CantidadAComprar { get; set; }
        public bool AutorizaEntrega { get; set; }
        public bool AutorizaCompra { get; set; }
    }
}