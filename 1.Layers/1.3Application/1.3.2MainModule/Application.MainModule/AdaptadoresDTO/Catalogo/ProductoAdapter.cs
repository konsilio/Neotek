using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.DTOs.Catalogo;
using Sagas.MainModule.Entidades;
using Application.MainModule.DTOs;

namespace Application.MainModule.AdaptadoresDTO.Seguridad
{
    public static class ProductoAdapter
    {
        #region Categoria Producto
        public static CategoriaProducto CategoriaProducto(CategoriaProductoCrearDto cProDto)
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

        public static CategoriaProducto FromDto(CategoriaProductoModificarDto cProDto, CategoriaProducto catPro)
        {
            var catProducto = FromEntity(catPro);
            catProducto.IdCategoria = cProDto.IdCategoria;
            catProducto.IdEmpresa = cProDto.IdEmpresa;
            catProducto.Nombre = cProDto.Nombre;
            catProducto.Descripcion = cProDto.Descripcion;
            return catProducto;
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

        public static LineaProducto FromDto(LineaProductoModificarDto lProDto, LineaProducto lPro)
        {
            var linProducto = FromEntity(lPro);
            linProducto.IdProductoLinea = lProDto.IdProductoLinea;
            linProducto.IdEmpresa = lProDto.IdEmpresa;
            linProducto.Linea = lProDto.Linea;
            linProducto.Descripcion = lProDto.Descripcion;
            return linProducto;
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

        #region Unidad de Medida
        public static UnidadMedida FromDto(UnidadMedidaCrearDto uMDto)
        {
            return new UnidadMedida()
            {
                IdEmpresa = uMDto.IdEmpresa,
                Nombre = uMDto.Nombre,
                Acronimo = uMDto.Acronimo,
                Descripcion = uMDto.Descripcion,
                FechaRegistro = DateTime.Now,
                Activo = true,
            };
        }

        public static UnidadMedida FromDto(UnidadMedidaModificarDto uMedDto, UnidadMedida uM)
        {
            var uMedida = FromEntity(uM);
            uMedida.IdUnidadMedida = uMedDto.IdUnidadMedida;
            uMedida.IdEmpresa = uMedDto.IdEmpresa;
            uMedida.Nombre = uMedDto.Nombre;
            uMedida.Acronimo = uMedDto.Acronimo;
            uMedida.Descripcion = uMedDto.Descripcion;
            return uMedida;
        }

        public static UnidadMedida FromEntity(UnidadMedida uM)
        {
            return new UnidadMedida()
            {
                IdUnidadMedida = uM.IdUnidadMedida,
                IdEmpresa = uM.IdEmpresa,
                Nombre = uM.Nombre,
                Acronimo = uM.Acronimo,
                Descripcion = uM.Descripcion,
                FechaRegistro = uM.FechaRegistro,
                Activo = uM.Activo,
            };
        }

        public static UnidadMedidaDto ToDTO(UnidadMedida uM)
        {
            return new UnidadMedidaDto()
            {
                IdUnidadMedida = uM.IdUnidadMedida,
                IdEmpresa = uM.IdEmpresa,
                Nombre =uM.Nombre,
                Acronimo = uM.Acronimo,
                Descripcion = uM.Descripcion,
            };
        }

        public static List<UnidadMedidaDto> ToDTO(List<UnidadMedida> usM)
        {
            return usM.Select(x => ToDTO(x)).ToList();
        }
        #endregion

        #region Producto
        public static Producto FromDto(ProductoCrearDto proDto)
        {
            return new Producto()
            {
                IdEmpresa = proDto.IdEmpresa,
                IdProductoServicioTipo = proDto.IdProductoServicioTipo,
                IdCuentaContable = proDto.IdCuentaContable,
                IdCategoria = proDto.IdCategoria,
                IdProductoLinea = proDto.IdProductoLinea,
                IdUnidadMedida = proDto.IdUnidadMedida,
                IdUnidadMedida2 = proDto.IdUnidadMedida2,
                Descripcion = proDto.Descripcion,
                EsActivoVenta = proDto.EsActivoVenta,
                EsGas = proDto.EsGas,
                EsTransporteGas = proDto.EsTransporteGas,
                Minimos = proDto.Minimos,
                Maximo = proDto.Maximo,
                UrlImagen = null,
                PathImagen = null,
                FechaRegistro = DateTime.Now,
                Activo = true,
            };
        }        
        public static Producto FromDto(ProductoModificarDto proDto, Producto pro)
        {
            var producto = FromEntity(pro);
            producto.IdProducto = proDto.IdProducto;
            producto.IdEmpresa = proDto.IdEmpresa;
            producto.IdProductoServicioTipo = proDto.IdProductoServicioTipo;
            producto.IdCuentaContable = proDto.IdCuentaContable;
            producto.IdCategoria = proDto.IdCategoria;
            producto.IdProductoLinea = proDto.IdProductoLinea;
            producto.IdUnidadMedida = proDto.IdUnidadMedida;
            producto.IdUnidadMedida2 = proDto.IdUnidadMedida2;
            producto.Descripcion = proDto.Descripcion;
            producto.EsActivoVenta = proDto.EsActivoVenta;
            producto.EsGas = proDto.EsGas;
            producto.EsTransporteGas = proDto.EsTransporteGas;
            producto.Minimos = proDto.Minimos;
            producto.Maximo = proDto.Maximo;
            return producto;
        }
        public static ProductoDTO ToDTO(Producto _prod)
        {
            ProductoDTO prodDTO = new ProductoDTO();
            prodDTO.IdProducto = _prod.IdProducto;
            prodDTO.IdEmpresa = _prod.IdEmpresa;
            prodDTO.IdProductoServicioTipo = _prod.IdProductoServicioTipo;
            prodDTO.TipoProducto = _prod.TipoServicioOProducto.Nombre;
            prodDTO.IdCategoria = _prod.IdCategoria;
            prodDTO.Categoria = _prod.Categoria.Nombre;
            prodDTO.IdProductoLinea = _prod.IdProductoLinea;
            prodDTO.ProductoLinea = _prod.LineaProducto.Linea;
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
            prodDTO.EsActivoVenta = _prod.EsActivoVenta;
            prodDTO.EsGas = _prod.EsGas;
            prodDTO.EsTransporteGas = _prod.EsTransporteGas;
            prodDTO.IdCuentaContable = _prod.IdCuentaContable;


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
            _prod.IdCuentaContable = prodDTO.IdCuentaContable;
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
                IdCuentaContable = prodAnterior.IdCuentaContable,
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
        #endregion
    }
}
