/***
 * TanquesDto
 * Clase para almacenar en una lista de objetos
 * los tanques vendidos en el reporte del dia 
 * Developer: Jorge Omar Tovar Martínez
 * Company:Neoteck
 * Date: 03/10/2018 
 * Update: 03/10/2018
 **/
namespace Application.MainModule.DTOs.Mobile
{
    public class TanquesDto
    {
        public string NombreTanque { get; set; }
        public int Normal { get; set; }
        public int Venta { get; set; }
    }
}
