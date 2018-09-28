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

namespace Application.MainModule.Servicios.Almacen
{
    public static class AlmacenGasServicio
    {
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
        public static UnidadAlmacenGas ObtenerUnidadAlamcenGas(AlmacenGasDescarga descarga)
        {
            if (descarga != null)
                if (descarga.UnidadAlmacen != null)
                    return descarga.UnidadAlmacen;

            return ObtenerUnidadAlamcenGas(descarga.IdCAlmacenGas.Value);
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
        /**********************************************/
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
            List<AlmacenGasDescarga> descargasGas = ObtenerDescargasTodas();

            if (descargasGas != null && descargasGas.Count > 0)
            {
                descargasGas.ForEach(x => aplicaciones.Add(AplicarDescarga(x)));
                new AlmacenGasDescargaDataAccess().Actualizar(aplicaciones);
            }

            return aplicaciones;
        }

        public static AplicaDescargaDto AplicarDescarga(AlmacenGasDescarga descarga)
        {
            UnidadAlmacenGas unidadEntrada = AlmacenGasServicio.ObtenerUnidadAlamcenGas(descarga);
            Empresa empresa = EmpresaServicio.Obtener(unidadEntrada);

            AplicaDescargaDto apDescDto = AplicarDescarga(unidadEntrada, descarga, empresa);
            apDescDto = OrdenCompraServicio.AplicarDescarga(apDescDto, descarga, empresa);

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

            unidadEntrada.CantidadActualKg = CalcularGasServicio.SumarKilogramos(unidadEntrada.CantidadActualKg, kilogramosRealesTractor);
            unidadEntrada.CantidadActualLt = CalcularGasServicio.ObtenerLitrosDesdeKilos(unidadEntrada.CantidadActualKg, empresa.FactorLitrosAKilos);
            unidadEntrada.PorcentajeActual = descarga.PorcenMagnatelOcularAlmacenFIN.Value;

            return new AplicaDescargaDto()
            {
                Descarga = descarga,
                DescargaSinNavigationProperties = AlmacenGasAdapter.FromEmtity(descarga),
                unidadEntrada = AlmacenGasAdapter.FromEmtity(unidadEntrada),
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
    }
}