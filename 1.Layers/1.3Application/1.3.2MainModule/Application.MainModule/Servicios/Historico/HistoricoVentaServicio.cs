using Application.MainModule.AdaptadoresDTO.Historico;
using Application.MainModule.DTOs;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.Servicios.Catalogos;
using Application.MainModule.Servicios.Seguridad;
using Sagas.MainModule.Entidades;
using Sagas.MainModule.ObjetosValor.Enum;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Historico
{
    public static class HistoricoVentaServicio
    {
        public static RespuestaDto Crear(List<HistoricoVentas> Lista)
        {
            return new HistoricoDataAcces().Insertar(Lista);
        }
        public static RespuestaDto Actualizar(HistoricoVentas entidad)
        {
            return new HistoricoDataAcces().Actualizar(entidad);
        }
        public static List<HistoricoVentas> Buscar()
        {
            return new HistoricoDataAcces().Obtener();
        }
        public static HistoricoVentas Buscar(int id)
        {
            return new HistoricoDataAcces().Obtener(id);
        }
        public static List<HistoricoVentas> Buscar(short id)
        {
            return new HistoricoDataAcces().Obtener();
        }
        public static List<int> ObtenerYears()
        {
            return new HistoricoDataAcces().ObtenerYears();
        }
        public static List<HistoricoVentas> BuscarPorMes(int anio, int mes)
        {
            List<HistoricoVentas> lista = new List<HistoricoVentas>();
            lista.AddRange(new HistoricoDataAcces().ObtenerPorMes(anio, mes));
            if (anio.Equals(DateTime.Now.Year))
            {
                var ventas = PuntoVentaServicio.ObtenerVentasPorPeriodo(TokenServicio.ObtenerIdEmpresa(), new DateTime(anio, mes, 1));
                lista.AddRange(HistoricoVentasAdapter.FromPV(ventas));
            }
            return lista;
        }
        public static List<HistoricoVentas> BuscarPorFiltros(HistoricoConsultaDTO dto)
        {
            List<HistoricoVentas> Lista = new List<HistoricoVentas>();
            if (!dto.Years.Exists(x => x.Year.Equals(DateTime.Now.Year)))            
                dto.Years.Add(new YearDTO() { Year = DateTime.Now.Year, Seleccionar = true });
            
            foreach (YearDTO item in dto.Years)
            {
                int y = item.Year;
                if (item.Seleccionar)
                {
                    if (dto.Enero)
                        Lista.AddRange(BuscarPorMes(y, 1));
                    if (dto.Febrero)
                        Lista.AddRange(BuscarPorMes(y, 2));
                    if (dto.Marzo)
                        Lista.AddRange(BuscarPorMes(y, 3));
                    if (dto.Abril)
                        Lista.AddRange(BuscarPorMes(y, 4));
                    if (dto.Mayo)
                        Lista.AddRange(BuscarPorMes(y, 5));
                    if (dto.Junio)
                        Lista.AddRange(BuscarPorMes(y, 6));
                    if (dto.Julio)
                        Lista.AddRange(BuscarPorMes(y, 7));
                    if (dto.Agosto)
                        Lista.AddRange(BuscarPorMes(y, 8));
                    if (dto.Septiembre)
                        Lista.AddRange(BuscarPorMes(y, 9));
                    if (dto.Octubre)
                        Lista.AddRange(BuscarPorMes(y, 10));
                    if (dto.Noviembre)
                        Lista.AddRange(BuscarPorMes(y, 11));
                    if (dto.Diciembre)
                        Lista.AddRange(BuscarPorMes(y, 12));
                }
            }            
            return Lista;
        }
        public static string TransformarAJason(List<HistoricoVentas> ventas, HistoricoConsultaDTO dto)
        {
            string json = string.Empty;
            if (dto.IdTipoReporte.Equals(TipoReporteHisotricoEnum.General))
            {
                json += JsonServicio.JsonGeneral(ventas, dto);
                json += JsonServicio.KeysLabesColorsGeneral(dto);
            }
            if (dto.IdTipoReporte.Equals(TipoReporteHisotricoEnum.CamionetaVSPipa))
            {
                json += JsonServicio.JsonCamionetasVSPipas(ventas, dto);
                json += JsonServicio.KeysLabesColorsCamVSPip(dto);
            }
            if (dto.IdTipoReporte.Equals(TipoReporteHisotricoEnum.LocalesVSForeaneos))
            {
                json += JsonServicio.JsonLocalesVSForaneos(ventas, dto);
                json += JsonServicio.KeysLabesColorsLocVSFor(dto);
            }
            json += JsonServicio.EstructuraJsonBar();
            return json;
        }
        public static List<YearDTO> VentasTotales(List<HistoricoVentas> ventas, HistoricoConsultaDTO dto)
        {
            List<YearDTO> year = new List<YearDTO>();
            
            foreach (YearDTO year2 in dto.Years)
            {
                YearDTO ynew = new YearDTO();
                ynew = year2;
                int y = year2.Year;
                if (year2.Seleccionar)
                {
                    ynew.MesesVenta = new List<MesVentaDto>();
                    MesVentaDto mes = new MesVentaDto();
                    if (dto.Enero)
                        ynew.MesesVenta.Add(new MesVentaDto() { mes = "Enero", montoTotal = ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(1)).Sum(t => t.MontoVenta) });
                    if (dto.Febrero)
                        ynew.MesesVenta.Add(new MesVentaDto() { mes = "Febrero", montoTotal = ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(2)).Sum(t => t.MontoVenta) });
                    if (dto.Marzo)
                        ynew.MesesVenta.Add(new MesVentaDto() { mes = "Marzo", montoTotal = ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(3)).Sum(t => t.MontoVenta) });
                    if (dto.Abril)
                        ynew.MesesVenta.Add(new MesVentaDto() { mes = "Abril", montoTotal = ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(4)).Sum(t => t.MontoVenta) });
                    if (dto.Mayo)                     
                        ynew.MesesVenta.Add(new MesVentaDto() { mes = "Mayo", montoTotal = ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(5)).Sum(t => t.MontoVenta) });
                    if (dto.Junio)
                        ynew.MesesVenta.Add(new MesVentaDto() { mes = "Junio", montoTotal = ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(6)).Sum(t => t.MontoVenta) });
                    if (dto.Julio)
                        ynew.MesesVenta.Add(new MesVentaDto() { mes = "Julio", montoTotal = ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(7)).Sum(t => t.MontoVenta) });
                    if (dto.Agosto)
                        ynew.MesesVenta.Add(new MesVentaDto() { mes = "Agosto", montoTotal = ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(8)).Sum(t => t.MontoVenta) });
                    if (dto.Septiembre)
                        ynew.MesesVenta.Add(new MesVentaDto() { mes = "Septiembre", montoTotal = ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(9)).Sum(t => t.MontoVenta) });
                    if (dto.Octubre)
                        ynew.MesesVenta.Add(new MesVentaDto() { mes = "Octubre", montoTotal = ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(10)).Sum(t => t.MontoVenta) });
                    if (dto.Noviembre)
                       ynew.MesesVenta.Add(new MesVentaDto() { mes = "Noviembre", montoTotal = ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(11)).Sum(t => t.MontoVenta) });
                    if (dto.Diciembre)
                        ynew.MesesVenta.Add(new MesVentaDto() { mes = "Diciembre", montoTotal = ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(12)).Sum(t => t.MontoVenta) });
                }
                year.Add(ynew);

            }
            return year;
        }
  
    }
}
