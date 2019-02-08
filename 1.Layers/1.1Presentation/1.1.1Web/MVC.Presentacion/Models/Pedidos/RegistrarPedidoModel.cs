using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Pedidos
{
    public class RegistrarPedidoModel
    {
        public int IdPedido { get; set; }
        [Display(Name = "Cliente")]
        public int IdCliente { get; set; }
        public int IdPedidoDetalle { get; set; }
        public short IdEmpresa { get; set; }
        public int IdEstatusPedido { get; set; }
        public string FolioVenta { get; set; }
        public DateTime FechaRegistroPedido { get; set; }
        public DateTime FechaPedido { get; set; }
        [Display(Name = "TipoUnidad")]
        public int IntTipoUnidad { get; set; }
        public int IdPipa { get; set; }
        public int IdCamioneta { get; set; }
        public int Ruta { get; set; }
        [Display(Name = "Domicilio")]
        public short Orden { get; set; }//IdDireccion
                                        //PedidoDetalle 
        public decimal TotalKilos { get; set; }
        public decimal TotalLitros { get; set; }
        public string Cantidad { get; set; }
        public string Cantidad20 { get; set; }
        public string Cantidad30 { get; set; }
        public string Cantidad45 { get; set; }
        public string MotivoCancelacion { get; set; }

        public string Telefono1 { get; set; }
        public string Rfc { get; set; }
    }
}