using Exceptions.MainModule.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Requisicion
{
    public class RequisicionRevPutDTO
    {

        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "IdRequisicion")]
        public int IdRequisicion { get; set; }
        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "NumeroRequisicion")]
        public string NumeroRequisicion { get; set; }

        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "IdUsuarioRevision")]
        public Nullable<int> IdUsuarioRevision { get; set; }

        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "OpinionAlmacen")]
        public string OpinionAlmacen { get; set; }

        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "FechaRevision")]
        public Nullable<System.DateTime> FechaRevision { get; set; }

        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "IdRequisicionEstatus")]
        public byte IdRequisicionEstatus { get; set; }

        public List<RequisicionProdReviPutDTO> ListaProductos { get; set; }
    }
}
