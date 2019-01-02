/**
 * LineaDto
 * Permite retornar la información de una linea de producto
 * @developer Jorge Omar Tovar Martínez jorge.tovar@neoteck.com.mx
 * @date   26/10/2018 04:10
 * @update 28/12/2018 12:54
 * @company Neoteck
 */
using System.Collections.Generic;

namespace Application.MainModule.DTOs.Mobile
{
    public class LineaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdLinea { get; set; }

        public List<ProductoDTO> Productos { get; set; } 
    }
}
