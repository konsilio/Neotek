using MVC.Presentacion.Agente;
using MVC.Presentacion.Models.Seguridad;
using MVC.Presentacion.Models.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.App_Code
{
    public static class VentasServicio
    {
        #region Caja General
        public static List<CajaGeneralModel> ListaVentasCajaGral(string token, string type)
        {
            var agente = new AgenteServicio();
            agente.BuscarListaVentaCajaGral(token, type);
            return agente._listaCajaGral;
        }

        public static List<CajaGeneralModel> ListaVentasCajaGralId(short id, string token)
        {
            var agente = new AgenteServicio();
            agente.BuscarListaVentaCajaGralIdE(id,token);
            return agente._listaCajaGral;
        }     

        public static List<CajaGeneralCamionetaModel> ListaVentasCajaGralCamioneta(string cveReporte, string token)
        {
            var agente = new AgenteServicio();
            agente.BuscarListaCajaGralCamioneta(cveReporte, token);
            return agente._listaCajaGralCamioneta;
        }
        public static List<VentaCorteAnticipoModel> ListaVentasCajaGralEstacion(string cveReporte, string token)
        {
            var agente = new AgenteServicio();
            agente.BuscarListaCajaGralEstacion(cveReporte, token);
            return agente._listaCajaGralEstacion;
        }

        public static RespuestaDTO CrearGuardarLiquidacion(CajaGeneralCamionetaModel cc, string tkn)
        {
            var agente = new AgenteServicio();
            agente.GuardarLiquidacion(cc, tkn);
            return agente._RespuestaDTO;
        }

        public static RespuestaDTO GuardarLiquidacionEstacion(VentaCorteAnticipoModel cc, string tkn)
        {
            var agente = new AgenteServicio();
            agente.GuardarLiquidacionEst(cc, tkn);
            return agente._RespuestaDTO;
        }

        #endregion
    }
}