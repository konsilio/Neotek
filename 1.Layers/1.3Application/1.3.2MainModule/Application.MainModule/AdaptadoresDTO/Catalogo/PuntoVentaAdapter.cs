using Application.MainModule.DTOs.Catalogo;
using Application.MainModule.Servicios.Catalogos;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.AdaptadoresDTO.Catalogo
{
    public class PuntoVentaAdapter
    {
        public static PuntoVentaDTO ToDTO(PuntoVenta pv)
        {
            PuntoVentaDTO usDTO = new PuntoVentaDTO()
            {
                IdPuntoVenta = pv.IdPuntoVenta,
                IdEmpresa = pv.IdEmpresa,
                IdCAlmacenGas = pv.IdCAlmacenGas,
                IdOperadorChofer = pv.IdOperadorChofer,
                FechaModificacion = pv.FechaModificacion,
                Activo = pv.Activo,
                FechaRegistro = pv.FechaRegistro,
                UnidadesAlmacen = EquipoTransporteServicio.ObtenerNumero(pv.IdEmpresa),//pv.UnidadesAlmacen,
                OperadorChofer = "",//pv.OperadorChofer,
                Empresa = "",//pv.Empresa,
            };
            return usDTO;
        }
        public static List<PuntoVentaDTO> ToDTO(List<PuntoVenta> lu)
        {
            List<PuntoVentaDTO> luDTO = lu.ToList().Select(x => ToDTO(x)).ToList();
            return luDTO;
        }
    }
}
