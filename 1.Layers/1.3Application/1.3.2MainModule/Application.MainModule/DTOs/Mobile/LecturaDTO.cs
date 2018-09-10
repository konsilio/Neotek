/***
 * Clase para la lectura inicial de Estación de carburación 
 * Developer: Jorge Omar Tovar Martínez
 * Commpany: Neoteck
 * Date: 10/09/2018
 * Updated: 10/09/2018
 */
using System;
using System.Collections.Generic;

namespace Application.MainModule.DTOs.Mobile
{
    public class LecturaDTO
    {
        /// <summary>
        /// Id del tipo de medidor 
        /// </summary>
        public short IdTipoMedidor { get; set; }

        /// <summary>
        /// Nombre del tipo de medidor
        /// </summary>
        public String NombreTipoMedidor { get; set; }

        /// <summary>
        /// Número de fotografias del medidor 
        /// </summary>
        public int CantidadFotografiasMedidor { get; set; }
        /// <summary>
        /// Listado de imagenes 
        /// </summary>
        public List<String> Imagenes { get; set; }

        /// <summary>
        /// Uris de las imagenes 
        /// </summary>
        public List<String> ImagenesURL { get; set; }
        
        /// <summary>
        /// Nombre de la estación de carburación 
        /// </summary>
        public String NombreEstacionCarburacion { get; set; }
        /// <summary>
        /// Id de la estación de carburación
        /// </summary>
        public short IdCAlmacenGas { get; set; }

        /// <summary>
        /// Imagen del P5000
        /// </summary>
        public String ImagenP5000 { get; set; }

        /// <summary>
        /// Cantidad marcada por el P5000
        /// </summary>
        public int CantidadP5000 { get; set; }
        
        /// <summary>
        ///  Porcentaje del medidor 
        /// </summary>
        public decimal PorcentajeMedidor { get; set; }

        /// <summary>
        /// Clave de operación
        /// </summary>
        public String ClaveProceso { get; set; }
        /// <summary>
        /// Fecha de la lectura
        /// </summary>
    }
}
