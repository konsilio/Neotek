using Application.MainModule.DTOs;
using Application.MainModule.DTOs.Compras;
using Application.MainModule.Servicios;
using Application.MainModule.Servicios.Compras;
using Application.MainModule.Servicios.Seguridad;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Flujos
{
    public class Compras
    {
        public OrdenCompraRespuestaDTO ComprarGas()
        {
            UsuarioAplicacionServicio.Obtener();

            return new OrdenCompraRespuestaDTO()
            {
                Exito = true
            };
        }
        public RequisicionOCDTO BuscarRequisicion(int idRequisicion)
        {
            return OrdenCompraServicio.BuscarRequisicion(idRequisicion);
        }
        public List<OrdenCompraRespuestaDTO> GenerarOrdenesCompra(OrdenCompraCrearDTO oc)
        {
            List<OrdenCompraRespuestaDTO> lrOC = new List<OrdenCompraRespuestaDTO>();
            List<OrdenCompra> locDTO = OrdenCompraServicio.IdentificarOrdenes(oc);
            locDTO = OrdenCompraServicio.AsignarProductos(oc.Productos, locDTO);
            locDTO = OrdenCompraServicio.CalcularTotales(locDTO);
            foreach (var ocDTO in locDTO)
            {
                ocDTO.NumOrdenCompra = FolioServicio.GeneraNumerOrdenCompra(ocDTO);
                lrOC.Add(OrdenCompraServicio.GuardarOrdenCompra(ocDTO));
            }
            return lrOC;
        }
        public OrdenCompraRespuestaDTO AutorizarOrdenCompra(OrdenCompraDTO dto)
        {
            OrdenCompraRespuestaDTO respDTO = new OrdenCompraRespuestaDTO();

            return respDTO;
        }
        public OrdenCompraRespuestaDTO CancelarOrdenCompra(OrdenCompraDTO dto)
        {
            OrdenCompraRespuestaDTO respDTO = new OrdenCompraRespuestaDTO();

            return respDTO;
        }
    }
}
