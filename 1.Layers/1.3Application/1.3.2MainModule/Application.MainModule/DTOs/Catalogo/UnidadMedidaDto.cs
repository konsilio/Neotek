using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Catalogo
{
    public class UnidadMedidaDto
    {
        public short IdUnidadMedida { get; set; }
        public short IdEmpresa { get; set; }
        public string Nombre { get; set; }
        public string Acronimo { get; set; }
        public string Descripcion { get; set; }
    }
}
