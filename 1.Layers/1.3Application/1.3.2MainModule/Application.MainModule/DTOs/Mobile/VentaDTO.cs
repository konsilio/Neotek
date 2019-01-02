/**
 *  VentaDTO
 *  Permite realizar el registro de la venta desde mobil
 *  tiene los datos principales que se extraen desde la 
 *  venta e incluye un Dto con los conceptos de este.
 *  Developer: Jorge Omar Tovar Martínez jorge.tovar@neoteck.com.mx
 *  Company: Neoteck
 *  Date: 29/10/2018 09:56
 *  Update 29/10/2018 09:56
 */
using System;
using System.Collections.Generic;

namespace Application.MainModule.DTOs.Mobile
{
    public class VentaDTO
    {
        public string FolioVenta { get; set; }
        public int IdCliente { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Iva { get; set; }
        public decimal Total { get; set; }
        public bool Factura { get; set; }
        public bool Credito { get; set; }
        public decimal Efectivo { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public decimal Cambio { get; set; }
        public bool SinNumero { get; set; }
        public bool TieneCredito { get; set; }
        public List<ConceptoDTO> Concepto { get; set; }
        public bool VentaExtraordinaria { get; set; }
    }
}
