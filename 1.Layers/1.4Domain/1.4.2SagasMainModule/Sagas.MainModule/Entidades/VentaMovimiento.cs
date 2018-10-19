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
    
    public partial class VentaMovimiento
    {
        public short IdEmpresa { get; set; }
        public short Year { get; set; }
        public byte Mes { get; set; }
        public byte Dia { get; set; }
        public short Orden { get; set; }
        public byte IdTipoMovimiento { get; set; }
        public int IdPuntoVenta { get; set; }
        public int IdCliente { get; set; }
        public int IdOperadorChofer { get; set; }
        public short IdCAlmacenGas { get; set; }
        public string FolioOperacionDia { get; set; }
        public string FolioVenta { get; set; }
        public string FolioAnticipo { get; set; }
        public string FolioCorteCaja { get; set; }
        public string TipoMovimiento { get; set; }
        public string Descripcion { get; set; }
        public decimal Ingreso { get; set; }
        public decimal Egreso { get; set; }
        public decimal Saldo { get; set; }
        public string PuntoVenta { get; set; }
        public string OperadorChoferNombre { get; set; }
        public System.DateTime FechaAplicacion { get; set; }
        public System.DateTime FechaRegistro { get; set; }
    
        public virtual UnidadAlmacenGas CAlmacenGas { get; set; }
        public virtual OperadorChofer COperadorChofer { get; set; }
        public virtual PuntoVenta CPuntoVenta { get; set; }
    }
}