using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Seguridad
{
    public class AutenticacionDTO
    {
        const string eReq = "Requerido";
        const string eTa = "Tampaño incorrecto";

        [Required(ErrorMessage = eReq)]
        [Display(Name = "IdEmpresa")]
        public short IdEmpresa { get; set; }

        [Required(ErrorMessage = eReq)]
        [StringLength(500, MinimumLength = 1, ErrorMessage = eTa)]
        [Display(Name = "Nombre de usuario")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = eReq)]
        [StringLength(250, MinimumLength = 8, ErrorMessage = eTa)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }


    }
}