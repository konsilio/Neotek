using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Catalogos
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