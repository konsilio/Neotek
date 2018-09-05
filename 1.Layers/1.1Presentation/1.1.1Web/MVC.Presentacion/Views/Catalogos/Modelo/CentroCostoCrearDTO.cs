using Exceptions.MainModule.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.MainModule.Catalogos.Model
{
    public class CentroCostoCrearDto
    {
        [Required(ErrorMessage = Error.R0002)]
        [Range(minimum: 1, maximum: short.MaxValue, ErrorMessage = Error.R0002)]
        [Display(Name = "Empresa")]
        public short IdEmpresa { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Range(minimum: 1, maximum: short.MaxValue, ErrorMessage = Error.R0002)]
        [Display(Name = "Tipo centro de costo")]
        public byte IdTipoCentroCosto { get; set; }  
        public int? IdEquipoTransporte { get; set; }        
        public int? IdVehiculoUtilitario { get; set; }       
        public short? IdCAlmacenGas { get; set; }
        public int? IdEstacionCarburacion { get; set; }        
        public int? IdCamioneta { get; set; }
        public int? IdPipa { get; set; }       
        public int? IdCilindro { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [StringLength(100, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Número")]
        public string Numero { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [StringLength(100, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Nombre o Descripción")]
        public string Descripcion { get; set; }
    }
}