using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.DTOs.Mobile;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.AdaptadoresDTO.Mobile;
using Sagas.MainModule.ObjetosValor.Enum;
using Application.MainModule.Servicios.Almacenes;

namespace Application.MainModule.Servicios.Mobile
{
    public class TraspasoServicio
    {
        public static RespuestaDto EvaluarClaveOperacion(TraspasoDto dto)
        {
            return GasServicio.EvaluarClaveOperacion(dto);
        }

        public static RespuestaDto Traspaso(TraspasoDto dto, bool esFinal,short IdEmpresa)
        {
            short orden =  (short)ObtenerOrden();
            var adapter = AlmacenTraspasoAdapter.FromDTO(dto,orden,IdEmpresa);
            
            adapter.ClaveOperacion = dto.ClaveOperacion;
            adapter.IdTipoEvento = esFinal ? TipoEventoEnum.Final:TipoEventoEnum.Inicial;
            adapter.Dia = (byte) dto.FechaRegistro.Day;
            adapter.Mes = (byte)dto.FechaRegistro.Month;
            adapter.Year = (short)dto.FechaRegistro.Year;
            adapter.FechaRegistro = DateTime.Now;
            adapter.FechaAplicacion = dto.FechaAplicacion;
            adapter.DatosProcesados = false;

            return AlmacenGasServicio.InsertarTraspaso(adapter);//Queda pendiente ver el metodo que se utilizara para el traspaso
        }

        public static int ObtenerOrden()
        {
            var traspasos = AlmacenGasServicio.ObtenerTraspasosNoProcesadas();
            if (traspasos != null)
                if (traspasos.Count != 0)
                    return traspasos.FindLast(x => x.Orden>0).Orden+1;
                else
                    return 1;
            else
                return 1;
        }
    }
}
