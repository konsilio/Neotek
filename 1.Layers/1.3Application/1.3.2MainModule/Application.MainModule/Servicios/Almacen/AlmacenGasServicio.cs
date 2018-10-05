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
using Application.MainModule.AdaptadoresDTO.Almacen;
using Application.MainModule.Servicios.Compras;
using Security.MainModule.Criptografia;
using Utilities.MainModule;
using Sagas.MainModule.ObjetosValor.Constantes;
using Application.MainModule.Servicios.Seguridad;

namespace Application.MainModule.Servicios.Almacen
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
        public static List<AlmacenGasTraspaso> ObtenertraspasosNoProcesadas()
        {
            return new AlmacenGasDataAccess().BuscarTodosTraspasosNoProcesadas();
        }
        public static List<AlmacenGasRecarga> ObtenerRecargasNoProcesadas(byte idTipoEvento)
        {
            return new AlmacenGasDataAccess().BuscarTodasRecargasNoProcesadas(idTipoEvento);
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

            return new AlmacenGasDataAccess().BuscarImagenes(traspaso.IdCAlmacenGasEntrada, traspaso.IdCAlmacenGasSalida);
        }
        public static string ObtenerNombreUnidadAlmacenGas(UnidadAlmacenGas uAG)
        {
            if (uAG.EsGeneral) return uAG.Numero;

            var nombre = EquipoTransporteServicio.ObtenerNombre(uAG);
            if (!string.IsNullOrEmpty(nombre))
                return nombre;

            return EstacionCarburacionServicio.ObtenerNombre(uAG);
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
            //var lecturas = LecturaGasServicio.ObtenerTomaLectura();
            var descargasDto = AplicarDescargas();
            var recargasDto = AplicarRecargas();
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
            AlmacenGasMovimiento ultimoMovimiento = ObtenerUltimoMovimiento(descarga.FechaEntraGas.Value);

            var hoy = DateTime.Now;
            var aplicacion = new AplicaDescargaDto {
                Movimiento = new AlmacenGasMovimiento {
                    IdEmpresa = empresa.IdEmpresa,
                    SalidaKg = 0,
                    SalidaLt = 0,
                    Year = (short)hoy.Year,
                    Mes = (byte)hoy.Month,
                    Dia = (byte)hoy.Day,
                    Orden = ObtenerNumOrdenMovimiento(ultimoMovimiento)
                }
            };
            
            decimal kilogramosPapeletaTractor = descarga.MasaKg.Value;
            decimal litrosPapeletaTractor = CalcularGasServicio.ObtenerLitrosDesdeKilos(kilogramosPapeletaTractor, empresa.FactorLitrosAKilos);
            decimal litrosRealesTractor = CalcularGasServicio.ObtenerLitrosEnElTanque(descarga.CapacidadTanqueLt.Value, descarga.PorcenMagnatelOcularTractorINI.Value);
            decimal kilogramosRealesTractor = CalcularGasServicio.ObtenerKilogramosDesdeLitros(litrosRealesTractor, empresa.FactorLitrosAKilos);
            aplicacion.Movimiento.RemanenteKg = CalcularGasServicio.ObtenerDiferenciaKilogramos(kilogramosRealesTractor, kilogramosPapeletaTractor);
            aplicacion.Movimiento.RemanenteLt = CalcularGasServicio.ObtenerLitrosDesdeKilos(aplicacion.Movimiento.RemanenteKg.Value, empresa.FactorLitrosAKilos);
            aplicacion.Movimiento.EntradaKg = kilogramosRealesTractor;
            aplicacion.Movimiento.EntradaLt = litrosRealesTractor;
            aplicacion.Movimiento.

            unidadEntrada.CantidadActualKg = CalcularGasServicio.SumarKilogramos(unidadEntrada.CantidadActualKg, kilogramosRealesTractor);
            unidadEntrada.CantidadActualLt = CalcularGasServicio.ObtenerLitrosDesdeKilos(unidadEntrada.CantidadActualKg, empresa.FactorLitrosAKilos);
            unidadEntrada.PorcentajeActual = descarga.PorcenMagnatelOcularAlmacenFIN.Value;

            unidadEntrada = AplicarDescargaAlmacenAlterno(unidadEntrada, descarga);

            AlmacenGas almacenGasTotal = ObtenerAlmacenGasTotal(empresa);
            almacenGasTotal = AplicarDescargaAlmacenTotal(almacenGasTotal, unidadEntrada, litrosRealesTractor, kilogramosRealesTractor);
            
            return new AplicaDescargaDto()
            {
                AlmacenGas = AlmacenGasAdapter.FromEntity(almacenGasTotal),
                Descarga = descarga,
                DescargaSinNavigationProperties = AlmacenGasAdapter.FromEntity(descarga),
                DescargaFotos = GenerarImagenes(descarga),                
                unidadEntrada = AlmacenGasAdapter.FromEntity(unidadEntrada),
                identidadUE = IdentificarTipoUnidadAlamcenGas(unidadEntrada),
                PorcentajeUE = unidadEntrada.PorcentajeActual,
                CantidadSINRemanenteKg = kilogramosPapeletaTractor,
                CantidadSINRemanenteLt = litrosPapeletaTractor,
                RemanenteKg = kilogramosRemanentes,
                RemanenteLt = litrosRemanentes,
                CantidadCONRemanenteKg = kilogramosRealesTractor,
                CantidadCONRemanenteLt = litrosRealesTractor,
            };
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

            if (unidadEntrada.EsAlterno)
            {
                almacen.CapacidadTotalLt = CalcularGasServicio.SumarLitros(almacen.CapacidadTotalLt, unidadEntrada.CapacidadTanqueLt.Value);
                almacen.CapacidadTotalKg = CalcularGasServicio.SumarKilogramos(almacen.CapacidadTotalKg, unidadEntrada.CapacidadTanqueKg.Value);
                almacen.PorcentajeActual = CalcularGasServicio.ObtenerPorcentajeDesdeLitros(almacen.CapacidadTotalLt, almacen.CantidadActualLt);
            }

            return almacen;
        }

        public static AlmacenGasMovimiento RegistrarMovimientoInventario(AplicaDescargaDto desDto)
        {
            return new AlmacenGasMovimiento
            {                
                IdTipoMovimiento = desDto,
                IdTipoEvento = desDto,
                IdOrdenVenta = desDto,
                IdAlmacenGas = desDto.AlmacenGas.IdAlmacenGas,
                IdCAlmacenGasPrincipal = desDto.unidadEntrada.IdCAlmacenGas,
                IdCAlmacenGasReferencia = desDto,
                IdAlmacenEntradaGasDescarga = desDto.Descarga.IdAlmacenEntradaGasDescarga,
                IdAlmacenGasRecarga = desDto,
                Hora = desDto,
                CAlmacenPrincipalNombre = desDto.unidadEntrada.Numero,
                CAlmacenReferenciaNombre = desDto.,
                OperadorChoferNombre = desDto.Descarga.NombreOperador,
                TipoEvento = desDto,
                TipoMovimiento = TipoMovimientoEnum.Descarga,
                RemanenteKg = desDto,
                RemanenteLt = desDto,
                EntradaKg = desDto,
                EntradaLt = desDto,
                SalidaKg = desDto,
                SalidaLt = desDto,
                CantidadActualKg = desDto,
                CantidadActualLt = desDto,
                CantidadAcumuladaDiaKg = desDto,
                CantidadAcumuladaDiaLt = desDto,
                CantidadAcumuladaMesKg = desDto,
                CantidadAcumuladaMesLt = desDto,
                CantidadAcumuladaAnioKg = desDto,
                CantidadAcumuladaAnioLt = desDto,
                FechaAplicacion = desDto,
                FechaRegistro = desDto,
                CantidadAnteriorGeneralKg = desDto,
                CantidadAnteriorGeneralLt = desDto,
                CantidadActualGeneralKg = desDto,
                CantidadActualGeneralLt = desDto,
                PorcentajeAnteriorGeneral = desDto,
                PorcentajeActualGeneral = desDto,
                CantidadAnteriorKg = desDto,
                CantidadAnteriorLt = desDto,
                CantidadActualTotalKg = desDto,
                CantidadActualTotalLt = desDto,
                PorcentajeAnterior = desDto,
                PorcentajeActual = desDto,
            };
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
            List<AlmacenGasTraspaso> traspasosGas = ObtenertraspasosNoProcesadas();

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
    }
}