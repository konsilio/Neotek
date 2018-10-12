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
    }
}
