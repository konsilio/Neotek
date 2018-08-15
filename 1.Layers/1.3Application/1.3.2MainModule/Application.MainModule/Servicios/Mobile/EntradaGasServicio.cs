using Application.MainModule.AdaptadoresDTO.Mobile;
using Application.MainModule.DTOs.Mobile;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.Servicios.Almacen;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Mobile
{
    public static class EntradaGasServicio
    {
        public static RespuestaDto RegistrarPapeleta(AlmacenGasDescarga alm)
        {
            /********************/
            // Validar la clave de operación, si existe no hace nada de esto
            // Envía una respuesta positiva
            /********************/
            alm.FechaEntraGas = alm.FechaRegistro;
            alm.DatosProcesados = false;
            return new AlmacenGasDescargaDataAccess().Insertar(alm);
        }

        public static RespuestaDto Descargar(DescargaDto desDto, bool finDescarga = false)
        {
            var des = AlmacenGasServicio.ObtenerPorOCompraExpedidor(desDto.IdOrdenCompra);
            short numOrden = (short)(des.Fotos.Max(x => x.Orden) + 1);
            desDto.FechaDescarga = DateTime.Now;            

            var descarga = AlmacenAdapter.FromEntity(des);
            descarga = AlmacenAdapter.FromDto(desDto, finDescarga);
            var fotos = AlmacenAdapter.FromDto(desDto.Imagenes, numOrden);

            return new AlmacenGasDescargaDataAccess().Actualizar(descarga, fotos);
        }
    }
}
