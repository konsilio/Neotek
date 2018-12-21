

using Application.MainModule.DTOs.Respuesta;
/**
* DatosVentaExtraforaneaDTO
* Modelo DTO para los datos de una venta extraforanea
* @developer Jorge Omar Tovar Martínez
* @company Neoteck
* @date 20/12/2018 02:20
* @update 20/12/2018 02:20
*/
namespace Application.MainModule.DTOs.Mobile.PuntoVenta
{
    public class DatosVentaExtraforaneaDTO:RespuestaDto
    {   
        /// <summary>
        /// Permite determinar si se puede realizar la venta extraforanea
        /// </summary>
        public bool VentaExtraforanea { get; set; }
    }
}
