using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Catalogo
{
    public class LineaProductoDto
    {
        public short IdProductoLinea { get; set; }
        public short IdEmpresa { get; set; }
        public string Linea { get; set; }
        public string Descripcion { get; set; }
    }
}
