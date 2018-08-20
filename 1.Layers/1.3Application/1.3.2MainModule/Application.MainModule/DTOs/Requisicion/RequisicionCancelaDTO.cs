using Exceptions.MainModule.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Requisicion
{
    public class RequisicionCancelaDTO
    {
        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "IdRequisicion")]
        public int IdRequisicion { get; set; }

        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "NumeroRequisicion")]
        public string NumeroRequisicion { get; set; }

        public int IdUsuarioRevision { get; set; }

        public int IdUsuarioAutorizacion { get; set; }

        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "FechaAutorizacion")]
        public DateTime FechaAutorizacion { get; set; }

        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "IdRequisicionEstatus")]
        public byte IdRequisicionEstatus { get; set; }


        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "MotivoCancelacion")]
        public string MotivoCancelacion { get; set; }
    }
}
