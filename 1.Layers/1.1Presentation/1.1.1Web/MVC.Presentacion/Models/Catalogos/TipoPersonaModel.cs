using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Catalogos
{
    public class TipoPersonaModel
    {
        public byte IdTipoPersona { get; set; }
        public string Descripcion { get; set; }
        public System.DateTime FechaRegistro { get; set; }
    }
}