using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Equipo
{
    public static class CalculosEquipoTrasporte
    {
        public static int ObtenerKilometrajeInicial(DetalleRecargaCombustible entidad)
        {
            return entidad.KilometrajeActual - (int)entidad.KilometrajeRecorrido;
        }
        public static decimal CalcularRendimientoVehicular(DetalleRecargaCombustible entidad)
        {
            return entidad.KilometrajeRecorrido / entidad.LitrosRecargados;
        }
    }
}
