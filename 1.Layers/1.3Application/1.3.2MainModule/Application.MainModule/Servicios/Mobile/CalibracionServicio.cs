using Application.MainModule.DTOs.Respuesta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.DTOs.Mobile;
using Application.MainModule.AdaptadoresDTO.Mobile;
using Sagas.MainModule.ObjetosValor.Enum;
using Application.MainModule.Servicios.Almacenes;
using Sagas.MainModule.Entidades;

namespace Application.MainModule.Servicios.Mobile
{
    public class CalibracionServicio
    {
        public static RespuestaDto EvaluarClaveOperacion(CalibracionDto dto)
        {
           return GasServicio.EvaluarClaveOperacion(dto);
        }

        public static RespuestaDto Calibracion(CalibracionDto dto, bool esFinal)
        {
            
            
            int IdOrden = ObtenerOrden();
            
            var adapter = CalibracionAdapter.FromDTO(dto, IdOrden);
            adapter.ClaveOperacion = dto.ClaveOperacion;
            adapter.IdTipoEvento = (esFinal) ? TipoEventoEnum.Final : TipoEventoEnum.Inicial;
            adapter.DatosProcesados = false;
            adapter.FechaRegistro = dto.FechaRegistro;
            adapter.FechaAplicacion = dto.FechaAplicacion;
           
            if (esFinal)
                adapter.IdDestinoCalibracion = (dto.IdDestino == 1) ? CalibracionDestinoEnum.MismoTanque : CalibracionDestinoEnum.TanquePortatil;
            var calibracion = AlmacenGasServicio.InsertarCalibracion(adapter);
            if (calibracion.Exito)
            {
                var unidadAlmacen = AlmacenGasServicio.ObtenerAlmacen(adapter.IdCAlmacenGas);
                //UnidadAlmacenGas almacenGas = new UnidadAlmacenGas() ;
               
                var almacenGas = CalibracionAdapter.FromDTOAlmacenCalibracion(unidadAlmacen,adapter);
                
                var respuestaDTO = AlmacenGasServicio.ActualizarCalibracionAlmacen(almacenGas);
                if (respuestaDTO.Exito)
                    return calibracion;
                else
                    return respuestaDTO;
            }
            return calibracion;
        }

        public static int ObtenerOrden()
        {
            var calibraciones = AlmacenGasServicio.ObtenerCalibracionesNoProcesadas();
            if (calibraciones != null)
                if (calibraciones.Count>  0)
                    return calibraciones.Count + 1;
                else
                    return 1;
            else
                return 1;
        }
    }
}
