using MVC.Presentacion.Agente;
using MVC.Presentacion.Models.Historico;
using MVC.Presentacion.Models.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.App_Code
{
    public class HistoricoServicio
    {
        private AgenteServicio agenteServico;
        public HistoricoServicio()
        {
             agenteServico = new AgenteServicio();
        }

        public List<HistoricoVentaModel> GetListaHistoricos(string tkn)
        {
            agenteServico.GetListaHistoricos(tkn);
            return agenteServico._ListHistoricoVenta;
        }

        public RespuestaDTO EliminarHistorico(HistoricoVentaModel dto, string tkn)
        {
            agenteServico.EliminarHistorico(dto,tkn);
            return agenteServico._RespuestaDTO;
        }

        public RespuestaDTO ActualizarHistorico(HistoricoVentaModel dto, string tkn)
        {
            agenteServico.ActualizarHistorico(dto,tkn);
            return agenteServico._RespuestaDTO;
        }

        public RespuestaDTO GuardarNuevoHistorico(List<HistoricoVentaModel> ListaHistorico, string tkn)
        {
            agenteServico.GuardarNuevoHistorico(ListaHistorico, tkn);
            return agenteServico._RespuestaDTO;
        }
    }
}