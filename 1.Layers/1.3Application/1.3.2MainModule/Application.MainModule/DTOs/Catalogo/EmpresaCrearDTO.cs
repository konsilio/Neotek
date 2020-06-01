using Exceptions.MainModule.Validaciones;
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.MainModule.DTOs.Catalogo
{
    [Serializable]
    public class EmpresaCrearDTO
    {
        [Required(ErrorMessage = Error.R0002)]
        [StringLength(100, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Nombre Comercial")]
        public string NombreComercial { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [StringLength(350, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Razon Social")]
        public string RazonSocial { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [StringLength(13, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "R.F.C")]
        public string Rfc { get; set; }
        [Display(Name = "Persona de contacto 1")]
        [StringLength(250, MinimumLength = 1, ErrorMessage = Error.R0004)]
        public string Persona1 { get; set; }
        [Display(Name = "Persona de contacto 2")]
        [StringLength(250, MinimumLength = 1, ErrorMessage = Error.R0004)]
        public string Persona2 { get; set; }
        [Display(Name = "Persona de contacto 3")]
        [StringLength(250, MinimumLength = 1, ErrorMessage = Error.R0004)]
        public string Persona3 { get; set; }
        [Display(Name = "Teléfono 1")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = Error.R0004)]
        public string Telefono1 { get; set; }
        [Display(Name = "Teléfono 2")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = Error.R0004)]
        public string Telefono2 { get; set; }
        [Display(Name = "Teléfono 3")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = Error.R0004)]
        public string Telefono3 { get; set; }
        [Display(Name = "Celular 1")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = Error.R0004)]
        public string Celular1 { get; set; }
        [Display(Name = "Celular 2")]
        [StringLength(250, MinimumLength = 1, ErrorMessage = Error.R0004)]
        public string Celular2 { get; set; }
        [Display(Name = "Celular 3")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = Error.R0004)]
        public string Celular3 { get; set; }
        [Display(Name = "Correo elect. 1")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = Error.R0004)]
        public string Email1 { get; set; }
        [Display(Name = "Correo elect. 2")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = Error.R0004)]
        public string Email2 { get; set; }
        [Display(Name = "Correo elect. 3")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = Error.R0004)]
        public string Email3 { get; set; }
        [Display(Name = "Sitio Web 1")]
        [StringLength(250, MinimumLength = 1, ErrorMessage = Error.R0004)]
        public string SitioWeb1 { get; set; }
        [Display(Name = "Sitio Web 2")]
        [StringLength(250, MinimumLength = 1, ErrorMessage = Error.R0004)]
        public string SitioWeb2 { get; set; }
        [Display(Name = "Sitio Web 3")]
        [StringLength(250, MinimumLength = 1, ErrorMessage = Error.R0004)]
        public string SitioWeb3 { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "IdPais")]
        public byte IdPais { get; set; }
        [Display(Name = "IdEstadoRep")]
        public Nullable<byte> IdEstadoRep { get; set; }
        [StringLength(150, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Estado")]
        public string EstadoProvincia { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [StringLength(100, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Municipio")]
        public string Municipio { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [StringLength(20, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "C.P.")]
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
        [Display(Name = "Núm Ext.")]
        public string NumExt { get; set; }
        [StringLength(10, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Núm Int.")]
        public string NumInt { get; set; }
        [Display(Name = "Pequeño (180px X 180px")]
        public string UrlLogotipo180px { get; set; }
        [Display(Name = "Mediano (500px X 500px)")]
        public string UrlLogotipo500px { get; set; }
        [Display(Name = "Grande (1000px X 1000px")]
        public string UrlLogotipo1000px { get; set; }
        public System.DateTime CierreInventario { get; set; }
        public decimal FactorCompraLitroAKilos { get; set; }
        public decimal FactorFleteGas { get; set; }
        public decimal FactorGalonALitros { get; set; }
        public decimal FactorLitrosAKilos { get; set; }
        public byte InventarioCrítico { get; set; }
        public byte InventarioSano { get; set; }
        public decimal MaxRemaGaseraMensual { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [MaxLength(150, ErrorMessage = Error.R0015)]
        [Display(Name = "Leyenda de ticket")]
        public string Leyenda { get; set; }
    }
}
