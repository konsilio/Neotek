﻿/**
 * DatosBusquedaCortesDTO
 * Permite crear un objeto de respuesta DTO
 * en el cual se incluye los datos de la estación,
 * las ventas encontradas y dependiendo si es corte o anticipo
 * se retornara un objeto con los datos de este si existen 
 * @developer Jorge Omar Tovar Martínez
 * @company Neoteck
 * @date 26/12/2018 01:03
 * @update 26/12/2018 01:03
 */
using System.Collections.Generic;
using Application.MainModule.DTOs.Respuesta;

namespace Application.MainModule.DTOs.Mobile.Cortes
{
    /// <summary>
    /// Objeto con los datos a responder para los corte
    /// o anticipos de un CAlmacenGas
    /// </summary>
    public class DatosBusquedaCortesDTO : RespuestaDto
    {
        /// <summary>
        /// Determina si ya se tiene anticipos
        /// </summary>
        public bool hayAnticipos { get; set; }
        /// <summary>
        /// Determina si existen cortes
        /// </summary>
        public bool hayCortes { get; set; }
        /// <summary>
        /// Determina si hay ventas
        /// </summary>
        public bool hayVentas { get; set; }
        /// <summary>
        /// Objeto que contiene información de los anticipos de la estación
        /// </summary>
        public AnticipoInfoDTO anticipo { get; set; }
        /// <summary>
        /// Objeto que contiene información de los cortes
        /// </summary>
        public CorteInfoDTO corte { get; set; }
        /// <summary>
        /// Objeto con la información de la estación a la que se 
        /// le realiza el corte o anticipo
        /// </summary>
        public EstacionesDto estacion { get; set; }
        /// <summary>
        /// Objeto con los datos de las ventas que se encontraron 
        /// </summary>
        public VentasInfoDTO venta { get; set; }

    }
    #region Clases internas de respuesta
    /// <summary>
    /// Permite crear un objeto dto con los datos de 
    /// corte y su total actual en caso de haber alguno
    /// </summary>
    public class CorteInfoDTO
    {
        /// <summary>
        /// Reprecenta el total de los cortes encontrados
        /// </summary>
        public decimal totalCortes { get; set; }
        /// <summary>
        /// Listado de cortes encontrados
        /// </summary>
        public List<CorteDto> cortes { get; set; }
        /// <summary>
        /// Constructor de clase para inicializar la lista de cortes 
        /// </summary>
        public CorteInfoDTO()
        {
            cortes = new List<CorteDto>();
        }
    }
    /// <summary>
    /// Permite crear un objeto con los datos de 
    /// los anticipos realizados y su total en 
    /// caso de haber alguno
    /// </summary>
    public class AnticipoInfoDTO
    {
        /// <summary>
        /// Suma total de los totales de anticipos
        /// </summary>
        public decimal totalAnticipos { get; set; }
        /// <summary>
        /// Listado de anticipos registrados en la estación
        /// </summary>
        public List<AnticipoDto> anticipos { get; set; }
        /// <summary>
        /// Constructor de clase para inicializar el listado de anticipos
        /// </summary>
        public AnticipoInfoDTO()
        {
            anticipos = new List<AnticipoDto>();
        }
    }
    /// <summary>
    /// Permite crear un objeto con los datos de 
    /// las ventas de una estación y 
    /// </summary>
    public class VentasInfoDTO
    {
        /// <summary>
        /// Reprecenta la suma total de las ventas realizadas
        /// </summary>
        public decimal totalVentas { get; set; }
        /// <summary>
        /// Lista de ventas encontradas para el corte o anticipo
        /// </summary>
        public List<VentaDTO> ventas { get; set; }
        /// <summary>
        /// Constructor de clase para inicializar la lista de ventas
        /// </summary>
        public VentasInfoDTO()
        {
            ventas = new List<VentaDTO>();
        }
    }
    #endregion
}
