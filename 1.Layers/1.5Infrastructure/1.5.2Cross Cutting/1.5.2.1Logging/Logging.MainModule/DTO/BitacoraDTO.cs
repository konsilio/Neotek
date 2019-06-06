using System;

namespace Logging.MainModule.DTO
{
    public class BitacoraDTO
    {
        public int Id { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public string Descripcion { get; set; }
        public string Accion { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
