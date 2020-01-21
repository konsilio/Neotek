using Exceptions.MainModule.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Catalogo
{
    [Serializable]
    public class EmpresaDTO
    {
        public short IdEmpresa { get; set; }
        public bool EsAdministracionCentral { get; set; }
        public byte IdAdministracionCentral { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [StringLength(100, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Nombre Comercial")]
        public string NombreComercial { get; set; }

        public System.DateTime FechaRegistro { get; set; }
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

        [StringLength(150, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Persona de contacto 1")]
        public string Persona1 { get; set; }
        
        [StringLength(150, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Persona de contacto 2")]
        public string Persona2 { get; set; }
        
        [StringLength(150, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Persona de contacto 3")]
        public string Persona3 { get; set; }
        
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
        [StringLength(13, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "R.F.C")]
        public string Rfc { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [StringLength(350, MinimumLength = 1, ErrorMessage =  Error.R0004)]
        [Display(Name = "Razon Social")]
        public string RazonSocial { get; set; }

        public bool Activo { get; set; }
        //public System.DateTime FechaRegistro { get; set; }
        public decimal FactorLitrosAKilos { get; set; }
        public System.DateTime CierreInventario { get; set; }
        public byte InventarioSano { get; set; }
        public byte InventarioCrítico { get; set; }
        public decimal MaxRemaGaseraMensual { get; set; }
        public decimal FactorGalonALitros { get; set; }
        public decimal FactorCompraLitroAKilos { get; set; }
        public decimal FactorFleteGas { get; set; }
        public string UrlLogotipoMenu { get; set; }
        public string UrlLogotipoLogin { get; set; }
        public string UrlLogotipo180px { get; set; }
        public string UrlLogotipo500px { get; set; }
        public string UrlLogotipo1000px { get; set; }
        public CoordenadasDTO Coordenadas { get; set; }


    }
}
