using Application.MainModule.AdaptadoresDTO.Pedidos;
using Application.MainModule.DTOs;
using Application.MainModule.DTOs.Catalogo;
using Application.MainModule.DTOs.Pedidos;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.AccesoADatos;
using Exceptions.MainModule.Validaciones;
using Sagas.MainModule.Entidades;
using Sagas.MainModule.ObjetosValor.Constantes;
using Sagas.MainModule.ObjetosValor.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Pedidos
{
    public class PedidosServicio
    {
        public static List<PedidoModelDto> Obtener()
        {
            List<PedidoModelDto> lPedidos = PedidosAdapter.ToDTO(new PedidosDataAccess().Buscar());
            return lPedidos;
        }
        public static List<PedidoModelDto> Obtener(short idempresa)
        {
            var pedidos = new PedidosDataAccess().Buscar(idempresa);
            return PedidosAdapter.ToDTO(pedidos);
        }
        public static RegistraPedidoDto Obtener(int idPedido)
        {
            RegistraPedidoDto Pedido = PedidosAdapter.ToDTOEdit(new PedidosDataAccess().BuscarPedido(idPedido));
            return Pedido;
        }
        public static List<EstatusPedidoDto> ObtenerEstatus()
        {
            return PedidosAdapter.ToDTO(new PedidosDataAccess().BuscarEstatus());
        }
        public static List<CamionetaDTO> ObtenerCamionetas(short idempresa)
        {
            List<CamionetaDTO> lCamionetas = AdaptadoresDTO.Pedidos.PedidosAdapter.ToDTO(new CamionetaDataAccess().ObtenerCamionetas(idempresa));
            return lCamionetas;
        }
        public static List<PipaDTO> ObtenerPipas(short idempresa)
        {
            List<PipaDTO> lPipas = AdaptadoresDTO.Pedidos.PedidosAdapter.ToDTO(new PipaDataAccess().ObtenerPipas(idempresa));
            return lPipas;
        }
        public static RespuestaDto Alta(Pedido _pedidoDto)
        {
            return new PedidosDataAccess().Insertar(_pedidoDto);
        }
        public static RespuestaDto Alta(List<PedidoDetalle> _pedidoDto)
        {
            return new PedidosDataAccess().Insertar(_pedidoDto);
        }
        public static RespuestaDto Alta(List<RespuestaSatisfaccionPedido> _pedidoDto)
        {
            return new PedidosDataAccess().Insertar(_pedidoDto);
        }
        public static RespuestaDto Modificar(Pedido _pedidoDto)
        {
            return new PedidosDataAccess().Actualizar(_pedidoDto);
        }
        public static RespuestaDto NoExiste()
        {
            string mensaje = string.Format(Error.NoExiste, "El pedido");

            return new RespuestaDto()
            {
                ModeloValido = true,
                Mensaje = mensaje,
                MensajesError = new List<string>() { mensaje },
            };
        }
        public static List<Pedido> Obtener(short idEmpresa, DateTime periodo)
        {
            return new PedidosDataAccess().Buscar(idEmpresa, periodo);
        }
        public static List<PedidoDashDTO> GenerarListDash(List<Pedido> lista, DateTime Periodo)
        {
            List<PedidoDashDTO> listDTO = new List<PedidoDashDTO>();
            int Dias = Periodo.Month.Equals(DateTime.Now.Month) && Periodo.Year.Equals(DateTime.Now.Year) ? DateTime.Now.Day : DateTime.DaysInMonth(Periodo.Year, Periodo.Month);
            for (int i = 1; i <= Dias; i++)
            {
                PedidoDashDTO dto = new PedidoDashDTO();
                dto.Dia = i;
                dto.TotalLlamadas = lista.Where(x => x.FechaRegistro.Day.Equals(i)).Count();
                dto.TotalVentas = lista.Where(x => x.FechaRegistro.Day.Equals(i)
                && (!x.IdEstatusPedido.Equals(EstatusPedidoEnum.Cancelado)
                    && !x.IdEstatusPedido.Equals(EstatusPedidoEnum.Solollamada))).Count();
                listDTO.Add(dto);
            }
            return listDTO;
        }
    }
}
