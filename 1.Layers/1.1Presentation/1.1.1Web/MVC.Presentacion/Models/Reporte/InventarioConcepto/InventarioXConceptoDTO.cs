using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models
{
    public class InventarioXConceptoDTO
    {
        public int IdREgristro { get; set; }
        public string Descirpcion { get; set; }
        public decimal Existencias { get; set; }
        public string Categoria { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }
}