using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.OrdenCompra
{
    public class OrdenCompraModel
    {
        public int IdOrdenCompra { get; set;}
        public int IdRequisicion { get; set; }
        public int IdSolicitante { get; set; }
        public int IdProveedor { get; set; }
        public DateTime FechaRequerida { get; set;}
        public DateTime FechaEntrada { get; set; }
        public string MotivoCompra { get; set; }
        public string RequeridoEn { get; set; }
        public string btn { get; set; }
        public List<OrdenCompraPorductoDTO> OrdenCompraProductos { get; set; }

    }
}