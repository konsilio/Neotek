using MVC.Presentacion.Models.Cobranza;
using System.Collections.Generic;


namespace MVC.Presentacion.Models
{
    public class CarteraDTO
    {
        public List<CargosModel> CarteraRecuperada { get; set; }
        public List<CargosModel> CarteraVencida { get; set; }
        public List<CargosModel> TopDeudoresL { get; set; }
        public List<CargosModel> TopDeudoresV { get; set; }
    }
}