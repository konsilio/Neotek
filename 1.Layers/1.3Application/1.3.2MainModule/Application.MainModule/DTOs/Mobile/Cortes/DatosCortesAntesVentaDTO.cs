
/**
* DatosCortesAntesVentaDTO
* Permite retornar una respuesta para verificar si existe
* alguncorte actualmente en la estación para que 
* en movil se pueda proceder con la venta 
* @author Jorge Omar Tovar Martínez jorge.tovar@neoiteck.com.mx
* @date 28/12/2018 04:00
* @update 28/12/2018 04:00
* @company Neoteck
*/
namespace Application.MainModule.DTOs.Mobile.Cortes
{
    public class DatosCortesAntesVentaDTO
    {
        public bool HayCorte { get; set; }
        public CorteDto Corte { get; set; }
    }
}
