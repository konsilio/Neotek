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
    public static class MantenimientoDetalleServicio
    {
        public static RespuestaDto Crear(DetalleMantenimiento entidad)
        {
            return new MantenimientoDetDataAccess().Insertar(entidad);
        }
        public static RespuestaDto Crear(DetalleMantenimiento entidad, Egreso egreso)
        {
            return new MantenimientoDetDataAccess().Insertar(entidad, egreso);
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
        public static List<DetalleMantenimiento> Buscar(DateTime fi, DateTime ff)
        {
            return new MantenimientoDetDataAccess().Obtener(fi, ff);
        }
        public static List<DetalleMantenimiento> Buscar(DateTime fi, DateTime ff, int id_vehiculo, bool EsCamioneta, bool EsPipa, bool EsUtilitario)
        {
            return new MantenimientoDetDataAccess().Obtener(fi, ff, id_vehiculo, EsCamioneta, EsPipa, EsUtilitario);
        }
        public static List<DetalleMantenimiento> Buscar(DateTime fi, DateTime ff, PuntoVenta pv)
        {
            int id_vehiculo = 0;
            bool EsCamioneta = false, EsPipa = false, EsUtilitario = false;
            if (pv.UnidadesAlmacen.IdPipa != null)
            {
                id_vehiculo = pv.UnidadesAlmacen.IdPipa.Value;
                EsPipa = true;
            }
            if (pv.UnidadesAlmacen.IdCamioneta != null)
            {
                id_vehiculo = pv.UnidadesAlmacen.IdCamioneta.Value;
                EsCamioneta = true;
            }
            return Buscar(fi, ff, id_vehiculo, EsCamioneta, EsPipa, EsUtilitario);
        }
        public static DetalleMantenimiento Buscar(int id)
        {
            return new MantenimientoDetDataAccess().Obtener(id);
        }

        public static string ObtenerNombre(DetalleMantenimiento qt)
        {
            if (qt.EsCamioneta)
                return new EquipoTransporteDataAccess().BuscarCamioneta(qt.id_vehiculo).Nombre;
            if (qt.EsPipa)
                return new EquipoTransporteDataAccess().BuscarPipa(qt.id_vehiculo).Nombre;
            if (qt.EsUtilitario)
                return new EquipoTransporteDataAccess().BuscarUtilitario(qt.id_vehiculo).Nombre;
            return null;
        }

    }
}
