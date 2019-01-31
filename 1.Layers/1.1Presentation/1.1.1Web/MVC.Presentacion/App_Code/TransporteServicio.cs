using MVC.Presentacion.Agente;
using MVC.Presentacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.App_Code
{
    public static class TransporteServicio
    {
        public static List<MantenimientoModel> GetCatMantenimiento(string tkn)
        {
            var agente = new AgenteServicio();
            agente.BuscarCatalogoMantenimiento(tkn);
            return agente._ListaMantenimientos;
        }
        public static List<MantenimientoDetalleModel> GetMantenimientos(string tkn)
        {
            var agente = new AgenteServicio();
            agente.BuscarMantenimiento(tkn);
            return agente._ListaMantenimientoDetalle;
        }
        public static List<RecargaCombustibleModel> GetRecargaCombustible(string tkn)
        {
            var agente = new AgenteServicio();
            agente.BuscarRecargasCombustible(tkn, TokenServicio.ObtenerIdEmpresa(tkn));
            return agente._ListaRecargasCombustible;
        }
        public static List<AsignacionModel> GetAsignacion(string tkn)
        {
            var agente = new AgenteServicio();
            agente.BuscarAsignaciones(tkn);
            return agente._ListaAsignaciones;
        }
    }
}