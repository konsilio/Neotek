using System.Collections.Generic;

namespace Application.MainModule.DTOs.Mobile
{
    public class TipoPersonaDto
    {
        public byte IdTipoPersona { get; set; }
        public string Descripcion { get; set; }
        public List<TipoRegimenFiscalDto> Regimenes { get; set; }
    }
}
