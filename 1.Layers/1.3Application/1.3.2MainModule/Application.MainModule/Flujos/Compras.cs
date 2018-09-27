using Application.MainModule.AdaptadoresDTO.Compras;
using Application.MainModule.AdaptadoresDTO.Mobile;
using Application.MainModule.DTOs;
using Application.MainModule.DTOs.Compras;
using Application.MainModule.DTOs.Requisicion;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.Servicios.Compras;
using Application.MainModule.Servicios.Notificacion;
using Application.MainModule.Servicios.Requisicion;
using Application.MainModule.Servicios.Seguridad;
using Sagas.MainModule.Entidades;
using Sagas.MainModule.ObjetosValor.Enum;
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
        /// <summary>
        /// Con el id de la Requisicion se genera un DTO con los datos para poder generar ordenes de compras
        /// </summary>
        /// <param name="idRequisicion"></param>
        /// <returns></returns>
        public RequisicionOCDTO BuscarRequisicion(int idRequisicion)
        {
            var Req = OrdenCompraServicio.BuscarRequisicion(idRequisicion);
            Req.Productos = OrdenCompraServicio.DescartarProductosParaOC(Req.Productos);
            //Retornamos el objeto con los productos filtrados
            return Req;
        }      
        /// <summary>
        /// Generamos la(s) ordene(s) de compra segun los provedores de la lista de productos.
        /// </summary>
        /// <param name="oc"></param>
        /// <returns></returns>
        public RespuestaDto GenerarOrdenesCompra(OrdenCompraCrearDTO oc)
        {
            RespuestaDto respuesta = new RespuestaDto();
            respuesta.Exito = true;
            List<OrdenCompra> locDTO = OrdenCompraServicio.IdentificarOrdenes(oc);
            locDTO = OrdenCompraServicio.AsignarProductos(oc.Productos, locDTO);
            locDTO = OrdenCompraServicio.CalcularTotales(locDTO);
            foreach (var ocDTO in locDTO)
            {                
                ocDTO.NumOrdenCompra = FolioServicio.GeneraNumerOrdenCompra(ocDTO);
                OrdenCompraRespuestaDTO orDTO = OrdenCompraServicio.GuardarOrdenCompra(ocDTO);
                respuesta.Mensaje+= orDTO.NumOrdenCompra +"|";
                if (orDTO.Exito)
                {
                    respuesta.Id = orDTO.IdOrdenCompra;
                    respuesta.EsInsercion = true;
                    RequisicionServicio.UpDateRequisicionEstaus(oc.IdRequisicion, 8);
                    NotificarServicio.OrdenDeCompraNueva(OrdenCompraServicio.Buscar(orDTO.IdOrdenCompra));
                }
                else
                {
                    respuesta.Exito = orDTO.Exito;
                    respuesta.Mensaje = orDTO.Mensaje;
                }
            }            
            return respuesta;
        }
        /// <summary>
        /// Actualiza los datos de la orden de compra de su autorizacion
        /// </summary>
        /// <param name="_oc"></param>
        /// <returns></returns>
        public RespuestaDto AutorizarOrdenCompra(OrdenCompraDTO _oc)
        {
            var resp = PermisosServicio.PuedeAutorizarOrdenCompra();
            if (!resp.Exito) return resp;
           
            var oc = OrdenCompraServicio.Buscar(_oc.IdOrdenCompra);
            if (oc == null) return OrdenCompraServicio.NoExiste();
          
            var entity = OrdenComprasAdapter.FromEntity(oc);
            entity.IdUsuarioAutorizador = TokenServicio.ObtenerIdUsuario();
            entity.FechaAutorizacion = Convert.ToDateTime(DateTime.Today.ToShortDateString());
            entity.IdOrdenCompraEstatus = OrdenCompraEstatusEnum.Proceso_compra;
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
        public OrdenCompraDTO BuscarOrdenCompra(int idOrdeCompra)
        { 
            //Valida permiso para consultar orden de compra
            var resp = PermisosServicio.PuedeConsultarOrdenCompra();
            if (!resp.Exito) return new OrdenCompraCrearDTO();

            //Se busca el id en la base y se genera DTO para enviar
            var oc = OrdenComprasAdapter.ToDTO(OrdenCompraServicio.Buscar(idOrdeCompra));           
            return OrdenCompraServicio.AgregarDatosRequisicion(oc) ;
        }
        public ComplementoGasDTO BuscarComplementoGas(int idOrdenCompra)
        {
            var oc = OrdenCompraServicio.Buscar(idOrdenCompra);
            var cg = OrdenCompraServicio.BuscarComplemento(oc);


            return cg;
        }
        public List<OrdenCompraEstatusDTO> ListaEstatus()
        {
            return OrdenComprasAdapter.ToDTO(OrdenCompraServicio.ListaEstatus());
        }
    }
}
