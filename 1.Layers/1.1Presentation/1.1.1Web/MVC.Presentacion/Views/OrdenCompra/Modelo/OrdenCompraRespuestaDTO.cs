using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.MainModule.OrdenCompra.Model
{
    public class OrdenCompraRespuestaDTO
    {
        public int IdOrdenCompra { get; set; }
        public string NumOrdenCompra{ get; set; }
        public bool Exito { get; set; }
        public string Mensaje { get; set; }
    }
}