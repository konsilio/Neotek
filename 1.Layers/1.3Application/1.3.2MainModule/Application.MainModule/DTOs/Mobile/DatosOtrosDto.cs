/**
 * DatosOtrosDto
 * Clase Dto para el catalgo de categorias de tipo producto
 * en el aprtado de otros en la app, se utiliza para las 
 * listas de Categoria,Linea y Producto dentro de categorias se 
 * esta anidados las correspondientes entidades relacionadas
 * Developer: Jorge Omar Tovar Martínez jorge.tovar@neoteck.com.mx
 * Date 26/10/2018 
 * Update 26/10/2018
 * Commpany: Neoteck
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Mobile
{
    public class DatosOtrosDto
    {
        public List<CategoriaDto> Categorias { get; set; }
        public List<LineaDto> Lineas{ get; set; }
        public List<ProductoDTO> Productos { get; set; }
    }
}
