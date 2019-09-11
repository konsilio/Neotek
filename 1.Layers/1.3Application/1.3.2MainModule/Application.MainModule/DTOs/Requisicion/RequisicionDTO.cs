using Exceptions.MainModule.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.MainModule.DTOs.Requisicion
{
    
    public class RequisicionDTO
    {
        public int IdRequisicion { get; set; }
        public int IdUsuarioSolicitante { get; set; }
        public int IdUsuarioAutorizacion { get; set; }
        public int IdUsuarioRevision { get; set; }
        public string UsuarioSolicitante { get; set; }
        public short IdEmpresa { get; set; }
        public string NombreComercial { get; set; }
        public string NumeroRequisicion { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Motivo de Requisicion")]
        public string MotivoRequisicion { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Requerido En")]
        public string RequeridoEn { get; set; }
        public string OpinionAlmacen { get; set; }
        public string MotivoCancelacion { get; set; }
        public byte IdRequisicionEstatus { get; set; }
        public string RequisicionEstatus { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Fecha Requerida")]
        public DateTime FechaRequerida { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaRevision { get; set; }
        public DateTime FechaAutorizacion { get; set; }
        public List<RequisicionProductoDTO> Productos { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Producto")]
        public int IdProducto { get; set; }
        
        public string Producto { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Tipo Producto")]
        public int IdTipoProducto { get; set; }
        
        public string TipoProducto { get; set; }
       
        public int IdCentroCosto { get; set; }
        public string CentroCosto { get; set; }
        public int IdUnidad { get; set; }
        public string Unidad { get; set; }
        public decimal Cantidad { get; set; }
       
        public string Aplicacion { get; set; }
        public bool EsExterno { get; set; }     
    }
}
