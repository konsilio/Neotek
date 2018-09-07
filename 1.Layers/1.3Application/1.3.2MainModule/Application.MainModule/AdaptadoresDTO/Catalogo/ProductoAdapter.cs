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
        #region Categoria Producto
        public static CategoriaProducto FromDto(CategoriaProductoCrearDto cProDto)
        {
            return new CategoriaProducto()
            {
                IdEmpresa = cProDto.IdEmpresa,
                Nombre = cProDto.Nombre,
                Descripcion = cProDto.Descripcion,
                FechaRegistro = DateTime.Now,
                Activo = true,
            };
        }

        public static CategoriaProducto FromDto(CategoriaProductoModificarDto cProDto)
        {
            return new CategoriaProducto()
            {
                IdCategoria = cProDto.IdCategoria,
                IdEmpresa = cProDto.IdEmpresa,
                Nombre = cProDto.Nombre,
                Descripcion = cProDto.Descripcion,
            };
        }

        public static CategoriaProducto FromEntity(CategoriaProducto antCatProducto)
        {
            return new CategoriaProducto()
            {
                IdCategoria = antCatProducto.IdCategoria,
                IdEmpresa = antCatProducto.IdEmpresa,
                Nombre = antCatProducto.Nombre,
                Descripcion = antCatProducto.Descripcion,
                FechaRegistro = antCatProducto.FechaRegistro,
                Activo = antCatProducto.Activo,
            };
        }

        public static CategoriaProductoDto ToDTO(CategoriaProducto _cProd)
        {
            return new CategoriaProductoDto()
            {
                IdCategoria = _cProd.IdCategoria,
                IdEmpresa = _cProd.IdEmpresa,
                Descripcion = _cProd.Descripcion,
                Nombre = _cProd.Nombre
            };
        }

        public static List<CategoriaProductoDto> ToDTO(List<CategoriaProducto> _cProd)
        {
            return _cProd.Select(x => ToDTO(x)).ToList();
        }
        #endregion

        #region Linea Producto
        public static LineaProducto FromDto(LineaProductoCrearDto lProDto)
        {
            return new LineaProducto()
            {
                IdEmpresa = lProDto.IdEmpresa,
                Linea = lProDto.Linea,
                Descripcion = lProDto.Descripcion,
                FechaRegistro = DateTime.Now,
                Activo = true,
            };
        }

        public static LineaProducto FromDto(LineaProductoModificarDto lProDto)
        {
            return new LineaProducto()
            {
                IdProductoLinea = lProDto.IdProductoLinea,
                IdEmpresa = lProDto.IdEmpresa,
                Linea = lProDto.Linea,
                Descripcion = lProDto.Descripcion,
            };
        }

        public static LineaProducto FromEntity(LineaProducto antCatProducto)
        {
            return new LineaProducto()
            {
                IdProductoLinea = antCatProducto.IdProductoLinea,
                IdEmpresa = antCatProducto.IdEmpresa,
                Linea = antCatProducto.Linea,
                Descripcion = antCatProducto.Descripcion,
                FechaRegistro = antCatProducto.FechaRegistro,
                Activo = antCatProducto.Activo,
            };
        }

        public static LineaProductoDto ToDTO(LineaProducto _lProd)
        {
            return new LineaProductoDto()
            {
                IdProductoLinea = _lProd.IdProductoLinea,
                IdEmpresa = _lProd.IdEmpresa,
                Descripcion = _lProd.Descripcion,
                Linea = _lProd.Linea
            };
        }

        public static List<LineaProductoDto> ToDTO(List<LineaProducto> _lProd)
        {
            return _lProd.Select(x => ToDTO(x)).ToList();
        }
        #endregion

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
        public static Producto FromEntity(Producto prodAnterior)
        {
            return new Producto()
            {
                IdProducto = prodAnterior.IdProducto,
                IdEmpresa = prodAnterior.IdEmpresa,
                IdProductoServicioTipo = prodAnterior.IdProductoServicioTipo,
                IdCategoria = prodAnterior.IdCategoria,
                IdProductoLinea = prodAnterior.IdProductoLinea,
                IdUnidadMedida = prodAnterior.IdUnidadMedida,
                IdUnidadMedida2 = prodAnterior.IdUnidadMedida2,
                Descripcion = prodAnterior.Descripcion,
                Minimos = prodAnterior.Minimos,
                Maximo = prodAnterior.Maximo,
                UrlImagen = prodAnterior.UrlImagen,
                PathImagen = prodAnterior.PathImagen,
                Activo = prodAnterior.Activo,
                FechaRegistro = prodAnterior.FechaRegistro,
                EsActivoVenta = prodAnterior.EsActivoVenta,
                EsGas = prodAnterior.EsGas,
                EsTransporteGas = prodAnterior.EsTransporteGas,
            };
        }

        public static List<Producto> FromEntity(List<Producto> lProdDTO)
        {
            return lProdDTO.ToList().Select(x => FromEntity(x)).ToList();
        }
    }
}
