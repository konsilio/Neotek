/**
 *  VentasCorteDTO
 *  Modelo DTO para la lista de ventas de las cuales se ligara el
 *  corte de caja
 *  Developer: Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
 *  Company: Neoteck
 *  Date: 04/12/2018
 *  Update 04/12/2018 
 */
namespace Application.MainModule.DTOs.Mobile.Cortes
{
    public class VentasCorteDTO
    {
        public string Corte { get; set; }
        public string TiketVenta { get; set; }
        public int IdVenta { get; set; }
    }
}
