using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.OrdenCompra
{
    public class OrdenCompraAutorizacionModel : OrdenCompraDTO
    {
        public List<OrdenCompraProductoCrearDTO> Productos { get; set; }
    }
}