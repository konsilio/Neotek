using MVC.Presentacion.Agente;
using MVC.Presentacion.Models.EquipoTransporte;
using MVC.Presentacion.Models.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.App_Code
{
    public static class EquipoTrServicio
    {
        public static List<ParqueVehicularModel> Obtener(short id, string tkn)
        {
            var agente = new AgenteServicio();
            agente.ListaPedidos(id, tkn);
            return agente._ListaVehiculos;
        }
        public static ParqueVehicularModel Obtener(int id, string tkn)
        {
            var agente = new AgenteServicio();
            agente.ObtenerVehiculoId(id, tkn);
            return agente._Vehiculos;
        }
        public static RespuestaDTO Crear(ParqueVehicularModel cc, string tkn)
        {
            var agente = new AgenteServicio();
            agente.GuardarVehiculo(cc, tkn);
            return agente._RespuestaDTO;
        }

        public static RespuestaDTO Modificar(ParqueVehicularModel cc, string tkn)
        {
            var agente = new AgenteServicio();
            agente.EditarVehiculo(cc, tkn);
            return agente._RespuestaDTO;
        }

        public static RespuestaDTO Eliminar(int id, string tkn)
        {
            var agente = new AgenteServicio();
            agente.EliminarVehiculo(id, tkn);
            return agente._RespuestaDTO;
        }
    }
}