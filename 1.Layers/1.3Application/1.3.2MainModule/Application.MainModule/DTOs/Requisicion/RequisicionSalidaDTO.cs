using Application.MainModule.DTOs.Almacen;
using Exceptions.MainModule.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Requisicion
{
    public class RequisicionSalidaDTO
    {      
        public short IdEmpresa { get; set; }
        public string Empresa { get; set; }
        public byte IdRequisicionEstatus { get; set; }
        public int IdRequisicion { get; set; }
        public string NumeroRequisicion { get; set; }
        public string UsuarioSolicitante { get; set; }
        public string MotivoRequisicion { get; set; }
        public string RequeridoEn { get; set; }     
        public DateTime FechaRequerida { get; set; }
        public DateTime FechaSalida { get; set; }
        public List<AlmacenSalidaProductoDTO> Productos { get; set; }
    }
}
