using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.MainModule.DTOs.Requisicion
{
    [Serializable]
    public class RequisicionDTO
    {
        public int IdRequisicion { get; set; }
        public int IdUsuarioSolicitante { get; set; }
        public string UsuarioSolicitante { get; set; }
        public short IdEmpresa { get; set; }
        public string NombreComercial { get; set; }
        public string NumeroRequisicion { get; set; }
        public string MotivoRequisicion { get; set; }
        public string RequeridoEn { get; set; }
        public byte IdRequisicionEstatus { get; set; }
        public string RequisicionEstatus { get; set; }
        public DateTime FechaRequerida { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int IdUsuarioRevision { get; set; }
        public string OpinionAlmacen { get; set; }
        public DateTime FechaRevision { get; set; }
        public string MotivoCancelacion { get; set; }
        public int IdUsuarioAutorizacion { get; set; }
        public DateTime FechaAutorizacion { get; set; }

    }
}
