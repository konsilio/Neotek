using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Catalogos
{
    public class LineaProductoDTO
    {
        public short IdProductoLinea { get; set; }
        public short IdEmpresa { get; set; }
        public string Linea { get; set; }
        public string Descripcion { get; set; }
    }
}