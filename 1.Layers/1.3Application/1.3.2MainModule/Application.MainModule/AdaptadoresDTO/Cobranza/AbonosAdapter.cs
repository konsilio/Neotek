using Application.MainModule.DTOs.Cobranza;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.Servicios.Catalogos;
using Application.MainModule.Servicios.Cobranza;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO.Cobranza
{
    class AbonosAdapter
    {
        public static AbonosDTO ToDTO(Abono _Abono)
        {
            AbonosDTO dto = new AbonosDTO();
            dto.IdAbono = _Abono.IdAbono;
            dto.IdCargo = _Abono.IdCargo;
            dto.FechaRegistro = _Abono.FechaRegistro;
            dto.FechaAbono = _Abono.FechaAbono;
            dto.MontoAbono = _Abono.MontoAbono;
            dto.IdFormaPago = _Abono.IdFormaPago;
            dto.FolioBancario = _Abono.FolioBancario;
            dto.FormaPago = FormaPagoServicio.Obtener(_Abono.IdFormaPago).Descripcion;
            return dto;
        }
        public static List<AbonosDTO> ToDTO(List<Abono> lAbono)
        {
            List<AbonosDTO> lprodDTO = lAbono.ToList().Select(x => ToDTO(x)).ToList();
            return lprodDTO;
        }
        public static Abono FromDTO(AbonosDTO pDTO)
        {
            Abono _p = new Abono();

            _p.IdAbono = pDTO.IdAbono;
            _p.IdCargo = pDTO.IdCargo;
            _p.FechaRegistro = DateTime.Now;
            _p.FechaAbono = pDTO.FechaAbono;
            _p.MontoAbono = pDTO.MontoAbono;
            _p.IdFormaPago = pDTO.IdFormaPago;
            _p.FolioBancario = pDTO.FolioBancario;
            _p.ACTIVO = true;

            return _p;
        }
        public static List<Abono> FromDTO(List<AbonosDTO> lPDTO)
        {
            List<Abono> lAbono = lPDTO.ToList().Select(x => FromDTO(x)).ToList();
            return lAbono;
        }
        public static Abono FromEntity(Abono pAnterior)
        {
            return new Abono()
            {
                IdAbono = pAnterior.IdAbono,
                IdCargo = pAnterior.IdCargo,
                FechaRegistro = pAnterior.FechaRegistro,
                FechaAbono = pAnterior.FechaAbono,
                MontoAbono = pAnterior.MontoAbono,
                IdFormaPago = pAnterior.IdFormaPago,
                FolioBancario = pAnterior.FolioBancario,
            };
        }
        static List<Abono> FromEntity(List<Abono> lPDTO)
        {
            return lPDTO.ToList().Select(x => FromEntity(x)).ToList();
        }
        public static Cargo FromDTO(CargosDTO _dto, decimal totalAbono)
        {
            Cargo dto = new Cargo();
            dto.IdCargo = _dto.IdCargo;
            dto.IdCliente = _dto.IdCliente;
            dto.IdEmpresa = _dto.IdEmpresa;
            dto.Ticket = _dto.Ticket;
            dto.FechaRegistro = _dto.FechaRegistro;
            dto.TotalCargo = _dto.TotalCargo;
            dto.TotalAbonos = _dto.TotalAbonos + totalAbono;
            dto.VentaExtraordinaria = _dto.VentaExtraordinaria;
            dto.Activo = _dto.Activo;
            dto.FechaVencimiento = _dto.FechaVencimiento;
            dto.Saldada = _dto.Saldada;

            return dto;
        }
        public static CargosDTO ToDTO(Cargo _dto)
        {
            List<Abono> lst = new AbonosDataAcces().BuscarTodos(_dto.IdCargo);
            CargosDTO dto = new CargosDTO();
            dto.IdCargo = _dto.IdCargo;
            dto.IdCliente = _dto.IdCliente;
            dto.Cliente = ClienteServicio.Obtener(_dto.IdCliente).RazonSocial;
            dto.Rfc = ClienteServicio.Obtener(_dto.IdCliente).Rfc;
            dto.IdEmpresa = _dto.IdEmpresa;
            dto.Ticket = _dto.Ticket;
            dto.FechaRegistro = _dto.FechaRegistro;
            dto.TotalCargo = _dto.TotalCargo;
            dto.TotalAbonos = _dto.TotalAbonos;
            dto.SaldoInsoluto = _dto.TotalCargo- _dto.TotalAbonos;
            dto.VentaExtraordinaria = _dto.VentaExtraordinaria;
            dto.Activo = _dto.Activo;
            dto.FechaVencimiento = _dto.FechaVencimiento;
            dto.Saldada = _dto.Saldada;
            dto.lstCreditoR = ToDTO(lst);
            dto.Abono = FromInit(_dto.IdCargo);
            //dto.Total = lst.Sum(x => x.MontoAbono);
            //dto.TotalEfectivo = lst.Where(y => y.IdFormaPago == 1).Sum(x => x.MontoAbono);
            //dto.TotalCheques = lst.Where(y => y.IdFormaPago == 2).Sum(x => x.MontoAbono);
            //dto.TotalTransferencia = lst.Where(y => y.IdFormaPago == 3).Sum(x => x.MontoAbono);
            return dto;
        }
        public static List<CargosDTO> ToDTO(List<Cargo> lCargo)
        {
            List<CargosDTO> lprodDTO = lCargo.ToList().Select(x => ToDTO(x)).ToList();
            lprodDTO[0].Total = CobranzaServicio.Total(lprodDTO, "T");
            lprodDTO[0].TotalEfectivo = CobranzaServicio.Total(lprodDTO, "TE");
            lprodDTO[0].TotalCheques = CobranzaServicio.Total(lprodDTO, "TC");
            lprodDTO[0].TotalTransferencia = CobranzaServicio.Total(lprodDTO, "TT");

            return lprodDTO;
        }
        public static CargosDTO ToDTORep(CarteraVencidaDTO _dto)
        {
            CargosDTO dto = new CargosDTO();
            dto.IdCliente = _dto.IdCliente;
            dto.IdEmpresa = _dto.IdEmpresa;
            dto.Cliente = ClienteServicio.Obtener(_dto.IdCliente).RazonSocial;
            dto.Rfc = ClienteServicio.Obtener(_dto.IdCliente).Rfc;
            dto.Nombre = _dto.Nombre;
            dto.Ticket = _dto.Ticket;
            dto.Serie = _dto.Serie;
            dto.TotalCargo = _dto.MontoCargo;
            dto.FechaRegistro = _dto.FechaReg;
            dto.FechaVencimiento = _dto.FechaVen;
            dto.SaldoActual = _dto.SaldoActual;
            dto.SaldoCorriente = _dto.SaldoCorriente;
            dto.SaldoVencido = _dto.SaldoVencido;
            dto.Dias1a7 = _dto.Dias1_7;
            dto.Dias8a16 = _dto.Dias8_16;
            dto.Dias17a31 = _dto.Dias17_31;
            dto.Dias32a61 = _dto.Dias32_61;
            dto.Dias62a91 = _dto.Dias62_91;
            dto.Mas91 = _dto.Mas91;
            //dto.TotSaldoActual = _dto.SaldoActualTotal;
            //dto.TotSaldoCorriente = _dto.SaldoCorrienteTotal;
            //dto.TotSaldoVencido = _dto.SaldoVencidoTotal;
            //dto.TotDias1a7 = _dto.Dias1_7Total;
            //dto.TotDias8a16 = _dto.Dias8_16Total;
            //dto.TotDias17a31 = _dto.Dias17_31Total;
            //dto.TotDias32a61 = _dto.Dias32_61Total;
            //dto.TotDias62a91 = _dto.Dias62_91Total;
            //dto.TotDiasmas91 = _dto.Mas91Total;
            return dto;
        }
        public static ReporteDTO ToDTORep(List<CarteraVencidaDTO> lCargoV, List<CarteraVencidaDTO> lCargoT)
        {
            ReporteDTO lprodDTO = new ReporteDTO();
            lprodDTO.reportedet = lCargoV.ToList().Select(x => ToDTORep(x)).ToList();
            lprodDTO.global = lCargoT;
            return lprodDTO;
        }
        public static AbonosDTO FromInit(int id)
        {
            AbonosDTO dto = new AbonosDTO();
            // dto.IdAbono = _Abono.IdAbono;
            dto.IdCargo = id;
            dto.FechaRegistro = DateTime.Now;
            dto.FechaAbono = DateTime.Now;
            dto.MontoAbono = 0;
            dto.IdFormaPago = 1;
            dto.FolioBancario = "";
            return dto;
        }
    }
}
