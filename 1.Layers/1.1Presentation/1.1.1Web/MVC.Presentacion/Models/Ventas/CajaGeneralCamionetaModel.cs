using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Ventas
{
    public class CajaGeneralCamionetaModel
    {
        public short IdEmpresa { get; set; }
        public short Year { get; set; }
        public byte Mes { get; set; }
        public byte Dia { get; set; }
        public short Orden { get; set; }
        public int IdPuntoVenta { get; set; }
        public int IdCliente { get; set; }
        public int IdOperadorChofer { get; set; }
        public Nullable<byte> IdTipoVenta { get; set; }
        public Nullable<int> IdFactura { get; set; }
        public string FolioOperacionDia { get; set; }
        public string FolioVenta { get; set; }
        public bool RequiereFactura { get; set; }
        public bool VentaACredito { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Descuento { get; set; }
        public decimal Iva { get; set; }
        public decimal Total { get; set; }
        public decimal PorcentajeIva { get; set; }
        public Nullable<decimal> EfectivoRecibido { get; set; }
        public Nullable<decimal> CambioRegresado { get; set; }
        public string PuntoVenta { get; set; }
        public string RazonSocial { get; set; }
        public string RFC { get; set; }
        public bool ClienteConCredito { get; set; }
        public string OperadorChofer { get; set; }
        public string Descripcion { get; set; }
        public string Concepto { get; set; }
        public bool DatosProcesados { get; set; }
        public System.DateTime FechaRegistro { get; set; }

        /***************************/
        public decimal VentaTotal { get; set; }
        public decimal VentaTotalCredito { get; set; }
        public decimal VentaTotalContado { get; set; }
        public decimal OtrasVentas { get; set; }
        public Nullable<int> IdCamioneta { get; set; }
        public Nullable<int> IdPipa { get; set; }
        public string Tipo { get; set; }
    }
}