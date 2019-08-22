using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models
{
    public class InventarioXConceptoDTO
    {
        public int IdRegistro { get; set; }
        public string Descripcion { get; set; }
        public decimal Existencias { get; set; }
        public string Categoria { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }
}