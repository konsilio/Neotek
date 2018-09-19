using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.UnitOfWork;
using Sagas.MainModule.Entidades;
using Application.MainModule.DTOs.Respuesta;
using Exceptions.MainModule;
using Exceptions.MainModule.Validaciones;

namespace Application.MainModule.Servicios.AccesoADatos
{
    public class ProductoDataAccess
    {
        private SagasDataUow uow;

        public ProductoDataAccess()
        {
            uow = new SagasDataUow();
        }

        public RespuestaDto Insertar(CategoriaProducto cProd)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<CategoriaProducto>().Insert(cProd);
                    uow.SaveChanges();
                    _respuesta.Id = cProd.IdCategoria;
                    _respuesta.EsInsercion = true;
                    _respuesta.Exito = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0002, "de la categaría");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }

        public RespuestaDto Insertar(LineaProducto LProd)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<LineaProducto>().Insert(LProd);
                    uow.SaveChanges();
                    _respuesta.Id = LProd.IdProductoLinea;
                    _respuesta.EsInsercion = true;
                    _respuesta.Exito = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0002, "de la línea de productos");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }

        public RespuestaDto Insertar(UnidadMedida uM)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<UnidadMedida>().Insert(uM);
                    uow.SaveChanges();
                    _respuesta.Id = uM.IdUnidadMedida;
                    _respuesta.EsInsercion = true;
                    _respuesta.Exito = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0002, "de la unidad de medida");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }

        public RespuestaDto Insertar(Producto prod)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Producto>().Insert(prod);
                    uow.SaveChanges();
                    _respuesta.Id = prod.IdProducto;
                    _respuesta.EsInsercion = true;
                    _respuesta.Exito = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0002, "del producto");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }

        public RespuestaDto Actualizar(CategoriaProducto cProd)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<CategoriaProducto>().Update(cProd);
                    uow.SaveChanges();
                    _respuesta.Id = cProd.IdCategoria;
                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0003, "de la categoría"); ;
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }

        public RespuestaDto Actualizar(LineaProducto lProd)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<LineaProducto>().Update(lProd);
                    uow.SaveChanges();
                    _respuesta.Id = lProd.IdProductoLinea;
                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0003, "de la línea de productos"); ;
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }

        public RespuestaDto Actualizar(UnidadMedida uM)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<UnidadMedida>().Update(uM);
                    uow.SaveChanges();
                    _respuesta.Id = uM.IdUnidadMedida;
                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0003, "de la unidad de medida"); ;
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }

        public RespuestaDto Actualizar(Producto prod)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Producto>().Update(prod);
                    uow.SaveChanges();
                    _respuesta.Id = prod.IdCuentaContable;
                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0003, "del producto"); ;
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }

        public List<Producto> ListaProductos()
        {
            return uow.Repository<Producto>().Get(x => x.Activo.Equals(true)).ToList();
        }
        public List<Producto> ListaProductos(short idEmpresa)
        {
            return uow.Repository<Producto>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                    && x.Activo.Equals(true)).ToList();
        }
        public Producto BuscarProducto(int idProducto)
        {
            return uow.Repository<Producto>().GetSingle(x => x.IdProducto.Equals(idProducto)
                                                          && x.Activo);
        }
        public CategoriaProducto BuscarCategoria(short idCategoria)
        {
            return uow.Repository<CategoriaProducto>().GetSingle(x => x.IdCategoria.Equals(idCategoria)
                                                                   && x.Activo);
        }
        public CategoriaProducto BuscarCategoria(short idEmpresa, string nombre)
        {
            return uow.Repository<CategoriaProducto>().GetSingle(x => x.IdEmpresa.Equals(idEmpresa)
                                                                   && x.Nombre.Equals(nombre)
                                                                   && x.Activo);
        }
        public CategoriaProducto BuscarCategoria(short idEmpresa, string nombre, short idCategoria)
        {
            return uow.Repository<CategoriaProducto>().GetSingle(x => x.IdEmpresa.Equals(idEmpresa)
                                                                   && x.Nombre.Equals(nombre)
                                                                   && x.IdCategoria != idCategoria
                                                                   && x.Activo);
        }
        public LineaProducto BuscarLineaProducto(short idLineaProducto)
        {
            return uow.Repository<LineaProducto>().GetSingle(x => x.IdProductoLinea.Equals(idLineaProducto)
                                                               && x.Activo);
        }
        public LineaProducto BuscarLineaProducto(short idEmpresa, string nombre)
        {
            return uow.Repository<LineaProducto>().GetSingle(x => x.IdEmpresa.Equals(idEmpresa)
                                                                   && x.Linea.Equals(nombre)
                                                                   && x.Activo);
        }
        public UnidadMedida BuscarUnidadMedida(short idUnidadMedida)
        {
            return uow.Repository<UnidadMedida>().GetSingle(x => x.IdUnidadMedida.Equals(idUnidadMedida)
                                                              && x.Activo);
        }
        public UnidadMedida BuscarUnidadMedida(short idEmpresa, string nombre, string acronimo)
        {
            return uow.Repository<UnidadMedida>().GetSingle(x => x.IdEmpresa.Equals(idEmpresa)                                                                   
                                                                && x.Activo
                                                                && (x.Nombre.Equals(nombre)
                                                                || x.Acronimo.Equals(acronimo)));
        }
        public List<CategoriaProducto> ListaCategorias()
        {
            return uow.Repository<CategoriaProducto>().Get(x => x.Activo.Equals(true)).ToList();
        }
        public List<CategoriaProducto> ListaCategorias(short idEmpresa)
        {
            return uow.Repository<CategoriaProducto>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                    && x.Activo.Equals(true)).ToList();
        }
        public List<LineaProducto> ListaLineaProductos()
        {
            return uow.Repository<LineaProducto>().Get(x => x.Activo.Equals(true)).ToList();
        }
        public List<LineaProducto> ListaLineaProductos(short idEmpresa)
        {
            return uow.Repository<LineaProducto>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                    && x.Activo.Equals(true)).ToList();
        }
        public List<UnidadMedida> ListaUnidadesMedida()
        {
            return uow.Repository<UnidadMedida>().Get(x => x.Activo.Equals(true)).ToList();
        }
        public List<UnidadMedida> ListaUnidadesMedida(short idEmpresa)
        {
            return uow.Repository<UnidadMedida>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                    && x.Activo.Equals(true)).ToList();
        }

        public List<ProductoAsociado> ListaProductosAsociados(int idProdcuto)
        {
            return uow.Repository<ProductoAsociado>().Get(x => x.IdProducto.Equals(idProdcuto)).ToList();
        }
    }
}
