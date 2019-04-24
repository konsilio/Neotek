﻿using Application.MainModule.DTOs;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.AccesoADatos;
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
    class HistoricoVentaServicio
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
        public static List<int> ObtenerElementosDistintos(short id)
        {
            return new HistoricoDataAcces().ObtenerElementosDistintos();
        }
        public static List<HistoricoVentas> BuscarPorMes(int anio, int mes)
        {
            return new HistoricoDataAcces().ObtenerPorMes(anio, mes);
        }
        public static List<HistoricoVentas> BuscarPorFiltros(HistoricoConsultaDTO dto)
        {
            List<HistoricoVentas> Lista = new List<HistoricoVentas>();
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
                json += JsonGeneral(ventas, dto);
                json += KeysLabesColorsGeneral(dto);
            }
            if (dto.IdTipoReporte.Equals(TipoReporteHisotricoEnum.CamionetaVSPipa))
            {
                json += JsonCamionetasVSPipas(ventas, dto);
                json += KeysLabesColorsCamVSPip(dto);
            }
            if (dto.IdTipoReporte.Equals(TipoReporteHisotricoEnum.LocalesVSForeaneos))
            {
                json += JsonLocalesVSForaneos(ventas, dto);
                json += KeysLabesColorsLocVSFor(dto);
            }
            json += EstructuraJson();
            return json;
        }
        private static string EstructuraJson()
        {
            string estruc = "element: 'm_bar_chart',";
            estruc += "xkey: 'y',";
            estruc += "hideHover: 'auto',";
            estruc += "gridLineColor: '#eef0f2',";
            estruc += "resize: 'true' }";

            return estruc;
        }
        private static string KeysLabesColorsGeneral(HistoricoConsultaDTO dto)
        {
            string keys = "ykeys: [";
            string labels = "labels: [";
            string barColors = "barColors: [";

            if (dto.Enero)
            {
                keys += "'a',";
                labels += "'Enero', ";
                barColors += "'#F44336', ";
            }
            if (dto.Febrero)
            {
                keys += "'b',";
                labels += " 'Febrero', ";
                barColors += "'#3F51B5', ";
            }
            if (dto.Marzo)
            {
                keys += "'c',";
                labels += "'Marzo', ";
                barColors += "'#009688', ";
            }
            if (dto.Abril)
            {
                keys += "'d',";
                labels += "'Abril', ";
                barColors += "'#9E9E9E', ";
            }
            if (dto.Mayo)
            {
                keys += "'e',";
                labels += "'Mayo', ";
                barColors += "'#9E9E9E', ";
            }
            if (dto.Junio)
            {
                keys += "'f',";
                labels += "'Junio', ";
                barColors += "'#FF5722', ";
            }
            if (dto.Julio)
            {
                keys += "'g',";
                labels += "'Julio', ";
                barColors += "'#795548', ";
            }
            if (dto.Agosto)
            {
                keys += "'h',";
                labels += "'Agosto', ";
                barColors += "'#673AB7', ";
            }
            if (dto.Septiembre)
            {
                keys += "'i',";
                labels += "'Septiembre', ";
                barColors += "'#607D8B', ";
            }
            if (dto.Octubre)
            {
                keys += "'j',";
                labels += "'Octubre', ";
                barColors += "''], ";
            }
            if (dto.Noviembre)
            {
                keys += "'k',";
                labels += "'Noviembre', ";
                barColors += "'#00BCD4', ";
            }
            if (dto.Diciembre)
            {
                keys += "'l',";
                labels += "'Diciembre',";
                barColors += "'#03A9F4',";
            }
            keys += "],";
            labels += "],";
            barColors += "],";
            return keys + labels + barColors;
        }
        private static string KeysLabesColorsCamVSPip(HistoricoConsultaDTO dto)
        {
            string keys = "ykeys: ['a', 'b'],";
            string labels = "labels: ['Camioneta', 'Pipa'],";
            string barColors = "barColors: ['#757575', '#26c6da'],";

            return keys + labels + barColors;
        }
        private static string KeysLabesColorsLocVSFor(HistoricoConsultaDTO dto)
        {
            string keys = "ykeys: ['a', 'b'],";
            string labels = "labels: ['Local', 'Foranea'],";
            string barColors = "barColors: ['#757575', '#26c6da'],";

            return keys + labels + barColors;
        }
        private static string JsonGeneral(List<HistoricoVentas> ventas, HistoricoConsultaDTO dto)
        {
            string json = "{ data: [ ";
            foreach (YearDTO year in dto.Years)
            {
                int y = year.Year;
                if (year.Seleccionar)
                {
                    json += "{";
                    json += string.Concat("y: ", "'", year.Year, "'", ",");
                    if (dto.Enero)
                        json += string.Concat("a: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(1)).Sum(t => t.MontoVenta).ToString(), "', ");
                    if (dto.Febrero)
                        json += string.Concat("b: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(2)).Sum(t => t.MontoVenta).ToString(), "', ");
                    if (dto.Marzo)
                        json += string.Concat("c: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(3)).Sum(t => t.MontoVenta).ToString(), "', ");
                    if (dto.Abril)
                        json += string.Concat("d: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(4)).Sum(t => t.MontoVenta).ToString(), "', ");
                    if (dto.Mayo)
                        json += string.Concat("e: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(5)).Sum(t => t.MontoVenta).ToString(), "', ");
                    if (dto.Junio)
                        json += string.Concat("f: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(6)).Sum(t => t.MontoVenta).ToString(), "', ");
                    if (dto.Julio)
                        json += string.Concat("g: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(7)).Sum(t => t.MontoVenta).ToString(), "', ");
                    if (dto.Agosto)
                        json += string.Concat("h: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(8)).Sum(t => t.MontoVenta).ToString(), "', ");
                    if (dto.Septiembre)
                        json += string.Concat("i: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(9)).Sum(t => t.MontoVenta).ToString(), "', ");
                    if (dto.Octubre)
                        json += string.Concat("j: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(10)).Sum(t => t.MontoVenta).ToString(), "', ");
                    if (dto.Noviembre)
                        json += string.Concat("k: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(11)).Sum(t => t.MontoVenta).ToString(), "', ");
                    if (dto.Diciembre)
                        json += string.Concat("l: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(12)).Sum(t => t.MontoVenta).ToString(), "', ");

                    json += "},";
                }

            }
            json = json.TrimEnd(',');
            return string.Concat(json, "],");
        }
        public static List<YearDTO> VentasTotales(List<HistoricoVentas> ventas, HistoricoConsultaDTO dto2)
        {
            List<YearDTO> year = new List<YearDTO>();
            
            foreach (YearDTO year2 in dto2.Years)
            {
                YearDTO ynew = new YearDTO();
                ynew = year2;
                int y = year2.Year;
                if (year2.Seleccionar)
                {
                    ynew.MesesVenta = new List<MesVentaDto>();
                    MesVentaDto mes = new MesVentaDto();
                    if (dto2.Enero)
                        ynew.MesesVenta.Add(new MesVentaDto() { mes = "Enero", montoTotal = ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(1)).Sum(t => t.MontoVenta) });
                    if (dto2.Febrero)
                        ynew.MesesVenta.Add(new MesVentaDto() { mes = "Febrero", montoTotal = ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(2)).Sum(t => t.MontoVenta) });
                    if (dto2.Marzo)
                        ynew.MesesVenta.Add(new MesVentaDto() { mes = "Marzo", montoTotal = ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(3)).Sum(t => t.MontoVenta) });
                    if (dto2.Abril)
                        ynew.MesesVenta.Add(new MesVentaDto() { mes = "Abril", montoTotal = ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(4)).Sum(t => t.MontoVenta) });
                    if (dto2.Mayo)                     
                        ynew.MesesVenta.Add(new MesVentaDto() { mes = "Mayo", montoTotal = ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(5)).Sum(t => t.MontoVenta) });
                    if (dto2.Junio)
                        ynew.MesesVenta.Add(new MesVentaDto() { mes = "Junio", montoTotal = ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(6)).Sum(t => t.MontoVenta) });
                    if (dto2.Julio)
                        ynew.MesesVenta.Add(new MesVentaDto() { mes = "Julio", montoTotal = ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(7)).Sum(t => t.MontoVenta) });
                    if (dto2.Agosto)
                        ynew.MesesVenta.Add(new MesVentaDto() { mes = "Agosto", montoTotal = ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(8)).Sum(t => t.MontoVenta) });
                    if (dto2.Septiembre)
                        ynew.MesesVenta.Add(new MesVentaDto() { mes = "Septiembre", montoTotal = ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(9)).Sum(t => t.MontoVenta) });
                    if (dto2.Octubre)
                        ynew.MesesVenta.Add(new MesVentaDto() { mes = "Octubre", montoTotal = ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(10)).Sum(t => t.MontoVenta) });
                    if (dto2.Noviembre)
                       ynew.MesesVenta.Add(new MesVentaDto() { mes = "Noviembre", montoTotal = ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(11)).Sum(t => t.MontoVenta) });
                    if (dto2.Diciembre)
                        ynew.MesesVenta.Add(new MesVentaDto() { mes = "Diciembre", montoTotal = ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(12)).Sum(t => t.MontoVenta) });
                }
                year.Add(ynew);

            }
            return year;
        }
        private static string JsonCamionetasVSPipas(List<HistoricoVentas> ventas, HistoricoConsultaDTO dto)
        {
            string json = "{ data: [ ";
            foreach (YearDTO year in dto.Years)
            {
                int y = year.Year;
                if (year.Seleccionar)
                {
                    if (dto.Enero)
                    {

                        json += string.Concat("{ y: 'Enero ", year.Year, "',");
                        json += string.Concat("a: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(1) && x.EsCamioneta.Equals(true)).Sum(t => t.MontoVenta).ToString(), "', ");
                        json += string.Concat("b: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(1) && x.EsPipa.Equals(true)).Sum(t => t.MontoVenta).ToString(), "'}, ");
                    }
                    if (dto.Febrero)
                    {
                        json += string.Concat("{ y: 'Febrero ", year.Year, "',");
                        json += string.Concat("a: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(2) && x.EsCamioneta.Equals(true)).Sum(t => t.MontoVenta).ToString(), "', ");
                        json += string.Concat("b: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(2) && x.EsPipa.Equals(true)).Sum(t => t.MontoVenta).ToString(), "'}, ");
                    }
                    if (dto.Marzo)
                    {
                        json += string.Concat("{ y: 'Marzo ", year.Year, "',");
                        json += string.Concat("a: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(3) && x.EsCamioneta.Equals(true)).Sum(t => t.MontoVenta).ToString(), "', ");
                        json += string.Concat("b: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(3) && x.EsPipa.Equals(true)).Sum(t => t.MontoVenta).ToString(), "'}, ");
                    }
                    if (dto.Abril)
                    {
                        json += string.Concat("{ y: 'Abril ", year.Year, "',");
                        json += string.Concat("a: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(4) && x.EsCamioneta.Equals(true)).Sum(t => t.MontoVenta).ToString(), "', ");
                        json += string.Concat("b: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(4) && x.EsPipa.Equals(true)).Sum(t => t.MontoVenta).ToString(), "'}, ");
                    }
                    if (dto.Mayo)
                    {
                        json += string.Concat("{ y: 'Mayo ", year.Year, "',");
                        json += string.Concat("a: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(5) && x.EsCamioneta.Equals(true)).Sum(t => t.MontoVenta).ToString(), "', ");
                        json += string.Concat("b: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(5) && x.EsPipa.Equals(true)).Sum(t => t.MontoVenta).ToString(), "'},");
                    }
                    if (dto.Junio)
                    {
                        json += string.Concat("{ y: 'Junio ", year.Year, "',");
                        json += string.Concat("a: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(6) && x.EsCamioneta.Equals(true)).Sum(t => t.MontoVenta).ToString(), "', ");
                        json += string.Concat("b: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(6) && x.EsPipa.Equals(true)).Sum(t => t.MontoVenta).ToString(), "'}, ");
                    }
                    if (dto.Julio)
                    {
                        json += string.Concat("{ y: 'Julio ", year.Year, "',");
                        json += string.Concat("a: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(7) && x.EsCamioneta.Equals(true)).Sum(t => t.MontoVenta).ToString(), "', ");
                        json += string.Concat("b: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(7) && x.EsPipa.Equals(true)).Sum(t => t.MontoVenta).ToString(), "'}, ");
                    }
                    if (dto.Agosto)
                    {
                        json += string.Concat("{ y: 'Agosto ", year.Year, "',");
                        json += string.Concat("a: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(8) && x.EsCamioneta.Equals(true)).Sum(t => t.MontoVenta).ToString(), "', ");
                        json += string.Concat("b: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(8) && x.EsPipa.Equals(true)).Sum(t => t.MontoVenta).ToString(), "'}, ");
                    }
                    if (dto.Septiembre)
                    {
                        json += string.Concat("{ y: 'Septiembre ", year.Year, "',");
                        json += string.Concat("a: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(9) && x.EsCamioneta.Equals(true)).Sum(t => t.MontoVenta).ToString(), "', ");
                        json += string.Concat("b: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(9) && x.EsPipa.Equals(true)).Sum(t => t.MontoVenta).ToString(), "'}, ");
                    }
                    if (dto.Octubre)
                    {
                        json += string.Concat("{ y: 'Octubre ", year.Year, "',");
                        json += string.Concat("a: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(10) && x.EsCamioneta.Equals(true)).Sum(t => t.MontoVenta).ToString(), "', ");
                        json += string.Concat("b: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(10) && x.EsPipa.Equals(true)).Sum(t => t.MontoVenta).ToString(), "'}, ");
                    }
                    if (dto.Noviembre)
                    {
                        json += string.Concat("{ y: 'Noviembre ", year.Year, "',");
                        json += string.Concat("a: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(11) && x.EsCamioneta.Equals(true)).Sum(t => t.MontoVenta).ToString(), "', ");
                        json += string.Concat("b: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(11) && x.EsPipa.Equals(true)).Sum(t => t.MontoVenta).ToString(), "'}, ");
                    }
                    if (dto.Diciembre)
                    {
                        json += string.Concat("{ y: 'Diciembre ", year.Year, "',");
                        json += string.Concat("a: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(12) && x.EsCamioneta.Equals(true)).Sum(t => t.MontoVenta).ToString(), "', ");
                        json += string.Concat("b: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(12) && x.EsPipa.Equals(true)).Sum(t => t.MontoVenta).ToString(), "'}, ");
                    }
                }
            }
            json = json.TrimEnd(',');
            return string.Concat(json, "],");
        }
        private static string JsonLocalesVSForaneos(List<HistoricoVentas> ventas, HistoricoConsultaDTO dto)
        {
            string json = "{ data: [ ";
            foreach (YearDTO year in dto.Years)
            {
                int y = year.Year;
                if (year.Seleccionar)
                {
                    if (dto.Enero)
                    {
                        json += string.Concat("{ y: 'Enero ", year.Year, "',");
                        json += string.Concat("a: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(1) && x.EsLocal.Equals(true)).Sum(t => t.MontoVenta).ToString(), "', ");
                        json += string.Concat("b: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(1) && x.EsLocal.Equals(false)).Sum(t => t.MontoVenta).ToString(), "'}, ");
                    }
                    if (dto.Febrero)
                    {
                        json += string.Concat("{ y: 'Febrero ", year.Year, "',");
                        json += string.Concat("a: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(2) && x.EsLocal.Equals(true)).Sum(t => t.MontoVenta).ToString(), "', ");
                        json += string.Concat("b: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(2) && x.EsLocal.Equals(false)).Sum(t => t.MontoVenta).ToString(), "'}, ");
                    }
                    if (dto.Marzo)
                    {
                        json += string.Concat("{ y: 'Marzo ", year.Year, "',");
                        json += string.Concat("a: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(3) && x.EsLocal.Equals(true)).Sum(t => t.MontoVenta).ToString(), "', ");
                        json += string.Concat("b: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(3) && x.EsLocal.Equals(false)).Sum(t => t.MontoVenta).ToString(), "'}, ");
                    }
                    if (dto.Abril)
                    {
                        json += string.Concat("{ y: 'Abril ", year.Year, "',");
                        json += string.Concat("a: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(4) && x.EsLocal.Equals(true)).Sum(t => t.MontoVenta).ToString(), "', ");
                        json += string.Concat("b: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(4) && x.EsLocal.Equals(false)).Sum(t => t.MontoVenta).ToString(), "'}, ");
                    }
                    if (dto.Mayo)
                    {
                        json += string.Concat("{ y: 'Mayo ", year.Year, "',");
                        json += string.Concat("a: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(5) && x.EsLocal.Equals(true)).Sum(t => t.MontoVenta).ToString(), "', ");
                        json += string.Concat("b: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(5) && x.EsLocal.Equals(false)).Sum(t => t.MontoVenta).ToString(), "'}, ");
                    }
                    if (dto.Junio)
                    {
                        json += string.Concat("{ y: 'Junio ", year.Year, "',");
                        json += string.Concat("a: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(6) && x.EsLocal.Equals(true)).Sum(t => t.MontoVenta).ToString(), "', ");
                        json += string.Concat("b: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(6) && x.EsLocal.Equals(false)).Sum(t => t.MontoVenta).ToString(), "'}, ");
                    }
                    if (dto.Julio)
                    {
                        json += string.Concat("{ y: 'Julio ", year.Year, "',");
                        json += string.Concat("a: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(7) && x.EsLocal.Equals(true)).Sum(t => t.MontoVenta).ToString(), "', ");
                        json += string.Concat("b: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(7) && x.EsLocal.Equals(false)).Sum(t => t.MontoVenta).ToString(), "'}, ");
                    }
                    if (dto.Agosto)
                    {
                        json += string.Concat("{ y: 'Agosto ", year.Year, "',");
                        json += string.Concat("a: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(8) && x.EsLocal.Equals(true)).Sum(t => t.MontoVenta).ToString(), "', ");
                        json += string.Concat("b: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(8) && x.EsLocal.Equals(false)).Sum(t => t.MontoVenta).ToString(), "'}, ");
                    }
                    if (dto.Septiembre)
                    {
                        json += string.Concat("{ y: 'Septiembre ", year.Year, "',");
                        json += string.Concat("a: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(9) && x.EsLocal.Equals(true)).Sum(t => t.MontoVenta).ToString(), "', ");
                        json += string.Concat("b: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(9) && x.EsLocal.Equals(false)).Sum(t => t.MontoVenta).ToString(), "'}, ");
                    }
                    if (dto.Octubre)
                    {
                        json += string.Concat("{ y: 'Octubre ", year.Year, "',");
                        json += string.Concat("a: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(10) && x.EsLocal.Equals(true)).Sum(t => t.MontoVenta).ToString(), "', ");
                        json += string.Concat("b: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(10) && x.EsLocal.Equals(false)).Sum(t => t.MontoVenta).ToString(), "'}, ");
                    }
                    if (dto.Noviembre)
                    {
                        json += string.Concat("{ y: 'Noviembre ", year.Year, "',");
                        json += string.Concat("a: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(11) && x.EsLocal.Equals(true)).Sum(t => t.MontoVenta).ToString(), "', ");
                        json += string.Concat("b: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(11) && x.EsLocal.Equals(false)).Sum(t => t.MontoVenta).ToString(), "'}, ");
                    }
                    if (dto.Diciembre)
                    {
                        json += string.Concat("{ y: 'Diciembre ", year.Year, "',");
                        json += string.Concat("a: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(12) && x.EsLocal.Equals(true)).Sum(t => t.MontoVenta).ToString(), "', ");
                        json += string.Concat("b: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(12) && x.EsLocal.Equals(false)).Sum(t => t.MontoVenta).ToString(), "'}, ");
                    }
                }
            }
            json = json.TrimEnd(',');
            return string.Concat(json, "],");
        }
    }
}