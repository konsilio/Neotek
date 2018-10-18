using MVC.Presentacion.Agente;
using MVC.Presentacion.Models;
using MVC.Presentacion.Models.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.App_Code
{
    public static class AlmacenServicio
    {
        public static List<AlmacenDTO> BuscarProductosAlmacen(short id, string tkn)
        {
            var agente = new AgenteServicio();
            agente.BuscarProductosAlmacen(id, tkn);
            return agente._listaAlmacen;
        }
        public static RespuestaDTO ModificarAlmacen(AlmacenDTO dto, string tkn)
        {
            var agente = new AgenteServicio();
            agente.ActualizarAlmacen(dto, tkn);
            return agente._RespuestaDTO;
        }
        public static AlmacenDTO ActivarEditarAlmacen(short id, string tkn)
        {
            return BuscarProductosAlmacen(TokenServicio.ObtenerIdEmpresa(tkn), tkn).SingleOrDefault(x => x.IdProducto.Equals(id));
        }
        public static List<RegistroDTO> BuscarRegistroAlmacen(short id, string tkn)
        {
            var agente = new AgenteServicio();
            agente.BuscarRegistroAlmacen(id, tkn);
            return agente._listaRegistroAlmacen;
        }
    }
}