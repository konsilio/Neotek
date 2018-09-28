using Application.MainModule.AdaptadoresDTO.Almacen;
using Application.MainModule.DTOs.Almacen;
using Application.MainModule.DTOs.Compras;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.Servicios.Almacen;
using Application.MainModule.Servicios.Compras;
using Application.MainModule.Servicios.Requisicion;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Flujos
{
    public class Almacen
    {
        public RespuestaDto GenerarEntradaProducto(OrdenCompraEntradasDTO dto)
        {
            List<Sagas.MainModule.Entidades.Almacen> _almacen = new List<Sagas.MainModule.Entidades.Almacen>();
            List<AlmacenEntradaProducto> entradas = new List<AlmacenEntradaProducto>();
            foreach (var prod in dto.Productos)
            {
                var Almacen = ProductoAlmacenServicio.ObtenerAlmacen(prod.IdProducto, dto.IdEmpresa);
                var AlmacenActualizar = ProductoAlmacenServicio.AlmacenEmtity(Almacen);
                AlmacenActualizar.Cantidad = +prod.Cantidad;
                _almacen.Add(AlmacenActualizar);

                var EntradaProd = ProductoAlmacenServicio.GenerarAlmacenEntradaProcuto(prod, dto.IdOrdenCompra, Almacen);
                entradas.Add(EntradaProd);
            }
            return ProductoAlmacenServicio.EntradaAlmcacenProductos(_almacen, entradas);
        }
        public OrdenCompraEntradasDTO BuscarOrdenCompra(int Id)
        {
            var oc = OrdenCompraServicio.Buscar(Id);
            var req = RequisicionServicio.Buscar(oc.IdRequisicion);
            return ProductoAlmacenServicio.AlmacenEntrada(oc, req);
        }
    }
}
