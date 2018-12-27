using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Cobranza
{
    public class CargosModel
    {
        public int IdCargo { get; set; }
        public int IdCliente { get; set; }
        public short IdEmpresa { get; set; }
        public string Ticket { get; set; }
        public System.DateTime FechaRegistro { get; set; }
        public decimal TotalCargo { get; set; }
        public decimal TotalAbonos { get; set; }
        public bool VentaExtraordinaria { get; set; }
        public bool Activo { get; set; }
        public System.DateTime FechaVencimiento { get; set; }
        public bool Saldada { get; set; }
    }
}