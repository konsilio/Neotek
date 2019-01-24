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
    public static class CamionetaServicio
    {
        public static RespuestaDto Registrar(Camioneta entidad)
        {
            return new CamionetaDataAccess().Insertar(entidad);
        }
        public static RespuestaDto Modificar(Camioneta entidad)
        {
            return new CamionetaDataAccess().Actualizar(entidad);
        }
        public static List<Camioneta> Obtener()
        {
            var empresa = EmpresaServicio.Obtener(TokenServicio.ObtenerIdEmpresa());

            if (empresa.EsAdministracionCentral)
                return new CamionetaDataAccess().ObtenerCamionetas();
            else
                return new CamionetaDataAccess().ObtenerCamionetas(empresa.IdEmpresa);
        }
        public static List<Camioneta> Obtener(short idEmpresa)
        {
            return new CamionetaDataAccess().ObtenerCamionetas(idEmpresa).ToList();
        }
    }
}
