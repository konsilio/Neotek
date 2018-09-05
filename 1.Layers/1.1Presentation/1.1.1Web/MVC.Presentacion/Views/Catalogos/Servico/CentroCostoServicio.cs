using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.MainModule.Agente;
using Web.MainModule.Catalogos.Model;
using Web.MainModule.Seguridad.Model;

namespace Web.MainModule.Catalogos.Servicio
{
    public class CentroCostoServicio
    {
        public List<CentroCostoDTO> BuscarCentrosCosto(string Tkn)
        {
            var agente = new AgenteServicios();
            agente.BuscarCentrosCostos(Tkn);
            return agente._listaCentrosCostos;
        }
        public RespuestaDto ModificarCentroCosto(CentroCostoModificarDto dto, string Tkn)
        {
            var agente = new AgenteServicios();
            agente.ModificarCtroCosto(dto, Tkn);
            return agente._respuestaDTO;
        }
        public RespuestaDto EliminarCentroCosto(CentroCostoEliminarDto dto, string Tkn)
        {
            var agente = new AgenteServicios();
            agente.EliminarCtroCosto(dto, Tkn);
            return agente._respuestaDTO;
        }
        public RespuestaDto NuevoCentroCosto(CentroCostoCrearDto dto, string Tkn)
        {
            var agente = new AgenteServicios();
            agente.GuardarCentroCosto(dto, Tkn);
            return agente._respuestaDTO;
        }
    }
}