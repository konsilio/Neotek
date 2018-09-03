using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.MainModule.OrdenCompra.Model
{
    [Serializable]
    public class OrdenCompraCrearDTO : OrdenCompraDTO
    {
        public List<OrdenCompraProductoCrearDTO> Productos { get; set; }
    }
}