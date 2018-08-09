using Exceptions.MainModule.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Requisicion
{
    public class RequisicionCrearDTO
    {
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
        [Display(Name = "FechaRegistro")]
        public System.DateTime FechaRegistro { get; set; }

        [EnsureMinimumElements(1, ErrorMessage = Error.R0006)]
        [Display(Name = "ListaProductos")]
        public List<RequisicionProductoCrearDTO> ListaProductos { get; set; }
    }
}
