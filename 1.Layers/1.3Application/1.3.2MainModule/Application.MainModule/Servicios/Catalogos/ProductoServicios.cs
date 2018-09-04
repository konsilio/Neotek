using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.DTOs.Catalogo;
using Sagas.MainModule.Entidades;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.AdaptadoresDTO.Catalogo;

namespace Application.MainModule.Servicios.Catalogos
{
    public static class ProductoServicios
    {
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
                lp.Add(AdaptadoresDTO.Catalogo.ProductoAdapter.ToDTO(new AccesoADatos.ProductoDataAccess().BuscarPorducto(item.IdProducto)));
            }
            return lp;
        }
        public static Producto ObtenerProdcuto(int idPord)
        {
            return new ProductoDataAccess().BuscarPorducto(idPord);
        }
    }
}
