using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.AccesoADatos;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios
{
    public static class UsoCFDIServicio
    {
        public static RespuestaDto Crear(UsoCFDI entidad)
        {
            return new UsoCFDIDataAccess().Insertar(entidad);
        }
        public static RespuestaDto Actualizar(UsoCFDI entidad)
        {
            return new UsoCFDIDataAccess().Actualizar(entidad);
        }
        public static List<UsoCFDI> Buscar()
        {
            return new UsoCFDIDataAccess().Obtener();
        }
        public static UsoCFDI Buscar(int id)
        {
            return new UsoCFDIDataAccess().Obtener(id);
        }
    }
}
