using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.Servicios.Catalogos;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.DTOs.Mobile;
using Sagas.MainModule.ObjetosValor.Enum;
using Application.MainModule.Servicios.Mobile;
using Application.MainModule.DTOs.Almacen;
using Application.MainModule.AdaptadoresDTO.Almacenes;
using Application.MainModule.Servicios.Compras;
using Security.MainModule.Criptografia;
using Utilities.MainModule;
using Sagas.MainModule.ObjetosValor.Constantes;
using Application.MainModule.Servicios.Seguridad;
using Application.MainModule.AdaptadoresDTO.Mobile;

namespace Application.MainModule.Servicios.Almacenes
{
    public static class AlmacenGasServicio
    {
        private static UnidadAlmacenGas RegistraAlmacenAlterno(AlmacenGasDescarga descarga, Empresa empresa)
        {
            RespuestaDto resp;
            UnidadAlmacenGas unidad;
            List<UnidadAlmacenGas> unidades = ObtenerUnidadesAlmacenGasAlternoNoActivos(empresa);
            if (unidades != null && unidades.Count > 0)
            {
                unidad = AlmacenGasAdapter.FromEntity(unidades.FirstOrDefault());
                unidad.IdAlmacenGas = ObtenerAlmacenGasTotal(empresa).IdAlmacenGas;
                unidad.IdCamioneta = null;
                unidad.IdEstacionCarburacion = null;
                unidad.IdPipa = null;
                unidad.IdEmpresa = empresa.IdEmpresa;
                unidad.IdTipoAlmacen = TipoUnidadAlmacenGasEnum.Fijo;
                unidad.IdTipoMedidor = descarga.IdTipoMedidorTractor;
                unidad.CantidadActualKg = 0;
                unidad.CantidadActualLt = 0;
                unidad.CapacidadTanqueKg = 0;
                unidad.CapacidadTanqueLt = 0;
                unidad.EsAlterno = true;
                unidad.EsGeneral = true;
                unidad.PorcentajeCalibracionPlaneada = 0;
                unidad.Numero = string.Format(AlmacenGasConst.NombreAlmacenAlterno, descarga.NumTanquePG);
                unidad.P5000Actual = 0;
                unidad.PorcentajeActual = 0;
                unidad.Activo = true;
                unidad.FechaRegistro = DateTime.Now;

                resp = new AlmacenGasDataAccess().Actualizar(unidad);
                return ObtenerUnidadAlamcenGas((short)resp.Id);
            }

            unidad = new UnidadAlmacenGas
            {
                IdAlmacenGas = ObtenerAlmacenGasTotal(empresa).IdAlmacenGas,
                IdCamioneta = null,
                IdEstacionCarburacion = null,
                IdPipa = null,
                IdEmpresa = empresa.IdEmpresa,
                IdTipoAlmacen = TipoUnidadAlmacenGasEnum.Fijo,
                IdTipoMedidor = descarga.IdTipoMedidorTractor,
                CantidadActualKg = 0,
                CantidadActualLt = 0,
                CapacidadTanqueKg = 0,
                CapacidadTanqueLt = 0,
                EsAlterno = true,
                EsGeneral = true,
                PorcentajeCalibracionPlaneada = 0,
                Numero = string.Format(AlmacenGasConst.NombreAlmacenAlterno, descarga.NumTanquePG),
                P5000Actual = null,
                PorcentajeActual = 0,
                Activo = true,
                FechaRegistro = DateTime.Now,
            };

            resp = new AlmacenGasDataAccess().Insertar(unidad);
            return ObtenerUnidadAlamcenGas((short)resp.Id);
        }
        public static RespuestaDto InsertarAutoconsumo(AlmacenGasAutoConsumo adapter)
        {
            return new AlmacenGasDataAccess().Insertar(adapter);
        }


        public static AlmacenGas ObtenerAlmacenGasTotal(Empresa empresa)
        {
            if (empresa.AlmacenesGas != null && empresa.AlmacenesGas.Count > 0)
                return empresa.AlmacenesGas.FirstOrDefault();

            return new AlmacenGasDataAccess().BuscarPorEmpresa(empresa.IdEmpresa);
        }
        public static RespuestaDto InsertarDescargaGas(AlmacenGasDescarga alm)
        {
            return new AlmacenGasDescargaDataAccess().Insertar(alm);
        }
        public static RespuestaDto ActualizarDescargaGas(AlmacenGasDescarga alm, List<AlmacenGasDescargaFoto> fotos)
        {
            return new AlmacenGasDescargaDataAccess().Actualizar(alm, fotos);
        }
        public static List<AlmacenGas> ObtenerTodos(short idEmpresa)
        {
            return new AlmacenGasDataAccess().BuscarTodos(idEmpresa);
        }
        public static AlmacenGasTomaLectura BuscarUltimaLectura(short idCAlmacenGas, byte idTipoEvento)
        {
            return new AlmacenGasDataAccess().BuscarUltimaLectura(idCAlmacenGas, idTipoEvento);
        }
        public static List<AlmacenGasTomaLectura> ObtenerLecturas(short idCAlmacenGas)
        {
            return new AlmacenGasDataAccess().BuscarLecturas(idCAlmacenGas);
        }
        public static List<AlmacenGasTomaLectura> ObtenerTomaLecturasDatosNoProcesados(UnidadAlmacenGas unidad)
        {
            bool noProcesados = false;

            if (unidad != null)
                if (unidad.TomasLectura != null && unidad.TomasLectura.Count > 0)
                    return unidad.TomasLectura.Where(x => x.DatosProcesados.Equals(noProcesados)).ToList();

            return new AlmacenGasDataAccess().BuscarLecturas(unidad.IdCAlmacenGas, noProcesados);
        }
        public static RespuestaDto InsertarLectura(AlmacenGasTomaLectura lia)
        {
            return new AlmacenGasDataAccess().Insertar(lia);
        }
        internal static RespuestaDto InsertarRecargaGas(AlmacenGasRecarga adapter)
        {
            return new AlmacenGasDataAccess().Insertar(adapter);
        }
        public static AlmacenGasRecarga ObtenerRecargaPorClaveOperacion(string claveOperacion)
        {
            return new AlmacenGasDataAccess().BuscarRecargaClaveOperacion(claveOperacion);
        }
        public static List<UnidadAlmacenGas> ObtenerAlmacenGeneral(short idEmpresa, bool incluyeAlterno = false)
        {
            return new AlmacenGasDataAccess().BuscarTodos(idEmpresa, true, incluyeAlterno);
        }
        public static List<UnidadAlmacenGas> ObtenerAlmacenGeneral(Empresa empresa, bool incluyeAlterno = false)
        {
            if (empresa.UnidadesAlmacenGas != null && empresa.UnidadesAlmacenGas.Count > 0)
                return empresa.UnidadesAlmacenGas.ToList();

            return new AlmacenGasDataAccess().BuscarTodos(empresa.IdEmpresa, true, incluyeAlterno);
        }
        public static List<UnidadAlmacenGas> ObtenerAlmacenGeneral(short idEmpresa)
        {
            return new AlmacenGasDataAccess().BuscarTodas(idEmpresa);
        }
        public static List<AlmacenGasDescarga> ObtenerDescargasNoProcesadas()
        {
            return new AlmacenGasDataAccess().BuscarTodasDescargasNoProcesadas();
        }
        public static List<AlmacenGasRecarga> ObtenerRecargasNoProcesadas()
        {
            return new AlmacenGasDataAccess().BuscarTodasRecargasNoProcesadas();
        }
        public static List<AlmacenGasTraspaso> ObtenerTraspasosNoProcesadas()
        {
            return new AlmacenGasDataAccess().BuscarTodosTraspasosNoProcesadas();
        }
        public static List<AlmacenGasRecarga> ObtenerRecargasNoProcesadas(byte idTipoEvento)
        {
            return new AlmacenGasDataAccess().BuscarTodasRecargasNoProcesadas(idTipoEvento);
        }
        public static List<AlmacenGasAutoConsumo> ObtenerAutoConsumosNoProcesadas()
        {
            return new AlmacenGasDataAccess().BuscarTodosAutoConsumosNoProcesadas();
        }
        public static List<AlmacenGasCalibracion> ObtenerCalibracionesNoProcesadas()
        {
            return new AlmacenGasDataAccess().BuscarTodasCalibracionesNoProcesadas();
        }
        public static List<AlmacenGasTomaLectura> ObtenerLecturasNoProcesadas()
        {
            return new AlmacenGasDataAccess().BuscarTodasLecturasNoProcesadas();
        }
        public static List<UnidadAlmacenGas> ObtenerEstaciones(short idEmpresa)
        {
            return new AlmacenGasDataAccess().BuscarTodosEstacionCarburacion(idEmpresa);
        }
        public static List<UnidadAlmacenGas> ObtenerPipas(short idEmpresa)
        {
            return new AlmacenGasDataAccess().BuscarTodosPipas(idEmpresa);
        }
        public static List<UnidadAlmacenGas> ObtenerCamionetas(short idEmpresa)
        {
            return new AlmacenGasDataAccess().BuscarTodosCamionetas(idEmpresa);
        }
        public static AlmacenGasTomaLectura ObtenerLecturaPorClaveOperacion(string claveProceso)
        {
            return new AlmacenGasDataAccess().BuscarClaveOperacion(claveProceso);
        }
        public static AlmacenGasTomaLectura ObtenerUltimaLectura(UnidadAlmacenGas uniAlm, bool final = false)
        {
            if (uniAlm != null)
                if (uniAlm.TomasLectura != null)
                    if (uniAlm.TomasLectura.Count > 0)
                        return !final
                            ? uniAlm.TomasLectura.Last(x => x.IdTipoEvento.Equals(TipoEventoEnum.Final))
                            : uniAlm.TomasLectura.Last(x => x.IdTipoEvento.Equals(TipoEventoEnum.Inicial));
            return !final
                ? BuscarUltimaLectura(uniAlm.IdCAlmacenGas, TipoEventoEnum.Final)
                : BuscarUltimaLectura(uniAlm.IdCAlmacenGas, TipoEventoEnum.Inicial);
        }
        public static AlmacenGas Obtener(short idAlmacenGas)
        {
            return new AlmacenGasDataAccess().Buscar(idAlmacenGas);
        }
        public static UnidadAlmacenGas ObtenerUnidadAlamcenGas(short idCAlmacenGas)
        {
            return new AlmacenGasDataAccess().BuscarUnidadAlamcenGas(idCAlmacenGas);
        }
        public static UnidadAlmacenGas ObtenerUnidadAlamcenGasActualizaAlterno(AlmacenGasDescarga descarga, Empresa empresa)
        {
            if (descarga.TanquePrestado.Value)
            {
                //var unidades = ObtenerUnidadesAlmacenGasAlterno(empresa);
                return RegistraAlmacenAlterno(descarga, empresa);
            }

            if (descarga != null)
                if (descarga.UnidadAlmacen != null)
                    return descarga.UnidadAlmacen;

            return ObtenerUnidadAlamcenGas(descarga.IdCAlmacenGas.Value);
        }
        public static UnidadAlmacenGas ObtenerUnidadAlamcenGas(AlmacenGasRecarga recarga, bool deSalida)
        {
            if (recarga != null)
            {
                if (!deSalida)
                {
                    if (recarga.UnidadAlmacenEntrada != null)
                        return recarga.UnidadAlmacenEntrada;
                }
                else
                {
                    if (recarga.UnidadAlmacenSalida != null)
                        return recarga.UnidadAlmacenSalida;
                }
            }

            if (!deSalida)
                return ObtenerUnidadAlamcenGas(recarga.IdCAlmacenGasEntrada);
            else
                return ObtenerUnidadAlamcenGas(recarga.IdCAlmacenGasSalida.Value);
        }
        public static UnidadAlmacenGas ObtenerUnidadAlamcenGas(AlmacenGasTraspaso traspaso, bool deSalida)
        {
            if (traspaso != null)
            {
                if (!deSalida)
                {
                    if (traspaso.UnidadEntrada != null)
                        return traspaso.UnidadEntrada;
                }
                else
                {
                    if (traspaso.UnidadSalida != null)
                        return traspaso.UnidadSalida;
                }
            }

            if (!deSalida)
                return ObtenerUnidadAlamcenGas(traspaso.IdCAlmacenGasEntrada);
            else
                return ObtenerUnidadAlamcenGas(traspaso.IdCAlmacenGasSalida);
        }
        public static UnidadAlmacenGas ObtenerUnidadAlamcenGas(AlmacenGasAutoConsumo autoConsumo, bool deSalida)
        {
            if (autoConsumo != null)
            {
                if (!deSalida)
                {
                    if (autoConsumo.UnidadEntrada != null)
                        return autoConsumo.UnidadEntrada;
                }
                else
                {
                    if (autoConsumo.UnidadSalida != null)
                        return autoConsumo.UnidadSalida;
                }
            }

            if (!deSalida)
                return ObtenerUnidadAlamcenGas(autoConsumo.IdCAlmacenGasEntrada);
            else
                return ObtenerUnidadAlamcenGas(autoConsumo.IdCAlmacenGasSalida);
        }
        public static UnidadAlmacenGas ObtenerUnidadAlamcenGas(AlmacenGasCalibracion calibracion)
        {
            if (calibracion != null)
            {
                if (calibracion.UnidadAlmacenGas != null)
                    return calibracion.UnidadAlmacenGas;
            }

            return ObtenerUnidadAlamcenGas(calibracion.IdCAlmacenGas);
        }
        public static UnidadAlmacenGas ObtenerUnidadAlamcenGas(AlmacenGasTomaLectura tomaLectura)
        {
            if (tomaLectura != null)
            {
                if (tomaLectura.UnidadAlmacenGas != null)
                    return tomaLectura.UnidadAlmacenGas;
            }

            return ObtenerUnidadAlamcenGas(tomaLectura.IdCAlmacenGas);
        }
        public static UnidadAlmacenGas ObtenerUnidadAlmacenGasAlterno(short idEmpresa)
        {
            return new AlmacenGasDataAccess().ObtenerUnidadAlmacenGasAlterno(idEmpresa);
        }
        public static List<UnidadAlmacenGas> ObtenerUnidadesAlmacenGasAlterno(Empresa empresa)
        {
            if (empresa.UnidadesAlmacenGas != null & empresa.UnidadesAlmacenGas.Count > 0)
            {
                var unidades = empresa.UnidadesAlmacenGas.Where(x => x.EsAlterno && x.Activo).ToList();
                if (unidades != null & unidades.Count > 0)
                    return unidades;
            }

            return new AlmacenGasDataAccess().ObtenerUnidadesAlmacenGasAlterno(empresa.IdEmpresa);
        }
        private static List<UnidadAlmacenGas> ObtenerUnidadesAlmacenGasAlternoNoActivos(Empresa empresa)
        {
            if (empresa.UnidadesAlmacenGas != null & empresa.UnidadesAlmacenGas.Count > 0)
            {
                var unidades = empresa.UnidadesAlmacenGas.Where(x => x.EsAlterno && !x.Activo).ToList();
                if (unidades != null & unidades.Count > 0)
                    return unidades;
            }

            return new AlmacenGasDataAccess().ObtenerUnidadesAlmacenGasAlternoNoActivos(empresa.IdEmpresa);
        }
        public static AlmacenGasDescarga ObtenerDescargaPorOCompraExpedidor(int idOCompra)
        {
            return new AlmacenGasDescargaDataAccess().BuscarOCompraExpedidor(idOCompra);
        }
        public static AlmacenGasDescarga ObtenerDescargaPorClaveOperacion(string claveOperacion)
        {
            return new AlmacenGasDescargaDataAccess().BuscarClaveOperacion(claveOperacion);
        }
        public static List<AlmacenGasDescarga> ObtenerDescargasTodas()
        {
            return new AlmacenGasDescargaDataAccess().BuscarTodas();
        }
        public static List<string> ObtenerRutaImagenesSinVigencia(DateTime fechaVigencia)
        {
            List<string> rutas = new AlmacenGasDescargaDataAccess().BuscarImagenesSinVigencia(fechaVigencia).Select(x => x.PathImagen).ToList();
            //rutas.AddRange(new AlmacenGasDataAccess().BuscarImagenesSinVigencia(fechaVigencia).Select(x => x.PathImagen).ToList());
            //rutas.AddRange(new AlmacenGasDataAccess().BuscarImagenesSinVigencia(fechaVigencia).Select(x => x.PathImagen).ToList());
            //rutas.AddRange(new AlmacenGasDataAccess().BuscarImagenesSinVigencia(fechaVigencia).Select(x => x.PathImagen).ToList());
            //rutas.AddRange(new AlmacenGasDataAccess().BuscarImagenesSinVigencia(fechaVigencia).Select(x => x.PathImagen).ToList());
            //rutas.AddRange(new AlmacenGasDataAccess().BuscarImagenesSinVigencia(fechaVigencia).Select(x => x.PathImagen).ToList());
            return rutas;
        }
        public static List<AlmacenGasDescargaFoto> ObtenerImagenes(AlmacenGasDescarga descarga)
        {
            if (descarga.Fotos != null && descarga.Fotos.Count > 0)
                return descarga.Fotos.ToList();

            return new AlmacenGasDescargaDataAccess().BuscarImagenes(descarga.IdAlmacenEntradaGasDescarga);
        }
        public static List<AlmacenGasRecargaFoto> ObtenerImagenes(AlmacenGasRecarga recarga)
        {
            if (recarga.Fotografias != null && recarga.Fotografias.Count > 0)
                return recarga.Fotografias.ToList();

            return new AlmacenGasDataAccess().BuscarImagenes(recarga.IdAlmacenGasRecarga);
        }
        public static List<AlmacenGasTraspasoFoto> ObtenerImagenes(AlmacenGasTraspaso traspaso)
        {
            if (traspaso.Fotografias != null && traspaso.Fotografias.Count > 0)
                return traspaso.Fotografias.ToList();

            return new AlmacenGasDataAccess().BuscarImagenesTraspaso(traspaso.IdEmpresa, traspaso.Year, traspaso.Mes, traspaso.Dia, traspaso.Orden);
        }
        public static List<AlmacenGasAutoConsumoFoto> ObtenerImagenes(AlmacenGasAutoConsumo AutoConsumo)
        {
            if (AutoConsumo.Fotografias != null && AutoConsumo.Fotografias.Count > 0)
                return AutoConsumo.Fotografias.ToList();

            return new AlmacenGasDataAccess().BuscarImagenesAutoConsumo(AutoConsumo.IdEmpresa, AutoConsumo.Year, AutoConsumo.Mes, AutoConsumo.Dia, AutoConsumo.Orden);
        }
        public static List<AlmacenGasCalibracionFoto> ObtenerImagenes(AlmacenGasCalibracion calibracion)
        {
            if (calibracion.Fotografias != null && calibracion.Fotografias.Count > 0)
                return calibracion.Fotografias.ToList();

            return new AlmacenGasDataAccess().BuscarImagenesCalibracion(calibracion.IdCAlmacenGas);
        }
        public static string ObtenerNombreUnidadAlmacenGas(UnidadAlmacenGas uAG)
        {
            if (uAG.EsGeneral) return uAG.Numero;

            var nombre = EquipoTransporteServicio.ObtenerNombre(uAG);
            if (!string.IsNullOrEmpty(nombre))
                return nombre;

            return EstacionCarburacionServicio.ObtenerNombre(uAG);
        }

        public static ReporteDiaDTO ReporteDia(DateTime fecha, short idCAlmacenGas)
        {
            var almacen = ObtenerAlmacen(idCAlmacenGas);
            if (almacen.IdCamioneta !=null && almacen.IdCamioneta>0)
            {
                var cilindros = new AlmacenGasDataAccess().BuscarTodosCilindros(TokenServicio.ObtenerIdEmpresa());
                //Falta agregar los datos de la venta de tanques
                var reporte = new ReporteAdapter().ToDto(almacen);

                reporte.Fecha = DateTime.Now;
                reporte.ClaveReporte = "2018FG675DGD43";
                return reporte;
            }
            else
            {
                var tipoMedidor = TipoMedidorGasServicio.Obtener(almacen.IdTipoMedidor.Value);
                var linicial = BuscarLecturaPorFecha(idCAlmacenGas,TipoEventoEnum.Inicial,fecha);
                var lfinal = BuscarLecturaPorFecha(idCAlmacenGas, TipoEventoEnum.Final,fecha);
                var operador = PuntoVentaServicio.ObtenerOperador(TokenServicio.ObtenerIdUsuario());
                var ventas = PuntoVentaServicio.BuscarPorOperadorChofer(operador.IdOperadorChofer);
                
                //Falta agregar los valores de la venta de gas
                var reporte = new ReporteAdapter().ToDto(almacen, tipoMedidor,linicial,lfinal);
                reporte.Fecha = DateTime.Now;
                reporte.ClaveReporte = "2018FG675DGD43";
                return reporte;
            }
        }

        public static List<AlmacenGasMovimiento> ObtenerMovimientosEnInventario(short idEmpresa, short idAlmacenGas, DateTime fecha)
        {
            return new AlmacenGasDataAccess().BuscarMovimientosEnInventario(idEmpresa, idAlmacenGas, (short)fecha.Year, (byte)fecha.Month, (byte)fecha.Day);
        }

        public static AlmacenGasMovimiento ObtenerUltimoMovimientoEnInventario(short idEmpresa, short idAlmacenGas)
        {
            return new AlmacenGasDataAccess().BuscarUltimoMovimientoEnInventario(idEmpresa, idAlmacenGas);
        }

        public static AlmacenGasMovimiento ObtenerUltimoMovimientoEnInventario(short idEmpresa, short idAlmacenGas, DateTime fecha)
        {
            var ulMov = new AlmacenGasDataAccess().BuscarUltimoMovimientoEnInventario(idEmpresa, idAlmacenGas, (short)fecha.Year, (byte)fecha.Month, (byte)fecha.Day);
            if (ulMov != null) return ulMov;

            ulMov = ObtenerUltimoMovimientoEnInventario(idEmpresa, idAlmacenGas);
            if (ulMov != null) return ulMov;

            return AlmacenGasAdapter.FromInit();
        }

        public static AlmacenGasMovimiento ObtenerUltimoMovimientoEnInventario(UnidadAlmacenGas unidad, DateTime fecha)
        {
            return new AlmacenGasDataAccess().BuscarUltimoMovimientoEnInventario(unidad.IdAlmacenGas.Value, unidad.IdCAlmacenGas, unidad.IdEmpresa, (short)fecha.Year, (byte)fecha.Month, (byte)fecha.Day);
        }

        public static List<AlmacenGasMovimiento> ObtenerUltimosMovimientosDeDescargas(AlmacenGasDescarga descarga, short idEmpresa)
        {
            var ulMovDia = new AlmacenGasDataAccess().BuscarUltimoMovimientoConTipoEvento(idEmpresa, TipoEventoEnum.Descarga, (short)descarga.FechaFinDescarga.Value.Year, (byte)descarga.FechaFinDescarga.Value.Month, (byte)descarga.FechaFinDescarga.Value.Day);
            var ulMovMes = new AlmacenGasDataAccess().BuscarUltimoMovimientoConTipoEvento(idEmpresa, TipoEventoEnum.Descarga, (short)descarga.FechaFinDescarga.Value.Year, (byte)descarga.FechaFinDescarga.Value.Month);
            var ulMovAnio = new AlmacenGasDataAccess().BuscarUltimoMovimientoConTipoEvento(idEmpresa, TipoEventoEnum.Descarga, (short)descarga.FechaFinDescarga.Value.Year);

            return new List<AlmacenGasMovimiento>()
            {
                ulMovDia, ulMovMes, ulMovAnio
            };
        }

        public static List<AlmacenGasMovimiento> ObtenerUltimosMovimientosDeDescargasPorUnidadAlmacenGas(short idEmpresa, short idCAlmacenGas, DateTime fecha)
        {
            var ulMovDia = new AlmacenGasDataAccess().BuscarUltimoMovimientoPorUnidadAlamcenGasConTipoEvento(idEmpresa, idCAlmacenGas, TipoEventoEnum.Descarga, (short)fecha.Year, (byte)fecha.Month, (byte)fecha.Day);
            var ulMovMes = new AlmacenGasDataAccess().BuscarUltimoMovimientoPorUnidadAlamcenGasConTipoEvento(idEmpresa, idCAlmacenGas, TipoEventoEnum.Descarga, (short)fecha.Year, (byte)fecha.Month);
            var ulMovAnio = new AlmacenGasDataAccess().BuscarUltimoMovimientoPorUnidadAlamcenGasConTipoEvento(idEmpresa, idCAlmacenGas, TipoEventoEnum.Descarga, (short)fecha.Year);

            return new List<AlmacenGasMovimiento>()
            {
                ulMovDia, ulMovMes, ulMovAnio
            };
        }
        public static AlmacenGasMovimiento ObtenerUltimoMovimientoDeDescargaPorUnidadAlmacenGas(short idEmpresa, short idCAlmacenGas, DateTime fecha)
        {
            var movimientos = ObtenerUltimosMovimientosDeDescargasPorUnidadAlmacenGas(idEmpresa, idCAlmacenGas, fecha);

            if (movimientos.ElementAt(0) != null) return movimientos.ElementAt(0);

            if (movimientos.ElementAt(1) != null) return movimientos.ElementAt(1);

            if (movimientos.ElementAt(2) != null) return movimientos.ElementAt(2);

            return AlmacenGasAdapter.FromInit();
        }
        public static AlmacenGasTomaLectura BuscarLecturaPorFecha(short idCAlmacenGas,byte tipoEvento,DateTime fecha)
        {
            return new AlmacenGasDataAccess().BuscarLectura(idCAlmacenGas, tipoEvento, fecha);
        }
        public static decimal ObtenerCantidadActualAlmacenGeneral(short IdEmpresa, bool EnLitros = true)
        {
            var almacenGas = new AlmacenGasDataAccess().ProductoAlmacenGas(IdEmpresa);
            if (EnLitros)
                return almacenGas.UnidadesAlmacenGas.Where(z => z.EsGeneral).Sum(x => x.CantidadActualLt);
            else
                return almacenGas.UnidadesAlmacenGas.Where(z => z.EsGeneral).Sum(x => x.CantidadActualKg);

        }
        public static UnidadAlmacenGasCilindro ObtenerCilindro(int idCilindro)
        {
            return new AlmacenGasDataAccess().BuscarCilindro(idCilindro);
        }
        public static UnidadAlmacenGasCilindro ObtenerCilindro(AlmacenGasRecargaCilindro cilindro)
        {
            if (cilindro.UnidadAlmacenGasCilindro != null)
                return cilindro.UnidadAlmacenGasCilindro;

            return new AlmacenGasDataAccess().BuscarCilindro(cilindro.IdCilindro);
        }
        public static CamionetaCilindro ObtenerCilindro(UnidadAlmacenGasCilindro cilindro, int idCilindro)
        {
            if (cilindro.CilindrosCamionetas != null && cilindro.CilindrosCamionetas.Count > 0)
                return cilindro.CilindrosCamionetas.FirstOrDefault(x => x.IdCilindro.Equals(idCilindro));

            return new AlmacenGasDataAccess().BuscarCilindroEnCamioneta(idCilindro);
        }
        public static CamionetaCilindro ObtenerCilindro(List<CamionetaCilindro> cilindros, int idCilindro)
        {
            if (cilindros != null && cilindros.Count > 0)
                return cilindros.FirstOrDefault(x => x.IdCilindro.Equals(idCilindro));

            return new AlmacenGasDataAccess().BuscarCilindroEnCamioneta(idCilindro);
        }
        public static List<UnidadAlmacenGasCilindro> ObtenerCilindros()
        {
            return new AlmacenGasDataAccess().BuscarTodosCilindros(TokenServicio.ObtenerIdEmpresa());
        }
        public static List<CamionetaCilindro> ObtenerCilindros(UnidadAlmacenGas unidad)
        {
            if (unidad.Camioneta != null)
                if (unidad.Camioneta.Cilindros != null && unidad.Camioneta.Cilindros.Count > 0)
                    return unidad.Camioneta.Cilindros.ToList();

            return new AlmacenGasDataAccess().BuscarTodosCilindros(unidad.IdCamioneta.Value);
        }
        /// <summary>
        /// Adaptamos una entidad AlmacenGasTomaLEcturaCilindro en una UnidadAlmacenGasCilindro.
        /// Solo en la cantidad de cilindros, y este método es especial ya que se hizo para la toma
        /// de lecturas de Camionetas
        /// </summary>
        /// <param name="tmCil"></param>
        /// <returns></returns>
        public static UnidadAlmacenGasCilindro AdaptarCilindro(AlmacenGasTomaLecturaCilindro tmCil)
        {
            var cil = ObtenerCilindro(tmCil.IdCilindro);
            if (cil != null)
                cil.Cantidad = tmCil.Cantidad;

            return cil;
        }

        public static UnidadAlmacenGasCilindro AdaptarCilindro(UnidadAlmacenGasCilindro cil, decimal cantidad)
        {
            if (cil != null)
                cil.Cantidad = cantidad;

            return cil;
        }
        public static List<UnidadAlmacenGasCilindro> AdaptarCilindro(List<AlmacenGasTomaLecturaCilindro> tmCil)
        {            
            return tmCil.Select(x=> AdaptarCilindro(x)).ToList();
        }

        public static List<UnidadAlmacenGas> ObtenerAlmacenes(short idEmpresa)
        {
            var al = new AlmacenGasDataAccess().BuscarTodas(idEmpresa);
            return al.Where(x => (x.IdPipa != null || x.IdCamioneta != null || x.IdEstacionCarburacion != null)).ToList();
        }

        public static List<UnidadAlmacenGasCilindro> AdaptarCilindro(decimal cantidad)
        {
            var cilindros = new List<UnidadAlmacenGasCilindro>();

            foreach (var cil in ObtenerCilindros())
                cilindros.Add(AdaptarCilindro(cil, cantidad));

            return cilindros;
        }

        public static identidadUnidadAlmacenGas IdentificarTipoUnidadAlamcenGas(UnidadAlmacenGas unidad)
        {
            if (unidad.EsGeneral && unidad.EsAlterno)
                return identidadUnidadAlmacenGas.AlmacenAlterno;

            if (unidad.EsGeneral && unidad.EsAlterno.Equals(false))
                return identidadUnidadAlmacenGas.AlmacenPrincipal;

            if (unidad.IdEstacionCarburacion != null && unidad.IdEstacionCarburacion > 0)
                return identidadUnidadAlmacenGas.EstacionCarburacion;

            if (unidad.IdPipa != null && unidad.IdPipa > 0)
                return identidadUnidadAlmacenGas.Pipa;

            //if (unidad.IdCamioneta != null && unidad.IdCamioneta > 0)
            return identidadUnidadAlmacenGas.Camioneta;
        }
        public static stringUnidad IdentificarTipoUnidadAlamcenGasString(UnidadAlmacenGas unidad)
        {
            if (unidad.EsGeneral && unidad.EsAlterno.Equals(false))
                return stringUnidad.AlmacenPrincipal;

            if (unidad.EsGeneral && unidad.EsAlterno)
                return stringUnidad.AlmacenAlterno;

            if (unidad.IdEstacionCarburacion != null && unidad.IdEstacionCarburacion > 0)
                return stringUnidad.EstacionCarburacion;

            if (unidad.IdPipa != null && unidad.IdPipa > 0)
                return stringUnidad.Pipa;

            //if (unidad.IdCamioneta != null && unidad.IdCamioneta > 0)
            return stringUnidad.Camioneta;
        }

        public static void ProcesarInventario()
        {
            //var lecturas = LecturaGasServicio.ObtenerTomaLectura();
            var descargasDto = AplicarDescargas();
            var recargasDto = AplicarRecargas();
            //var traspasosDto = AplicarTraspaso();
            //var autoConsumosDto = AplicarAutoConsumo();
        }
        
        public static void CalcularInventarioAlmacenPrincipal(UnidadAlmacenGas unidad)
        {
            //var lecturas = ObtenerTomaLecturasDatosNoProcesados(unidad);
            //var lecturas = ObtenerTomaLecturasDatosNoProcesados(unidad);
            //var lecturas = ObtenerTomaLecturasDatosNoProcesados(unidad);
            //var lecturas = ObtenerTomaLecturasDatosNoProcesados(unidad);
            //var lecturas = ObtenerTomaLecturasDatosNoProcesados(unidad);
            //var lecturas = ObtenerTomaLecturasDatosNoProcesados(unidad);
            //var lecturas = ObtenerTomaLecturasDatosNoProcesados(unidad);
            //var lecturas = ObtenerTomaLecturasDatosNoProcesados(unidad);
        }
        public static List<AplicaDescargaDto> AplicarDescargas()
        {
            List<AplicaDescargaDto> aplicaciones = new List<AplicaDescargaDto>();
            List<AlmacenGasDescarga> descargasGas = ObtenerDescargasNoProcesadas();

            if (descargasGas != null && descargasGas.Count > 0)
            {
                descargasGas.ForEach(x => aplicaciones.Add(AplicarDescarga(x)));
                //new AlmacenGasDescargaDataAccess().Actualizar(aplicaciones);
            }

            return aplicaciones;
        }

        public static AplicaDescargaDto AplicarDescarga(AlmacenGasDescarga descarga)
        {
            Empresa empresa = EmpresaServicio.Obtener(descarga);
            UnidadAlmacenGas unidadEntrada = AlmacenGasServicio.ObtenerUnidadAlamcenGasActualizaAlterno(descarga, empresa);

            AplicaDescargaDto apDescDto = AplicarDescarga(unidadEntrada, descarga, empresa);
            apDescDto = OrdenCompraServicio.AplicarDescarga(apDescDto, descarga, empresa);

            new AlmacenGasDescargaDataAccess().Actualizar(apDescDto);

            return apDescDto;
        }

        public static AplicaDescargaDto AplicarDescarga(UnidadAlmacenGas unidadEntrada, AlmacenGasDescarga descarga, Empresa empresa)
        {            
            decimal kilogramosPapeletaTractor = descarga.MasaKg.Value;
            decimal litrosPapeletaTractor = CalcularGasServicio.ObtenerLitrosDesdeKilos(kilogramosPapeletaTractor, empresa.FactorLitrosAKilos);
            decimal litrosRealesTractor = CalcularGasServicio.ObtenerLitrosEnElTanque(descarga.CapacidadTanqueLt.Value, descarga.PorcenMagnatelOcularTractorINI.Value);
            decimal kilogramosRealesTractor = CalcularGasServicio.ObtenerKilogramosDesdeLitros(litrosRealesTractor, empresa.FactorLitrosAKilos);
            decimal kilogramosRemanentes = CalcularGasServicio.ObtenerDiferenciaKilogramos(kilogramosRealesTractor, kilogramosPapeletaTractor);
            decimal litrosRemanentes = CalcularGasServicio.ObtenerLitrosDesdeKilos(kilogramosRemanentes, empresa.FactorLitrosAKilos);

            decimal unidadEntradaCantidadKg = unidadEntrada.CantidadActualKg;
            decimal unidadEntradaCantidadLt = unidadEntrada.CantidadActualLt;
            decimal unidadEntradaPorcentaje = unidadEntrada.PorcentajeActual;

            unidadEntrada.CantidadActualKg = CalcularGasServicio.SumarKilogramos(unidadEntrada.CantidadActualKg, kilogramosRealesTractor);
            unidadEntrada.CantidadActualLt = CalcularGasServicio.ObtenerLitrosDesdeKilos(unidadEntrada.CantidadActualKg, empresa.FactorLitrosAKilos);
            unidadEntrada.PorcentajeActual = descarga.PorcenMagnatelOcularAlmacenFIN.Value;
            
            unidadEntrada = AplicarDescargaAlmacenAlterno(unidadEntrada, descarga);

            AlmacenGas almacenGasTotal = ObtenerAlmacenGasTotal(empresa);
            decimal almacenTotalCantidadActualKg = almacenGasTotal.CantidadActualKg;
            decimal almacenTotalCantidadActualLt = almacenGasTotal.CantidadActualLt;
            decimal almacenTotalPorcent = almacenGasTotal.PorcentajeActual;
            decimal almacenGeneralCantidadActualKg = almacenGasTotal.CantidadActualGeneralKg;
            decimal almacenGeneralCantidadActualLt = almacenGasTotal.CantidadActualGeneralLt;
            decimal almacenGeneralPorcent = almacenGasTotal.PorcentajeActualGeneral;
            almacenGasTotal = AplicarDescargaAlmacenTotal(almacenGasTotal, unidadEntrada, litrosRealesTractor, kilogramosRealesTractor);

            AlmacenGasMovimiento ulMov = ObtenerUltimoMovimientoEnInventario(empresa.IdEmpresa, almacenGasTotal.IdAlmacenGas, descarga.FechaFinDescarga.Value);
            AlmacenGasMovimiento ulMovDescarga = ObtenerUltimoMovimientoDeDescargaPorUnidadAlmacenGas(empresa.IdEmpresa, unidadEntrada.IdCAlmacenGas, descarga.FechaFinDescarga.Value);
            //RemanenteDto remaDto = RemanenteServicio.ObtenerRemanente(descarga, almacenGasTotal.IdAlmacenGas, empresa.IdEmpresa);

            var invAnterior = new InventarioAnteriorDto
            {
                EntradaKg = kilogramosRealesTractor,
                EntradaLt = litrosRealesTractor,
                CantidadAnteriorKg = unidadEntradaCantidadKg,
                CantidadAnteriorLt = unidadEntradaCantidadLt,
                PorcentajeAnterior = unidadEntradaPorcentaje,
                P5000Anterior = null,

                CAlmEntradaDiaKg = CalcularGasServicio.SumarKilogramos(ulMovDescarga.CAlmEntradaDiaKg, kilogramosRealesTractor),
                CAlmEntradaDiaLt = CalcularGasServicio.SumarLitros(ulMovDescarga.CAlmEntradaDiaLt, litrosRealesTractor),
                CAlmEntradaMesKg = CalcularGasServicio.SumarKilogramos(ulMovDescarga.CAlmEntradaMesKg, kilogramosRealesTractor),
                CAlmEntradaMesLt = CalcularGasServicio.SumarLitros(ulMovDescarga.CAlmEntradaMesLt, litrosRealesTractor),
                CAlmEntradaAnioKg = CalcularGasServicio.SumarKilogramos(ulMovDescarga.CAlmEntradaAnioKg, kilogramosRealesTractor),
                CAlmEntradaAnioLt = CalcularGasServicio.SumarLitros(ulMovDescarga.CAlmEntradaAnioLt, litrosRealesTractor),
                CAlmSalidaDiaKg = ulMovDescarga.CAlmSalidaDiaKg,
                CAlmSalidaDiaLt = ulMovDescarga.CAlmSalidaDiaLt,
                CAlmSalidaMesKg = ulMovDescarga.CAlmSalidaMesKg,
                CAlmSalidaMesLt = ulMovDescarga.CAlmSalidaMesLt,
                CAlmSalidaAnioKg = ulMovDescarga.CAlmSalidaAnioKg,
                CAlmSalidaAnioLt = ulMovDescarga.CAlmSalidaAnioLt,
                CantidadAcumuladaDiaKg = CalcularGasServicio.SumarKilogramos(ulMov.CantidadAcumuladaDiaKg, kilogramosRealesTractor),
                CantidadAcumuladaDiaLt = CalcularGasServicio.SumarLitros(ulMov.CantidadAcumuladaDiaLt, litrosRealesTractor),
                CantidadAcumuladaMesKg = CalcularGasServicio.SumarKilogramos(ulMov.CantidadAcumuladaMesKg, kilogramosRealesTractor),
                CantidadAcumuladaMesLt = CalcularGasServicio.SumarLitros(ulMov.CantidadAcumuladaMesLt, litrosRealesTractor),
                CantidadAcumuladaAnioKg = CalcularGasServicio.SumarKilogramos(ulMov.CantidadAcumuladaAnioKg, kilogramosRealesTractor),
                CantidadAcumuladaAnioLt = CalcularGasServicio.SumarLitros(ulMov.CantidadAcumuladaAnioLt, litrosRealesTractor),
                
                RemaKg = kilogramosRemanentes,
                RemaLt = litrosRemanentes,
                RemaDiaKg = CalcularGasServicio.SumarKilogramos(ulMovDescarga.RemaDiaKg != null ? ulMovDescarga.RemaDiaKg.Value : 0, kilogramosRemanentes),
                RemaDiaLt = CalcularGasServicio.SumarLitros(ulMovDescarga.RemaDiaLt != null ? ulMovDescarga.RemaDiaLt.Value : 0, litrosRemanentes),
                RemaMesKg = CalcularGasServicio.SumarKilogramos(ulMovDescarga.RemaMesKg != null ? ulMovDescarga.RemaMesKg.Value : 0, kilogramosRemanentes),
                RemaMesLt = CalcularGasServicio.SumarLitros(ulMovDescarga.RemaMesLt != null ? ulMovDescarga.RemaMesLt.Value : 0, litrosRemanentes),
                RemaAnioKg = CalcularGasServicio.SumarKilogramos(ulMovDescarga.RemaAnioKg != null ?ulMovDescarga.RemaAnioKg.Value : 0, kilogramosRemanentes),
                RemaAnioLt = CalcularGasServicio.SumarLitros(ulMovDescarga.RemaAnioLt != null ? ulMovDescarga.RemaAnioLt.Value : 0, litrosRemanentes),
                RemaAcumDiaKg = CalcularGasServicio.SumarKilogramos(ulMov.RemaAcumDiaKg, kilogramosRemanentes),
                RemaAcumDiaLt = CalcularGasServicio.SumarLitros(ulMov.RemaAcumDiaLt, litrosRemanentes),
                RemaAcumMesKg = CalcularGasServicio.SumarKilogramos(ulMov.RemaAcumMesKg, kilogramosRemanentes),
                RemaAcumMesLt = CalcularGasServicio.SumarLitros(ulMov.RemaAcumMesLt, litrosRemanentes),
                RemaAcumAnioKg = CalcularGasServicio.SumarKilogramos(ulMov.RemaAcumAnioKg, kilogramosRemanentes),
                RemaAcumAnioLt = CalcularGasServicio.SumarLitros(ulMov.RemaAcumAnioLt, litrosRemanentes),
                
                DescargaKg = kilogramosRemanentes,
                DescargaLt = litrosRemanentes,
                DescargaDiaKg = CalcularGasServicio.SumarKilogramos(ulMovDescarga.DescargaDiaKg != null ? ulMovDescarga.DescargaDiaKg.Value : 0, kilogramosRemanentes),
                DescargaDiaLt = CalcularGasServicio.SumarLitros(ulMovDescarga.DescargaDiaLt != null ? ulMovDescarga.DescargaDiaLt.Value : 0, litrosRemanentes),
                DescargaMesKg = CalcularGasServicio.SumarKilogramos(ulMovDescarga.DescargaMesKg != null ? ulMovDescarga.DescargaMesKg.Value : 0, kilogramosRemanentes),
                DescargaMesLt = CalcularGasServicio.SumarLitros(ulMovDescarga.DescargaMesLt != null ? ulMovDescarga.DescargaMesLt.Value : 0, litrosRemanentes),
                DescargaAnioKg = CalcularGasServicio.SumarKilogramos(ulMovDescarga.DescargaAnioKg != null ? ulMovDescarga.DescargaAnioKg.Value : 0, kilogramosRemanentes),
                DescargaAnioLt = CalcularGasServicio.SumarLitros(ulMovDescarga.DescargaAnioLt != null ? ulMovDescarga.DescargaAnioLt.Value : 0, litrosRemanentes),
                DescargaAcumDiaKg = CalcularGasServicio.SumarKilogramos(ulMov.DescargaAcumDiaKg, kilogramosRemanentes),
                DescargaAcumDiaLt = CalcularGasServicio.SumarLitros(ulMov.DescargaAcumDiaLt, litrosRemanentes),
                DescargaAcumMesKg = CalcularGasServicio.SumarKilogramos(ulMov.DescargaAcumMesKg, kilogramosRemanentes),
                DescargaAcumMesLt = CalcularGasServicio.SumarLitros(ulMov.DescargaAcumMesLt, litrosRemanentes),
                DescargaAcumAnioKg = CalcularGasServicio.SumarKilogramos(ulMov.DescargaAcumAnioKg, kilogramosRemanentes),
                DescargaAcumAnioLt = CalcularGasServicio.SumarLitros(ulMov.DescargaAcumAnioLt, litrosRemanentes),
                
                CantidadAnteriorTotalKg = almacenTotalCantidadActualKg,
                CantidadAnteriorTotalLt = almacenTotalCantidadActualLt,
                PorcentajeAnteriorTotal = almacenTotalPorcent,
                CantidadAnteriorGeneralKg = almacenGeneralCantidadActualKg,
                CantidadAnteriorGeneralLt = almacenGeneralCantidadActualLt,
                PorcentajeAnteriorGeneral = almacenGeneralPorcent,
            };

            return new AplicaDescargaDto()
            {
                AlmacenGas = AlmacenGasAdapter.FromEntity(almacenGasTotal),
                Descarga = descarga,
                DescargaSinNavigationProperties = AlmacenGasAdapter.FromEntity(descarga),
                DescargaFotos = GenerarImagenes(descarga),
                unidadEntrada = AlmacenGasAdapter.FromEntity(unidadEntrada),
                identidadUE = IdentificarTipoUnidadAlamcenGas(unidadEntrada),
                Movimiento = AlmacenGasAdapter.FromEntity(unidadEntrada, descarga, almacenGasTotal, ulMov, empresa, invAnterior),
            };
        }

        public static UnidadAlmacenGas ObtenerAlmacen(short idCAlmacenGas)
        {
            return new AlmacenGasDataAccess().BuscarAlmacen(idCAlmacenGas);
        }

        public static UnidadAlmacenGas AplicarDescargaAlmacenAlterno(UnidadAlmacenGas unidadEntrada, AlmacenGasDescarga descarga)
        {
            if (unidadEntrada.EsAlterno)
            {
                unidadEntrada.CapacidadTanqueLt = CalcularGasServicio.SumarLitros(unidadEntrada.CapacidadTanqueLt.Value, descarga.CapacidadTanqueLt.Value);
                unidadEntrada.CapacidadTanqueKg = CalcularGasServicio.SumarKilogramos(unidadEntrada.CapacidadTanqueKg.Value, descarga.CapacidadTanqueKg.Value);
                unidadEntrada.PorcentajeActual = descarga.PorcenMagnatelOcularTractorINI.Value;
            }

            return unidadEntrada;
        }

        public static AlmacenGas AplicarDescargaAlmacenTotal(AlmacenGas almacen, UnidadAlmacenGas unidadEntrada, decimal litrosRealesTractor, decimal kilogramosRealesTractor)
        {
            almacen.CantidadActualLt = CalcularGasServicio.SumarLitros(almacen.CantidadActualLt, litrosRealesTractor); 
            almacen.CantidadActualKg = CalcularGasServicio.SumarKilogramos(almacen.CantidadActualKg, kilogramosRealesTractor);
            almacen.PorcentajeActual = CalcularGasServicio.ObtenerPorcentajeDesdeLitros(almacen.CapacidadTotalLt, almacen.CantidadActualLt);

            almacen.CantidadActualGeneralLt = CalcularGasServicio.SumarLitros(almacen.CantidadActualGeneralLt, litrosRealesTractor);
            almacen.CantidadActualGeneralKg = CalcularGasServicio.SumarKilogramos(almacen.CantidadActualGeneralKg, kilogramosRealesTractor);
            almacen.PorcentajeActualGeneral = CalcularGasServicio.ObtenerPorcentajeDesdeLitros(almacen.CapacidadGeneralLt, almacen.CantidadActualGeneralLt);

            if (unidadEntrada.EsAlterno)
            {
                almacen.CapacidadTotalLt = CalcularGasServicio.SumarLitros(almacen.CapacidadTotalLt, unidadEntrada.CapacidadTanqueLt.Value);
                almacen.CapacidadTotalKg = CalcularGasServicio.SumarKilogramos(almacen.CapacidadTotalKg, unidadEntrada.CapacidadTanqueKg.Value);
                almacen.PorcentajeActual = CalcularGasServicio.ObtenerPorcentajeDesdeLitros(almacen.CapacidadTotalLt, almacen.CantidadActualLt);

                almacen.CapacidadGeneralLt = CalcularGasServicio.SumarLitros(almacen.CapacidadGeneralLt, unidadEntrada.CapacidadTanqueLt.Value);
                almacen.CapacidadGeneralKg = CalcularGasServicio.SumarKilogramos(almacen.CapacidadGeneralKg, unidadEntrada.CapacidadTanqueKg.Value);
                almacen.PorcentajeActual = CalcularGasServicio.ObtenerPorcentajeDesdeLitros(almacen.CapacidadGeneralLt, almacen.CantidadActualGeneralLt);
            }

            return almacen;
        }

        public static List<AlmacenGasDescargaFoto> GenerarImagenes(AlmacenGasDescarga descarga)
        {
            List<AlmacenGasDescargaFoto> imagenes = ObtenerImagenes(descarga);

            var fotos = new List<AlmacenGasDescargaFoto>();

            if (imagenes != null && imagenes.Count > 0)
            {
                foreach (var imagen in imagenes)
                {
                    var img = ImagenServicio.ObtenerImagen(imagen);
                    var foto = AlmacenGasAdapter.FromEntity(img);
                    fotos.Add(foto);
                }
            }

            return fotos;
        }
        
        public static List<AplicaRecargaDto> AplicarRecargas()
        {
            List<AplicaRecargaDto> aplicaciones = new List<AplicaRecargaDto>();
            List<AlmacenGasRecarga> recargasGas = ObtenerRecargasNoProcesadas();

            List<AlmacenGasRecarga> recargasGasIniciales = recargasGas.Where(x => x.IdTipoEvento.Equals(TipoEventoEnum.Inicial)).ToList();
            List<AlmacenGasRecarga> recargasGasFinales = recargasGas.Where(x => x.IdTipoEvento.Equals(TipoEventoEnum.Final)).ToList();

            if (recargasGasIniciales != null && recargasGasIniciales.Count > 0)
            {
                recargasGasIniciales.ForEach(x => aplicaciones.Add(AplicarRecarga(x, recargasGasFinales)));
                //new AlmacenGasRecargaDataAccess().Actualizar(aplicaciones);
            }

            return aplicaciones;
        }
        public static AplicaRecargaDto AplicarRecarga(AlmacenGasRecarga recargaInicial, List<AlmacenGasRecarga> recargasFinales)
        {
            AplicaRecargaDto apReDto = new AplicaRecargaDto()
            {
                RecargaLecturaInicial = recargaInicial,
                RecargasFinales = recargasFinales,
                unidadSalida = AlmacenGasServicio.ObtenerUnidadAlamcenGas(recargaInicial, true),
                unidadEntrada = AlmacenGasServicio.ObtenerUnidadAlamcenGas(recargaInicial, false),
            };

            apReDto.Empresa = EmpresaServicio.Obtener(apReDto.unidadEntrada);
            apReDto = AplicarRecarga(apReDto);

            return apReDto;
        }

        public static AplicaRecargaDto AplicarRecarga(AplicaRecargaDto apReDto)
        {
            apReDto.identidadUS = IdentificarTipoUnidadAlamcenGas(apReDto.unidadSalida);
            apReDto.identidadUE = IdentificarTipoUnidadAlamcenGas(apReDto.unidadEntrada);

            switch (apReDto.identidadUE)
            {
                case identidadUnidadAlmacenGas.Pipa: apReDto = AplicarRecargaPipa(apReDto); break;
                case identidadUnidadAlmacenGas.EstacionCarburacion: apReDto = AplicarRecargaEstacion(apReDto); break;
                case identidadUnidadAlmacenGas.Camioneta: apReDto = AplicarRecargaCamioneta(apReDto); break;
            }

            new AlmacenGasDataAccess().Actualizar(apReDto);
            return apReDto;
        }

        public static AplicaRecargaDto AplicarRecargaEstacion(AplicaRecargaDto apReDto)
        {
            apReDto.RecargaLecturaFinal = apReDto.RecargasFinales.FirstOrDefault(x => x.IdCAlmacenGasEntrada.Equals(apReDto.RecargaLecturaInicial.IdCAlmacenGasEntrada));

            if (apReDto.RecargaLecturaFinal == null)
                return new AplicaRecargaDto();

            decimal LitrosRecargadosP5000 = CalcularGasServicio.ObtenerDiferenciaLecturaP5000(apReDto.RecargaLecturaFinal.P5000Salida.Value, apReDto.RecargaLecturaInicial.P5000Salida.Value);
            //decimal LitrosRecargadosPorcentaje = CalcularGasServicio.ObtenerDiferenciaPorcentaje(apReDto.RecargaLecturaFinal.ProcentajeEntrada.Value, apReDto.RecargaLecturaInicial.ProcentajeEntrada.Value);
            decimal LitrosRecargados = CalcularGasServicio.RestarLitrosDesdePorcentaje(LitrosRecargadosP5000, apReDto.unidadSalida.PorcentajeCalibracionPlaneada);
            decimal KilosRecargados = CalcularGasServicio.ObtenerKilogramosDesdeLitros(LitrosRecargados, apReDto.Empresa.FactorLitrosAKilos);
                        
            apReDto = AplicarRecarga(apReDto, LitrosRecargados, KilosRecargados);

            return apReDto;
        }

        public static AplicaRecargaDto AplicarRecargaPipa(AplicaRecargaDto apReDto)
        {
            apReDto.RecargaLecturaFinal = apReDto.RecargasFinales.FirstOrDefault(x => x.IdCAlmacenGasEntrada.Equals(apReDto.RecargaLecturaInicial.IdCAlmacenGasEntrada));
                        
            if (apReDto.RecargaLecturaFinal == null)
                return new AplicaRecargaDto();

            decimal porcentajeRecargadoEnUnidadEntrada = CalcularGasServicio.ObtenerDiferenciaPorcentaje(apReDto.RecargaLecturaFinal.ProcentajeEntrada.Value, apReDto.RecargaLecturaInicial.ProcentajeEntrada.Value);
            decimal LitrosRecargados = CalcularGasServicio.ObtenerLitrosDesdePorcentaje(apReDto.unidadEntrada.CapacidadTanqueLt.Value, porcentajeRecargadoEnUnidadEntrada);
            decimal KilosRecargados = CalcularGasServicio.ObtenerKilogramosDesdeLitros(LitrosRecargados, apReDto.Empresa.FactorLitrosAKilos);
            
            apReDto = AplicarRecarga(apReDto, LitrosRecargados, KilosRecargados);

            //apReDto.AlmacenGas.CantidadActualLt = CalcularGasServicio.RestarLitros(apReDto.AlmacenGas.CantidadActualLt, LitrosRecargados);
            //apReDto.AlmacenGas.CantidadActualKg = CalcularGasServicio.RestarKilogramos(apReDto.AlmacenGas.CantidadActualKg, KilosRecargados);
            //apReDto.AlmacenGas.PorcentajeActual = CalcularGasServicio.ObtenerPorcentajeDesdeLitros(apReDto.AlmacenGas.CapacidadTotalLt, LitrosRecargados);

            return apReDto;
        }

        public static AplicaRecargaDto AplicarRecargaCamioneta(AplicaRecargaDto apReDto)
        {
            apReDto.CilindrosEnCamionetaInsertar = new List<CamionetaCilindro>();
            apReDto.CilindrosEnCamionetaModificar = new List<CamionetaCilindro>();
            apReDto.CilindrosEnCamionetaEliminar = new List<CamionetaCilindro>();

            List<CamionetaCilindro> CilindrosEnCamioneta = ObtenerCilindros(apReDto.unidadEntrada);
            decimal KilosRecargados = 0;

            foreach (var cilindro in apReDto.RecargaLecturaInicial.Cilindros)
            {
                UnidadAlmacenGasCilindro cilindroUA = ObtenerCilindro(cilindro);
                CamionetaCilindro cilindroCam = ObtenerCilindro(CilindrosEnCamioneta, cilindro.IdCilindro);
                if (cilindroCam != null)
                {                    
                    cilindroCam.Cantidad = cilindro.Cantidad;
                    cilindroCam = AlmacenGasAdapter.FromEntity(cilindroCam);
                    apReDto.CilindrosEnCamionetaModificar.Add(cilindroCam);
                    CilindrosEnCamioneta.RemoveAt(CilindrosEnCamioneta.FindIndex(x => x.IdCilindro.Equals(cilindroCam.IdCilindro)));
                }
                else
                {                    
                    cilindroCam = AlmacenGasAdapter.FromEntity(cilindro, apReDto.unidadEntrada, cilindroUA);
                    apReDto.CilindrosEnCamionetaInsertar.Add(cilindroCam);
                }

                KilosRecargados = CalcularGasServicio.SumarKilogramos(KilosRecargados, cilindroUA.CapacidadKg, cilindro.Cantidad);
            }

            foreach (var cilindro in CilindrosEnCamioneta)
            {
                if (apReDto.RecargaLecturaInicial.Cilindros.FirstOrDefault (x => x.IdCilindro.Equals(cilindro.IdCilindro)) == null)
                {
                    CamionetaCilindro cilindroCam = AlmacenGasAdapter.FromEntity(cilindro);
                    apReDto.CilindrosEnCamionetaEliminar.Add(cilindroCam);
                }
            }

            decimal LitrosRecargados = CalcularGasServicio.ObtenerLitrosDesdeKilos(KilosRecargados, apReDto.Empresa.FactorLitrosAKilos);
            apReDto = AplicarRecarga(apReDto, LitrosRecargados, KilosRecargados);

            return apReDto;
        }

        public static AplicaRecargaDto AplicarRecarga(AplicaRecargaDto apReDto, decimal LitrosRecargados, decimal KilosRecargados)
        {
            apReDto.unidadEntrada.CantidadActualLt = CalcularGasServicio.SumarLitros(apReDto.unidadEntrada.CantidadActualLt, LitrosRecargados);
            apReDto.unidadEntrada.CantidadActualKg = CalcularGasServicio.SumarKilogramos(apReDto.unidadEntrada.CantidadActualKg, KilosRecargados);
            
            apReDto.unidadSalida.CantidadActualLt = CalcularGasServicio.RestarLitros(apReDto.unidadSalida.CantidadActualLt, LitrosRecargados);
            apReDto.unidadSalida.CantidadActualKg = CalcularGasServicio.RestarKilogramos(apReDto.unidadSalida.CantidadActualKg, KilosRecargados);
            apReDto.unidadSalida.PorcentajeActual = CalcularGasServicio.ObtenerPorcentajeDesdeLitros(apReDto.unidadSalida.CapacidadTanqueLt.Value, apReDto.unidadSalida.CantidadActualLt);

            apReDto.RecargaLecturaInicialFotos = GenerarImagenes(apReDto.RecargaLecturaInicial);
            apReDto.RecargaLecturaInicial.DatosProcesados = true;

            apReDto.unidadEntrada = AlmacenGasAdapter.FromEntity(apReDto.unidadEntrada);
            apReDto.unidadSalida = AlmacenGasAdapter.FromEntity(apReDto.unidadSalida);

            if (apReDto.identidadUE != identidadUnidadAlmacenGas.Camioneta)
            {
                apReDto.unidadEntrada.PorcentajeActual = apReDto.RecargaLecturaFinal.ProcentajeEntrada.Value;
                apReDto.RecargaLecturaFinalFotos = GenerarImagenes(apReDto.RecargaLecturaFinal);
                apReDto.RecargaLecturaFinal.DatosProcesados = true;
                apReDto.RecargaLecturaFinalSinNavProp = AlmacenGasAdapter.FromEntity(apReDto.RecargaLecturaFinal);
            }
            
            apReDto.RecargaLecturaInicialSinNavProp = AlmacenGasAdapter.FromEntity(apReDto.RecargaLecturaInicial);

            return apReDto;
        }

        public static List<AlmacenGasRecargaFoto> GenerarImagenes(AlmacenGasRecarga recarga)
        {
            List<AlmacenGasRecargaFoto> imagenes = ObtenerImagenes(recarga);

            var fotos = new List<AlmacenGasRecargaFoto>();

            if (imagenes != null && imagenes.Count > 0)
            {
                foreach (var imagen in imagenes)
                {
                    var img = ImagenServicio.ObtenerImagen(imagen);
                    var foto = AlmacenGasAdapter.FromEntity(img);
                    fotos.Add(foto);
                }
            }

            return fotos;
        }

        public static List<AplicaTraspasoDto> AplicarTraspaso()
        {
            List<AplicaTraspasoDto> aplicaciones = new List<AplicaTraspasoDto>();
            List<AlmacenGasTraspaso> traspasosGas = ObtenerTraspasosNoProcesadas();

            List<AlmacenGasTraspaso> traspasosGasIniciales = traspasosGas.Where(x => x.IdTipoEvento.Equals(TipoEventoEnum.Inicial)).ToList();
            List<AlmacenGasTraspaso> traspasosGasFinales = traspasosGas.Where(x => x.IdTipoEvento.Equals(TipoEventoEnum.Final)).ToList();

            if (traspasosGasIniciales != null && traspasosGasIniciales.Count > 0)
            {
                traspasosGasIniciales.ForEach(x => aplicaciones.Add(AplicarTraspaso(x, traspasosGasFinales)));
                //new AlmacenGasTraspasoDataAccess().Actualizar(aplicaciones);
            }

            return aplicaciones;
        }

        public static AplicaTraspasoDto AplicarTraspaso(AlmacenGasTraspaso TraspasoInicial, List<AlmacenGasTraspaso> TraspasosFinales)
        {
            AplicaTraspasoDto apReDto = new AplicaTraspasoDto()
            {
                TraspasoLecturaInicial = TraspasoInicial,
                TraspasosFinales = TraspasosFinales,
                unidadSalida = AlmacenGasServicio.ObtenerUnidadAlamcenGas(TraspasoInicial, true),
                unidadEntrada = AlmacenGasServicio.ObtenerUnidadAlamcenGas(TraspasoInicial, false),
            };

            apReDto.Empresa = EmpresaServicio.Obtener(apReDto.unidadEntrada);
            apReDto = AplicarTraspaso(apReDto);

            return apReDto;
        }

        public static AplicaTraspasoDto AplicarTraspaso(AplicaTraspasoDto apTrasDto)
        {
            apTrasDto.identidadUS = IdentificarTipoUnidadAlamcenGas(apTrasDto.unidadSalida);
            apTrasDto.identidadUE = IdentificarTipoUnidadAlamcenGas(apTrasDto.unidadEntrada);
            AplicarTraspasoProceso(apTrasDto);

            new AlmacenGasDataAccess().Actualizar(apTrasDto);
            return apTrasDto;
        }

        public static AplicaTraspasoDto AplicarTraspasoProceso(AplicaTraspasoDto apTrasDto)
        {
            apTrasDto.TraspasoLecturaFinal = apTrasDto.TraspasosFinales.FirstOrDefault(x => x.IdCAlmacenGasEntrada.Equals(apTrasDto.TraspasoLecturaInicial.IdCAlmacenGasEntrada));

            if (apTrasDto.TraspasoLecturaFinal == null)
                return new AplicaTraspasoDto();

            decimal LitrosTraspasados = CalcularGasServicio.ObtenerDiferenciaLecturaP5000(apTrasDto.TraspasoLecturaFinal.P5000Salida, apTrasDto.TraspasoLecturaInicial.P5000Salida);
            decimal KilosTraspasados = CalcularGasServicio.ObtenerKilogramosDesdeLitros(LitrosTraspasados, apTrasDto.Empresa.FactorLitrosAKilos);

            apTrasDto = AplicarTraspaso(apTrasDto, LitrosTraspasados, KilosTraspasados);
            return apTrasDto;
        }

        public static AplicaTraspasoDto AplicarTraspaso(AplicaTraspasoDto apTrasDto, decimal LitrosTraspasados, decimal KilosTraspasados)
        {
            apTrasDto.unidadEntrada.CantidadActualLt = CalcularGasServicio.SumarLitros(apTrasDto.unidadEntrada.CantidadActualLt, LitrosTraspasados);
            apTrasDto.unidadEntrada.CantidadActualKg = CalcularGasServicio.SumarKilogramos(apTrasDto.unidadEntrada.CantidadActualKg, KilosTraspasados);            
            apTrasDto.unidadEntrada.PorcentajeActual = CalcularGasServicio.ObtenerPorcentajeDesdeLitros(apTrasDto.unidadEntrada.CapacidadTanqueLt.Value, apTrasDto.unidadEntrada.CantidadActualLt);
            apTrasDto.unidadEntrada.P5000Actual = apTrasDto.TraspasoLecturaFinal.P5000Entrada;

            apTrasDto.unidadSalida.CantidadActualLt = CalcularGasServicio.RestarLitros(apTrasDto.unidadSalida.CantidadActualLt, LitrosTraspasados);
            apTrasDto.unidadSalida.CantidadActualKg = CalcularGasServicio.RestarKilogramos(apTrasDto.unidadSalida.CantidadActualKg, KilosTraspasados);
            apTrasDto.unidadSalida.P5000Actual = apTrasDto.TraspasoLecturaFinal.P5000Salida;

            if (apTrasDto.identidadUS.Equals(identidadUnidadAlmacenGas.EstacionCarburacion))
                apTrasDto.unidadSalida.PorcentajeActual = apTrasDto.TraspasoLecturaInicial.PorcentajeSalida.Value;
            if(apTrasDto.identidadUS.Equals(identidadUnidadAlmacenGas.Pipa))
                apTrasDto.unidadSalida.PorcentajeActual = CalcularGasServicio.ObtenerPorcentajeDesdeLitros(apTrasDto.unidadSalida.CapacidadTanqueLt.Value, apTrasDto.unidadSalida.CantidadActualLt);
            
            apTrasDto.unidadEntrada = AlmacenGasAdapter.FromEntity(apTrasDto.unidadEntrada);
            apTrasDto.unidadSalida = AlmacenGasAdapter.FromEntity(apTrasDto.unidadSalida);

            apTrasDto.TraspasoLecturaInicialFotos = GenerarImagenes(apTrasDto.TraspasoLecturaInicial);
            apTrasDto.TraspasoLecturaInicial.DatosProcesados = true;
            apTrasDto.TraspasoLecturaInicialSinNavProp = AlmacenGasAdapter.FromEntity(apTrasDto.TraspasoLecturaInicial);

            apTrasDto.TraspasoLecturaFinalFotos = GenerarImagenes(apTrasDto.TraspasoLecturaFinal);
            apTrasDto.TraspasoLecturaFinal.DatosProcesados = true;
            apTrasDto.TraspasoLecturaFinalSinNavProp = AlmacenGasAdapter.FromEntity(apTrasDto.TraspasoLecturaFinal);
            return apTrasDto;
        }

        public static List<AlmacenGasTraspasoFoto> GenerarImagenes(AlmacenGasTraspaso Traspaso)
        {
            List<AlmacenGasTraspasoFoto> imagenes = ObtenerImagenes(Traspaso);

            var fotos = new List<AlmacenGasTraspasoFoto>();

            if (imagenes != null && imagenes.Count > 0)
            {
                foreach (var imagen in imagenes)
                {
                    var img = ImagenServicio.ObtenerImagen(imagen);
                    var foto = AlmacenGasAdapter.FromEntity(img);
                    fotos.Add(foto);
                }
            }

            return fotos;
        }

        public static List<AplicaAutoConsumoDto> AplicarAutoConsumo()
        {
            List<AplicaAutoConsumoDto> aplicaciones = new List<AplicaAutoConsumoDto>();
            List<AlmacenGasAutoConsumo> AutoConsumosGas = ObtenerAutoConsumosNoProcesadas();

            List<AlmacenGasAutoConsumo> AutoConsumosGasIniciales = AutoConsumosGas.Where(x => x.IdTipoEvento.Equals(TipoEventoEnum.Inicial)).ToList();
            List<AlmacenGasAutoConsumo> AutoConsumosGasFinales = AutoConsumosGas.Where(x => x.IdTipoEvento.Equals(TipoEventoEnum.Final)).ToList();

            if (AutoConsumosGasIniciales != null && AutoConsumosGasIniciales.Count > 0)
            {
                AutoConsumosGasIniciales.ForEach(x => aplicaciones.Add(AplicarAutoConsumo(x, AutoConsumosGasFinales)));
                //new AlmacenGasAutoConsumoDataAccess().Actualizar(aplicaciones);
            }

            return aplicaciones;
        }

        public static AplicaAutoConsumoDto AplicarAutoConsumo(AlmacenGasAutoConsumo AutoConsumoInicial, List<AlmacenGasAutoConsumo> AutoConsumosFinales)
        {
            AplicaAutoConsumoDto apAuDto = new AplicaAutoConsumoDto()
            {
                AutoConsumoLecturaInicial = AutoConsumoInicial,
                AutoConsumosFinales = AutoConsumosFinales,
                unidadSalida = AlmacenGasServicio.ObtenerUnidadAlamcenGas(AutoConsumoInicial, true),
                unidadEntrada = AlmacenGasServicio.ObtenerUnidadAlamcenGas(AutoConsumoInicial, false),
            };

            apAuDto.Empresa = EmpresaServicio.Obtener(apAuDto.unidadEntrada);
            apAuDto = AplicarAutoConsumo(apAuDto);

            return apAuDto;
        }

        public static AplicaAutoConsumoDto AplicarAutoConsumo(AplicaAutoConsumoDto apAutoDto)
        {
            apAutoDto.identidadUS = IdentificarTipoUnidadAlamcenGas(apAutoDto.unidadSalida);
            apAutoDto.identidadUE = IdentificarTipoUnidadAlamcenGas(apAutoDto.unidadEntrada);
            AplicarAutoConsumoProceso(apAutoDto);

            new AlmacenGasDataAccess().Actualizar(apAutoDto);
            return apAutoDto;
        }

        public static AplicaAutoConsumoDto AplicarAutoConsumoProceso(AplicaAutoConsumoDto apTrasDto)
        {
            apTrasDto.AutoConsumoLecturaFinal = apTrasDto.AutoConsumosFinales.FirstOrDefault(x => x.IdCAlmacenGasEntrada.Equals(apTrasDto.AutoConsumoLecturaInicial.IdCAlmacenGasEntrada));

            if (apTrasDto.AutoConsumoLecturaFinal == null)
                return new AplicaAutoConsumoDto();

            decimal LitrosCarburados = CalcularGasServicio.ObtenerDiferenciaLecturaP5000(apTrasDto.AutoConsumoLecturaFinal.P5000Salida, apTrasDto.AutoConsumoLecturaInicial.P5000Salida);
            decimal KilosCarburados = CalcularGasServicio.ObtenerKilogramosDesdeLitros(LitrosCarburados, apTrasDto.Empresa.FactorLitrosAKilos);
            
            apTrasDto = AplicarAutoConsumo(apTrasDto, LitrosCarburados, KilosCarburados);
            return apTrasDto;
        }

        public static AplicaAutoConsumoDto AplicarAutoConsumo(AplicaAutoConsumoDto apTrasDto, decimal LitrosCarburados, decimal KilosCarburados)
        {
            apTrasDto.unidadSalida.CantidadActualLt = CalcularGasServicio.RestarLitros(apTrasDto.unidadSalida.CantidadActualLt, LitrosCarburados);
            apTrasDto.unidadSalida.CantidadActualKg = CalcularGasServicio.RestarKilogramos(apTrasDto.unidadSalida.CantidadActualKg, KilosCarburados);
            apTrasDto.unidadSalida.PorcentajeActual = CalcularGasServicio.ObtenerPorcentajeDesdeLitros(apTrasDto.unidadSalida.CapacidadTanqueLt.Value, apTrasDto.unidadSalida.CantidadActualLt);
            apTrasDto.unidadSalida.P5000Actual = apTrasDto.AutoConsumoLecturaFinal.P5000Salida;
                       
            apTrasDto.unidadEntrada = apTrasDto.unidadEntrada.Equals(apTrasDto.unidadSalida)
                ? null
                : AlmacenGasAdapter.FromEntity(apTrasDto.unidadEntrada);
            apTrasDto.unidadSalida = AlmacenGasAdapter.FromEntity(apTrasDto.unidadSalida);

            apTrasDto.AutoConsumoLecturaInicialFotos = GenerarImagenes(apTrasDto.AutoConsumoLecturaInicial);
            apTrasDto.AutoConsumoLecturaInicial.DatosProcesados = true;
            apTrasDto.AutoConsumoLecturaInicialSinNavProp = AlmacenGasAdapter.FromEntity(apTrasDto.AutoConsumoLecturaInicial);

            apTrasDto.AutoConsumoLecturaFinalFotos = GenerarImagenes(apTrasDto.AutoConsumoLecturaFinal);
            apTrasDto.AutoConsumoLecturaFinal.DatosProcesados = true;
            apTrasDto.AutoConsumoLecturaFinalSinNavProp = AlmacenGasAdapter.FromEntity(apTrasDto.AutoConsumoLecturaFinal);
            return apTrasDto;
        }

        public static List<AlmacenGasAutoConsumoFoto> GenerarImagenes(AlmacenGasAutoConsumo AutoConsumo)
        {
            List<AlmacenGasAutoConsumoFoto> imagenes = ObtenerImagenes(AutoConsumo);

            var fotos = new List<AlmacenGasAutoConsumoFoto>();

            if (imagenes != null && imagenes.Count > 0)
            {
                foreach (var imagen in imagenes)
                {
                    var img = ImagenServicio.ObtenerImagen(imagen);
                    var foto = AlmacenGasAdapter.FromEntity(img);
                    fotos.Add(foto);
                }
            }

            return fotos;
        }

        public static List<AplicaCalibracionDto> AplicarCalibracion()
        {
            List<AplicaCalibracionDto> aplicaciones = new List<AplicaCalibracionDto>();
            List<AlmacenGasCalibracion> CalibracionsGas = ObtenerCalibracionesNoProcesadas();

            List<AlmacenGasCalibracion> CalibracionsGasIniciales = CalibracionsGas.Where(x => x.IdTipoEvento.Equals(TipoEventoEnum.Inicial)).ToList();
            List<AlmacenGasCalibracion> CalibracionsGasFinales = CalibracionsGas.Where(x => x.IdTipoEvento.Equals(TipoEventoEnum.Final)).ToList();

            if (CalibracionsGasIniciales != null && CalibracionsGasIniciales.Count > 0)
            {
                CalibracionsGasIniciales.ForEach(x => aplicaciones.Add(AplicarCalibracion(x, CalibracionsGasFinales)));
                //new AlmacenGasCalibracionDataAccess().Actualizar(aplicaciones);
            }

            return aplicaciones;
        }

        public static AplicaCalibracionDto AplicarCalibracion(AlmacenGasCalibracion CalibracionInicial, List<AlmacenGasCalibracion> CalibracionsFinales)
        {
            AplicaCalibracionDto apCaliDto = new AplicaCalibracionDto()
            {
                CalibracionLecturaInicial = CalibracionInicial,
                CalibracionsFinales = CalibracionsFinales,
                unidadAlmacenGas = AlmacenGasServicio.ObtenerUnidadAlamcenGas(CalibracionInicial)
            };

            apCaliDto.Empresa = EmpresaServicio.Obtener(apCaliDto.unidadAlmacenGas);
            apCaliDto = AplicarCalibracion(apCaliDto);

            return apCaliDto;
        }

        public static AplicaCalibracionDto AplicarCalibracion(AplicaCalibracionDto apCaliDto)
        {
            apCaliDto.identidadUA = IdentificarTipoUnidadAlamcenGas(apCaliDto.unidadAlmacenGas);
            AplicarCalibracionProceso(apCaliDto);

            new AlmacenGasDataAccess().Actualizar(apCaliDto);
            return apCaliDto;
        }

        public static AplicaCalibracionDto AplicarCalibracionProceso(AplicaCalibracionDto apCaliDto)
        {
            apCaliDto.CalibracionLecturaFinal = apCaliDto.CalibracionsFinales.FirstOrDefault(x => x.IdCAlmacenGas.Equals(apCaliDto.CalibracionLecturaInicial.IdCAlmacenGas));

            if (apCaliDto.CalibracionLecturaFinal == null)
                return new AplicaCalibracionDto();

            if (apCaliDto.identidadUA.Equals(identidadUnidadAlmacenGas.Camioneta))
            {
                return AplicarCalibracionCamioneta(apCaliDto);
            }

            decimal Litros = CalcularGasServicio.ObtenerDiferenciaLecturaP5000(apCaliDto.CalibracionLecturaFinal.P5000.Value, apCaliDto.CalibracionLecturaInicial.P5000.Value);
            decimal Kilos = CalcularGasServicio.ObtenerKilogramosDesdeLitros(Litros, apCaliDto.Empresa.FactorLitrosAKilos);

            apCaliDto = AplicarCalibracion(apCaliDto, Litros, Kilos);
            return apCaliDto;
        }

        public static AplicaCalibracionDto AplicarCalibracion(AplicaCalibracionDto apCaliDto, decimal Litros, decimal Kilos)
        {
            if (apCaliDto.CalibracionLecturaFinal.IdDestinoCalibracion.Equals(CalibracionDestinoEnum.TanquePortatil))
            {
                apCaliDto.unidadAlmacenGasPrincipal = ObtenerAlmacenGeneral(apCaliDto.Empresa).FirstOrDefault();
                apCaliDto.unidadAlmacenGasPrincipal.CantidadActualLt = CalcularGasServicio.SumarLitros(apCaliDto.unidadAlmacenGasPrincipal.CantidadActualLt, Litros);
                apCaliDto.unidadAlmacenGasPrincipal.CantidadActualKg = CalcularGasServicio.SumarKilogramos(apCaliDto.unidadAlmacenGasPrincipal.CantidadActualKg, Kilos);

                apCaliDto.unidadAlmacenGas.CantidadActualLt = CalcularGasServicio.RestarLitros(apCaliDto.unidadAlmacenGas.CantidadActualLt, Litros);
                apCaliDto.unidadAlmacenGas.CantidadActualKg = CalcularGasServicio.RestarKilogramos(apCaliDto.unidadAlmacenGas.CantidadActualKg, Kilos);
                apCaliDto.unidadAlmacenGas.PorcentajeActual = apCaliDto.CalibracionLecturaFinal.Porcentaje;
            }

            apCaliDto.unidadAlmacenGas.P5000Actual = apCaliDto.CalibracionLecturaFinal.P5000;
            apCaliDto.unidadAlmacenGas.PorcentajeCalibracionPlaneada = apCaliDto.CalibracionLecturaFinal.PorcentajeCalibracion.Value;

            apCaliDto.unidadAlmacenGas = AlmacenGasAdapter.FromEntity(apCaliDto.unidadAlmacenGas);

            apCaliDto.CalibracionLecturaInicialFotos = GenerarImagenes(apCaliDto.CalibracionLecturaInicial);
            apCaliDto.CalibracionLecturaInicial.DatosProcesados = true;
            apCaliDto.CalibracionLecturaInicialSinNavProp = AlmacenGasAdapter.FromEntity(apCaliDto.CalibracionLecturaInicial);

            apCaliDto.CalibracionLecturaFinalFotos = GenerarImagenes(apCaliDto.CalibracionLecturaFinal);
            apCaliDto.CalibracionLecturaFinal.DatosProcesados = true;
            apCaliDto.CalibracionLecturaFinalSinNavProp = AlmacenGasAdapter.FromEntity(apCaliDto.CalibracionLecturaFinal);
            return apCaliDto;
        }

        public static AplicaCalibracionDto AplicarCalibracionCamioneta(AplicaCalibracionDto apCaliDto)
        {
            apCaliDto.unidadAlmacenGas.PorcentajeCalibracionPlaneada = apCaliDto.CalibracionLecturaInicial.PorcentajeCalibracion.Value;
            apCaliDto.unidadAlmacenGas = AlmacenGasAdapter.FromEntity(apCaliDto.unidadAlmacenGas);

            apCaliDto.CalibracionLecturaInicial.DatosProcesados = true;
            apCaliDto.CalibracionLecturaInicialSinNavProp = AlmacenGasAdapter.FromEntity(apCaliDto.CalibracionLecturaInicial);

            return apCaliDto;
        }

        public static List<AlmacenGasCalibracionFoto> GenerarImagenes(AlmacenGasCalibracion Calibracion)
        {
            List<AlmacenGasCalibracionFoto> imagenes = ObtenerImagenes(Calibracion);

            var fotos = new List<AlmacenGasCalibracionFoto>();

            if (imagenes != null && imagenes.Count > 0)
            {
                foreach (var imagen in imagenes)
                {
                    var img = ImagenServicio.ObtenerImagen(imagen);
                    var foto = AlmacenGasAdapter.FromEntity(img);
                    fotos.Add(foto);
                }
            }

            return fotos;
        }

        public static List<AplicaTomaLecturaDto> AplicarTomaLectura()
        {
            List<AplicaTomaLecturaDto> aplicaciones = new List<AplicaTomaLecturaDto>();
            List<AlmacenGasTomaLectura> TomaLecturasGas = ObtenerLecturasNoProcesadas();

            List<AlmacenGasTomaLectura> TomaLecturasGasIniciales = TomaLecturasGas.Where(x => x.IdTipoEvento.Equals(TipoEventoEnum.Inicial)).ToList();
            List<AlmacenGasTomaLectura> TomaLecturasGasFinales = TomaLecturasGas.Where(x => x.IdTipoEvento.Equals(TipoEventoEnum.Final)).ToList();

            if (TomaLecturasGasIniciales != null && TomaLecturasGasIniciales.Count > 0)
            {
                TomaLecturasGasIniciales.ForEach(x => aplicaciones.Add(AplicarTomaLectura(x, TomaLecturasGasFinales)));
                //new AlmacenGasTomaLecturaDataAccess().Actualizar(aplicaciones);
            }

            return aplicaciones;
        }

        public static AplicaTomaLecturaDto AplicarTomaLectura(AlmacenGasTomaLectura TomaLecturaInicial, List<AlmacenGasTomaLectura> TomaLecturasFinales)
        {
            AplicaTomaLecturaDto apCaliDto = new AplicaTomaLecturaDto()
            {
                TomaLecturaLecturaInicial = TomaLecturaInicial,
                TomaLecturasFinales = TomaLecturasFinales,
                unidadAlmacenGas = AlmacenGasServicio.ObtenerUnidadAlamcenGas(TomaLecturaInicial)
            };

            apCaliDto.Empresa = EmpresaServicio.Obtener(apCaliDto.unidadAlmacenGas);
            apCaliDto = AplicarTomaLectura(apCaliDto);

            return apCaliDto;
        }

        public static AplicaTomaLecturaDto AplicarTomaLectura(AplicaTomaLecturaDto apLectDto)
        {
            apLectDto.identidadUA = IdentificarTipoUnidadAlamcenGas(apLectDto.unidadAlmacenGas);
            AplicarTomaLecturaProceso(apLectDto);

            new AlmacenGasDataAccess().Actualizar(apLectDto);
            return apLectDto;
        }

        public static AplicaTomaLecturaDto AplicarTomaLecturaProceso(AplicaTomaLecturaDto apLectDto)
        {
            apLectDto.TomaLecturaLecturaFinal = apLectDto.TomaLecturasFinales.FirstOrDefault(x => x.IdCAlmacenGas.Equals(apLectDto.TomaLecturaLecturaInicial.IdCAlmacenGas));

            if (apLectDto.TomaLecturaLecturaFinal == null)
                return new AplicaTomaLecturaDto();

            switch (apLectDto.identidadUA)
            {
                case identidadUnidadAlmacenGas.Pipa: apLectDto = AplicarTomaLecturaPipa(apLectDto); break;
                case identidadUnidadAlmacenGas.EstacionCarburacion: break;
                case identidadUnidadAlmacenGas.Camioneta: break;
                case identidadUnidadAlmacenGas.AlmacenAlterno: break;
                default: break;
            }
            return apLectDto;
        }

        public static AplicaTomaLecturaDto AplicarTomaLecturaPipa(AplicaTomaLecturaDto apLectDto)
        {
            //decimal LitrosSalientes = CalcularGasServicio.ObtenerDiferenciaLecturaP5000(apLectDto.TomaLecturaLecturaFinal.P5000.Value, apLectDto.TomaLecturaLecturaInicial.P5000.Value);
            //decimal KilosSalientes = CalcularGasServicio.ObtenerKilogramosDesdeLitros(LitrosSalientes, apLectDto.Empresa.FactorLitrosAKilos);



            ////Hizo autoconsumos como unidad de salida
            ////Hizo calibración
            ////Hizo recargas como unidad de salida
            ////Hizo traspasos como unidad de salida

            //if (apLectDto.TomaLecturaLecturaFinal.IdDestinoTomaLectura.Equals(TomaLecturaDestinoEnum.TanquePortatil))
            //{
            //    apLectDto.unidadAlmacenGasPrincipal = ObtenerAlmacenGeneral(apLectDto.Empresa).FirstOrDefault();
            //    apLectDto.unidadAlmacenGasPrincipal.CantidadActualLt = CalcularGasServicio.SumarLitros(apLectDto.unidadAlmacenGasPrincipal.CantidadActualLt, Litros);
            //    apLectDto.unidadAlmacenGasPrincipal.CantidadActualKg = CalcularGasServicio.SumarKilogramos(apLectDto.unidadAlmacenGasPrincipal.CantidadActualKg, KilosSalientes);

            //    apLectDto.unidadAlmacenGas.CantidadActualLt = CalcularGasServicio.RestarLitros(apLectDto.unidadAlmacenGas.CantidadActualLt, Litros);
            //    apLectDto.unidadAlmacenGas.CantidadActualKg = CalcularGasServicio.RestarKilogramos(apLectDto.unidadAlmacenGas.CantidadActualKg, KilosSalientes);
            //    apLectDto.unidadAlmacenGas.PorcentajeActual = apLectDto.TomaLecturaLecturaFinal.Porcentaje;
            //}

            //apLectDto.unidadAlmacenGas.P5000Actual = apLectDto.TomaLecturaLecturaFinal.P5000;
            //apLectDto.unidadAlmacenGas.PorcentajeTomaLecturaPlaneada = apLectDto.TomaLecturaLecturaFinal.PorcentajeTomaLectura.Value;

            //apLectDto.unidadAlmacenGas = AlmacenGasAdapter.FromEntity(apLectDto.unidadAlmacenGas);

            //apLectDto.TomaLecturaLecturaInicialFotos = GenerarImagenes(apLectDto.TomaLecturaLecturaInicial);
            //apLectDto.TomaLecturaLecturaInicial.DatosProcesados = true;
            //apLectDto.TomaLecturaLecturaInicialSinNavProp = AlmacenGasAdapter.FromEntity(apLectDto.TomaLecturaLecturaInicial);

            //apLectDto.TomaLecturaLecturaFinalFotos = GenerarImagenes(apLectDto.TomaLecturaLecturaFinal);
            //apLectDto.TomaLecturaLecturaFinal.DatosProcesados = true;
            //apLectDto.TomaLecturaLecturaFinalSinNavProp = AlmacenGasAdapter.FromEntity(apLectDto.TomaLecturaLecturaFinal);
            return apLectDto;
        }

        public static AplicaTomaLecturaDto AplicarTomaLecturaCamioneta(AplicaTomaLecturaDto apLectDto)
        {
            //apLectDto.unidadAlmacenGas.PorcentajeTomaLecturaPlaneada = apLectDto.TomaLecturaLecturaInicial.PorcentajeTomaLectura.Value;
            //apLectDto.unidadAlmacenGas = AlmacenGasAdapter.FromEntity(apLectDto.unidadAlmacenGas);

            //apLectDto.TomaLecturaLecturaInicial.DatosProcesados = true;
            //apLectDto.TomaLecturaLecturaInicialSinNavProp = AlmacenGasAdapter.FromEntity(apLectDto.TomaLecturaLecturaInicial);

            return apLectDto;
        }

        public static List<AlmacenGasTomaLecturaFoto> GenerarImagenes(AlmacenGasTomaLectura TomaLectura)
        {
            //List<AlmacenGasTomaLecturaFoto> imagenes = ObtenerImagenes(TomaLectura);

            //var fotos = new List<AlmacenGasTomaLecturaFoto>();

            //if (imagenes != null && imagenes.Count > 0)
            //{
            //    foreach (var imagen in imagenes)
            //    {
            //        var img = ImagenServicio.ObtenerImagen(imagen);
            //        var foto = AlmacenGasAdapter.FromEntity(img);
            //        fotos.Add(foto);
            //    }
            //}

            return null;
            //return fotos;
        }

        //public static AlmacenGasMovimiento AplicarInventarioMovimiento(AplicaInventarioMovimientoDto movDto)
        //{
        //    return null;
        //}

        public static AlmacenGasAutoConsumo ObtenerAutoconsumo(string claveOperacion)
        {
            return new AlmacenGasDataAccess().BuscarAutoconsumoClaveOperacion(claveOperacion);
        }

        public static AlmacenGasCalibracion ObtenerCalibracion(string claveOperacion)
        {
            return new AlmacenGasDataAccess().BuscarCalibracion(claveOperacion);
        }
        public static RespuestaDto InsertarCalibracion(AlmacenGasCalibracion adapter)
        {
            return new AlmacenGasDataAccess().Insertar(adapter);
        }


        //public static object CrearMovimiento(AplicaRecargaDto apReDto)
        //{
        //    AlmacenGasMovimiento ultimoMovimiento = ObtenerUltimoMovimientoEnInventario(empresa.IdEmpresa, almacenGasTotal.IdAlmacenGas, descarga.FechaFinDescarga.Value);
        //    RemanenteDto remaDto = RemanenteServicio.ObtenerRemanente(descarga, almacenGasTotal.IdAlmacenGas, empresa.IdEmpresa);

        //    var invAnterior = new InventarioAnteriorDto
        //    {
        //        RemanenteKg = kilogramosRemanentes,
        //        RemanenteLt = litrosRemanentes,
        //        RemanenteAcumuladoDiaKg = CalcularGasServicio.SumarKilogramos(remaDto.RemanenteAcumuladoDiaKg, kilogramosRemanentes),
        //        RemanenteAcumuladoDiaLt = CalcularGasServicio.SumarLitros(remaDto.RemanenteAcumuladoDiaLt, litrosRemanentes),
        //        RemanenteAcumuladoMesKg = CalcularGasServicio.SumarKilogramos(remaDto.RemanenteAcumuladoMesKg, kilogramosRemanentes),
        //        RemanenteAcumuladoMesLt = CalcularGasServicio.SumarLitros(remaDto.RemanenteAcumuladoMesLt, litrosRemanentes),
        //        RemanenteAcumuladoAnioKg = CalcularGasServicio.SumarKilogramos(remaDto.RemanenteAcumuladoAnioKg, kilogramosRemanentes),
        //        RemanenteAcumuladoAnioLt = CalcularGasServicio.SumarLitros(remaDto.RemanenteAcumuladoAnioLt, litrosRemanentes),
        //        EntradaKg = kilogramosRealesTractor,
        //        EntradaLt = litrosRealesTractor,
        //        SalidaKg = 0,
        //        SalidaLt = 0,
        //        CantidadAnteriorKg = unidadEntradaCantidadKg,
        //        CantidadAnteriorLt = unidadEntradaCantidadLt,
        //        CantidadAcumuladaDiaKg = CalcularGasServicio.SumarKilogramos(ultimoMovimiento.CantidadAcumuladaDiaKg.Value, kilogramosRealesTractor),
        //        CantidadAcumuladaDiaLt = CalcularGasServicio.SumarLitros(ultimoMovimiento.CantidadAcumuladaDiaLt.Value, litrosRealesTractor),
        //        CantidadAcumuladaMesKg = CalcularGasServicio.SumarKilogramos(ultimoMovimiento.CantidadAcumuladaMesKg.Value, kilogramosRealesTractor),
        //        CantidadAcumuladaMesLt = CalcularGasServicio.SumarLitros(ultimoMovimiento.CantidadAcumuladaMesLt.Value, litrosRealesTractor),
        //        CantidadAcumuladaAnioKg = CalcularGasServicio.SumarKilogramos(ultimoMovimiento.CantidadAcumuladaAnioKg.Value, kilogramosRealesTractor),
        //        CantidadAcumuladaAnioLt = CalcularGasServicio.SumarLitros(ultimoMovimiento.CantidadAcumuladaAnioLt.Value, litrosRealesTractor),
        //        PorcentajeAnterior = unidadEntradaPorcentaje,
        //        P5000Anterior = null,
        //        CantidadAnteriorTotalKg = almacenTotalCantidadActualKg,
        //        CantidadAnteriorTotalLt = almacenTotalCantidadActualLt,
        //        PorcentajeAnteriorTotal = almacenTotalPorcent,
        //        CantidadAnteriorGeneralKg = almacenGeneralCantidadActualKg,
        //        CantidadAnteriorGeneralLt = almacenGeneralCantidadActualLt,
        //        PorcentajeAnteriorGeneral = almacenGeneralPorcent,
        //    };
        //}
        public static AlmacenGasTraspaso ObtenerTraspaso(string claveOperacion)
        {
            return new AlmacenGasDataAccess().BuscarTraspaso(claveOperacion);
        }

        public static RespuestaDto InsertarTraspaso(AlmacenGasTraspaso dto)
        {
            return new AlmacenGasDataAccess().Insertar(dto);
        }

        public static VentaCorteAnticipoEC ObtenerAnticipo(string claveOperacion)
        {
            return new PuntoVentaDataAccess().BuscarAnticipo(claveOperacion);
        }

        public static List<VentaCorteAnticipoEC> ObetnerAnticipos(short idEmpresa)
        {
            return new PuntoVentaDataAccess().Anticipos( idEmpresa);
        }

        public static RespuestaDto InsertarAnticipo(VentaCorteAnticipoEC adapter)
        {
            return new PuntoVentaDataAccess().InsertarCorte(adapter);
        }

        public static VentaCorteAnticipoEC ObtenerCorte(string claveOperacion)
        {
            return new PuntoVentaDataAccess().BuscarCorte(claveOperacion);
        }

        public static List<VentaCorteAnticipoEC> ObtenerCortes(short idEmpresa)
        {
            return new PuntoVentaDataAccess().Cortes(idEmpresa);
        }

        public static RespuestaDto InsertCorte(VentaCorteAnticipoEC corte)
        {
            return new PuntoVentaDataAccess().InsertarCorte(corte);
        }
    }
}
