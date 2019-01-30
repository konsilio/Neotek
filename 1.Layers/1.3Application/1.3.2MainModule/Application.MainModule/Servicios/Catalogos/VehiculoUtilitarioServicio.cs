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
    public static class VehiculoUtilitarioServicio
    {
        public static RespuestaDto Registrar(CUtilitario entidad)
        {
            return new VehiculoUtilitarioDataAccess().Insertar(entidad);
        }
        public static RespuestaDto Modificar(CUtilitario entidad)
        {
            return new VehiculoUtilitarioDataAccess().Actualizar(entidad);
        }
        public static List<CUtilitario> Obtener()
        {
            var empresa = EmpresaServicio.Obtener(TokenServicio.ObtenerIdEmpresa());

            if (empresa.EsAdministracionCentral)
                return new VehiculoUtilitarioDataAccess().Obtener();
            else
                return new VehiculoUtilitarioDataAccess().Obtener(empresa.IdEmpresa);
        }
        public static List<CUtilitario> Obtener(short idEmpresa)
        {
            return new VehiculoUtilitarioDataAccess().Obtener(idEmpresa).ToList();
        }
        public static CUtilitario Obtener(short idEmpresa, string nombre, string numero)
        {
            return new VehiculoUtilitarioDataAccess().Obtener(idEmpresa, nombre, numero);
        }
        public static RespuestaDto NoExiste()
        {
            string mensaje = string.Format(Error.NoExiste, "El vehiculo utilitario");

            return new RespuestaDto()
            {
                ModeloValido = true,
                Mensaje = mensaje,
                MensajesError = new List<string>() { mensaje },
            };
        }
    }
}
