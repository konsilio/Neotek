using Application.MainModule.Servicios.Catalogos;
using Application.MainModule.DTOs.Mobile;
using Application.MainModule.AdaptadoresDTO.Mobile;

namespace Application.MainModule.Servicios.Mobile
{
    class ClientesServicio
    {
        public static DatosTipoPersonaDto ConsultarTipoPersonas()
        {
            var tpersona = TipoPersonaServicio.ListaTipoPersona();
            var tregimen = RegimenServicio.ListaRegimen();
            //var puntoventa = PuntoVentaServicio.ObtenerPorUsuarioAplicacion();
            return TipoPersonaAdapter.ToDto(tpersona,tregimen);
            
        }
    }
}
