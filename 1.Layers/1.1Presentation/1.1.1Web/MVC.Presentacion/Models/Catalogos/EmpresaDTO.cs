using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Catalogos
{

    public class EmpresaDTO 
    {
        public short IdEmpresa { get; set; }
        public bool EsAdministracionCentral { get; set; }
        public byte IdAdministracionCentral { get; set; }
        public string NombreComercial { get; set; }
        public System.DateTime FechaRegistro { get; set; }
        public byte IdPais { get; set; }
        public Nullable<byte> IdEstadoRep { get; set; }
        public string EstadoProvincia { get; set; }
        public string Municipio { get; set; }
        public string CodigoPostal { get; set; }
        public string Colonia { get; set; }
        public string Calle { get; set; }
        public string NumExt { get; set; }
        public string NumInt { get; set; }
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
        public string Rfc { get; set; }
        public string RazonSocial { get; set; }
        public bool Activo { get; set; }
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
    }
}