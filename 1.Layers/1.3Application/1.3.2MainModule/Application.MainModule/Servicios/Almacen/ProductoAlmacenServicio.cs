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
using Sagas.MainModule.ObjetosValor.Constantes;

namespace Application.MainModule.Servicios.Almacen
{
    public static class ProductoAlmacenServicio
    {
       
        public static RespuestaDto EntradaAlmcacenProductos(Sagas.MainModule.Entidades.Almacen _almacen, AlmacenEntradaProducto prod)
        {
            return new AlmacenDataAccess().ActualizarAlmacenEntradas(_almacen, prod);
        }
        public static RespuestaDto EntradaAlmcacenProductos(List<Sagas.MainModule.Entidades.Almacen> _almacen, List<Sagas.MainModule.Entidades.Almacen> _almacenCrear, List<AlmacenEntradaProducto> prod)
        {
            return new AlmacenDataAccess().ActualizarAlmacenEntradas(_almacen, _almacenCrear, prod);
        }
        public static AlmacenEntradaProducto GenerarAlmacenEntradaProcuto(AlmacenEntradaDTO dto, int idOC, Sagas.MainModule.Entidades.Almacen _alm)
        {
            return AlmacenProductoAdapter.FromDTO(dto, idOC, _alm);
        }
        public static Sagas.MainModule.Entidades.Almacen GenerarAlmacenConEntradaProcuto(AlmacenEntradaDTO dto, int idOC, Sagas.MainModule.Entidades.Almacen _alm)
        {
            _alm.Entradas.Add(AlmacenProductoAdapter.FromDTO(dto, idOC, _alm));
            return _alm;
        }
        public static OrdenCompraEntradasDTO AlmacenEntrada(OrdenCompra oc, Sagas.MainModule.Entidades.Requisicion req)
        {
            return AlmacenProductoAdapter.ToDTO(oc, req);
        }
        public static Sagas.MainModule.Entidades.Almacen ObtenerAlmacen(int Idpord, short idEmpresa)
        {
            return new AlmacenDataAccess().ProductoAlmacen(Idpord, idEmpresa);
        }
        public static RespuestaDto InsertarAlmacen(Sagas.MainModule.Entidades.Almacen almacen)
        {          
            return new AlmacenDataAccess().Insertar(almacen);
        }
        public static Sagas.MainModule.Entidades.Almacen AlmacenEntity(Sagas.MainModule.Entidades.Almacen almacen)
        {
            return AlmacenAdapter.FromEmtity(almacen);
        }
        public static RespuestaDto InsertarAlmacenEntrada(List<Sagas.MainModule.Entidades.Almacen> _almacen, List<AlmacenEntradaProducto> prod)
        {
            return new AlmacenDataAccess().InsertarAlmacenEntradas(_almacen, prod);
        }
        public static Sagas.MainModule.Entidades.Almacen GenaraAlmacenNuevo(int Idpord, short idEmpresa, decimal cantidad)
        {
            return new Sagas.MainModule.Entidades.Almacen()
            {
                IdEmpresa = idEmpresa,
                IdProduto = Idpord,
                FechaRegistro = DateTime.Now,
                FechaActualizacion = DateTime.Now,
                Cantidad = cantidad,
                Ubicacion = AlmacenConst.UbicacionPendiente,
                Entradas = new List<AlmacenEntradaProducto>()               
            };
        }        
    }
}
