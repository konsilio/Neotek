using MVC.Presentacion.Agente;
using MVC.Presentacion.Models.Cobranza;
using MVC.Presentacion.Models.Seguridad;
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
        public static RespuestaDTO AltaNuevoCargo(List<CargosModel> model, string tkn)
        {
            var agente = new AgenteServicio();
           // agente.GuardarNuevoAbono(model, tkn);
            return agente._RespuestaDTO;
        }
        public static RespuestaDTO ActualizarCargo(CargosModel model, string tkn)
        {
            var agente = new AgenteServicio();
            agente.GuardarEdicionCargo(model, tkn);
            return agente._RespuestaDTO;
        }
        //public static RespuestaDTO EliminarCargo(CargosModel model, string tkn)
        //{
        //    var agente = new AgenteServicio();
        //    agente.CancelarNuevoCargo(model, tkn);
        //    return agente._RespuestaDTO;
        //}
     
    }
}