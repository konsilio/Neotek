using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models
{
    public class ImagenDTO
    {
        public string Nombre { get; set; }
        public short Oden { get; set; }
        public string Tipo { get; set; }
        public string Momento { get; set; }
        public string UrlImg { get; set; }
        public string PathImagen { get; set; }
        public short IdImagenDe { get; set; }
        public string CadenaBase64 { get; set; }
        public string Extencion { get; set; }
        public string Lectura { get; set; }
    }
}