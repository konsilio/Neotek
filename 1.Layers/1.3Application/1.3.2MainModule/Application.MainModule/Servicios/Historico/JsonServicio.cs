using Application.MainModule.DTOs;
using Application.MainModule.DTOs.Almacen;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios
{
    public static class JsonServicio
    {
        public static string KeysLabesColorsGeneral(HistoricoConsultaDTO dto)
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
                barColors += "'#757575', ";
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
            keys = keys.TrimEnd(',');
            labels = labels.TrimEnd(',');
            barColors = barColors.TrimEnd(',');
            keys += "],";
            labels += "],";
            barColors += "],";
            return keys + labels + barColors;
        }
        public static string KeysLabesColorsCamVSPip(HistoricoConsultaDTO dto)
        {
            string keys = "ykeys: ['a', 'b'],";
            string labels = "labels: ['Camioneta', 'Pipa'],";
            string barColors = "barColors: ['#757575', '#26c6da'],";

            return keys + labels + barColors;
        }
        public static string KeysLabesColorsLocVSFor(HistoricoConsultaDTO dto)
        {
            string keys = "ykeys: ['a', 'b'],";
            string labels = "labels: ['Local', 'Foranea'],";
            string barColors = "barColors: ['#757575', '#26c6da'],";

            return keys + labels + barColors;
        }
        public static string JsonGeneral(List<HistoricoVentas> ventas, HistoricoConsultaDTO dto)
        {
            string json = "{ data: [ ";
            foreach (YearDTO year in dto.Years)
            {
                int y = year.Year;
                if (year.Seleccionar)
                {
                    //json += "{";
                    //json += string.Concat("{ y: ", "'", year.Year, "'", ",", "a: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(1)).Sum(t => t.MontoVenta).ToString(), "',m:'1'}");
                    if (dto.Enero)
                        json += string.Concat("{ y: ", "'ENE- ", year.Year, "'", ",", "a: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(1)).Sum(t => t.MontoVenta).ToString(), "',m:'1'},");
                    if (dto.Febrero)
                        json += string.Concat("{ y: ", "'FEB- ", year.Year, "'", ",", "b: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(2)).Sum(t => t.MontoVenta).ToString(), "',m:'2'},");
                    if (dto.Marzo)
                        json += string.Concat("{ y: ", "'MAR- ", year.Year, "'", ",", "c: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(3)).Sum(t => t.MontoVenta).ToString(), "',m:'3'},");
                    if (dto.Abril)
                        json += string.Concat("{ y: ", "'ABR- ", year.Year, "'", ",", "d: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(4)).Sum(t => t.MontoVenta).ToString(), "',m:'4'},");
                    if (dto.Mayo)
                        json += string.Concat("{ y: ", "'MAY- ", year.Year, "'", ",", "e: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(5)).Sum(t => t.MontoVenta).ToString(), "',m:'5'},");
                    if (dto.Junio)
                        json += string.Concat("{ y: ", "'JUN- ", year.Year, "'", ",", "f: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(6)).Sum(t => t.MontoVenta).ToString(), "',m:'6'},");
                    if (dto.Julio)
                        json += string.Concat("{ y: ", "'JUL- ", year.Year, "'", ",", "g: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(7)).Sum(t => t.MontoVenta).ToString(), "',m:'7'},");
                    if (dto.Agosto)
                        json += string.Concat("{ y: ", "'AGO- ", year.Year, "'", ",", "h: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(8)).Sum(t => t.MontoVenta).ToString(), "',m:'8'},");
                    if (dto.Septiembre)
                        json += string.Concat("{ y: ", "'SEP- ", year.Year, "'", ",", "i: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(9)).Sum(t => t.MontoVenta).ToString(), "',m:'9'},");
                    if (dto.Octubre)
                        json += string.Concat("{ y: ", "'OCT- ", year.Year, "'", ",", "j: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(10)).Sum(t => t.MontoVenta).ToString(), "',m:'10'},");
                    if (dto.Noviembre)
                        json += string.Concat("{ y: ", "'NOV- ", year.Year, "'", ",", "k: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(11)).Sum(t => t.MontoVenta).ToString(), "',m:'11'},");
                    if (dto.Diciembre)
                        json += string.Concat("{ y: ", "'DIC- ", year.Year, "'", ",", "l: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(12)).Sum(t => t.MontoVenta).ToString(), "',m:'12'},");
                    //json = json.TrimEnd(',');
                    //json += "},";
                }
            }
            json = json.TrimEnd(',');
            return string.Concat(json, "],");
        }
        public static string JsonGeneralRemanente(List<RemanenteGeneralDTO> rema)
        {
            string json = " { type: 'line',";
            json += " data: { labels :[";
            foreach (RemanenteGeneralDTO r in rema)
                json += string.Concat('"', "Día ", r.dia.ToString(), '"', ",");
            json = json.TrimEnd(',');
            json += "], datasets: [{ data: [ ";
            foreach (RemanenteGeneralDTO r in rema)
                json += string.Concat(r.InventarioFisico.ToString(), ",");
            json = json.TrimEnd(',');
            json += string.Concat("],", EstructuraJsonDataSetAAreaChart("Remanente"), "}");
            json += ", { data: [";
            foreach (RemanenteGeneralDTO r in rema)
                json += string.Concat(r.InventarioLibro.ToString(), ",");
            json = json.TrimEnd(',');
            json += string.Concat("],", EstructuraJsonDataSetBAreaChart("Gas en almacenes"), "}]");
            json += JsonOpcionsKilos();
            return json;
        }
        public static string JsonOpcionsKilos()
        {
            string json = "} ,options: { responsive: true, legend: false,";
            json += "scales:{ yAxes:[{ scaleLabel: { display: true, labelString: 'Kilos'} }],";
            return json += "xAxes:[{ scaleLabel: { display: true, labelString: 'Días del mes'} }] } } }";
        }
        public static string JsonCallCenter(List<PedidoDashDTO> lista)
        {
            string json = " { type: 'line',";
            json += " data: { labels :[";
            foreach (var dto in lista)
                json += string.Concat('"', "Día " ,dto.Dia.ToString(), '"', ",");
            json = json.TrimEnd(',');
            json += "], datasets: [{ data: [ ";
            foreach (var dto in lista)
                json += string.Concat(dto.TotalVentas.ToString(), ",");
            json = json.TrimEnd(',');
            json += string.Concat("],", EstructuraJsonDataSetAAreaChart("Generaron venta"), "}");
            json += ", { data: [";
            foreach (var dto in lista)
                json += string.Concat(dto.TotalLlamadas.ToString(), ",");
            json = json.TrimEnd(',');
            json += string.Concat("],", EstructuraJsonDataSetBAreaChart("Total de llamadas"), "}]");
            return string.Concat(json, JsonOpcionsLlamadas());
        }
        public static string JsonOpcionsLlamadas()
        {
            string json = "} ,options: { responsive: true, legend: false,";
            json += "scales:{ yAxes:[{ scaleLabel: { display: true, labelString: 'Llamadas'} }],";
            return json += "xAxes:[{ scaleLabel: { display: true, labelString: 'Días del mes'} }] } } }";
        }
        public static string JsonCamionetasVSPipas(List<HistoricoVentas> ventas, HistoricoConsultaDTO dto)
        {
            string json = "{ data: [ ";
            foreach (YearDTO year in dto.Years)
            {
                int y = year.Year;
                if (year.Seleccionar)
                {
                    if (dto.Enero)
                    {

                        json += string.Concat("{ y: 'ENE- ", year.Year, "',");
                        json += string.Concat("m:'1',");
                        json += string.Concat("a: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(1) && x.EsCamioneta.Equals(true)).Sum(t => t.MontoVenta).ToString(), "', ");
                        json += string.Concat("b: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(1) && x.EsPipa.Equals(true)).Sum(t => t.MontoVenta).ToString(), "'}, ");
                    }
                    if (dto.Febrero)
                    {
                        json += string.Concat("{ y: 'FEB- ", year.Year, "',");
                        json += string.Concat("m:'2',");
                        json += string.Concat("a: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(2) && x.EsCamioneta.Equals(true)).Sum(t => t.MontoVenta).ToString(), "', ");
                        json += string.Concat("b: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(2) && x.EsPipa.Equals(true)).Sum(t => t.MontoVenta).ToString(), "'}, ");
                    }
                    if (dto.Marzo)
                    {
                        json += string.Concat("{ y: 'MAR- ", year.Year, "',");
                        json += string.Concat("m:'3',");
                        json += string.Concat("a: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(3) && x.EsCamioneta.Equals(true)).Sum(t => t.MontoVenta).ToString(), "', ");
                        json += string.Concat("b: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(3) && x.EsPipa.Equals(true)).Sum(t => t.MontoVenta).ToString(), "'}, ");
                    }
                    if (dto.Abril)
                    {
                        json += string.Concat("{ y: 'ABR- ", year.Year, "',");
                        json += string.Concat("m:'4',");
                        json += string.Concat("a: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(4) && x.EsCamioneta.Equals(true)).Sum(t => t.MontoVenta).ToString(), "', ");
                        json += string.Concat("b: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(4) && x.EsPipa.Equals(true)).Sum(t => t.MontoVenta).ToString(), "'}, ");
                    }
                    if (dto.Mayo)
                    {
                        json += string.Concat("{ y: 'MAY- ", year.Year, "',");
                        json += string.Concat("m:'5',");
                        json += string.Concat("a: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(5) && x.EsCamioneta.Equals(true)).Sum(t => t.MontoVenta).ToString(), "', ");
                        json += string.Concat("b: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(5) && x.EsPipa.Equals(true)).Sum(t => t.MontoVenta).ToString(), "'},");
                    }
                    if (dto.Junio)
                    {
                        json += string.Concat("{ y: 'JUN- ", year.Year, "',");
                        json += string.Concat("m:'6',");
                        json += string.Concat("a: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(6) && x.EsCamioneta.Equals(true)).Sum(t => t.MontoVenta).ToString(), "', ");
                        json += string.Concat("b: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(6) && x.EsPipa.Equals(true)).Sum(t => t.MontoVenta).ToString(), "'}, ");
                    }
                    if (dto.Julio)
                    {
                        json += string.Concat("{ y: 'JUL- ", year.Year, "',");
                        json += string.Concat("m:'7',");
                        json += string.Concat("a: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(7) && x.EsCamioneta.Equals(true)).Sum(t => t.MontoVenta).ToString(), "', ");
                        json += string.Concat("b: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(7) && x.EsPipa.Equals(true)).Sum(t => t.MontoVenta).ToString(), "'}, ");
                    }
                    if (dto.Agosto)
                    {
                        json += string.Concat("{ y: 'AGO- ", year.Year, "',");
                        json += string.Concat("m:'8',");
                        json += string.Concat("a: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(8) && x.EsCamioneta.Equals(true)).Sum(t => t.MontoVenta).ToString(), "', ");
                        json += string.Concat("b: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(8) && x.EsPipa.Equals(true)).Sum(t => t.MontoVenta).ToString(), "'}, ");
                    }
                    if (dto.Septiembre)
                    {
                        json += string.Concat("{ y: 'SEP- ", year.Year, "',");
                        json += string.Concat("m:'9',");
                        json += string.Concat("a: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(9) && x.EsCamioneta.Equals(true)).Sum(t => t.MontoVenta).ToString(), "', ");
                        json += string.Concat("b: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(9) && x.EsPipa.Equals(true)).Sum(t => t.MontoVenta).ToString(), "'}, ");
                    }
                    if (dto.Octubre)
                    {
                        json += string.Concat("{ y: 'OCT- ", year.Year, "',");
                        json += string.Concat("m:'10',");
                        json += string.Concat("a: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(10) && x.EsCamioneta.Equals(true)).Sum(t => t.MontoVenta).ToString(), "', ");
                        json += string.Concat("b: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(10) && x.EsPipa.Equals(true)).Sum(t => t.MontoVenta).ToString(), "'}, ");
                    }
                    if (dto.Noviembre)
                    {
                        json += string.Concat("{ y: 'NOV- ", year.Year, "',");
                        json += string.Concat("m:'11',");
                        json += string.Concat("a: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(11) && x.EsCamioneta.Equals(true)).Sum(t => t.MontoVenta).ToString(), "', ");
                        json += string.Concat("b: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(11) && x.EsPipa.Equals(true)).Sum(t => t.MontoVenta).ToString(), "'}, ");
                    }
                    if (dto.Diciembre)
                    {
                        json += string.Concat("{ y: 'DIC- ", year.Year, "',");
                        json += string.Concat("m:'12',");
                        json += string.Concat("a: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(12) && x.EsCamioneta.Equals(true)).Sum(t => t.MontoVenta).ToString(), "', ");
                        json += string.Concat("b: '", ventas.Where(x => x.Anio.Equals(y) && x.Mes.Equals(12) && x.EsPipa.Equals(true)).Sum(t => t.MontoVenta).ToString(), "'}, ");
                    }
                }
            }
            json = json.TrimEnd(',');
            return string.Concat(json, "],");
        }
        public static string JsonLocalesVSForaneos(List<HistoricoVentas> ventas, HistoricoConsultaDTO dto)
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
                        json += string.Concat("a: '", ventas.Where(x => x.EsCamioneta && x.Anio.Equals(y) && x.Mes.Equals(1) && x.EsLocal.Equals(true)).Sum(t => t.MontoVenta).ToString(), "', ");
                        json += string.Concat("b: '", ventas.Where(x => x.EsCamioneta && x.Anio.Equals(y) && x.Mes.Equals(1) && x.EsLocal.Equals(false)).Sum(t => t.MontoVenta).ToString(), "'}, ");
                    }
                    if (dto.Febrero)
                    {
                        json += string.Concat("{ y: 'Febrero ", year.Year, "',");
                        json += string.Concat("a: '", ventas.Where(x => x.EsCamioneta && x.Anio.Equals(y) && x.Mes.Equals(2) && x.EsLocal.Equals(true)).Sum(t => t.MontoVenta).ToString(), "', ");
                        json += string.Concat("b: '", ventas.Where(x => x.EsCamioneta && x.Anio.Equals(y) && x.Mes.Equals(2) && x.EsLocal.Equals(false)).Sum(t => t.MontoVenta).ToString(), "'}, ");
                    }
                    if (dto.Marzo)
                    {
                        json += string.Concat("{ y: 'Marzo ", year.Year, "',");
                        json += string.Concat("a: '", ventas.Where(x => x.EsCamioneta && x.Anio.Equals(y) && x.Mes.Equals(3) && x.EsLocal.Equals(true)).Sum(t => t.MontoVenta).ToString(), "', ");
                        json += string.Concat("b: '", ventas.Where(x => x.EsCamioneta && x.Anio.Equals(y) && x.Mes.Equals(3) && x.EsLocal.Equals(false)).Sum(t => t.MontoVenta).ToString(), "'}, ");
                    }
                    if (dto.Abril)
                    {
                        json += string.Concat("{ y: 'Abril ", year.Year, "',");
                        json += string.Concat("a: '", ventas.Where(x => x.EsCamioneta && x.Anio.Equals(y) && x.Mes.Equals(4) && x.EsLocal.Equals(true)).Sum(t => t.MontoVenta).ToString(), "', ");
                        json += string.Concat("b: '", ventas.Where(x => x.EsCamioneta && x.Anio.Equals(y) && x.Mes.Equals(4) && x.EsLocal.Equals(false)).Sum(t => t.MontoVenta).ToString(), "'}, ");
                    }
                    if (dto.Mayo)
                    {
                        json += string.Concat("{ y: 'Mayo ", year.Year, "',");
                        json += string.Concat("a: '", ventas.Where(x => x.EsCamioneta && x.Anio.Equals(y) && x.Mes.Equals(5) && x.EsLocal.Equals(true)).Sum(t => t.MontoVenta).ToString(), "', ");
                        json += string.Concat("b: '", ventas.Where(x => x.EsCamioneta && x.Anio.Equals(y) && x.Mes.Equals(5) && x.EsLocal.Equals(false)).Sum(t => t.MontoVenta).ToString(), "'}, ");
                    }
                    if (dto.Junio)
                    {
                        json += string.Concat("{ y: 'Junio ", year.Year, "',");
                        json += string.Concat("a: '", ventas.Where(x => x.EsCamioneta && x.Anio.Equals(y) && x.Mes.Equals(6) && x.EsLocal.Equals(true)).Sum(t => t.MontoVenta).ToString(), "', ");
                        json += string.Concat("b: '", ventas.Where(x => x.EsCamioneta && x.Anio.Equals(y) && x.Mes.Equals(6) && x.EsLocal.Equals(false)).Sum(t => t.MontoVenta).ToString(), "'}, ");
                    }
                    if (dto.Julio)
                    {
                        json += string.Concat("{ y: 'Julio ", year.Year, "',");
                        json += string.Concat("a: '", ventas.Where(x => x.EsCamioneta && x.Anio.Equals(y) && x.Mes.Equals(7) && x.EsLocal.Equals(true)).Sum(t => t.MontoVenta).ToString(), "', ");
                        json += string.Concat("b: '", ventas.Where(x => x.EsCamioneta && x.Anio.Equals(y) && x.Mes.Equals(7) && x.EsLocal.Equals(false)).Sum(t => t.MontoVenta).ToString(), "'}, ");
                    }
                    if (dto.Agosto)
                    {
                        json += string.Concat("{ y: 'Agosto ", year.Year, "',");
                        json += string.Concat("a: '", ventas.Where(x => x.EsCamioneta && x.Anio.Equals(y) && x.Mes.Equals(8) && x.EsLocal.Equals(true)).Sum(t => t.MontoVenta).ToString(), "', ");
                        json += string.Concat("b: '", ventas.Where(x => x.EsCamioneta && x.Anio.Equals(y) && x.Mes.Equals(8) && x.EsLocal.Equals(false)).Sum(t => t.MontoVenta).ToString(), "'}, ");
                    }
                    if (dto.Septiembre)
                    {
                        json += string.Concat("{ y: 'Septiembre ", year.Year, "',");
                        json += string.Concat("a: '", ventas.Where(x => x.EsCamioneta && x.Anio.Equals(y) && x.Mes.Equals(9) && x.EsLocal.Equals(true)).Sum(t => t.MontoVenta).ToString(), "', ");
                        json += string.Concat("b: '", ventas.Where(x => x.EsCamioneta && x.Anio.Equals(y) && x.Mes.Equals(9) && x.EsLocal.Equals(false)).Sum(t => t.MontoVenta).ToString(), "'}, ");
                    }
                    if (dto.Octubre)
                    {
                        json += string.Concat("{ y: 'Octubre ", year.Year, "',");
                        json += string.Concat("a: '", ventas.Where(x => x.EsCamioneta && x.Anio.Equals(y) && x.Mes.Equals(10) && x.EsLocal.Equals(true)).Sum(t => t.MontoVenta).ToString(), "', ");
                        json += string.Concat("b: '", ventas.Where(x => x.EsCamioneta && x.Anio.Equals(y) && x.Mes.Equals(10) && x.EsLocal.Equals(false)).Sum(t => t.MontoVenta).ToString(), "'}, ");
                    }
                    if (dto.Noviembre)
                    {
                        json += string.Concat("{ y: 'Noviembre ", year.Year, "',");
                        json += string.Concat("a: '", ventas.Where(x => x.EsCamioneta && x.Anio.Equals(y) && x.Mes.Equals(11) && x.EsLocal.Equals(true)).Sum(t => t.MontoVenta).ToString(), "', ");
                        json += string.Concat("b: '", ventas.Where(x => x.EsCamioneta && x.Anio.Equals(y) && x.Mes.Equals(11) && x.EsLocal.Equals(false)).Sum(t => t.MontoVenta).ToString(), "'}, ");
                    }
                    if (dto.Diciembre)
                    {
                        json += string.Concat("{ y: 'Diciembre ", year.Year, "',");
                        json += string.Concat("a: '", ventas.Where(x => x.EsCamioneta && x.Anio.Equals(y) && x.Mes.Equals(12) && x.EsLocal.Equals(true)).Sum(t => t.MontoVenta).ToString(), "', ");
                        json += string.Concat("b: '", ventas.Where(x => x.EsCamioneta && x.Anio.Equals(y) && x.Mes.Equals(12) && x.EsLocal.Equals(false)).Sum(t => t.MontoVenta).ToString(), "'}, ");
                    }
                }
            }
            json = json.TrimEnd(',');
            return string.Concat(json, "],");
        }
        public static string EstructuraJsonArea()
        {
            string estruc = "element: 'm_area_Remanente',";
            estruc += "xkey: 'day',";
            estruc += "ykeys: ['a', 'b'],";
            estruc += "labels: ['Rema', 'Venta'],";
            estruc += "pointSize: 3,";
            estruc += "fillOpacity: 0,";
            estruc += "pointStrokeColors: ['#222222', '#cccccc'],";
            estruc += "behaveLikeLine: 'true',";
            estruc += "hideHover: 'auto',";
            estruc += "gridLineColor: '#e0e0e0',";
            estruc += "lineWidth: 2,";
            estruc += "lineColors: ['#222222', '#cccccc'],";
            estruc += "resize: 'true' }";
            return estruc;
        }
        public static string EstructuraJsonDataSetAAreaChart(string Label)
        {
            string estruc = string.Concat(" label: ", '"', Label, '"', ",");
            estruc += "borderColor: 'rgba(241,95,121, 0.2)',";
            estruc += "backgroundColor: 'rgba(241,95,121, 0.5)',";
            estruc += "pointBorderColor: 'rgba(241,95,121, 0.3)',";
            estruc += "pointBackgroundColor: 'rgba(241,95,121, 0.2)',";
            estruc += "pointBorderWidth: 1";
            return estruc;
        }
        public static string EstructuraJsonDataSetBAreaChart(string Label)
        {
            string estruc = string.Concat("label: ", '"', Label, '"', ",");
            estruc += "borderColor: 'rgba(140,147,154, 0.2)',";
            estruc += "backgroundColor: 'rgba(140,147,154, 0.2)',";
            estruc += "pointBorderColor: 'rgba(140,147,154, 0)',";
            estruc += "pointBackgroundColor: 'rgba(140,147,154, 0.9)',";
            estruc += "pointBorderWidth: 1";
            return estruc;
        }
        public static string EstructuraJsonBar()
        {
            string estruc = "element: 'm_bar_chart',";
            estruc += "xkey: 'y',";
            estruc += "hideHover: 'auto',";
            estruc += "gridLineColor: '#eef0f2',";
            estruc += "resize: 'true' }";

            return estruc;
        }
    }
}
