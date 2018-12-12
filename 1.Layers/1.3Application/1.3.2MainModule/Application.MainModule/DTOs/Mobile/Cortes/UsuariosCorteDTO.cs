/**
 * UsuariosCorteDTO
 * Modelo para el listado de usuarios que 
 * se implementaran en la sección de cortes
 * @Developer: Jorge Omar Tovar Martínez
 * @Date: 11/12/2018
 * @Update: 11/12/2018
 * @company: Neotecks
 */
using Application.MainModule.DTOs.Respuesta;
using System.Collections.Generic;

namespace Application.MainModule.DTOs.Mobile.Cortes
{
    public class UsuariosCorteDTO:RespuestaDto
    {
        /// <summary>
        /// Id de la empresa que corresponde 
        /// </summary>
        public short IdEmpresa { get; set; }
        /// <summary>
        /// Lista de usuarios para cortess
        /// </summary>
        public List<UsuarioDTO> Usuarios { get; set; }
    }
}
