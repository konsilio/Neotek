using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.Servicios.Catalogos;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using Application.MainModule.DTOs.Mobile;
using Sagas.MainModule.ObjetosValor.Enum;
using Application.MainModule.DTOs.Almacen;
using Application.MainModule.AdaptadoresDTO.Almacenes;
using Application.MainModule.Servicios.Compras;
using Utilities.MainModule;
using Sagas.MainModule.ObjetosValor.Constantes;
using Application.MainModule.Servicios.Seguridad;
using Application.MainModule.AdaptadoresDTO.Mobile;
using Application.MainModule.DTOs.Ventas;
using Application.MainModule.DTOs;
using Exceptions.MainModule.Validaciones;
using Application.MainModule.AdaptadoresDTO.Ventas;
using Exceptions.MainModule;
using Application.MainModule.AdaptadoresDTO.Catalogo;

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
        public static RespuestaDto RegistraAlmacen(EquipoTransporteDTO ec)
        {
            AlmacenGas almacen = new AlmacenGas()
            {
                IdEmpresa = TokenServicio.ObtenerIdEmpresa(),
                CantidadActualKg = 0,
                CantidadActualLt = 0,
                CapacidadTotalKg = ec.CapacidadKg,
                CapacidadTotalLt = ec.CapacidadLts,
                CapacidadGeneralKg = 0,
                CapacidadGeneralLt = 0,
                CantidadActualGeneralKg = 0,
                CantidadActualGeneralLt = 0,
                PorcentajeActualGeneral = 0,
                PorcentajeActual = 0,
                Activo = true,
                FechaRegistro = DateTime.Now,
            };
            var newAlmacen = new AlmacenGasDataAccess().Insertar(almacen);
            if (!newAlmacen.Exito) return newAlmacen;
            UnidadAlmacenGas unidad = new UnidadAlmacenGas
            {
                IdAlmacenGas = Convert.ToInt16(newAlmacen.Id),
                IdCamioneta = ec.IdCamioneta,
                IdEstacionCarburacion = null,
                IdPipa = ec.IdPipa,
                IdEmpresa = TokenServicio.ObtenerIdEmpresa(),
                IdTipoAlmacen = TipoUnidadAlmacenGasEnum.Movil,
                IdTipoMedidor = ec.IdCamioneta != null ? TipoMedidorGasEnum.Magnetel : ec.IdTipoMedidor,
                CantidadActualKg = 0,
                CantidadActualLt = 0,
                CapacidadTanqueKg = ec.CapacidadKg,
                CapacidadTanqueLt = ec.CapacidadLts,
                EsAlterno = false,
                EsGeneral = true,
                PorcentajeCalibracionPlaneada = 0,
                Numero = string.Format(ec.IdCamioneta != null ? AlmacenGasConst.NombreCaioneta : string.Empty + ec.IdPipa != null ? AlmacenGasConst.NombrePipa : string.Empty, ec.Descripcion),
                P5000Actual = null,
                PorcentajeActual = 0,
                Activo = true,
                FechaRegistro = DateTime.Now,
            };
            return new AlmacenGasDataAccess().Insertar(unidad);
        }
        public static RespuestaDto ActualizarCalibracionAlmacen(UnidadAlmacenGas almacenActualizar)
        {
            return new AlmacenDataAccess().ActualizaAlmacen(almacenActualizar);
        }
        public static CamionetaCilindro BuscarCamionetaCilindro(int idCamioneta, int idCilindro, short idEmpresa)
        {
            return new AlmacenGasDataAccess().BuscarCamionetaCilindro(idCamioneta, idCilindro, idEmpresa);
        }
        /// <summary>
        /// ActualizaCilindro
        /// Permite actualizar los cilindros de la camioneta 
        /// </summary>
        /// <param name="camionetaCilindro">Listado de cilindros a actualizar</param>
        /// <returns>RespuestaDTO con el resultado de la actualización</returns>
        public static RespuestaDto ActualizaCilindro(CamionetaCilindro camionetaCilindro)
        {
            return new AlmacenDataAccess().Actualizar(camionetaCilindro);
        }
        public static List<AlmacenGasRecarga> ObtenerRecargaInicial(short IdCAlmacenGasEntrada)
        {
            return new AlmacenGasDataAccess().ObtenerRecargaInicial(IdCAlmacenGasEntrada);
        }
        public static RespuestaDto InsertarAutoconsumo(AlmacenGasAutoConsumo adapter)
        {
            return new AlmacenGasDataAccess().Insertar(adapter);
        }
        public static AlmacenGas ObtenerAlmacenGasTotal(short idEmpresa)
        {
            return new AlmacenGasDataAccess().BuscarPorEmpresa(idEmpresa);
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
        public static RespuestaDto ActualizarDescargaGas(AlmacenGasDescarga alm, List<AlmacenGasDescargaFoto> fotos, List<OrdenCompra> ocs)
        {
            return new AlmacenGasDescargaDataAccess().Actualizar(alm, fotos, ocs);
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
        public static List<AlmacenGasTomaLectura> ObtenerLecturas(short idCAlmacenGas, DateTime fecha)
        {
            return new AlmacenGasDataAccess().BuscarLecturas(idCAlmacenGas, fecha);
        }
        public static List<AlmacenGasTomaLecturaCilindro> ObtenerLecturasCamioneta(short idCAlmacenGas)
        {
            return new AlmacenGasDataAccess().BuscarLecturasCamioneta(idCAlmacenGas);
        }
        public static List<AlmacenGasTomaLectura> ObtenerLecturaIncialdelMes(short idCAlmacenGas, int mes, int anio)
        {
            return new AlmacenGasDataAccess().BuscarLecturas(idCAlmacenGas, mes, anio);
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
        /// <summary>
        /// Permite crear un objeto de tipo Reporte del día en 
        /// caso de que este ya este registrado 
        /// </summary>
        /// <param name="resp">Entidad de tipo RporteDia</param>
        /// <param name="almacen">Unidad Almacen gas que se reuiqere el reporte </param>
        /// <returns></returns>
        public static ReporteDiaDTO ReporteDiaExistente(ReporteDelDia resp, UnidadAlmacenGas almacen)
        {
            ReporteDiaDTO reporte = null;
            var lectInicial = BuscarLecturaPorFecha(almacen.IdCAlmacenGas, TipoEventoEnum.Inicial, resp.FechaReporte);
            var lectFinal = BuscarLecturaPorFecha(almacen.IdCAlmacenGas, TipoEventoEnum.Final, resp.FechaReporte);
            var precioVenta = PrecioVentaGasServicio.ObtenerPrecioVigente(TokenServicio.ObtenerIdEmpresa());

            if (almacen.IdCamioneta > 0 && almacen.IdCamioneta != null)
            {
                var cilindrosInicial = lectInicial.Cilindros;
                var cilindrosFinal = lectFinal.Cilindros;
                var autoConsumo = new AlmacenDataAccess().BuscarAutoconsumo(almacen, resp.FechaReporte);
                var puntoVenta = PuntoVentaServicio.Obtener(almacen);
                var ventasContado = PuntoVentaServicio.ObtenerVentasContado(puntoVenta.IdPuntoVenta, resp.FechaReporte);
                var ventasCredito = PuntoVentaServicio.ObtenerVentasCredito(puntoVenta.IdPuntoVenta, resp.FechaReporte);

                var ventas = new PuntoVentaDataAccess().ObtenerVentas(puntoVenta.IdPuntoVenta, resp.FechaReporte);

                //decimal totalVentaCilindros = 0, totalVentaGas = 0, KiliosVenta = 0, LitrosVenta = 0, totalOtros = 0;
                List<OtrasVentasDto> otrasVentas = new List<OtrasVentasDto>();
                decimal totalCarburacion = 0;
                if (autoConsumo != null)
                    totalCarburacion = (autoConsumo.UnidadEntrada.CapacidadTanqueLt ?? 0 / 100) * lectFinal.Porcentaje ?? 0;
                reporte = ReporteAdapter.ToDtoCamioneta(resp, almacen, cilindrosInicial, cilindrosFinal, lectInicial, lectFinal, otrasVentas, ventasContado, ventasCredito);
                reporte.Carburacion = totalCarburacion;
                reporte.OtrasVentasTotal = ventas.Sum(tv => tv.VentaPuntoDeVentaDetalle.Where(vd => !vd.IdProducto.Equals(precioVenta.IdProducto)
                                                                                                        && !vd.IdProductoLinea.Equals(precioVenta.IdProductoLinea)
                                                                                                        && !vd.IdCategoria.Equals(precioVenta.IdCategoria)).Sum(x => x.Subtotal));
                // reporte.Precio = precioVenta.PrecioSalidaKg ?? 0; //CalculosGenerales.Promediar(ventasContado.Sum(x => x.VentaPuntoDeVentaDetalle.Sum(y => y.PrecioUnitarioProducto) ?? 0), ventasContado.Count);
                reporte.KilosDeVenta = ventas.Sum(tv => tv.VentaPuntoDeVentaDetalle.Where(vd => vd.IdProducto.Equals(precioVenta.IdProducto)
                                                      && vd.IdCategoria.Equals(precioVenta.IdCategoria)).Sum(x => x.CantidadKg ?? 0));
            }
            if (almacen.IdEstacionCarburacion != null)
                reporte = ReporteAdapter.ToDtoEstacion(resp, almacen, lectInicial, lectFinal);
            if (almacen.IdPipa != null)
                reporte = ReporteAdapter.ToDtoPipa(resp, almacen, lectInicial, lectFinal);
            reporte.Precio = precioVenta.PrecioSalidaKg ?? 0;

            return reporte;
        }
        /// <summary>
        /// Permite realizar la busqueda de un reporte del día en caso de existir con 
        /// los parametros que se envian 
        /// </summary>
        /// <param name="fecha">Fecha de busqueda</param>
        /// <param name="idCAlmacenGas">Id de CAlmacenGas a buscar</param>
        /// <param name="idEmpresa">id de la empresa a la que pertenece el almacen</param>
        /// <returns>Entidad de tipo ReporteDia con los datos encontrados</returns>
        public static ReporteDelDia BuscarReporteDia(DateTime fecha, short idCAlmacenGas, short idEmpresa)
        {
            return new AlmacenGasDataAccess().BuscarReporte(fecha, idCAlmacenGas, idEmpresa);
        }
        public static List<UnidadAlmacenGas> ObtenerAlmacenGeneral(short idEmpresa, bool incluyeAlterno = false)
        {
            return new AlmacenGasDataAccess().BuscarTodos(idEmpresa, true, incluyeAlterno);
        }
        public static List<UnidadAlmacenGas> ObtenerAlmacenGeneral(Empresa empresa, bool incluyeAlterno = false)
        {
            if (empresa.UnidadesAlmacenGas != null && empresa.UnidadesAlmacenGas.Count > 0)
                return empresa.UnidadesAlmacenGas.ToList();

            return ObtenerAlmacenGeneral(empresa.IdEmpresa, incluyeAlterno);
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
        public static List<AlmacenGasAutoConsumo> ObtenerAutoConsumos(DateTime fi)
        {
            return new AlmacenGasDataAccess().BuscarTodosAutoConsumos(fi);
        }
        public static List<AlmacenGasCalibracion> ObtenerCalibracionesNoProcesadas()
        {
            return new AlmacenGasDataAccess().BuscarTodasCalibracionesNoProcesadas();
        }
        public static List<AlmacenGasTomaLectura> ObtenerLecturasNoProcesadas()
        {
            return new AlmacenGasDataAccess().BuscarTodasLecturasNoProcesadas();
        }
        public static List<AlmacenGasTomaLectura> ObtenerLecturasNoProcesadas(byte idTipoEvento)
        {
            return new AlmacenGasDataAccess().BuscarTodasLecturasNoProcesadas(idTipoEvento);
        }
        public static List<UnidadAlmacenGas> ObtenerPuntosVenta(short idEmpresa)
        {
            return new AlmacenGasDataAccess().BuscarTodosPuntosVenta(idEmpresa);
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
                        if (final == false && uniAlm.TomasLectura != null)
                        {
                            var lecturas = uniAlm.TomasLectura.Count(x => x.IdTipoEvento.Equals(TipoEventoEnum.Final));
                            if (lecturas > 0)
                                return uniAlm.TomasLectura.Last(x => x.IdTipoEvento.Equals(TipoEventoEnum.Final));
                            else
                                return uniAlm.TomasLectura.Last();
                        }
                        else
                        {
                            if (!final)
                            {
                                var ultimalecturaInicial = uniAlm.TomasLectura.Last(x => x.IdTipoEvento.Equals(TipoEventoEnum.Final));
                                if (ultimalecturaInicial != null)
                                    return ultimalecturaInicial;
                                else
                                    return uniAlm.TomasLectura.Last();
                            }
                            else
                            {
                                var lecFinales = uniAlm.TomasLectura.ToList().Find(x => x.IdTipoEvento.Equals(TipoEventoEnum.Inicial));
                                if (lecFinales != null)
                                {
                                    var ultimalecturaFinal = uniAlm.TomasLectura.Last(x => x.IdTipoEvento.Equals(TipoEventoEnum.Inicial));
                                    return ultimalecturaFinal;
                                }
                                else
                                    return uniAlm.TomasLectura.Last();
                            }
                        }
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
        public static UnidadAlmacenGas ObtenerPorPipa(int id)
        {
            return new AlmacenGasDataAccess().BuscarAlmacenPorPipa(id);
        }
        public static UnidadAlmacenGas ObtenerPorCamioneta(int id)
        {
            return new AlmacenGasDataAccess().BuscarAlmacenPorCamioneta(id);
        }
        public static UnidadAlmacenGas ObtenerUnidadAlamcenGasActualizaAlterno(AlmacenGasDescarga descarga, Empresa empresa)
        {
            if (descarga.TanquePrestado ?? false)
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
        public static UnidadAlmacenGas ObtenerUnidadAlmacenGas(AlmacenGasTomaLectura tomaLectura)
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
        public static RespuestaDto ActualizaCilindroCamioneta(CamionetaCilindro cilindro)
        {
            return new AlmacenDataAccess().Actualizar(cilindro);
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
        public static AlmacenGasDescarga ObtenerDescargaPorIdrequisicion(int IdReq)
        {
            return new AlmacenGasDescargaDataAccess().BuscarAlmacenDescargaPorRequisicion(IdReq);
        }
        public static AlmacenGasDescarga ObtenerDescargaPorClaveOperacion(string claveOperacion)
        {
            return new AlmacenGasDescargaDataAccess().BuscarClaveOperacion(claveOperacion);
        }
        public static List<AlmacenGasDescarga> ObtenerDescargasTodas()
        {
            return new AlmacenGasDescargaDataAccess().BuscarTodas();
        }
        public static List<AlmacenGasDescarga> ObtenerDescargasTodas(DateTime periodo)
        {
            return new AlmacenGasDescargaDataAccess().BuscarTodas(periodo, TokenServicio.ObtenerIdEmpresa());
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
            if (descarga.Fotos != null)
                if (descarga.Fotos.Count > 0)
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
        public static List<Camioneta> ObtenerCamionetasEmpresa(short idEmpresa)
        {
            return new AlmacenGasDataAccess().ObtenerCamionetasEmpresa(idEmpresa);
        }
        public static Camioneta ObtenerCamioneta(int camioneta)
        {
            return new AlmacenGasDataAccess().ObtenerCamioneta(camioneta);
        }
        public static Pipa ObtenerPipa(int pipa)
        {
            return new PipaDataAccess().ObtenerPipa(pipa);
        }
        public static List<AlmacenGasCalibracionFoto> ObtenerImagenes(AlmacenGasCalibracion calibracion)
        {
            if (calibracion.Fotografias != null && calibracion.Fotografias.Count > 0)
                return calibracion.Fotografias.ToList();
            return new AlmacenGasDataAccess().BuscarImagenesCalibracion(calibracion.IdCAlmacenGas);
        }
        public static List<AlmacenGasTomaLecturaFoto> ObtenerImagenes(AlmacenGasTomaLectura TomaLectura)
        {
            if (TomaLectura.Fotografias != null && TomaLectura.Fotografias.Count > 0)
                return TomaLectura.Fotografias.ToList();
            return new AlmacenGasDataAccess().BuscarImagenesTomaLectura(TomaLectura.IdCAlmacenGas);
        }
        public static string ObtenerNombreUnidadAlmacenGas(UnidadAlmacenGas uAG)
        {
            if (uAG.EsGeneral) return uAG.Numero;

            var nombre = EquipoTransporteServicio.ObtenerNombre(uAG);
            if (!string.IsNullOrEmpty(nombre))
                return nombre;

            return EstacionCarburacionServicio.ObtenerNombre(uAG);
        }
        public static string ObtenerNombreUnidadAlmacenGas(Pedido p)
        {
            if (p.IdCamioneta != 0)
                return ObtenerCamioneta(p.IdCamioneta.Value).Nombre;
            if (p.IdPipa != 0)
                return ObtenerPipa(p.IdPipa.Value).Nombre;
            return "Sin Asignar";

        }
        public static string ObtenerCantidad(Pedido p, string cant)
        {
            if (p.IdCamioneta != 0)
                return cant.TrimEnd(' ').TrimEnd(',');
            if (p.PedidoDetalle.Count > 0)
                return CalculosGenerales.Truncar(p.PedidoDetalle.FirstOrDefault().Cantidad.Value, 2).ToString().Split(',')[0] + " Kg";
            return Error.NoEncontrado;
        }
        public static ReporteDiaDTO ReporteDia(DateTime fecha, short idCAlmacenGas)
        {
            var almacen = ObtenerAlmacen(idCAlmacenGas);

            ReporteDiaDTO reporteDTO = new ReporteDiaDTO();
            var lectInicial = BuscarLecturaPorFecha(almacen.IdCAlmacenGas, TipoEventoEnum.Inicial, fecha);
            var lectFinal = BuscarLecturaPorFecha(almacen.IdCAlmacenGas, TipoEventoEnum.Final, fecha);
            var puntoVenta = PuntoVentaServicio.Obtener(almacen);
            var ventasContado = PuntoVentaServicio.ObtenerVentasContado(puntoVenta.IdPuntoVenta, fecha);
            var ventasCredito = PuntoVentaServicio.ObtenerVentasCredito(puntoVenta.IdPuntoVenta, fecha);
            var TotalVentas = new PuntoVentaDataAccess().ObtenerVentas(puntoVenta.IdPuntoVenta, fecha);
            var precioVenta = PrecioVentaGasServicio.ObtenerPrecioVigente(TokenServicio.ObtenerIdEmpresa());
            decimal precioVentaGas = precioVenta.PrecioSalidaKg ?? 0;
            #region Camioneta
            if (almacen.IdCamioneta != null && almacen.IdCamioneta > 0)
            {
                #region Verifico si hay lecturas
                if (lectInicial != null && lectFinal != null)
                {
                    var cilindrosInicial = lectInicial.Cilindros;
                    var cilindrosFinal = lectFinal.Cilindros;
                    var autoConsumo = new AlmacenDataAccess().BuscarAutoconsumo(almacen, fecha);
                    reporteDTO = ReporteAdapter.ToDtoCamioneta(almacen, cilindrosInicial, cilindrosFinal, ventasContado, ventasCredito, lectInicial, lectFinal);
                    reporteDTO.OtrasVentas = TotalVentas.SelectMany(tv => tv.VentaPuntoDeVentaDetalle.Where(vd => !vd.IdProducto.Equals(precioVenta.IdProducto)
                                                                                                        && !vd.IdProductoLinea.Equals(precioVenta.IdProductoLinea)
                                                                                                        && !vd.IdCategoria.Equals(precioVenta.IdCategoria)).ToList().Select(x => PuntoVentaAdapter.FromDTO(x))).ToList();
                    reporteDTO.Fecha = fecha;
                    reporteDTO.EsCamioneta = true;
                    if (autoConsumo != null)
                        reporteDTO.Carburacion = (autoConsumo.UnidadEntrada.CapacidadTanqueLt ?? 0 / 100) * lectFinal.Porcentaje ?? 0;
                    reporteDTO.OtrasVentasTotal = TotalVentas.Sum(tv => tv.VentaPuntoDeVentaDetalle.Where(vd => !vd.IdProducto.Equals(precioVenta.IdProducto)
                                                                                                        && !vd.IdProductoLinea.Equals(precioVenta.IdProductoLinea)
                                                                                                        && !vd.IdCategoria.Equals(precioVenta.IdCategoria)).Sum(x => x.Subtotal));
                    reporteDTO.Precio = reporteDTO.Precio = CalculosGenerales.Promediar(ventasContado.Sum(x => x.VentaPuntoDeVentaDetalle.Sum(y => y.PrecioUnitarioProducto) ?? 0), ventasContado.Count);
                    reporteDTO.Error = false;
                    reporteDTO.Mensaje = Exito.OK;
                    reporteDTO.IdCAlmacenGas = almacen.IdCAlmacenGas;
                    reporteDTO.NombreCAlmacen = almacen.Camioneta.Nombre;
                    reporteDTO.Estacion = almacen.Camioneta.Nombre;
                    reporteDTO.ClaveReporte = DateTime.Now.Year + "R" + FechasFunciones.ObtenerClaveUnica();
                    reporteDTO.KilosDeVenta = TotalVentas.Sum(tv => tv.VentaPuntoDeVentaDetalle.Where(vd => vd.IdProducto.Equals(precioVenta.IdProducto)
                                                                                                        && vd.IdProductoLinea.Equals(precioVenta.IdProductoLinea)
                                                                                                        && vd.IdCategoria.Equals(precioVenta.IdCategoria)).Sum(x => x.CantidadKg ?? 0));
                }
                else
                {
                    reporteDTO.Error = true;
                    reporteDTO.Mensaje = "Para poder realizar el reporte del día es necesario tener las lecturas iniciales y finales la camioneta: " + almacen.Camioneta.Nombre;
                }
                #endregion
            }
            #endregion
            #region Estación y pipa
            else
            {
                #region Verifico si hay lecturas
                if (lectInicial != null && lectFinal != null)
                {
                    reporteDTO.Error = false;
                    reporteDTO.Fecha = fecha;
                    reporteDTO.Mensaje = "Exito";
                    reporteDTO.Precio = precioVentaGas;//CalculosGenerales.Promediar(ventasContado.Sum(x => x.VentaPuntoDeVentaDetalle.Sum(y => y.PrecioUnitarioProducto) ?? 0), TotalVentas.Count);
                    reporteDTO.Importe = TotalVentas.Where(t => !t.VentaACredito).Sum(x => x.Total);
                    reporteDTO.LitrosVenta = TotalVentas.Sum(x => x.VentaPuntoDeVentaDetalle.Sum(v => v.CantidadLt ?? 0));
                    reporteDTO.ClaveReporte = fecha.Year + "R" + FechasFunciones.ObtenerClaveUnica();
                    reporteDTO.ImporteCredito = TotalVentas.Where(t => t.VentaACredito).Sum(x => x.Total);
                    if (almacen.EstacionCarburacion != null)
                        reporteDTO.NombreCAlmacen = almacen.EstacionCarburacion.Nombre;
                    if (almacen.Pipa != null)
                        reporteDTO.NombreCAlmacen = almacen.Pipa.Nombre;
                    //reporteDTO.Estacion = almacen.EstacionCarburacion.Nombre;
                    reporteDTO.Medidor = TipoMedidorAdapter.ToDto(almacen.Medidor);
                    reporteDTO.LecturaInicial = ReporteAdapter.ToDTO(lectInicial);
                    reporteDTO.LecturaFinal = ReporteAdapter.ToDTO(lectFinal);
                }
                else
                {
                    reporteDTO.Error = true;
                    if (almacen.IdPipa > 0)
                        reporteDTO.Mensaje = "Para poder realizar el reporte del día es necesario tener las lecturas iniciales y finales de la pipa: " + almacen.Pipa.Nombre;
                    else
                        reporteDTO.Mensaje = "Para poder realizar el reporte del día es necesario tener las lecturas iniciales y finales de la estación: " + almacen.EstacionCarburacion.Nombre;
                }
                #endregion                
            }
            #endregion
            #region Registro reporte en tabla
            if (reporteDTO.Error == false)
            {
                var reporteEntity = ReporteAdapter.FromDTO(reporteDTO);
                var usuario = TokenServicio.ObtenerUsuarioAplicacion();
                var operadorChofer = puntoVenta.OperadorChofer;
                var usuarioEncargado = puntoVenta.OperadorChofer.Usuario;
                var cortes = PuntoVentaServicio.ObtenerCortes(puntoVenta, fecha);
                //var reportes = new AlmacenGasDataAccess().ObtenerReportes();
                decimal totalAnticipos = 0;
                decimal totalCortes = 0;
                if (almacen.IdEstacionCarburacion > 0)
                {
                    var anticipos = PuntoVentaServicio.ObtenerAnticipos(puntoVenta, fecha);
                    if (anticipos != null && anticipos.Count > 0)
                        totalAnticipos = anticipos.Sum(x => x.TotalAnticipado);
                }
                if (cortes != null)
                    totalCortes = cortes.TotalAnticipado;
                reporteEntity.IdPuntoVenta = puntoVenta.IdPuntoVenta;
                reporteEntity.IdCAlmacenGas = puntoVenta.IdCAlmacenGas;
                reporteEntity.IdOperadorChofer = puntoVenta.IdOperadorChofer;
                reporteEntity.IdEmpresa = almacen.IdEmpresa;
                reporteEntity.Orden = ordenReportes(new AlmacenGasDataAccess().ObtenerReportes());
                reporteEntity.IdUsuarioJEC = usuario.IdUsuario;
                reporteEntity.UsuarioJEC = usuario.Nombre + " " + usuario.Apellido1 + " " + usuario.Apellido2;
                reporteEntity.OperadorChofer = usuarioEncargado.Nombre + " " + usuarioEncargado.Apellido1 + " " + usuarioEncargado.Apellido2;
                reporteEntity.ImporteAnticipos = totalAnticipos;
                reporteEntity.ImporteCortes = totalCortes;

                reporteEntity.PuntoVenta = reporteDTO.NombreCAlmacen;
                var respuesta = PuntoVentaServicio.RegistarReporteDia(reporteEntity);
                if (!respuesta.Exito)
                {
                    reporteDTO.Error = true;
                    reporteDTO.Mensaje = respuesta.Mensaje;
                }
            }
            #endregion
            var upVentas = CajaGeneralAdapter.FromEntity(ventasContado);
            upVentas.Select(x => { x.OperadorChofer = reporteDTO.ClaveReporte; return x; });
            var resp = PuntoVentaServicio.Modificar(upVentas);
            if (!resp.Exito)
                return ErrorDTO(resp);

            return reporteDTO;
        }
        public static ReporteDiaDTO CrearReporteMobil(VPuntoVentaDetalleDTO reporte, UnidadAlmacenGas almacen)
        {
            string nombreAlmacen = "";
            if (almacen.IdEstacionCarburacion != 0 && almacen != null)
                nombreAlmacen = almacen.EstacionCarburacion.Nombre;

            else if (almacen.IdPipa != 0 && almacen != null)
                nombreAlmacen = almacen.Pipa.Nombre;
            var litrosVenta = reporte.P5000Inicial ?? 0 - reporte.P5000Final ?? 0;
            return new ReporteDiaDTO()
            {
                ClaveReporte = reporte.FolioOperacion,
                EsCamioneta = false,
                Carburacion = reporte.Carburacion ?? 0,
                Fecha = reporte.FechaRegistro,
                KilosDeVenta = reporte.CantidadKg ?? 0,
                Importe = 0,
                ImporteCredito = 0,
                LecturaFinal = new LecturaAlmacenDto()
                {
                    CantidadP5000 = reporte.P5000Final ?? 0,
                    PorcentajeMedidor = reporte.PorcentajeFinal ?? 0
                },
                LecturaInicial = new LecturaAlmacenDto()
                {
                    CantidadP5000 = reporte.PorcentajeInicial ?? 0,
                    PorcentajeMedidor = reporte.PorcentajeInicial ?? 0
                },
                Medidor = TipoMedidorAdapter.ToDto(almacen.Medidor),
                IdCAlmacenGas = almacen.IdCAlmacenGas,
                NombreCAlmacen = nombreAlmacen,
                Precio = reporte.PrecioLitro ?? 0,
                LitrosVenta = litrosVenta

            };
        }
        /// <summary>
        /// Permuite buscar si el almacen de gas cuenta con una lectura, 
        /// en la fecha actual, retornara una entidad de tipo 
        /// AlmacenGasTomaLectura como resultado, en caso de que no 
        /// retornara un objeto nulo 
        /// </summary>
        /// <param name="fecha">Fecha de busqueda </param>
        /// <param name="idCAlmacenGas">Id del CAlmacenGas que se consultara </param>
        /// <returns>Entidad resultado en caso de existir un registro</returns>
        public static AlmacenGasTomaLectura ObtenerLecturaFinal(DateTime fecha, short idCAlmacenGas)
        {
            return new AlmacenGasDataAccess().ObtenerLectura(fecha, idCAlmacenGas);
        }
        /// <summary>
        /// BuscarLectura
        /// Permite realizar la busqeuda de una lectura con los parametros enviados.
        /// Se envian de parametros el id del IdCAlmacengas a buscar la lectura, una
        /// fecha de busqueda en especifico y si es inicial o final lo que se retorna
        /// en caso de encontrar retornara una entidad de tipo AlmacenGasTomaLectura
        /// </summary>
        /// <param name="idCAlmacenGas">Id de CAlmacenGas</param>
        /// <param name="fecha">Fecha de busqueda</param>
        /// <param name="inicial">Es una lectura incial o final </param>
        /// <returns>Una entidad de tipo AlmacenGasTomaLectura en caso de existir</returns>
        public static AlmacenGasTomaLectura BuscarLectura(short idCAlmacenGas, DateTime fecha, bool inicial = true)
        {
            return new AlmacenGasDataAccess().BuscarLectura(idCAlmacenGas, fecha, inicial);
        }
        public static List<Pipa> ObtenerPipasEmpresa(short idEmpresa)
        {
            return new PipaDataAccess().ObtenerPipas(idEmpresa);
        }
        public static short ordenReportes(List<ReporteDelDia> reportes)
        {
            if (reportes != null)
                if (reportes.Count > 0)
                    return (short)(reportes.OrderByDescending(x => x.Orden).FirstOrDefault().Orden + 1);
                else
                    return 1;
            else
                return 1;
        }
        public static short ObtenerOrdenMovimientoEnInventario(short idEmpresa, short idAlmacenGas, DateTime fecha)
        {
            var listaMovimientos = ObtenerMovimientosEnInventario(idEmpresa, idAlmacenGas, fecha);
            if (listaMovimientos == null || listaMovimientos.Count <= 0) return 1;

            int numOrden = listaMovimientos.LastOrDefault().Orden;
            numOrden++;

            return (short)numOrden;
        }
        public static List<AlmacenGasMovimiento> ObtenerMovimientos(string folio, DateTime fecha)
        {
            return new AlmacenGasDataAccess().BuscarMovimientos(folio, (short)fecha.Year, (byte)fecha.Month, (byte)fecha.Day);
        }
        public static List<AlmacenGasMovimiento> ObtenerMovimientosEnInventario(short idEmpresa, short idAlmacenGas, DateTime fecha)
        {
            return new AlmacenGasDataAccess().BuscarMovimientosEnInventario(idEmpresa, idAlmacenGas, (short)fecha.Year, (byte)fecha.Month, (byte)fecha.Day);
        }
        public static AlmacenGasMovimiento ObtenerUltimoMovimientoEnInventario(short idEmpresa, short idAlmacenGas)
        {
            return new AlmacenGasDataAccess().BuscarUltimoMovimientoEnInventario(idEmpresa, idAlmacenGas);
        }
        public static AlmacenGasMovimiento ObtenerUltimoMovimientoEnInventario(short idEmpresa, short idAlmacenGas, byte idTipoEvento, byte idTipoMovimiento)
        {
            return new AlmacenGasDataAccess().BuscarUltimoMovimientoEnInventario(idEmpresa, idAlmacenGas, idTipoEvento, idTipoMovimiento);
        }
        public static AlmacenGasMovimiento ObtenerUltimoMovimientoEnInventario(short idEmpresa, short idAlmacenGas, DateTime? fecha)
        {
            if (fecha == null)
                return AlmacenGasAdapter.FromInit();
            var ulMov = new AlmacenGasDataAccess().BuscarUltimoMovimientoEnInventario(idEmpresa, idAlmacenGas, (short)fecha.Value.Year, (byte)fecha.Value.Month, (byte)fecha.Value.Day);
            if (ulMov != null) return ulMov;

            ulMov = ObtenerUltimoMovimientoEnInventario(idEmpresa, idAlmacenGas);
            if (ulMov != null) return ulMov;

            return AlmacenGasAdapter.FromInit();
        }
        public static AlmacenGasMovimiento ObtenerUltimoMovimientoEnInventario(short idEmpresa, short idAlmacenGas, byte idTipoEvento, byte idTipoMovimiento, DateTime fecha)
        {
            var ulMov = new AlmacenGasDataAccess().BuscarUltimoMovimientoEnInventario(idEmpresa, idAlmacenGas, idTipoEvento, idTipoMovimiento, (short)fecha.Year, (byte)fecha.Month, (byte)fecha.Day);
            if (ulMov != null) return ulMov;

            ulMov = ObtenerUltimoMovimientoEnInventario(idEmpresa, idAlmacenGas, idTipoEvento, idTipoMovimiento);
            if (ulMov != null) return ulMov;

            return AlmacenGasAdapter.FromInit();
        }
        public static AlmacenGasMovimiento ObtenerUltimoMovimientoEnInventario(UnidadAlmacenGas unidad, DateTime fecha)
        {
            return new AlmacenGasDataAccess().BuscarUltimoMovimientoEnInventario(unidad.IdAlmacenGas.Value, unidad.IdCAlmacenGas, unidad.IdEmpresa, (short)fecha.Year, (byte)fecha.Month, (byte)fecha.Day);
        }
        public static List<AlmacenGasTraspaso> Traspasos(short idCAlmacenGas)
        {
            return new AlmacenGasDataAccess().Traspasos(idCAlmacenGas);
        }
        public static List<AlmacenGasMovimiento> ObtenerUltimosMovimientosDeDescargas(AlmacenGasDescarga descarga, short idEmpresa)
        {
            if (descarga.FechaFinDescarga != null)
            {
                var ulMovDia = new AlmacenGasDataAccess().BuscarUltimoMovimientoConTipoEvento(idEmpresa, TipoEventoEnum.Descarga, (short)descarga.FechaFinDescarga.Value.Year, (byte)descarga.FechaFinDescarga.Value.Month, (byte)descarga.FechaFinDescarga.Value.Day);
                var ulMovMes = new AlmacenGasDataAccess().BuscarUltimoMovimientoConTipoEvento(idEmpresa, TipoEventoEnum.Descarga, (short)descarga.FechaFinDescarga.Value.Year, (byte)descarga.FechaFinDescarga.Value.Month);
                var ulMovAnio = new AlmacenGasDataAccess().BuscarUltimoMovimientoConTipoEvento(idEmpresa, TipoEventoEnum.Descarga, (short)descarga.FechaFinDescarga.Value.Year);

                return new List<AlmacenGasMovimiento>()
                {
                    ulMovDia, ulMovMes, ulMovAnio
                };
            }
            return new List<AlmacenGasMovimiento>();
        }
        public static List<AlmacenGasMovimiento> ObtenerUltimosMovimientosPorUnidadAlmacenGas(short idEmpresa, short idCAlmacenGas, DateTime fecha)
        {
            var ulMovDia = new AlmacenGasDataAccess().BuscarUltimoMovimientoPorUnidadAlamcenGasConTipoEvento(idEmpresa, idCAlmacenGas, TipoEventoEnum.Descarga, (short)fecha.Year, (byte)fecha.Month, (byte)fecha.Day);
            var ulMovMes = new AlmacenGasDataAccess().BuscarUltimoMovimientoPorUnidadAlamcenGasConTipoEvento(idEmpresa, idCAlmacenGas, TipoEventoEnum.Descarga, (short)fecha.Year, (byte)fecha.Month);
            var ulMovAnio = new AlmacenGasDataAccess().BuscarUltimoMovimientoPorUnidadAlamcenGasConTipoEvento(idEmpresa, idCAlmacenGas, TipoEventoEnum.Descarga, (short)fecha.Year);

            return new List<AlmacenGasMovimiento>()
            {
                ulMovDia, ulMovMes, ulMovAnio
            };
        }
        public static List<AlmacenGasMovimiento> ObtenerUltimosMovimientosVentasAlmacenGas(short idEmpresa, short idCAlmacenGas, DateTime fecha, Byte TEvento)
        {
            var ulMovDia = new AlmacenGasDataAccess().BuscarUltimoMovimientoPorUnidadAlamcenGasConTipoEvento(idEmpresa, idCAlmacenGas, TEvento, (short)fecha.Year, (byte)fecha.Month, (byte)fecha.Day);
            var ulMovMes = new AlmacenGasDataAccess().BuscarUltimoMovimientoPorUnidadAlamcenGasConTipoEvento(idEmpresa, idCAlmacenGas, TEvento, (short)fecha.Year, (byte)fecha.Month);
            var ulMovAnio = new AlmacenGasDataAccess().BuscarUltimoMovimientoPorUnidadAlamcenGasConTipoEvento(idEmpresa, idCAlmacenGas, TEvento, (short)fecha.Year);

            return new List<AlmacenGasMovimiento>()
            {
                ulMovDia, ulMovMes, ulMovAnio
            };
        }
        public static AlmacenGasMovimiento ObtenerUltimoMovimientoDeVenta(short idEmpresa, short idCAlmacenGas, DateTime fecha)
        {
            var movimientos = ObtenerUltimosMovimientosVentasAlmacenGas(idEmpresa, idCAlmacenGas, fecha, TipoEventoEnum.Venta);

            if (movimientos.ElementAt(0) != null) return movimientos.ElementAt(0);

            if (movimientos.ElementAt(1) != null) return movimientos.ElementAt(1);

            if (movimientos.ElementAt(2) != null) return movimientos.ElementAt(2);

            return AlmacenGasAdapter.FromInit();
        }
        public static List<AlmacenGasMovimiento> ObtenerUltimosMovimientosPorUnidadAlmacenGas(short idEmpresa, short idCAlmacenGas, byte idTipoEvento, byte idTipoMovimiento, DateTime fecha)
        {
            var ulMovDia = new AlmacenGasDataAccess().BuscarUltimoMovimientoPorUnidadAlamcenGasConTipoEvento(idEmpresa, idCAlmacenGas, idTipoEvento, idTipoMovimiento, (short)fecha.Year, (byte)fecha.Month, (byte)fecha.Day);
            var ulMovMes = new AlmacenGasDataAccess().BuscarUltimoMovimientoPorUnidadAlamcenGasConTipoEvento(idEmpresa, idCAlmacenGas, idTipoEvento, idTipoMovimiento, (short)fecha.Year, (byte)fecha.Month);
            var ulMovAnio = new AlmacenGasDataAccess().BuscarUltimoMovimientoPorUnidadAlamcenGasConTipoEvento(idEmpresa, idCAlmacenGas, idTipoEvento, idTipoMovimiento, (short)fecha.Year);

            return new List<AlmacenGasMovimiento>()
            {
                ulMovDia, ulMovMes, ulMovAnio
            };
        }
        public static AlmacenGasMovimiento ObtenerUltimoMovimientoPorUnidadAlmacenGas(short idEmpresa, short idCAlmacenGas, DateTime? fecha)
        {
            if (fecha == null) return AlmacenGasAdapter.FromInit();

            var movimientos = ObtenerUltimosMovimientosPorUnidadAlmacenGas(idEmpresa, idCAlmacenGas, fecha.Value);

            if (movimientos.ElementAt(0) != null) return movimientos.ElementAt(0);

            if (movimientos.ElementAt(1) != null) return movimientos.ElementAt(1);

            if (movimientos.ElementAt(2) != null) return movimientos.ElementAt(2);

            return AlmacenGasAdapter.FromInit();
        }
        public static AlmacenGasMovimiento ObtenerUltimoMovimientoPorUnidadAlmacenGas(short idEmpresa, short idCAlmacenGas)
        {
            var movimiento = new AlmacenGasDataAccess().BuscarUltimoMovimientoPorUnidadAlamcenGas(idEmpresa, idCAlmacenGas);
            if (movimiento != null) return movimiento;

            return AlmacenGasAdapter.FromInit();
        }
        public static AlmacenGasMovimiento ObtenerUltimoMovimientoPorUnidadAlmacenGas(short idEmpresa, short idCAlmacenGas, byte idTipoEvento, byte idTipoMovimiento)
        {
            var movimiento = new AlmacenGasDataAccess().BuscarUltimoMovimientoPorUnidadAlamcenGasConTipoEvento(idEmpresa, idCAlmacenGas, idTipoEvento, idTipoMovimiento);
            if (movimiento != null) return movimiento;

            return AlmacenGasAdapter.FromInit();
        }
        public static AlmacenGasMovimiento ObtenerUltimoMovimientoPorUnidadAlmacenGas(short idEmpresa, short idCAlmacenGas, byte idTipoEvento, byte idTipoMovimiento, DateTime fecha)
        {
            var movimiento = new AlmacenGasDataAccess().BuscarUltimoMovimientoPorUnidadAlamcenGasConTipoEvento(idEmpresa, idCAlmacenGas, idTipoEvento, idTipoMovimiento, (short)fecha.Year, (byte)fecha.Month, (byte)fecha.Day);
            if (movimiento != null) return movimiento;

            return AlmacenGasAdapter.FromInit();
        }
        public static AlmacenGasMovimiento ObtenerUltimoMovimientoPorUnidadAlmacenGas(short idEmpresa, short idCAlmacenGas, byte idTipoEvento, byte idTipoMovimiento, DateTime fecha, short OrdenMayorA)
        {
            var movimiento = ObtenerUltimoMovimientoPorUnidadAlmacenGas(idEmpresa, idCAlmacenGas, idTipoEvento, idTipoMovimiento, fecha);
            if (movimiento.IdEmpresa <= 0 && movimiento.Year <= 0 && movimiento.Mes <= 0 && movimiento.Dia <= 0 && movimiento.Orden <= 0) return movimiento;

            if (movimiento.Orden <= OrdenMayorA) return AlmacenGasAdapter.FromInit();

            return movimiento;
        }
        public static AlmacenGasTomaLectura BuscarLecturaPorFecha(short idCAlmacenGas, byte tipoEvento, DateTime fecha)
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
            //return new AlmacenGasDataAccess().BuscarTodosCilindros(TokenServicio.ObtenerIdEmpresa());
            return new AlmacenGasDataAccess().BuscarTodosCilindros((short)2);
        }
        public static List<CamionetaCilindro> ObtenerCilindros(UnidadAlmacenGas unidad)
        {
            if (unidad.Camioneta != null)
                if (unidad.Camioneta.Cilindros != null && unidad.Camioneta.Cilindros.Count > 0)
                    return unidad.Camioneta.Cilindros.ToList();

            return new AlmacenGasDataAccess().BuscarTodosCilindros(unidad.IdCamioneta.Value);
        }
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
            return tmCil.Select(x => AdaptarCilindro(x)).ToList();
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
            //var lecturasIniciales = AplicarTomaLecturaInicial();
            var descargasDto = AplicarDescargas();
            var recargasDto = AplicarRecargas();
            var traspasosDto = AplicarTraspaso();
            //var autoConsumosDto = AplicarAutoConsumo();
            //var calibracionesDto = AplicarCalibracion();
            var lecturasFinales = AplicarTomaLecturaFinal();
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
            decimal kilogramosPapeletaTractor = descarga.MasaKg ?? 0;
            decimal litrosPapeletaTractor = CalcularGasServicio.ObtenerLitrosDesdeKilos(kilogramosPapeletaTractor, empresa.FactorLitrosAKilos);
            decimal litrosRealesTractor = CalcularGasServicio.ObtenerLitrosEnElTanque(descarga.CapacidadTanqueLt.Value, descarga.PorcenMagnatelOcularTractorINI ?? 0);
            decimal kilogramosRealesTractor = CalcularGasServicio.ObtenerKilogramosDesdeLitros(litrosRealesTractor, empresa.FactorLitrosAKilos);
            decimal kilogramosRemanentes = CalcularGasServicio.ObtenerDiferenciaKilogramos(kilogramosRealesTractor, kilogramosPapeletaTractor);
            decimal litrosRemanentes = CalcularGasServicio.ObtenerLitrosDesdeKilos(kilogramosRemanentes, empresa.FactorLitrosAKilos);

            decimal unidadEntradaCantidadKg = unidadEntrada.CantidadActualKg;
            decimal unidadEntradaCantidadLt = unidadEntrada.CantidadActualLt;
            decimal unidadEntradaPorcentaje = unidadEntrada.PorcentajeActual;

            unidadEntrada.CantidadActualKg = CalcularGasServicio.SumarKilogramos(unidadEntrada.CantidadActualKg, kilogramosRealesTractor);
            unidadEntrada.CantidadActualLt = CalcularGasServicio.ObtenerLitrosDesdeKilos(unidadEntrada.CantidadActualKg, empresa.FactorLitrosAKilos);
            unidadEntrada.PorcentajeActual = descarga.PorcenMagnatelOcularAlmacenFIN ?? 0;

            unidadEntrada = AplicarDescargaAlmacenAlterno(unidadEntrada, descarga);

            AlmacenGas almacenGasTotal = ObtenerAlmacenGasTotal(empresa.IdEmpresa);
            decimal almacenTotalCantidadActualKg = almacenGasTotal.CantidadActualKg;
            decimal almacenTotalCantidadActualLt = almacenGasTotal.CantidadActualLt;
            decimal almacenTotalPorcent = almacenGasTotal.PorcentajeActual;
            decimal almacenGeneralCantidadActualKg = almacenGasTotal.CantidadActualGeneralKg;
            decimal almacenGeneralCantidadActualLt = almacenGasTotal.CantidadActualGeneralLt;
            decimal almacenGeneralPorcent = almacenGasTotal.PorcentajeActualGeneral;
            almacenGasTotal = AplicarDescargaAlmacenTotal(almacenGasTotal, unidadEntrada, litrosRealesTractor, kilogramosRealesTractor);

            AlmacenGasMovimiento ulMov = ObtenerUltimoMovimientoEnInventario(empresa.IdEmpresa, almacenGasTotal.IdAlmacenGas, descarga.FechaFinDescarga);
            AlmacenGasMovimiento ulMovDescarga = ObtenerUltimoMovimientoPorUnidadAlmacenGas(empresa.IdEmpresa, unidadEntrada.IdCAlmacenGas, descarga.FechaFinDescarga);
            RemaDto remaDto = RemaServicio.ObtenerRema(descarga, almacenGasTotal.IdAlmacenGas, empresa.IdEmpresa);

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
                RemaDiaKg = CalcularGasServicio.SumarKilogramos(ulMovDescarga.RemaDiaKg ?? 0, kilogramosRemanentes),
                RemaDiaLt = CalcularGasServicio.SumarLitros(ulMovDescarga.RemaDiaLt ?? 0, litrosRemanentes),
                RemaMesKg = CalcularGasServicio.SumarKilogramos(ulMovDescarga.RemaMesKg ?? 0, kilogramosRemanentes),
                RemaMesLt = CalcularGasServicio.SumarLitros(ulMovDescarga.RemaMesLt ?? 0, litrosRemanentes),
                RemaAnioKg = CalcularGasServicio.SumarKilogramos(ulMovDescarga.RemaAnioKg ?? 0, kilogramosRemanentes),
                RemaAnioLt = CalcularGasServicio.SumarLitros(ulMovDescarga.RemaAnioLt ?? 0, litrosRemanentes),
                RemaAcumDiaKg = CalcularGasServicio.SumarKilogramos(ulMov.RemaAcumDiaKg, kilogramosRemanentes),
                RemaAcumDiaLt = CalcularGasServicio.SumarLitros(ulMov.RemaAcumDiaLt, litrosRemanentes),
                RemaAcumMesKg = CalcularGasServicio.SumarKilogramos(ulMov.RemaAcumMesKg, kilogramosRemanentes),
                RemaAcumMesLt = CalcularGasServicio.SumarLitros(ulMov.RemaAcumMesLt, litrosRemanentes),
                RemaAcumAnioKg = CalcularGasServicio.SumarKilogramos(ulMov.RemaAcumAnioKg, kilogramosRemanentes),
                RemaAcumAnioLt = CalcularGasServicio.SumarLitros(ulMov.RemaAcumAnioLt, litrosRemanentes),

                DescargaKg = kilogramosRealesTractor,
                DescargaLt = litrosRealesTractor,
                DescargaDiaKg = CalcularGasServicio.SumarKilogramos(ulMovDescarga.DescargaDiaKg ?? 0, kilogramosRealesTractor),
                DescargaDiaLt = CalcularGasServicio.SumarLitros(ulMovDescarga.DescargaDiaLt ?? 0, litrosRealesTractor),
                DescargaMesKg = CalcularGasServicio.SumarKilogramos(ulMovDescarga.DescargaMesKg ?? 0, kilogramosRealesTractor),
                DescargaMesLt = CalcularGasServicio.SumarLitros(ulMovDescarga.DescargaMesLt ?? 0, litrosRealesTractor),
                DescargaAnioKg = CalcularGasServicio.SumarKilogramos(ulMovDescarga.DescargaAnioKg ?? 0, kilogramosRealesTractor),
                DescargaAnioLt = CalcularGasServicio.SumarLitros(ulMovDescarga.DescargaAnioLt ?? 0, litrosRealesTractor),
                DescargaAcumDiaKg = CalcularGasServicio.SumarKilogramos(ulMov.DescargaAcumDiaKg, kilogramosRealesTractor),
                DescargaAcumDiaLt = CalcularGasServicio.SumarLitros(ulMov.DescargaAcumDiaLt, litrosRealesTractor),
                DescargaAcumMesKg = CalcularGasServicio.SumarKilogramos(ulMov.DescargaAcumMesKg, kilogramosRealesTractor),
                DescargaAcumMesLt = CalcularGasServicio.SumarLitros(ulMov.DescargaAcumMesLt, litrosRealesTractor),
                DescargaAcumAnioKg = CalcularGasServicio.SumarKilogramos(ulMov.DescargaAcumAnioKg, kilogramosRealesTractor),
                DescargaAcumAnioLt = CalcularGasServicio.SumarLitros(ulMov.DescargaAcumAnioLt, litrosRealesTractor),

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
                //DescargaFotos = GenerarImagenes(descarga),
                unidadEntrada = AlmacenGasAdapter.FromEntity(unidadEntrada),
                identidadUE = IdentificarTipoUnidadAlamcenGas(unidadEntrada),
                Movimiento = AlmacenGasAdapter.FromEntity(unidadEntrada, descarga, almacenGasTotal, ulMov, empresa, invAnterior),
            };
        }
        public static UnidadAlmacenGas ObtenerAlmacen(short idCAlmacenGas)
        {
            return new AlmacenGasDataAccess().BuscarAlmacen(idCAlmacenGas);
        }
        public static UnidadAlmacenGas ObtenerAlmacenPrincipal(short idEmpresa)
        {
            return new AlmacenGasDataAccess().BuscarAlmacenPrincipal(idEmpresa);
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

            decimal porcentajeRecargadoEnUnidadEntrada = CalcularGasServicio.ObtenerDiferenciaPorcentaje(apReDto.RecargaLecturaFinal.ProcentajeEntrada ?? 0, apReDto.RecargaLecturaInicial.ProcentajeEntrada ?? 0);
            decimal LitrosRecargados = CalcularGasServicio.ObtenerLitrosDesdePorcentaje(apReDto.unidadEntrada.CapacidadTanqueLt.Value, porcentajeRecargadoEnUnidadEntrada);
            decimal KilosRecargados = CalcularGasServicio.ObtenerKilogramosDesdeLitros(LitrosRecargados, apReDto.Empresa.FactorLitrosAKilos);

            apReDto = AplicarRecarga(apReDto, LitrosRecargados, KilosRecargados);
            apReDto.AlmacenGasAnterior.CantidadActualLt = CalcularGasServicio.RestarLitros(apReDto.AlmacenGasAnterior.CantidadActualLt, LitrosRecargados);
            apReDto.AlmacenGasAnterior.CantidadActualKg = CalcularGasServicio.RestarKilogramos(apReDto.AlmacenGasAnterior.CantidadActualKg, KilosRecargados);
            apReDto.AlmacenGasAnterior.PorcentajeActual = CalcularGasServicio.ObtenerPorcentajeDesdeLitros(apReDto.AlmacenGasAnterior.CapacidadTotalLt, LitrosRecargados);

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
                if (apReDto.RecargaLecturaInicial.Cilindros.FirstOrDefault(x => x.IdCilindro.Equals(cilindro.IdCilindro)) == null)
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

            apReDto.AlmacenGasAnterior = ObtenerAlmacenGasTotal(apReDto.Empresa);
            apReDto = AplicarRecargaAlmacenTotal(apReDto, LitrosRecargados, KilosRecargados);

            AlmacenGasMovimiento ulMov = ObtenerUltimoMovimientoEnInventario(apReDto.Empresa.IdEmpresa, apReDto.AlmacenGas.IdAlmacenGas, apReDto.RecargaLecturaFinal.FechaAplicacion);
            AlmacenGasMovimiento ulMovUnidadEntrada = ObtenerUltimoMovimientoPorUnidadAlmacenGas(apReDto.Empresa.IdEmpresa, apReDto.unidadEntrada.IdCAlmacenGas, apReDto.RecargaLecturaFinal.FechaAplicacion);
            AlmacenGasMovimiento ulMovUnidadSalida = ObtenerUltimoMovimientoPorUnidadAlmacenGas(apReDto.Empresa.IdEmpresa, apReDto.unidadSalida.IdCAlmacenGas, apReDto.RecargaLecturaFinal.FechaAplicacion);

            var invEntradaAnterior = new InventarioAnteriorDto
            {
                NombreOperador = OperadorChoferServicio.ObtenerNombreCompleto(apReDto.unidadEntrada),

                EntradaKg = KilosRecargados,
                EntradaLt = LitrosRecargados,
                CantidadAnteriorKg = ulMovUnidadEntrada.CantidadActualKg,
                CantidadAnteriorLt = ulMovUnidadEntrada.CantidadActualLt,
                PorcentajeAnterior = ulMovUnidadEntrada.PorcentajeActual ?? 0,
                P5000Anterior = ulMovUnidadEntrada.P5000Actual,

                CAlmEntradaDiaKg = CalcularGasServicio.SumarKilogramos(ulMovUnidadEntrada.CAlmEntradaDiaKg, KilosRecargados),
                CAlmEntradaDiaLt = CalcularGasServicio.SumarLitros(ulMovUnidadEntrada.CAlmEntradaDiaLt, LitrosRecargados),
                CAlmEntradaMesKg = CalcularGasServicio.SumarKilogramos(ulMovUnidadEntrada.CAlmEntradaMesKg, KilosRecargados),
                CAlmEntradaMesLt = CalcularGasServicio.SumarLitros(ulMovUnidadEntrada.CAlmEntradaMesLt, LitrosRecargados),
                CAlmEntradaAnioKg = CalcularGasServicio.SumarKilogramos(ulMovUnidadEntrada.CAlmEntradaAnioKg, KilosRecargados),
                CAlmEntradaAnioLt = CalcularGasServicio.SumarLitros(ulMovUnidadEntrada.CAlmEntradaAnioLt, LitrosRecargados),
                CAlmSalidaDiaKg = ulMovUnidadEntrada.CAlmSalidaDiaKg,
                CAlmSalidaDiaLt = ulMovUnidadEntrada.CAlmSalidaDiaLt,
                CAlmSalidaMesKg = ulMovUnidadEntrada.CAlmSalidaMesKg,
                CAlmSalidaMesLt = ulMovUnidadEntrada.CAlmSalidaMesLt,
                CAlmSalidaAnioKg = ulMovUnidadEntrada.CAlmSalidaAnioKg,
                CAlmSalidaAnioLt = ulMovUnidadEntrada.CAlmSalidaAnioLt,
                CantidadAcumuladaDiaKg = CalcularGasServicio.SumarKilogramos(ulMovUnidadEntrada.CantidadAcumuladaDiaKg, KilosRecargados),
                CantidadAcumuladaDiaLt = CalcularGasServicio.SumarLitros(ulMovUnidadEntrada.CantidadAcumuladaDiaLt, LitrosRecargados),
                CantidadAcumuladaMesKg = CalcularGasServicio.SumarKilogramos(ulMovUnidadEntrada.CantidadAcumuladaMesKg, KilosRecargados),
                CantidadAcumuladaMesLt = CalcularGasServicio.SumarLitros(ulMovUnidadEntrada.CantidadAcumuladaMesLt, LitrosRecargados),
                CantidadAcumuladaAnioKg = CalcularGasServicio.SumarKilogramos(ulMovUnidadEntrada.CantidadAcumuladaAnioKg, KilosRecargados),
                CantidadAcumuladaAnioLt = CalcularGasServicio.SumarLitros(ulMovUnidadEntrada.CantidadAcumuladaAnioLt, LitrosRecargados),

                RecargaKg = KilosRecargados,
                RecargaLt = LitrosRecargados,
                RecargaDiaKg = CalcularGasServicio.SumarKilogramos(ulMovUnidadEntrada.RecargaDiaKg ?? 0, KilosRecargados),
                RecargaDiaLt = CalcularGasServicio.SumarLitros(ulMovUnidadEntrada.RecargaDiaLt ?? 0, LitrosRecargados),
                RecargaMesKg = CalcularGasServicio.SumarKilogramos(ulMovUnidadEntrada.RecargaMesKg ?? 0, KilosRecargados),
                RecargaMesLt = CalcularGasServicio.SumarLitros(ulMovUnidadEntrada.RecargaMesLt ?? 0, LitrosRecargados),
                RecargaAnioKg = CalcularGasServicio.SumarKilogramos(ulMovUnidadEntrada.RecargaAnioKg ?? 0, KilosRecargados),
                RecargaAnioLt = CalcularGasServicio.SumarLitros(ulMovUnidadEntrada.RecargaAnioLt ?? 0, LitrosRecargados),
                RecargaAcumDiaKg = CalcularGasServicio.SumarKilogramos(ulMov.RecargaAcumDiaKg, KilosRecargados),
                RecargaAcumDiaLt = CalcularGasServicio.SumarLitros(ulMov.RecargaAcumDiaLt, LitrosRecargados),
                RecargaAcumMesKg = CalcularGasServicio.SumarKilogramos(ulMov.RecargaAcumMesKg, KilosRecargados),
                RecargaAcumMesLt = CalcularGasServicio.SumarLitros(ulMov.RecargaAcumMesLt, LitrosRecargados),
                RecargaAcumAnioKg = CalcularGasServicio.SumarKilogramos(ulMov.RecargaAcumAnioKg, KilosRecargados),
                RecargaAcumAnioLt = CalcularGasServicio.SumarLitros(ulMov.RecargaAcumAnioLt, LitrosRecargados),

                CantidadAnteriorTotalKg = apReDto.AlmacenGasAnterior.CapacidadTotalKg,
                CantidadAnteriorTotalLt = apReDto.AlmacenGasAnterior.CapacidadTotalLt,
                PorcentajeAnteriorTotal = apReDto.AlmacenGasAnterior.PorcentajeActual,
                CantidadAnteriorGeneralKg = apReDto.AlmacenGasAnterior.CantidadActualGeneralKg,
                CantidadAnteriorGeneralLt = apReDto.AlmacenGasAnterior.CantidadActualGeneralLt,
                PorcentajeAnteriorGeneral = apReDto.AlmacenGasAnterior.PorcentajeActualGeneral,
            };

            var invSalidaAnterior = new InventarioAnteriorDto
            {
                NombreOperador = OperadorChoferServicio.ObtenerNombreCompleto(apReDto.unidadSalida),

                SalidaKg = KilosRecargados,
                SalidaLt = LitrosRecargados,
                CantidadAnteriorKg = ulMovUnidadSalida.CantidadActualKg,
                CantidadAnteriorLt = ulMovUnidadSalida.CantidadActualLt,
                PorcentajeAnterior = ulMovUnidadSalida.PorcentajeActual ?? 0,
                P5000Anterior = ulMovUnidadSalida.P5000Actual ?? 0,

                CAlmSalidaDiaKg = CalcularGasServicio.SumarKilogramos(ulMovUnidadSalida.CAlmSalidaDiaKg, KilosRecargados),
                CAlmSalidaDiaLt = CalcularGasServicio.SumarLitros(ulMovUnidadSalida.CAlmSalidaDiaLt, LitrosRecargados),
                CAlmSalidaMesKg = CalcularGasServicio.SumarKilogramos(ulMovUnidadSalida.CAlmSalidaMesKg, KilosRecargados),
                CAlmSalidaMesLt = CalcularGasServicio.SumarLitros(ulMovUnidadSalida.CAlmSalidaMesLt, LitrosRecargados),
                CAlmSalidaAnioKg = CalcularGasServicio.SumarKilogramos(ulMovUnidadSalida.CAlmSalidaAnioKg, KilosRecargados),
                CAlmSalidaAnioLt = CalcularGasServicio.SumarLitros(ulMovUnidadSalida.CAlmSalidaAnioLt, LitrosRecargados),
                CAlmEntradaDiaKg = ulMovUnidadSalida.CAlmEntradaDiaKg,
                CAlmEntradaDiaLt = ulMovUnidadSalida.CAlmEntradaDiaLt,
                CAlmEntradaMesKg = ulMovUnidadSalida.CAlmEntradaMesKg,
                CAlmEntradaMesLt = ulMovUnidadSalida.CAlmEntradaMesLt,
                CAlmEntradaAnioKg = ulMovUnidadSalida.CAlmEntradaAnioKg,
                CAlmEntradaAnioLt = ulMovUnidadSalida.CAlmEntradaAnioLt,
                CantidadAcumuladaDiaKg = CalcularGasServicio.SumarKilogramos(ulMovUnidadSalida.CantidadAcumuladaDiaKg, KilosRecargados),
                CantidadAcumuladaDiaLt = CalcularGasServicio.SumarLitros(ulMovUnidadSalida.CantidadAcumuladaDiaLt, LitrosRecargados),
                CantidadAcumuladaMesKg = CalcularGasServicio.SumarKilogramos(ulMovUnidadSalida.CantidadAcumuladaMesKg, KilosRecargados),
                CantidadAcumuladaMesLt = CalcularGasServicio.SumarLitros(ulMovUnidadSalida.CantidadAcumuladaMesLt, LitrosRecargados),
                CantidadAcumuladaAnioKg = CalcularGasServicio.SumarKilogramos(ulMovUnidadSalida.CantidadAcumuladaAnioKg, KilosRecargados),
                CantidadAcumuladaAnioLt = CalcularGasServicio.SumarLitros(ulMovUnidadSalida.CantidadAcumuladaAnioLt, LitrosRecargados),

                RecargaKg = KilosRecargados,
                RecargaLt = LitrosRecargados,
                RecargaDiaKg = CalcularGasServicio.SumarKilogramos(ulMovUnidadSalida.RecargaDiaKg != null ? ulMovUnidadSalida.RecargaDiaKg.Value : 0, KilosRecargados),
                RecargaDiaLt = CalcularGasServicio.SumarLitros(ulMovUnidadSalida.RecargaDiaLt != null ? ulMovUnidadSalida.RecargaDiaLt.Value : 0, LitrosRecargados),
                RecargaMesKg = CalcularGasServicio.SumarKilogramos(ulMovUnidadSalida.RecargaMesKg != null ? ulMovUnidadSalida.RecargaMesKg.Value : 0, KilosRecargados),
                RecargaMesLt = CalcularGasServicio.SumarLitros(ulMovUnidadSalida.RecargaMesLt != null ? ulMovUnidadSalida.RecargaMesLt.Value : 0, LitrosRecargados),
                RecargaAnioKg = CalcularGasServicio.SumarKilogramos(ulMovUnidadSalida.RecargaAnioKg != null ? ulMovUnidadSalida.RecargaAnioKg.Value : 0, KilosRecargados),
                RecargaAnioLt = CalcularGasServicio.SumarLitros(ulMovUnidadSalida.RecargaAnioLt != null ? ulMovUnidadSalida.RecargaAnioLt.Value : 0, LitrosRecargados),
                RecargaAcumDiaKg = CalcularGasServicio.SumarKilogramos(ulMov.RecargaAcumDiaKg, KilosRecargados),
                RecargaAcumDiaLt = CalcularGasServicio.SumarLitros(ulMov.RecargaAcumDiaLt, LitrosRecargados),
                RecargaAcumMesKg = CalcularGasServicio.SumarKilogramos(ulMov.RecargaAcumMesKg, KilosRecargados),
                RecargaAcumMesLt = CalcularGasServicio.SumarLitros(ulMov.RecargaAcumMesLt, LitrosRecargados),
                RecargaAcumAnioKg = CalcularGasServicio.SumarKilogramos(ulMov.RecargaAcumAnioKg, KilosRecargados),
                RecargaAcumAnioLt = CalcularGasServicio.SumarLitros(ulMov.RecargaAcumAnioLt, LitrosRecargados),

                CantidadAnteriorTotalKg = apReDto.AlmacenGasAnterior.CapacidadTotalKg,
                CantidadAnteriorTotalLt = apReDto.AlmacenGasAnterior.CapacidadTotalLt,
                PorcentajeAnteriorTotal = apReDto.AlmacenGasAnterior.PorcentajeActual,
                CantidadAnteriorGeneralKg = apReDto.AlmacenGasAnterior.CantidadActualGeneralKg,
                CantidadAnteriorGeneralLt = apReDto.AlmacenGasAnterior.CantidadActualGeneralLt,
                PorcentajeAnteriorGeneral = apReDto.AlmacenGasAnterior.PorcentajeActualGeneral,
            };

            apReDto.unidadEntrada = AlmacenGasAdapter.FromEntity(apReDto.unidadEntrada);
            apReDto.unidadSalida = AlmacenGasAdapter.FromEntity(apReDto.unidadSalida);

            if (apReDto.identidadUE != identidadUnidadAlmacenGas.Camioneta)
            {
                apReDto.unidadEntrada.PorcentajeActual = apReDto.RecargaLecturaFinal.ProcentajeEntrada ?? 0;
                apReDto.RecargaLecturaFinalFotos = GenerarImagenes(apReDto.RecargaLecturaFinal);
                apReDto.RecargaLecturaFinal.DatosProcesados = true;
                apReDto.RecargaLecturaFinalSinNavProp = AlmacenGasAdapter.FromEntity(apReDto.RecargaLecturaFinal);
            }

            apReDto.MovimientoEntrada = AlmacenGasAdapter.FromEntity(apReDto.unidadEntrada, apReDto.RecargaLecturaFinal, apReDto.AlmacenGas, ulMovUnidadEntrada, apReDto.Empresa, invEntradaAnterior, apReDto.unidadSalida.IdCAlmacenGas, apReDto.unidadSalida.Numero, true);
            apReDto.MovimientoSalida = AlmacenGasAdapter.FromEntity(apReDto.unidadSalida, apReDto.RecargaLecturaFinal, apReDto.AlmacenGas, ulMovUnidadSalida, apReDto.Empresa, invSalidaAnterior, apReDto.unidadEntrada.IdCAlmacenGas, apReDto.unidadEntrada.Numero, false);
            apReDto.RecargaLecturaInicialSinNavProp = AlmacenGasAdapter.FromEntity(apReDto.RecargaLecturaInicial);

            return apReDto;
        }
        public static AplicaRecargaDto AplicarRecargaAlmacenTotal(AplicaRecargaDto apReDto, decimal litrosSalientes, decimal kilogramosSalientes)
        {
            apReDto.AlmacenGas = apReDto.AlmacenGasAnterior;

            if (apReDto.identidadUE.Equals(identidadUnidadAlmacenGas.Pipa) || apReDto.identidadUE.Equals(identidadUnidadAlmacenGas.Camioneta))
            {
                apReDto.AlmacenGas.CantidadActualGeneralLt = CalcularGasServicio.RestarLitros(apReDto.AlmacenGasAnterior.CantidadActualGeneralLt, litrosSalientes);
                apReDto.AlmacenGas.CantidadActualGeneralKg = CalcularGasServicio.RestarKilogramos(apReDto.AlmacenGasAnterior.CantidadActualGeneralKg, kilogramosSalientes);
                apReDto.AlmacenGas.PorcentajeActualGeneral = CalcularGasServicio.ObtenerPorcentajeDesdeLitros(apReDto.AlmacenGasAnterior.CapacidadGeneralLt, apReDto.AlmacenGasAnterior.CantidadActualGeneralLt);
            }
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
                //new AlmacenGasDataAccess().Actualizar(aplicaciones);
            }
            return aplicaciones;
        }
        public static AplicaTraspasoDto AplicarTraspaso(AlmacenGasTraspaso TraspasoInicial, List<AlmacenGasTraspaso> TraspasosFinales)
        {
            AplicaTraspasoDto apReDto = new AplicaTraspasoDto()
            {
                TraspasoLecturaInicial = TraspasoInicial,
                TraspasosFinales = TraspasosFinales,
                unidadSalida = ObtenerUnidadAlamcenGas(TraspasoInicial, true),
                unidadEntrada = ObtenerUnidadAlamcenGas(TraspasoInicial, false),
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

            if (apTrasDto.identidadUS != identidadUnidadAlmacenGas.EstacionCarburacion || apTrasDto.identidadUS != identidadUnidadAlmacenGas.Pipa)
                LitrosTraspasados = CalcularGasServicio.RestarLitrosDesdePorcentaje(LitrosTraspasados, apTrasDto.unidadSalida.PorcentajeCalibracionPlaneada);

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
            if (apTrasDto.identidadUS.Equals(identidadUnidadAlmacenGas.Pipa))
                apTrasDto.unidadSalida.PorcentajeActual = CalcularGasServicio.ObtenerPorcentajeDesdeLitros(apTrasDto.unidadSalida.CapacidadTanqueLt.Value, apTrasDto.unidadSalida.CantidadActualLt);

            apTrasDto.AlmacenGasAnterior = ObtenerAlmacenGasTotal(apTrasDto.Empresa);
            apTrasDto.AlmacenGas = apTrasDto.AlmacenGasAnterior;

            AlmacenGasMovimiento ulMov = ObtenerUltimoMovimientoEnInventario(apTrasDto.Empresa.IdEmpresa, apTrasDto.AlmacenGas.IdAlmacenGas, apTrasDto.TraspasoLecturaFinal.FechaAplicacion);
            AlmacenGasMovimiento ulMovUnidadEntrada = ObtenerUltimoMovimientoPorUnidadAlmacenGas(apTrasDto.Empresa.IdEmpresa, apTrasDto.unidadEntrada.IdCAlmacenGas, apTrasDto.TraspasoLecturaFinal.FechaAplicacion);
            AlmacenGasMovimiento ulMovUnidadSalida = ObtenerUltimoMovimientoPorUnidadAlmacenGas(apTrasDto.Empresa.IdEmpresa, apTrasDto.unidadSalida.IdCAlmacenGas, apTrasDto.TraspasoLecturaFinal.FechaAplicacion);

            var invEntradaAnterior = new InventarioAnteriorDto
            {
                NombreOperador = OperadorChoferServicio.ObtenerNombreCompleto(apTrasDto.unidadEntrada),

                EntradaKg = KilosTraspasados,
                EntradaLt = LitrosTraspasados,
                CantidadAnteriorKg = ulMovUnidadEntrada.CantidadActualKg,
                CantidadAnteriorLt = ulMovUnidadEntrada.CantidadActualLt,
                PorcentajeAnterior = ulMovUnidadEntrada.PorcentajeActual,
                P5000Anterior = ulMovUnidadEntrada.P5000Actual,

                CAlmEntradaDiaKg = CalcularGasServicio.SumarKilogramos(ulMovUnidadEntrada.CAlmEntradaDiaKg, KilosTraspasados),
                CAlmEntradaDiaLt = CalcularGasServicio.SumarLitros(ulMovUnidadEntrada.CAlmEntradaDiaLt, LitrosTraspasados),
                CAlmEntradaMesKg = CalcularGasServicio.SumarKilogramos(ulMovUnidadEntrada.CAlmEntradaMesKg, KilosTraspasados),
                CAlmEntradaMesLt = CalcularGasServicio.SumarLitros(ulMovUnidadEntrada.CAlmEntradaMesLt, LitrosTraspasados),
                CAlmEntradaAnioKg = CalcularGasServicio.SumarKilogramos(ulMovUnidadEntrada.CAlmEntradaAnioKg, KilosTraspasados),
                CAlmEntradaAnioLt = CalcularGasServicio.SumarLitros(ulMovUnidadEntrada.CAlmEntradaAnioLt, LitrosTraspasados),
                CAlmSalidaDiaKg = ulMovUnidadEntrada.CAlmSalidaDiaKg,
                CAlmSalidaDiaLt = ulMovUnidadEntrada.CAlmSalidaDiaLt,
                CAlmSalidaMesKg = ulMovUnidadEntrada.CAlmSalidaMesKg,
                CAlmSalidaMesLt = ulMovUnidadEntrada.CAlmSalidaMesLt,
                CAlmSalidaAnioKg = ulMovUnidadEntrada.CAlmSalidaAnioKg,
                CAlmSalidaAnioLt = ulMovUnidadEntrada.CAlmSalidaAnioLt,
                CantidadAcumuladaDiaKg = CalcularGasServicio.SumarKilogramos(ulMovUnidadEntrada.CantidadAcumuladaDiaKg, KilosTraspasados),
                CantidadAcumuladaDiaLt = CalcularGasServicio.SumarLitros(ulMovUnidadEntrada.CantidadAcumuladaDiaLt, LitrosTraspasados),
                CantidadAcumuladaMesKg = CalcularGasServicio.SumarKilogramos(ulMovUnidadEntrada.CantidadAcumuladaMesKg, KilosTraspasados),
                CantidadAcumuladaMesLt = CalcularGasServicio.SumarLitros(ulMovUnidadEntrada.CantidadAcumuladaMesLt, LitrosTraspasados),
                CantidadAcumuladaAnioKg = CalcularGasServicio.SumarKilogramos(ulMovUnidadEntrada.CantidadAcumuladaAnioKg, KilosTraspasados),
                CantidadAcumuladaAnioLt = CalcularGasServicio.SumarLitros(ulMovUnidadEntrada.CantidadAcumuladaAnioLt, LitrosTraspasados),

                TraspasoKg = KilosTraspasados,
                TraspasoLt = LitrosTraspasados,
                TraspasoDiaKg = CalcularGasServicio.SumarKilogramos(ulMovUnidadEntrada.TraspasoDiaKg != null ? ulMovUnidadEntrada.TraspasoDiaKg.Value : 0, KilosTraspasados),
                TraspasoDiaLt = CalcularGasServicio.SumarLitros(ulMovUnidadEntrada.TraspasoDiaLt != null ? ulMovUnidadEntrada.TraspasoDiaLt.Value : 0, LitrosTraspasados),
                TraspasoMesKg = CalcularGasServicio.SumarKilogramos(ulMovUnidadEntrada.TraspasoMesKg != null ? ulMovUnidadEntrada.TraspasoMesKg.Value : 0, KilosTraspasados),
                TraspasoMesLt = CalcularGasServicio.SumarLitros(ulMovUnidadEntrada.TraspasoMesLt != null ? ulMovUnidadEntrada.TraspasoMesLt.Value : 0, LitrosTraspasados),
                TraspasoAnioKg = CalcularGasServicio.SumarKilogramos(ulMovUnidadEntrada.TraspasoAnioKg != null ? ulMovUnidadEntrada.TraspasoAnioKg.Value : 0, KilosTraspasados),
                TraspasoAnioLt = CalcularGasServicio.SumarLitros(ulMovUnidadEntrada.TraspasoAnioLt != null ? ulMovUnidadEntrada.TraspasoAnioLt.Value : 0, LitrosTraspasados),
                TraspasoAcumDiaKg = CalcularGasServicio.SumarKilogramos(ulMov.TraspasoAcumDiaKg, KilosTraspasados),
                TraspasoAcumDiaLt = CalcularGasServicio.SumarLitros(ulMov.TraspasoAcumDiaLt, LitrosTraspasados),
                TraspasoAcumMesKg = CalcularGasServicio.SumarKilogramos(ulMov.TraspasoAcumMesKg, KilosTraspasados),
                TraspasoAcumMesLt = CalcularGasServicio.SumarLitros(ulMov.TraspasoAcumMesLt, LitrosTraspasados),
                TraspasoAcumAnioKg = CalcularGasServicio.SumarKilogramos(ulMov.TraspasoAcumAnioKg, KilosTraspasados),
                TraspasoAcumAnioLt = CalcularGasServicio.SumarLitros(ulMov.TraspasoAcumAnioLt, LitrosTraspasados),

                CantidadAnteriorTotalKg = apTrasDto.AlmacenGasAnterior.CapacidadTotalKg,
                CantidadAnteriorTotalLt = apTrasDto.AlmacenGasAnterior.CapacidadTotalLt,
                PorcentajeAnteriorTotal = apTrasDto.AlmacenGasAnterior.PorcentajeActual,
                CantidadAnteriorGeneralKg = apTrasDto.AlmacenGasAnterior.CantidadActualGeneralKg,
                CantidadAnteriorGeneralLt = apTrasDto.AlmacenGasAnterior.CantidadActualGeneralLt,
                PorcentajeAnteriorGeneral = apTrasDto.AlmacenGasAnterior.PorcentajeActualGeneral,
            };

            var invSalidaAnterior = new InventarioAnteriorDto
            {
                NombreOperador = OperadorChoferServicio.ObtenerNombreCompleto(apTrasDto.unidadSalida),

                SalidaKg = KilosTraspasados,
                SalidaLt = LitrosTraspasados,
                CantidadAnteriorKg = ulMovUnidadSalida.CantidadActualKg,
                CantidadAnteriorLt = ulMovUnidadSalida.CantidadActualLt,
                PorcentajeAnterior = ulMovUnidadSalida.PorcentajeActual,
                P5000Anterior = ulMovUnidadSalida.P5000Actual,

                CAlmSalidaDiaKg = CalcularGasServicio.SumarKilogramos(ulMovUnidadSalida.CAlmSalidaDiaKg, KilosTraspasados),
                CAlmSalidaDiaLt = CalcularGasServicio.SumarLitros(ulMovUnidadSalida.CAlmSalidaDiaLt, LitrosTraspasados),
                CAlmSalidaMesKg = CalcularGasServicio.SumarKilogramos(ulMovUnidadSalida.CAlmSalidaMesKg, KilosTraspasados),
                CAlmSalidaMesLt = CalcularGasServicio.SumarLitros(ulMovUnidadSalida.CAlmSalidaMesLt, LitrosTraspasados),
                CAlmSalidaAnioKg = CalcularGasServicio.SumarKilogramos(ulMovUnidadSalida.CAlmSalidaAnioKg, KilosTraspasados),
                CAlmSalidaAnioLt = CalcularGasServicio.SumarLitros(ulMovUnidadSalida.CAlmSalidaAnioLt, LitrosTraspasados),
                CAlmEntradaDiaKg = ulMovUnidadSalida.CAlmEntradaDiaKg,
                CAlmEntradaDiaLt = ulMovUnidadSalida.CAlmEntradaDiaLt,
                CAlmEntradaMesKg = ulMovUnidadSalida.CAlmEntradaMesKg,
                CAlmEntradaMesLt = ulMovUnidadSalida.CAlmEntradaMesLt,
                CAlmEntradaAnioKg = ulMovUnidadSalida.CAlmEntradaAnioKg,
                CAlmEntradaAnioLt = ulMovUnidadSalida.CAlmEntradaAnioLt,
                CantidadAcumuladaDiaKg = CalcularGasServicio.SumarKilogramos(ulMovUnidadSalida.CantidadAcumuladaDiaKg, KilosTraspasados),
                CantidadAcumuladaDiaLt = CalcularGasServicio.SumarLitros(ulMovUnidadSalida.CantidadAcumuladaDiaLt, LitrosTraspasados),
                CantidadAcumuladaMesKg = CalcularGasServicio.SumarKilogramos(ulMovUnidadSalida.CantidadAcumuladaMesKg, KilosTraspasados),
                CantidadAcumuladaMesLt = CalcularGasServicio.SumarLitros(ulMovUnidadSalida.CantidadAcumuladaMesLt, LitrosTraspasados),
                CantidadAcumuladaAnioKg = CalcularGasServicio.SumarKilogramos(ulMovUnidadSalida.CantidadAcumuladaAnioKg, KilosTraspasados),
                CantidadAcumuladaAnioLt = CalcularGasServicio.SumarLitros(ulMovUnidadSalida.CantidadAcumuladaAnioLt, LitrosTraspasados),

                TraspasoKg = KilosTraspasados,
                TraspasoLt = LitrosTraspasados,
                TraspasoDiaKg = CalcularGasServicio.SumarKilogramos(ulMovUnidadSalida.TraspasoDiaKg != null ? ulMovUnidadSalida.TraspasoDiaKg.Value : 0, KilosTraspasados),
                TraspasoDiaLt = CalcularGasServicio.SumarLitros(ulMovUnidadSalida.TraspasoDiaLt != null ? ulMovUnidadSalida.TraspasoDiaLt.Value : 0, LitrosTraspasados),
                TraspasoMesKg = CalcularGasServicio.SumarKilogramos(ulMovUnidadSalida.TraspasoMesKg != null ? ulMovUnidadSalida.TraspasoMesKg.Value : 0, KilosTraspasados),
                TraspasoMesLt = CalcularGasServicio.SumarLitros(ulMovUnidadSalida.TraspasoMesLt != null ? ulMovUnidadSalida.TraspasoMesLt.Value : 0, LitrosTraspasados),
                TraspasoAnioKg = CalcularGasServicio.SumarKilogramos(ulMovUnidadSalida.TraspasoAnioKg != null ? ulMovUnidadSalida.TraspasoAnioKg.Value : 0, KilosTraspasados),
                TraspasoAnioLt = CalcularGasServicio.SumarLitros(ulMovUnidadSalida.TraspasoAnioLt != null ? ulMovUnidadSalida.TraspasoAnioLt.Value : 0, LitrosTraspasados),
                TraspasoAcumDiaKg = CalcularGasServicio.SumarKilogramos(ulMov.TraspasoAcumDiaKg, KilosTraspasados),
                TraspasoAcumDiaLt = CalcularGasServicio.SumarLitros(ulMov.TraspasoAcumDiaLt, LitrosTraspasados),
                TraspasoAcumMesKg = CalcularGasServicio.SumarKilogramos(ulMov.TraspasoAcumMesKg, KilosTraspasados),
                TraspasoAcumMesLt = CalcularGasServicio.SumarLitros(ulMov.TraspasoAcumMesLt, LitrosTraspasados),
                TraspasoAcumAnioKg = CalcularGasServicio.SumarKilogramos(ulMov.TraspasoAcumAnioKg, KilosTraspasados),
                TraspasoAcumAnioLt = CalcularGasServicio.SumarLitros(ulMov.TraspasoAcumAnioLt, LitrosTraspasados),

                CantidadAnteriorTotalKg = apTrasDto.AlmacenGasAnterior.CapacidadTotalKg,
                CantidadAnteriorTotalLt = apTrasDto.AlmacenGasAnterior.CapacidadTotalLt,
                PorcentajeAnteriorTotal = apTrasDto.AlmacenGasAnterior.PorcentajeActual,
                CantidadAnteriorGeneralKg = apTrasDto.AlmacenGasAnterior.CantidadActualGeneralKg,
                CantidadAnteriorGeneralLt = apTrasDto.AlmacenGasAnterior.CantidadActualGeneralLt,
                PorcentajeAnteriorGeneral = apTrasDto.AlmacenGasAnterior.PorcentajeActualGeneral,
            };

            apTrasDto.MovimientoEntrada = AlmacenGasAdapter.FromEntity(apTrasDto.unidadEntrada, apTrasDto.TraspasoLecturaFinal, apTrasDto.AlmacenGas, ulMovUnidadEntrada, apTrasDto.Empresa, invEntradaAnterior, apTrasDto.unidadSalida.IdCAlmacenGas, apTrasDto.unidadSalida.Numero, true);
            apTrasDto.MovimientoSalida = AlmacenGasAdapter.FromEntity(apTrasDto.unidadSalida, apTrasDto.TraspasoLecturaFinal, apTrasDto.AlmacenGas, ulMovUnidadSalida, apTrasDto.Empresa, invSalidaAnterior, apTrasDto.unidadEntrada.IdCAlmacenGas, apTrasDto.unidadEntrada.Numero, false);

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
                    if (img != null)
                    {
                        var foto = AlmacenGasAdapter.FromEntity(img);
                        fotos.Add(foto);
                    }                   
                  
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
        public static AplicaAutoConsumoDto AplicarAutoConsumoProceso(AplicaAutoConsumoDto apAutoDto)
        {
            apAutoDto.AutoConsumoLecturaFinal = apAutoDto.AutoConsumosFinales.FirstOrDefault(x => x.IdCAlmacenGasEntrada.Equals(apAutoDto.AutoConsumoLecturaInicial.IdCAlmacenGasEntrada));

            if (apAutoDto.AutoConsumoLecturaFinal == null)
                return new AplicaAutoConsumoDto();

            decimal LitrosCarburados = CalcularGasServicio.ObtenerDiferenciaLecturaP5000(apAutoDto.AutoConsumoLecturaFinal.P5000Salida, apAutoDto.AutoConsumoLecturaInicial.P5000Salida);

            if (apAutoDto.identidadUS != identidadUnidadAlmacenGas.EstacionCarburacion || apAutoDto.identidadUS != identidadUnidadAlmacenGas.Pipa)
                LitrosCarburados = CalcularGasServicio.RestarLitrosDesdePorcentaje(LitrosCarburados, apAutoDto.unidadSalida.PorcentajeCalibracionPlaneada);

            decimal KilosCarburados = CalcularGasServicio.ObtenerKilogramosDesdeLitros(LitrosCarburados, apAutoDto.Empresa.FactorLitrosAKilos);
            apAutoDto = AplicarAutoConsumo(apAutoDto, LitrosCarburados, KilosCarburados);
            return apAutoDto;
        }
        public static AplicaAutoConsumoDto AplicarAutoConsumo(AplicaAutoConsumoDto apAutoDto, decimal LitrosCarburados, decimal KilosCarburados)
        {
            apAutoDto.unidadSalida.CantidadActualLt = CalcularGasServicio.RestarLitros(apAutoDto.unidadSalida.CantidadActualLt, LitrosCarburados);
            apAutoDto.unidadSalida.CantidadActualKg = CalcularGasServicio.RestarKilogramos(apAutoDto.unidadSalida.CantidadActualKg, KilosCarburados);
            apAutoDto.unidadSalida.PorcentajeActual = CalcularGasServicio.ObtenerPorcentajeDesdeLitros(apAutoDto.unidadSalida.CapacidadTanqueLt.Value, apAutoDto.unidadSalida.CantidadActualLt);
            apAutoDto.unidadSalida.P5000Actual = apAutoDto.AutoConsumoLecturaFinal.P5000Salida;

            apAutoDto.AlmacenGasAnterior = ObtenerAlmacenGasTotal(apAutoDto.Empresa);
            apAutoDto = AplicarAutoConsumoAlmacenTotal(apAutoDto, LitrosCarburados, KilosCarburados);

            AlmacenGasMovimiento ulMov = ObtenerUltimoMovimientoEnInventario(apAutoDto.Empresa.IdEmpresa, apAutoDto.AlmacenGas.IdAlmacenGas, apAutoDto.AutoConsumoLecturaFinal.FechaAplicacion);
            AlmacenGasMovimiento ulMovUnidadSalida = ObtenerUltimoMovimientoPorUnidadAlmacenGas(apAutoDto.Empresa.IdEmpresa, apAutoDto.unidadSalida.IdCAlmacenGas, apAutoDto.AutoConsumoLecturaFinal.FechaAplicacion);

            var invSalidaAnterior = new InventarioAnteriorDto
            {
                NombreOperador = OperadorChoferServicio.ObtenerNombreCompleto(apAutoDto.unidadSalida),

                SalidaKg = KilosCarburados,
                SalidaLt = LitrosCarburados,
                CantidadAnteriorKg = ulMovUnidadSalida.CantidadActualKg,
                CantidadAnteriorLt = ulMovUnidadSalida.CantidadActualLt,
                PorcentajeAnterior = ulMovUnidadSalida.PorcentajeActual,
                P5000Anterior = ulMovUnidadSalida.P5000Actual,

                CAlmSalidaDiaKg = CalcularGasServicio.SumarKilogramos(ulMovUnidadSalida.CAlmSalidaDiaKg, KilosCarburados),
                CAlmSalidaDiaLt = CalcularGasServicio.SumarLitros(ulMovUnidadSalida.CAlmSalidaDiaLt, LitrosCarburados),
                CAlmSalidaMesKg = CalcularGasServicio.SumarKilogramos(ulMovUnidadSalida.CAlmSalidaMesKg, KilosCarburados),
                CAlmSalidaMesLt = CalcularGasServicio.SumarLitros(ulMovUnidadSalida.CAlmSalidaMesLt, LitrosCarburados),
                CAlmSalidaAnioKg = CalcularGasServicio.SumarKilogramos(ulMovUnidadSalida.CAlmSalidaAnioKg, KilosCarburados),
                CAlmSalidaAnioLt = CalcularGasServicio.SumarLitros(ulMovUnidadSalida.CAlmSalidaAnioLt, LitrosCarburados),
                CAlmEntradaDiaKg = ulMovUnidadSalida.CAlmEntradaDiaKg,
                CAlmEntradaDiaLt = ulMovUnidadSalida.CAlmEntradaDiaLt,
                CAlmEntradaMesKg = ulMovUnidadSalida.CAlmEntradaMesKg,
                CAlmEntradaMesLt = ulMovUnidadSalida.CAlmEntradaMesLt,
                CAlmEntradaAnioKg = ulMovUnidadSalida.CAlmEntradaAnioKg,
                CAlmEntradaAnioLt = ulMovUnidadSalida.CAlmEntradaAnioLt,
                CantidadAcumuladaDiaKg = CalcularGasServicio.SumarKilogramos(ulMovUnidadSalida.CantidadAcumuladaDiaKg, KilosCarburados),
                CantidadAcumuladaDiaLt = CalcularGasServicio.SumarLitros(ulMovUnidadSalida.CantidadAcumuladaDiaLt, LitrosCarburados),
                CantidadAcumuladaMesKg = CalcularGasServicio.SumarKilogramos(ulMovUnidadSalida.CantidadAcumuladaMesKg, KilosCarburados),
                CantidadAcumuladaMesLt = CalcularGasServicio.SumarLitros(ulMovUnidadSalida.CantidadAcumuladaMesLt, LitrosCarburados),
                CantidadAcumuladaAnioKg = CalcularGasServicio.SumarKilogramos(ulMovUnidadSalida.CantidadAcumuladaAnioKg, KilosCarburados),
                CantidadAcumuladaAnioLt = CalcularGasServicio.SumarLitros(ulMovUnidadSalida.CantidadAcumuladaAnioLt, LitrosCarburados),

                AutoconsumoKg = KilosCarburados,
                AutoconsumoLt = LitrosCarburados,
                AutoconsumoDiaKg = CalcularGasServicio.SumarKilogramos(ulMovUnidadSalida.AutoconsumoDiaKg != null ? ulMovUnidadSalida.AutoconsumoDiaKg.Value : 0, KilosCarburados),
                AutoconsumoDiaLt = CalcularGasServicio.SumarLitros(ulMovUnidadSalida.AutoconsumoDiaLt != null ? ulMovUnidadSalida.AutoconsumoDiaLt.Value : 0, LitrosCarburados),
                AutoconsumoMesKg = CalcularGasServicio.SumarKilogramos(ulMovUnidadSalida.AutoconsumoMesKg != null ? ulMovUnidadSalida.AutoconsumoMesKg.Value : 0, KilosCarburados),
                AutoconsumoMesLt = CalcularGasServicio.SumarLitros(ulMovUnidadSalida.AutoconsumoMesLt != null ? ulMovUnidadSalida.AutoconsumoMesLt.Value : 0, LitrosCarburados),
                AutoconsumoAnioKg = CalcularGasServicio.SumarKilogramos(ulMovUnidadSalida.AutoconsumoAnioKg != null ? ulMovUnidadSalida.AutoconsumoAnioKg.Value : 0, KilosCarburados),
                AutoconsumoAnioLt = CalcularGasServicio.SumarLitros(ulMovUnidadSalida.AutoconsumoAnioLt != null ? ulMovUnidadSalida.AutoconsumoAnioLt.Value : 0, LitrosCarburados),
                AutoconsumoAcumDiaKg = CalcularGasServicio.SumarKilogramos(ulMov.AutoconsumoAcumDiaKg, KilosCarburados),
                AutoconsumoAcumDiaLt = CalcularGasServicio.SumarLitros(ulMov.AutoconsumoAcumDiaLt, LitrosCarburados),
                AutoconsumoAcumMesKg = CalcularGasServicio.SumarKilogramos(ulMov.AutoconsumoAcumMesKg, KilosCarburados),
                AutoconsumoAcumMesLt = CalcularGasServicio.SumarLitros(ulMov.AutoconsumoAcumMesLt, LitrosCarburados),
                AutoconsumoAcumAnioKg = CalcularGasServicio.SumarKilogramos(ulMov.AutoconsumoAcumAnioKg, KilosCarburados),
                AutoconsumoAcumAnioLt = CalcularGasServicio.SumarLitros(ulMov.AutoconsumoAcumAnioLt, LitrosCarburados),

                CantidadAnteriorTotalKg = apAutoDto.AlmacenGasAnterior.CapacidadTotalKg,
                CantidadAnteriorTotalLt = apAutoDto.AlmacenGasAnterior.CapacidadTotalLt,
                PorcentajeAnteriorTotal = apAutoDto.AlmacenGasAnterior.PorcentajeActual,
                CantidadAnteriorGeneralKg = apAutoDto.AlmacenGasAnterior.CantidadActualGeneralKg,
                CantidadAnteriorGeneralLt = apAutoDto.AlmacenGasAnterior.CantidadActualGeneralLt,
                PorcentajeAnteriorGeneral = apAutoDto.AlmacenGasAnterior.PorcentajeActualGeneral,
            };

            apAutoDto.MovimientoSalida = AlmacenGasAdapter.FromEntity(apAutoDto.unidadSalida, apAutoDto.AutoConsumoLecturaFinal, apAutoDto.AlmacenGas, ulMovUnidadSalida, apAutoDto.Empresa, invSalidaAnterior, apAutoDto.unidadEntrada.IdCAlmacenGas, apAutoDto.unidadEntrada.Numero);
            apAutoDto.unidadEntrada = apAutoDto.unidadEntrada.Equals(apAutoDto.unidadSalida) ? null : AlmacenGasAdapter.FromEntity(apAutoDto.unidadEntrada);
            apAutoDto.unidadSalida = AlmacenGasAdapter.FromEntity(apAutoDto.unidadSalida);
            apAutoDto.AutoConsumoLecturaInicialFotos = GenerarImagenes(apAutoDto.AutoConsumoLecturaInicial);
            apAutoDto.AutoConsumoLecturaInicial.DatosProcesados = true;
            apAutoDto.AutoConsumoLecturaInicialSinNavProp = AlmacenGasAdapter.FromEntity(apAutoDto.AutoConsumoLecturaInicial);
            apAutoDto.AutoConsumoLecturaFinalFotos = GenerarImagenes(apAutoDto.AutoConsumoLecturaFinal);
            apAutoDto.AutoConsumoLecturaFinal.DatosProcesados = true;
            apAutoDto.AutoConsumoLecturaFinalSinNavProp = AlmacenGasAdapter.FromEntity(apAutoDto.AutoConsumoLecturaFinal);
            return apAutoDto;
        }
        public static AplicaAutoConsumoDto AplicarAutoConsumoAlmacenTotal(AplicaAutoConsumoDto apAutDto, decimal litrosCarburado, decimal kilogramosCarburados)
        {
            apAutDto.AlmacenGas = apAutDto.AlmacenGasAnterior;
            if (apAutDto.identidadUS.Equals(identidadUnidadAlmacenGas.AlmacenPrincipal) || apAutDto.identidadUS.Equals(identidadUnidadAlmacenGas.AlmacenAlterno))
            {
                apAutDto.AlmacenGas.CantidadActualLt = CalcularGasServicio.RestarLitros(apAutDto.AlmacenGasAnterior.CantidadActualLt, litrosCarburado);
                apAutDto.AlmacenGas.CantidadActualKg = CalcularGasServicio.RestarKilogramos(apAutDto.AlmacenGasAnterior.CantidadActualKg, kilogramosCarburados);
                apAutDto.AlmacenGas.PorcentajeActual = CalcularGasServicio.ObtenerPorcentajeDesdeLitros(apAutDto.AlmacenGasAnterior.CapacidadTotalLt, apAutDto.AlmacenGasAnterior.CantidadActualLt);

                apAutDto.AlmacenGas.CantidadActualGeneralLt = CalcularGasServicio.RestarLitros(apAutDto.AlmacenGasAnterior.CantidadActualGeneralLt, litrosCarburado);
                apAutDto.AlmacenGas.CantidadActualGeneralKg = CalcularGasServicio.RestarKilogramos(apAutDto.AlmacenGasAnterior.CantidadActualGeneralKg, kilogramosCarburados);
                apAutDto.AlmacenGas.PorcentajeActualGeneral = CalcularGasServicio.ObtenerPorcentajeDesdeLitros(apAutDto.AlmacenGasAnterior.CapacidadGeneralLt, apAutDto.AlmacenGasAnterior.CantidadActualGeneralLt);
            }
            return apAutDto;
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
            InventarioAnteriorDto invEntradaAnterior;
            decimal litrosARegistrar = 0, kilosARegistrar = 0;

            apCaliDto.AlmacenGasAnterior = ObtenerAlmacenGasTotal(apCaliDto.Empresa);
            apCaliDto.AlmacenGas = apCaliDto.AlmacenGasAnterior;

            AlmacenGasMovimiento ulMov = ObtenerUltimoMovimientoEnInventario(apCaliDto.Empresa.IdEmpresa, apCaliDto.AlmacenGas.IdAlmacenGas, apCaliDto.CalibracionLecturaFinal.FechaAplicacion.Value);

            if (apCaliDto.CalibracionLecturaFinal.IdDestinoCalibracion.Equals(CalibracionDestinoEnum.TanquePortatil))
            {
                apCaliDto.unidadAlmacenGasPrincipal = ObtenerAlmacenGeneral(apCaliDto.Empresa.IdEmpresa).FirstOrDefault();
                apCaliDto.unidadAlmacenGasPrincipal.CantidadActualLt = CalcularGasServicio.SumarLitros(apCaliDto.unidadAlmacenGasPrincipal.CantidadActualLt, Litros);
                apCaliDto.unidadAlmacenGasPrincipal.CantidadActualKg = CalcularGasServicio.SumarKilogramos(apCaliDto.unidadAlmacenGasPrincipal.CantidadActualKg, Kilos);

                apCaliDto.unidadAlmacenGas.CantidadActualLt = CalcularGasServicio.RestarLitros(apCaliDto.unidadAlmacenGas.CantidadActualLt, Litros);
                apCaliDto.unidadAlmacenGas.CantidadActualKg = CalcularGasServicio.RestarKilogramos(apCaliDto.unidadAlmacenGas.CantidadActualKg, Kilos);
                apCaliDto.unidadAlmacenGas.PorcentajeActual = apCaliDto.CalibracionLecturaFinal.Porcentaje;

                apCaliDto.AlmacenGas.CantidadActualGeneralLt = CalcularGasServicio.SumarLitros(apCaliDto.AlmacenGasAnterior.CantidadActualGeneralLt, Litros);
                apCaliDto.AlmacenGas.CantidadActualGeneralKg = CalcularGasServicio.SumarLitros(apCaliDto.AlmacenGasAnterior.CantidadActualGeneralKg, Kilos);
                apCaliDto.AlmacenGas.PorcentajeActualGeneral = CalcularGasServicio.ObtenerPorcentajeDesdeLitros(apCaliDto.AlmacenGas.CapacidadGeneralLt, apCaliDto.AlmacenGas.CantidadActualGeneralLt);

                litrosARegistrar = Litros; kilosARegistrar = Kilos;

                AlmacenGasMovimiento ulMovUnidadEntrada = ObtenerUltimoMovimientoPorUnidadAlmacenGas(apCaliDto.Empresa.IdEmpresa, apCaliDto.unidadAlmacenGasPrincipal.IdCAlmacenGas, apCaliDto.CalibracionLecturaFinal.FechaAplicacion.Value);

                invEntradaAnterior = new InventarioAnteriorDto
                {
                    NombreOperador = OperadorChoferServicio.ObtenerNombreCompleto(apCaliDto.unidadAlmacenGasPrincipal),

                    EntradaKg = kilosARegistrar,
                    EntradaLt = litrosARegistrar,
                    CantidadAnteriorKg = ulMovUnidadEntrada.CantidadActualKg,
                    CantidadAnteriorLt = ulMovUnidadEntrada.CantidadActualLt,
                    PorcentajeAnterior = ulMovUnidadEntrada.PorcentajeActual,
                    P5000Anterior = ulMovUnidadEntrada.P5000Actual,

                    CAlmEntradaDiaKg = CalcularGasServicio.SumarKilogramos(ulMovUnidadEntrada.CAlmEntradaDiaKg, kilosARegistrar),
                    CAlmEntradaDiaLt = CalcularGasServicio.SumarLitros(ulMovUnidadEntrada.CAlmEntradaDiaLt, litrosARegistrar),
                    CAlmEntradaMesKg = CalcularGasServicio.SumarKilogramos(ulMovUnidadEntrada.CAlmEntradaMesKg, kilosARegistrar),
                    CAlmEntradaMesLt = CalcularGasServicio.SumarLitros(ulMovUnidadEntrada.CAlmEntradaMesLt, litrosARegistrar),
                    CAlmEntradaAnioKg = CalcularGasServicio.SumarKilogramos(ulMovUnidadEntrada.CAlmEntradaAnioKg, kilosARegistrar),
                    CAlmEntradaAnioLt = CalcularGasServicio.SumarLitros(ulMovUnidadEntrada.CAlmEntradaAnioLt, litrosARegistrar),
                    CAlmSalidaDiaKg = ulMovUnidadEntrada.CAlmSalidaDiaKg,
                    CAlmSalidaDiaLt = ulMovUnidadEntrada.CAlmSalidaDiaLt,
                    CAlmSalidaMesKg = ulMovUnidadEntrada.CAlmSalidaMesKg,
                    CAlmSalidaMesLt = ulMovUnidadEntrada.CAlmSalidaMesLt,
                    CAlmSalidaAnioKg = ulMovUnidadEntrada.CAlmSalidaAnioKg,
                    CAlmSalidaAnioLt = ulMovUnidadEntrada.CAlmSalidaAnioLt,
                    CantidadAcumuladaDiaKg = CalcularGasServicio.SumarKilogramos(ulMovUnidadEntrada.CantidadAcumuladaDiaKg, kilosARegistrar),
                    CantidadAcumuladaDiaLt = CalcularGasServicio.SumarLitros(ulMovUnidadEntrada.CantidadAcumuladaDiaLt, litrosARegistrar),
                    CantidadAcumuladaMesKg = CalcularGasServicio.SumarKilogramos(ulMovUnidadEntrada.CantidadAcumuladaMesKg, kilosARegistrar),
                    CantidadAcumuladaMesLt = CalcularGasServicio.SumarLitros(ulMovUnidadEntrada.CantidadAcumuladaMesLt, litrosARegistrar),
                    CantidadAcumuladaAnioKg = CalcularGasServicio.SumarKilogramos(ulMovUnidadEntrada.CantidadAcumuladaAnioKg, kilosARegistrar),
                    CantidadAcumuladaAnioLt = CalcularGasServicio.SumarLitros(ulMovUnidadEntrada.CantidadAcumuladaAnioLt, litrosARegistrar),

                    CalibracionKg = kilosARegistrar,
                    CalibracionLt = litrosARegistrar,
                    CalibracionDiaKg = CalcularGasServicio.SumarKilogramos(ulMovUnidadEntrada.CalibracionDiaKg != null ? ulMovUnidadEntrada.CalibracionDiaKg.Value : 0, kilosARegistrar),
                    CalibracionDiaLt = CalcularGasServicio.SumarLitros(ulMovUnidadEntrada.CalibracionDiaLt != null ? ulMovUnidadEntrada.CalibracionDiaLt.Value : 0, litrosARegistrar),
                    CalibracionMesKg = CalcularGasServicio.SumarKilogramos(ulMovUnidadEntrada.CalibracionMesKg != null ? ulMovUnidadEntrada.CalibracionMesKg.Value : 0, kilosARegistrar),
                    CalibracionMesLt = CalcularGasServicio.SumarLitros(ulMovUnidadEntrada.CalibracionMesLt != null ? ulMovUnidadEntrada.CalibracionMesLt.Value : 0, litrosARegistrar),
                    CalibracionAnioKg = CalcularGasServicio.SumarKilogramos(ulMovUnidadEntrada.CalibracionAnioKg != null ? ulMovUnidadEntrada.CalibracionAnioKg.Value : 0, kilosARegistrar),
                    CalibracionAnioLt = CalcularGasServicio.SumarLitros(ulMovUnidadEntrada.CalibracionAnioLt != null ? ulMovUnidadEntrada.CalibracionAnioLt.Value : 0, litrosARegistrar),
                    CalibracionAcumDiaKg = CalcularGasServicio.SumarKilogramos(ulMov.CalibracionAcumDiaKg, kilosARegistrar),
                    CalibracionAcumDiaLt = CalcularGasServicio.SumarLitros(ulMov.CalibracionAcumDiaLt, litrosARegistrar),
                    CalibracionAcumMesKg = CalcularGasServicio.SumarKilogramos(ulMov.CalibracionAcumMesKg, kilosARegistrar),
                    CalibracionAcumMesLt = CalcularGasServicio.SumarLitros(ulMov.CalibracionAcumMesLt, litrosARegistrar),
                    CalibracionAcumAnioKg = CalcularGasServicio.SumarKilogramos(ulMov.CalibracionAcumAnioKg, kilosARegistrar),
                    CalibracionAcumAnioLt = CalcularGasServicio.SumarLitros(ulMov.CalibracionAcumAnioLt, litrosARegistrar),

                    CantidadAnteriorTotalKg = apCaliDto.AlmacenGasAnterior.CapacidadTotalKg,
                    CantidadAnteriorTotalLt = apCaliDto.AlmacenGasAnterior.CapacidadTotalLt,
                    PorcentajeAnteriorTotal = apCaliDto.AlmacenGasAnterior.PorcentajeActual,
                    CantidadAnteriorGeneralKg = apCaliDto.AlmacenGasAnterior.CantidadActualGeneralKg,
                    CantidadAnteriorGeneralLt = apCaliDto.AlmacenGasAnterior.CantidadActualGeneralLt,
                    PorcentajeAnteriorGeneral = apCaliDto.AlmacenGasAnterior.PorcentajeActualGeneral,
                };

                apCaliDto.MovimientoEntrada = AlmacenGasAdapter.FromEntity(apCaliDto.unidadAlmacenGasPrincipal, apCaliDto.CalibracionLecturaFinal, apCaliDto.AlmacenGas, ulMovUnidadEntrada, apCaliDto.Empresa, invEntradaAnterior, true, apCaliDto.unidadAlmacenGas.IdCAlmacenGas, apCaliDto.unidadAlmacenGas.Numero);
            }

            AlmacenGasMovimiento ulMovSalida = ObtenerUltimoMovimientoPorUnidadAlmacenGas(apCaliDto.Empresa.IdEmpresa, apCaliDto.unidadAlmacenGas.IdCAlmacenGas, apCaliDto.CalibracionLecturaFinal.FechaAplicacion.Value);

            var invSalidaAnterior = new InventarioAnteriorDto
            {
                NombreOperador = OperadorChoferServicio.ObtenerNombreCompleto(apCaliDto.unidadAlmacenGas),

                SalidaKg = kilosARegistrar,
                SalidaLt = litrosARegistrar,
                CantidadAnteriorKg = ulMovSalida.CantidadActualKg,
                CantidadAnteriorLt = ulMovSalida.CantidadActualLt,
                PorcentajeAnterior = ulMovSalida.PorcentajeActual,
                P5000Anterior = ulMovSalida.P5000Actual,

                CAlmSalidaDiaKg = CalcularGasServicio.SumarKilogramos(ulMovSalida.CAlmSalidaDiaKg, kilosARegistrar),
                CAlmSalidaDiaLt = CalcularGasServicio.SumarLitros(ulMovSalida.CAlmSalidaDiaLt, litrosARegistrar),
                CAlmSalidaMesKg = CalcularGasServicio.SumarKilogramos(ulMovSalida.CAlmSalidaMesKg, kilosARegistrar),
                CAlmSalidaMesLt = CalcularGasServicio.SumarLitros(ulMovSalida.CAlmSalidaMesLt, litrosARegistrar),
                CAlmSalidaAnioKg = CalcularGasServicio.SumarKilogramos(ulMovSalida.CAlmSalidaAnioKg, kilosARegistrar),
                CAlmSalidaAnioLt = CalcularGasServicio.SumarLitros(ulMovSalida.CAlmSalidaAnioLt, litrosARegistrar),
                CAlmEntradaDiaKg = ulMovSalida.CAlmEntradaDiaKg,
                CAlmEntradaDiaLt = ulMovSalida.CAlmEntradaDiaLt,
                CAlmEntradaMesKg = ulMovSalida.CAlmEntradaMesKg,
                CAlmEntradaMesLt = ulMovSalida.CAlmEntradaMesLt,
                CAlmEntradaAnioKg = ulMovSalida.CAlmEntradaAnioKg,
                CAlmEntradaAnioLt = ulMovSalida.CAlmEntradaAnioLt,
                CantidadAcumuladaDiaKg = CalcularGasServicio.SumarKilogramos(ulMovSalida.CantidadAcumuladaDiaKg, kilosARegistrar),
                CantidadAcumuladaDiaLt = CalcularGasServicio.SumarLitros(ulMovSalida.CantidadAcumuladaDiaLt, litrosARegistrar),
                CantidadAcumuladaMesKg = CalcularGasServicio.SumarKilogramos(ulMovSalida.CantidadAcumuladaMesKg, kilosARegistrar),
                CantidadAcumuladaMesLt = CalcularGasServicio.SumarLitros(ulMovSalida.CantidadAcumuladaMesLt, litrosARegistrar),
                CantidadAcumuladaAnioKg = CalcularGasServicio.SumarKilogramos(ulMovSalida.CantidadAcumuladaAnioKg, kilosARegistrar),
                CantidadAcumuladaAnioLt = CalcularGasServicio.SumarLitros(ulMovSalida.CantidadAcumuladaAnioLt, litrosARegistrar),

                CalibracionKg = Kilos,
                CalibracionLt = Litros,
                CalibracionDiaKg = CalcularGasServicio.SumarKilogramos(ulMovSalida.CalibracionDiaKg != null ? ulMovSalida.CalibracionDiaKg.Value : 0, Kilos),
                CalibracionDiaLt = CalcularGasServicio.SumarLitros(ulMovSalida.CalibracionDiaLt != null ? ulMovSalida.CalibracionDiaLt.Value : 0, Litros),
                CalibracionMesKg = CalcularGasServicio.SumarKilogramos(ulMovSalida.CalibracionMesKg != null ? ulMovSalida.CalibracionMesKg.Value : 0, Kilos),
                CalibracionMesLt = CalcularGasServicio.SumarLitros(ulMovSalida.CalibracionMesLt != null ? ulMovSalida.CalibracionMesLt.Value : 0, Litros),
                CalibracionAnioKg = CalcularGasServicio.SumarKilogramos(ulMovSalida.CalibracionAnioKg != null ? ulMovSalida.CalibracionAnioKg.Value : 0, Kilos),
                CalibracionAnioLt = CalcularGasServicio.SumarLitros(ulMovSalida.CalibracionAnioLt != null ? ulMovSalida.CalibracionAnioLt.Value : 0, Litros),
                CalibracionAcumDiaKg = CalcularGasServicio.SumarKilogramos(ulMov.CalibracionAcumDiaKg, Kilos),
                CalibracionAcumDiaLt = CalcularGasServicio.SumarLitros(ulMov.CalibracionAcumDiaLt, Litros),
                CalibracionAcumMesKg = CalcularGasServicio.SumarKilogramos(ulMov.CalibracionAcumMesKg, Kilos),
                CalibracionAcumMesLt = CalcularGasServicio.SumarLitros(ulMov.CalibracionAcumMesLt, Litros),
                CalibracionAcumAnioKg = CalcularGasServicio.SumarKilogramos(ulMov.CalibracionAcumAnioKg, Kilos),
                CalibracionAcumAnioLt = CalcularGasServicio.SumarLitros(ulMov.CalibracionAcumAnioLt, Litros),

                CantidadAnteriorTotalKg = apCaliDto.AlmacenGasAnterior.CapacidadTotalKg,
                CantidadAnteriorTotalLt = apCaliDto.AlmacenGasAnterior.CapacidadTotalLt,
                PorcentajeAnteriorTotal = apCaliDto.AlmacenGasAnterior.PorcentajeActual,
                CantidadAnteriorGeneralKg = apCaliDto.AlmacenGasAnterior.CantidadActualGeneralKg,
                CantidadAnteriorGeneralLt = apCaliDto.AlmacenGasAnterior.CantidadActualGeneralLt,
                PorcentajeAnteriorGeneral = apCaliDto.AlmacenGasAnterior.PorcentajeActualGeneral,
            };

            apCaliDto.MovimientoSalida = apCaliDto.CalibracionLecturaFinal.IdDestinoCalibracion.Equals(CalibracionDestinoEnum.TanquePortatil)
                ? AlmacenGasAdapter.FromEntity(apCaliDto.unidadAlmacenGas, apCaliDto.CalibracionLecturaFinal, apCaliDto.AlmacenGas, ulMovSalida, apCaliDto.Empresa, invSalidaAnterior, false, apCaliDto.unidadAlmacenGasPrincipal.IdCAlmacenGas, apCaliDto.unidadAlmacenGasPrincipal.Numero)
                : AlmacenGasAdapter.FromEntity(apCaliDto.unidadAlmacenGas, apCaliDto.CalibracionLecturaFinal, apCaliDto.AlmacenGas, ulMovSalida, apCaliDto.Empresa, invSalidaAnterior, false, null, null);

            apCaliDto.unidadAlmacenGas.P5000Actual = apCaliDto.CalibracionLecturaFinal.P5000;
            apCaliDto.unidadAlmacenGas.PorcentajeCalibracionPlaneada = apCaliDto.CalibracionLecturaFinal.PorcentajeCalibracion.Value;

            apCaliDto.AlmacenGas = AlmacenGasAdapter.FromEntity(apCaliDto.AlmacenGas);
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
        public static List<AplicaTomaLecturaDto> AplicarTomaLecturaInicial()
        {
            List<AplicaTomaLecturaDto> aplicaciones = new List<AplicaTomaLecturaDto>();
            List<AlmacenGasTomaLectura> TomaLecturasGasIniciales = ObtenerLecturasNoProcesadas(TipoEventoEnum.Inicial);

            if (TomaLecturasGasIniciales != null && TomaLecturasGasIniciales.Count > 0)
                TomaLecturasGasIniciales.ForEach(x => aplicaciones.Add(AplicarTomaLecturaInicial(x)));

            return aplicaciones;
        }
        public static List<AplicaTomaLecturaDto> AplicarTomaLecturaFinal()
        {
            List<AplicaTomaLecturaDto> aplicaciones = new List<AplicaTomaLecturaDto>();
            List<AlmacenGasTomaLectura> TomaLecturasGasFinales = ObtenerLecturasNoProcesadas(TipoEventoEnum.Final);

            if (TomaLecturasGasFinales != null && TomaLecturasGasFinales.Count > 0)
                TomaLecturasGasFinales.ForEach(x => aplicaciones.Add(AplicarTomaLecturaFinal(x)));

            return aplicaciones;
        }
        public static AplicaTomaLecturaDto AplicarTomaLecturaInicial(AlmacenGasTomaLectura TomaLecturaInicial)
        {
            AplicaTomaLecturaDto apLecturaDto = new AplicaTomaLecturaDto()
            {
                TomaLecturaLectura = TomaLecturaInicial,
                unidadAlmacenGas = ObtenerUnidadAlmacenGas(TomaLecturaInicial)
            };

            apLecturaDto.Empresa = EmpresaServicio.Obtener(apLecturaDto.unidadAlmacenGas);
            apLecturaDto = AplicarTomaLecturaInicial(apLecturaDto);

            return apLecturaDto;
        }
        public static AplicaTomaLecturaDto AplicarTomaLecturaFinal(AlmacenGasTomaLectura TomaLecturaFinal)
        {
            AplicaTomaLecturaDto apLecturaDto = new AplicaTomaLecturaDto()
            {
                TomaLecturaLectura = TomaLecturaFinal,
                //TomaLecturasFinales = TomaLecturasGasFinales,
                unidadAlmacenGas = AlmacenGasServicio.ObtenerUnidadAlmacenGas(TomaLecturaFinal)
            };

            apLecturaDto.Empresa = EmpresaServicio.Obtener(apLecturaDto.unidadAlmacenGas);
            apLecturaDto = AplicarTomaLecturaFinal(apLecturaDto);

            return apLecturaDto;
        }
        public static AplicaTomaLecturaDto AplicarTomaLecturaInicial(AplicaTomaLecturaDto apLectDto)
        {
            apLectDto.identidadUA = IdentificarTipoUnidadAlamcenGas(apLectDto.unidadAlmacenGas);
            AplicarTomaLecturaInicialProceso(apLectDto);

            new AlmacenGasDataAccess().Actualizar(apLectDto);
            return apLectDto;
        }
        public static AplicaTomaLecturaDto AplicarTomaLecturaFinal(AplicaTomaLecturaDto apLectDto)
        {
            apLectDto.identidadUA = IdentificarTipoUnidadAlamcenGas(apLectDto.unidadAlmacenGas);
            AplicarTomaLecturaFinalProceso(apLectDto);

            new AlmacenGasDataAccess().Actualizar(apLectDto);
            return apLectDto;
        }
        public static AplicaTomaLecturaDto AplicarTomaLecturaInicialProceso(AplicaTomaLecturaDto apLectDto)
        {
            apLectDto.AlmacenGas = ObtenerAlmacenGasTotal(apLectDto.Empresa.IdEmpresa);
            apLectDto.ulMov = ObtenerUltimoMovimientoEnInventario(apLectDto.Empresa.IdEmpresa, apLectDto.AlmacenGas.IdAlmacenGas);
            apLectDto.ulMovUnidad = ObtenerUltimoMovimientoPorUnidadAlmacenGas(apLectDto.Empresa.IdEmpresa, apLectDto.TomaLecturaLectura.IdCAlmacenGas);

            switch (apLectDto.identidadUA)
            {
                case identidadUnidadAlmacenGas.Pipa: apLectDto = AplicarTomaLecturaPipa(apLectDto); break;
                case identidadUnidadAlmacenGas.EstacionCarburacion: apLectDto = AplicarTomaLecturaEstacion(apLectDto); break;
                case identidadUnidadAlmacenGas.Camioneta: apLectDto = AplicarTomaLecturaCamioneta(apLectDto); break;
                //case identidadUnidadAlmacenGas.AlmacenAlterno: break;
                default: apLectDto = AplicarTomaLecturaAlmacenPrincipal(apLectDto); break;
            }

            return apLectDto;
        }
        public static AplicaTomaLecturaDto AplicarTomaLecturaFinalProceso(AplicaTomaLecturaDto apLectDto)
        {
            apLectDto.AlmacenGas = ObtenerAlmacenGasTotal(apLectDto.Empresa.IdEmpresa);

            apLectDto.ulMov = ObtenerUltimoMovimientoEnInventario(apLectDto.Empresa.IdEmpresa, apLectDto.AlmacenGas.IdAlmacenGas);
            apLectDto.ulMovUnidad = ObtenerUltimoMovimientoPorUnidadAlmacenGas(apLectDto.Empresa.IdEmpresa, apLectDto.TomaLecturaLectura.IdCAlmacenGas);

            switch (apLectDto.identidadUA)
            {
                case identidadUnidadAlmacenGas.Pipa: apLectDto = AplicarTomaLecturaPipa(apLectDto); break;
                case identidadUnidadAlmacenGas.EstacionCarburacion: apLectDto = AplicarTomaLecturaEstacion(apLectDto); break;
                case identidadUnidadAlmacenGas.Camioneta: apLectDto = AplicarTomaLecturaCamioneta(apLectDto); break;
                //case identidadUnidadAlmacenGas.AlmacenAlterno: break;
                default: apLectDto = AplicarTomaLecturaFinalAlmacenPrincipal(apLectDto); break;
            }

            //var invAnterior = new InventarioAnteriorDto
            //{   
            //    NombreOperador = null,
            //    CantidadAnteriorKg = apLectDto.ulMovUnidad.CantidadActualKg,
            //    CantidadAnteriorLt = apLectDto.ulMovUnidad.CantidadActualLt,
            //    PorcentajeAnterior = apLectDto.ulMovUnidad.PorcentajeActual,
            //    P5000Anterior = apLectDto.ulMovUnidad.P5000Actual,

            //    CAlmEntradaDiaKg = apLectDto.ulMovUnidad.CAlmEntradaDiaKg,
            //    CAlmEntradaDiaLt = apLectDto.ulMovUnidad.CAlmEntradaDiaLt,
            //    CAlmEntradaMesKg = apLectDto.ulMovUnidad.CAlmEntradaMesKg,
            //    CAlmEntradaMesLt = apLectDto.ulMovUnidad.CAlmEntradaMesLt,
            //    CAlmEntradaAnioKg = apLectDto.ulMovUnidad.CAlmEntradaAnioKg,
            //    CAlmEntradaAnioLt = apLectDto.ulMovUnidad.CAlmEntradaAnioLt,
            //    CAlmSalidaDiaKg = apLectDto.ulMovUnidad.CAlmSalidaDiaKg,
            //    CAlmSalidaDiaLt = apLectDto.ulMovUnidad.CAlmSalidaDiaLt,
            //    CAlmSalidaMesKg = apLectDto.ulMovUnidad.CAlmSalidaMesKg,
            //    CAlmSalidaMesLt = apLectDto.ulMovUnidad.CAlmSalidaMesLt,
            //    CAlmSalidaAnioKg = apLectDto.ulMovUnidad.CAlmSalidaAnioKg,
            //    CAlmSalidaAnioLt = apLectDto.ulMovUnidad.CAlmSalidaAnioLt,
            //    CantidadAcumuladaDiaKg = apLectDto.ulMovUnidad.CantidadAcumuladaDiaKg,
            //    CantidadAcumuladaDiaLt = apLectDto.ulMovUnidad.CantidadAcumuladaDiaLt,
            //    CantidadAcumuladaMesKg = apLectDto.ulMovUnidad.CantidadAcumuladaMesKg,
            //    CantidadAcumuladaMesLt = apLectDto.ulMovUnidad.CantidadAcumuladaMesLt,
            //    CantidadAcumuladaAnioKg = apLectDto.ulMovUnidad.CantidadAcumuladaAnioKg,
            //    CantidadAcumuladaAnioLt = apLectDto.ulMovUnidad.CantidadAcumuladaAnioLt,

            //    CantidadAnteriorTotalKg = apLectDto.ulMov.CantidadActualTotalKg,
            //    CantidadAnteriorTotalLt = apLectDto.ulMov.CantidadActualTotalLt,
            //    PorcentajeAnteriorTotal = apLectDto.ulMov.PorcentajeActualTotal,
            //    CantidadAnteriorGeneralKg = apLectDto.ulMov.CantidadActualGeneralKg,
            //    CantidadAnteriorGeneralLt = apLectDto.ulMov.CantidadActualGeneralLt,
            //    PorcentajeAnteriorGeneral = apLectDto.ulMov.PorcentajeActualGeneral,
            //};

            //apLectDto.MovimientoUnidad = AlmacenGasAdapter.FromEntity(apLectDto.unidadAlmacenGas, apLectDto.TomaLecturaLectura, apLectDto.AlmacenGas, apLectDto.ulMovUnidad, apLectDto.Empresa, invAnterior, true);
            ////apLectDto.AlmacenGas = AlmacenGasAdapter.FromEntity(apLectDto.AlmacenGas);
            //apLectDto.unidadAlmacenGas = AlmacenGasAdapter.FromEntity(apLectDto.unidadAlmacenGas);
            //apLectDto.TomaLecturaLectura = AlmacenGasAdapter.FromEntity(apLectDto.TomaLecturaLectura);

            return apLectDto;
        }
        public static AplicaTomaLecturaDto AplicarTomaLecturaAlmacenPrincipal(AplicaTomaLecturaDto apLectDto)
        {
            apLectDto.unidadAlmacenGas.PorcentajeActual = apLectDto.TomaLecturaLectura.Porcentaje.Value;
            apLectDto.unidadAlmacenGas.P5000Actual = null;
            apLectDto.unidadAlmacenGas.CantidadActualLt = CalcularGasServicio.ObtenerLitrosDesdePorcentaje(apLectDto.unidadAlmacenGas.CapacidadTanqueLt.Value, apLectDto.unidadAlmacenGas.PorcentajeActual);
            apLectDto.unidadAlmacenGas.CantidadActualKg = CalcularGasServicio.ObtenerKilogramosDesdeLitros(apLectDto.unidadAlmacenGas.CantidadActualLt, apLectDto.Empresa.FactorLitrosAKilos);
            apLectDto.TomaLecturaLectura.DatosProcesados = true;

            var invAnterior = new InventarioAnteriorDto
            {
                Orden = ObtenerOrdenMovimientoEnInventario(apLectDto.Empresa.IdEmpresa, apLectDto.AlmacenGas.IdAlmacenGas, apLectDto.TomaLecturaLectura.FechaAplicacion),
                NombreOperador = "Pendiente",
                CantidadAnteriorKg = apLectDto.ulMovUnidad.CantidadActualKg,
                CantidadAnteriorLt = apLectDto.ulMovUnidad.CantidadActualLt,
                PorcentajeAnterior = apLectDto.ulMovUnidad.PorcentajeActual,
                P5000Anterior = apLectDto.ulMovUnidad.P5000Actual,

                CAlmLecturaInicialMagnatel = apLectDto.TomaLecturaLectura.Porcentaje != null ? apLectDto.TomaLecturaLectura.Porcentaje.Value : 0,
                CAlmLecturaInicialP5000 = apLectDto.TomaLecturaLectura.P5000 != null ? apLectDto.TomaLecturaLectura.P5000.Value : 0,
                CAlmEntradaMesKg = apLectDto.ulMovUnidad.CAlmEntradaMesKg,
                CAlmEntradaMesLt = apLectDto.ulMovUnidad.CAlmEntradaMesLt,
                CAlmEntradaAnioKg = apLectDto.ulMovUnidad.CAlmEntradaAnioKg,
                CAlmEntradaAnioLt = apLectDto.ulMovUnidad.CAlmEntradaAnioLt,
                CAlmSalidaMesKg = apLectDto.ulMovUnidad.CAlmSalidaMesKg,
                CAlmSalidaMesLt = apLectDto.ulMovUnidad.CAlmSalidaMesLt,
                CAlmSalidaAnioKg = apLectDto.ulMovUnidad.CAlmSalidaAnioKg,
                CAlmSalidaAnioLt = apLectDto.ulMovUnidad.CAlmSalidaAnioLt,
                CantidadAcumuladaMesKg = apLectDto.ulMovUnidad.CantidadAcumuladaMesKg,
                CantidadAcumuladaMesLt = apLectDto.ulMovUnidad.CantidadAcumuladaMesLt,
                CantidadAcumuladaAnioKg = apLectDto.ulMovUnidad.CantidadAcumuladaAnioKg,
                CantidadAcumuladaAnioLt = apLectDto.ulMovUnidad.CantidadAcumuladaAnioLt,

                CantidadAnteriorTotalKg = apLectDto.ulMov != null ? apLectDto.ulMov.CantidadActualTotalKg : 0,
                CantidadAnteriorTotalLt = apLectDto.ulMov != null ? apLectDto.ulMov.CantidadActualTotalLt : 0,
                PorcentajeAnteriorTotal = apLectDto.ulMov != null ? apLectDto.ulMov.PorcentajeActualTotal : 0,
                CantidadAnteriorGeneralKg = apLectDto.ulMov != null ? apLectDto.ulMov.CantidadActualGeneralKg : 0,
                CantidadAnteriorGeneralLt = apLectDto.ulMov != null ? apLectDto.ulMov.CantidadActualGeneralLt : 0,
                PorcentajeAnteriorGeneral = apLectDto.ulMov != null ? apLectDto.ulMov.PorcentajeActualGeneral : 0,

            };
            apLectDto.MovimientoUnidad = AlmacenGasAdapter.FromEntity(apLectDto.unidadAlmacenGas, apLectDto.TomaLecturaLectura, apLectDto.AlmacenGas, apLectDto.ulMov, apLectDto.Empresa, invAnterior, true);
            apLectDto.AlmacenGas = AlmacenGasAdapter.FromEntity(apLectDto.AlmacenGas);
            apLectDto.unidadAlmacenGas = AlmacenGasAdapter.FromEntity(apLectDto.unidadAlmacenGas);
            apLectDto.TomaLecturaLectura = AlmacenGasAdapter.FromEntity(apLectDto.TomaLecturaLectura);

            return apLectDto;
        }
        public static AplicaTomaLecturaDto AplicarTomaLecturaFinalAlmacenPrincipal(AplicaTomaLecturaDto apLectDto)
        {
            AlmacenGasMovimiento ulMovLecturaInicial = ObtenerUltimoMovimientoPorUnidadAlmacenGas(apLectDto.Empresa.IdEmpresa, apLectDto.TomaLecturaLectura.IdCAlmacenGas, TipoEventoEnum.TomaLectura, TipoMovimientoEnum.LectInicial, apLectDto.TomaLecturaLectura.FechaAplicacion);
            if (ulMovLecturaInicial.IdEmpresa <= 0 && ulMovLecturaInicial.Year <= 0 && ulMovLecturaInicial.Mes <= 0 && ulMovLecturaInicial.Dia <= 0 && ulMovLecturaInicial.Orden <= 0)
                return new AplicaTomaLecturaDto();

            AlmacenGasMovimiento ulMovLecturaFinal = ObtenerUltimoMovimientoPorUnidadAlmacenGas(apLectDto.Empresa.IdEmpresa, apLectDto.TomaLecturaLectura.IdCAlmacenGas, TipoEventoEnum.TomaLectura, TipoMovimientoEnum.LectFinal, apLectDto.TomaLecturaLectura.FechaAplicacion);
            if (ulMovLecturaInicial.IdEmpresa > 0 && ulMovLecturaInicial.Year > 0 && ulMovLecturaInicial.Mes > 0 && ulMovLecturaInicial.Dia > 0 && ulMovLecturaInicial.Orden > 0)
            {
                if (ulMovLecturaInicial.Orden <= ulMovLecturaFinal.Orden)
                    return new AplicaTomaLecturaDto();
            }

            AlmacenGasMovimiento ulMovDescarga = ObtenerUltimoMovimientoPorUnidadAlmacenGas(apLectDto.Empresa.IdEmpresa, apLectDto.TomaLecturaLectura.IdCAlmacenGas, TipoEventoEnum.Descarga, TipoMovimientoEnum.Entrada, apLectDto.TomaLecturaLectura.FechaAplicacion, ulMovLecturaInicial.Orden);
            AlmacenGasMovimiento ulMovRecarga = ObtenerUltimoMovimientoPorUnidadAlmacenGas(apLectDto.Empresa.IdEmpresa, apLectDto.TomaLecturaLectura.IdCAlmacenGas, TipoEventoEnum.Recarga, TipoMovimientoEnum.Salida, apLectDto.TomaLecturaLectura.FechaAplicacion, ulMovLecturaInicial.Orden);
            AlmacenGasMovimiento ulMovAutoConsumo = ObtenerUltimoMovimientoPorUnidadAlmacenGas(apLectDto.Empresa.IdEmpresa, apLectDto.TomaLecturaLectura.IdCAlmacenGas, TipoEventoEnum.AutoConsumo, TipoMovimientoEnum.Salida, apLectDto.TomaLecturaLectura.FechaAplicacion, ulMovLecturaInicial.Orden);

            decimal litrosDescargados = ulMovDescarga.IdEmpresa <= 0 && ulMovDescarga.Year <= 0 && ulMovDescarga.Mes <= 0 && ulMovDescarga.Dia <= 0 && ulMovDescarga.Orden <= 0
                ? ulMovDescarga.CAlmEntradaDiaLt : 0;

            decimal litrosRecarcados = ulMovRecarga.IdEmpresa <= 0 && ulMovRecarga.Year <= 0 && ulMovRecarga.Mes <= 0 && ulMovRecarga.Dia <= 0 && ulMovRecarga.Orden <= 0
                ? ulMovRecarga.CAlmSalidaDiaLt : 0;

            decimal litrosCarburados = ulMovAutoConsumo.IdEmpresa <= 0 && ulMovAutoConsumo.Year <= 0 && ulMovAutoConsumo.Mes <= 0 && ulMovAutoConsumo.Dia <= 0 && ulMovAutoConsumo.Orden <= 0
                ? ulMovAutoConsumo.CAlmSalidaDiaLt : 0;

            decimal litrosFinales = CalcularGasServicio.ObtenerLitrosFinalesAlmacenPrinAlt(ulMovLecturaInicial.CantidadActualLt, litrosDescargados, litrosRecarcados, litrosCarburados);
            decimal kilosFinales = CalcularGasServicio.ObtenerKilogramosDesdeLitros(litrosFinales, apLectDto.Empresa.FactorLitrosAKilos);
            decimal litrosEnTanque = CalcularGasServicio.ObtenerLitrosEnElTanque(apLectDto.unidadAlmacenGas.CapacidadTanqueLt.Value, apLectDto.TomaLecturaLectura.Porcentaje.Value);
            litrosEnTanque = litrosEnTanque > 0 ? litrosEnTanque : 0;
            decimal kilosEnTanque = CalcularGasServicio.ObtenerKilogramosDesdeLitros(litrosEnTanque, apLectDto.Empresa.FactorLitrosAKilos);
            //decimal SalidaGasDiaLt = CalcularGasServicio.ObtenerDiferenciaLitros(, ulMovLecturaInicial.CantidadActualLt);

            apLectDto.unidadAlmacenGas.CantidadActualLt = litrosEnTanque;
            apLectDto.unidadAlmacenGas.CantidadActualKg = kilosEnTanque;
            apLectDto.unidadAlmacenGas.PorcentajeActual = apLectDto.TomaLecturaLectura.Porcentaje.Value;
            apLectDto.unidadAlmacenGas.P5000Actual = null;

            apLectDto.AlmacenGasAnterior = ObtenerAlmacenGasTotal(apLectDto.Empresa.IdEmpresa);
            apLectDto.AlmacenGas = apLectDto.AlmacenGasAnterior;

            apLectDto = AplicarTomaLecturaFinalAlmacenAlterno(apLectDto);

            apLectDto.AlmacenGas.CantidadActualLt = CalcularGasServicio.ObtenerLitrosFinalesAlmacenPrinAlt(ulMovLecturaInicial.CantidadActualTotalLt, litrosDescargados, litrosRecarcados, litrosCarburados);
            apLectDto.AlmacenGas.CantidadActualKg = CalcularGasServicio.ObtenerKilogramosDesdeLitros(apLectDto.AlmacenGas.CantidadActualLt, apLectDto.Empresa.FactorLitrosAKilos);
            apLectDto.AlmacenGas.PorcentajeActual = CalcularGasServicio.ObtenerPorcentajeDesdeLitros(apLectDto.AlmacenGas.CapacidadTotalLt, apLectDto.AlmacenGas.CantidadActualLt);
            apLectDto.AlmacenGas.CantidadActualGeneralLt = CalcularGasServicio.ObtenerLitrosFinalesAlmacenPrinAlt(ulMovLecturaInicial.CantidadActualGeneralLt, litrosDescargados, litrosRecarcados, litrosCarburados);
            apLectDto.AlmacenGas.CantidadActualGeneralKg = CalcularGasServicio.ObtenerKilogramosDesdeLitros(apLectDto.AlmacenGas.CantidadActualGeneralLt, apLectDto.Empresa.FactorLitrosAKilos);
            apLectDto.AlmacenGas.PorcentajeActualGeneral = CalcularGasServicio.ObtenerPorcentajeDesdeLitros(apLectDto.AlmacenGas.CapacidadGeneralLt, apLectDto.AlmacenGas.CantidadActualGeneralLt);

            apLectDto.TomaLecturaLectura.DatosProcesados = true;

            var invAnterior = new InventarioAnteriorDto
            {
                Orden = ObtenerOrdenMovimientoEnInventario(apLectDto.Empresa.IdEmpresa, apLectDto.AlmacenGas.IdAlmacenGas, apLectDto.TomaLecturaLectura.FechaAplicacion),
                NombreOperador = "Pendiente",
                CantidadAnteriorKg = apLectDto.ulMovUnidad.CantidadActualKg,
                CantidadAnteriorLt = apLectDto.ulMovUnidad.CantidadActualLt,
                PorcentajeAnterior = apLectDto.ulMovUnidad.PorcentajeActual,
                P5000Anterior = apLectDto.ulMovUnidad.P5000Actual,

                CAlmLecturaInicialMagnatel = apLectDto.TomaLecturaLectura.Porcentaje != null ? apLectDto.TomaLecturaLectura.Porcentaje.Value : 0,
                CAlmLecturaInicialP5000 = apLectDto.TomaLecturaLectura.P5000 != null ? apLectDto.TomaLecturaLectura.P5000.Value : 0,
                CAlmEntradaMesKg = apLectDto.ulMovUnidad.CAlmEntradaMesKg,
                CAlmEntradaMesLt = apLectDto.ulMovUnidad.CAlmEntradaMesLt,
                CAlmEntradaAnioKg = apLectDto.ulMovUnidad.CAlmEntradaAnioKg,
                CAlmEntradaAnioLt = apLectDto.ulMovUnidad.CAlmEntradaAnioLt,
                CAlmSalidaMesKg = apLectDto.ulMovUnidad.CAlmSalidaMesKg,
                CAlmSalidaMesLt = apLectDto.ulMovUnidad.CAlmSalidaMesLt,
                CAlmSalidaAnioKg = apLectDto.ulMovUnidad.CAlmSalidaAnioKg,
                CAlmSalidaAnioLt = apLectDto.ulMovUnidad.CAlmSalidaAnioLt,
                CantidadAcumuladaMesKg = apLectDto.ulMovUnidad.CantidadAcumuladaMesKg,
                CantidadAcumuladaMesLt = apLectDto.ulMovUnidad.CantidadAcumuladaMesLt,
                CantidadAcumuladaAnioKg = apLectDto.ulMovUnidad.CantidadAcumuladaAnioKg,
                CantidadAcumuladaAnioLt = apLectDto.ulMovUnidad.CantidadAcumuladaAnioLt,

                CantidadAnteriorTotalKg = apLectDto.ulMov.CantidadActualTotalKg,
                CantidadAnteriorTotalLt = apLectDto.ulMov.CantidadActualTotalLt,
                PorcentajeAnteriorTotal = apLectDto.ulMov.PorcentajeActualTotal,
                CantidadAnteriorGeneralKg = apLectDto.ulMov.CantidadActualGeneralKg,
                CantidadAnteriorGeneralLt = apLectDto.ulMov.CantidadActualGeneralLt,
                PorcentajeAnteriorGeneral = apLectDto.ulMov.PorcentajeActualGeneral,
            };

            apLectDto.MovimientoUnidad = AlmacenGasAdapter.FromEntity(apLectDto.unidadAlmacenGas, apLectDto.TomaLecturaLectura, apLectDto.AlmacenGas, apLectDto.ulMovUnidad, apLectDto.Empresa, invAnterior, true);
            apLectDto.AlmacenGas = AlmacenGasAdapter.FromEntity(apLectDto.AlmacenGas);
            apLectDto.unidadAlmacenGas = AlmacenGasAdapter.FromEntity(apLectDto.unidadAlmacenGas);
            apLectDto.TomaLecturaLectura = AlmacenGasAdapter.FromEntity(apLectDto.TomaLecturaLectura);

            return apLectDto;
        }
        public static AplicaTomaLecturaDto AplicarTomaLecturaFinalAlmacenAlterno(AplicaTomaLecturaDto apLectDto)
        {
            if (apLectDto.unidadAlmacenGas.EsAlterno && apLectDto.unidadAlmacenGas.CantidadActualLt <= 0)
            {
                apLectDto.unidadAlmacenGas.Activo = false;
                apLectDto.AlmacenGas.CapacidadTotalLt = CalcularGasServicio.RestarLitros(apLectDto.AlmacenGas.CapacidadTotalLt, apLectDto.unidadAlmacenGas.CapacidadTanqueLt.Value);
                apLectDto.AlmacenGas.CapacidadTotalKg = CalcularGasServicio.ObtenerKilogramosDesdeLitros(apLectDto.AlmacenGas.CapacidadTotalLt, apLectDto.Empresa.FactorLitrosAKilos);
                apLectDto.AlmacenGas.CapacidadGeneralLt = CalcularGasServicio.RestarLitros(apLectDto.AlmacenGas.CapacidadGeneralLt, apLectDto.unidadAlmacenGas.CapacidadTanqueLt.Value);
                apLectDto.AlmacenGas.CapacidadGeneralKg = CalcularGasServicio.ObtenerKilogramosDesdeLitros(apLectDto.AlmacenGas.CapacidadGeneralLt, apLectDto.Empresa.FactorLitrosAKilos);
            }
            return apLectDto;
        }
        public static AplicaTomaLecturaDto AplicarTomaLecturaPipa(AplicaTomaLecturaDto apLectDto)
        {
            apLectDto.unidadAlmacenGas.PorcentajeActual = apLectDto.TomaLecturaLectura.Porcentaje.Value;
            apLectDto.unidadAlmacenGas.P5000Actual = apLectDto.TomaLecturaLectura.P5000;
            apLectDto.unidadAlmacenGas.CantidadActualLt = CalcularGasServicio.ObtenerLitrosDesdePorcentaje(apLectDto.unidadAlmacenGas.CapacidadTanqueLt.Value, apLectDto.unidadAlmacenGas.PorcentajeActual);
            apLectDto.unidadAlmacenGas.CantidadActualKg = CalcularGasServicio.ObtenerKilogramosDesdeLitros(apLectDto.unidadAlmacenGas.CantidadActualLt, apLectDto.Empresa.FactorLitrosAKilos);
            apLectDto.TomaLecturaLectura.DatosProcesados = true;

            //apLectDto.MovimientoUnidad = AlmacenGasAdapter.FromEntity(apLectDto.unidadAlmacenGas, apLectDto.TomaLecturaLectura, apLectDto.AlmacenGas, apLectDto.ulMov, apLectDto.Empresa, invAnterior, true);
            apLectDto.AlmacenGas = AlmacenGasAdapter.FromEntity(apLectDto.AlmacenGas);
            apLectDto.unidadAlmacenGas = AlmacenGasAdapter.FromEntity(apLectDto.unidadAlmacenGas);
            apLectDto.TomaLecturaLectura = AlmacenGasAdapter.FromEntity(apLectDto.TomaLecturaLectura);
            return apLectDto;
        }
        public static AplicaTomaLecturaDto AplicarTomaLecturaFinalPipa(AplicaTomaLecturaDto apLectDto)
        {
            apLectDto.unidadAlmacenGas.PorcentajeActual = apLectDto.TomaLecturaLectura.Porcentaje.Value;
            apLectDto.unidadAlmacenGas.P5000Actual = apLectDto.TomaLecturaLectura.P5000;
            apLectDto.unidadAlmacenGas.CantidadActualLt = CalcularGasServicio.ObtenerLitrosDesdePorcentaje(apLectDto.unidadAlmacenGas.CapacidadTanqueLt.Value, apLectDto.unidadAlmacenGas.PorcentajeActual);
            apLectDto.unidadAlmacenGas.CantidadActualKg = CalcularGasServicio.ObtenerKilogramosDesdeLitros(apLectDto.unidadAlmacenGas.CantidadActualLt, apLectDto.Empresa.FactorLitrosAKilos);
            apLectDto.TomaLecturaLectura.DatosProcesados = true;

            //apLectDto.MovimientoUnidad = AlmacenGasAdapter.FromEntity(apLectDto.unidadAlmacenGas, apLectDto.TomaLecturaLectura, apLectDto.AlmacenGas, apLectDto.ulMov, apLectDto.Empresa, invAnterior, true);
            apLectDto.AlmacenGas = AlmacenGasAdapter.FromEntity(apLectDto.AlmacenGas);
            apLectDto.unidadAlmacenGas = AlmacenGasAdapter.FromEntity(apLectDto.unidadAlmacenGas);
            apLectDto.TomaLecturaLectura = AlmacenGasAdapter.FromEntity(apLectDto.TomaLecturaLectura);
            return apLectDto;
        }
        public static AplicaTomaLecturaDto AplicarTomaLecturaEstacion(AplicaTomaLecturaDto apLectDto)
        {
            apLectDto.unidadAlmacenGas.PorcentajeActual = apLectDto.TomaLecturaLectura.Porcentaje ?? 0;
            apLectDto.unidadAlmacenGas.P5000Actual = apLectDto.TomaLecturaLectura.P5000;
            apLectDto.unidadAlmacenGas.CantidadActualLt = CalcularGasServicio.ObtenerLitrosDesdePorcentaje(apLectDto.unidadAlmacenGas.CapacidadTanqueLt.Value, apLectDto.unidadAlmacenGas.PorcentajeActual);
            apLectDto.unidadAlmacenGas.CantidadActualKg = CalcularGasServicio.ObtenerKilogramosDesdeLitros(apLectDto.unidadAlmacenGas.CantidadActualLt, apLectDto.Empresa.FactorLitrosAKilos);
            apLectDto.TomaLecturaLectura.DatosProcesados = true;


            apLectDto.AlmacenGas = AlmacenGasAdapter.FromEntity(apLectDto.AlmacenGas);
            apLectDto.unidadAlmacenGas = AlmacenGasAdapter.FromEntity(apLectDto.unidadAlmacenGas);
            apLectDto.TomaLecturaLectura = AlmacenGasAdapter.FromEntity(apLectDto.TomaLecturaLectura);
            apLectDto.TomaLecturaLectura.Cilindros = null;
            apLectDto.TomaLecturaLectura.Fotografias = null;
            return apLectDto;
        }
        public static AplicaTomaLecturaDto AplicarTomaLecturaCamioneta(AplicaTomaLecturaDto apLectDto)
        {
            AlmacenGasMovimiento ulMovRecarga = ObtenerUltimoMovimientoPorUnidadAlmacenGas(apLectDto.Empresa.IdEmpresa, apLectDto.unidadAlmacenGas.IdCAlmacenGas, TipoEventoEnum.Recarga, TipoMovimientoEnum.Entrada);
            apLectDto.AlmacenGas = AlmacenGasAdapter.FromEntity(apLectDto.AlmacenGas);
            apLectDto.unidadAlmacenGas = AlmacenGasAdapter.FromEntity(apLectDto.unidadAlmacenGas);
            apLectDto.TomaLecturaLectura = AlmacenGasAdapter.FromEntity(apLectDto.TomaLecturaLectura);

            if (ulMovRecarga.IdEmpresa <= 0 && ulMovRecarga.Year <= 0 && ulMovRecarga.Mes <= 0 && ulMovRecarga.Dia <= 0)
                return new AplicaTomaLecturaDto();

            apLectDto.unidadAlmacenGas.PorcentajeActual = apLectDto.TomaLecturaLectura.Porcentaje.Value;
            apLectDto.unidadAlmacenGas.P5000Actual = ulMovRecarga.P5000Actual;
            apLectDto.unidadAlmacenGas.CantidadActualLt = ulMovRecarga.CantidadActualKg;
            apLectDto.unidadAlmacenGas.CantidadActualKg = ulMovRecarga.CantidadActualKg;
            apLectDto.TomaLecturaLectura.DatosProcesados = true;

            return apLectDto;
        }
        public static List<AlmacenGasTomaLecturaFoto> GenerarImagenes(AlmacenGasTomaLectura TomaLectura)
        {
            List<AlmacenGasTomaLecturaFoto> imagenes = ObtenerImagenes(TomaLectura);

            var fotos = new List<AlmacenGasTomaLecturaFoto>();

            if (imagenes != null && imagenes.Count > 0)
            {
                foreach (var imagen in imagenes)
                {
                    var img = ImagenServicio.ObtenerImagen(imagen);
                    var foto = AlmacenGasAdapter.FromEntity(img);
                    fotos.Add(foto);
                }
            }

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
            return new PuntoVentaDataAccess().Anticipos(idEmpresa);
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
        public static List<EstacionCarburacion> ObtenerEstacionesEmpresa(short idEmpresa)
        {
            return new AlmacenDataAccess().ObtenerEstaciones(idEmpresa);
        }
        public static decimal ObtenerKgInventarioFisico(DateTime fecha, short idEmpresa)
        {
            decimal Ifkg = 0;
            var almacenes = ObtenerAlmacenes(idEmpresa);
            foreach (var almacen in almacenes)
            {
                var lecturas = ObtenerLecturas(almacen.IdCAlmacenGas);
                foreach (var lect in lecturas)
                {
                    if (lect.FechaAplicacion.ToShortDateString().Equals(fecha.ToShortDateString()) && lect.ClaveOperacion.Contains("LF"))
                    {
                        if (lect.Porcentaje != null)
                            Ifkg += CalcularAlmacenServicio.ObtenerKgLectura(lect.UnidadAlmacenGas.CapacidadTanqueLt ?? 0, lect.Porcentaje ?? 0, almacen.EsGeneral ? 1 : (decimal)0.54);
                        else
                        {
                            foreach (var cilindro in lect.Cilindros)
                                Ifkg += CalcularAlmacenServicio.ObtenerKgLecturaCilindro(cilindro.Cantidad, cilindro.Cilindro.CapacidadKg);
                        }
                    }
                }
            }
            return Ifkg;
        }
        public static RemanenteDTO BusquedaGeneralPeriodoActual()
        {
            return new RemanenteDTO()
            {
                IdEmpresa = TokenServicio.ObtenerIdEmpresa(),
                IdTipo = TipoRemanenteEnum.General,
                //Fecha = new DateTime(2018, 12, 1),//Comentar esta linea en produccion
                Fecha = DateTime.Now, //Comentar esta line en Desarollo
                IdPuntoVenta = 0
            };
        }
        private static ReporteDiaDTO ErrorDTO(RespuestaDto respuesta)
        {
            return new ReporteDiaDTO()
            {
                Error = true,
                Mensaje = respuesta.Mensaje,
            };
        }
        public static string ObtenerLecutraCamioneta(AlmacenGasTomaLectura lectura)
        {
            var cantinIcial = string.Empty;
            cantinIcial += CalculosGenerales.Truncar(lectura.Cilindros.FirstOrDefault(x => x.IdCilindro.Equals(2)).Cantidad, 2).ToString() + " Cilindro(s) 20Kg -";
            cantinIcial += CalculosGenerales.Truncar(lectura.Cilindros.FirstOrDefault(x => x.IdCilindro.Equals(3)).Cantidad, 2).ToString() + " Cilindro(s) 30Kg -";
            cantinIcial += CalculosGenerales.Truncar(lectura.Cilindros.FirstOrDefault(x => x.IdCilindro.Equals(4)).Cantidad, 2).ToString() + " Cilindro(s) 45Kg";
            return cantinIcial;
        }
        public static string ObtenerDiferenciaLecutraCamioneta(AlmacenGasTomaLectura li, AlmacenGasTomaLectura lf)
        {
            decimal cantinIcial = 0;
            //cantinIcial += CalculosGenerales.Truncar(CalculosGenerales.DiferenciaEntreDosNumero(li.Cilindros.FirstOrDefault(x => x.IdCilindro.Equals(2)).Cantidad, lf.Cilindros.FirstOrDefault(x => x.IdCilindro.Equals(2)).Cantidad), 2).ToString() + " Cilindro(s) 20Kg -";
            //cantinIcial += CalculosGenerales.Truncar(CalculosGenerales.DiferenciaEntreDosNumero(li.Cilindros.FirstOrDefault(x => x.IdCilindro.Equals(3)).Cantidad, lf.Cilindros.FirstOrDefault(x => x.IdCilindro.Equals(3)).Cantidad), 2).ToString() + " Cilindro(s) 30Kg -";
            //cantinIcial += CalculosGenerales.Truncar(CalculosGenerales.DiferenciaEntreDosNumero(li.Cilindros.FirstOrDefault(x => x.IdCilindro.Equals(4)).Cantidad, lf.Cilindros.FirstOrDefault(x => x.IdCilindro.Equals(4)).Cantidad), 2).ToString() + " Cilindro(s) 45Kg ";

            cantinIcial += CalculosGenerales.DiferenciaEntreDosNumero(li.Cilindros.FirstOrDefault(x => x.IdCilindro.Equals(2)).Cantidad, lf.Cilindros.FirstOrDefault(x => x.IdCilindro.Equals(2)).Cantidad)*20;
            cantinIcial += CalculosGenerales.DiferenciaEntreDosNumero(li.Cilindros.FirstOrDefault(x => x.IdCilindro.Equals(3)).Cantidad, lf.Cilindros.FirstOrDefault(x => x.IdCilindro.Equals(3)).Cantidad) * 30;
            cantinIcial += CalculosGenerales.DiferenciaEntreDosNumero(li.Cilindros.FirstOrDefault(x => x.IdCilindro.Equals(4)).Cantidad, lf.Cilindros.FirstOrDefault(x => x.IdCilindro.Equals(4)).Cantidad) * 45;
            return cantinIcial.ToString();
        }

    }
}
