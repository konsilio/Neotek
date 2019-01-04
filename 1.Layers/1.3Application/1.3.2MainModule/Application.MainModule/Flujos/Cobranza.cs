using Application.MainModule.AdaptadoresDTO.Cobranza;
using Application.MainModule.DTOs.Cobranza;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.Cobranza;
using Application.MainModule.Servicios.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                       
            return CobranzaServicio.Alta(abono);
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
