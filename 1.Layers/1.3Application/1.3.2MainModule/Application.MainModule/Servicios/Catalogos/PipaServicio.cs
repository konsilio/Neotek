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
    public static class PipaServicio
    {
        public static RespuestaDto Registrar(Pipa entidad)
        {
            return new PipaDataAccess().Insertar(entidad);
        }
        public static RespuestaDto Modificar(Pipa entidad)
        {
            return new PipaDataAccess().Actualizar(entidad);
        }
        public static List<Pipa> Obtener()
        {
            var empresa = EmpresaServicio.Obtener(TokenServicio.ObtenerIdEmpresa());

            if (empresa.EsAdministracionCentral)
                return new PipaDataAccess().ObtenerPipas();
            else
                return new PipaDataAccess().ObtenerPipas(empresa.IdEmpresa);
        }
        public static List<Pipa> Obtener(short idEmpresa)
        {
            return new PipaDataAccess().ObtenerPipas(idEmpresa).ToList();
        }
    }
}
