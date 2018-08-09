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
        public short IdEmpresa { get; set; }
        public string NumeroRequisicion { get; set; }
        public string MotivoRequisicion { get; set; }
        public string RequeridoEn { get; set; }
        public byte IdRequisicionEstatus { get; set; }
        public System.DateTime FechaRequerida { get; set; }
        public System.DateTime FechaRegistro { get; set; }
        public Nullable<int> IdUsuarioRevision { get; set; }
        public string OpinionAlmacen { get; set; }
        public Nullable<System.DateTime> FechaRevision { get; set; }
        public string MotivoCancelacion { get; set; }
        public Nullable<int> IdUsuarioAutorizacion { get; set; }
        public Nullable<System.DateTime> FechaAutorizacion { get; set; }

    }
}
