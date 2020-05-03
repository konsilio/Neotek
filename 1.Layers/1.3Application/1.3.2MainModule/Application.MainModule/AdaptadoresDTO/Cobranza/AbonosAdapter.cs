using Application.MainModule.DTOs.Cobranza;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.Servicios.Catalogos;
using Application.MainModule.Servicios.Cobranza;
using Application.MainModule.Servicios.Facturacion;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO.Cobranza
{
    public class AbonosAdapter
    {
        public static AbonosDTO ToDTO(Abono _Abono)
        {
            AbonosDTO dto = new AbonosDTO();
            var venta = CFDIServicio.Buscar(_Abono.Id_RelTF ?? 0);
           
            dto.IdCliente = _Abono.Cargo.IdCliente;
            dto.Cliente = ClienteServicio.ObtenerNomreCliente(_Abono.Cargo.CCliente);
            dto.IdAbono = _Abono.IdAbono;
            dto.IdCargo = _Abono.IdCargo;
            dto.ticket = _Abono.Cargo.Ticket;
            dto.FechaRegistro = _Abono.FechaRegistro;
            dto.FechaAbono = _Abono.FechaAbono;
            dto.MontoAbono = _Abono.MontoAbono;
            dto.IdFormaPago = _Abono.IdFormaPago;
            dto.FolioBancario = _Abono.FolioBancario;
            dto.FormaPago = FormaPagoServicio.Obtener(_Abono.IdFormaPago).Descripcion;
            dto.Id_RelTF = _Abono.Id_RelTF ?? 0;
            if (venta != null)
            {
                dto.URLXml = venta.URLXml;
                dto.URLPdf = venta.URLPdf;
            }
            //dto.URL_XML = venta.URLXml;
            //dto.URL_CFDI = venta.URLPdf;
            return dto;
        }
        public static List<AbonosDTO> ToDTO(List<Abono> lAbono)
        {
            List<AbonosDTO> lprodDTO = lAbono.ToList().Select(x => ToDTO(x)).ToList();
            return lprodDTO;
        }

        public static AbonosDTO ToDTOAbono(CRecuperadaDTO _Abono)
        {
            AbonosDTO dto = new AbonosDTO();
            dto.FechaRegistro = _Abono.FechaRegistro;
            dto.FechaAbono = _Abono.FechaAbono;
            dto.MontoAbono = _Abono.MontoAbono;
            dto.IdFormaPago = _Abono.IdFormaPago;
            dto.FolioBancario = _Abono.FolioBancario;
            dto.FormaPago = _Abono.FormaPago;
            return dto;
        }

        public static List<AbonosDTO> ToDTOAbonos(List<CRecuperadaDTO> lAbono)
        {
            List<AbonosDTO> lprodDTO = lAbono.ToList().Select(x => ToDTOAbono(x)).ToList();
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
            if (!pDTO.Id_RelTF.Equals(0))
                _p.Id_RelTF = pDTO.Id_RelTF;

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
                Id_RelTF = pAnterior.Id_RelTF,
            };
        }
        public static List<Abono> FromEntity(List<Abono> lPDTO)
        {
            return lPDTO.ToList().Select(x => FromEntity(x)).ToList();
        }
        public static Cargo FromDTO(CargosDTO _dto, decimal totalAbono)
        {
            var totAbonado = _dto.TotalAbonos + totalAbono;
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
            dto.Saldada = totAbonado >= _dto.TotalCargo ? true : false;

            return dto;
        }
        public static Cargo FromEmty(Cargo entity)
        {
            Cargo dto = new Cargo
            {
                IdCargo = entity.IdCargo,
                IdCliente = entity.IdCliente,
                IdEmpresa = entity.IdEmpresa,
                Ticket = entity.Ticket,
                FechaRegistro = entity.FechaRegistro,
                TotalCargo = entity.TotalCargo,
                TotalAbonos = entity.TotalAbonos,
                VentaExtraordinaria = entity.VentaExtraordinaria,
                Activo = entity.Activo,
                FechaVencimiento = entity.FechaVencimiento,
                Saldada = entity.Saldada
            };
            return dto;
        }
        public static CargosDTO ToDTO(Cargo _dto)
        {
            List<Abono> lst = new AbonosDataAcces().BuscarTodos(_dto.IdCargo);
            var venta = CFDIServicio.Buscar(_dto.Ticket);
            CargosDTO dto = new CargosDTO();
            dto.IdCargo = _dto.IdCargo;
            dto.IdCliente = _dto.IdCliente;
            dto.Cliente = string.Concat(_dto.CCliente.Nombre, " ", _dto.CCliente.Apellido1);
            if (_dto.CCliente.IdTipoPersona.Equals(2))
                dto.Cliente = _dto.CCliente.RazonSocial;
            dto.Rfc = ClienteServicio.Obtener(_dto.IdCliente).Rfc;
            dto.IdEmpresa = _dto.IdEmpresa;
            dto.Ticket = _dto.Ticket;
            dto.FechaRegistro = _dto.FechaRegistro;
            dto.TotalCargo = _dto.TotalCargo;
            dto.TotalAbonos = _dto.TotalAbonos;
            dto.SaldoInsoluto = _dto.TotalCargo - _dto.TotalAbonos;
            dto.VentaExtraordinaria = _dto.VentaExtraordinaria;
            dto.Activo = _dto.Activo;
            dto.FechaVencimiento = _dto.FechaVencimiento;
            dto.Saldada = _dto.Saldada;
            dto.lstCreditoR = ToDTO(lst);
            dto.Abono = FromInit(_dto.IdCargo);
            dto.Dias1a7 = ((TimeSpan)(DateTime.Now - _dto.FechaVencimiento)).Days;
            //dto.Total = lst.Sum(x => x.MontoAbono);
            //dto.TotalEfectivo = lst.Where(y => y.IdFormaPago == 1).Sum(x => x.MontoAbono);
            //dto.TotalCheques = lst.Where(y => y.IdFormaPago == 2).Sum(x => x.MontoAbono);
            //dto.TotalTransferencia = lst.Where(y => y.IdFormaPago == 3).Sum(x => x.MontoAbono);
            if (venta != null)
            {
                dto.URL_XML = venta.URLXml;
                dto.URL_CFDI = venta.URLPdf;
            }

            return dto;
        }
        public static List<CargosDTO> ToDTO(List<Cargo> lCargo)
        {
            List<CargosDTO> lprodDTO = lCargo.ToList().Select(x => ToDTO(x)).ToList();
            if (lprodDTO.Count > 10)
            {
                lprodDTO[0].Total = CobranzaServicio.Total(lprodDTO, "T");
                lprodDTO[0].TotalEfectivo = CobranzaServicio.Total(lprodDTO, "TE");
                lprodDTO[0].TotalCheques = CobranzaServicio.Total(lprodDTO, "TC");
                lprodDTO[0].TotalTransferencia = CobranzaServicio.Total(lprodDTO, "TT");
            }
            return lprodDTO;
        }
        public static CargosDTO ToDTOcr(Cargo _dto)
        {
            List<Abono> lst = new AbonosDataAcces().BuscarTodos(_dto.IdCargo);
            var venta = CFDIServicio.Buscar(_dto.Ticket);
            CargosDTO dto = new CargosDTO();
            dto.IdCargo = _dto.IdCargo;
            dto.IdCliente = _dto.IdCliente;
            dto.Cliente = string.Concat(_dto.CCliente.Nombre, " ", _dto.CCliente.Apellido1);
            if (_dto.CCliente.IdTipoPersona.Equals(2))
                dto.Cliente = _dto.CCliente.RazonSocial;
            dto.Rfc = ClienteServicio.Obtener(_dto.IdCliente).Rfc;
            dto.IdEmpresa = _dto.IdEmpresa;
            dto.Ticket = _dto.Ticket;
            dto.FechaRegistro = _dto.FechaRegistro;
            dto.TotalCargo = _dto.TotalCargo;
            dto.TotalAbonos = _dto.TotalAbonos;
            dto.SaldoInsoluto = _dto.TotalCargo - _dto.TotalAbonos;
            dto.VentaExtraordinaria = _dto.VentaExtraordinaria;
            dto.Activo = _dto.Activo;
            dto.FechaVencimiento = _dto.FechaVencimiento;
            dto.Saldada = _dto.Saldada;
            dto.lstCreditoR = ToDTO(lst);
            dto.Abono = FromInit(_dto.IdCargo);
            dto.Dias1a7 = ((TimeSpan)(DateTime.Now - _dto.FechaVencimiento)).Days;
            //dto.Total = lst.Sum(x => x.MontoAbono);
            //dto.TotalEfectivo = lst.Where(y => y.IdFormaPago == 1).Sum(x => x.MontoAbono);
            //dto.TotalCheques = lst.Where(y => y.IdFormaPago == 2).Sum(x => x.MontoAbono);
            //dto.TotalTransferencia = lst.Where(y => y.IdFormaPago == 3).Sum(x => x.MontoAbono);
            if (venta != null)
            {
                dto.URL_XML = venta.URLXml;
                dto.URL_CFDI = venta.URLPdf;
            }

            return dto;
        }
        public static List<AbonosDTO> ToDTOAbono(Cargo _dto)
        {
            List<Abono> lst = new AbonosDataAcces().BuscarTodos(_dto.IdCargo);
            var venta = CFDIServicio.Buscar(_dto.Ticket);
            List<AbonosDTO> dto = new List<AbonosDTO>();
            dto = ToDTO(lst);        
            return dto;
        }
        public static List<AbonosDTO> ToDTOcr(List<Cargo> lCargo)
        {
            List<AbonosDTO> lprodDTO = new List<AbonosDTO>();
            foreach (Cargo c in lCargo)
            {
                lprodDTO.AddRange(ToDTOAbono(c));
            }
          
            return lprodDTO;
        }
        public static ReporteCreditoRecDto ToDTOCR(List<Cargo> lCargo)
        {
            ReporteCreditoRecDto lprodDTO = new ReporteCreditoRecDto();
            lprodDTO.reporteCargos = lCargo.ToList().Select(x => ToDTOcr(x)).ToList();
            lprodDTO.reporteAbonos = ToDTOcr(lCargo);
            if (lprodDTO.reporteCargos.Count > 10)
            {
                lprodDTO.reporteCargos[0].Total = CobranzaServicio.Total(lprodDTO.reporteCargos, "T");
                lprodDTO.reporteCargos[0].TotalEfectivo = CobranzaServicio.Total(lprodDTO.reporteCargos, "TE");
                lprodDTO.reporteCargos[0].TotalCheques = CobranzaServicio.Total(lprodDTO.reporteCargos, "TC");
                lprodDTO.reporteCargos[0].TotalTransferencia = CobranzaServicio.Total(lprodDTO.reporteCargos, "TT");
            }
            return lprodDTO;
        }

        public static CargosDTO ToDTO(CRecuperadaDTO _dto)
        {
            // List<Abono> lst = new AbonosDataAcces().BuscarTodos(_dto.IdCargo);
            CargosDTO dto = new CargosDTO();
            //dto.IdCargo = _dto.IdCargo;
            dto.IdCliente = _dto.IdCliente;
            dto.Cliente = ClienteServicio.Obtener(_dto.IdCliente).RazonSocial;
            dto.Rfc = ClienteServicio.Obtener(_dto.IdCliente).Rfc;
            dto.IdEmpresa = _dto.IdEmpresa;
            dto.Ticket = _dto.Ticket;
            dto.FechaRegistro = _dto.FechaRegistro;
            dto.Nombre = _dto.NombreCliente;
            //dto.TotalCargo = _dto.TotalCargo;
            //dto.TotalAbonos = _dto.TotalAbonos;
            //dto.SaldoInsoluto = _dto.TotalCargo - _dto.TotalAbonos;
            //dto.VentaExtraordinaria = _dto.VentaExtraordinaria;
            dto.Activo = true;//_dto.Activo;
            dto.FechaVencimiento = _dto.FechaRegistro;//_dto.FechaVencimiento;
            dto.Saldada = true;//_dto.Saldada;
            //dto.lstCreditoR = ToDTOAbono(_dto);
            //dto.Abono = FromInit(_dto.IdCargo);
            //dto.Total = lst.Sum(x => x.MontoAbono);
            //dto.TotalEfectivo = lst.Where(y => y.IdFormaPago == 1).Sum(x => x.MontoAbono);
            //dto.TotalCheques = lst.Where(y => y.IdFormaPago == 2).Sum(x => x.MontoAbono);
            //dto.TotalTransferencia = lst.Where(y => y.IdFormaPago == 3).Sum(x => x.MontoAbono);
            dto.URL_CFDI = _dto.Url_PDF;
            dto.URL_XML = _dto.Url_XML;
            return dto;
        }
        public static List<CargosDTO> ToDTO(List<CRecuperadaDTO> lstCRecuperado, List<CRecuperadaTotalesDTO> lstTotal)
        {
            List<CargosDTO> lprodDTO = lstCRecuperado.ToList().Select(x => ToDTO(x)).ToList();
            if (lprodDTO.Count() > 0)
            {
                lprodDTO[0].lstCreditoR = ToDTOAbonos(lstCRecuperado);
                lprodDTO[0].Total = lstTotal[0].Total;//CobranzaServicio.Total(lprodDTO, "T");
                lprodDTO[0].TotalEfectivo = lstTotal[0].Total_Efectivo;//CobranzaServicio.Total(lprodDTO, "TE");
                lprodDTO[0].TotalCheques = lstTotal[0].Total_Cheques;//CobranzaServicio.Total(lprodDTO, "TC");
                lprodDTO[0].TotalTransferencia = lstTotal[0].Total_Transferencia;//CobranzaServicio.Total(lprodDTO, "TT");
            }
            return lprodDTO;
        }
        public static CargosDTO ToDTORep(CarteraVencidaDTO _dto)
        {
            var usu = ClienteServicio.Obtener(_dto.IdCliente);
            CargosDTO dto = new CargosDTO();
            dto.IdCliente = _dto.IdCliente;
            dto.IdEmpresa = _dto.IdEmpresa;
            dto.Cliente = usu.RazonSocial;
            dto.Rfc = usu.Rfc;
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
            return dto;
        }
        public static ReporteDTO ToDTORep(List<CarteraVencidaDTO> lCargoV, List<CarteraVencidaDTO> lCargoT)
        {
            ReporteDTO lprodDTO = new ReporteDTO();
            lprodDTO.reportedet = lCargoV.Select(x => ToDTORep(x)).ToList();
            lprodDTO.reportedet.AddRange(Totalizador(lprodDTO.reportedet));
            lprodDTO.global = Global(lCargoT);
            return lprodDTO;
        }
        public static List<CargosDTO> Totalizador(List<CargosDTO> cargos)
        {
            List<CargosDTO> List = new List<CargosDTO>();
            foreach (var item in cargos.Select(x => x.IdCliente).Distinct())
            {
                List.Add(new CargosDTO()
                {
                    Nombre = cargos.FirstOrDefault(c => c.IdCliente.Equals(item)).Nombre,
                    Ticket = "Total",
                    Serie = "Total",
                    TotalCargo = cargos.Where(c => c.IdCliente.Equals(item)).Sum(x => x.TotalCargo),
                    SaldoActual = cargos.Where(c => c.IdCliente.Equals(item)).Sum(x => x.SaldoActual),
                    SaldoCorriente = cargos.Where(c => c.IdCliente.Equals(item)).Sum(x => x.SaldoCorriente),
                    SaldoVencido = cargos.Where(c => c.IdCliente.Equals(item)).Sum(x => x.SaldoVencido),
                    Dias1a7 = cargos.Where(c => c.IdCliente.Equals(item)).Sum(x => x.Dias1a7),
                    Dias8a16 = cargos.Where(c => c.IdCliente.Equals(item)).Sum(x => x.Dias8a16),
                    Dias17a31 = cargos.Where(c => c.IdCliente.Equals(item)).Sum(x => x.Dias17a31),
                    Dias32a61 = cargos.Where(c => c.IdCliente.Equals(item)).Sum(x => x.Dias32a61),
                    Dias62a91 = cargos.Where(c => c.IdCliente.Equals(item)).Sum(x => x.Dias62a91),
                    Mas91 = cargos.Where(c => c.IdCliente.Equals(item)).Sum(x => x.Mas91)
                });
            }
            return List;

        }
        public static List<CarteraVencidaDTO> Global(List<CarteraVencidaDTO> cargos)
        {
            List<CarteraVencidaDTO> respuesta = new List<CarteraVencidaDTO>();
            respuesta.Add(new CarteraVencidaDTO()
            {
                Nombre = "Total",
                SaldoActualTotal = cargos.Sum(x => x.SaldoActualTotal),
                SaldoCorrienteTotal = cargos.Sum(x => x.SaldoCorrienteTotal),
                SaldoVencidoTotal = cargos.Sum(x => x.SaldoVencidoTotal),
                Dias1_7Total = cargos.Sum(x => x.Dias1_7Total),
                Dias8_16Total = cargos.Sum(x => x.Dias8_16Total),
                Dias17_31Total = cargos.Sum(x => x.Dias17_31Total),
                Dias32_61Total = cargos.Sum(x => x.Dias32_61Total),
                Dias62_91Total = cargos.Sum(x => x.Dias62_91Total),
                Mas91Total = cargos.Sum(x => x.Mas91Total),
            });
            return respuesta;

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
            dto.Id_RelTF = 0;
            return dto;
        }
    }
}
