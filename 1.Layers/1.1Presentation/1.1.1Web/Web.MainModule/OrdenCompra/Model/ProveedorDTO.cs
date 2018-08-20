using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.MainModule.OrdenCompra.Model
{
    public class ProveedorDTO
    {
        public int IdProveedor { get; set; }
        public short IdEmpresa { get; set; }
        public byte IdTipoProveedor { get; set; }
        public Nullable<int> IdCuentaContable { get; set; }
        public byte IdFormaDePago { get; set; }
        public short IdBanco { get; set; }
        public byte IdPais { get; set; }
        public Nullable<byte> IdEstadoRep { get; set; }
        public byte IdTipoPersona { get; set; }
        public short IdRegimenFiscal { get; set; }
        public string NombreComercial { get; set; }
        public bool ProdutoPrinicpal { get; set; }
        public bool TransportistaProdutoPrinicpal { get; set; }
        public string Vende { get; set; }
        public bool Activo { get; set; }
        public System.DateTime FechaRegistro { get; set; }
        public string Cuenta { get; set; }
        public decimal DiasCredito { get; set; }
        public string Persona1 { get; set; }
        public string Persona2 { get; set; }
        public string Persona3 { get; set; }
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
        public string EstadoProvincia { get; set; }
        public string Municipio { get; set; }
        public string CodigoPostal { get; set; }
        public string Colonia { get; set; }
        public string Calle { get; set; }
        public string NumExt { get; set; }
        public string NumInt { get; set; }
        public string Rfc { get; set; }
        public string RazonSocial { get; set; }
    }    
}