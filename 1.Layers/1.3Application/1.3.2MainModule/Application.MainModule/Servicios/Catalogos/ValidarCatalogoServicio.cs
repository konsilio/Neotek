﻿using Application.MainModule.DTOs.Catalogo;
using Application.MainModule.DTOs.Respuesta;
using Exceptions.MainModule.Validaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Catalogos
{
    public static class ValidarCatalogoServicio
    {
        #region Centro de costos
        public static RespuestaDto CentroCosto(CentroCostoCrearDto ccDto, bool esModificacion = false)
        {
            var respuesta = new RespuestaDto() { Exito = true, ModeloValido = true, MensajesError = new List<string>() };
            //bool unidadAsignada = false;
            List<string> unidadMensaje = new List<string>();
            // Existencia            
            if (CentroCostoServicio.Existe(ccDto.Numero, ccDto.Descripcion))
            {
                respuesta.Exito = false;
                respuesta.ModeloValido = false;
                respuesta.MensajesError.Add(string.Format(Error.C0005, "El número", "Centro de Costos"));
            }

            if (!esModificacion)
            {
                if (ccDto.IdCAlmacenGas != 0)
                    unidadMensaje.Add("Unidad de alamcen de Gas");

                if (ccDto.IdCamioneta != 0)
                    unidadMensaje.Add("Camioneta");


                if (ccDto.IdCilindro != 0)
                    unidadMensaje.Add("Cilindros de gas");

                if (ccDto.IdEquipoTransporte != 0)
                    unidadMensaje.Add("Equipo de transporte");

                if (ccDto.IdEstacionCarburacion != 0)
                    unidadMensaje.Add("Estación de carburación");

                if (ccDto.IdPipa != 0)
                    unidadMensaje.Add("Pipa");

                if (ccDto.IdVehiculoUtilitario != 0 )
                    unidadMensaje.Add("Vehiculo utilitario");

                //if (ccDto.IdCAlmacenGas != 0  && unidadAsignada.Equals(false)) unidadAsignada = true;
                //else if (unidadAsignada) unidadMensaje = string.Concat(unidadMensaje, " Unidad de alamcen de Gas");

                //if (ccDto.IdCamioneta != 0 && unidadAsignada.Equals(false)) unidadAsignada = true;
                //else if (unidadAsignada) unidadMensaje = string.Concat(unidadMensaje, ", Camioneta");

                //if (ccDto.IdCilindro != 0 && unidadAsignada.Equals(false)) unidadAsignada = true;
                //else if (unidadAsignada) unidadMensaje = string.Concat(unidadMensaje, ", Cilindros de gas");

                //if (ccDto.IdEquipoTransporte != 0 && unidadAsignada.Equals(false)) unidadAsignada = true;
                //else if (unidadAsignada) unidadMensaje = string.Concat(unidadMensaje, ", Equipo de transporte");

                //if (ccDto.IdEstacionCarburacion != 0 && unidadAsignada.Equals(false)) unidadAsignada = true;
                //else if (unidadAsignada) unidadMensaje = string.Concat(unidadMensaje, ", Estación de carburación");

                //if (ccDto.IdPipa != 0 && unidadAsignada.Equals(false)) unidadAsignada = true;
                //else if (unidadAsignada) unidadMensaje = string.Concat(unidadMensaje, ", Pipa");

                //if (ccDto.IdVehiculoUtilitario != 0 && unidadAsignada.Equals(false)) unidadAsignada = true;
                //else if (unidadAsignada) unidadMensaje = string.Concat(unidadMensaje, ", Vehiculo utilitario");

                //if (!string.IsNullOrEmpty(unidadMensaje))
                if (unidadMensaje.Count > 1)
                {
                    respuesta.Exito = false;
                    respuesta.ModeloValido = false;
                    string unidades = string.Empty;
                    foreach (var item in unidadMensaje)
                    {
                        if (string.IsNullOrEmpty(unidades))
                            unidades = item;
                        else
                            unidades = string.Concat(unidades, ", ", item);
                    } 
                    respuesta.MensajesError.Add(string.Format(Error.C0006, unidades));
                }
            }

            return respuesta;
        }
        #endregion

        #region Productos
        public static RespuestaDto CategoriaProducto(CategoriaProductoCrearDto cpDto)
        {
            var respuesta = new RespuestaDto() { Exito = true, ModeloValido = true, MensajesError = new List<string>() };
            // Existencia   
            if (ProductoServicio.ExisteCategoria(cpDto.Nombre))
            {
                respuesta.Exito = false;
                respuesta.ModeloValido = false;
                respuesta.MensajesError.Add(string.Format(Error.C0005, "La categoría de producto", string.Empty));
            }
            return respuesta;
        }

        public static RespuestaDto CategoriaProducto(CategoriaProductoModificarDto cpDto)
        {
            var respuesta = new RespuestaDto() { Exito = true, ModeloValido = true, MensajesError = new List<string>() };
            // Existencia   
            if (ProductoServicio.ExisteCategoria(cpDto.Nombre, cpDto.IdCategoria))
            {
                respuesta.Exito = false;
                respuesta.ModeloValido = false;
                respuesta.MensajesError.Add(string.Format(Error.C0005, "La categoría de producto", string.Empty));
            }
            return respuesta;
        }


        public static RespuestaDto LineaProducto(LineaProductoCrearDto lpDto)
        {
            var respuesta = new RespuestaDto() { Exito = true, ModeloValido = true, MensajesError = new List<string>() };
            // Existencia                    
            if (ProductoServicio.ExisteLinea(lpDto.Linea))
            {
                respuesta.Exito = false;
                respuesta.ModeloValido = false;
                respuesta.MensajesError.Add(string.Format(Error.C0005, "La línea del productos", "Línea de producto"));
            }

            return respuesta;
        }
        public static RespuestaDto LineaProducto(LineaProductoModificarDto lpDto, bool esModificacion = false)
        {
            var respuesta = new RespuestaDto() { Exito = true, ModeloValido = true, MensajesError = new List<string>() };
            // Existencia                    
            if (ProductoServicio.ExisteLinea(lpDto.Linea, lpDto.IdProductoLinea))
            {
                respuesta.Exito = false;
                respuesta.ModeloValido = false;
                respuesta.MensajesError.Add(string.Format(Error.C0005, "La línea del productos", "Línea de producto"));
            }

            return respuesta;
        }

        public static RespuestaDto UnidadMedida(UnidadMedidaCrearDto uMDto, bool esModificacion = false)
        {
            var respuesta = new RespuestaDto() { Exito = true, ModeloValido = true, MensajesError = new List<string>() };
            // Existencia            
            if (ProductoServicio.ExisteUnidadMedida(uMDto.Nombre, uMDto.Acronimo))
            {
                respuesta.Exito = false;
                respuesta.ModeloValido = false;
                respuesta.MensajesError.Add(string.Format(Error.C0005, "El nombre o acrónimo", "unidad de medida"));
            }

            return respuesta;
        }
        public static RespuestaDto UnidadMedida(UnidadMedidaModificarDto uMDto, bool esModificacion = false)
        {
            var respuesta = new RespuestaDto() { Exito = true, ModeloValido = true, MensajesError = new List<string>() };
            // Existencia            
            if (ProductoServicio.ExisteUnidadMedida(uMDto.Nombre, uMDto.Acronimo, uMDto.IdUnidadMedida))
            {
                respuesta.Exito = false;
                respuesta.ModeloValido = false;
                respuesta.MensajesError.Add(string.Format(Error.C0005, "El nombre o acrónimo", "unidad de medida"));
            }

            return respuesta;
        }

        public static RespuestaDto Producto(ProductoCrearDto pDto, bool esModificacion = false)
        {
            var respuesta = new RespuestaDto() { Exito = true, ModeloValido = true, MensajesError = new List<string>() };

            if (pDto.Minimos.Value > pDto.Maximo.Value)
            {
                respuesta.Exito = false;
                respuesta.ModeloValido = false;
                respuesta.MensajesError.Add(string.Format(Error.CP0001, pDto.Minimos.Value, pDto.Maximo.Value));
            }

            if (pDto.EsTransporteGas && (pDto.EsActivoVenta || pDto.EsGas))
            {
                respuesta.Exito = false;
                respuesta.ModeloValido = false;
                respuesta.MensajesError.Add(string.Format(Error.CP0002, pDto.Minimos.Value, pDto.Maximo.Value));
            }

            return respuesta;
        }
        #endregion
    }
}
