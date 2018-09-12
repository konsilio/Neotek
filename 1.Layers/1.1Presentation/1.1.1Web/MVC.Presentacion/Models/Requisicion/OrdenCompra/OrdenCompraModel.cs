using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.OrdenCompra
{
    public class OrdenCompraModel : OrdenCompraDTO
    {
        
        public string NumRequisicion { get; set; }
        public int IdSolicitante { get; set; }
        public string Solicitante { get; set; }    
        public string NombreEmpresa { get; set; }
        public int IdCentroCostos { get; set; }       
        public DateTime FechaEntrada { get; set; }
        public string MotivoCompra { get; set; }
        public string RequeridoEn { get; set; }
        public string btn { get; set; }
        public List<OrdenCompraPorductoDTO> OrdenCompraProductos { get; set; }
     }
}