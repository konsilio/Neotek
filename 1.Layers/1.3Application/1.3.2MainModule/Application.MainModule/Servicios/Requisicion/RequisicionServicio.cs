using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.DTOs.Requisicion;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.AdaptadoresDTO.Requisicion;

namespace Application.MainModule.Servicios.Requisicion
{
    public static class RequisicionServicio
    {
        public static RespuestaRequisicionDto GuardarRequisicionNueva(RequisicionEDTO _req)
        {

           
            var requisicionResp = new RequisicionDataAccess().InsertarNueva(RequisicionAdapter.FromEDTO(_req));
            if (requisicionResp != null)            
                return new RespuestaRequisicionDto()
                {
                    IdRequisicion = requisicionResp.IdRequisicion,
                    NumRequisicion = requisicionResp.NumRequisicion,
                    Exito = true,
                    Mensaje = requisicionResp.Mensaje
                };            
            else
                return new RespuestaRequisicionDto()
                {
                    Exito = false,
                    Mensaje = "Error" //Agregar mensaje espesifico en Exceptions
                };            
        }
    }
}
