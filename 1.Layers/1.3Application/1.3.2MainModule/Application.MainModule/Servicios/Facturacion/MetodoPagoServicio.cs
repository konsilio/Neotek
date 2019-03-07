using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.AccesoADatos;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Facturacion
{
    public static class MetodoPagoServicio
    {
        public static RespuestaDto Crear(MetodoPago entidad)
        {
            return new MetodoPagoDataAccess().Insertar(entidad);
        }
        public static RespuestaDto Actuualizar(MetodoPago entidad)
        {
            return new MetodoPagoDataAccess().Actualizar(entidad);
        }
        public static List<MetodoPago> Buscar()
        {
            return new MetodoPagoDataAccess().Obtener();
        }
        public static MetodoPago Buscar(int id)
        {
            return new MetodoPagoDataAccess().Obtener(id);
        }
    } 
}
