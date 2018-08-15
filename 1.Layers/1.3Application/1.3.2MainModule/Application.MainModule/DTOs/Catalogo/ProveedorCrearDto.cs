using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exceptions.MainModule.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace Application.MainModule.DTOs.Catalogo
{
    public class ProveedorCrearDto
    {
        [Required(ErrorMessage = Error.R0002)]
        [Range(minimum: 1, maximum: short.MaxValue, ErrorMessage = Error.R0002)]
        [Display(Name = "IdEmpresa")]
        public short IdEmpresa { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Range(minimum: 1, maximum: byte.MaxValue, ErrorMessage = Error.R0002)]
        [Display(Name = "IdTipoProveedor")]
        public byte IdTipoProveedor { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = Error.R0002)]
        [Display(Name = "IdCuentaContable")]
        public Nullable<int> IdCuentaContable { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Range(minimum: 1, maximum: byte.MaxValue, ErrorMessage = Error.R0002)]
        [Display(Name = "IdFormaDePago")]
        public byte IdFormaDePago { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Range(minimum: 1, maximum: short.MaxValue, ErrorMessage = Error.R0002)]
        [Display(Name = "IdBanco")]
        public short IdBanco { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Range(minimum: 1, maximum: byte.MaxValue, ErrorMessage = Error.R0002)]
        [Display(Name = "IdPais")]
        public byte IdPais { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Range(minimum: 1, maximum: byte.MaxValue, ErrorMessage = Error.R0002)]
        [Display(Name = "IdEstadoRep")]
        public Nullable<byte> IdEstadoRep { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Range(minimum: 1, maximum: byte.MaxValue, ErrorMessage = Error.R0002)]
        [Display(Name = "IdTipoPersona")]
        public byte IdTipoPersona { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Range(minimum: 1, maximum: short.MaxValue, ErrorMessage = Error.R0002)]
        [Display(Name = "IdRegimenFiscal")]
        public short IdRegimenFiscal { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [StringLength(100, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "NombreComercial")]
        public string NombreComercial { get; set; }
        public bool ProdutoPrinicpal { get; set; }
        public bool TransportistaProdutoPrinicpal { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [StringLength(250, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Vende")]
        public string Vende { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [StringLength(150, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Cuenta")]
        public string Cuenta { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "DiasCredito")]
        public decimal DiasCredito { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [StringLength(150, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Persona1")]
        public string Persona1 { get; set; }

        [StringLength(150, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Persona2")]
        public string Persona2 { get; set; }

        [StringLength(150, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Persona3")]
        public string Persona3 { get; set; }

        [StringLength(50, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Telefono1")]
        public string Telefono1 { get; set; }

        [StringLength(50, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Telefono2")]
        public string Telefono2 { get; set; }

        [StringLength(50, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Telefono2")]
        public string Telefono3 { get; set; }

        [StringLength(50, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Celular1")]
        public string Celular1 { get; set; }

        [StringLength(50, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Celular1")]
        public string Celular2 { get; set; }

        [StringLength(50, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Celular1")]
        public string Celular3 { get; set; }

        [EmailAddress(ErrorMessage = Error.R0004)]
        [StringLength(200, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Email1")]
        public string Email1 { get; set; }

        [EmailAddress(ErrorMessage = Error.R0004)]
        [StringLength(200, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Email1")]
        public string Email2 { get; set; }

        [EmailAddress(ErrorMessage = Error.R0004)]
        [StringLength(200, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Email1")]
        public string Email3 { get; set; }

        [StringLength(150, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "SitioWeb1")]
        public string SitioWeb1 { get; set; }

        [StringLength(150, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "SitioWeb2")]
        public string SitioWeb2 { get; set; }

        [StringLength(150, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "SitioWeb3")]
        public string SitioWeb3 { get; set; }

        [StringLength(150, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "EstadoProvincia")]
        public string EstadoProvincia { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [StringLength(100, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Municipio")]
        public string Municipio { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [StringLength(6, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "CodigoPostal")]
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
        [Display(Name = "NumExt")]
        public string NumExt { get; set; }

        [StringLength(10, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "NumExt")]
        public string NumInt { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [StringLength(13, MinimumLength = 12, ErrorMessage = Error.C0001)]
        [RegularExpression(ExpresionRegular.Rfc, ErrorMessage = Error.C0001)]
        [Display(Name = "RFC")]
        public string Rfc { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [StringLength(350, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "RazonSocial")]
        public string RazonSocial { get; set; }
    }
}
