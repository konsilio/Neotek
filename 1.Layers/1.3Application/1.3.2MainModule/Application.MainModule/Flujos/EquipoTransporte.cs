using Application.MainModule.DTOs;
using Application.MainModule.Servicios.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Flujos
{
    public class EquipoTransporte
    {
        public List<EquipoTransporteDTO> ListaCargos(short idempresa)
        {
            var resp = PermisosServicio.PuedeConsultarParqueVehicular();
            if (!resp.Exito) return null;

            
             return CobranzaServicio.Obtener(idempresa).ToList();
   }
    }
}
