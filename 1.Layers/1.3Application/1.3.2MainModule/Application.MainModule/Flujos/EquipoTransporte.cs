using Application.MainModule.DTOs;
using Application.MainModule.Servicios.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using Application.MainModule.Servicios.Catalogos;

namespace Application.MainModule.Flujos
{
    public class EquipoTransporte
    {
        public List<EquipoTransporteDTO> ListaEquiposdeTransporte(short idempresa)
        {
            var resp = PermisosServicio.PuedeConsultarParqueVehicular();
            if (!resp.Exito) return null;

            return EquipoTransporteServicio.Obtener(idempresa).ToList();
        }
    }
}
