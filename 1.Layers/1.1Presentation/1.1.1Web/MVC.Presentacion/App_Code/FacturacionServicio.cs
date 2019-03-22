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
            _agente._VentaDTO.seleccionar = true;
            return _agente._VentaDTO;
        }

        public static List<CFDIDTO> ObtenerCFDIs(FacturacionModel model)
        {
            AgenteServicio _agente = new AgenteServicio();
            if (string.IsNullOrEmpty(model.Ticket))
            {
                _agente.ListaCFDIs(model);
                return _agente._ListaCFDIs;
            }
            else
            {
                _agente.BuscarCFDI(model.Ticket);
                return new List<CFDIDTO>() { _agente._CFDIDTO };
            }           
        }
        public static CFDIDTO ObtenerCFDI(string ticket)
        {
            AgenteServicio _agente = new AgenteServicio();
            _agente.BuscarCFDI(ticket);           
            return _agente._CFDIDTO;
        }
    }
}