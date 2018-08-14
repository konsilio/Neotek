using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.MainModule.Requisicion.Model
{
    [Serializable]
    public class RequisicionProductoDTO
    {
        public int IdRequisicion { get; set; }
        public int IdProducto { get; set; }
        public int IdTipoProducto { get; set; }
        public int IdCentroCosto { get; set; }
        public decimal Cantidad { get; set; }
        public string Aplicacion { get; set; }
        public Nullable<bool> RevisionFisica { get; set; }
        public Nullable<decimal> CantidadAlmacenActual { get; set; }
        public Nullable<decimal> CantidadAComprar { get; set; }
        public Nullable<bool> AutorizaEntrega { get; set; }
        public Nullable<bool> AutorizaCompra { get; set; }
    }
}