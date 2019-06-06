using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.DTOs.Mobile;

namespace Application.MainModule.DTOs.Seguridad
{
    public class RespuestaAutenticacionMobileDto: RespuestaAutenticacionDto
    {
        public List<Application.MainModule.DTOs.Mobile.MenuDto> listMenu { get; set; }
        /// <summary>
        /// Id del amancen de gas asignado , en caso de no tener sera 0 
        /// </summary>
        public short IdCAlmacenGas { get; set; }
    }
}
