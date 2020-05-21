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
using Sagas.MainModule.Entidades;
using Application.MainModule.Servicios.Seguridad;
using Application.MainModule.AdaptadoresDTO.Almacenes;

namespace Application.MainModule.Servicios.Mobile
{
    public class RecargaGasServicio
    {
        public static RespuestaDto EvaluarClaveOperacion(RecargaDTO rdto)
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

            var recarga = AlmacenGasServicio.InsertarRecargaGas(adapter);
            if (recarga.Exito)
            {
               // Actualizo el inventario despues de la recarga
                foreach (var reargaCilindro in adapter.Cilindros)
                {
                    CamionetaCilindro cilindro = new CamionetaCilindro();
                    cilindro.IdCamioneta = adapter.IdCAlmacenGasEntrada;
                    cilindro.IdEmpresa = TokenServicio.ObtenerIdEmpresa();
                    cilindro.IdCilindro = reargaCilindro.IdCilindro;
                    cilindro.Cantidad = reargaCilindro.Cantidad;
                    if (cilindro.Cantidad > 0)                    
                        AlmacenGasServicio.ActualizaCilindroCamioneta(cilindro);                    
                }               
            }
            return recarga;
        }

        public static RespuestaDto Recarga(RecargaDTO rdto, bool esFinal = false)
        {
            var adapter = AlmacenRecargaAdapter.FromDTOEvento(rdto);
            var almacen = AlmacenGasServicio.ObtenerAlmacen(rdto.IdCAlmacenGasEntrada);
            var almacenSalida = AlmacenGasServicio.ObtenerAlmacen(rdto.IdCAlmacenGasSalida);
            if (esFinal)
                if (almacen.IdPipa > 0)
                    adapter.IdCAlmacenGasSalida = rdto.IdCAlmacenGasEntrada;

            adapter.IdTipoEvento = esFinal ? TipoEventoEnum.Final : TipoEventoEnum.Inicial;
            adapter.DatosProcesados = false;
            adapter.FechaRegistro = DateTime.Now;
            adapter.FechaAplicacion = rdto.FechaAplicacion;          
            adapter.Fotografias = AlmacenRecargaAdapter.FromDTO(rdto.Imagenes);
            try
            {
                //Se actualiza el estatus actual del almacen
                almacen.P5000Actual = rdto.P5000Entrada;
                almacen.PorcentajeActual = rdto.ProcentajeEntrada;
                almacen.CantidadActualLt = CalcularGasServicio.ObtenerLitrosEnElTanque(almacen.CapacidadTanqueLt ?? 0, rdto.ProcentajeEntrada);
                almacen.CantidadActualKg = CalcularGasServicio.ObtenerKilogramosDesdeLitros(almacen.CantidadActualLt, TokenServicio.ObtenerEmprsaAplicacion().FactorLitrosAKilos);
                almacenSalida.P5000Actual = rdto.P5000Salida;
                almacenSalida.PorcentajeActual = rdto.ProcentajeSalida;
                almacenSalida.CantidadActualLt = CalcularGasServicio.ObtenerLitrosEnElTanque(almacenSalida.CapacidadTanqueLt ?? 0, rdto.ProcentajeSalida);
                almacenSalida.CantidadActualKg = CalcularGasServicio.ObtenerKilogramosDesdeLitros(almacenSalida.CantidadActualLt, TokenServicio.ObtenerEmprsaAplicacion().FactorLitrosAKilos);
                AlmacenGasServicio.ActualizarAlmacen(AlmacenGasAdapter.FromEntity(almacen));
                AlmacenGasServicio.ActualizarAlmacen(AlmacenGasAdapter.FromEntity(almacenSalida));
            }
            catch (Exception) { }
            return AlmacenGasServicio.InsertarRecargaGas(adapter);
        }
    }
}
