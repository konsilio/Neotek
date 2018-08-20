using Application.MainModule.Servicios.AccesoADatos;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Seguridad
{
    public static class RolServicio
    {
        public static List<Rol> ObtenerRoles(short idEmpresa)
        {
            return new RolDataAccess().Buscar(idEmpresa);
        }

        public static List<Rol> ObtenerRoles(Empresa empresa)
        {
            if (empresa != null)
                if (empresa.Roles != null && empresa.Roles.Count > 0)
                    return empresa.Roles.ToList();

            return ObtenerRoles(empresa.IdEmpresa);
        }
    }
}
