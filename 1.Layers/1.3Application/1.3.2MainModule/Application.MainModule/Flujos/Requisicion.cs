using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.DTOs.Requisicion;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.DTOs.Seguridad;
using Application.MainModule.Servicios.Requisicion;

namespace Application.MainModule.Flujos
{
    public class Requisicion
    {
        public RespuestaRequisicionDto InsertRequisicionNueva(RequisicionEDTO _req)
        {
            var _requisicion = AdaptadoresDTO.Requisicion.RequisicionAdapter.FromEDTO(_req);
            _requisicion =  Servicios.Almacen.ProductoAlmacenServicio.CalcularAlmacenProcutos(_requisicion);
            return RequisicionServicio.GuardarRequisicionNueva(_requisicion);
        }
        public List<RequisicionDTO> BuscarRequisicionesPorEmpresa(Int16 idEmpresa)
        {
            return RequisicionServicio.BuscarRequisicionPorIdEmpresa(idEmpresa);
        }
        public RequisicionRevisionDTO BuscarRequisicion(int idRequisicion)
        {
            return RequisicionServicio.BuscarRequisicion(idRequisicion);
        }
        public RequisicionAutorizacionDTO BuscarRequisicionAuto(int numRequisicon)
        {
            return RequisicionServicio.BuscarRequisicionAuto(numRequisicon);
        }
        public RespuestaRequisicionDto ActualizarRequisicionRevision(RequisicionRevPutDTO _req)
        {
            return RequisicionServicio.UpdateRequisicionRevision(_req);
        }
        public RespuestaRequisicionDto ActualizarRequisicionAutorizacion(RequisicionAutPutDTO _req)
        {
            return RequisicionServicio.UpDateRequisicionAutoriza(_req);
        }
        public RespuestaRequisicionDto CancelarRequisicion(RequisicionCancelaDTO _req)
        {
            return RequisicionServicio.CancelarRequisicion(_req);
        }       
    }
}
