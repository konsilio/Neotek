using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models
{
    public class CreditoRecuperadoDTO
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public decimal Total { get; set; }
        public List<CreditoRecuperadoAbonoDTO> Abonos { get; set; }



    }

    public class CreditoRecuperadoAbonoDTO
    {
        public string Nota { get; set; }
        public string FechaCarga { get; set; }
        public DateTime FechaAbono { get; set; }
        public decimal Importe { get; set; }
        public string FormaDePago { get; set; }

    }
}