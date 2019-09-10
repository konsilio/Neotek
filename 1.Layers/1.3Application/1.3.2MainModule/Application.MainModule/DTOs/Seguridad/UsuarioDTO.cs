using Exceptions.MainModule.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Seguridad
{
   public class UsuarioDTO
    {
        public int IdUsuario { get; set; }
        public short IdEmpresa { get; set; }
        public short IdRol { get; set; }
        public bool EsAdministracionCentral { get; set; }
        public bool EsSuperAdmin { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [StringLength(100, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [StringLength(80, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Apellido1")]
        public string Apellido1 { get; set; }
         
        [StringLength(80, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Apellido2")]
        public string Apellido2 { get; set; }
        public string NombreCompleto { get; set; }
        public bool Activo { get; set; }
        public System.DateTime FechaRegistro { get; set; }

        [StringLength(350, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Usuario")]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [StringLength(250, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [StringLength(50, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Teléfono 1")]
        public string Telefono1 { get; set; }

        [StringLength(50, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Teléfono 2")]
        public string Telefono2 { get; set; }

        [StringLength(50, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Teléfono 3")]
        public string Telefono3 { get; set; }

        [StringLength(50, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Celular 1")]
        public string Celular1 { get; set; }

        [StringLength(50, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Celular 2")]
        public string Celular2 { get; set; }

        [StringLength(50, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Celular 3")]
        public string Celular3 { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [StringLength(200, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Correo elect. 1")]
        public string Email1 { get; set; }

        [StringLength(200, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Correo elect. 2")]
        public string Email2 { get; set; }

        [StringLength(200, MinimumLength = 1, ErrorMessage = Error.R0004)]
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
        public byte IdPais { get; set; }
        public Nullable<byte> IdEstadoRep { get; set; }
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
    }


    public class UsuarioEditDTO
    {
        public int IdUsuario { get; set; }
        public short IdEmpresa { get; set; }
        public short IdRol { get; set; }
        public bool EsAdministracionCentral { get; set; }
        public bool EsSuperAdmin { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [StringLength(100, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [StringLength(80, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Apellido1")]
        public string Apellido1 { get; set; }

        [StringLength(80, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Apellido2")]
        public string Apellido2 { get; set; }
        public string NombreCompleto { get; set; }
        public bool Activo { get; set; }
        public System.DateTime FechaRegistro { get; set; }

        //[StringLength(350, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Usuario")]
        public string NombreUsuario { get; set; }

        //[Required(ErrorMessage = Error.R0002)]
        //[StringLength(250, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [StringLength(50, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Teléfono 1")]
        public string Telefono1 { get; set; }

        [StringLength(50, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Teléfono 2")]
        public string Telefono2 { get; set; }

        [StringLength(50, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Teléfono 3")]
        public string Telefono3 { get; set; }

        [StringLength(50, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Celular 1")]
        public string Celular1 { get; set; }

        [StringLength(50, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Celular 2")]
        public string Celular2 { get; set; }

        [StringLength(50, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Celular 3")]
        public string Celular3 { get; set; }

        //[Required(ErrorMessage = Error.R0002)]
        //[StringLength(200, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Correo elect. 1")]
        public string Email1 { get; set; }

        [StringLength(200, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Correo elect. 2")]
        public string Email2 { get; set; }

        [StringLength(200, MinimumLength = 1, ErrorMessage = Error.R0004)]
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
        public byte IdPais { get; set; }
        public Nullable<byte> IdEstadoRep { get; set; }
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
    }
}
