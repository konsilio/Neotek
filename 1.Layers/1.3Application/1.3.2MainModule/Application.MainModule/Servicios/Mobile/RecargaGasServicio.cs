/***
 * Clase servicio para la recarga de gas 
 * Developer: Jorge Omar Tovar Martínez
 * Commpany: Neoteck
 * Date: 14/09/2018
 * Updated: 14/09/2018
 */
using System;
using Application.MainModule.DTOs.Mobile;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.Almacenes;
using Application.MainModule.AdaptadoresDTO.Mobile;
using Sagas.MainModule.ObjetosValor.Enum;

namespace Application.MainModule.Servicios.Mobile
{
    public class RecargaGasServicio
    {
        public  static RespuestaDto  EvaluarClaveOperacion(RecargaDTO rdto)
        {
            return GasServicio.EvaluarClaveOperacion(rdto);
        }

        public static RespuestaDto Recarga(RecargaDTO rdto)
        {
            var adapter = AlmacenRecargaAdapter.FromDTO(rdto);
            adapter.Cilindros = AlmacenRecargaAdapter.FromDTOCilindros(rdto);
            adapter.IdTipoEvento = TipoEventoEnum.Inicial;
            adapter.DatosProcesados = false;
            adapter.FechaRegistro = DateTime.Now;
            adapter.FechaAplicacion = rdto.FechaAplicacion;

            return AlmacenGasServicio.InsertarRecargaGas(adapter);
        }

        public static RespuestaDto Recarga(RecargaDTO rdto,bool esFinal = false)
        {
            var adapter = AlmacenRecargaAdapter.FromDTOEvento(rdto);
            var almacen = AlmacenGasServicio.ObtenerAlmacen(rdto.IdCAlmacenGasEntrada);
            if (esFinal)
                if (almacen.IdPipa > 0)
                    adapter.IdCAlmacenGasSalida = rdto.IdCAlmacenGasEntrada;

            adapter.IdTipoEvento =  esFinal? TipoEventoEnum.Final:TipoEventoEnum.Inicial;
            adapter.DatosProcesados = false;
            adapter.FechaRegistro = DateTime.Now;
            adapter.FechaAplicacion = rdto.FechaAplicacion;
            adapter.Fotografias = AlmacenRecargaAdapter.FromDTO(rdto.Imagenes);

            return AlmacenGasServicio.InsertarRecargaGas(adapter);
        }
    }
}
