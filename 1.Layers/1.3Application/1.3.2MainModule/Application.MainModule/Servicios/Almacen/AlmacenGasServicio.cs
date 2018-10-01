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
using Application.MainModule.Servicios.Seguridad;
using Application.MainModule.Servicios.Mobile;
using Application.MainModule.DTOs.Almacen;
using Application.MainModule.AdaptadoresDTO.Almacen;
using Application.MainModule.Servicios.Compras;
using Security.MainModule.Criptografia;
using Utilities.MainModule;
using Sagas.MainModule.ObjetosValor.Constantes;

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
        public static List<UnidadAlmacenGasCilindro> ObtenerCilindros()
        {
            return new AlmacenGasDataAccess().BuscarTodosCilindros(TokenServicio.ObtenerIdEmpresa());
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

        public static void ProcesarInventario()
        {
            //var lecturas = LecturaGasServicio.ObtenerTomaLectura();
            var descargasDto = AplicarDescargas();
            //var recargasDto = AplicarRecargas();

        }

        public static void CalcularInventarioDeUnidadAlmacenGas(UnidadAlmacenGas unidad)
        {            
            switch (IdentificarTipoUnidadAlamcenGas(unidad))
            {
                case identidadUnidadAlmacenGas.AlmacenPrincipal: CalcularInventarioAlmacenPrincipal(unidad); break;
                case identidadUnidadAlmacenGas.AlmacenAlterno: break;
                case identidadUnidadAlmacenGas.EstacionCarburacion: break;
                case identidadUnidadAlmacenGas.Pipa: break;
                case identidadUnidadAlmacenGas.Camioneta: break;
            }
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
                new AlmacenGasDescargaDataAccess().Actualizar(aplicaciones);
            }

            return aplicaciones;
        }

        public static List<AplicaRecargaDto> AplicarRecargas()
        {
            List<AplicaRecargaDto> aplicaciones = new List<AplicaRecargaDto>();
            List<AlmacenGasRecarga> recargasGas = ObtenerRecargasNoProcesadas();

            List<AlmacenGasRecarga> recargasGasIniciales = recargasGas.Where(x => x.TipoEvento.Equals(TipoEventoEnum.Inicial)).ToList();
            List<AlmacenGasRecarga> recargasGasFinales = recargasGas.Where(x => x.TipoEvento.Equals(TipoEventoEnum.Final)).ToList();

            if (recargasGasIniciales != null && recargasGasIniciales.Count > 0 && recargasGasFinales != null && recargasGasFinales.Count > 0)
            {                
                recargasGasIniciales.ForEach(x => aplicaciones.Add(AplicarRecarga(x, recargasGasFinales)));
                //new AlmacenGasRecargaDataAccess().Actualizar(aplicaciones);
            }

            return aplicaciones;
        }
        public static AplicaDescargaDto AplicarDescarga(AlmacenGasDescarga descarga)
        {
            Empresa empresa = EmpresaServicio.Obtener(descarga);
            UnidadAlmacenGas unidadEntrada = AlmacenGasServicio.ObtenerUnidadAlamcenGasActualizaAlterno(descarga, empresa);            

            AplicaDescargaDto apDescDto = AplicarDescarga(unidadEntrada, descarga, empresa);
            apDescDto = OrdenCompraServicio.AplicarDescarga(apDescDto, descarga, empresa);

            return apDescDto;
        }
        public static AplicaRecargaDto AplicarRecarga(AlmacenGasRecarga recargaInicial, List<AlmacenGasRecarga> recargasFinales)
        {
            var recargaFinal = recargasFinales.FirstOrDefault(x => x.IdCAlmacenGasEntrada.Equals(recargaInicial.IdCAlmacenGasEntrada));
            if (recargaFinal == null)
                return new AplicaRecargaDto();                 

            AplicaRecargaDto apReDto = new AplicaRecargaDto()
            {
                RecargaLecturaInicial = recargaInicial,
                RecargaLecturaFinal = recargaFinal,
                unidadSalida = AlmacenGasServicio.ObtenerUnidadAlamcenGas(recargaInicial, true),
                unidadEntrada = AlmacenGasServicio.ObtenerUnidadAlamcenGas(recargaInicial, false),                
            };

            Empresa empresa = EmpresaServicio.Obtener(apReDto.unidadEntrada);

            apReDto = AplicarRecarga(apReDto, empresa);

            return apReDto;
        }

        public static AplicaDescargaDto AplicarDescarga(UnidadAlmacenGas unidadEntrada, AlmacenGasDescarga descarga, Empresa empresa)
        {
            descarga.CapacidadTanqueKg = CalcularGasServicio.ObtenerKilogramosDesdeLitros(descarga.CapacidadTanqueLt.Value, empresa.FactorLitrosAKilos);

            decimal kilogramosPapeletaTractor = descarga.MasaKg.Value;
            decimal litrosPapeletaTractor = CalcularGasServicio.ObtenerLitrosDesdeKilos(kilogramosPapeletaTractor, empresa.FactorLitrosAKilos);
            decimal litrosRealesTractor = CalcularGasServicio.ObtenerLitrosEnElTanque(descarga.CapacidadTanqueLt.Value, descarga.PorcenMagnatelOcularTractorINI.Value);
            decimal kilogramosRealesTractor = CalcularGasServicio.ObtenerKilogramosDesdeLitros(litrosRealesTractor, empresa.FactorLitrosAKilos);
            decimal kilogramosRemanentes = CalcularGasServicio.ObtenerDiferenciaKilogramos(kilogramosRealesTractor, kilogramosPapeletaTractor);
            decimal litrosRemanentes = CalcularGasServicio.ObtenerLitrosDesdeKilos(kilogramosRemanentes, empresa.FactorLitrosAKilos);
            
            unidadEntrada.CantidadActualKg = CalcularGasServicio.SumarKilogramos(unidadEntrada.CantidadActualKg, kilogramosRealesTractor);
            unidadEntrada.CantidadActualLt = CalcularGasServicio.SumarLitros(unidadEntrada.CantidadActualLt, litrosRealesTractor);
            unidadEntrada.PorcentajeActual = descarga.PorcenMagnatelOcularAlmacenFIN.Value;

            unidadEntrada = AplicarDescargaAlmacenAlterno(unidadEntrada, descarga);

            return new AplicaDescargaDto()
            {
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

        public static AplicaRecargaDto AplicarRecarga(AplicaRecargaDto apReDto, Empresa empresa)
        {
            apReDto.identidadUS = IdentificarTipoUnidadAlamcenGas(apReDto.unidadSalida);
            apReDto.identidadUE = IdentificarTipoUnidadAlamcenGas(apReDto.unidadEntrada);

            switch (apReDto.identidadUE)
            {
                case identidadUnidadAlmacenGas.Pipa: apReDto = AplicarRecargaPipa(apReDto, empresa); break;
                case identidadUnidadAlmacenGas.EstacionCarburacion: break;
                case identidadUnidadAlmacenGas.Camioneta: break;
            }

            return apReDto;
        }

        public static AplicaRecargaDto AplicarRecargaPipa(AplicaRecargaDto apReDto, Empresa empresa)
        {
            //decimal porcentajeRecargado = CalcularGasServicio.ObtenerDiferenciaPorcentaje(apReDto.Recarga.ProcentajeEntrada);
            return null;
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
    }
}

      