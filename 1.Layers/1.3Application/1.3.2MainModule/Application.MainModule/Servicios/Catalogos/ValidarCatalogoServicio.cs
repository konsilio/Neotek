using Application.MainModule.DTOs.Catalogo;
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
            bool unidadAsignada = false;
            string unidadMensaje = string.Empty;
            // Existencia            
            if (CentroCostoServicio.Existe(ccDto.Numero, ccDto.Descripcion))
            {
                respuesta.Exito = false;
                respuesta.ModeloValido = false;
                respuesta.MensajesError.Add(string.Format(Error.C0005, "El número", "Centro de Costos"));
            }

            if (!esModificacion)
            {
                if (ccDto.IdCAlmacenGas != null && unidadAsignada.Equals(false)) unidadAsignada = true;
                else if (unidadAsignada) unidadMensaje = string.Concat(unidadMensaje, " Unidad de alamcen de Gas");

                if (ccDto.IdCamioneta != null && unidadAsignada.Equals(false)) unidadAsignada = true;
                else if (unidadAsignada) unidadMensaje = string.Concat(unidadMensaje, ", Camioneta");

                if (ccDto.IdCilindro != null && unidadAsignada.Equals(false)) unidadAsignada = true;
                else if (unidadAsignada) unidadMensaje = string.Concat(unidadMensaje, ", Cilindros de gas");

                if (ccDto.IdEquipoTransporte != null && unidadAsignada.Equals(false)) unidadAsignada = true;
                else if (unidadAsignada) unidadMensaje = string.Concat(unidadMensaje, ", Equipo de transporte");

                if (ccDto.IdEstacionCarburacion != null && unidadAsignada.Equals(false)) unidadAsignada = true;
                else if (unidadAsignada) unidadMensaje = string.Concat(unidadMensaje, ", Estación de carburación");

                if (ccDto.IdPipa != null && unidadAsignada.Equals(false)) unidadAsignada = true;
                else if (unidadAsignada) unidadMensaje = string.Concat(unidadMensaje, ", Pipa");

                if (ccDto.IdVehiculoUtilitario != null && unidadAsignada.Equals(false)) unidadAsignada = true;
                else if (unidadAsignada) unidadMensaje = string.Concat(unidadMensaje, ", Vehiculo utilitario");

                if (!string.IsNullOrEmpty(unidadMensaje))
                {
                    respuesta.Exito = false;
                    respuesta.ModeloValido = false;
                    respuesta.MensajesError.Add(string.Format(Error.C0006, unidadMensaje));
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


        public static RespuestaDto LineaProducto(LineaProductoCrearDto lpDto, bool esModificacion = false)
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
