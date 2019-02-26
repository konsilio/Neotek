using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.DTOs.Transporte;
using Application.MainModule.Servicios.AccesoADatos;
using Exceptions.MainModule.Validaciones;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios
{
    public static class AsignacionUtilitarioServicio
    {
        public static RespuestaDto Crear(AsignacionUtilitarios entidad)
        {
            return new AsigancionUtilitariosDataAccess().Insertar(entidad);
        }
        public static RespuestaDto Actualizar(AsignacionUtilitarios entidad)
        {
            return new AsigancionUtilitariosDataAccess().Actualizar(entidad);
        }
        public static List<AsignacionUtilitarios> Buscar()
        {
            return new AsigancionUtilitariosDataAccess().Obtener();
        }
        public static AsignacionUtilitarios Buscar(int id)
        {
            return new AsigancionUtilitariosDataAccess().Obtener(id);
        }
        public static AsignacionUtilitarios BuscarPorUtilitario(int id)
        {
            return new AsigancionUtilitariosDataAccess().ObtenerPorUtilitario(id);
        }
        public static AsignacionUtilitarios Buscar(TransporteDTO dto)
        {
            return new AsigancionUtilitariosDataAccess().Obtener(dto);
        }
        public static RespuestaDto Existe()
        {
            string mensaje = string.Format(Error.SiExiste, "La asignacion");

            return new RespuestaDto()
            {
                ModeloValido = true,
                Mensaje = mensaje,
                Exito = false,
                MensajesError = new List<string>() { mensaje },
            };
        }
    }
}
