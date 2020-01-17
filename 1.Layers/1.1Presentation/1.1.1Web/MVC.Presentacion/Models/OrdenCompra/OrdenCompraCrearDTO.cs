using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.OrdenCompra
{
    [Serializable]
    public class OrdenCompraCrearDTO : OrdenCompraDTO
    {
        public List<OrdenCompraProductoCrearDTO> ProductosDTO { get; set; }
    }
}