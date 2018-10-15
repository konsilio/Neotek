using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.DTOs.Mobile;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.Almacen;
using Sagas.MainModule.Entidades;
using Application.MainModule.AdaptadoresDTO.Mobile;
using Application.MainModule.Servicios.Seguridad;
using Sagas.MainModule.ObjetosValor.Enum;

namespace Application.MainModule.Servicios.Mobile
{
    public class AutoconsumoServicio
    {
        /// <summary>
        /// Permite verificar si el registro del autoconsumo ya se registro
        /// en la base de datos, se tomara como parametro el 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static RespuestaDto EvaluarAutoconsumo(AutoconsumoDTO dto)
        {
            return GasServicio.EvaluarClaveOperacion(dto);
        }

        public static RespuestaDto Autoconsumo(AutoconsumoDTO dto,bool esFinal)
        {
                var adapter = AlmacenAutoconsumoAdapter.FormDTO(dto);

                var almacenEntrada = AlmacenGasServicio.ObtenerAlmacen(dto.IdCAlmacenGasEntrada);
                var almacenSalida = AlmacenGasServicio.ObtenerAlmacen(dto.IdCAlmacenGasSalida);
                var almacenesAutoconsumo = AlmacenGasServicio.ObtenerAutoConsumosNoProcesadas();
            
                short IdOrden = (short) Orden(almacenesAutoconsumo);

                adapter.DatosProcesados = false;
                adapter.FechaRegistro = DateTime.Now;
                adapter.IdEmpresa = TokenServicio.ObtenerIdEmpresa();
                adapter.IdTipoEvento = esFinal ? TipoEventoEnum.Final : TipoEventoEnum.Inicial;
                adapter.Orden =  IdOrden;
                adapter.Dia = (byte)dto.FechaRegistro.Date.Day;
                adapter.Mes = (byte)dto.FechaRegistro.Month;
                adapter.Year = (short)dto.FechaRegistro.Year;
                adapter.Fotografias = AlmacenAutoconsumoAdapter.FormDTO(dto, almacenEntrada, almacenSalida,IdOrden,adapter.IdEmpresa);
                    
                return AlmacenGasServicio.InsertarAutoconsumo(adapter);//Falta realizar el registro por que no se encontro el data access
        }

        public static int Orden(List<AlmacenGasAutoConsumo> autoconsumos)
        {
            if (autoconsumos != null)
                if (autoconsumos.Count == 0)
                    return 1;
                else
                    return Convert.ToInt32(autoconsumos.FindLast(x => x.Orden > 0).Orden )+ 1;
            else
                return 1;
        }
    }
}
