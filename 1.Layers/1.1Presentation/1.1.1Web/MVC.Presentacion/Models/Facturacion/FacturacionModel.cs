using MVC.Presentacion.Models.Catalogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Facturacion
{
    public class FacturacionModel
    {    
        public int IdCliente { get; set; }
        public string RFC { get; set; }     
        public short IdEmpresa { get; set; }
        public string Ticket { get; set; }
        public System.DateTime FechaVenta { get; set; }     
    }
}