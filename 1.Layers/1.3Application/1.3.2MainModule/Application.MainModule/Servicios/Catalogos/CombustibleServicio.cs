using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.Servicios.Seguridad;
using Exceptions.MainModule.Validaciones;
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
        public static RespuestaDto Eliminar(CCombustible entidad)
        {
            return new CombustibleDataAccess().Eliminar(entidad);
        }
        public static List<CCombustible> Obtener()
        {
            var empresa = EmpresaServicio.Obtener(TokenServicio.ObtenerIdEmpresa());

            if (empresa.EsAdministracionCentral)
                return new CombustibleDataAccess().ObtenerCombustible();
            else
                return new CombustibleDataAccess().ObtenerCombustible(empresa.IdEmpresa);
        }
        public static List<CCombustible> Obtener(short idEmpresa)
        {
            return new CombustibleDataAccess().ObtenerCombustible(idEmpresa).ToList();
        }
        public static List<CCombustible> Obtener(short idEmpresa,string desc)
        {
            var empresa = EmpresaServicio.Obtener(TokenServicio.ObtenerIdEmpresa());

            return new CombustibleDataAccess().ObtenerCombustible(idEmpresa, desc);
        }
        public static CCombustible Obtener(int idCombustible)
        {
            return new CombustibleDataAccess().ObtenerCombustible(idCombustible);
        }

        public static RespuestaDto NoExiste()
        {
            string mensaje = string.Format(Error.NoExiste, "El tipo combustible");

            return new RespuestaDto()
            {
                ModeloValido = true,
                Mensaje = mensaje,
                MensajesError = new List<string>() { mensaje },
            };
        }
    }
}
