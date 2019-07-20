using MVC.Presentacion.Agente;
using MVC.Presentacion.Models;
using MVC.Presentacion.Models.Almacen;
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
        public static RespuestaDTO RegistrarSalida(RequisicionSalidaDTO model, string tkn)
        {
            AgenteServicio agente = new AgenteServicio();
            agente.RegistrarSalida(model, tkn);
            return agente._RespuestaDTO;
        }
        public static RequisicionSalidaDTO BuscarRequisicionSalida(int idRequisicion, string tkn)
        {
            AgenteServicio agente = new AgenteServicio();
            agente.BuscarRequsicionSalida(idRequisicion, tkn);
            return agente._RequisicionSalida;
        }
        public static List<RemanenteGeneralDTO> BuscarRemanente(RemanenteModel model, string tkn)
        {
            AgenteServicio agente = new AgenteServicio();
            agente.BuscarRemanenteGeneral(model, tkn);
            return agente._ListaRemanenteGenaral;
        }
        public static List<RemanentePuntoVentaTodosDTO> BuscarRemanentePuntoVenta(RemanenteModel model, string tkn)
        {
            AgenteServicio agente = new AgenteServicio();
            agente.BuscarRemanentePtoVenta(model, tkn);
            return agente._ListaRemanentePtoVenta;
        }
    }
}