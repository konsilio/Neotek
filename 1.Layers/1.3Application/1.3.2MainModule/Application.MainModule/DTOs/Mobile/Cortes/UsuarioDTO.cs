/**
 * UsuarioDTO
 * Modelo DTO para obtener los datos importantes del 
 * usuario para los cortes y anticipos
 * @Developer: Jorge Omar Tovar Martínez
 * @Date: 11/12/2018
 * @Update: 11/12/2018
 * @company: Neotecks
 */

namespace Application.MainModule.DTOs.Mobile.Cortes
{
    public class UsuarioDTO
    {
        /// <summary>
        /// Id del usuario 
        /// </summary>
        public int IdUsuario { get; set; }
        /// <summary>
        /// Nombre del usuario
        /// </summary>
        public string Nombre { get; set; }
        /// <summary>
        /// Id de la empresa correspondiente
        /// </summary>
        public int IdEmpresa { get; set; }
    }
}
