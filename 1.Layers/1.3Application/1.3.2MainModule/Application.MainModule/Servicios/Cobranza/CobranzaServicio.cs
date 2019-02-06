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
        public static List<CargosDTO> CRecuperada(short idempresa)
        {
            List<CargosDTO> lPedidos = AdaptadoresDTO.Cobranza.AbonosAdapter.ToDTO(new AbonosDataAcces().Buscar(idempresa));
            return lPedidos;
        }
        public static ReporteDTO Obtener(List<CarteraVencidaDTO> lCargoV, List<CarteraVencidaDTO> lCargoT)
        {
            ReporteDTO lPedidos = AdaptadoresDTO.Cobranza.AbonosAdapter.ToDTORep(lCargoV, lCargoT);
            return lPedidos;
        }
        public static CargosDTO Obtener(int idCargo)
        {
            CargosDTO Pedido = AdaptadoresDTO.Cobranza.AbonosAdapter.ToDTO(new AbonosDataAcces().Buscar(idCargo));
            return Pedido;
        }
        public static decimal Total(List<CargosDTO> lst, string type)
        {
            decimal result = 0;

            if(type =="T")
             foreach(var x in lst)
                {
                    foreach (var s in x.lstCreditoR)
                    {
                        result += s.MontoAbono;
                    }
                }
            if (type == "TE")
                foreach (var x in lst)
                {
                    foreach (var s in x.lstCreditoR.Where(t=>t.IdFormaPago == 1))
                    {
                        result += s.MontoAbono;
                    }
                }
            if (type == "TC")
                foreach (var x in lst)
                {
                    foreach (var s in x.lstCreditoR.Where(t => t.IdFormaPago == 2))
                    {
                        result += s.MontoAbono;
                    }
                }
            if (type == "TT")
                foreach (var x in lst)
                {
                    foreach (var s in x.lstCreditoR.Where(t => t.IdFormaPago == 3))
                    {
                        result += s.MontoAbono;
                    }
                }

            return result;
        }
        public static RespuestaDto Alta(Abono _abonoDto)
        {
            return new AbonosDataAcces().Insertar(_abonoDto);
        }
        public static RespuestaDto Alta(List<Abono> _abonoDto)
        {
            return new AbonosDataAcces().Insertar(_abonoDto);
        }
        public static RespuestaDto Update(Cargo _abonoDto)
        {
            return new AbonosDataAcces().Actualizar(_abonoDto);
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
