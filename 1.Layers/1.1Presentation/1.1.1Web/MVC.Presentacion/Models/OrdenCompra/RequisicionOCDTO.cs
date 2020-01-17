using MVC.Presentacion.Models.Requisicion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.OrdenCompra
{
    [Serializable]
    public class RequisicionOCDTO : RequisicionDTO
    {
        public bool EsGasTransporte { get; set; }
        public List<ProductoOCDTO> ProductosOC { get; set; }
    }
}