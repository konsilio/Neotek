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

public partial class UnidadMedida
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public UnidadMedida()
    {
        this.Productos = new HashSet<Producto>();
        this.ProductosConUnidadAlterna = new HashSet<Producto>();
    }

    public short IdUnidadMedida { get; set; }
    public short IdEmpresa { get; set; }
    public string Nombre { get; set; }
    public string Acronimo { get; set; }
    public string Descripcion { get; set; }
    public bool Activo { get; set; }
    public System.DateTime FechaRegistro { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<Producto> Productos { get; set; }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<Producto> ProductosConUnidadAlterna { get; set; }
    public virtual Empresa Empresa { get; set; }
}
