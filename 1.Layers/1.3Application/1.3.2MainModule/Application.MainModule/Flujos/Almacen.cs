using Application.MainModule.AdaptadoresDTO.Almacen;
using Application.MainModule.DTOs.Almacen;
using Application.MainModule.DTOs.Compras;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.Almacen;
using Application.MainModule.Servicios.Catalogos;
using Application.MainModule.Servicios.Compras;
using Application.MainModule.Servicios.Requisicion;
using Sagas.MainModule.Entidades;
using System.Collections.Generic;

namespace Application.MainModule.Flujos
{
    public class Almacen
    {
        public RespuestaDto GenerarEntradaProducto(OrdenCompraEntradasDTO dto)
        {
            List<Sagas.MainModule.Entidades.Almacen> _almacen = new List<Sagas.MainModule.Entidades.Almacen>();
            List<Sagas.MainModule.Entidades.Almacen> _almacenCrear = new List<Sagas.MainModule.Entidades.Almacen>();
            List<AlmacenEntradaProducto> entradas = new List<AlmacenEntradaProducto>();

            foreach (var prod in dto.Productos)
            {
                var _Almacen = ProductoAlmacenServicio.ObtenerAlmacen(prod.IdProducto, dto.IdEmpresa);
                if (_Almacen == null)
                {
                    var nuevoAlmacen = ProductoAlmacenServicio.GenaraAlmacenNuevo(prod.IdProducto, dto.IdEmpresa, prod.Cantidad );
                    nuevoAlmacen = ProductoAlmacenServicio.GenerarAlmacenConEntradaProcuto(prod, dto.IdOrdenCompra, nuevoAlmacen);                    
                    _almacenCrear.Add(nuevoAlmacen);
                }
                else
                {
                    var AlmacenActualizar = ProductoAlmacenServicio.AlmacenEntity(_Almacen);
                    AlmacenActualizar.Cantidad = CalcularAlmacenServicio.ObtenerSumaEntradaAlmacen(AlmacenActualizar.Cantidad, prod.Cantidad);
                    _almacen.Add(AlmacenActualizar);
                    var EntradaProd = ProductoAlmacenServicio.GenerarAlmacenEntradaProcuto(prod, dto.IdOrdenCompra, _Almacen);
                    entradas.Add(EntradaProd);
                }
            }
            var respEntrada = ProductoAlmacenServicio.EntradaAlmcacenProductos(_almacen, _almacenCrear, entradas);
            if (respEntrada.Exito)
            {
                return new Compras().FinalizarEntradaProductoOrdenCompra(new DTOs.OrdenCompraDTO { IdOrdenCompra = dto.IdOrdenCompra });
            }
            return respEntrada;
        }
        public OrdenCompraEntradasDTO BuscarOrdenCompra(int Id)
        {
            var oc = OrdenCompraServicio.Buscar(Id);
            var req = RequisicionServicio.Buscar(oc.IdRequisicion);
            return ProductoAlmacenServicio.AlmacenEntrada(oc, req);
        }
        public List<AlmacenDTO> ProductosAlmacen(short idEmpresa)
        {
            //Validar Permisos

            var prods = ProductoAlmacenServicio.Buscar(idEmpresa);
            return AlmacenProductoAdapter.ToDTO(prods);
        }
        public RespuestaDto ActualizarAlmacen(AlmacenDTO dto)
        {
            //Validar permisos
            var almacen = ProductoAlmacenServicio.ObtenerAlmacen(dto.IdProduto ,dto.IdEmpresa);
            var entity = ProductoAlmacenServicio.AlmacenEntity(almacen);
            var prod = ProductoServicio.ObtenerProducto(dto.IdProduto);

            if (dto.Cantidad > entity.Cantidad)
            {
                //ProductoAlmacenServicio.SalidaAlmcacenProductos();
            }
            else
            {
                entity.Cantidad = dto.Cantidad;              
                var EntradaProd = ProductoAlmacenServicio.GenerarAlmacenEntradaProcuto(new AlmacenEntradaDTO {  }, 0, almacen);
                ProductoAlmacenServicio.EntradaAlmcacenProductos(entity, EntradaProd);
            }
             
            return new RespuestaDto();
        }
    }
}
