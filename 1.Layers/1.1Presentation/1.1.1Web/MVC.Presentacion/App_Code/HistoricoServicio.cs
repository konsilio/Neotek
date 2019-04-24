using MVC.Presentacion.Agente;
using MVC.Presentacion.Models.Historico;
using MVC.Presentacion.Models.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.App_Code
{
    public static class HistoricoServicio
    {
       
        public static List<HistoricoVentaModel> GetListaHistoricos(string tkn)
        {
           AgenteServicio agenteServico = new AgenteServicio();
            agenteServico.GetListaHistoricos(tkn);
            return agenteServico._ListHistoricoVenta;
        }


        public static List<YearsDTO> GetYears(string tkn)
        {
            AgenteServicio agenteServico = new AgenteServicio();
            agenteServico.ObtenerElementosDistintos(tkn);
            return agenteServico._ListYears;
        }

        public static string GetJson(HistoricoVentasConsulta modelo , string tkn)
        {
            AgenteServicio agenteServico = new AgenteServicio();
            agenteServico.GetJson(modelo,tkn);
            return agenteServico._json;
        }

        public static List<YearsDTO> GetVentasTotalesxMes(HistoricoVentasConsulta modelo, string tkn)
        {
            AgenteServicio agenteServico = new AgenteServicio();
            agenteServico.GetVentasTotales(modelo, tkn);
            return agenteServico._ListYears;
        }

        public static RespuestaDTO EliminarHistorico(HistoricoVentaModel dto, string tkn)
        {
            AgenteServicio agenteServico = new AgenteServicio();
            agenteServico.EliminarHistorico(dto, tkn);
            return agenteServico._RespuestaDTO;
        }

        public static RespuestaDTO ActualizarHistorico(HistoricoVentaModel dto, string tkn)
        {
            AgenteServicio agenteServico = new AgenteServicio();
            agenteServico.ActualizarHistorico(dto, tkn);
            return agenteServico._RespuestaDTO;
        }

        public static RespuestaDTO GuardarNuevoHistorico(List<HistoricoVentaModel> ListaHistorico, string tkn)
        {
            AgenteServicio agenteServico = new AgenteServicio();
            agenteServico.GuardarNuevoHistorico(ListaHistorico, tkn);
            return agenteServico._RespuestaDTO;
        }
    }
}