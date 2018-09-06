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

        public static List<CentroCosto> Obtener()
        {
            var empresa = new EmpresaDataAccess().Buscar(TokenServicio.ObtenerIdEmpresa());

            if (empresa.EsAdministracionCentral)
                return new CentroCostoDataAccess().BuscarTodos();
            else
                return new CentroCostoDataAccess().BuscarTodos(empresa.IdEmpresa);
        }

        public static CentroCosto Obtener(int IdCentroCosto)
        {
            return new CentroCostoDataAccess().Buscar(IdCentroCosto);
        }

        public static bool Existe(string numero, string descripcion)
        {
            var ccDAccess = new CentroCostoDataAccess();
            var idEmpresa = TokenServicio.ObtenerIdEmpresa();

            var centro = ccDAccess.BuscarNumero(idEmpresa, numero);
            if (centro != null) return true;

            centro = ccDAccess.BuscarDescripcion(idEmpresa, descripcion);
            if (centro != null) return true;

            return false;
        }

        public static RespuestaDto NoExiste()
        {
            string mensaje = string.Format(Error.NoExiste, "El Centro de Costo");

            return new RespuestaDto()
            {
                ModeloValido = true,
                Mensaje = mensaje,
                MensajesError = new List<string>() { mensaje },
            };
        }





        public static List<ProductoDTO> ListaProductos(short idEpresa)
        {
            return ProductoAdapter.ToDTO(new ProductoDataAccess().ListaProductos(idEpresa));
        }
        public static List<ProductoAsociado> ListaProductoAsociados(int idProducto)
        {
            return new ProductoDataAccess().ListaProductosAsociados(idProducto);
        }
        public static List<ProductoDTO> ListaProductoAsociados(List<ProductoAsociado> lprdAso)
        {
            List<ProductoDTO> lp = new List<ProductoDTO>();
            foreach (var item in lprdAso)
            {
                lp.Add(AdaptadoresDTO.Catalogo.ProductoAdapter.ToDTO(new AccesoADatos.ProductoDataAccess().BuscarProducto(item.IdProducto)));
            }
            return lp;
        }
        public static Producto ObtenerProdcuto(int idPord)
        {
            return new ProductoDataAccess().BuscarProducto(idPord);
        }
    }
}
