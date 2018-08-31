using Application.MainModule.AdaptadoresDTO.Compras;
using Application.MainModule.AdaptadoresDTO.Mobile;
using Application.MainModule.DTOs;
using Application.MainModule.DTOs.Compras;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.Servicios.Compras;
using Application.MainModule.Servicios.Notificacion;
using Application.MainModule.Servicios.Requisicion;
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
                OrdenCompraRespuestaDTO orDTO = OrdenCompraServicio.GuardarOrdenCompra(ocDTO);
                lrOC.Add(orDTO);
                if (orDTO.Exito)
                {
                    RequisicionServicio.UpDateRequisicionEstaus(oc.IdRequisicion, 8);
                    NotificarServicio.OrdenDeCompraNueva(OrdenCompraServicio.Buscar(orDTO.IdOrdenCompra));
                }
            }            
            return lrOC;
        }
        public RespuestaDto AutorizarOrdenCompra(OrdenCompraAutorizacionDTO _oc)
        {
            var resp = PermisosServicio.PuedeAutorizarOrdenCompra();
            if (!resp.Exito) return resp;
           
            var oc = OrdenCompraServicio.Buscar(_oc.IdOrdenCompra);
            if (oc == null) return OrdenCompraServicio.NoExiste();
          
            var entity = OrdenComprasAdapter.FromEntity(oc);
            entity.IdUsuarioAutorizador = TokenServicio.ObtenerIdUsuario();
            entity.FechaAutorizacion = Convert.ToDateTime(DateTime.Today.ToShortDateString());
            entity.IdOrdenCompraEstatus = 3;
            return OrdenCompraServicio.Actualizar(entity);
        }
        public RespuestaDto CancelarOrdenCompra(OrdenCompraDTO dto)
        {
            //Falta rol en la Base de datos
            //var resp = PermisosServicio.PuedeCancelarOrdeCompra();
            //if (!resp.Exito) return resp;

            var oc = OrdenCompraServicio.Buscar(dto.IdOrdenCompra);
            if (oc == null) return OrdenCompraServicio.NoExiste();

            var entity = OrdenComprasAdapter.FromEntity(oc);
            entity.IdOrdenCompraEstatus = 5;
            return OrdenCompraServicio.Actualizar(entity);
        }
        public List<OrdenCompraDTO> ListaOrdenCompra(short IdEmpresa)
        {
            var resp = PermisosServicio.PuedeConsultarOrdenCompra();
            if (!resp.Exito) return new List<OrdenCompraDTO>();

            var _locEntity = OrdenCompraServicio.BuscarTodo(IdEmpresa);
            List<OrdenCompraDTO> loc = OrdenComprasAdapter.ToDTO(_locEntity);
            return loc;
        }
        public OrdenCompraCrearDTO BuscarOrdenCompra(int idOrdeCompra)
        { 
            //Valida permiso para consultar orden de compra
            var resp = PermisosServicio.PuedeConsultarOrdenCompra();
            if (!resp.Exito) return new OrdenCompraCrearDTO();
                     
            //Se busca el id en la base y se genera DTO para enviar
            return OrdenComprasAdapter.ToCDTO(OrdenCompraServicio.Buscar(idOrdeCompra));
        }
        public ComplementoGasDTO BuscarComplementoGas(int idOrdenCompra)
        {
            var oc = OrdenCompraServicio.Buscar(idOrdenCompra);
            var cg = OrdenCompraServicio.BuscarComplemento(oc);


            return cg;
        }
    }
}
