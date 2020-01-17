//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sagas.MainModule.Entidades
{
    using System;
    using System.Collections.Generic;
    
    public partial class Proveedor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Proveedor()
        {
            this.OrdenesCompra = new HashSet<OrdenCompra>();
            this.EntregasGas = new HashSet<AlmacenGasDescarga>();
            this.TransportesDeGas = new HashSet<AlmacenGasDescarga>();
        }
    
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
    
        public virtual TipoProveedor TipoProveedor { get; set; }
        public virtual Empresa Empresa { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrdenCompra> OrdenesCompra { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlmacenGasDescarga> EntregasGas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlmacenGasDescarga> TransportesDeGas { get; set; }
        public virtual EstadosRepublica EstadoDeRepublica { get; set; }
        public virtual FormaPago FormaDePago { get; set; }
        public virtual Pais Pais { get; set; }
        public virtual Banco Banco { get; set; }
        public virtual CuentaContable CuentaContable { get; set; }
        public virtual RegimenFiscal RegimenFiscal { get; set; }
        public virtual TipoPersona TipoPersonaFiscal { get; set; }
    }
}
