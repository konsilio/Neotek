/***
 * Clase para la lectura de entrada y salida 
 * Developer: Jorge Omar Tovar Martínez
 * Commpany: Neoteck
 * Date: 10/09/2018
 * Updated: 10/09/2018
 */
using System;
using Application.MainModule.DTOs.Mobile;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.Almacenes;
using Application.MainModule.AdaptadoresDTO.Mobile;
using Sagas.MainModule.ObjetosValor.Enum;
using Sagas.MainModule.Entidades;
using System.Collections.Generic;
using Application.MainModule.Servicios.Seguridad;
using System.Linq;
using Application.MainModule.Servicios.Catalogos;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.Servicios.Ventas;
using Application.MainModule.AdaptadoresDTO.Almacenes;

namespace Application.MainModule.Servicios.Mobile
{
    public static class LecturaGasServicio
    {
        internal static RespuestaDto EvaluarClaveOperacion(LecturaDTO ldto)
        {
            return GasServicio.EvaluarClaveOperacion(ldto);
        }

        public static RespuestaDto Lectura(LecturaDTO liadto, bool finalizar = false)
        {
            var adapter = AlmacenLecturaAdapter.FromDTO(liadto);
            var al = AlmacenGasServicio.ObtenerLecturas(liadto.IdCAlmacenGas);
            adapter.IdOrden = Orden(al);
            adapter.Fotografias = AlmacenLecturaAdapter.FromDTO(liadto.Imagenes, adapter.IdCAlmacenGas, adapter.IdOrden);
            adapter.IdTipoEvento = finalizar ? TipoEventoEnum.Final : TipoEventoEnum.Inicial;
            adapter.EsEncargadoAnden = false;
            adapter.EsEncargadoPuerta = false;
            adapter.DatosProcesados = false;
            adapter.FechaRegistro = DateTime.Now;
            var almacen = AlmacenGasServicio.ObtenerAlmacen(liadto.IdCAlmacenGas);
            try
            {
                if (finalizar && almacen.IdCamioneta == null)
                {
                    almacen.CantidadActualLt = CalcularGasServicio.ObtenerLitrosEnElTanque(almacen.CapacidadTanqueLt ?? 0, adapter.Porcentaje ?? 0);
                    almacen.CantidadActualKg = CalcularGasServicio.ObtenerKilogramosDesdeLitros(almacen.CantidadActualLt, TokenServicio.ObtenerEmprsaAplicacion().FactorLitrosAKilos);
                    AlmacenGasServicio.ActualizarAlmacen(AlmacenGasAdapter.FromEntity(almacen));
                }
            }
            catch (Exception) { }
            return AlmacenGasServicio.InsertarLectura(adapter);
        }

        private static int Orden(List<AlmacenGasTomaLectura> alm)
        {
            if (alm != null)
                if (alm.Count == 0)
                    return 1;
                else
                    return alm.FindLast(x => x.IdOrden > 0).IdOrden + 1;
            else
                return 1;
        }

        public static RespuestaDto EvaluarClaveOperacion(LecturaCamionetaDTO lcdto)
        {
            return GasServicio.EvaluarClaveOperacion(lcdto);
        }

        public static RespuestaDto Lectura(LecturaCamionetaDTO lcdto, bool finalizar = false)
        {
            var al = AlmacenGasServicio.ObtenerLecturas(lcdto.IdCAlmacenGas);
            var almacen = AlmacenGasServicio.ObtenerAlmacen(lcdto.IdCAlmacenGas);
            int idOrden = Orden(al);
            var adapter = AlmacenLecturaAdapter.FromDTO(lcdto, idOrden);

            adapter = EvaluarEsEncargadoPuerta(adapter, lcdto);
            adapter.IdTipoEvento = finalizar ? TipoEventoEnum.Final : TipoEventoEnum.Inicial;
            adapter.DatosProcesados = false;
            adapter.FechaRegistro = DateTime.Now;
            var lecturaCamioenta = AlmacenGasServicio.InsertarLectura(adapter);
            #region Actualizo los cilindros
            if (lecturaCamioenta.Exito)
            {
                var camioneta = AlmacenGasServicio.ObtenerAlmacen(lcdto.IdCAlmacenGas);
                foreach (AlmacenGasTomaLecturaCilindro cilindro in adapter.Cilindros)
                {
                    var camionetaCilindro = AlmacenGasServicio.BuscarCamionetaCilindro(camioneta.IdCamioneta.Value, cilindro.IdCilindro, almacen.IdEmpresa);
                    CamionetaCilindro camionetaCilindroActualizar = new CamionetaCilindro();
                    camionetaCilindroActualizar.IdCamioneta = camionetaCilindro.IdCamioneta;
                    camionetaCilindroActualizar.IdEmpresa = camionetaCilindro.IdEmpresa;
                    camionetaCilindroActualizar.IdCilindro = camionetaCilindro.IdCilindro;
                    camionetaCilindroActualizar.Cantidad = cilindro.Cantidad;
                    var actualizar = AlmacenGasServicio.ActualizaCilindro(camionetaCilindroActualizar);
                }
            }
            #endregion
            return lecturaCamioenta;
        }
        public static List<UnidadAlmacenGas> AcomodarUnidadAlmacenGasDelUsuario(List<UnidadAlmacenGas> alms)
        {
            var almacenes = new List<UnidadAlmacenGas>();
            var puntoVenta = PuntoVentaServicio.ObtenerPorUsuarioAplicacion();
            if (puntoVenta != null)
            {
                var almUsuario = alms.SingleOrDefault(x => x.IdCAlmacenGas.Equals(puntoVenta.IdCAlmacenGas));
                if (almUsuario != null)
                {
                    var i = alms.FindIndex(x => x.IdCAlmacenGas.Equals(puntoVenta.IdCAlmacenGas));
                    alms.RemoveAt(i);
                    almacenes.Add(almUsuario);
                }
            }

            almacenes.AddRange(alms);
            return almacenes;
        }

        /// <summary>
        /// Permite obtener si existe alguna lectura en la pipa , encaso de no existir  
        /// retornara una lista vacia 
        /// </summary>
        /// <param name="idPipa">Id de la pipa a buscar los registros</param>
        /// <param name="idCAlmacenGas">Id del CAlmacenGas </param>
        /// <returns>Listado encontrado de pipa en caso de no existir retorna una lista vacia</returns>
        public static List<AlmacenGasTomaLectura> ObtenerUltimaLecturasIniciales(short idCAlmacenGas)
        {
            return new AlmacenGasDataAccess().ObtenerUltimaLecturasIniciales(idCAlmacenGas);
        }

        public static List<UnidadAlmacenGas> AcomodarUltimaLectura(List<UnidadAlmacenGas> alms, bool esFinalizar)
        {
            int i = 0;
            foreach (var alm in alms)
            {
                var ultimalectura = AlmacenGasServicio.ObtenerUltimaLectura(alm, esFinalizar);
                if (ultimalectura != null)
                {
                    alms.ElementAt(i).P5000Actual = ultimalectura.P5000;
                    alms.ElementAt(i).PorcentajeActual = ultimalectura.Porcentaje ?? 0;
                }
            }

            return alms;
        }

        public static List<AlmacenGasTomaLectura> AcomodarUltimaLecturaCamioneta(List<UnidadAlmacenGas> alms, bool esFinalizar)
        {
            var lecturas = new List<AlmacenGasTomaLectura>();
            foreach (var alm in alms)
            {
                lecturas.Add(AlmacenGasServicio.ObtenerUltimaLectura(alm, esFinalizar));
            }

            return lecturas;
        }

        public static List<UnidadAlmacenGas> ObtenerAlamcenesGeneralConUltimaLectura(bool esFinalizar)
        {
            var alms = AlmacenGasServicio.ObtenerAlmacenGeneral(TokenServicio.ObtenerIdEmpresa(), true);

            return AcomodarUltimaLectura(alms, esFinalizar);
        }

        public static List<UnidadAlmacenGas> ObtenerEstacionesCarburacionConUltimaLectura(bool esFinalizar)
        {
            var alms = AlmacenGasServicio.ObtenerEstaciones(TokenServicio.ObtenerIdEmpresa());
            alms = AcomodarUnidadAlmacenGasDelUsuario(alms);

            return AcomodarUltimaLectura(alms, esFinalizar);
        }

        public static List<UnidadAlmacenGas> ObtenerPipaConUltimaLectura(bool esFinalizar)
        {
            var alms = AlmacenGasServicio.ObtenerPipas(TokenServicio.ObtenerIdEmpresa());
            alms = AcomodarUnidadAlmacenGasDelUsuario(alms);

            return AcomodarUltimaLectura(alms, esFinalizar);
        }

        public static List<UnidadAlmacenGas> ObtenerCamionetaConUltimaLectura(bool esFinalizar)
        {
            var alms = AlmacenGasServicio.ObtenerCamionetas(TokenServicio.ObtenerIdEmpresa());
            return AcomodarUnidadAlmacenGasDelUsuario(alms);
        }

        public static DatosTomaLecturaDto ConsultaDatosTomaLecturaAlmacenGeneral(bool esFinalizar)
        {
            var medidores = TipoMedidorGasServicio.Obtener();
            // Falta considerar el alamcen alterno
            var alms = ObtenerAlamcenesGeneralConUltimaLectura(esFinalizar);
            return AlmacenLecturaAdapter.ToDto(alms, medidores);
        }

        public static DatosTomaLecturaDto ConsultaDatosTomaLecturaEstacionCarburacion(bool esFinalizar)
        {
            var medidores = TipoMedidorGasServicio.Obtener();
            var alms = ObtenerEstacionesCarburacionConUltimaLectura(esFinalizar);
            return AlmacenLecturaAdapter.ToDto(alms, medidores);
        }

        public static DatosTomaLecturaDto ConsultaDatosTomaLecturaPipa(bool esFinalizar)
        {
            var medidores = TipoMedidorGasServicio.Obtener();
            var alms = ObtenerPipaConUltimaLectura(esFinalizar);
            return AlmacenLecturaAdapter.ToDto(alms, medidores);
        }

        public static DatosTomaLecturaDto ConsultaDatosTomaLecturaCamioneta(bool esFinalizar)
        {
            var medidores = TipoMedidorGasServicio.Obtener();
            var alms = ObtenerCamionetaConUltimaLectura(esFinalizar);
            var lecturas = AcomodarUltimaLecturaCamioneta(alms, esFinalizar);
            return AlmacenLecturaAdapter.ToDto(alms, lecturas, medidores);
        }

        private static AlmacenGasTomaLectura EvaluarEsEncargadoPuerta(AlmacenGasTomaLectura alm, LecturaCamionetaDTO lcDto)
        {
            alm.EsEncargadoPuerta = lcDto.EsEncargadoPuerta;
            alm.EsEncargadoAnden = !lcDto.EsEncargadoPuerta;

            return alm;
        }
        public static DatosTomaLecturaDto ConsultaDatosReporteDelDia()
        {
            var alms = AlmacenGasServicio.ObtenerAlmacenes(TokenServicio.ObtenerIdEmpresa());
            return AlmacenLecturaAdapter.ToDtoReporte(alms);
        }

        private static List<UnidadAlmacenGas> ObtenerAlmacenesGas()
        {
            var alm = AlmacenGasServicio.ObtenerAlmacenes(TokenServicio.ObtenerIdEmpresa());
            var acom = AcomodarUnidadAlmacenGasDelUsuario(alm);
            return acom;
        }

        /// <summary>
        /// Permite obtener la última lectura de una estación, se envia de parametros su 
        /// Id de calmacen gas y una fecha como parametro la cual por defecto tiene la fecha actual  
        /// </summary>
        /// <param name="idCAlmacenGas">Id de CAlmacenGas</param>
        /// <param name="fecha">Fecha actual en la que se desa buscar.Nota:solo se tomara dd/mm/YYYY para la busqueda</param>
        /// <returns>Entidad AlmacenGasTomaLectura con el valor encontrado</returns>
        public static AlmacenGasTomaLectura ObtenerUltimaLecturaInicial(short idCAlmacenGas, DateTime fecha)
        {
            return new AlmacenGasDataAccess().ObtenerUltimaLecturaInicial(idCAlmacenGas, fecha);
        }
        public static AlmacenGasTomaLectura ObtenerUltimaLecturaFinal(short idCAlmacenGas, DateTime fecha)
        {
            return new AlmacenGasDataAccess().ObtenerUltimaLecturaFinal(idCAlmacenGas, fecha);
        }
        public static decimal ObtenerKilosGasCamioneta(short idCAlmacenGas, DateTime fecha, int idPuntoVenta)
        {
            decimal TotalKilosGas = 0;
            decimal TotalKilosVenta = 0;
            var lecturaInical = ObtenerUltimaLecturaInicial(idCAlmacenGas, fecha);
            var catCilindros = AlmacenGasServicio.ObtenerCilindros();
            foreach (var cilindro in lecturaInical.Cilindros)
            {
                TotalKilosGas += cilindro.Cantidad * catCilindros.SingleOrDefault(x => x.IdCilindro.Equals(cilindro.IdCilindro)).CapacidadKg;
            }
            var Ventas = CajaGeneralServicio.ObtenerVentasPuntosVenta(idPuntoVenta);
            foreach (var venta in Ventas)
            {
                TotalKilosVenta += venta.VentaPuntoDeVentaDetalle.Sum(x => x.CantidadKg ?? 0);
            }
            return CalcularPreciosVentaServicio.ObtenerKilosCamioneta(TotalKilosGas, TotalKilosVenta);
        }
    }
}
