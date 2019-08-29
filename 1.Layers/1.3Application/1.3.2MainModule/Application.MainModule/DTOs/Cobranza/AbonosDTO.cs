using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Exceptions.MainModule.Validaciones;

namespace Application.MainModule.DTOs.Cobranza
{

    public class AbonosDTO
    {
        public int IdAbono { get; set; }
        public int IdCargo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaAbono { get; set; }
        public decimal MontoAbono { get; set; }
        public byte IdFormaPago { get; set; }
        public string FolioBancario { get; set; }
        public string FormaPago { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "que royo")]

        public int Id_RelTF { get; set; }
        public string URLPdf { get; set; }
        public string URLXml { get; set; }
    }
}
