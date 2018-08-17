﻿using Application.MainModule.AdaptadoresDTO.Mobile;
using Application.MainModule.DTOs.Mobile;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.Almacen;
using Exceptions.MainModule.Validaciones;
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
            alm.FechaEntraGas = alm.FechaRegistro;
            alm.DatosProcesados = false;
            return AlmacenGasServicio.Insertar(alm);
        }

        public static RespuestaDto Descargar(DescargaDto desDto, bool finDescarga = false)
        {
            var des = AlmacenGasServicio.ObtenerDescargaPorOCompraExpedidor(desDto.IdOrdenCompra);
            short numOrden = (short)(des.Fotos.Max(x => x.Orden) + 1);
            desDto.FechaDescarga = DateTime.Now;            

            var descarga = AlmacenAdapter.FromEntity(des);
            descarga = AlmacenAdapter.FromDto(desDto, descarga, finDescarga);
            var fotos = AlmacenAdapter.FromDto(desDto.Imagenes, descarga.IdAlmacenEntradaGasDescarga, numOrden);

            return AlmacenGasServicio.Actualizar(descarga, fotos);
        }

        public static RespuestaDto EvaluarClaveOperacion(DescargaDto dto)
        {
            return EvaluarClaveOperacion(dto.ClaveOperacion);
        }

        public static RespuestaDto EvaluarClaveOperacion(PapeletaDTO dto)
        {
            return EvaluarClaveOperacion(dto.ClaveOperacion);
        }
        
        public static RespuestaDto EvaluarExistenciaRegistro(PapeletaDTO dto)
        {
            return EvaluarClaveOperacion(dto.ClaveOperacion);
        }

        public static RespuestaDto EvaluarExistenciaRegistro(DescargaDto dto)
        {
            return EvaluarClaveOperacion(dto.ClaveOperacion);
        }

        private static RespuestaDto EvaluarClaveOperacion(string claveOperacion)
        {
            var alm = AlmacenGasServicio.ObtenerDescargaPorClaveOperacion(claveOperacion);
            return new RespuestaDto()
            {
                Exito = alm != null ? true : false,
                Mensaje = alm != null ? Exito.OK : string.Format(Error.M0002, "la clave de operación"),
            };
        }

        private static RespuestaDto EvaluarExistenciaRegistro(int idOCompra)
        {
            var alm = AlmacenGasServicio.ObtenerDescargaPorOCompraExpedidor(idOCompra);
            return new RespuestaDto()
            {
                Exito = alm != null ? true : false,
                Mensaje = alm != null ? Exito.OK : string.Format(Error.M0002, "la descarga"),
            };
        }
    }
}
