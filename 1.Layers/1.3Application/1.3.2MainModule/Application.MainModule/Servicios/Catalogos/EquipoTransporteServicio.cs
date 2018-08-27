using Application.MainModule.Servicios.AccesoADatos;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Catalogos
{
    public static class EquipoTransporteServicio
    {
        public static string ObtenerNombre(UnidadAlmacenGas uAG)
        {
            if (uAG.IdCamioneta != null)
                return new EquipoTransporteDataAccess().BuscarCamioneta(uAG).Nombre;
            if (uAG.IdPipa != null)
                return new EquipoTransporteDataAccess().BuscarPipa(uAG).Nombre;
            if (uAG.IdEstacionCarburacion != null)
                return new EquipoTransporteDataAccess().BuscarEstacionCarburacion(uAG).Nombre;
            return null;
        }
    }
}
