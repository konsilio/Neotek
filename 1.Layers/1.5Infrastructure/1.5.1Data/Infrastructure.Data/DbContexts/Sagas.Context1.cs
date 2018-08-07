﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Infrastructure.Data.DbContexts
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using Sagas.MainModule.Entidades; //KSH_19/07/2018 Se agrega para que el generador automático de código genere este using hacia la capa de dominio.
    
    public partial class SagasMainModuleEntities : DbContext
    {
        public SagasMainModuleEntities()
            : base("name=SagasMainModuleEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AdministracionCentral> AdministracionCentral { get; set; }
        public virtual DbSet<EstadosRepublica> EstadosRepublica { get; set; }
        public virtual DbSet<Pais> Pais { get; set; }
        public virtual DbSet<Empresa> Empresa { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<UsuarioAC> UsuarioAC { get; set; }
        public virtual DbSet<Requisicion> Requisicion { get; set; }
        public virtual DbSet<RequisicionEstatus> RequisicionEstatus { get; set; }
        public virtual DbSet<RequisicionProducto> RequisicionProducto { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<CProductoCategoria> CProductoCategoria { get; set; }
        public virtual DbSet<CProductoLinea> CProductoLinea { get; set; }
        public virtual DbSet<CProductoServicioTipo> CProductoServicioTipo { get; set; }
        public virtual DbSet<CProductoUnidadMedida> CProductoUnidadMedida { get; set; }
        public virtual DbSet<CProveedor> CProveedor { get; set; }
        public virtual DbSet<CProveedorBancario> CProveedorBancario { get; set; }
        public virtual DbSet<CProveedorContacto> CProveedorContacto { get; set; }
        public virtual DbSet<CProveedorDireccion> CProveedorDireccion { get; set; }
        public virtual DbSet<CProveedorFiscal> CProveedorFiscal { get; set; }
        public virtual DbSet<CProveedorTipoProveedor> CProveedorTipoProveedor { get; set; }
        public virtual DbSet<OrdenCompra> OrdenCompra { get; set; }
        public virtual DbSet<OrdenCompraEstatus> OrdenCompraEstatus { get; set; }
        public virtual DbSet<OrdenCompraImporte> OrdenCompraImporte { get; set; }
        public virtual DbSet<CProductoAsociado> CProductoAsociado { get; set; }
        public virtual DbSet<OrdenCompraProducto> OrdenCompraProducto { get; set; }
    }
}
