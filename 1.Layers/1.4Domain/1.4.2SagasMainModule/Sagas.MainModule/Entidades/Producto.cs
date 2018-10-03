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

public partial class Producto
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Producto()
    {
        this.ProductoAsociado = new HashSet<ProductoAsociado>();
        this.ProductoAsociado1 = new HashSet<ProductoAsociado>();
        this.Almacen = new HashSet<Almacen>();
        this.EntradasAAlmacenMercancias = new HashSet<AlmacenEntradaProducto>();
        this.SalidasAlmacenMercancias = new HashSet<AlmacenSalidaProducto>();
        this.RequisicionProducto = new HashSet<RequisicionProducto>();
        this.OrdenCompraProducto = new HashSet<OrdenCompraProducto>();
        this.PrecioVenta = new HashSet<PrecioVenta>();
    }

    public int IdProducto { get; set; }
    public short IdEmpresa { get; set; }
    public short IdProductoServicioTipo { get; set; }
    public int IdCuentaContable { get; set; }
    public short IdCategoria { get; set; }
    public short IdProductoLinea { get; set; }
    public short IdUnidadMedida { get; set; }
    public Nullable<short> IdUnidadMedida2 { get; set; }
    public string Descripcion { get; set; }
    public bool EsActivoVenta { get; set; }
    public bool EsGas { get; set; }
    public bool EsTransporteGas { get; set; }
    public Nullable<decimal> Minimos { get; set; }
    public Nullable<decimal> Maximo { get; set; }
    public string UrlImagen { get; set; }
    public string PathImagen { get; set; }
    public bool Activo { get; set; }
    public Nullable<System.DateTime> FechaRegistro { get; set; }

    public virtual CategoriaProducto Categoria { get; set; }
    public virtual LineaProducto LineaProducto { get; set; }
    public virtual TipoServicioOProducto TipoServicioOProducto { get; set; }
    public virtual UnidadMedida UnidadMedida { get; set; }
    public virtual UnidadMedida UnidadMedida1 { get; set; }
    public virtual Empresa Empresa { get; set; }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<ProductoAsociado> ProductoAsociado { get; set; }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<ProductoAsociado> ProductoAsociado1 { get; set; }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<Almacen> Almacen { get; set; }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<AlmacenEntradaProducto> EntradasAAlmacenMercancias { get; set; }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<AlmacenSalidaProducto> SalidasAlmacenMercancias { get; set; }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<RequisicionProducto> RequisicionProducto { get; set; }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<OrdenCompraProducto> OrdenCompraProducto { get; set; }
    public virtual CuentaContable CuentaContable { get; set; }
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<PrecioVenta> PrecioVenta { get; set; }
}
