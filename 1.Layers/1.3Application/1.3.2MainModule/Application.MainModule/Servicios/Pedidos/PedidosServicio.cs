using Application.MainModule.DTOs.Catalogo;
using Application.MainModule.DTOs.Pedidos;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.AccesoADatos;
using Exceptions.MainModule.Validaciones;
using Sagas.MainModule.Entidades;
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
        public static List<PedidoModelDto> Obtener(short idempresa)
        {
            List<PedidoModelDto> lPedidos = AdaptadoresDTO.Pedidos.PedidosAdapter.ToDTO(new PedidosDataAccess().Buscar(idempresa));
            return lPedidos;
        }
        public static PedidoModelDto Obtener(int idPedido)
        {
            PedidoModelDto Pedido = AdaptadoresDTO.Pedidos.PedidosAdapter.ToDTO(new PedidosDataAccess().BuscarPedido(idPedido));
            return Pedido;
        }
        public static stringEstatus GetEstatusPedido(short status)
        {
            if (status == 1)
                return stringEstatus.PedidoCreado;
            if (status == 2)
                return stringEstatus.EnRuta;
            if (status == 3)
                return stringEstatus.Surtido;
            if (status == 4)
                return stringEstatus.Cancelado;
            if (status == 5)
                return stringEstatus.Solollamada;


            return stringEstatus.PedidoCreado;
        }
        public static List<EstatusPedidoDto> ObtenerEstatus()
        {
            List<EstatusPedidoDto> lPedidos = GetEstatusPedido();
            return lPedidos;
        }
        public static List<EstatusPedidoDto> GetEstatusPedido()
        {
            List<EstatusPedidoDto> _lst = new List<EstatusPedidoDto>();
            EstatusPedidoDto _item = new EstatusPedidoDto();
            _item.IdEstatusPedido = EstatusPedidoEnum.PedidoCreado;
            _item.Descripcion = getString(stringEstatus.PedidoCreado.ToString());
            _lst.Add(_item);
            EstatusPedidoDto _item2 = new EstatusPedidoDto();
            _item2.IdEstatusPedido = EstatusPedidoEnum.EnRuta;
            _item2.Descripcion = getString(stringEstatus.EnRuta.ToString());
            _lst.Add(_item2);
            EstatusPedidoDto _item3 = new EstatusPedidoDto();
            _item3.IdEstatusPedido = EstatusPedidoEnum.Surtido;
            _item3.Descripcion = getString(stringEstatus.Surtido.ToString());
            _lst.Add(_item3);
            EstatusPedidoDto _item4 = new EstatusPedidoDto();
            _item4.IdEstatusPedido = EstatusPedidoEnum.Cancelado;
            _item4.Descripcion = getString(stringEstatus.Cancelado.ToString());
            _lst.Add(_item4);
            EstatusPedidoDto _item5 = new EstatusPedidoDto();
            _item5.IdEstatusPedido = EstatusPedidoEnum.Solollamada;
            _item5.Descripcion = getString(stringEstatus.Solollamada.ToString());
            _lst.Add(_item5);
            return _lst;
        }
        public static string getString(string cadena)
        {
            switch (cadena)
            {
                case "PedidoCreado":
                    cadena = "Pedido Creado";
                    break;
                case "EnRuta":
                    cadena = "En Ruta";
                    break;
                case "Solollamada":
                    cadena = "Solo llamada";
                    break;
            }

            return cadena;
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
    }
}
