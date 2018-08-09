using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Exceptions.MainModule.Validaciones;

namespace Web.MainModule.Requisicion.Model
{
    [Serializable]
    public class RequisicionDTO
    {
        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "IdRequisicion")]
        public int IdRequisicion { get; set; }
        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "IdUsuarioSolicitante")]
        public int IdUsuarioSolicitante { get; set; }
        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "MotivoRequisicion")]
        public short IdEmpresa { get; set; }
        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "MotivoRequisicion")]
        public string NumeroRequisicion { get; set; }
        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "MotivoRequisicion")]
        public string MotivoRequisicion { get; set; }
        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "MotivoRequisicion")]
        public string RequeridoEn { get; set; }
        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "MotivoRequisicion")]
        public byte IdRequisicionEstatus { get; set; }
        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "MotivoRequisicion")]
        public System.DateTime FechaRequerida { get; set; }
        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "MotivoRequisicion")]
        public System.DateTime FechaRegistro { get; set; }
        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "MotivoRequisicion")]
        public Nullable<int> IdUsuarioRevision { get; set; }
        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "MotivoRequisicion")]
        public string OpinionAlmacen { get; set; }
        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "MotivoRequisicion")]
        public Nullable<System.DateTime> FechaRevision { get; set; }
        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "MotivoRequisicion")]
        public string MotivoCancelacion { get; set; }
        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "MotivoRequisicion")]
        public Nullable<int> IdUsuarioAutorizacion { get; set; }
        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "MotivoRequisicion")]
        public Nullable<System.DateTime> FechaAutorizacion { get; set; }

    }
}