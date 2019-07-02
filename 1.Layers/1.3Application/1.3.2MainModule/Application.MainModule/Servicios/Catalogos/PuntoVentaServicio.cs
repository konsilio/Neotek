using Application.MainModule.DTOs.Catalogo;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.AccesoADatos;
using Exceptions.MainModule.Validaciones;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.DTOs.Mobile.Cortes;
using Application.MainModule.Servicios.Almacenes;
using Application.MainModule.AdaptadoresDTO.Catalogo;
using Application.MainModule.DTOs.Ventas;
using Application.MainModule.AdaptadoresDTO.Ventas;

namespace Application.MainModule.Servicios.Catalogos
{
    public static class PuntoVentaServicio
    {
        public static List<PuntoVentaDTO> Obtener()
        {
            List<PuntoVentaDTO> lPventas = PuntoVentaAdapter.ToDTO(new PuntoVentaDataAccess().BuscarTodos());
            return lPventas;
        }
        public static List<VentaPuntoDeVenta> ObtenerVentas(DateTime fi, DateTime ff)
        {           
            return new PuntoVentaDataAccess().BuscarVentas(fi, ff);
        }
        public static PuntoVenta Obtener(int idPuntoVenta)
        {
            return new PuntoVentaDataAccess().Buscar(idPuntoVenta);
        }
        public static OperadorChoferDTO ObtenerOperador(int idUsuario)
        {
            OperadorChoferDTO lPventas = AdaptadoresDTO.Catalogo.OperadorChoferAdapter.ToOperador(new PuntoVentaDataAccess().BuscarPorUsuario(idUsuario));
            return lPventas;
        }
        public static List<OperadorChoferDTO> ObtenerUsuariosOperador(short idEmpresa)
        {
            List<OperadorChoferDTO> lPventas = AdaptadoresDTO.Catalogo.OperadorChoferAdapter.ToUsuariosOpe(new OperadorChoferDataAccess().BuscarTodos(idEmpresa));
            return lPventas;
        }
        public static List<PuntoVenta> ObtenerIdEmp(short IdEmpresa)
        {
            return new PuntoVentaDataAccess().BuscarTodos(IdEmpresa);
        }
        public static PuntoVenta Obtener(OperadorChofer opCh)
        {
            if (opCh != null)
                if (opCh.PuntosVenta != null)
                    if (opCh.PuntosVenta.Count > 0)
                        return opCh.PuntosVenta.FirstOrDefault();

            return BuscarPorOperadorChofer(opCh.IdOperadorChofer).FirstOrDefault();
        }
        public static PuntoVenta Obtener(UnidadAlmacenGas unidadAlmacen)
        {
            return new PuntoVentaDataAccess().BuscarPorUnidadAlmacenGas(unidadAlmacen.IdCAlmacenGas);
        }
        public static PuntoVenta Obtener(short unidadAlmacen)
        {
            return new PuntoVentaDataAccess().BuscarPorUnidadAlmacenGas(unidadAlmacen);
        }
        public static List<PuntoVenta> BuscarPorOperadorChofer(int idOperadorChofer)
        {
            return new PuntoVentaDataAccess().BuscarPorOperadorChofer(idOperadorChofer);
        }
        public static PuntoVenta ObtenerPorUsuarioAplicacion()
        {
            var operadorChofer = OperadorChoferServicio.ObtenerPorUsuarioAplicacion();
            return operadorChofer != null ? Obtener(operadorChofer) : null;
        }
        public static RespuestaDto Insertar(PuntoVenta pv)
        {
            return new PuntoVentaDataAccess().Insertar(pv);
        }
        public static RespuestaDto Eliminar(PuntoVenta cteLoc)
        {
            return new PuntoVentaDataAccess().Eliminar(cteLoc);
        }
        public static RespuestaDto Modificar(PuntoVenta cte)
        {
            return new PuntoVentaDataAccess().Actualizar(cte);
        }
        public static RespuestaDto NoExiste()
        {
            string mensaje = string.Format(Error.NoExiste, "El punto de venta");

            return new RespuestaDto()
            {
                ModeloValido = true,
                Mensaje = mensaje,
                MensajesError = new List<string>() { mensaje },
            };
        }
        public static List<VentaCajaGeneral> ObtenerVentasCajaGral()
        {
            return new PuntoVentaDataAccess().ObtenerVentasCajaGral();
        }
        public static VentaPuntoDeVenta EvaluarFolio(string folioVenta)
        {
            return new PuntoVentaDataAccess().EvaluarFolio(folioVenta);
        }
        public static RespuestaDto InsertMobile(VentaPuntoDeVenta venta)
        {
            return new PuntoVentaDataAccess().InsertarMobile(venta);
        }
        public static List<PrecioVenta> ObtenerPreciosVenta(short idEmpresa)
        {
            return new PuntoVentaDataAccess().PreciosVenta(idEmpresa);
        }
        public static RespuestaDto InsertMobil(VentaCajaGeneral corteCajaGeneral)
        {
            return new PuntoVentaDataAccess().InesertarVentaGeneral(corteCajaGeneral);
        }
        public static List<VentaPuntoDeVenta> ObtenerVentasContado(int idPuntoVenta, DateTime fecha)
        {
            return new PuntoVentaDataAccess().BuscarVentasTipoPago(idPuntoVenta, fecha);
        }
        public static List<VentaPuntoDeVenta> ObtenerVentasTOP(int TOP, DateTime fecha)
        {
            return new PuntoVentaDataAccess().BuscarVentasTOP(TOP, fecha);          
        }
        public static List<VentaPuntoVentaDTO> ObtenerVentasTOPDTO(int TOP, DateTime fecha)
        {
            var vts = ObtenerVentasTOP(TOP, fecha);
            return CajaGeneralAdapter.ToDTOP(vts);
        }
        public static List<VentaPuntoDeVenta> ObtenerVentasCredito(int idPuntoVenta, DateTime fecha)
        {
            return new PuntoVentaDataAccess().BuscarVentasTipoPago(idPuntoVenta, fecha, true);
        }
        public static List<VentaCorteAnticipoEC> ObtenerAnticipos(UnidadAlmacenGas unidadEstacion)
        {
            return new PuntoVentaDataAccess().Anticipos(unidadEstacion);
        }
        public static List<VentaCorteAnticipoEC> ObtenerAnticipos(UnidadAlmacenGas unidadEstacion, DateTime fecha)
        {
            return new PuntoVentaDataAccess().Anticipos(unidadEstacion, fecha);
        }
        public static List<VentaCorteAnticipoEC> ObtenerCortesAnticipos()
        {
            return new PuntoVentaDataAccess().ObtenerCortesAnticipos();
        }
        public static object ActualizarVentasCorte(VentaPuntoDeVenta item)
        {
            return new PuntoVentaDataAccess().ActualizarVentas(item);
        }
        public static VentaPuntoDeVenta Obtener(string tiketVenta)
        {
            return new PuntoVentaDataAccess().ObtenerVenta(tiketVenta);
        }
        public static List<VentaPuntoDeVenta> ObtenerVentasPorRFC(string rfc)
        {
            return new PuntoVentaDataAccess().BuscarVentasPorRFC(rfc);
        }
        public static List<VentaPuntoDeVenta> ObtenerVentasPorCliente(int id)
        {
            return new PuntoVentaDataAccess().BuscarVentasPorCliente(id);
        }
        public static List<VentaPuntoDeVenta> ObtenerVentasPorCliente(short id)
        {
            return new PuntoVentaDataAccess().BuscarVentasPorEmpresa(id);
        }
        public static List<VentaPuntoDeVenta> ObtenerVentasPorPeriodo(short id, DateTime Periodo)
        {
            return new PuntoVentaDataAccess().BuscarVentasPorPeriodo(id, Periodo);
        }
        public static List<VentaPuntoDeVenta> ObtenerVentasPorCliente(int id,DateTime fi, DateTime ff)
        {
            return new PuntoVentaDataAccess().BuscarVentasPorCliente(id).Where(x => (x.FechaRegistro > fi) && (x.FechaRegistro < ff)).ToList();
        }
        /// <summary>
        /// Permite realizar el registro del cargo de una venta 
        /// que es de tipo extraordinaria, se envia como parametro
        /// una entidad de tipo Cargo y retornara un objeto de tipo
        /// RespuestaDTO con la respuesta del registro del cargo 
        /// </summary>
        /// <param name="cargo">Entidad de tipo Cargo con los datos a registrar de la venta extraordinaria</param>
        /// <returns>Modelo DTO de tipo RespuestaDTO con la respuesta del registro</returns>
        public static RespuestaDto insertCargoMobile(Cargo cargo)
        {
            return new PuntoVentaDataAccess().insertCargoMobile(cargo);
        }
        /// <summary>
        /// buscarCorte
        /// Permite realizar la consulta de un corte en la estación, se envian como
        /// parametros el día , mes , año y la estación a buscar , se retornara uno bjeto det tipo
        /// VentaCorteAnticipoEC con el resultado de la conslta.
        /// </summary>
        /// <param name="dia">Día en que se realizo la venta </param>
        /// <param name="mes">Mes en que se realizo la venta </param>
        /// <param name="year">Año en que se relizo la venta </param>
        /// <param name="idCAlmacenGas">Id del CAlmacenGas del que se consulta </param>
        /// <returns>Retornara un objeto de tipo VentaCorteAnticipoEC con la respuesta de este </returns>
        public static VentaCorteAnticipoEC buscarCorte(DateTime fecha, short idCAlmacenGas)
        {
            return new PuntoVentaDataAccess().BuscarCorte(fecha, idCAlmacenGas);
        }
        public static List<VentaCorteAnticipoEC> ObtenerAnticipos(PuntoVenta puntoVenta, DateTime fecha)
        {
            if (puntoVenta != null)
                return new PuntoVentaDataAccess().BuscarAnticipos(fecha, puntoVenta.IdCAlmacenGas);
            else
                return null;
        }
        public static VentaCorteAnticipoEC ObtenerCortes(PuntoVenta puntoVenta, DateTime fecha)
        {
            if (puntoVenta != null)
                return new PuntoVentaDataAccess().BuscarCorte(fecha, puntoVenta.IdCAlmacenGas);
            else
                return null;
        }
        public static RespuestaDto RegistarReporteDia(ReporteDelDia reporteEntity)
        {
            return new PuntoVentaDataAccess().RegistrarReporteDia(reporteEntity);
        }
        public static string ObtenerSerie(int idPuntoVenta)
        {
            var almacen = Obtener(idPuntoVenta).UnidadesAlmacen;
            if (almacen.IdPipa != null)
                return almacen.Pipa.Serie;
            if (almacen.IdCamioneta != null)
                return almacen.Camioneta.Serie;
            if (almacen.IdEstacionCarburacion != null)
                return almacen.EstacionCarburacion.Serie;
            return string.Empty;
        }
        public static string ObtenerFolio(int idPuntoVenta)
        {
            var almacen = AlmacenGasServicio.ObtenerUnidadAlamcenGas(Obtener(idPuntoVenta).IdCAlmacenGas);
            if (almacen.IdPipa != null)
                return almacen.Pipa.Folio != null ? (almacen.Pipa.Folio.Value + 1).ToString() : "1";
            if (almacen.IdCamioneta != null)
                return almacen.Camioneta.Folio != null ? (almacen.Camioneta.Folio.Value + 1).ToString() : "1";
            if (almacen.IdEstacionCarburacion != null)
                return almacen.EstacionCarburacion.Folio != null ? (almacen.EstacionCarburacion.Folio.Value + 1).ToString() : "1";
            return string.Empty;
        }
        public static RespuestaDto ActualizarFolio(int idPuntoVenta)
        {
            var almacen = AlmacenGasServicio.ObtenerUnidadAlamcenGas(Obtener(idPuntoVenta).IdCAlmacenGas);
            if (almacen.IdPipa != null)
            {
                var pipa = PipaAdapter.FromEntity(almacen.Pipa);
                pipa.Folio++;
                return PipaServicio.Modificar(pipa);
            }
            if (almacen.IdCamioneta != null)
            {
                var camioneta = CamionetaAdapter.FromEntity(almacen.Camioneta);
                camioneta.Folio++;
                return CamionetaServicio.Modificar(camioneta);
            }
            if (almacen.IdEstacionCarburacion != null)
            {
                var estacion = EstacionCarburacionAdapter.FromEmtity(almacen.EstacionCarburacion);
                estacion.Folio++;
                return EstacionCarburacionServicio.Modificar(estacion);
            }
            return new RespuestaDto() { Exito = false, Mensaje = string.Format(Error.NoExiste, "El punto de venta" )};
        }
    }
}
