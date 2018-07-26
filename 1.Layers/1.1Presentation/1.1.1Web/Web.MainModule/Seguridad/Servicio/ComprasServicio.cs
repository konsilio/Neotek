using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.MainModule.Agente;

namespace Web.MainModule.Seguridad.Servicio
{
    public class ComprasServicio
    {
        public Model.ComprasDTO Compra(string token)
        {
            var respuesta = new AgenteServicios();
            respuesta.Compras(token);
            return respuesta._respuestacompra;
        }
    }
}