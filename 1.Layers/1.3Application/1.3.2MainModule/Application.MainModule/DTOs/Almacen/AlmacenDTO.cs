using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Almacen
{
    public class AlmacenDTO
    {
        public int IdAlmacen { get; set; }
        public short IdEmpresa { get; set; }
        public int IdProduto { get; set; }
        public decimal Cantidad { get; set; }
        public string Ubicacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
