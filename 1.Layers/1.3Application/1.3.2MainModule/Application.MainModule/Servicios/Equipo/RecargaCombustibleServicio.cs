using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.AccesoADatos;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Equipo
{
    public static class RecargaCombustibleServicio
    {
        public static RespuestaDto Crear(DetalleRecargaCombustible entidad)
        {
            return new RecargaCombustibleDataAccess().Insertar(entidad);
        }
        public static RespuestaDto Actualizar(DetalleRecargaCombustible entidad)
        {
            return new RecargaCombustibleDataAccess().Actualizar(entidad);
        }
        public static RespuestaDto Borrar(DetalleRecargaCombustible entidad)
        {
            return new RecargaCombustibleDataAccess().Borrar(entidad);
        }
        public static List<DetalleRecargaCombustible> Buscar()
        {
            return new RecargaCombustibleDataAccess().Obtener();
        }
        public static DetalleRecargaCombustible Buscar(int id)
        {
            return new RecargaCombustibleDataAccess().Obtener(id);
        }        
    }
}
