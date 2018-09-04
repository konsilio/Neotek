using Exceptions.MainModule.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.MainModule.Catalogos.Model
{
    public class EmpDTO
    {
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "IdEmpresa")]
        public int IdCuentaContable { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "NombreComercial")]
        public string NombreComercial { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "IdPais")]
        public byte IdPais { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "IdEstadoRep")]
        public byte IdEstadoRep { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "EstadoProvincia")]
        public string EstadoProvincia { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Rfc")]
        public string Rfc { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "RazonSocial")]
        public string RazonSocial { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Persona1")]
        public string Persona1 { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Celular1")]
        public string Celular1 { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Telefono1")]
        public string Telefono1 { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Email1")]
        public string Email1 { get; set; }


        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Activo")]
        public bool Activo { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "FechaRegistro")]
        public DateTime FechaRegistro { get; set; }
    }
}