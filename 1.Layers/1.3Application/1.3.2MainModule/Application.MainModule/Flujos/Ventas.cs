using Application.MainModule.AdaptadoresDTO.Ventas;
using Application.MainModule.DTOs.Almacen;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.DTOs.Ventas;
using Application.MainModule.Servicios.Seguridad;
using Application.MainModule.Servicios.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Flujos
{
    public class Ventas
    {
        public List<CajaGeneralDTO> CajaGeneral()
        {
          
            var resp = PermisosServicio.PuedeConsultarCajaGeneral();
            if (!resp.Exito) return null;

            if (TokenServicio.EsSuperUsuario())
                return CajaGeneralServicio.Obtener().ToList();

            else
                return CajaGeneralServicio.Obtener().Where(x => x.IdEmpresa.Equals(TokenServicio.ObtenerIdEmpresa())).ToList();
        }

        public List<AlmacenGasMovimientoDto> MovimientosGas(short empresa, short year, byte month, byte dia, short orden)
        {           
            var resp = PermisosServicio.PuedeConsultarCajaGeneral();
            if (!resp.Exito) return null;

            return CajaGeneralServicio.ObtenerPVDetalle(empresa, year, month, dia, orden).ToList();

        }

        public List<VPuntoVentaDetalleDTO> MovimientosGasCilindro(short? empresa, short year, byte month, byte dia, short? orden)
        {
            var resp = PermisosServicio.PuedeConsultarCajaGeneral();
            if (!resp.Exito) return null;

            return CajaGeneralServicio.ObtenerVentas(empresa.Value, year, month, dia, orden.Value).ToList();

        }

        public List<CajaGeneralDTO> CajaGeneralIdEmpresa(short IdEmpresa)
        {
            var resp = PermisosServicio.PuedeConsultarCajaGeneral();
            if (!resp.Exito) return null;
            return CajaGeneralServicio.ObtenerIdEmp(IdEmpresa).ToList();
        }

        public List<VentaPuntoVentaDTO> CajaGeneralCamioneta(string cveReporte)
        {
            var resp = PermisosServicio.PuedeConsultarCajaGeneral();
            if (!resp.Exito) return null;
            return CajaGeneralServicio.ObtenerPV(cveReporte).ToList();
        }
        public List<VentaCorteAnticipoDTO> CajaGeneralEstacion(string cveReporte)
        {
            var resp = PermisosServicio.PuedeConsultarCajaGeneral();
            if (!resp.Exito) return null;

            return CajaGeneralServicio.ObtenerCE(cveReporte).ToList();
        }

        public RespuestaDto GuardarReporteLiquidado(VentaPuntoVentaDTO Dto)
        {
            var resp = PermisosServicio.PuedeModificarCajaGeneral();
            if (!resp.Exito) return resp;

            var reporte = CajaGeneralServicio.ObtenerPV(Dto.FolioOperacionDia).ToList();
            if (reporte == null) return CajaGeneralServicio.NoExiste();

            var rcg = CajaGeneralServicio.ObtenerCG(Dto.FolioOperacionDia);

            var rep = CajaGeneralAdapter.FromDto(rcg);
          
            return CajaGeneralServicio.Actualizar(rep);
        }
        public RespuestaDto GuardarReporteLiquidadoEst(VentaCorteAnticipoDTO Dto)
        {
            var resp = PermisosServicio.PuedeModificarCajaGeneral();
            if (!resp.Exito) return resp;

            var reporte = CajaGeneralServicio.ObtenerCE(Dto.FolioOperacion).ToList();
            if (reporte == null) return CajaGeneralServicio.NoExiste();

            var rep = CajaGeneralAdapter.FromDtoCE(reporte);
            return CajaGeneralServicio.Actualizar(rep);
        }
    }
}
