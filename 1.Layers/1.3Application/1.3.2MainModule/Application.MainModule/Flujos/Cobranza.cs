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

            if (TokenServicio.EsSuperUsuario())
                return CobranzaServicio.Obtener(idempresa).ToList();

            else
                return CobranzaServicio.Obtener(idempresa).Where(x => x.IdEmpresa.Equals(TokenServicio.ObtenerIdEmpresa())).ToList();
        }
        public DataSet ReporteDetallado(int? idCliente, DateTime? fecha, short? empresa)
        {
           
            List<System.Data.SqlClient.SqlParameter> lp = new List<System.Data.SqlClient.SqlParameter>();
            if (idCliente != 0)
                lp.Add(new System.Data.SqlClient.SqlParameter("IdCliente", idCliente));
            if (fecha.Value.Year != 1) {              
            lp.Add(new System.Data.SqlClient.SqlParameter("Fecha", fecha));}
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
            
            return  CobranzaServicio.Obtener(repDetallado, repDetallado2);
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
