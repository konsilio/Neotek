using Exceptions.MainModule.Validaciones;
using Sagas.MainModule.Entidades;
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
        public int IdCliente { get; set; }
        public short Orden { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "IdEmpresa")]
        public short IdEmpresa { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Tipo Persona")]
        public Nullable<byte> IdTipoPersona { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Regimen Fiscal")]
        public Nullable<short> IdRegimenFiscal { get; set; }

        [Display(Name = "IdCuentaContable")]
        public Nullable<int> IdCuentaContable { get; set; }

        //[Required(ErrorMessage = Error.R0002)]
        [StringLength(100, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        //[Required(ErrorMessage = Error.R0002)]
        [StringLength(80, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Apellido1")]
        public string Apellido1 { get; set; }

        [StringLength(80, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Apellido2")]
        public string Apellido2 { get; set; }

        [Display(Name = "DescuentoXKilo")]
        public decimal DescuentoXKilo { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "limiteCreditoMonto")]
        public decimal limiteCreditoMonto { get; set; }

        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "limiteCreditoDias")]
        public short limiteCreditoDias { get; set; }
        public decimal CreditoDisponibleMonto { get; set; }
 
        [StringLength(12, MinimumLength = 7, ErrorMessage = Error.R0004)]
        [Display(Name = "Teléfono 1")]
        public string Telefono1 { get; set; }

        //[StringLength(50, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Telefono2")]
        public string Telefono2 { get; set; }

        //[StringLength(50, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Telefono3")]
        public string Telefono3 { get; set; }

        [StringLength(50, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Celular1")]
        public string Celular1 { get; set; }

        [StringLength(50, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Celular2")]
        public string Celular2 { get; set; }

        [StringLength(50, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Celular3")]
        public string Celular3 { get; set; }

        [StringLength(200, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Email1")]
        public string Email1 { get; set; }

        [StringLength(200, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Email2")]
        public string Email2 { get; set; }

        [StringLength(200, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Email3")]
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

        [StringLength(350, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Usuario")]
        public string Usuario { get; set; }

        [StringLength(250, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "AccesoPortal")]
        public bool AccesoPortal { get; set; }
    
        [StringLength(13, MinimumLength = 13, ErrorMessage = Error.R0004)]
        [Display(Name = "Rfc")]
        public string Rfc { get; set; }

        [StringLength(350, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "RazonSocial")]
        public string RazonSocial { get; set; }

        [StringLength(300, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "RepresentanteLegal")]
        public string RepresentanteLegal { get; set; }

        [StringLength(50, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Telefono")]
        public string Telefono { get; set; }

        [StringLength(50, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Celular")]
        public string Celular { get; set; }

        [StringLength(350, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "CorreoElectronico")]
        public string CorreoElectronico { get; set; }

        [StringLength(350, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Domicilio")]
        public string Domicilio { get; set; }
        public string Cliente { get; set; }
        public List<ClienteLocacionDTO> Locaciones { get; set; }
        public ClienteLocacionDTO Locacion { get; set; }
        public bool VentaExtraordinaria { get; set; }
        public bool EsFijo { get; set; }
    }
}
