using Exceptions.MainModule.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Catalogo
{
  public class ClienteCrearDto
    {
        const string eReq = Error.R0002;
        const string eTa = Error.R0004;

        public int IdCliente { get; set; }

        [Required(ErrorMessage = eReq)]
        [Display(Name = "IdEmpresa")]
        public short IdEmpresa { get; set; }

        [Required(ErrorMessage = eReq)]
        [Display(Name = "IdTipoPersona")]
        public Nullable<byte> IdTipoPersona { get; set; }

        [Required(ErrorMessage = eReq)]
        [Display(Name = "IdRegimenFiscal")]
        public Nullable<short> IdRegimenFiscal { get; set; }

        [Display(Name = "IdCuentaContable")]
        public Nullable<int> IdCuentaContable { get; set; }

        [StringLength(100, MinimumLength = 1, ErrorMessage = eTa)]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [StringLength(80, MinimumLength = 1, ErrorMessage = eTa)]
        [Display(Name = "Apellido1")]
        public string Apellido1 { get; set; }

        [StringLength(80, MinimumLength = 1, ErrorMessage = eTa)]
        [Display(Name = "Apellido2")]
        public string Apellido2 { get; set; }

        [Display(Name = "PorcentDescuento")]
        public decimal PorcentDescuento { get; set; }

        [Required(ErrorMessage = eReq)]
        [Display(Name = "limiteCreditoMonto")]
        public decimal limiteCreditoMonto { get; set; }

        [Required(ErrorMessage = eReq)]
        [Display(Name = "limiteCreditoDias")]
        public short limiteCreditoDias { get; set; }

        [StringLength(50, MinimumLength = 1, ErrorMessage = eTa)]
        [Display(Name = "Telefono1")]
        public string Telefono1 { get; set; }

        [StringLength(50, MinimumLength = 1, ErrorMessage = eTa)]
        [Display(Name = "Telefono2")]
        public string Telefono2 { get; set; }

        [StringLength(50, MinimumLength = 1, ErrorMessage = eTa)]
        [Display(Name = "Telefono3")]
        public string Telefono3 { get; set; }

        [StringLength(50, MinimumLength = 1, ErrorMessage = eTa)]
        [Display(Name = "Celular1")]
        public string Celular1 { get; set; }

        [StringLength(50, MinimumLength = 1, ErrorMessage = eTa)]
        [Display(Name = "Celular2")]
        public string Celular2 { get; set; }

        [StringLength(50, MinimumLength = 1, ErrorMessage = eTa)]
        [Display(Name = "Celular3")]
        public string Celular3 { get; set; }

        [StringLength(200, MinimumLength = 1, ErrorMessage = eTa)]
        [Display(Name = "Email1")]
        public string Email1 { get; set; }

        [StringLength(200, MinimumLength = 1, ErrorMessage = eTa)]
        [Display(Name = "Email2")]
        public string Email2 { get; set; }

        [StringLength(200, MinimumLength = 1, ErrorMessage = eTa)]
        [Display(Name = "Email3")]
        public string Email3 { get; set; }

        [StringLength(150, MinimumLength = 1, ErrorMessage = eTa)]
        [Display(Name = "SitioWeb1")]
        public string SitioWeb1 { get; set; }

        [StringLength(150, MinimumLength = 1, ErrorMessage = eTa)]
        [Display(Name = "SitioWeb2")]
        public string SitioWeb2 { get; set; }

        [StringLength(150, MinimumLength = 1, ErrorMessage = eTa)]
        [Display(Name = "SitioWeb3")]
        public string SitioWeb3 { get; set; }

        [StringLength(350, MinimumLength = 1, ErrorMessage = eTa)]
        [Display(Name = "Usuario")]
        public string Usuario { get; set; }

        [StringLength(250, MinimumLength = 1, ErrorMessage = eTa)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "AccesoPortal")]
        public bool AccesoPortal { get; set; }

        [StringLength(13, MinimumLength = 1, ErrorMessage = eTa)]
        [Display(Name = "Rfc")]
        public string Rfc { get; set; }

        [StringLength(350, MinimumLength = 1, ErrorMessage = eTa)]
        [Display(Name = "RazonSocial")]
        public string RazonSocial { get; set; }

        [StringLength(300, MinimumLength = 1, ErrorMessage = eTa)]
        [Display(Name = "RepresentanteLegal")]
        public string RepresentanteLegal { get; set; }

        [StringLength(50, MinimumLength = 1, ErrorMessage = eTa)]
        [Display(Name = "Telefono")]
        public string Telefono { get; set; }

        [StringLength(50, MinimumLength = 1, ErrorMessage = eTa)]
        [Display(Name = "Celular")]
        public string Celular { get; set; }

        [StringLength(350, MinimumLength = 1, ErrorMessage = eTa)]
        [Display(Name = "CorreoElectronico")]
        public string CorreoElectronico { get; set; }

        [StringLength(350, MinimumLength = 1, ErrorMessage = eTa)]
        [Display(Name = "Domicilio")]
        public string Domicilio { get; set; }
    }
}
