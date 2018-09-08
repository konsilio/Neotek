using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Catalogos
{
    public class CatalogoRespuestaDTO
    {
        public bool Exito { get; set; }
        public int Id { get; set; }
        public string Mensaje { get; set; }
    }
}