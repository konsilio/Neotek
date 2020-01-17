using Exceptions.MainModule.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Requisicion
{
    public class RequisicionCancelaDTO
    {
        public int IdRequisicion { get; set; }
        public string MotivoCancelacion { get; set; }
    }
}