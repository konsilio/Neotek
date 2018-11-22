
using System.Collections.Generic;
/**
*AnticiposEstacionDTO
* Modelo anidado DTO  para poder extraer el total de anticipos 
* que se han registrado en la estación de carburación 
*/
namespace Application.MainModule.DTOs.Mobile
{
    public class AnticiposEstacionDTO
    {
        public int IdEstacion { get; set; }
        public int IdCAlmacenGas { get; set; }
        public decimal Total { get; set; }
        public List<AnticipoDto> Anticipos { get; set; }
    }
}
