using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.DTOs.Catalogo;
using Sagas.MainModule.Entidades;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.AdaptadoresDTO.Catalogo;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.Seguridad;
using Exceptions.MainModule.Validaciones;

namespace Application.MainModule.Servicios.Catalogos
{
    public static class ProductoServicios
    {
        public static RespuestaDto RegistrarCategoriaProducto(CategoriaProducto cProd)
        {
            return new ProductoDataAccess().Insertar(cProd);
        }
        public static RespuestaDto RegistrarLineaProducto(LineaProducto lProd)
        {
            return new ProductoDataAccess().Insertar(lProd);
        }
        public static RespuestaDto RegistrarUnidadMedida(UnidadMedida uMedida)
        {
            return new ProductoDataAccess().Insertar(uMedida);
        }
        public static RespuestaDto RegistrarProducto(Producto prod)
        {
            return new ProductoDataAccess().Insertar(prod);
        }

        public static RespuestaDto ModificarCategoriaProducto(CategoriaProducto cProd)
        {
            return new ProductoDataAccess().Actualizar(cProd);
        }

        public static RespuestaDto ModificarLineaProducto(LineaProducto lProd)
        {
            return new ProductoDataAccess().Actualizar(lProd);
        }

        public static RespuestaDto ModificarUnidadMedida(UnidadMedida uMedida)
        {
            return new ProductoDataAccess().Actualizar(uMedida);
        }

        public static RespuestaDto ModificarProducto(Producto prod)
        {
            return new ProductoDataAccess().Actualizar(prod);
        }

        public static List<CategoriaProducto> ObtenerCategorias()
        {
            var empresa = EmpresaServicio.Obtener(TokenServicio.ObtenerIdEmpresa());

            if (empresa.EsAdministracionCentral)
                return new ProductoDataAccess().ListaCategorias();
            else
                return new ProductoDataAccess().ListaCategorias(empresa.IdEmpresa);
        }
        public static List<LineaProducto> ObtenerLineasProducto()
        {
            var empresa = EmpresaServicio.Obtener(TokenServicio.ObtenerIdEmpresa());

            if (empresa.EsAdministracionCentral)
                return new ProductoDataAccess().ListaLineaProductos();
            else
                return new ProductoDataAccess().ListaLineaProductos(empresa.IdEmpresa);
        }
        public static List<UnidadMedida> ObtenerUnidadesMedida()
        {
            var empresa = EmpresaServicio.Obtener(TokenServicio.ObtenerIdEmpresa());

            if (empresa.EsAdministracionCentral)
                return new ProductoDataAccess().ListaUnidadesMedida();
            else
                return new ProductoDataAccess().ListaUnidadesMedida(empresa.IdEmpresa);
        }
        public static List<Producto> Obtener()
        {
            var empresa = EmpresaServicio.Obtener(TokenServicio.ObtenerIdEmpresa());

            if (empresa.EsAdministracionCentral)
                return new ProductoDataAccess().ListaProductos();
            else
                return new ProductoDataAccess().ListaProductos(empresa.IdEmpresa);
        }

        public static CategoriaProducto ObtenerCategoria(short idCategoria)
        {
            return new ProductoDataAccess().BuscarCategoria(idCategoria);
        }

        public static LineaProducto ObtenerLineaProducto(short idLineaProducto)
        {
            return new ProductoDataAccess().BuscarLineaProducto(idLineaProducto);
        }
        public static UnidadMedida ObtenerUnidadMedida(short idUnidadMedida)
        {
            return new ProductoDataAccess().BuscarUnidadMedida(idUnidadMedida);
        }
        public static Producto ObtenerProducto(int idProducto)
        {
            return new ProductoDataAccess().BuscarProducto(idProducto);
        }        
        public static List<Producto> ListaProductos()
        {
            var empresa = EmpresaServicio.Obtener(TokenServicio.ObtenerIdEmpresa());

            if (empresa.EsAdministracionCentral)
                return new ProductoDataAccess().ListaProductos();
            else
                return new ProductoDataAccess().ListaProductos(empresa.IdEmpresa);
        }
        public static List<ProductoAsociado> ListaProductoAsociados(int idProducto)
        {
            return new ProductoDataAccess().ListaProductosAsociados(idProducto);
        }
        public static List<ProductoAsociado> ListaProductoAsociados(Producto producto)
        {
            if (producto != null)
                if (producto.ProductoAsociado != null)
                    return producto.ProductoAsociado.ToList();
            
            return ListaProductoAsociados(producto.IdProducto);
        }
        public static Producto ObtenerProductoAsociado(int idproAsociado)
        {
            return new ProductoDataAccess().BuscarProducto(idproAsociado);
        }
        public static Producto ObtenerProducto(ProductoAsociado proAsociado)
        {
            if (proAsociado != null)
            {
                if (proAsociado.Producto != null)
                    return proAsociado.ProdAsociado;
            }

            return ObtenerProductoAsociado(proAsociado.IdProductoAsociado);
        }
        public static List<Producto> ListaProductoAsociados(List<ProductoAsociado> lprdAso)
        {
            return lprdAso.Select(x => ObtenerProducto(x)).ToList();
        }
        public static bool ExisteCategoria(string nombre)
        {
            var proDAccess = new ProductoDataAccess();
            var idEmpresa = TokenServicio.ObtenerIdEmpresa();

            var categoria = proDAccess.BuscarCategoria(idEmpresa, nombre);
            if (categoria != null) return true;

            return false;
        }

        public static bool ExisteLinea(string nombre)
        {
            var proDAccess = new ProductoDataAccess();
            var idEmpresa = TokenServicio.ObtenerIdEmpresa();

            var categoria = proDAccess.BuscarLineaProducto(idEmpresa, nombre);
            if (categoria != null) return true;

            return false;
        }

        public static bool ExisteUnidadMedida(string nombre, string acronimo)
        {
            var proDAccess = new ProductoDataAccess();
            var idEmpresa = TokenServicio.ObtenerIdEmpresa();

            var categoria = proDAccess.BuscarUnidadMedida(idEmpresa, nombre, acronimo);
            if (categoria != null) return true;

            return false;
        }

        public static RespuestaDto NoExiste(string paramtro)
        {
            string mensaje = string.Format(Error.NoExiste, paramtro);

            return new RespuestaDto()
            {
                ModeloValido = true,
                Mensaje = mensaje,
                MensajesError = new List<string>() { mensaje },
            };
        }
    }
}
