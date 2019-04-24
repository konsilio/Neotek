using MVC.Presentacion.Agente;
using MVC.Presentacion.Models;
using MVC.Presentacion.Models.Cobranza;
using MVC.Presentacion.Models.Seguridad;
using MVC.Presentacion.Models.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.App_Code
{
    public class CobranzaServicio
    {
        public static List<CargosModel> ObtenerCargos(short id, string tkn)
        {
            var agente = new AgenteServicio();
            agente.ListaCargos(id, tkn);
            return agente._ListaCargos;
        }
        public static List<CargosModel> ObtenerListaR(short id, string tkn)
        {
            var agente = new AgenteServicio();
            agente.ListaCRecuperada(id, tkn);
            return agente._ListaCargos;
        }
        public static List<CargosModel> ObtenerCargosFilter(DateTime fecha1, DateTime fecha2, int Cliente, string rfc, string ticket, short id, string tkn)
        {
            var agente = new AgenteServicio();
            agente.ListaCargosFilter(fecha1, fecha2, Cliente, rfc, ticket, id, tkn);
            return agente._ListaCargos;
        }
        public static List<CargosModel> ObtenerCargosFilter(CargosModel _mod, string tkn)
        {
            var agente = new AgenteServicio();
            agente.ListaCargosFilter(_mod, tkn);
            return agente._ListaCargos;
        }
        public static ReporteModel ObtenerListaCartera( string tkn, CargosModel model)
        {
            var agente = new AgenteServicio();           
            agente.ListaCartera(model, tkn);
            return agente._repCartera;
        }        
        public static CargosModel ObtenerIdCargo(int id, string tkn)
        {
            var agente = new AgenteServicio();
            agente.ObtenerCargoId(id, tkn);
            return agente._Cargo;
        }      
        public static RespuestaDTO AltaNuevoCargo(CargosModel model, string tkn)
        {
            var agente = new AgenteServicio();
            agente.GuardarNuevoAbono(model, tkn);
            return agente._RespuestaDTO;
        }
        public static RespuestaDTO guardarAbonos(CargosModel model, string tkn)
        {
            var agente = new AgenteServicio();
            agente.GuardarNuevoAbono(LST(model.Abono), tkn);
            return agente._RespuestaDTO;
        }
        public static RespuestaDTO AltaAbono(AbonosModel model, string tkn)
        {
            var agente = new AgenteServicio();
            agente.GuardarNuevoAbono(LST(model), tkn);
            return agente._RespuestaDTO;
        }
        public static RespuestaDTO AltaAbonos(List<AbonosModel> model, string tkn)
        {
            var agente = new AgenteServicio();
            agente.GuardarNuevoAbono(model, tkn);
            return agente._RespuestaDTO;
        }
        public static RespuestaDTO ActualizarCargo(CargosModel model, string tkn)
        {
            var agente = new AgenteServicio();
            agente.GuardarEdicionCargo(model, tkn);
            return agente._RespuestaDTO;
        }
        public static List<AbonosModel> LST(AbonosModel model)
        {
            List<AbonosModel> lst = new List<AbonosModel>();           
            lst.Add(model);
            return lst;
        }
        public static List<VentaPuntoVentaDTO> ObtenerTickets(FacturacionGlobalModel model, string tkn)
        {
            AgenteServicio _agente = new AgenteServicio();
            _agente.ListaTickets(model, tkn);
            return _agente._ListaVenta;
        }


        //public static RespuestaDTO EliminarCargo(CargosModel model, string tkn)
        //{
        //    var agente = new AgenteServicio();
        //    agente.CancelarNuevoCargo(model, tkn);
        //    return agente._RespuestaDTO;
        //}     
    }
}