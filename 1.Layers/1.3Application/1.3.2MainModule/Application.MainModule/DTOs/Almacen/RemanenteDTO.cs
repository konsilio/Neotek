using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Almacen
{
   public  class RemanenteDTO
    {        
            public short IdEmpresa { get; set; }
            public int IdTipo { get; set; }
            public int IdPuntoVenta { get; set; }
            public DateTime Fecha { get; set; }
        
    }
}
