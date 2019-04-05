using System;

namespace MVC.Presentacion.Models
{
    public class MetodoPagoDTO
    {
        public int Id_MetodoPago { get; set; }
        public string MetodoPagoSAT { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaIniVigencia { get; set; }
        public Nullable<DateTime> FechaFinVigencia { get; set; }
    }
}