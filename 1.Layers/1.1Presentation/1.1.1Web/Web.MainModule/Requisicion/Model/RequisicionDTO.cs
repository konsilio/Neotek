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
        [Display(Name = "UsuarioSolicitante")]
        public string UsuarioSolicitante { get; set; }
        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "IdEmpresa")]
        public short IdEmpresa { get; set; }
        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "NombreComercial")]
        public string NombreComercial { get; set; }
        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "NumeroRequisicion")]
        public string NumeroRequisicion { get; set; }
        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "MotivoRequisicion")]
        public string MotivoRequisicion { get; set; }
        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "RequeridoEn")]
        public string RequeridoEn { get; set; }
        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "IdRequisicionEstatus")]
        public byte IdRequisicionEstatus { get; set; }
        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "RequisicionEstatus")]
        public string RequisicionEstatus { get; set; }
        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "FechaRequerida")]
        public DateTime FechaRequerida { get; set; }
        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "FechaRegistro")]
        public DateTime FechaRegistro { get; set; }
        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "IdUsuarioRevision")]
        public int IdUsuarioRevision { get; set; }
        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "OpinionAlmacen")]
        public string OpinionAlmacen { get; set; }
        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "FechaRevision")]
        public DateTime FechaRevision { get; set; }
        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "MotivoCancelacion")]
        public string MotivoCancelacion { get; set; }
        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "IdUsuarioAutorizacion")]
        public int IdUsuarioAutorizacion { get; set; }
        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "FechaAutorizacion")]
        public DateTime FechaAutorizacion { get; set; }
    }
}