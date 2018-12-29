using Application.MainModule.DTOs.Cobranza;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.AccesoADatos;
using Exceptions.MainModule.Validaciones;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Cobranza
{
    public class CobranzaServicio
    {
        public static List<CargosDTO> Obtener(short idempresa)
        {
            List<CargosDTO> lPedidos = AdaptadoresDTO.Cobranza.AbonosAdapter.ToDTO(new AbonosDataAcces().BuscarTodos(idempresa));
            return lPedidos;
        }
        public static CargosDTO Obtener(int idCargo)
        {
            CargosDTO Pedido = AdaptadoresDTO.Cobranza.AbonosAdapter.ToDTO(new AbonosDataAcces().Buscar(idCargo));
            return Pedido;
        }
        public static RespuestaDto Alta(Abono _abonoDto)
        {
            return new AbonosDataAcces().Insertar(_abonoDto);
        }
        public static RespuestaDto NoExiste()
        {
            string mensaje = string.Format(Error.NoExiste, "El Abono");

            return new RespuestaDto()
            {
                ModeloValido = true,
                Mensaje = mensaje,
                MensajesError = new List<string>() { mensaje },
            };
        }
    }
}
