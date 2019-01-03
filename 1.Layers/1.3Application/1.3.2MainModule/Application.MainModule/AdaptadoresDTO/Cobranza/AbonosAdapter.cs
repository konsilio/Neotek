using Application.MainModule.DTOs.Cobranza;
using Application.MainModule.Servicios.AccesoADatos;
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
            _p.FechaRegistro = pDTO.FechaRegistro;
            _p.FechaAbono = pDTO.FechaAbono;
            _p.MontoAbono = pDTO.MontoAbono;
            _p.IdFormaPago = pDTO.IdFormaPago;
            _p.FolioBancario = pDTO.FolioBancario;

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
        public static CargosDTO ToDTO(Cargo _dto)
        {
            CargosDTO dto = new CargosDTO();
            dto.IdCargo = _dto.IdCargo;
            dto.IdCliente = _dto.IdCliente;
            dto.IdEmpresa = _dto.IdEmpresa;
            dto.Ticket = _dto.Ticket;
            dto.FechaRegistro = _dto.FechaRegistro;
            dto.TotalCargo = _dto.TotalCargo;
            dto.TotalAbonos = _dto.TotalAbonos;
            dto.VentaExtraordinaria = _dto.VentaExtraordinaria;
            dto.Activo = _dto.Activo;
            dto.FechaVencimiento = _dto.FechaVencimiento;
            dto.Saldada = _dto.Saldada;
            List<Abono> lst = new AbonosDataAcces().BuscarTodos(_dto.IdCargo);
            //if (lst != null && lst.Count > 0)
            //{ dto.Abonos = ToDTO(lst.LastOrDefault()); }

            //else
            //{
                dto.Abonos = FromInit(_dto.IdCargo);
            //}

            return dto;
        }
        public static List<CargosDTO> ToDTO(List<Cargo> lCargo)
        {
            List<CargosDTO> lprodDTO = lCargo.ToList().Select(x => ToDTO(x)).ToList();
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
