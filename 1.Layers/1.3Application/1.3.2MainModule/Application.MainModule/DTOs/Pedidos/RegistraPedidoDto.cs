using Exceptions.MainModule.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Pedidos
{
    public class RegistraPedidoDto
    {
        public int IdPedido { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [StringLength(100, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "Cliente")]
        public int IdCliente { get; set; }
        public int IdPedidoDetalle { get; set; }
        public short IdEmpresa { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "EstatusPedido")]
        public int IdEstatusPedido { get; set; }
        public string FolioVenta { get; set; }
        public DateTime FechaRegistroPedido { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [Display(Name = "FechaPedido")]
        public DateTime FechaPedido { get; set; }
        [Required(ErrorMessage = Error.R0002)]
        [StringLength(100, MinimumLength = 1, ErrorMessage = Error.R0004)]
        [Display(Name = "TipoUnidad")]
        public int IntTipoUnidad { get; set; }
        [Display(Name = "Pipa")]
        public int IdPipa { get; set; }       
        public int IdCamioneta { get; set; }
        public int Ruta { get; set; }
        [Required(ErrorMessage = Error.R0002)]
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
    }
}
