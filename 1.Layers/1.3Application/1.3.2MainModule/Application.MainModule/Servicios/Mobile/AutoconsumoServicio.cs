using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.DTOs.Mobile;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.Almacenes;
using Sagas.MainModule.Entidades;
using Application.MainModule.AdaptadoresDTO.Mobile;
using Application.MainModule.Servicios.Seguridad;
using Sagas.MainModule.ObjetosValor.Enum;
using Exceptions.MainModule.Validaciones;
using Utilities.MainModule;
using Application.MainModule.Servicios.AccesoADatos;

namespace Application.MainModule.Servicios.Mobile
{
    public class AutoconsumoServicio
    {
        public static RespuestaDto EvaluarAutoconsumo(AutoconsumoDTO dto)
        {
            return GasServicio.EvaluarClaveOperacion(dto);
        }

        public static ReporteDiaDTO Autoconsumo(AutoconsumoDTO dto, bool esFinal)
        {
            var adapter = AlmacenAutoconsumoAdapter.FormDTO(dto);
            var almacenEntrada = AlmacenGasServicio.ObtenerAlmacen(dto.IdCAlmacenGasEntrada);
            UnidadAlmacenGas almacenSalida = new UnidadAlmacenGas();
            if (dto.IdCAlmacenGasSalida == 0)
            {
                almacenSalida = AlmacenGasServicio.ObtenerAlmacenPrincipal(TokenServicio.ObtenerIdEmpresa());
                dto.IdCAlmacenGasSalida = almacenSalida.IdCAlmacenGas;
            }
            else
                almacenSalida = AlmacenGasServicio.ObtenerAlmacen(dto.IdCAlmacenGasSalida);
            
            //var almacenesAutoconsumo = AlmacenGasServicio.ObtenerAutoConsumosNoProcesadas();
            short IdOrden = Orden(dto);
            adapter.DatosProcesados = false;
            adapter.FechaRegistro = DateTime.Now;
            adapter.IdEmpresa = TokenServicio.ObtenerIdEmpresa();
            adapter.IdTipoEvento = esFinal ? TipoEventoEnum.Final : TipoEventoEnum.Inicial;
            adapter.Orden = IdOrden;
            adapter.Dia = (byte)dto.FechaRegistro.Date.Day;
            adapter.Mes = (byte)dto.FechaRegistro.Month;
            adapter.Year = (short)dto.FechaRegistro.Year;
            adapter.IdCAlmacenGasSalida = almacenSalida.IdCAlmacenGas;
            adapter.Fotografias = AlmacenAutoconsumoAdapter.FormDTO(dto, almacenEntrada, almacenSalida, IdOrden, adapter.IdEmpresa);
            adapter.FechaAplicacion = dto.FechaAplicacion;
            if (esFinal)
            {
                var AutoCosnumoInicial = AlmacenGasServicio.ObtenerAutoconsumoInicial(adapter);
                if (AutoCosnumoInicial == null)
                    return new ReporteDiaDTO() { Exito = false, Mensaje = Error.AC0001, };
                else if (DeterminaRelacion(AutoCosnumoInicial))
                    return new ReporteDiaDTO() { Exito = false, Mensaje = Error.AC0001, };
                adapter.OrdenRelacion = AutoCosnumoInicial.Orden;
                var respAuto = AlmacenGasServicio.InsertarAutoconsumo(adapter);
                if (!respAuto.Exito)
                    return new ReporteDiaDTO() { Exito = respAuto.Exito, Mensaje = respAuto.Mensaje };
                else
                    return AutoConsumo(adapter);

            }
            var RespInicial = AlmacenGasServicio.InsertarAutoconsumo(adapter);
            return new ReporteDiaDTO() { Exito = RespInicial.Exito, Mensaje = RespInicial.Mensaje };
        }
        public static ReporteDiaDTO AutoConsumo(AlmacenGasAutoConsumo final)
        {
            var AutoconsumoFinal = AlmacenGasServicio.ObtenerAutoconsumo(final.ClaveOperacion);
            var AutoConsumoInicial = AlmacenGasServicio.ObtenerAutoconsumoInicial(AutoconsumoFinal);
            if (AutoConsumoInicial != null)
            {
                return new ReporteDiaDTO()
                {
                    Exito = true,
                    Mensaje = Exito.OK,
                    LecturaInicial = new LecturaAlmacenDto { ClaveProceso = AutoConsumoInicial.ClaveOperacion, CantidadP5000 = AutoConsumoInicial.P5000Salida },
                    LecturaFinal = new LecturaAlmacenDto { ClaveProceso = AutoconsumoFinal.ClaveOperacion, CantidadP5000 = AutoconsumoFinal.P5000Salida },
                    LitrosVenta = CalculosGenerales.DiferenciaEntreDosNumero(AutoconsumoFinal.P5000Salida, AutoConsumoInicial.P5000Salida),
                    NombreCAlmacen = AutoconsumoFinal.UnidadEntrada.Numero,
                    Estacion = AutoconsumoFinal.UnidadSalida.Numero,
                    ClaveReporte = AutoconsumoFinal.ClaveOperacion,
                };
            }
            else
            {
                return new ReporteDiaDTO()
                {
                    Exito = false,
                    Mensaje = Error.AC0001
                };
            }
        }
        public static short Orden(AutoconsumoDTO dto)
        {
            var autoconsumosDelDia = AlmacenGasServicio.ObtenerAutoconsumo(DateTime.Now);
            if (autoconsumosDelDia != null && autoconsumosDelDia.Count > 0)
                return (short)(autoconsumosDelDia.OrderByDescending(x => x.Orden).FirstOrDefault().Orden + 1);
            else
                return 1;
        }
        public static bool DeterminaRelacion(AlmacenGasAutoConsumo dto)
        {
            return new AlmacenGasDataAccess().BuscarSiEstaRelacionado(dto);
        }
    }
}
