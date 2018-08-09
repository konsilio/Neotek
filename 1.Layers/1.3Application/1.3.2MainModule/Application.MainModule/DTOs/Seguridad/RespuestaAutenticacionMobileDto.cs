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
        public List<MenuDto> listMenu { get; set; }
    }
}
