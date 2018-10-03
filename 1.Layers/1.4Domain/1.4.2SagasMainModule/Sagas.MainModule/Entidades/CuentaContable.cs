//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

public partial class CuentaContable
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public CuentaContable()
    {
        this.CProveedor = new HashSet<Proveedor>();
        this.Productos = new HashSet<Producto>();
        this.Clientes = new HashSet<Cliente>();
    }

    public int IdCuentaContable { get; set; }
    public short IdEmpresa { get; set; }
    public string Numero { get; set; }
    public string Descripcion { get; set; }
    public bool Activo { get; set; }
    public System.DateTime FechaRegistro { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<Proveedor> CProveedor { get; set; }
    public virtual Empresa Empresa { get; set; }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<Producto> Productos { get; set; }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<Cliente> Clientes { get; set; }
}
