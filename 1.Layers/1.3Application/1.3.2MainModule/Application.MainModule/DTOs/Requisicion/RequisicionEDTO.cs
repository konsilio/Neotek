using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Requisicion
{
    public class RequisicionEDTO : RequisicionDTO
    {
        public List<RequisicionProductoDTO> ListaProductos { get; set; }
    }
}
