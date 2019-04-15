using MVC.Presentacion.Models.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models
{
    public class FacturacionGlobalModel
    {
        public int IdCliente { get; set; }
        public DateTime FechaIni { get; set; }
        public DateTime FechaFinal { get; set; }
        public List<VentaPuntoVentaDTO> Tickets { get; set; }
    }
}