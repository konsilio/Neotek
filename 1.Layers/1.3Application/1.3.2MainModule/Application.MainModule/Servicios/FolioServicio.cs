using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.DTOs;
using Application.MainModule.DTOs.Requisicion;
using Sagas.MainModule.Entidades;

namespace Application.MainModule.Servicios
{
    public static class FolioServicio
    {
        public static string GenerarNumeroRequisicion(RequisicionDTO _req)
        {
            var contador = new AccesoADatos.RequisicionDataAccess().BuscarUltimaRequi() + 1;

            string numReq = "R";
            numReq += _req.IdEmpresa.ToString("D3");
            numReq += _req.FechaRegistro.Year.ToString();
            numReq += contador.ToString("D4");
            return numReq;
        }
        public static string GeneraNumerOrdenCompra(OrdenCompra _oc)
        {
            var contador = new AccesoADatos.OrdenCompraDataAccess().BuscarUltimaOC() + 1;
            string numOC = "OC";
            numOC += _oc.IdEmpresa.ToString("D3");
            numOC += _oc.FechaRegistro.Year.ToString();
            numOC += contador.ToString("D4");
            return numOC;
        }
        public static List<Sagas.MainModule.Entidades.Requisicion> GenerarNumeroRequisicion(List<Sagas.MainModule.Entidades.Requisicion> _req)
        {
            var contador = new AccesoADatos.RequisicionDataAccess().BuscarUltimaRequi() + 1;
            int index = 0;
            foreach (var r in _req)
            {
                _req.ElementAt(index).NumeroRequisicion = GenerarNumeroRequisicion(_req.ElementAt(index), contador);
                index++;
                contador++;
            }
            return _req;
        }

        public static string GenerarNumeroRequisicion(Sagas.MainModule.Entidades.Requisicion _r, int contador)
        {
            string numReq = "R";
            numReq += _r.IdEmpresa.ToString("D3");
            numReq += _r.FechaRegistro.Year.ToString();
            numReq += contador.ToString("D4");
            
            return numReq;
        }
    }
}
