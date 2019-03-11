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
    public static class MantenimientoServicio
    {
        public static RespuestaDto Crear(CMantenimiento entidad)
        {
            return new MantenimientoDataAccess().Insertar(entidad);
        }
        public static RespuestaDto Actualizar(CMantenimiento entidad)
        {
            return new MantenimientoDataAccess().Actualizar(entidad);
        }
        public static List<CMantenimiento> Buscar()
        {
            return new MantenimientoDataAccess().Obtener();
        }
        public static CMantenimiento Buscar(int id)
        {
            return new MantenimientoDataAccess().Obtener(id);
        }
        public static List<CMantenimiento> Buscar(short id)
        {
            return new MantenimientoDataAccess().Obtener(id);
        }
    }
}
