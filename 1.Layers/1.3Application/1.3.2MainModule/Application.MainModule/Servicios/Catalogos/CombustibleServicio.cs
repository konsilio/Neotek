using Application.MainModule.DTOs.Respuesta;
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
            return new CamionetaDataAccess().Insertar(entidad);
        }
        public static RespuestaDto Modificar(CCombustible entidad)
        {
            return new CamionetaDataAccess().Actualizar(entidad);
        }
        public static List<CCombustible> Obtener()
        {
            var empresa = EmpresaServicio.Obtener(TokenServicio.ObtenerIdEmpresa());

            if (empresa.EsAdministracionCentral)
                return new CamionetaDataAccess().ObtenerCamionetas();
            else
                return new CamionetaDataAccess().ObtenerCamionetas(empresa.IdEmpresa);
        }
        public static List<CCombustible> Obtener(short idEmpresa)
        {
            return new CamionetaDataAccess().ObtenerCamionetas(idEmpresa).ToList();
        }
    }
}
