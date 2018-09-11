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

        internal static RespuestaDto EvaluarClaveOperacion(LecturaCamionetaDTO lcdto)
        {
            return GasServicio.EvaluarClaveOperacion(lcdto);
        }

        public static RespuestaDto Lectura(LecturaCamionetaDTO lcdto,bool finalizar = false)
        {
            var adapter = AlmacenLecturaAdapter.FromDTO(lcdto);
            var al = AlmacenGasServicio.ObtenerLecturas(lcdto.IdCAlmacenGas);

            adapter.IdOrden = Orden(al);
            adapter.IdTipoEvento = finalizar ? TipoEventoEnum.Final : TipoEventoEnum.Inicial;
            
            adapter.Cilindros = AlmacenLecturaAdapter.FromDTO(lcdto.CilindroCantidad,lcdto.IdCilindro, adapter.IdCAlmacenGas, adapter.IdOrden);

            adapter.DatosProcesados = false;
            adapter.FechaRegistro = DateTime.Now;

            return AlmacenGasServicio.InsertarLectura(adapter);
        }
    }
}
