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
using Exceptions.MainModule.Validaciones;

namespace Application.MainModule.Servicios.Almacen
{
    public static class ProductoAlmacenServicio
    {

        public static RespuestaDto EntradaAlmcacenProductos(Sagas.MainModule.Entidades.Almacen _almacen, AlmacenEntradaProducto prod)
        {
            return new AlmacenDataAccess().ActualizarAlmacenEntradas(_almacen, prod);
        }
        public static RespuestaDto SalidaAlmcacenProductos(Sagas.MainModule.Entidades.Almacen _almacen, AlmacenSalidaProducto prod)
        {
            return new AlmacenDataAccess().ActualizarAlmacenSalida(_almacen, prod);
        }
        public static RespuestaDto Actualiza(Sagas.MainModule.Entidades.Almacen _almacen)
        {
            return new AlmacenDataAccess().Actualizar(_almacen);
        }
        public static RespuestaDto EntradaAlmcacenProductos(List<Sagas.MainModule.Entidades.Almacen> _almacen, List<Sagas.MainModule.Entidades.Almacen> _almacenCrear, List<AlmacenEntradaProducto> prod, OrdenCompra oc, List<OrdenCompraProducto> ocp)
        {
            return new AlmacenDataAccess().ActualizarAlmacenEntradas(_almacen, _almacenCrear, prod, oc, ocp);
        }
        public static RespuestaDto SalidaAlmcacenProductos(List<Sagas.MainModule.Entidades.Almacen> _almacen, List<AlmacenSalidaProducto> prod, Sagas.MainModule.Entidades.Requisicion _requisicion, List<RequisicionProducto> _productos)
        {
            return new AlmacenDataAccess().ActualizarAlmacenSalidas(_almacen, prod, _requisicion, _productos);
        }
        public static AlmacenEntradaProducto GenerarAlmacenEntradaProcuto(AlmacenEntradaDTO dto, int idOC, Sagas.MainModule.Entidades.Almacen _alm)
        {
            return AlmacenProductoAdapter.FromDTO(dto, idOC, _alm);
        }
        public static AlmacenSalidaProducto GenerarAlmacenSalidaProcuto(AlmacenSalidaProductoDTO dto, int idOC, Sagas.MainModule.Entidades.Almacen _alm)
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
        public static List<Sagas.MainModule.Entidades.Almacen> BuscarAlmacen(short idEmpresa)
        {
            if (TokenServicio.ObtenerEsAdministracionCentral())
                if (idEmpresa.Equals(0))
                    return new AlmacenDataAccess().ListaProductosAlmacenTodos();
                else
                    return new AlmacenDataAccess().ListaProductosAlmacen(idEmpresa);
            else
                return new AlmacenDataAccess().ListaProductosAlmacen(TokenServicio.ObtenerIdEmpresa());
        }
        public static List<AlmacenEntradaProducto> BuscarEntradasTodo(short idEmpresa)
        {
            if (TokenServicio.ObtenerEsAdministracionCentral())
                if (idEmpresa.Equals(0))
                    return new AlmacenEntradaProductoDataAccess().BuscarTodos();
                else
                    return new AlmacenEntradaProductoDataAccess().BuscarTodos(idEmpresa);
            else
                return new AlmacenEntradaProductoDataAccess().BuscarTodos(TokenServicio.ObtenerIdEmpresa());
        }
        public static List<AlmacenSalidaProducto> BuscarSalidaTodo(short idEmpresa)
        {
            if (TokenServicio.ObtenerEsAdministracionCentral())
                if (idEmpresa.Equals(0))
                    return new AlmacenSalidaProductoDataAccess().BuscarTodos();
                else
                    return new AlmacenSalidaProductoDataAccess().BuscarTodos(idEmpresa);
            else
                return new AlmacenSalidaProductoDataAccess().BuscarTodos(TokenServicio.ObtenerIdEmpresa());
        }
        public static List<RegistroDTO> UnirRegistros(List<AlmacenSalidaProducto> Salidas, List<AlmacenEntradaProducto> Entradas)
        {
            List<RegistroDTO> Registro = new List<RegistroDTO>();
            foreach (var _Salida in Salidas)
            {
                RegistroDTO dto = new RegistroDTO()
                {
                    IdProducto = _Salida.IdProducto,
                    IdCategoria = _Salida.Productos.IdCategoria,
                    IdProductoLinea = _Salida.Productos.IdProductoLinea,
                    IdEmpresa = _Salida.Almacen.IdEmpresa,
                    NombreEmpresa = _Salida.Almacen.Empresa.NombreComercial,
                    Referencia = _Salida.Observaciones_,
                    Descripcion = _Salida.Productos.Descripcion,
                    CantidadAnterior = _Salida.CantidadAnterior,
                    Cantidad = _Salida.Cantidad,
                    CantidadFinal = _Salida.CantidadFinal,
                    FechaRegistro = _Salida.FechaRegistro,
                    EsEntrada = false,
                    EsSalida = true,
                };
                Registro.Add(dto);
            }
            foreach (var _Entrada in Entradas)
            {
                RegistroDTO dto = new RegistroDTO()
                {
                    IdProducto = _Entrada.IdProducto,
                    IdCategoria = _Entrada.Productos.IdCategoria,
                    IdProductoLinea = _Entrada.Productos.IdProductoLinea,
                    IdEmpresa = _Entrada.Almacen.IdEmpresa,
                    NombreEmpresa = _Entrada.Almacen.Empresa.NombreComercial,
                    Referencia = _Entrada.Observaciones_,
                    Descripcion = _Entrada.Productos.Descripcion,
                    CantidadAnterior  =_Entrada.CantidadAnterior,
                    Cantidad = _Entrada.Cantidad,
                    CantidadFinal = _Entrada.CantidadFinal,
                    FechaRegistro = _Entrada.FechaRegistro,
                    EsEntrada = true,
                    EsSalida = false,
                };
                Registro.Add(dto);
            }
            return Registro.OrderBy(x => x.FechaRegistro).ToList();
        }
        public static RespuestaDto NoExiste()
        {
            string mensaje = string.Format(Error.NoExiste, "El prodcuto en almacen ");

            return new RespuestaDto()
            {
                Exito = false,
                ModeloValido = true,
                Mensaje = mensaje,
                MensajesError = new List<string>() { mensaje },
            };
        }
        public static RespuestaDto CantidadInsuficiente()
        {
            string mensaje = string.Format(Error.A0002);

            return new RespuestaDto()
            {
                Exito = false,
                ModeloValido = true,
                Mensaje = mensaje,
                MensajesError = new List<string>() { mensaje },
            };
        }
    }
}
