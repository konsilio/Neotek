using Application.MainModule.DTOs.Catalogo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Pedidos
{
    public class PedidoModelDto : ClienteCrearDto
    {
        public int IdPedido { get; set; }
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
        public List<PedidoModelDto> Pedidos { get; set; }
        public List<ClienteCrearDto> clientes { get; set; }
        public List<EncuestaDto> encuesta { get; set; }
    }

}
