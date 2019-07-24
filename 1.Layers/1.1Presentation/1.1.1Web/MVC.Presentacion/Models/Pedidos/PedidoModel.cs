using MVC.Presentacion.Models.Catalogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models.Pedidos
{
    public class PedidoModel 
    {
        public int IdPedido { get; set; }
        public int IdCliente { get; set; }
        public short Orden { get; set; }
        public int IdPedidoDetalle { get; set; }
        public int IdEstatusPedido { get; set; }
        public string EstatusPedido { get; set; }
        public string Cantidad { get; set; }
        public string Cantidad20 { get; set; }
        public string Cantidad30 { get; set; }
        public string Cantidad45 { get; set; }
        public string MotivoCancelacion { get; set; }
        public string Calle { get; set; }
        public string Colonia { get; set; }
        public string Unidad { get; set; }
        public string NombreRfc { get; set; }
        public string Telefono { get; set; }
        public int IdPipa { get; set; }
        public int IdCamioneta { get; set; }
        public string ReferenciaUbicacion { get; set; }
        public DateTime FechaRegistroPedido { get; set; }
        public DateTime FechaEntregaPedido { get; set; }
        public string Empresa { get; set; }
        public string TipoPersonaFiscal { get; set; }
        public string RegimenFiscal { get; set; }
        public string FolioVenta { get; set; }
        public int Ruta { get; set; }
        public short IdDireccion { get; set; }
        public decimal TotalKilos { get; set; }
        public decimal TotalLitros { get; set; }
        public List<PedidoModel> Pedidos { get; set; }
        public List<ClientesModel> clientes { get; set; }
        public ClientesModel cliente { get; set; }
        public List<EncuestaModel> encuesta { get; set; }
    }
}