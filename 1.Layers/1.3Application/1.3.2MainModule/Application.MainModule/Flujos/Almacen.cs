using Application.MainModule.AdaptadoresDTO.Almacen;
using Application.MainModule.DTOs.Almacen;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.Servicios.Almacen;
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
        public RespuestaDto GenerarEntradaProducto(AlmacenCrearEntradaDTO _entrada)
        {
            var Almacen = new AlmacenDataAccess().ProductoAlmacen(_entrada.IdProduto, _entrada.IdEmpresa);
            var AlmacenActualizar = AlmacenAdapter.FromEmtity(Almacen);
            AlmacenActualizar.Cantidad = +_entrada.Cantidad;
            var EntradaProd = ProductoAlmacenServicio.GenerarAlmacenEntradaProcuto(_entrada, Almacen);

            return ProductoAlmacenServicio.EntradaAlmcacenProductos(Almacen, EntradaProd);
        }
        public RespuestaDto GenerarEntradaProducto(List<AlmacenCrearEntradaDTO> _entradas)
        {
            List<Sagas.MainModule.Entidades.Almacen> _almacen = new List<Sagas.MainModule.Entidades.Almacen>();
            List<AlmacenEntradaProducto> entradas = new List<AlmacenEntradaProducto>();
            foreach (var prod in _entradas)
            {
                var Almacen = new AlmacenDataAccess().ProductoAlmacen(prod.IdProduto, prod.IdEmpresa);
                var AlmacenActualizar = AlmacenAdapter.FromEmtity(Almacen);
                AlmacenActualizar.Cantidad = +prod.Cantidad;
                _almacen.Add(AlmacenActualizar);

                var EntradaProd = ProductoAlmacenServicio.GenerarAlmacenEntradaProcuto(prod, Almacen);
                entradas.Add(EntradaProd);
            }
            return ProductoAlmacenServicio.EntradaAlmcacenProductos(_almacen, entradas);
        }
    }
}
