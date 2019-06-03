using Application.MainModule.DTOs.Cobranza;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models
{
    public class CarteraDTO
    {
        public List<CargosDTO> CarteraRecuperada { get; set; }
        public List<CargosDTO> CarteraVencida { get; set; }
    }
}