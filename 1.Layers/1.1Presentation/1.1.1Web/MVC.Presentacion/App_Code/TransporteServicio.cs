using MVC.Presentacion.Agente;
using MVC.Presentacion.Models;
using MVC.Presentacion.Models.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.App_Code
{
    public static class TransporteServicio
    {
        public static List<MantenimientoModel> ListaCatMantenimiento(string tkn)
        {
            var agente = new AgenteServicio();
            agente.BuscarCatalogoMantenimiento(tkn);
            return agente._ListaMantenimientos;
        }       
        public static List<MantenimientoDetalleModel> ListaMantenimientos(string tkn)
        {
            var agente = new AgenteServicio();
            agente.BuscarMantenimiento(tkn);
            return agente._ListaMantenimientoDetalle;
        }
        public static RespuestaDTO RegistrarMantenimiento(MantenimientoDetalleModel model, string tkn)
        {
            var agente = new AgenteServicio();
            agente.GuardarMantenimiento(model, tkn);
            return agente._RespuestaDTO;
        }
        public static MantenimientoDetalleModel ActivarEditarMantenimiento(int id, string tkn)
        {
            return ListaMantenimientos(tkn).SingleOrDefault(x => x.Id_DetalleMtto.Equals(id));
        }
        public static RespuestaDTO ModificarManteniminento(MantenimientoDetalleModel model, string tkn)
        {
            var agente = new AgenteServicio();
            agente.ModificaMantenimiento(model, tkn);
            return agente._RespuestaDTO;
        }
        public static RespuestaDTO EliminarMantenimiento(int id, string tkn)
        {
            var agente = new AgenteServicio();
            agente.EliminarMantenimiento(id, tkn);
            return agente._RespuestaDTO;
        }

        public static List<RecargaCombustibleModel> ListaRecargaCombustible(string tkn)
        {
            var agente = new AgenteServicio();
            agente.BuscarRecargasCombustible(tkn, TokenServicio.ObtenerIdEmpresa(tkn));
            return agente._ListaRecargasCombustible;
        }
        public static RespuestaDTO GuardarRecargaCombustible(RecargaCombustibleModel model, string tkn)
        {
            var agente = new AgenteServicio();
            agente.GuardarRecargaCombustible(model, tkn);
            return agente._RespuestaDTO;
        }
        public static RespuestaDTO EditarRecargaCombustible(RecargaCombustibleModel model, string tkn)
        {
            var agente = new AgenteServicio();
            agente.ModificarRecargaCombustible(model, tkn);
            return agente._RespuestaDTO;
        }
        public static RecargaCombustibleModel ActivarEditarRecarga(int id, string tkn)
        {
            return ListaRecargaCombustible(tkn).SingleOrDefault(x => x.Id_DetalleRecargaComb.Equals(id));
        }
        public static RespuestaDTO EliminarRecargaCombustible(RecargaCombustibleModel model, string tkn)
        {
            var agente = new AgenteServicio();
            agente.EliminarRecargaCombustible(model, tkn);
            return agente._RespuestaDTO;
        }
        public static List<AsignacionModel> ListaAsignacion(string tkn)
        {
            var agente = new AgenteServicio();
            agente.BuscarAsignaciones(tkn);
            return agente._ListaAsignaciones;
        }
        public static RespuestaDTO GuardarAsignacion(AsignacionModel model, string tkn)
        {
            var agente = new AgenteServicio();
            agente.GuardarAsignacion(model, tkn);
            return agente._RespuestaDTO;
        }
        public static RespuestaDTO EliminarAsignacion(AsignacionModel model, string tkn)
        {
            var agente = new AgenteServicio();
            agente.EliminarAsignacion(model, tkn);
            return agente._RespuestaDTO;
        }
    }
}