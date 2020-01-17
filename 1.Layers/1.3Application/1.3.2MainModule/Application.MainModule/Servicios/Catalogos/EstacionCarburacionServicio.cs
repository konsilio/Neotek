using Application.MainModule.DTOs.Catalogo;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.AccesoADatos;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Catalogos
{
    public static class EstacionCarburacionServicio
    {
        public static string ObtenerNombre(UnidadAlmacenGas uAG)
        {   
            if (uAG.IdEstacionCarburacion != null)
                return new EstacionCarburacionDataAccess().BuscarEstacionCarburacion(uAG).Nombre;
            return null;
        }
        public static EstacionCarburacion Obtener(int idEstacion)
        {
            return new EstacionCarburacionDataAccess().Buscar(idEstacion);
        }
        public static RespuestaDto Modificar(EstacionCarburacion entidad)
        {
            return new EstacionCarburacionDataAccess().Actualizar(entidad);
        }
        public static List<EstacionCarburacion> ObtenerTodas()
        {
            return new EstacionCarburacionDataAccess().BuscarTodos();
        }
        public static List<EstacionCarburacion> ObtenerTodas(short IdEmpresa)
        {
            return new EstacionCarburacionDataAccess().BuscarTodos(IdEmpresa);
        }
        public static List<EstacionCarburacion> Obtener(List<EstacionCarburacionDTO> estaciones)
        {
            List<EstacionCarburacion> lista = new List<EstacionCarburacion>();
            foreach (var item in estaciones)
                if(item.Activo && Obtener(item.IdEstacionCarburacion) != null)
                lista.Add(Obtener(item.IdEstacionCarburacion));
            return lista;
        }
    }
}
