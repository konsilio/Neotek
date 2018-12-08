using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Ventas
{
    public class MovimientosGasModel
    {
        public short IdEmpresa { get; set; }
        public short Year { get; set; }
        public byte Mes { get; set; }
        public byte Dia { get; set; }
        public short Orden { get; set; }
        public byte IdTipoMovimiento { get; set; }
        public Nullable<byte> IdTipoEvento { get; set; }
        public Nullable<short> IdOrdenVenta { get; set; }
        public short IdAlmacenGas { get; set; }
        public short IdCAlmacenGasPrincipal { get; set; }
        public Nullable<short> IdCAlmacenGasReferencia { get; set; }
        public Nullable<int> IdAlmacenEntradaGasDescarga { get; set; }
        public Nullable<int> IdAlmacenGasRecarga { get; set; }
        public string FolioOperacionDia { get; set; }
        public string CAlmacenPrincipalNombre { get; set; }
        public string CAlmacenReferenciaNombre { get; set; }
        public string OperadorChoferNombre { get; set; }
        public string TipoEvento { get; set; }
        public string TipoMovimiento { get; set; }
        public decimal EntradaKg { get; set; }
        public decimal EntradaLt { get; set; }
        public decimal SalidaKg { get; set; }
        public decimal SalidaLt { get; set; }
        public Nullable<decimal> P5000Anterior { get; set; }
        public Nullable<decimal> P5000Actual { get; set; }
        public System.DateTime FechaAplicacion { get; set; }
        public System.DateTime FechaRegistro { get; set; }
        /********************/
        public decimal venta { get; set; }
    }
}