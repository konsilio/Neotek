using Application.MainModule.DTOs.Mobile;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.Almacenes;
using Application.MainModule.Servicios.Catalogos;
using Exceptions.MainModule.Validaciones;
using Sagas.MainModule.Entidades;
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
        public static RespuestaDto EvaluarClaveOperacion(AutoconsumoDTO dto)
        {
            var autoconsumo = AlmacenGasServicio.ObtenerAutoconsumo(dto.ClaveOperacion);
            var resp = EvaluarClaveOperacion(autoconsumo);
            if (resp.Exito)
                resp.Id = autoconsumo.Orden;
            return resp;

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
        public static RespuestaDto EvaluarClaveOperacion(CalibracionDto dto)
        {
            var cal = AlmacenGasServicio.ObtenerCalibracion(dto.ClaveOperacion);
            return EvaluarClaveOperacion(cal);
        }
        public static RespuestaDto EvaluarClaveOperacion(TraspasoDto dto)
        {
            var tras = AlmacenGasServicio.ObtenerTraspaso(dto.ClaveOperacion);
            return EvaluarClaveOperacion(tras); 
        }
        public static RespuestaDto EvaluarClaveOperacion(AnticipoDto dto)
        {
            var anti = AlmacenGasServicio.ObtenerAnticipo(dto.ClaveOperacion);
            return EvaluarClaveOperacion(anti);
        }
        public static List<VentaCorteAnticipoEC> ObtenerAnticipos(short idEmpresa)
        {
            return AlmacenGasServicio.ObetnerAnticipos(idEmpresa); 
        }
        public static RespuestaDto Anticipo(VentaCorteAnticipoEC adapter)
        {
            return AlmacenGasServicio.InsertarAnticipo(adapter);
        }
        public static RespuestaDto EvaluarClaveOperacion(CorteDto dto)
        {
            var cort = AlmacenGasServicio.ObtenerCorte(dto.ClaveOperacion);
            return EvaluarClaveOperacion(cort);
        }
        public static List<VentaCorteAnticipoEC> ObtenerCortes(short idEmpresa)
        {
            return AlmacenGasServicio.ObtenerCortes(idEmpresa);
        }
        public static RespuestaDto Corte(VentaCorteAnticipoEC adapter)
        {
            return AlmacenGasServicio.InsertCorte(adapter);
        }
        public static RespuestaDto EvaluarClaveOperacion(VentaDTO venta)
        {
            var vent = PuntoVentaServicio.EvaluarFolio(venta.FolioVenta);
            return EvaluarClaveOperacion(vent);
        }
    }
}
