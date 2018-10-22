using System;

namespace Application.MainModule.DTOs.Mobile
{
    public class AnticipoDto
    {
        public short Id { get; set; }
        public string Tiket { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
    }
}
