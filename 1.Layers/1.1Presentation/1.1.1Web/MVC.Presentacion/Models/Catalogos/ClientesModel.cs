using Exceptions.MainModule.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Catalogos
{
    public class ClientesModel
    {  

        public int IdCliente { get; set; }
        public short Orden { get; set; }
              
        public short IdEmpresa { get; set; }
        
        public Nullable<byte> IdTipoPersona { get; set; }
        
        public Nullable<short> IdRegimenFiscal { get; set; }
        
        public Nullable<int> IdCuentaContable { get; set; }
        
        public string Nombre { get; set; }
        
        public string Apellido1 { get; set; }
        
        public string Apellido2 { get; set; }
        
        public decimal DescuentoXKilo { get; set; }
        
        public decimal limiteCreditoMonto { get; set; }
        
        public short limiteCreditoDias { get; set; }
        
        public string Telefono1 { get; set; }
        
        public string Telefono2 { get; set; }
        
        public string Telefono3 { get; set; }
        
        public string Celular1 { get; set; }
        
        public string Celular2 { get; set; }
        
        public string Celular3 { get; set; }
        
        public string Email1 { get; set; }
        
        public string Email2 { get; set; }
        
        public string Email3 { get; set; }
        
        public string SitioWeb1 { get; set; }
        
        public string SitioWeb2 { get; set; }
        
        public string SitioWeb3 { get; set; }
        
        public string Usuario { get; set; }
        
        public string Password { get; set; }
        
        public bool AccesoPortal { get; set; }
        
        public string Rfc { get; set; }
        
        public string RazonSocial { get; set; }
        
        public string RepresentanteLegal { get; set; }
        
        public string Telefono { get; set; }
        
        public string Celular { get; set; }
        
        public string CorreoElectronico { get; set; }
        
        public string Domicilio { get; set; }

        public string Cliente { get; set; }
        public List<ClienteLocacionMod> Locaciones { get; set; }
        public bool VentaExtraordinaria { get; set; }
    }
}