using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.MainModule.Agente;
using Web.MainModule.Catalogos.Model;
using Web.MainModule.Seguridad.Model;

namespace Web.MainModule.Catalogos.Servicio
{
    public class CuentaContableServicio
    {
        public List<EmpresaDTO> BuscarGaseras(string tkn)
        {
            var agente = new AgenteServicios();
            agente.ListaEmpresas(tkn);
            return agente._listaEmpresas;
        }
        public RespuestaDto GuardarCtaCtble(CuentaContableCrearDto cc, string tkn)
        {
            var agente = new AgenteServicios();
            agente.GuardarCuentaContable(cc, tkn);
            return agente._respuestaDTO;
        }
        public List<CuentaContableDTO> ListaCtaCtble(short idEmpresa, string tkn)
        {
            var agente = new AgenteServicios();
            agente.BuscarCuentasContables(idEmpresa, tkn);
            return agente._listaCuentasContable;
        }
        public RespuestaDto EliminarCtaContable(CuentaContableEliminarDto cc, string tkn)
        {
            var agente = new AgenteServicios();
            agente.EliminarCtaCtble(cc, tkn);
            return agente._respuestaDTO;
        }
        public RespuestaDto ModificarCtaContable(CuentaContableModificarDto cc, string tkn)
        {
            var agente = new AgenteServicios();
            agente.ModificarCtaCtble(cc, tkn);
            return agente._respuestaDTO;
        }
    }
}