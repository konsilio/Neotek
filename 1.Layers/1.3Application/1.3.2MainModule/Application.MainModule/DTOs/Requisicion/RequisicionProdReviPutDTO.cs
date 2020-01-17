using Exceptions.MainModule.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Requisicion
{
    [Serializable]
    public class RequisicionProdReviPutDTO
    {
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "IdProducto")]
        public int IdProducto { get; set; }

        public short Orden { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "RevisionFisica")]
        public bool RevisionFisica { get; set; }
    }
}
