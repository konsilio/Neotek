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
        public CatalogoRespuestaDTO GuardarCtaCtble(CuentaContableDTO cc, string tkn)
        {
            var agente = new AgenteServicios();
            agente.GuardarCuentaContable(cc, tkn);
            return agente._respuestaCatalogos;
        }
        public List<CuentaContableDTO> ListaCtaCtble(string tkn)
        {
            var agente = new AgenteServicios();
            agente.BuscarCuentasContables(tkn);
            return agente._listaCuentasContable;
        }
    }
}