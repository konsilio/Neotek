/***
 * Clase para la información de la lectura de Almacen
 * Developer: Jorge Omar Tovar Martínez
 * Commpany: Neoteck
 * Date: 07/09/2018
 * Updated: 07/09/2018
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Mobile
{
    /// <summary>
    /// LecturaAlmacenDto
    /// Permite alnacenar los datos del las
    /// lecturas de almacen inicial y final 
    /// </summary>
    public class LecturaAlmacenDto
    {   
        /// <summary>
        /// Clave de operación de la lectura (Ej. LIA07092018114101,LFA07092018114101)
        /// </summary>
        public string ClaveOperacion { get; set; }
        
        /// <summary>
        /// Id de la estación de carburación
        /// </summary>
        public int IdEstacionCarburacion { get; set; }
        
        /// <summary>
        /// Id del tipo de medidor 
        /// </summary>
        public int IdTipoMedidor { get; set; }
        
        /// <summary>
        /// Procentaje que marco el medidor P5000
        /// </summary>
        public decimal PorcentajeP5000 { get; set; }
        
        /// <summary>
        /// Imagen del medidor P50000
        /// </summary>
        public string ImagenP5000 { get; set; }
        
        /// <summary>
        /// Procentaje del medidor 
        /// </summary>
        public decimal PorcentajeMedidor { get; set; }
        
        /// <summary>
        /// Lista de imagenes del medidor 
        /// </summary>
        public List<String> Imagenes { get; set; }
    }
}
