using Application.MainModule.DTOs.Catalogo;
using Exceptions.MainModule.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Pedidos
{
    public class PedidoModelDto
    {
        public int IdPedido { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Tipo de Persona Fiscal")]
        public int IdCliente { get; set; }       
        public short Orden { get; set; }
        public int IdPedidoDetalle { get; set; }       
        public int IdEstatusPedido { get; set; }
        public string EstatusPedido { get; set; }       
        public string Cantidad { get; set; }
        //[Required(ErrorMessage = Error.R0002)]
        //[StringLength(100, MinimumLength = 1, ErrorMessage = Error.R0004)]
        //[Display(Name = "Cantidad Lts")]
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
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Telefono")]
        public string Telefono { get; set; }
        public string ReferenciaUbicacion { get; set; }
        public DateTime FechaRegistroPedido { get; set; }
        public DateTime FechaEntregaPedido { get; set; }
        public DateTime? FechaSurtido { get; set; }
        public string Empresa { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "Tipo de Persona Fiscal")]
        public string TipoPersonaFiscal { get; set; }
        public string RegimenFiscal { get; set; }
        public string FolioVenta { get; set; }
        public int Ruta { get; set; }
        public short IdDireccion { get; set; }
        public decimal TotalKilos { get; set; }
        public decimal TotalLitros { get; set; }
        public List<PedidoModelDto> Pedidos { get; set; }
        public List<ClienteCrearDto> clientes { get; set; }
        public ClienteCrearDto cliente { get; set; }
        public List<EncuestaDto> encuesta { get; set; }
    }

}
