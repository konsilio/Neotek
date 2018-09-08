using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Catalogos
{
  
    public class EmpresaModel : Empresa
    {
        public string NombreComercial { get; set; }
        public string RazonSocial { get; set; }
        public string Rfc { get; set; }
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
        public int IdPais { get; set; }
        public int IdEstadoRep { get; set; }
        public string EstadoProvincia { get; set; }
        public string Municipio { get; set; }
        public string CodigoPostal { get; set; }
        public string Colonia { get; set; }
        public string Calle { get; set; }
        public string NumExt { get; set; }
        public string NumInt { get; set; }
        public List<EmpresaDTO> Empresas { get; set; }
    }
}