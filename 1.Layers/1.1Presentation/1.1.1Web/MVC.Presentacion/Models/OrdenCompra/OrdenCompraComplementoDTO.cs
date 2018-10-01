using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.OrdenCompra
{
    public class OrdenCompraComplementoDTO : OrdenCompraModel
    {
        public string FolioFiscalUUID { get; set; }
        public string FolioFactura { get; set; }
        public Nullable<System.DateTime> FechaResgistroFactura { get; set; }
    }
}