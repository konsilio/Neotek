using Application.MainModule.AdaptadoresDTO.Cobranza;
using Application.MainModule.DTOs.Cobranza;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.Servicios.Cobranza;
using Application.MainModule.Servicios.Seguridad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Utilities.MainModule;

namespace Application.MainModule.Flujos
{
    public class Cobranza
    {
        public List<CargosDTO> ListaCargos(short idempresa)
        {
            var resp = PermisosServicio.PuedeConsultarCargos();
            if (!resp.Exito) return null;
            return CobranzaServicio.Obtener(idempresa).ToList();
        }
        public List<CargosDTO> ListaCRecuperada(short idempresa)
        {
            var resp = PermisosServicio.PuedeConsultarCargos();
            if (!resp.Exito) return null;
            return CobranzaServicio.CRecuperada(idempresa).ToList();
        }
        public DataSet ReporteCarteraRecuperada(int? idCliente, short? empresa, DateTime? fechaIni, DateTime? fechaFin, string ticket)
        {
            List<System.Data.SqlClient.SqlParameter> lp = new List<System.Data.SqlClient.SqlParameter>();
            if (idCliente != 0)
                lp.Add(new System.Data.SqlClient.SqlParameter("IdCliente", idCliente));
            if (fechaIni.Value.Year != 1)
            {
                lp.Add(new System.Data.SqlClient.SqlParameter("FecIni", fechaIni));
            }
            if (fechaFin.Value.Year != 1)
            {
                lp.Add(new System.Data.SqlClient.SqlParameter("FecFin", fechaFin));
            }
            if (ticket != ""&& ticket !=null)
            {
                lp.Add(new System.Data.SqlClient.SqlParameter("Ticket", ticket));
            }
            if (empresa != 0)
                lp.Add(new System.Data.SqlClient.SqlParameter("IdEmpresa", empresa));
            return new DataAccess().StoredProcedure_DataSet("SpSel_CarteraRecuperada", lp);
        }
        public List<CargosDTO> CarteraRecuperada(int? idCliente,short? empresa,DateTime? fechaIni, DateTime? fechaFin, string ticket)
        {
            var resp = PermisosServicio.PuedeConsultarCargos();
            if (!resp.Exito) return null;

            DataSet ds = ReporteCarteraRecuperada(idCliente, empresa, fechaIni, fechaFin, ticket);
            DataTable dtt = ds.Tables[0];
            List<CRecuperadaDTO> repDetallado = dtt.DataTableToList<CRecuperadaDTO>();
            DataTable dtt2 = ds.Tables[1];
            List<CRecuperadaTotalesDTO> repDetallado2 = dtt2.DataTableToList<CRecuperadaTotalesDTO>();

            return CobranzaServicio.CRecuperada(repDetallado, repDetallado2, empresa.Value).ToList();
        }
        public DataSet ReporteDetallado(int? idCliente, DateTime? fecha, short? empresa)
        {

            List<System.Data.SqlClient.SqlParameter> lp = new List<System.Data.SqlClient.SqlParameter>();
            if (idCliente != 0)
                lp.Add(new System.Data.SqlClient.SqlParameter("IdCliente", idCliente));
            if (fecha.Value.Year != 1)
            {
                lp.Add(new System.Data.SqlClient.SqlParameter("Fecha", fecha));
            }
            if (empresa != 0)
                lp.Add(new System.Data.SqlClient.SqlParameter("IdEmpresa", empresa));
            return new DataAccess().StoredProcedure_DataSet("SpSel_CarteraVencida", lp);
        }
        public ReporteDTO ListaCargos(int? idCliente, DateTime? fecha, short? empresa)
        {
            var resp = PermisosServicio.PuedeConsultarCargos();
            if (!resp.Exito) return null;

            DataSet ds = ReporteDetallado(idCliente, fecha, empresa);
            DataTable dtt = ds.Tables[0];
            List<CarteraVencidaDTO> repDetallado = dtt.DataTableToList<CarteraVencidaDTO>();
            DataTable dtt2 = ds.Tables[1];
            List<CarteraVencidaDTO> repDetallado2 = dtt2.DataTableToList<CarteraVencidaDTO>();

            return CobranzaServicio.Obtener(repDetallado, repDetallado2);
        }
        //public List<CargosDTO> ListaCargos(DataSet Reporte)
        //{
        //    CargosDTO objEmp = new CargosDTO();
        //    List<CargosDTO> repDetallado = new List<CargosDTO>();
        //    DataTable dtt = Reporte.Tables[0];
        //    repDetallado = dtt.DataTableToList<CargosDTO>();
        //    return repDetallado;
        //}
        public CargosDTO CargoId(int idCargo)
        {
            var resp = PermisosServicio.PuedeConsultarCargos();
            if (!resp.Exito) return null;

            return CobranzaServicio.Obtener(idCargo);
        }
        public RespuestaDto Registra(AbonosDTO abonosDto)
        {
            var resp = PermisosServicio.PuedeRegistrarAbonos();
            if (!resp.Exito) return resp;

            var abono = AbonosAdapter.FromDTO(abonosDto);
            var insAbono = CobranzaServicio.Alta(abono);
            if (!insAbono.Exito) return resp;

            var cargo = CobranzaServicio.Obtener(abonosDto.IdCargo);
            var UpdCargo = AbonosAdapter.FromDTO(cargo, abonosDto.MontoAbono);
            return CobranzaServicio.Update(UpdCargo);
        }
        public RespuestaDto Registra(List<AbonosDTO> abonosDto)
        {
            var resp = PermisosServicio.PuedeRegistrarAbonos();
            if (!resp.Exito) return resp;

            var abono = AbonosAdapter.FromDTO(abonosDto);

            var insAbono = CobranzaServicio.Alta(abono);
            if (!insAbono.Exito) return resp;

            var cargo = CobranzaServicio.Obtener(abonosDto[0].IdCargo);
            var UpdCargo = AbonosAdapter.FromDTO(cargo, abonosDto[0].MontoAbono);
            return CobranzaServicio.Update(UpdCargo);

        }
    }
}
