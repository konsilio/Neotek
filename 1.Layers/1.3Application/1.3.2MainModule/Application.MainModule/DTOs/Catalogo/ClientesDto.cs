using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Catalogo
{
  /// <summary>
  /// 
  /// </summary>
  public class ClientesDto : ClienteCrearDto
    {
        public string Empresa { get; set; }
        public string TipoPersonaFiscal { get; set; }
        public string RegimenFiscal { get; set; }
    }
}
