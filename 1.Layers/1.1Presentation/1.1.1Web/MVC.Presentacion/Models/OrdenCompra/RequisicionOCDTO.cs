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
        public List<ProductoOCDTO> Productos { get; set; }
    }
}