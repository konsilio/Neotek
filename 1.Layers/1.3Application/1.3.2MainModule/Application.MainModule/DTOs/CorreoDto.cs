using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs
{
    public class CorreoDto
    {
        public string De { get; set; }
        public string Para { get; set; }
        public List<string> ParaLista { get; set; }
        public string Mensaje { get; set; }
        public string Asunto { get; set; }
        public List<string> ConCopia { get; set; }
        public List<string> ConCopiaOculta { get; set; }
        public string RutaArchivo { get; set; }
    }
}
