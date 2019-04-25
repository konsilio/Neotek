using Application.MainModule.AdaptadoresDTO.Catalogo;
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
        public static RespuestaDto Borrar(int id)
        {
            var entidad = Obtener(id);
            entidad.Activo = false;
            return new CamionetaDataAccess().Actualizar(CamionetaAdapter.FromEntity(entidad));
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
        public static Camioneta Obtener(short idEmpresa, string nombre, string numero)
        {
            return new CamionetaDataAccess().ObtenerCamioneta(idEmpresa, nombre, numero);
        }
        public static Camioneta Obtener(int id)
        {
            return new CamionetaDataAccess().ObtenerCamioneta(id);
        }
        public static Camioneta Obtener(int id, bool activo)
        {
            return new CamionetaDataAccess().ObtenerCamioneta(id, activo);
        }
        public static RespuestaDto NoExiste()
        {
            string mensaje = string.Format(Error.NoExiste, "La camioneta");

            return new RespuestaDto()
            {
                ModeloValido = true,
                Mensaje = mensaje,
                MensajesError = new List<string>() { mensaje },
            };
        }
    }
}
