using MVC.Presentacion.Models.Cobranza;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models
{
    public class CreditoOtorgadoModel
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public decimal Total { get; set; }
        public decimal Litros { get; set; }
        public List<CreditoOtorgadoCargosDTO> Abonos { get; set; }
    }

    public class CreditoOtorgadoCargosDTO
    {
        public string Nota { get; set; }
        public string FechaCarga { get; set; }
        public decimal Importe { get; set; }
        public string Litros { get; set; }
        public string Vendedor { get; set; }
     

    }
}