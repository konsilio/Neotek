using Application.MainModule.DTOs.Almacen;
using Exceptions.MainModule.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.MainModule.DTOs.Compras
{
    [Serializable]
    public class OrdenCompraEntradasDTO
    {
        public int IdOrdenCompra { get; set; }
        public string NumOrdenCompra { get; set; }
        public short IdEmpresa { get; set; }
        public string Empresa { get; set; }
        public byte IdOrdenCompraEstatus { get; set; }       
        public int IdRequisicion { get; set; }
        public string NumeroRequisicion { get; set; }
        public string UsuarioSolicitante { get; set; }
        public string MotivoRequisicion { get; set; }
        public string RequeridoEn { get; set; }
        public int IdProveedor { get; set; }
        public string Proveedor { get; set; }
        public DateTime FechaRequerida { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Fecha de entrada")]
        public DateTime FechaEntrada { get; set; }
        public List<AlmacenEntradaDTO> Productos { get; set; }
    }
}
