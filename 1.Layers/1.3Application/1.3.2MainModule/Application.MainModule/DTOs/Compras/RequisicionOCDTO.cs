using Application.MainModule.DTOs.Requisicion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Compras
{
    [Serializable]
    public class RequisicionOCDTO : RequisicionDTO
    {
        public List<ProductoOCDTO> Productos { get; set; }
    }
}
