using System;


namespace MVC.Presentacion.Models
{
    public class UsoCFDIDTO
    {
        public int Id_UsoCFDI { get; set; }
        public string UsoCFDISAT { get; set; }
        public string Descripcion { get; set; }
        public bool PersonaFisica { get; set; }
        public bool PersonaMoral { get; set; }
        public DateTime? FechaIniVigencia { get; set; }
        public DateTime? FechaFinVigencia { get; set; }
    }
}