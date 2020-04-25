using Application.MainModule.DTOs.Seguridad;
using Exceptions.MainModule.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Mobile
{
    public class LoginFbDTO: AutenticacionDto
    {
        [Required(ErrorMessage = Error.S0001)]
        [Display(Name = "FbToken")]
        public String FbToken { get; set; }
        //public string Version { get; set; }
    }
}
