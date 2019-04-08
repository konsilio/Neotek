using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Exceptions.MainModule.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace Application.MainModule.DTOs
{
    public class HistoricoVentaDTO
    {
        [Key]
        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "IdHistorico")]
        public int Id { get; set; }

        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "Mes")]
        public int Mes { get; set; }

        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "Año")]
        public int Anio { get; set; }

        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "MontoVenta")]
        public decimal MontoVenta { get; set; }

        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "EsPipa")]
        public bool EsPipa { get; set; }

        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "EsCamioneta")]
        public bool EsCamioneta { get; set; }

        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "EsLocal")]
        public bool EsLocal { get; set; }
     
        [Display(Name = "Fecha Registro")]
        public Nullable<DateTime> FechaRegistro { get; set; }
    }
}
