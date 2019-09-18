using Exceptions.MainModule.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs
{
   public class EgresoDTO
    {
        public int IdEgreso { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "IdEmpresa")]
        public short IdEmpresa { get; set; }
        public System.DateTime FechaRegistro { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = Error.R0002)]
        [Display(Name = "Centro de Costo")]
        public int IdCentroCosto { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Cuenta Contableo")]
        [Range(minimum: 2, maximum: int.MaxValue, ErrorMessage = Error.R0002)]
        public int IdCuentaContable { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = Error.R0003)]
        [Display(Name = "Monto")]
        public decimal Monto { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Descripcion")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Es Externo")]
        public bool EsExterno { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Activo")]
        public bool Activo { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "GastoMensual")]
        public bool GastoMensual { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "EsFiscal")]
        public bool EsFiscal { get; set; }
    }
}
