using Exceptions.MainModule.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.MainModule.Requisicion.Model
{
    public class RequisicionRevisionDTO
    {
        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "IdRequisicion")]
        public int IdRequisicion { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "NumeroRequisicion")]
        public string NumeroRequisicion { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "IdUsuarioSolicitante")]
        public int IdUsuarioSolicitante { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "IdEmpresa")]
        public short IdEmpresa { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [StringLength(500, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "MotivoRequisicion")]
        public string MotivoRequisicion { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [StringLength(500, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "RequeridoEn")]
        public string RequeridoEn { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "IdRequisicionEstatus")]
        public byte IdRequisicionEstatus { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "FechaRequerida")]
        public System.DateTime FechaRequerida { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "OpinionAlmacen")]
        public string OpinionAlmacen { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "MotivoCancelacion")]
        public string MotivoCancelacion { get; set; }
        public List<RequisicionProductoRevisionDTO> ListaProductos { get; set; }
    }
}