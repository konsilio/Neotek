using Application.MainModule.DTOs.Cobranza;

using System.Collections.Generic;

namespace Application.MainModule.DTOs
{
    public class CarteraDTO
    {
        public List<CargosDTO> CarteraRecuperada { get; set; }
        public List<CargosDTO> CarteraVencida { get; set; }
        public List<CargosDTO> TopDeudoresL { get; set; }
        public List<CargosDTO> TopDeudoresV { get; set; }
    }
}
