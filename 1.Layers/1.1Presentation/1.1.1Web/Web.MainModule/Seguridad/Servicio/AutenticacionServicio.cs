using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.MainModule.Agente;

namespace Web.MainModule.Seguridad.Servicio
{
    public class AutenticacionServicio
    {
        public string Autenticar(short _rs, string _us, string _pwr)
        {
            var agente = new AgenteServicios();
            agente.Acceder(new Model.AutenticacionDto() { IdEmpresa = _rs, Password =_pwr, Usuario = _us});
            return agente._respuestaAutenticacion.Mensaje;          
        }
    }
}