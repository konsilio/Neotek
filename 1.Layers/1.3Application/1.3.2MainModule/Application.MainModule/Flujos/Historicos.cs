using Application.MainModule.AdaptadoresDTO.Historico;
using Application.MainModule.DTOs;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.Historico;
using Application.MainModule.Servicios.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Flujos
{
    public class Historicos
    {
        public HistoricoVentaDTO TodoCatalogo(int IdHistorico)
        {
            var lista = HistoricoVentaServicio.Buscar(IdHistorico);
            return HistoricoVentasAdapter.ToDTO(lista);
        }
        public List<HistoricoVentaDTO> TodoCatalogo()
        {
            var lista = HistoricoVentaServicio.Buscar(TokenServicio.ObtenerIdEmpresa());
            return HistoricoVentasAdapter.ToDTO(lista);
        }
        public List<YearDTO> ObtenerAnios()
        {
            var lista = HistoricoVentaServicio.ObtenerYears();
            return YearsAdapater.ToDTO(lista);
        }
        public RespuestaDto CrearCatalogo(List<HistoricoVentaDTO> dto)
        {
            var resp = PermisosServicio.HistoricoVentas();
            if (!resp.Exito) return resp;

            var newMan = HistoricoVentasAdapter.FromDTO(dto);
            return HistoricoVentaServicio.Crear(newMan);
        }
        public RespuestaDto ModificarCatalogo(HistoricoVentaDTO dto)
        {
            var resp = PermisosServicio.HistoricoVentas();
            if (!resp.Exito) return resp;

            var entidad = HistoricoVentaServicio.Buscar(dto.Id);
            var emty = HistoricoVentasAdapter.FormEmtity(entidad);
            return HistoricoVentaServicio.Actualizar(emty);
        }
        public RespuestaDto EliminarCatalogo(HistoricoVentaDTO dto)
        {
            var resp = PermisosServicio.HistoricoVentas();
            if (!resp.Exito) return resp;

            var entidad = HistoricoVentaServicio.Buscar(dto.Id);
            var emty = HistoricoVentasAdapter.FormEmtity(entidad);

            return HistoricoVentaServicio.Actualizar(emty);
        }
        public string GenerarJsonConsulta(HistoricoConsultaDTO dto)
        {
            //Traer los registros segun los parametros de años y meses 
            var Ventas = HistoricoVentaServicio.BuscarPorFiltros(dto);
            //transformar segun tipo de consulta            
            return HistoricoVentaServicio.TransformarAJason(Ventas, dto);
        }
        public List<YearDTO> VentasConsultas(HistoricoConsultaDTO dto)
        {
            var hVentas = HistoricoVentaServicio.BuscarPorFiltros(dto);
            return HistoricoVentaServicio.VentasTotales(hVentas, dto);
        }
    }
}
