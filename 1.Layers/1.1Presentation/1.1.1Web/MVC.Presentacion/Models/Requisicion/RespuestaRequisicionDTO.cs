using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Requisicion
{
    public class RespuestaRequisicionDTO
    {
        public int IdRequisicion { get; set; }
        public string NumRequisicion { get; set; }
        public bool Exito { get; set; }
        public string Mensaje { get; set; }
    }
}