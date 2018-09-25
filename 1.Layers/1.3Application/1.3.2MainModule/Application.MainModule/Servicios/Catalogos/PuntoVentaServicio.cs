using Application.MainModule.DTOs.Catalogo;
using Application.MainModule.Servicios.AccesoADatos;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Catalogos
{
    public static class PuntoVentaServicio
    {
        public static List<PuntoVentaDTO> Obtener()
        {
            // return new PuntoVentaDataAccess().BuscarTodos();
            List<PuntoVentaDTO> lPventas = AdaptadoresDTO.Catalogo.PuntoVentaAdapter.ToDTO(new PuntoVentaDataAccess().BuscarTodos());
            return lPventas;
        }

        public static PuntoVenta Obtener(int idPuntoVenta)
        {
            return new PuntoVentaDataAccess().Buscar(idPuntoVenta);
        }

        public static PuntoVenta Obtener(OperadorChofer opCh)
        {
            if (opCh != null)
                if (opCh.PuntosVenta != null)
                    if (opCh.PuntosVenta.Count > 0)
                        return opCh.PuntosVenta.FirstOrDefault();

            return BuscarPorOperadorChofer(opCh.IdOperadorChofer).FirstOrDefault();
        }

        public static List<PuntoVenta> BuscarPorOperadorChofer(int idOperadorChofer)
        {
            return new PuntoVentaDataAccess().BuscarPorOperadorChofer(idOperadorChofer);
        }

        public static PuntoVenta ObtenerPorUsuarioAplicacion()
        {
            var operadorChofer = OperadorChoferServicio.ObtenerPorUsuarioAplicacion();
            return Obtener(operadorChofer);
        }
    }
}
