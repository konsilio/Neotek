using Application.MainModule.DTOs.Mobile;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.Almacen;
using Exceptions.MainModule.Validaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Mobile
{
    public static class GasServicio
    {
        public static RespuestaDto EvaluarClaveOperacion(PapeletaDTO papeleta)
        {
            var alm = AlmacenGasServicio.ObtenerDescargaPorClaveOperacion(papeleta.ClaveOperacion);
            return EvaluarClaveOperacion(alm);
        }

        public static RespuestaDto EvaluarClaveOperacion(DescargaDto descarga)
        {
            var alm = AlmacenGasServicio.ObtenerDescargaPorClaveOperacion(descarga.ClaveOperacion);
            return EvaluarClaveOperacion(alm);
        }

        public static RespuestaDto EvaluarClaveOperacion(LecturaDTO lectura)
        {
            var alm = AlmacenGasServicio.ObtenerLecturaPorClaveOperacion(lectura.ClaveProceso);
            return EvaluarClaveOperacion(alm);
        }
        
        private static RespuestaDto EvaluarClaveOperacion<T>(T entidad)
        {
            return new RespuestaDto()
            {
                Exito = entidad != null ? true : false,
                Mensaje = entidad != null ? Exito.OK : string.Format(Error.M0002, "la clave de operación"),
            };
        }

        public static RespuestaDto EvaluarClaveOperacion(LecturaCamionetaDTO lcdto)
        {
            var alm = AlmacenGasServicio.ObtenerLecturaPorClaveOperacion(lcdto.ClaveProceso);
            return EvaluarClaveOperacion(alm);
        }

        public static RespuestaDto EvaluarClaveOperacion(RecargaDTO rdto)
        {
            var alm = AlmacenGasServicio.ObtenerRecargaPorClaveOperacion(rdto.ClaveOperacion);

            return EvaluarClaveOperacion(alm);
        }
    }
}
