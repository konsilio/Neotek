using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Mobile
{
    public class LineaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public List<ProductoDTO> Productos { get; set; } 
    }
}
