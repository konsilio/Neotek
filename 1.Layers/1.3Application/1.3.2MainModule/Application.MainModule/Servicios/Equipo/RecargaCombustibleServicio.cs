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
        public static RespuestaDto Crear(DetalleRecargaCombustible entidad, Egreso egreso)
        {
            return new RecargaCombustibleDataAccess().Insertar(entidad, egreso);
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
        public static List<DetalleRecargaCombustible> Buscar(DateTime fi, DateTime ff)
        {
            return new RecargaCombustibleDataAccess().Obtener(fi, ff);
        }
        public static List<DetalleRecargaCombustible> Buscar(DateTime f, int id_vehiculo, bool EsCamioneta, bool EsPipa, bool EsUtilitario)
        {
            return new RecargaCombustibleDataAccess().Obtener(f, id_vehiculo, EsCamioneta, EsPipa, EsUtilitario);
        }
        public static List<DetalleRecargaCombustible> Buscar(DateTime fi, DateTime ff, int id_vehiculo, bool EsCamioneta, bool EsPipa, bool EsUtilitario)
        {
            return new RecargaCombustibleDataAccess().Obtener(fi, ff, id_vehiculo, EsCamioneta, EsPipa, EsUtilitario);
        }

        public static List<DetalleRecargaCombustible> Buscar(DateTime fi, DateTime ff, PuntoVenta pv)
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
        public static DetalleRecargaCombustible Buscar(int id)
        {
            return new RecargaCombustibleDataAccess().Obtener(id);
        }
        public static DetalleRecargaCombustible BuscarAnterior(DetalleRecargaCombustible entidad)
        {
            var recargas = new RecargaCombustibleDataAccess().ObtenerPorVehiculo(entidad.Id_Vehiculo);
            return recargas.OrderByDescending(x => x.FechaRecarga).FirstOrDefault(y => y.FechaRecarga < entidad.FechaRecarga);
        }
    }
}
