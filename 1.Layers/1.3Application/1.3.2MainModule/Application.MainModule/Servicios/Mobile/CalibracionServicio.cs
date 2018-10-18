using Application.MainModule.DTOs.Respuesta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.DTOs.Mobile;
using Application.MainModule.AdaptadoresDTO.Mobile;
using Sagas.MainModule.ObjetosValor.Enum;
using Application.MainModule.Servicios.Almacen;

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
            if (esFinal)
                adapter.IdDestinoCalibracion = (dto.IdDestino == 1) ? CalibracionDestinoEnum.MismoTanque : CalibracionDestinoEnum.TanquePortatil;
            adapter.FechaRegistro = dto.FechaRegistro;

            return AlmacenGasServicio.InsertarCalibracion(adapter);
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
