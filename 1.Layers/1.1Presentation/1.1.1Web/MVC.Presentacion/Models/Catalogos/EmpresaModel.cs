using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Catalogos
{
        [Serializable]
    public class EmpresaModel //: Empresa
    {

            const string eReq = "Requerido";//R0002
            const string eTa = "Tamaño incorrecto";//R0004

            [Required(ErrorMessage = eReq)]
            [StringLength(250, MinimumLength = 1, ErrorMessage = eTa)]
            [Display(Name = "Nombre Comercial")]
            public string NombreComercial { get; set; }

            [Required(ErrorMessage = eReq)]
            [StringLength(250, MinimumLength = 1, ErrorMessage = eTa)]
            [Display(Name = "Razon Social")]
            public string RazonSocial { get; set; }

            [Required(ErrorMessage = eReq)]
            [StringLength(250, MinimumLength = 1, ErrorMessage = eTa)]
            [Display(Name = "Rfc")]
            public string Rfc { get; set; }

            [Display(Name = "Persona de contacto 1")]
            [StringLength(250, MinimumLength = 1, ErrorMessage = eTa)]
            public string Persona1 { get; set; }

            [Display(Name = "Persona de contacto 2")]
            [StringLength(250, MinimumLength = 1, ErrorMessage = eTa)]
            public string Persona2 { get; set; }

            [Display(Name = "Persona de contacto 3")]
            [StringLength(250, MinimumLength = 1, ErrorMessage = eTa)]
            public string Persona3 { get; set; }

            [Display(Name = "Teléfono 1")]
            [StringLength(250, MinimumLength = 1, ErrorMessage = eTa)]
            public string Telefono1 { get; set; }

            [Display(Name = "Teléfono 2")]
            [StringLength(250, MinimumLength = 1, ErrorMessage = eTa)]
            public string Telefono2 { get; set; }

            [Display(Name = "Teléfono 3")]
            [StringLength(250, MinimumLength = 1, ErrorMessage = eTa)]
            public string Telefono3 { get; set; }

            [Display(Name = "Celular 1")]
            [StringLength(250, MinimumLength = 1, ErrorMessage = eTa)]
            public string Celular1 { get; set; }

            [Display(Name = "Celular 2")]
            [StringLength(250, MinimumLength = 1, ErrorMessage = eTa)]
            public string Celular2 { get; set; }

            [Display(Name = "Celular 3")]
            [StringLength(250, MinimumLength = 1, ErrorMessage = eTa)]
            public string Celular3 { get; set; }

            [Display(Name = "Correo elect. 1")]
            [StringLength(250, MinimumLength = 1, ErrorMessage = eTa)]
            public string Email1 { get; set; }

            [Display(Name = "Correo elect. 2")]
            [StringLength(250, MinimumLength = 1, ErrorMessage = eTa)]
            public string Email2 { get; set; }

            [Display(Name = "Correo elect. 3")]
            [StringLength(250, MinimumLength = 1, ErrorMessage = eTa)]
            public string Email3 { get; set; }

            [Display(Name = "Sitio Web 1")]
            [StringLength(250, MinimumLength = 1, ErrorMessage = eTa)]
            public string SitioWeb1 { get; set; }

            [Display(Name = "Sitio Web 2")]
            [StringLength(250, MinimumLength = 1, ErrorMessage = eTa)]
            public string SitioWeb2 { get; set; }

            [Display(Name = "Sitio Web 3")]
            [StringLength(250, MinimumLength = 1, ErrorMessage = eTa)]
            public string SitioWeb3 { get; set; }

            [Required(ErrorMessage = eReq)]
            [Display(Name = "IdPais")]
            public Nullable<byte> IdPais { get; set; }

            [Required(ErrorMessage = eReq)]
            [Display(Name = "IdEstadoRep")]
            public Nullable<byte> IdEstadoRep { get; set; }

           // [Required(ErrorMessage = eReq)]
            [StringLength(250, MinimumLength = 1, ErrorMessage = eTa)]
            [Display(Name = "Estado")]
            public string EstadoProvincia { get; set; }

            [Required(ErrorMessage = eReq)]
            [StringLength(250, MinimumLength = 1, ErrorMessage = eTa)]
            [Display(Name = "Municipio")]
            public string Municipio { get; set; }

            [Required(ErrorMessage = eReq)]
            [StringLength(250, MinimumLength = 1, ErrorMessage = eTa)]
            [Display(Name = "C.P.")]
            public string CodigoPostal { get; set; }

            [Required(ErrorMessage = eReq)]
            [StringLength(250, MinimumLength = 1, ErrorMessage = eTa)]
            [Display(Name = "Colonia")]
            public string Colonia { get; set; }

            [Required(ErrorMessage = eReq)]
            [StringLength(250, MinimumLength = 1, ErrorMessage = eTa)]
            [Display(Name = "Calle")]
            public string Calle { get; set; }

            [Required(ErrorMessage = eReq)]
            [StringLength(250, MinimumLength = 1, ErrorMessage = eTa)]
            [Display(Name = "Núm Ext.")]
            public string NumExt { get; set; }

            [StringLength(250, MinimumLength = 1, ErrorMessage = eTa)]
            [Display(Name = "Núm Int.")]
            public string NumInt { get; set; }

            [StringLength(250, MinimumLength = 1, ErrorMessage = eTa)]
            [Display(Name = "Pequeño (180px X 180px")]
            public string UrlLogotipo180px { get; set; }

            [StringLength(250, MinimumLength = 1, ErrorMessage = eTa)]
            [Display(Name = "Mediano (500px X 500px)")]
            public string UrlLogotipo500px { get; set; }

            [StringLength(250, MinimumLength = 1, ErrorMessage = eTa)]
            [Display(Name = "Grande (1000px X 1000px")]
            public string UrlLogotipo1000px { get; set; } 
    
    }
}