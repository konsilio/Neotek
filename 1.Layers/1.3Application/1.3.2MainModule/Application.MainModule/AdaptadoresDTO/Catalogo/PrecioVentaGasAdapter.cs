using Application.MainModule.DTOs;
using Application.MainModule.DTOs.Catalogo;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.Servicios.Catalogos;
using Application.MainModule.Servicios.Seguridad;
using Application.MainModule.Servicios.Ventas;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO.Catalogo
{
    public class PrecioVentaGasAdapter
    {
        public static PrecioVentaDTO ToDTO(PrecioVenta pv)
        {
            //var nombreEmp = EmpresaServicio.Obtener(pv.IdEmpresa).NombreComercial;
            try
            {
                var lol = pv.CEstacionCarburacion != null ? pv.CEstacionCarburacion.Nombre : "General";
                PrecioVentaDTO usDTO = new PrecioVentaDTO()
                {
                    IdPrecioVenta = pv.IdPrecioVenta,
                    IdEmpresa = pv.IdEmpresa,
                    IdEstacion = pv.IdEstacion ?? 0,
                    Estacion = pv.CEstacionCarburacion != null ? pv.CEstacionCarburacion.Nombre : "General",
                    IdPrecioVentaEstatus = pv.IdPrecioVentaEstatus,
                    IdCategoria = pv.IdCategoria,
                    IdProductoLinea = pv.IdProductoLinea,
                    IdProducto = pv.IdProducto,
                    Categoria = pv.Categoria,
                    Linea = pv.Linea,
                    Producto = pv.Producto,
                    PrecioActual = pv.PrecioActual ?? 0,
                    PrecioPemexKg = pv.PrecioPemexKg ?? 0,
                    PrecioPemexLt = pv.PrecioPemexLt ?? 0,
                    UtilidadEsperadaKg = pv.UtilidadEsperadaKg ?? 0,
                    UtilidadEsperadaLt = pv.UtilidadEsperadaLt ?? 0,
                    PrecioSalida = pv.PrecioSalida,
                    PrecioSalidaKg = pv.PrecioSalidaKg,
                    PrecioSalidaLt = pv.PrecioSalidaLt,
                    EsGas = pv.EsGas,
                    FechaProgramada = pv.FechaProgramada,
                    FechaVencimiento = pv.FechaVencimiento,
                    Activo = pv.Activo,
                    FechaRegistro = pv.FechaRegistro,
                    Empresa = EmpresaServicio.Obtener(pv.IdEmpresa).NombreComercial,
                    PrecioVentaEstatus = PrecioVentaGasServicio.Obtener(pv.IdPrecioVentaEstatus).Descripcion,
                    CategoriaProducto = ProductoServicio.ObtenerProducto((pv.IdProducto)).Descripcion,//Concepto
                };
                return usDTO;

            }
            catch (Exception ex)
            {
                throw;
            }
          
        }
        public static List<PrecioVentaDTO> ToDTO(List<PrecioVenta> lu)
        {
            
            List<PrecioVentaDTO> luDTO = lu.ToList().Select(x => ToDTO(x)).ToList();            
            
            return luDTO;
        }
      
        public static PrecioVentaEstatusDTO ToDTOs(PrecioVentaEstatus pv)
        {

            PrecioVentaEstatusDTO usDTO = new PrecioVentaEstatusDTO()
            {
                IdPrecioVentaEstatus = pv.IdPrecioVentaEstatus,
                Descripción = pv.Descripcion,
                Activo = pv.Activo,
                FechaRegsitro = pv.FechaRegsitro,
            };
            return usDTO;
        }
        public static List<PrecioVentaEstatusDTO> ToDTOEstatus(List<PrecioVentaEstatus> lu)
        {
            List<PrecioVentaEstatusDTO> luDTO = lu.ToList().Select(x => ToDTOs(x)).ToList();
            return luDTO;
        }
        public static PrecioVenta FromTo(PrecioVentaDTO PVGasDTO)
        {
            var Prod = ProductoServicio.Obtener(EmpresaServicio.Obtener(PVGasDTO.IdEmpresa));
            var factorLtaKg = EmpresaServicio.Obtener(PVGasDTO.IdEmpresa).FactorLitrosAKilos;
            var IdStatus = CalcularPreciosVentaServicio.GetEstatusPrecioVenta(PVGasDTO.PrecioVentaEstatus);
            var IdPreVenta = PVGasDTO.IdPrecioVenta == 0 ? 1 : PrecioVentaGasServicio.ObtenerUltimoIdPrecioVenta() + 1;
            var flete = PVGasDTO.PrecioFlete != null ? (decimal)PVGasDTO.PrecioFlete : 0;
            return new PrecioVenta()
            {
                IdPrecioVenta = PVGasDTO.IdPrecioVenta,//(short)IdPreVenta,
                IdEmpresa = PVGasDTO.IdEmpresa,
                IdPrecioVentaEstatus = IdStatus,
                IdCategoria = PVGasDTO.IdCategoria,
                IdProductoLinea = PVGasDTO.IdProductoLinea,
                IdProducto = PVGasDTO.IdProducto,
                Categoria = PVGasDTO.Categoria,
                Linea = PVGasDTO.Linea,
                Producto = PVGasDTO.Producto,
                PrecioActual = PVGasDTO.PrecioActual ?? 0,
                PrecioPemexKg = PVGasDTO.PrecioPemexKg ?? 0,
                PrecioPemexLt = PVGasDTO.PrecioPemexLt ?? 0,
                UtilidadEsperadaKg = PVGasDTO.UtilidadEsperadaKg ?? 0,
                UtilidadEsperadaLt = PVGasDTO.UtilidadEsperadaLt ?? 0,
                PrecioSalida = PVGasDTO.PrecioSalida,
                PrecioSalidaKg = CalcularPreciosVentaServicio.ObtenerPrecioSalidaKg((decimal)PVGasDTO.PrecioPemexKg, (decimal)PVGasDTO.UtilidadEsperadaKg, flete),//PVGasDTO.PrecioSalidaKg,
                PrecioSalidaLt = CalcularPreciosVentaServicio.ObtenerPrecioSalidaLt(((decimal)PVGasDTO.PrecioPemexKg + flete), factorLtaKg),
                EsGas = PVGasDTO.EsGas,
                FechaProgramada = PVGasDTO.FechaProgramada,
                FechaVencimiento = PVGasDTO.FechaVencimiento,
                Activo = true,
                FechaRegistro = DateTime.Now

            };
        }
        public static PrecioVenta FromTO(PrecioVentaDTO PVGasDTO)
        {
            //var Prod = ProductoServicio.Obtener(EmpresaServicio.Obtener(PVGasDTO.IdEmpresa));
            //var factorLtaKg = EmpresaServicio.Obtener(PVGasDTO.IdEmpresa).FactorLitrosAKilos;
            var IdStatus = CalcularPreciosVentaServicio.GetEstatusPrecioVenta(PVGasDTO.PrecioVentaEstatus);
            var IdPreVenta = new PrecioVentaDataAccess().BuscarUltimoPrecio() + 1;//PrecioVentaGasServicio.ObtenerUltimoIdPrecioVenta() + 1;
            var flete = PVGasDTO.PrecioFlete != null ? (decimal)PVGasDTO.PrecioFlete : 0;         
             return new PrecioVenta()
            {
                IdPrecioVenta = (short)IdPreVenta,
                IdEmpresa = PVGasDTO.IdEmpresa,
                IdEstacion = PVGasDTO.IdEstacion != 0 ? PVGasDTO.IdEstacion : null,
                IdPrecioVentaEstatus = IdStatus,
                IdCategoria = PVGasDTO.IdCategoria,
                IdProductoLinea = PVGasDTO.IdProductoLinea,
                IdProducto = PVGasDTO.IdProducto,
                Categoria = PVGasDTO.Categoria,
                Linea = PVGasDTO.Linea,
                Producto = PVGasDTO.Producto,
                PrecioActual = PVGasDTO.PrecioActual,
                PrecioPemexKg = PVGasDTO.PrecioPemexKg,
                PrecioPemexLt = PVGasDTO.PrecioPemexLt,
                UtilidadEsperadaKg = PVGasDTO.UtilidadEsperadaKg,
                UtilidadEsperadaLt = PVGasDTO.UtilidadEsperadaLt,
                PrecioSalida = PVGasDTO.PrecioSalida,
                //PrecioSalidaKg = CalcularPreciosVentaServicio.ObtenerPrecioSalidaKg((decimal)PVGasDTO.PrecioPemexKg, (decimal)PVGasDTO.UtilidadEsperadaKg, (decimal)flete),//PVGasDTO.PrecioSalidaKg,
                //PrecioSalidaLt = CalcularPreciosVentaServicio.ObtenerPrecioSalidaLt(((decimal)PVGasDTO.PrecioPemexKg + (decimal)flete), factorLtaKg),
                PrecioSalidaKg = PVGasDTO.PrecioSalidaKg,
                PrecioSalidaLt = PVGasDTO.PrecioSalidaLt,
                EsGas = PVGasDTO.EsGas,
                FechaProgramada = PVGasDTO.FechaProgramada,
                FechaVencimiento = PVGasDTO.FechaVencimiento,
                Activo = true,
                FechaRegistro = DateTime.Now

            };
        }
        public static List<PrecioVenta> FromDTO(PrecioVentaDTO entidad)
        {
            entidad.IdEstacion = int.Parse(entidad.Estacion);
            List<Producto> Prod = new List<Producto>();
            if (entidad.IdProducto.Equals(0))
                Prod = ProductoServicio.Obtener(EmpresaServicio.Obtener(entidad.IdEmpresa));
            else
                Prod.Add(ProductoServicio.ObtenerProducto(entidad.IdProducto));
            List<PrecioVentaDTO> _lst = new List<PrecioVentaDTO>();
            if (Prod.Count() >= 1)
            {
                foreach (var p in Prod)
                {
                    _lst.Add(EmpresaProds(entidad, p));
                }
            }
            return _lst.Select(x => FromTO(x)).ToList();
        }
        public static PrecioVentaDTO EmpresaProds(PrecioVentaDTO entidad, Producto p)
        {
            entidad = ToDTOtest(entidad, p);
            return entidad;
        }
        public static PrecioVentaDTO ToDTOtest(PrecioVentaDTO PVGasDTO, Producto p)
        {
            //var factorLtaKg = EmpresaServicio.Obtener(PVGasDTO.IdEmpresa).FactorLitrosAKilos;
            var IdStatus = CalcularPreciosVentaServicio.GetEstatusPrecioVenta(PVGasDTO.PrecioVentaEstatus);
            //var _Categoria = ProductoServicio.ObtenerCategoria(p.IdCategoria).Nombre;
            //var _Linea = ProductoServicio.ObtenerLineaProducto(p.IdProductoLinea).Linea;
            //var flete = PVGasDTO.PrecioFlete != null ? (decimal)PVGasDTO.PrecioFlete : 0;
            return new PrecioVentaDTO()
            {
                IdEmpresa = PVGasDTO.IdEmpresa,
                IdEstacion = PVGasDTO.IdEstacion,
                IdPrecioVentaEstatus = IdStatus,
                IdCategoria = p.IdCategoria,
                IdProductoLinea = p.IdProductoLinea,
                IdProducto = p.IdProducto,
                Categoria = p.Categoria.Nombre,
                Linea = p.LineaProducto.Linea,
                Producto = p.Descripcion,
                PrecioActual = PVGasDTO.PrecioActual,
                PrecioPemexKg = PVGasDTO.PrecioPemexKg,
                PrecioPemexLt = PVGasDTO.PrecioPemexLt,
                UtilidadEsperadaKg = PVGasDTO.UtilidadEsperadaKg,
                UtilidadEsperadaLt = PVGasDTO.UtilidadEsperadaLt,
                PrecioSalida = PVGasDTO.PrecioSalida,
                //PrecioSalidaKg = CalcularPreciosVentaServicio.ObtenerPrecioSalidaKg((decimal)PVGasDTO.PrecioPemexKg, (decimal)PVGasDTO.UtilidadEsperadaKg, (decimal)flete),//PVGasDTO.PrecioSalidaKg,
                //PrecioSalidaLt = CalcularPreciosVentaServicio.ObtenerPrecioSalidaLt(((decimal)PVGasDTO.PrecioPemexKg + (decimal)flete), factorLtaKg),
                PrecioSalidaKg = PVGasDTO.PrecioSalidaKg,
                PrecioSalidaLt = PVGasDTO.PrecioSalidaLt,
                PrecioFlete = PVGasDTO.PrecioFlete,
                EsGas = PVGasDTO.EsGas,
                FechaProgramada = PVGasDTO.FechaProgramada,
                FechaVencimiento = PVGasDTO.FechaVencimiento,
                Activo = true,
                FechaRegistro = DateTime.Now,
            };

        }
        public static PrecioVenta FromDtoEditar(PrecioVentaDTO Ctedto, PrecioVenta catCte)
        {            
            var catPrecioVenta = FromEntity(catCte);
            catPrecioVenta.IdPrecioVenta = Ctedto.IdPrecioVenta;
            if (Ctedto.IdEstacion != 0) { catPrecioVenta.IdEstacion = Ctedto.IdEstacion; }
            if (Ctedto.IdEmpresa != 0) { catPrecioVenta.IdEmpresa = Ctedto.IdEmpresa; } else { catPrecioVenta.IdEmpresa = catPrecioVenta.IdEmpresa; }
            if (Ctedto.IdPrecioVentaEstatus != 0) { catPrecioVenta.IdPrecioVentaEstatus = Ctedto.IdPrecioVentaEstatus; } else catPrecioVenta.IdPrecioVentaEstatus = catPrecioVenta.IdPrecioVentaEstatus;
            if (Ctedto.IdCategoria != 0) catPrecioVenta.IdCategoria = Ctedto.IdCategoria; else catPrecioVenta.IdCategoria = catPrecioVenta.IdCategoria;
            if (Ctedto.IdProductoLinea != 0) catPrecioVenta.IdProductoLinea = Ctedto.IdProductoLinea; else catPrecioVenta.IdProductoLinea = catPrecioVenta.IdProductoLinea;
            if (Ctedto.IdProducto != 0) catPrecioVenta.IdProducto = Ctedto.IdProducto; else catPrecioVenta.IdProducto = catPrecioVenta.IdProducto;
            //if (Ctedto.Categoria != null) catPrecioVenta.Categoria = Ctedto.Categoria; else catPrecioVenta.Categoria = catPrecioVenta.Categoria;
            //if (Ctedto.Linea != null) catPrecioVenta.Linea = Ctedto.Linea; else catPrecioVenta.Linea = catPrecioVenta.Linea;
            //if (Ctedto.Producto != null) catPrecioVenta.Producto = Ctedto.Producto; else catPrecioVenta.Producto = catPrecioVenta.Producto;
            if (Ctedto.Categoria != null) catPrecioVenta.Categoria = catCte.Categoria; else catPrecioVenta.Categoria = catCte.Categoria;
            if (Ctedto.Linea != null) catPrecioVenta.Linea = catCte.Linea; else catPrecioVenta.Linea = catCte.Linea;
            if (Ctedto.Producto != null) catPrecioVenta.Producto = catCte.Producto; else catPrecioVenta.Producto = catCte.Producto;
            if (Ctedto.PrecioActual != null) catPrecioVenta.PrecioActual = Ctedto.PrecioActual; else catPrecioVenta.PrecioActual = catPrecioVenta.PrecioActual;
            if (Ctedto.PrecioPemexKg != null) catPrecioVenta.PrecioPemexKg = Ctedto.PrecioPemexKg; else catPrecioVenta.PrecioPemexKg = catPrecioVenta.PrecioPemexKg;
            if (Ctedto.PrecioPemexLt != null) catPrecioVenta.PrecioPemexLt = Ctedto.PrecioPemexLt; else catPrecioVenta.PrecioPemexLt = catPrecioVenta.PrecioPemexLt;
            if (Ctedto.UtilidadEsperadaKg != null) catPrecioVenta.UtilidadEsperadaKg = Ctedto.UtilidadEsperadaKg; else catPrecioVenta.UtilidadEsperadaKg = catPrecioVenta.UtilidadEsperadaKg;
            if (Ctedto.UtilidadEsperadaLt != null) catPrecioVenta.UtilidadEsperadaLt = Ctedto.UtilidadEsperadaLt; else catPrecioVenta.UtilidadEsperadaLt = catPrecioVenta.UtilidadEsperadaLt;
            if (Ctedto.PrecioSalida != null) catPrecioVenta.PrecioSalida = Ctedto.PrecioSalida; else catPrecioVenta.PrecioSalida = catPrecioVenta.PrecioSalida;
            if (Ctedto.PrecioSalidaKg != null) catPrecioVenta.PrecioSalidaKg = Ctedto.PrecioSalidaKg; else catPrecioVenta.PrecioSalidaKg = catPrecioVenta.PrecioSalidaKg;
            if (Ctedto.PrecioSalidaLt != null) catPrecioVenta.PrecioSalidaLt = Ctedto.PrecioSalidaLt; else catPrecioVenta.PrecioSalidaLt = catPrecioVenta.PrecioSalidaLt;
            if (Ctedto.FechaProgramada.ToString() != "01/01/0001 0:00:00") catPrecioVenta.FechaProgramada = Ctedto.FechaProgramada; else catPrecioVenta.FechaProgramada = catPrecioVenta.FechaProgramada;            

            return catPrecioVenta;
        }
        public static PrecioVenta FromEntity(PrecioVenta pv)
        {
            return new PrecioVenta()
            {
                IdEmpresa = pv.IdEmpresa,
                IdPrecioVentaEstatus = pv.IdPrecioVentaEstatus,
                IdCategoria = pv.IdCategoria,
                IdProductoLinea = pv.IdProductoLinea,
                IdProducto = pv.IdProducto,
                Categoria = pv.Categoria,
                Linea = pv.Linea,
                Producto = pv.Producto,
                PrecioActual = pv.PrecioActual,
                PrecioPemexKg = pv.PrecioPemexKg,
                PrecioPemexLt = pv.PrecioPemexLt,
                UtilidadEsperadaKg = pv.UtilidadEsperadaKg,
                UtilidadEsperadaLt = pv.UtilidadEsperadaLt,
                PrecioSalida = pv.PrecioSalida,
                PrecioSalidaKg = pv.PrecioSalidaKg,
                PrecioSalidaLt = pv.PrecioSalidaLt,
                EsGas = pv.EsGas,
                FechaProgramada = pv.FechaProgramada,
                FechaVencimiento = pv.FechaVencimiento,
                Activo = pv.Activo,
                FechaRegistro = pv.FechaRegistro,

            };
        }
        public static PrecioVentaDTO ToDTO(PrecioVenta pv, Producto producto)
        {
            //var nombreEmp = EmpresaServicio.Obtener(pv.IdEmpresa).NombreComercial;
            var usuario = UsuarioServicio.Obtener(TokenServicio.ObtenerIdUsuario());

            PrecioVentaDTO usDTO = new PrecioVentaDTO()
            {
                IdPrecioVenta = pv.IdPrecioVenta,
                IdEmpresa = pv.IdEmpresa,
                IdPrecioVentaEstatus = pv.IdPrecioVentaEstatus,
                IdCategoria = pv.IdCategoria,
                IdProductoLinea = pv.IdProductoLinea,
                IdProducto = pv.IdProducto,
                Categoria = pv.Categoria,
                Linea = pv.Linea,
                Producto = pv.Producto,
                PrecioActual = pv.PrecioActual ?? 0,
                PrecioPemexKg = pv.PrecioPemexKg ?? 0,
                PrecioPemexLt = pv.PrecioPemexLt ?? 0,
                UtilidadEsperadaKg = pv.UtilidadEsperadaKg ?? 0,
                UtilidadEsperadaLt = pv.UtilidadEsperadaLt ?? 0,
                //PrecioSalida =  usuario.OperadoresChoferes.FirstOrDefault().PuntosVenta.FirstOrDefault().UnidadesAlmacen.IdEstacionCarburacion != null ? Convert.ToDecimal(10.20) : pv.PrecioSalida ?? 0,
                //PrecioSalidaKg = usuario.OperadoresChoferes.FirstOrDefault().PuntosVenta.FirstOrDefault().UnidadesAlmacen.IdEstacionCarburacion != null ? Convert.ToDecimal(10.20) : pv.PrecioSalidaKg ?? 0,
                //PrecioSalidaLt = usuario.OperadoresChoferes.FirstOrDefault().PuntosVenta.FirstOrDefault().UnidadesAlmacen.IdEstacionCarburacion != null ? Convert.ToDecimal(10.20) : pv.PrecioSalidaLt ?? 0,

                PrecioSalida = pv.PrecioSalida ?? 0,
                PrecioSalidaKg = pv.PrecioSalidaKg ?? 0,
                PrecioSalidaLt = pv.PrecioSalidaLt ?? 0,
                EsGas = pv.EsGas,
                FechaProgramada = pv.FechaProgramada,
                FechaVencimiento = pv.FechaVencimiento ?? DateTime.MinValue,
                Activo = pv.Activo,
                FechaRegistro = pv.FechaRegistro,
                Empresa = usuario.Empresa.NombreComercial,
                PrecioVentaEstatus = pv.Estatus.Descripcion,
                CategoriaProducto = producto.Descripcion,//Concepto
                IdUnidadMedida = producto.IdUnidadMedida,
            };
            if (usuario.OperadoresChoferes.FirstOrDefault().PuntosVenta.FirstOrDefault().UnidadesAlmacen.IdEstacionCarburacion != 0)
            {
                var id = usuario.OperadoresChoferes.FirstOrDefault().PuntosVenta.FirstOrDefault().UnidadesAlmacen.IdEstacionCarburacion;
                var PrecioSalidaKg = usuario.OperadoresChoferes.FirstOrDefault().PuntosVenta.FirstOrDefault().UnidadesAlmacen.EstacionCarburacion;
                var PrecioSalidaLt = usuario.OperadoresChoferes.FirstOrDefault().PuntosVenta.FirstOrDefault().UnidadesAlmacen.EstacionCarburacion;
                if (usuario.OperadoresChoferes.FirstOrDefault().PuntosVenta.FirstOrDefault().UnidadesAlmacen.IdEstacionCarburacion.Equals(id) && PrecioSalidaKg != null && PrecioSalidaLt != null)
                {
                    //usDTO.PrecioSalida = Convert.ToDecimal(8.88);
                    usDTO.IdEstacion = id ?? 0;                    
                    usDTO.PrecioSalidaKg = Convert.ToDecimal(PrecioSalidaKg.CPrecioVenta.FirstOrDefault().PrecioSalidaKg);
                    usDTO.PrecioSalidaLt = Convert.ToDecimal(PrecioSalidaLt.CPrecioVenta.FirstOrDefault().PrecioSalidaLt);
                }
            }
            return usDTO;
        }
        public static List<RepHistorioPrecioDTO> ToRepo(List<PrecioVenta> entidades, HistoricoPrecioDTO dto)
        {
            int id = 0;
            List<RepHistorioPrecioDTO> list = new List<RepHistorioPrecioDTO>();
            foreach (var item in entidades)
            {
                if (dto.LitroGas) list.Add(ToRepo(item, 1, id++));
                if (dto.Clilindro10K) list.Add(ToRepo(item, 10, id++));
                if (dto.Clilindro20K) list.Add(ToRepo(item, 20, id++));
                if (dto.Clilindro30K) list.Add(ToRepo(item, 30, id++));
                if (dto.Clilindro45K) list.Add(ToRepo(item, 45, id++));
            }
            return list;
        }
        public static RepHistorioPrecioDTO ToRepo(PrecioVenta entidad, int Multiplio, int id)
        {
            return new RepHistorioPrecioDTO()
            {
                ID = id,
                Fecha = entidad.FechaRegistro,
                Producto = CamionetaServicio.TipoCilindro(Multiplio),
                Precio = (double)(entidad.PrecioSalidaKg * Multiplio),
            };
        }
    }
}
