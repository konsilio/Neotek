using Exceptions.MainModule.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Catalogo
{
    public class UsuarioCrearDto
    {
        [Required(ErrorMessage = Error.R0002)]
        [StringLength(250, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Nombres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [StringLength(250, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Apellido 1")]
        public string Apellido1 { get; set; }
             
        [StringLength(250, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Apellido 2")]
        public string Apellido2 { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [StringLength(250, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Correo elect")]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [StringLength(250, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }
            
        [StringLength(250, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Teléfono 1")]
        public string Telefono1 { get; set; }
        
        [StringLength(250, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Teléfono 2")]
        public string Telefono2 { get; set; }
        
        [StringLength(250, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Teléfono 3")]
        public string Telefono3 { get; set; }
        
        [StringLength(250, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Celular 1")]
        public string Celular1 { get; set; }
        
        [StringLength(250, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Celular 2")]
        public string Celular2 { get; set; }

        [StringLength(250, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Celular 3")]
        public string Celular3 { get; set; }
                        
        [StringLength(250, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Correo elect. 2")]
        public string Email2 { get; set; }


        [StringLength(250, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Correo elect. 3")]
        public string Email3 { get; set; }


        [StringLength(250, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Sitio Web 1")]
        public string SitioWeb1 { get; set; }


        [StringLength(250, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Sitio Web 2")]
        public string SitioWeb2 { get; set; }


        [StringLength(250, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Sitio Web 3")]
        public string SitioWeb3 { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [StringLength(250, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Pais")]
        public byte IdPais { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Range(minimum: 1, maximum: byte.MaxValue, ErrorMessage = Error.R0002)]
        [Display(Name = "IdEstadoRep")]
        public Nullable<byte> IdEstadoRep { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [StringLength(250, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Estado")]
        public string EstadoProvincia { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [StringLength(250, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Municipio")]
        public string Municipio { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [StringLength(250, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "C.P.")]
        public string CodigoPostal { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [StringLength(250, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Colonia")]
        public string Colonia { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [StringLength(250, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Calle")]
        public string Calle { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [StringLength(250, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Núm Ext.")]
        public string NumExt { get; set; }

        [StringLength(250, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Núm Int.")]
        public string NumInt { get; set; }
               
    }
}
