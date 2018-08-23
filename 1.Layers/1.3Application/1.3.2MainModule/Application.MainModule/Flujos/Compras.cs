using Application.MainModule.AdaptadoresDTO.Compras;
using Application.MainModule.DTOs;
using Application.MainModule.DTOs.Compras;
using Application.MainModule.Servicios.Compras;
using Application.MainModule.Servicios.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
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
            List<OrdenCompraDTO> locDTO = OrdenCompraServicio.IdentificarOrdenes(oc);
            locDTO = OrdenCompraServicio.AsignarProductos(oc.Productos, locDTO);
            locDTO = OrdenCompraServicio.CalcularTotales(locDTO);
            foreach (var ocDTO in locDTO)
            {
                lrOC.Add(OrdenCompraServicio.GuardarOrdenCompra(OrdenComprasAdapter.FromDTO(ocDTO)));
            }
            return lrOC;
        }
    }
}
