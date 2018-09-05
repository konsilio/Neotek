﻿using Exceptions.MainModule.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.MainModule.Requisicion.Model
{
    public class RequisicionAutPutDTO
    {
        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "IdRequisicion")]
        public int IdRequisicion { get; set; }
        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "NumeroRequisicion")]
        public string NumeroRequisicion { get; set; }
        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "IdUsuarioAutorizacion")]
        public int IdUsuarioAutorizacion { get; set; }
        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "FechaAutorizacion")]
        public DateTime FechaAutorizacion { get; set; }
        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "IdRequisicionEstatus")]
        public byte IdRequisicionEstatus { get; set; }

        public string MotivoCancelacion { get; set; }
        public List<RequisicionProdAutPutDTO> ListaProductos { get; set; }

    }
}