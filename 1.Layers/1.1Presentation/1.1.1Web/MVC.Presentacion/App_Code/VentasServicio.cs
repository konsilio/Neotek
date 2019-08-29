using MVC.Presentacion.Agente;
using MVC.Presentacion.Models.Catalogos;
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
            agente.BuscarListaVentaCajaGralIdE(id, token);
            return agente._listaCajaGral;
        }

        public static CorteCajaDTO ListaVentasCajaGralCamioneta(string cveReporte, string token)
        {
            var agente = new AgenteServicio();
            agente.BuscarListaCajaGralCamioneta(cveReporte, token);
            return agente._CorteCaja;
        }
        public static List<MovimientosGasModel> ListaVentasMovimientosGas(CajaGeneralCamionetaModel reporte, string token)
        {
            var agente = new AgenteServicio();
            agente.BuscarListaMovGas(reporte, token);
            return agente._ListaMovimientosGas;
        }
        public static MovimientosGasCilindros List(CajaGeneralCamionetaModel rep, MovimientosGasCilindros m)
        {
            MovimientosGasCilindros entiti = new MovimientosGasCilindros();
            entiti.IdEmpresa = rep.IdEmpresa;
            entiti.Year = rep.Year;
            entiti.Mes = rep.Mes;
            entiti.Dia = rep.Dia;
            entiti.Orden = rep.Orden;

            return entiti;
        }
        public static List<MovimientosGasCilindros> ListaVentasMovimientosGasC(CajaGeneralCamionetaModel reporte, string token)
        {
            MovimientosGasCilindros m = new MovimientosGasCilindros();
            MovimientosGasCilindros model = List(reporte, m);

            var agente = new AgenteServicio();
            agente.BuscarListaMovGasCilindros(model, token);
            return agente._ListaMovimientosGasC;
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
        public static RespuestaDTO GuardarLiquidacion(CorteCajaDTO dto, string tkn)
        {
            var agente = new AgenteServicio();
            agente.GenerarLiquidacion(dto, tkn);
            return agente._RespuestaDTO;
        }
        public static List<PuntoVentaModel> ListaPuntoVentaLiquidacion(string tkn)
        {
            var agente = new AgenteServicio();
            agente.BuscarPtoVentasLiquidacion(tkn);
            return agente._listaPuntosV;
        }
        public static List<VentaCajaGeneralDTO> BuscarLiquidacionesDelDia(string tkn)
        {
            var agente = new AgenteServicio();
            agente.ObtenerLiquidacionesDelDia(tkn);
            return agente._ListaVentaCajaGeneralDTO;
        }
        #endregion
    }
}