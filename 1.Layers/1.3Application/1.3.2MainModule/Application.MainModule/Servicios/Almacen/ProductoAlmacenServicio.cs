using Application.MainModule.DTOs.Respuesta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sagas.MainModule.Entidades;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.DTOs.Almacen;
using Application.MainModule.Servicios.Seguridad;
using Application.MainModule.Servicios.Catalogos;
using Application.MainModule.DTOs.Compras;
using Application.MainModule.AdaptadoresDTO.Almacen;

namespace Application.MainModule.Servicios.Almacen
{
    public static class ProductoAlmacenServicio
    {
        public static Sagas.MainModule.Entidades.Requisicion CalcularAlmacenProcutos(Sagas.MainModule.Entidades.Requisicion _requisicion)
        {
            int x = 0;
            foreach (RequisicionProducto _prod in _requisicion.Productos)
            {
                var prod = ProductoServicio.ObtenerProducto(_prod.IdProducto);
                if (prod.EsGas)
                {
                    _requisicion.Productos.ElementAt(x).CantidadAlmacenActual = AlmacenGasServicio.ObtenerCantidadActualAlmacenGeneral(_requisicion.IdEmpresa);
                    _requisicion.Productos.ElementAt(x).CantidadAComprar = _prod.Cantidad;
                }
                else
                {
                    var almacen = new AlmacenDataAccess().ProductoAlmacen(_prod.IdProducto, _requisicion.IdEmpresa);
                    _requisicion.Productos.ElementAt(x).CantidadAlmacenActual = almacen != null ? almacen.Cantidad : 0;
                    if (_prod.CantidadAlmacenActual.Value > _prod.Cantidad)
                        _requisicion.Productos.ElementAt(x).CantidadAComprar = 0;
                    else
                        _requisicion.Productos.ElementAt(x).CantidadAComprar = Math.Abs(_prod.CantidadAlmacenActual.Value - _prod.Cantidad);
                }
                _requisicion.Productos.ElementAt(x).EsActivoVenta = prod.EsActivoVenta;
                _requisicion.Productos.ElementAt(x).EsGas = prod.EsGas;
                _requisicion.Productos.ElementAt(x).EsTransporteGas = prod.EsTransporteGas;

                x++;
            }
            return _requisicion;
        }
        public static RespuestaDto EntradaAlmcacenProductos(Sagas.MainModule.Entidades.Almacen _almacen, AlmacenEntradaProducto prod)
        {
            return new AlmacenDataAccess().ActualizarAlmacenEntradas(_almacen, prod);
        }
        public static RespuestaDto EntradaAlmcacenProductos(List<Sagas.MainModule.Entidades.Almacen> _almacen, List<AlmacenEntradaProducto> prod)
        {
            return new AlmacenDataAccess().ActualizarAlmacenEntradas(_almacen, prod);
        }
        public static AlmacenEntradaProducto GenerarAlmacenEntradaProcuto(AlmacenEntradaDTO dto, int idOC, Sagas.MainModule.Entidades.Almacen _alm)
        {
            return AlmacenProductoAdapter.FromDTO(dto, idOC, _alm);
        }
        public static OrdenCompraEntradasDTO AlmacenEntrada(OrdenCompra oc, Sagas.MainModule.Entidades.Requisicion req)
        {
            return AlmacenProductoAdapter.ToDTO(oc, req);
        }
        public static Sagas.MainModule.Entidades.Almacen ObtenerAlmacen(int Idpord, short idEmpresa)
        {
            return new AlmacenDataAccess().ProductoAlmacen(Idpord, idEmpresa);
        }
        public static Sagas.MainModule.Entidades.Almacen AlmacenEmtity(Sagas.MainModule.Entidades.Almacen almacen)
        {
           return AlmacenAdapter.FromEmtity(almacen);
        }
    }
}
