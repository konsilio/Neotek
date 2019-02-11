﻿using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.AccesoADatos;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Equipo
{
    public static class MantenimientoDetalleServicio
    {
        public static RespuestaDto Crear(DetalleMantenimiento entidad)
        {
            return new MantenimientoDetDataAccess().Insertar(entidad);
        }
        public static RespuestaDto Actualizar(DetalleMantenimiento entidad)
        {
            return new MantenimientoDetDataAccess().Actualizar(entidad);
        }
        public static RespuestaDto Borrar(DetalleMantenimiento entidad)
        {
            return new MantenimientoDetDataAccess().Borrar(entidad);
        }
        public static List<DetalleMantenimiento> Buscar()
        {
            return new MantenimientoDetDataAccess().Obtener();
        }
        public static DetalleMantenimiento Buscar(int id)
        {
            return new MantenimientoDetDataAccess().Obtener(id);
        }
    }
}
