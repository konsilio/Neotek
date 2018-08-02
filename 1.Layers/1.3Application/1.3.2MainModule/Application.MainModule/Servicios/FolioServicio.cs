using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.DTOs.Requisicion;

namespace Application.MainModule.Servicios
{
    public static class FolioServicio
    {
        public static string GenerarNumeroRequisicion(RequisicionEDTO _req)
        {
            var contador = 12;// Busca el contador en la BD

            string numReq = "R";
            numReq += _req.IdEmpresa.ToString("D3");
            numReq += _req.FechaRegistro.Year.ToString();
            numReq += contador.ToString("D4");
            return numReq;
        }
    }
}
