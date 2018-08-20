using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.DTOs.Catalogo;
using Sagas.MainModule.Entidades;

namespace Application.MainModule.AdaptadoresDTO.Catalogo
{
    public static class ProductoAdapter
    {
        public static ProductoDTO ToDTO(Producto _prod)
        {
            ProductoDTO prodDTO = new ProductoDTO();
            prodDTO.IdProducto = _prod.IdProducto;
            prodDTO.IdEmpresa = _prod.IdEmpresa;
            prodDTO.IdProductoServicioTipo = _prod.IdProductoServicioTipo;
            prodDTO.TipoProducto = _prod.TipoServicioOProducto.Descripcion;
            prodDTO.IdCategoria = _prod.IdCategoria;
            prodDTO.IdProductoLinea = _prod.IdProductoLinea;
            prodDTO.IdUnidadMedida = _prod.IdUnidadMedida;
            prodDTO.UnidadMedida = _prod.UnidadMedida.Acronimo;
            prodDTO.IdUnidadMedida2 = _prod.IdUnidadMedida2;
            prodDTO.Descripcion = _prod.Descripcion;
            prodDTO.Minimos = _prod.Minimos;
            prodDTO.Maximo = _prod.Maximo;
            prodDTO.UrlImagen = _prod.UrlImagen;
            prodDTO.PathImagen = _prod.PathImagen;
            prodDTO.Activo = _prod.Activo;
            prodDTO.FechaRegistro = _prod.FechaRegistro;
            return prodDTO;
        }
        public static List<ProductoDTO> ToDTO(List<Producto> lProd)
        {
            List<ProductoDTO> lprodDTO = lProd.ToList().Select(x => ToDTO(x)).ToList();
            return lprodDTO;
        }
        public static Producto FromDTO(ProductoDTO prodDTO)
        {
            Producto _prod = new Producto();
            _prod.IdProducto = prodDTO.IdProducto;
            _prod.IdEmpresa = prodDTO.IdEmpresa;
            _prod.IdProductoServicioTipo = prodDTO.IdProductoServicioTipo;
            _prod.TipoServicioOProducto.Descripcion = prodDTO.TipoProducto;
            _prod.IdCategoria = prodDTO.IdCategoria;
            _prod.IdProductoLinea = prodDTO.IdProductoLinea;
            _prod.IdUnidadMedida = prodDTO.IdUnidadMedida;
            _prod.UnidadMedida.Descripcion = prodDTO.UnidadMedida;
            _prod.IdUnidadMedida2 = prodDTO.IdUnidadMedida2;
            _prod.Descripcion = prodDTO.Descripcion;
            _prod.Minimos = prodDTO.Minimos;
            _prod.Maximo = prodDTO.Maximo;
            _prod.UrlImagen = prodDTO.UrlImagen;
            _prod.PathImagen = prodDTO.PathImagen;
            _prod.Activo = prodDTO.Activo;
            _prod.FechaRegistro = prodDTO.FechaRegistro;
            return _prod;
        }
        public static List<Producto> FromDTO(List<ProductoDTO> lProdDTO)
        {
            List<Producto> lprod = lProdDTO.ToList().Select(x => FromDTO(x)).ToList();
            return lprod;
        }

    }
}
