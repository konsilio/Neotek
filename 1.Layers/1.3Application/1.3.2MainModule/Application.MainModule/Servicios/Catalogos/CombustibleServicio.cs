using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.Servicios.Seguridad;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Catalogos
{
    public static class CombustibleServicio
    {
        public static RespuestaDto Registrar(CCombustible entidad)
        {
            return new CombustibleDataAccess().Insertar(entidad);
        }
        public static RespuestaDto Modificar(CCombustible entidad)
        {
            return new CombustibleDataAccess().Actualizar(entidad);
        }
        public static List<CCombustible> Obtener()
        {
            var empresa = EmpresaServicio.Obtener(TokenServicio.ObtenerIdEmpresa());

            if (empresa.EsAdministracionCentral)
                return new CombustibleDataAccess().ObtenerCamionetas();
            else
                return new CombustibleDataAccess().ObtenerCamionetas(empresa.IdEmpresa);
        }
        public static List<CCombustible> Obtener(short idEmpresa)
        {
            return new CombustibleDataAccess().ObtenerCamionetas(idEmpresa).ToList();
        }
    }
}
