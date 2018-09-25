using Application.MainModule.DTOs.Catalogo;
using Application.MainModule.Servicios.Catalogos;
using Application.MainModule.Servicios.Seguridad;
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
            var idUser = OperadorChoferServicio.Obtener(pv.IdOperadorChofer);
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
                OperadorChofer = idUser.Usuario.Nombre + " " + idUser.Usuario.Apellido1 + " " + idUser.Usuario.Apellido2,//UsuarioServicio.Obtener(idUser.IdUsuario).ToString(),
                Empresa = EmpresaServicio.Obtener(pv.IdEmpresa).NombreComercial,
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
