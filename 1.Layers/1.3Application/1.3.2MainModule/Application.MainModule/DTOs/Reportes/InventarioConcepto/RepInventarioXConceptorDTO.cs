using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs
{
    public class RepInventarioXConceptorDTO
    {
        public int IdREgristro { get; set; }
        public string Descirpcion { get; set; }
        public decimal Existencias { get; set; }
        public string Categoria { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }
}
