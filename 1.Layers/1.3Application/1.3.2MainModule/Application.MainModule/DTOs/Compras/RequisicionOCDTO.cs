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
        public bool EsGasTransporte { get; set; }
        public List<ProductoOCDTO> ProductosOC { get; set; }
    }
}
