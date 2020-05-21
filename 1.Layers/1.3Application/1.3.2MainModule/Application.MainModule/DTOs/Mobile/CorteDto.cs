using Application.MainModule.DTOs.Mobile.Cortes;
using System;
using System.Collections.Generic;


namespace Application.MainModule.DTOs.Mobile
{
    public class CorteDto
    {
        public short IdCorte { get; set; }
        public string Tiket { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public string ClaveOperacion { get; set; }
        public short IdCAlmacenGas { get; set; }
        public decimal Total { get; set; }
        public string Recibe { get; set; }
        public DateTime FechaCorte { get; set; }
        public List<VentasCorteDTO> Conceptos { get; set; }
        public short IdEntrega { get; set; }
        public short IdRecibe { get; set; }
        public string Entrega { get; set; }
        public decimal PrecioSalida { get; set; }
    }
}
