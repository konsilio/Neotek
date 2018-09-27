using Application.MainModule.DTOs.Catalogo;
using Application.MainModule.Servicios.Almacen;
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
            var Unidad = AlmacenGasServicio.ObtenerUnidadAlamcenGas(pv.IdCAlmacenGas);
            var result = AlmacenGasServicio.IdentificarTipoUnidadAlamcenGasString(Unidad);
            PuntoVentaDTO usDTO = new PuntoVentaDTO()
            {
                IdPuntoVenta = pv.IdPuntoVenta,
                IdEmpresa = pv.IdEmpresa,
                IdCAlmacenGas = pv.IdCAlmacenGas,
                IdOperadorChofer = pv.IdOperadorChofer,
                FechaModificacion = pv.FechaModificacion,
                Activo = pv.Activo,
                FechaRegistro = pv.FechaRegistro,
                UnidadesAlmacen = result.ToString(),//pv.UnidadesAlmacen,
                OperadorChofer = idUser.Usuario.Nombre + " " + idUser.Usuario.Apellido1 + " " + idUser.Usuario.Apellido2,//UsuarioServicio.Obtener(idUser.IdUsuario).ToString(),
                Empresa = EmpresaServicio.Obtener(pv.IdEmpresa).NombreComercial,
                PuntoVenta = EquipoTransporteServicio.ObtenerNumero(pv.IdEmpresa, pv.IdCAlmacenGas),
                IdUsuario = idUser.Usuario.IdUsuario,
            };
            return usDTO;
        }
        public static List<PuntoVentaDTO> ToDTO(List<PuntoVenta> lu)
        {
            List<PuntoVentaDTO> luDTO = lu.ToList().Select(x => ToDTO(x)).ToList();
            return luDTO;
        }
        public static PuntoVenta FromDto(PuntoVentaDTO pv)
        {
            return new PuntoVenta()
            {
                IdPuntoVenta = pv.IdPuntoVenta,
                IdEmpresa = pv.IdEmpresa,
                IdCAlmacenGas = pv.IdCAlmacenGas,
                IdOperadorChofer = pv.IdOperadorChofer,
                FechaModificacion = pv.FechaModificacion,
                Activo = pv.Activo,
                FechaRegistro = pv.FechaRegistro
               
            };
        }
    }
}
