using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.OrdenCompra
{
    public class EntradaMercanciaModel
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
        public DateTime FechaEntrada { get; set; }
        public List<AlmacenEntradaDTO> Producto;
    }
}