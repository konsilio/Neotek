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
    
    public partial class Empresa
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Empresa()
        {
            this.Usuario = new HashSet<Usuario>();
            this.Productos = new HashSet<Producto>();
            this.CategoriasProducto = new HashSet<CategoriaProducto>();
            this.LineasProducto = new HashSet<LineaProducto>();
            this.TiposServicioOProducto = new HashSet<TipoServicioOProducto>();
            this.UnidadesMedida = new HashSet<UnidadMedida>();
            this.Proveedores = new HashSet<Proveedor>();
            this.OrdenesCompra = new HashSet<OrdenCompra>();
            this.Requisiciones = new HashSet<Requisicion>();
            this.Almacen = new HashSet<Almacen>();
            this.AlmacenesGas = new HashSet<AlmacenGas>();
            this.UnidadesAlmacenGas = new HashSet<UnidadAlmacenGas>();
            this.Camionetas = new HashSet<Camioneta>();
            this.EstacionesCarburacion = new HashSet<EstacionCarburacion>();
            this.Pipas = new HashSet<Pipa>();
            this.CamionetasCilindros = new HashSet<CamionetaCilindro>();
            this.Roles = new HashSet<Rol>();
            this.CentrosCosto = new HashSet<CentroCosto>();
            this.CuentasContables = new HashSet<CuentaContable>();
            this.Clientes = new HashSet<Cliente>();
            this.PuntosVenta = new HashSet<PuntoVenta>();
            this.UnidadesAlmacenGasCilindro = new HashSet<UnidadAlmacenGasCilindro>();
            this.OperadoresChoferes = new HashSet<OperadorChofer>();
            this.PreciosVenta = new HashSet<PrecioVenta>();
            this.AlmacenGasMovimiento = new HashSet<AlmacenGasMovimiento>();
            this.CCombustible = new HashSet<CCombustible>();
            this.CMantenimiento = new HashSet<CMantenimiento>();
            this.CUtilitario = new HashSet<CUtilitario>();
            this.AsignacionUtilitarios = new HashSet<AsignacionUtilitarios>();
            this.CDetalleEquipoTransporte = new HashSet<CDetalleEquipoTransporte>();
            this.ControlAsistencia = new HashSet<ControlAsistencia>();
        }
    
        public short IdEmpresa { get; set; }
        public bool EsAdministracionCentral { get; set; }
        public string NombreComercial { get; set; }
        public System.DateTime FechaRegistro { get; set; }
        public bool Activo { get; set; }
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
        public decimal FactorLitrosAKilos { get; set; }
        public System.DateTime CierreInventario { get; set; }
        public byte InventarioSano { get; set; }
        public byte InventarioCrítico { get; set; }
        public decimal MaxRemaGaseraMensual { get; set; }
        public decimal FactorGalonALitros { get; set; }
        public decimal FactorCompraLitroAKilos { get; set; }
        public decimal FactorFleteGas { get; set; }
        public decimal CalibracionEstacionCarburacion { get; set; }
        public decimal CalibracionPipa { get; set; }
        public decimal CalibracionCamioneta { get; set; }
        public string UrlLogotipoMenu { get; set; }
        public string UrlLogotipoLogin { get; set; }
        public string UrlLogotipo180px { get; set; }
        public string UrlLogotipo500px { get; set; }
        public string UrlLogotipo1000px { get; set; }
        public Nullable<decimal> CoordenadaLat { get; set; }
        public Nullable<decimal> CoordenadaLong { get; set; }
        public string Leyenda { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Usuario> Usuario { get; set; }
        public virtual EstadosRepublica EstadosRepublica { get; set; }
        public virtual Pais Pais { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Producto> Productos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CategoriaProducto> CategoriasProducto { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LineaProducto> LineasProducto { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TipoServicioOProducto> TiposServicioOProducto { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UnidadMedida> UnidadesMedida { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Proveedor> Proveedores { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrdenCompra> OrdenesCompra { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Requisicion> Requisiciones { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Almacen> Almacen { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlmacenGas> AlmacenesGas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UnidadAlmacenGas> UnidadesAlmacenGas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Camioneta> Camionetas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EstacionCarburacion> EstacionesCarburacion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pipa> Pipas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CamionetaCilindro> CamionetasCilindros { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rol> Roles { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CentroCosto> CentrosCosto { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CuentaContable> CuentasContables { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cliente> Clientes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PuntoVenta> PuntosVenta { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UnidadAlmacenGasCilindro> UnidadesAlmacenGasCilindro { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OperadorChofer> OperadoresChoferes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PrecioVenta> PreciosVenta { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlmacenGasMovimiento> AlmacenGasMovimiento { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CCombustible> CCombustible { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CMantenimiento> CMantenimiento { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CUtilitario> CUtilitario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AsignacionUtilitarios> AsignacionUtilitarios { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CDetalleEquipoTransporte> CDetalleEquipoTransporte { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ControlAsistencia> ControlAsistencia { get; set; }
    }
}
