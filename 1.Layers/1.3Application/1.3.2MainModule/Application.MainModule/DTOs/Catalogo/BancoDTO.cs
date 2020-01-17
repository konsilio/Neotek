using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Catalogo
{
    public class BancoDTO
    {
        public short IdBanco { get; set; }
        public string Clave { get; set; }
        public string NombreCorto { get; set; }
        public string NombreRazónSocial { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
