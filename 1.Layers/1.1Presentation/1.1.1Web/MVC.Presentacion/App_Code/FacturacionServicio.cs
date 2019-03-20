
using MVC.Presentacion.Agente;
using MVC.Presentacion.Models.Facturacion;
using MVC.Presentacion.Models.Ventas;
using System.Collections.Generic;

namespace MVC.Presentacion.App_Code
{
    public class FacturacionServicio
    {
        public static List<VentaPuntoVentaDTO> ObtenerTickets(FacturacionModel model)
        {
            AgenteServicio _agente = new AgenteServicio();
            _agente.ListaTickets(model);
            return _agente._ListaVenta;
        }
        public static VentaPuntoVentaDTO ObtenerTicket(string ticket)
        {
            AgenteServicio _agente = new AgenteServicio();
            _agente.ListaTicket(ticket);
            return _agente._VentaDTO;
        }
    }
}