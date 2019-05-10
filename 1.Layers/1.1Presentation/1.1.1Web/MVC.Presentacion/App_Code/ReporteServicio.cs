using MVC.Presentacion.Agente;
using MVC.Presentacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.App_Code
{
    public static class ReporteServicio
    {
        public static List<CuentasPorPagarDTO> BuscarCuentasPorPagar(CuentasPorPagarModel model, string token)
        {
            var respuestaReq = new AgenteServicio();
            respuestaReq.BuscaCuentasPorPagar(model, token);
            return respuestaReq._ListaCuentasPorPagar;
        }
        public static List<InventarioPorPuntoVentaDTO> BuscarInventarioPorPuntoVenta(InventarioPorPuntoVentaModel model, string token)
        {
            var respuestaReq = new AgenteServicio();
            respuestaReq.BuscaInventarioPorPuntoVenta(model, token);
            return respuestaReq._ListaInventarioPuntoVenta;
        }
        public static List<HistoricoPrecioVentaDTO> BuscarHistoricoPrecioVenta(HistoricoPrecioVentaModel model, string token)
        {
            var respuestaReq = new AgenteServicio();
            respuestaReq.BuscarHistoricoPrecioVenta(model, token);
            return respuestaReq._ListaHistoricoPrecioVenta;
        }
    }
}