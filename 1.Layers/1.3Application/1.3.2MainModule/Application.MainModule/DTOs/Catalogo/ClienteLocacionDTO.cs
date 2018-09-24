using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Catalogo
{
   public class ClienteLocacionDTO
    {
        public int IdCliente { get; set; }
        public short Orden { get; set; }
        public byte IdPais { get; set; }
        public Nullable<byte> IdEstadoRep { get; set; }
        public string EstadoProvincia { get; set; }
        public string Municipio { get; set; }
        public string CodigoPostal { get; set; }
        public string Colonia { get; set; }
        public string Calle { get; set; }
        public string NumExt { get; set; }
        public string NumInt { get; set; }
        public string formatted_address { get; set; }
        public string location_lat { get; set; }
        public string location_lng { get; set; }
        public string place_id { get; set; }
        public string TipoLocacion { get; set; }
        public string Pais { get; set; }
        public string Estado { get; set; }
    }
}
