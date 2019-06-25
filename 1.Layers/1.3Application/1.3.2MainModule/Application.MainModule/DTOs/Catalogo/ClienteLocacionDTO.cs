using Exceptions.MainModule.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Catalogo
{

    public class ClienteLocacionDTO
    {
        public int IdCliente { get; set; }
        public short Orden { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "IdPais")]
        
        public byte IdPais { get; set; }

        //[StringLength(150, MinimumLength = 1, ErrorMessage = Error.R0002)]
        // [Display(Name = "IdPais")]
        public byte IdEstadoRep { get; set; }

        [StringLength(150, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Estado Provincia")]
        public string EstadoProvincia { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [StringLength(100, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Municipio")]
        public string Municipio { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [StringLength(20, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Codigo Postal")]
        public string CodigoPostal { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [StringLength(100, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Colonia")]
        public string Colonia { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [StringLength(250, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Calle")]
        public string Calle { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [StringLength(10, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Num Ext")]
        public string NumExt { get; set; }

        [StringLength(10, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Num Int")]
        public string NumInt { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Referencia")]
        public string formatted_address { get; set; }
        public string location_lat { get; set; }
        public string location_lng { get; set; }
        public string place_id { get; set; }
        [Required(ErrorMessage = Error.R0002)]      
        [Display(Name = "Tipo Locacion")]
        public string TipoLocacion { get; set; }
        public string Pais { get; set; }
        public string Estado { get; set; }
    }
}
