using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Mobile
{
    public class DescargaDto
    {
        public string ClaveOperacion { get; set; }
        public int IdOrdenCompra {get; set;}
        //public string NombreTipoMedidorTractor {get; set;}
        //public string NombreTipoMedidorAlmacen {get; set;}
        public short IdTipoMedidorTractor {get; set;}
        public short IdTipoMedidorAlmacen {get; set;}
        //public int CantidadFotosAlmacen {get; set;}
        //public int CantidadFotosTractor {get; set;}
        public bool TanquePrestado {get; set;}
        public decimal PorcentajeMedidorAlmacen {get; set;}
        public decimal PorcentajeMedidorTractor {get; set;}
        public short IdAlmacen {get; set;}
        public DateTime FechaDescarga { get; set; }
        public List<String> Imagenes { get; set; }
    }
}
