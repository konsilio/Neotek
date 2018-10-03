using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Mobile
{
    public class TipoRegimenFiscalDto
    {
        public short IdRegimenFiscal { get; set; }
        public byte IdTipoPersona { get; set; }
        public string c_RegimenFiscal { get; set; }
        public string Descripcion { get; set; }
        public bool AplicaPersonaFisica { get; set; }
        public bool AplicaPersonaMoral { get; set; }
    }
}
