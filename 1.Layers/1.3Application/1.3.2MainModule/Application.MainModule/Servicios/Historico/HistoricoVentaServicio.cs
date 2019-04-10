using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.AccesoADatos;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Historico
{
    class HistoricoVentaServicio
    {
        public static RespuestaDto Crear(List<HistoricoVentas> Lista)
        {
            return new HistoricoDataAcces().Insertar(Lista);
        }
        public static RespuestaDto Actualizar(HistoricoVentas entidad)
        {
            return new HistoricoDataAcces().Actualizar(entidad);
        }
        public static List<HistoricoVentas> Buscar()
        {
            return new HistoricoDataAcces().Obtener();
        }
        public static HistoricoVentas Buscar(int id)
        {
            return new HistoricoDataAcces().Obtener(id);
        }
        public static List<HistoricoVentas> Buscar(short id)
        {
            return new HistoricoDataAcces().Obtener();
        }
    }
}
