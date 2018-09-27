using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Catalogo
{
   public class PuntoVentaDTO
    {
        public int IdPuntoVenta { get; set; }
        public short IdEmpresa { get; set; }
        public short IdCAlmacenGas { get; set; }
        public int IdOperadorChofer { get; set; }
        public System.DateTime FechaModificacion { get; set; }
        public bool Activo { get; set; }
        public System.DateTime FechaRegistro { get; set; }
        public string UnidadesAlmacen { get; set; }
        public string OperadorChofer { get; set; }
        public string Empresa { get; set; }
        public string PuntoVenta { get; set; }
        public int IdUsuario { get; set; }
    }
}
