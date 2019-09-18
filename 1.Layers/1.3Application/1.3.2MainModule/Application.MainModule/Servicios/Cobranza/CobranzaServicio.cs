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
            return AdaptadoresDTO.Cobranza.AbonosAdapter.ToDTO(new AbonosDataAcces().Buscar(idempresa));
            //return lPedidos;
        }
        public static List<Abono> Obtener(DateTime f)
        {
            return new AbonosDataAcces().BuscarTodos(f);
        }
        public static Abono ObtenerAbono(int id)
        {
            return new AbonosDataAcces().BuscarAbono(id);
        }
        public static RespuestaDto ActualizarAbono(Abono entidad)
        {
            return new AbonosDataAcces().Actualizar(entidad);
        }
        public static List<CargosDTO> CRecuperada(short idempresa)
        {
            return AdaptadoresDTO.Cobranza.AbonosAdapter.ToDTO(new AbonosDataAcces().BuscarTodos(idempresa));
            //return lPedidos;
        }

        public static ReporteCreditoRecDto CRecuperadaList(short idempresa)
        {
            return AdaptadoresDTO.Cobranza.AbonosAdapter.ToDTOCR(new AbonosDataAcces().BuscarTodos(idempresa));
            //return lPedidos;
        }

        public static List<CargosDTO> CRecuperada(List<CRecuperadaDTO> lstCRecuperado, List<CRecuperadaTotalesDTO> lstTotal, short idempresa)
        {
            return AdaptadoresDTO.Cobranza.AbonosAdapter.ToDTO(lstCRecuperado, lstTotal);
            //return lPedidos;
        }
        public static ReporteDTO Obtener(List<CarteraVencidaDTO> lCargoV, List<CarteraVencidaDTO> lCargoT)
        {
            return AdaptadoresDTO.Cobranza.AbonosAdapter.ToDTORep(lCargoV, lCargoT);
            //return lPedidos;
        }
        public static List<Cargo> CVencida(short idEmpresa)
        {
            return new AbonosDataAcces().BuscarVencidos(idEmpresa);
        }
        public static List<CargosDTO> CarteraVencida(short idEmpresa)
        {
            return AdaptadoresDTO.Cobranza.AbonosAdapter.ToDTO(CVencida(idEmpresa));
        }
        public static List<Cargo> TopDeudores(short idEmpresa)
        {
            List<Cargo> resuesta = new List<Cargo>();
            var clientes = CVencida(idEmpresa).Select(x => x.IdCliente).Distinct();
            foreach (int id in clientes)
            {
                var cargos = new AbonosDataAcces().Buscar(idEmpresa, id);
                if (cargos != null && !cargos.Count.Equals(0))
                {
                    Cargo c = cargos[0];
                    c.TotalCargo = cargos.Sum(x => x.TotalCargo);
                    resuesta.Add(c);
                }
            }
            return resuesta.OrderBy(x => ((TimeSpan)(x.FechaVencimiento - DateTime.Now)).Days).ToList();
        }
        public static List<CargosDTO> TopDeudores(short idEmpresa, int top)
        {
            return AdaptadoresDTO.Cobranza.AbonosAdapter.ToDTO(TopDeudores(idEmpresa)).Take(top).ToList();
        }
        public static List<Cargo> TopDeudoresValiosos(short idEmpresa)
        {
            List<Cargo> resuesta = new List<Cargo>();
            var clientes = CVencida(idEmpresa).Select(x => x.IdCliente).Distinct();
            foreach (int id in clientes)
            {
                var cargos = new AbonosDataAcces().Buscar(idEmpresa, id);
                if (cargos != null && !cargos.Count.Equals(0))
                {
                    Cargo c = cargos[0];
                    c.TotalCargo = cargos.Sum(x => x.TotalCargo);
                    resuesta.Add(c);
                }
            }
            return resuesta.OrderByDescending(x => x.TotalCargo).ToList();
        }
        public static List<CargosDTO> TopDeudoresValiosos(short idEmpresa, int top)
        {
            return AdaptadoresDTO.Cobranza.AbonosAdapter.ToDTO(TopDeudoresValiosos(idEmpresa)).Take(top).ToList();
        }
        public static CargosDTO Obtener(int idCargo)
        {
            CargosDTO Pedido = AdaptadoresDTO.Cobranza.AbonosAdapter.ToDTO(new AbonosDataAcces().Buscar(idCargo));
            return Pedido;
        }
        public static decimal Total(List<CargosDTO> lst, string type)
        {
            decimal result = 0;

            if (type == "T")
                foreach (var x in lst)
                {
                    foreach (var s in x.lstCreditoR)
                    {
                        result += s.MontoAbono;
                    }
                }
            if (type == "TE")
                foreach (var x in lst)
                {
                    foreach (var s in x.lstCreditoR.Where(t => t.IdFormaPago == 1))
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
        public static int CalcularNumAbono(Abono abono)
        {
            int consecutivo = 1;
            foreach (var item in abono.Cargo.Abono.OrderBy(x => x.FechaAbono))
            {
                if (item.IdAbono.Equals(abono.IdAbono))
                    return consecutivo;
                else
                    consecutivo++;
            }
            return 0;
        }
        public static decimal CalcularNumSaldoAnteriorAbono(Abono abono)
        {
            decimal saldo = abono.Cargo.TotalCargo;
            foreach (var item in abono.Cargo.Abono.OrderBy(x => x.FechaAbono))
            {
                if (item.IdAbono.Equals(abono.IdAbono))
                    return saldo;
                else
                    saldo = saldo - item.MontoAbono;
            }
            return 0;
        }
        public static decimal CalcularNumSaldoInsolutoAbono(Abono abono)
        {
            decimal saldo = abono.Cargo.TotalCargo;
            foreach (var item in abono.Cargo.Abono.OrderBy(x => x.FechaAbono))
            {
                if (item.IdAbono.Equals(abono.IdAbono))
                    return saldo - item.MontoAbono;
                else
                    saldo = saldo - item.MontoAbono;
            }
            return 0;
        }
    }
}
