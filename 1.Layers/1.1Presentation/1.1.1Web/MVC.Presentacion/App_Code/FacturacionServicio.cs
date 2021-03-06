﻿using MVC.Presentacion.Agente;
using MVC.Presentacion.Models;
using MVC.Presentacion.Models.Facturacion;
using MVC.Presentacion.Models.Seguridad;
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
        public static List<CFDIDTO> ObtenerCFDIs(string token)
        {
            AgenteServicio _agente = new AgenteServicio();
            _agente.ListaCFDIs(token);
            return _agente._ListaCFDIs;

        }        
        public static CFDIDTO ObtenerCFDI(string ticket)
        {
            AgenteServicio _agente = new AgenteServicio();
            _agente.BuscarCFDI(ticket);
            return _agente._CFDIDTO;
        }
        public static List<VentaPuntoVentaDTO> DescartarRepetidos(List<VentaPuntoVentaDTO> nueva, List<VentaPuntoVentaDTO> actual)
        {
            foreach (var vn in nueva)
                if (!actual.Exists(x => x.FolioVenta.Equals(vn.FolioVenta)))
                    actual.Add(vn);

            return actual;
        }
        public static RespuestaDTO GenerarFacturas(FacturacionModel model)
        {
            AgenteServicio _agente = new AgenteServicio();
            var cfdis = AdaptarFacturaModelo(model);
            _agente.PostRegistrarCFDILst(cfdis);
            return _agente._RespuestaDTO;
        }
        public static RespuestaDTO GenerarFacturasGlobal(FacturacionGlobalModel model, string token)
        {
            AgenteServicio _agente = new AgenteServicio();           
            _agente.PostRegistrarCFDIGlobal(model, token);
            return _agente._RespuestaDTO;
        }
        private static List<CFDIDTO> AdaptarFacturaModelo(FacturacionModel model)
        {
            List<CFDIDTO> cfdis = new List<CFDIDTO>();
            foreach (var ticket in model.Tickets)
            {
                CFDIDTO cfdi = new CFDIDTO();
                cfdi.Id_FolioVenta = ticket.FolioVenta;
                cfdi.Id_MetodoPago = 0;
                cfdi.Id_FormaPago = (byte)model.IdFormaPago;
                cfdi.IdUsoCFDI = model.IdUsoCFDI;
                cfdis.Add(cfdi);
            }
            return cfdis;
        }
        public static RespuestaDTO GenerarFacturasAbono(int id, string token)
        {
            AgenteServicio _agente = new AgenteServicio();
            _agente.FacturarPago(id, token);
            return _agente._RespuestaDTO;
        }
    }
}