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
    
        public virtual DbSet<EstadosRepublica> EstadosRepublica { get; set; }
        public virtual DbSet<Pais> Pais { get; set; }
        public virtual DbSet<Empresa> Empresa { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Requisicion> Requisicion { get; set; }
        public virtual DbSet<RequisicionEstatus> RequisicionEstatus { get; set; }
        public virtual DbSet<RequisicionProducto> RequisicionProducto { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<CategoriaProducto> CategoriaProducto { get; set; }
        public virtual DbSet<LineaProducto> LineaProducto { get; set; }
        public virtual DbSet<TipoServicioOProducto> TipoServicioOProducto { get; set; }
        public virtual DbSet<UnidadMedida> UnidadMedida { get; set; }
        public virtual DbSet<Proveedor> Proveedor { get; set; }
        public virtual DbSet<TipoProveedor> TipoProveedor { get; set; }
        public virtual DbSet<OrdenCompra> OrdenCompra { get; set; }
        public virtual DbSet<OrdenCompraEstatus> OrdenCompraEstatus { get; set; }
        public virtual DbSet<ProductoAsociado> ProductoAsociado { get; set; }
        public virtual DbSet<OrdenCompraProducto> OrdenCompraProducto { get; set; }
        public virtual DbSet<Almacen> Almacen { get; set; }
        public virtual DbSet<AlmacenGasDescarga> AlmacenGasDescarga { get; set; }
        public virtual DbSet<AlmacenGasDescargaFoto> AlmacenGasDescargaFoto { get; set; }
        public virtual DbSet<AlmacenGas> AlmacenGas { get; set; }
        public virtual DbSet<UnidadAlmacenGas> UnidadAlmacenGas { get; set; }
        public virtual DbSet<UnidadAlmacenGasCilindro> UnidadAlmacenGasCilindro { get; set; }
        public virtual DbSet<TipoUnidadAlmacenGas> TipoUnidadAlmacenGas { get; set; }
        public virtual DbSet<TipoMedidorUnidadAlmacenGas> TipoMedidorUnidadAlmacenGas { get; set; }
        public virtual DbSet<Camioneta> Camioneta { get; set; }
        public virtual DbSet<EstacionCarburacion> EstacionCarburacion { get; set; }
        public virtual DbSet<FormaPago> FormaPago { get; set; }
        public virtual DbSet<Pipa> Pipa { get; set; }
        public virtual DbSet<ImagenDe> ImagenDe { get; set; }
        public virtual DbSet<AlmacenEntradaProducto> AlmacenEntradaProducto { get; set; }
        public virtual DbSet<AlmacenSalidaProducto> AlmacenSalidaProducto { get; set; }
        public virtual DbSet<CamionetaCilindro> CamionetaCilindro { get; set; }
        public virtual DbSet<CentroCosto> CentroCosto { get; set; }
        public virtual DbSet<Banco> Banco { get; set; }
        public virtual DbSet<UsuarioRol> UsuarioRol { get; set; }
        public virtual DbSet<CuentaContable> CuentaContable { get; set; }
        public virtual DbSet<RegimenFiscal> RegimenFiscal { get; set; }
        public virtual DbSet<TipoPersona> TipoPersona { get; set; }
        public virtual DbSet<TipoCentroCosto> TipoCentroCosto { get; set; }
        public virtual DbSet<AlmacenGasTomaLectura> AlmacenGasTomaLectura { get; set; }
        public virtual DbSet<AlmacenGasTomaLecturaCilindro> AlmacenGasTomaLecturaCilindro { get; set; }
        public virtual DbSet<AlmacenGasTomaLecturaFoto> AlmacenGasTomaLecturaFoto { get; set; }
        public virtual DbSet<AlmacenGasRecarga> AlmacenGasRecarga { get; set; }
        public virtual DbSet<AlmacenGasRecargaCilindro> AlmacenGasRecargaCilindro { get; set; }
        public virtual DbSet<AlmacenGasRecargaFoto> AlmacenGasRecargaFoto { get; set; }
        public virtual DbSet<AlmacenGasAutoConsumo> AlmacenGasAutoConsumo { get; set; }
        public virtual DbSet<AlmacenGasAutoConsumoFoto> AlmacenGasAutoConsumoFoto { get; set; }
        public virtual DbSet<AlmacenGasCalibracion> AlmacenGasCalibracion { get; set; }
        public virtual DbSet<AlmacenGasCalibracionFoto> AlmacenGasCalibracionFoto { get; set; }
        public virtual DbSet<AlmacenGasTraspaso> AlmacenGasTraspaso { get; set; }
        public virtual DbSet<AlmacenGasTraspasoFoto> AlmacenGasTraspasoFoto { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<ClienteLocacion> ClienteLocacion { get; set; }
        public virtual DbSet<OperadorChofer> OperadorChofer { get; set; }
        public virtual DbSet<PuntoVenta> PuntoVenta { get; set; }
        public virtual DbSet<TipoEvento> TipoEvento { get; set; }
        public virtual DbSet<TipoOperadorChofer> TipoOperadorChofer { get; set; }
        public virtual DbSet<OrdenCompraPago> OrdenCompraPago { get; set; }
        public virtual DbSet<PrecioVenta> PrecioVenta { get; set; }
        public virtual DbSet<PrecioVentaEstatus> PrecioVentaEstatus { get; set; }
        public virtual DbSet<AlmacenGasMovimiento> AlmacenGasMovimiento { get; set; }
        public virtual DbSet<AlmacenGasMovimientoFoto> AlmacenGasMovimientoFoto { get; set; }
        public virtual DbSet<CTipoMovimiento> CTipoMovimiento { get; set; }
        public virtual DbSet<ReporteDelDia> ReporteDelDia { get; set; }
        public virtual DbSet<VentaCajaGeneral> VentaCajaGeneral { get; set; }
        public virtual DbSet<VentaCorteAnticipoEC> VentaCorteAnticipoEC { get; set; }
        public virtual DbSet<VentaMovimiento> VentaMovimiento { get; set; }
        public virtual DbSet<VentaPuntoDeVenta> VentaPuntoDeVenta { get; set; }
        public virtual DbSet<VentaPuntoDeVentaDetalle> VentaPuntoDeVentaDetalle { get; set; }
        public virtual DbSet<Abono> Abono { get; set; }
        public virtual DbSet<Cargo> Cargo { get; set; }
        public virtual DbSet<CTipoEgreso> CTipoEgreso { get; set; }
        public virtual DbSet<Egreso> Egreso { get; set; }
        public virtual DbSet<Ingreso> Ingreso { get; set; }
        public virtual DbSet<Pedido> Pedido { get; set; }
        public virtual DbSet<PedidoDetalle> PedidoDetalle { get; set; }
        public virtual DbSet<PedidoEstatus> PedidoEstatus { get; set; }
        public virtual DbSet<RespuestaSatisfaccionPedido> RespuestaSatisfaccionPedido { get; set; }
        public virtual DbSet<CCombustible> Combustible { get; set; }
        public virtual DbSet<CDetalleEquipoTransporte> DetalleEquipoTransporte { get; set; }
        public virtual DbSet<CMantenimiento> Mantenimiento { get; set; }
        public virtual DbSet<CUtilitario> Utilitario { get; set; }
        public virtual DbSet<DetalleMantenimiento> DetalleMantenimiento { get; set; }
        public virtual DbSet<DetalleRecargaCombustible> DetalleRecargaCombustible { get; set; }
        public virtual DbSet<AsignacionUtilitarios> AsignacionUtilitarios { get; set; }
        public virtual DbSet<MetodoPago> MetodoPago { get; set; }
        public virtual DbSet<CFDI> CFDI { get; set; }
        public virtual DbSet<UsoCFDI> UsoCFDI { get; set; }
        public virtual DbSet<HistoricoVentas> HistoricoVentas { get; set; }
        public virtual DbSet<Bitacora> Bitacora { get; set; }
        public virtual DbSet<CuentaContableAutorizado> CuentaContableAutorizado { get; set; }
    }
}
