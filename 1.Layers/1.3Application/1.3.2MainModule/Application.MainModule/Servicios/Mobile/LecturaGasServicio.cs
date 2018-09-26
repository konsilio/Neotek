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
using Application.MainModule.Servicios.Almacen;
using Application.MainModule.AdaptadoresDTO.Mobile;
using Sagas.MainModule.ObjetosValor.Enum;
using Sagas.MainModule.Entidades;
using System.Collections.Generic;
using Application.MainModule.Servicios.Seguridad;
using System.Linq;
using Application.MainModule.Servicios.Catalogos;

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
            adapter.Fotografias = AlmacenLecturaAdapter.FromDTO(liadto.Imagenes,adapter.IdCAlmacenGas,adapter.IdOrden);
            
            adapter.IdTipoEvento = finalizar ? TipoEventoEnum.Final: TipoEventoEnum.Inicial;
            adapter.EsEncargadoAnden = false;
            adapter.EsEncargadoPuerta = false;
            adapter.DatosProcesados = false;
            adapter.FechaRegistro = DateTime.Now;
            
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

        public static RespuestaDto Lectura(LecturaCamionetaDTO lcdto,bool finalizar = false)
        {
            
            var al = AlmacenGasServicio.ObtenerLecturas(lcdto.IdCAlmacenGas);
            int idOrden = Orden(al);
            var adapter = AlmacenLecturaAdapter.FromDTO(lcdto,idOrden);

            adapter = EvaluarEsEncargadoPuerta(adapter, lcdto);

            adapter.IdTipoEvento = finalizar ? TipoEventoEnum.Final : TipoEventoEnum.Inicial;            
            adapter.DatosProcesados = false;
            adapter.FechaRegistro = DateTime.Now;

            return AlmacenGasServicio.InsertarLectura(adapter);
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

        public static List<UnidadAlmacenGas> AcomodarUltimaLectura(List<UnidadAlmacenGas> alms, bool esFinalizar)
        {
            int i = 0;
            foreach (var alm in alms)
            {
                var ultimalectura = AlmacenGasServicio.ObtenerUltimaLectura(alm, esFinalizar);
                if (ultimalectura != null)
                {
                    alms.ElementAt(i).P5000Actual = ultimalectura.P5000;
                    alms.ElementAt(i).PorcentajeActual = ultimalectura.Porcentaje.Value;                 
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
            var alms = ObtenerAlmacenesGas();
            return AlmacenLecturaAdapter.ToDtoReporte(alms);
        }

        private static List<UnidadAlmacenGas>  ObtenerAlmacenesGas()
        {
            var alm = AlmacenGasServicio.ObtenerAlmacenes(TokenServicio.ObtenerIdEmpresa());
            var acom = AcomodarUnidadAlmacenGasDelUsuario(alm);
            return acom;
        }
    }
}
