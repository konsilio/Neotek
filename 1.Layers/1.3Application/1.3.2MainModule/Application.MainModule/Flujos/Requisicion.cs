using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.DTOs.Requisicion;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.Requisicion;

namespace Application.MainModule.Flujos
{
    public class Requisicion
    {
        public RespuestaRequisicionDto InsertRequisicionNueva(RequisicionEDTO _req)
        {            
            return RequisicionServicio.GuardarRequisicionNueva(_req);
        }
        public List<RequisicionDTO> BuscarRequisicionesPorEmpresa(Int16 idEmpresa)
        {
            return RequisicionServicio.BuscarRequisicionPorIdEmpresa(idEmpresa);
        }
        public RequisicionEDTO BuscarRequisicion(string numRequisicon)
        {
            return RequisicionServicio.BuscarRequisicion(numRequisicon);
        }
    }
}
