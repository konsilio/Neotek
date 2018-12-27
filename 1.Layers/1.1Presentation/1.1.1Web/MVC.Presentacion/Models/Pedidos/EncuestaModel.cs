using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Pedidos
{
    public class EncuestaModel
    {
        public int IdRespuesta { get; set; }
        public int IdPedido { get; set; }
        public int IdPregunta { get; set; }
        public string Respuesta { get; set; }
    }
}