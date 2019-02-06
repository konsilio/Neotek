using System;

namespace MVC.Presentacion.Models
{
    public class AsignacionUtilitarioModel
    {
        public int IdAsignacionUtilitario { get; set; }
        public short IdEmpresa { get; set; }
        public int IdUtilitario { get; set; }
        public int IdChoferOperador { get; set; }
        public DateTime FechaMdidificacion { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
