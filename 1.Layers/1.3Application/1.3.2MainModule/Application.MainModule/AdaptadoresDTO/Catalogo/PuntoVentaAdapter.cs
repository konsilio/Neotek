using Application.MainModule.DTOs.Catalogo;
using Application.MainModule.Servicios.Almacenes;
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

        public static PuntoVenta FromDtoEditar(PuntoVentaDTO Ctedto, PuntoVenta catCte)
        {
            var catPuntoVenta = FromEntity(catCte);             
            if (Ctedto.IdOperadorChofer != 0) { catPuntoVenta.IdOperadorChofer = Ctedto.IdOperadorChofer; } else { catPuntoVenta.IdOperadorChofer = catPuntoVenta.IdOperadorChofer; }
          
            return catPuntoVenta;
        }

        public static PuntoVenta FromEntity(PuntoVenta cte)
        {
            return new PuntoVenta()
            {
                IdPuntoVenta = cte.IdPuntoVenta,
                IdEmpresa = cte.IdEmpresa,
                IdCAlmacenGas = cte.IdCAlmacenGas,
                IdOperadorChofer = cte.IdOperadorChofer,
                FechaModificacion = cte.FechaModificacion,
                Activo = cte.Activo,
                FechaRegistro = cte.FechaRegistro,
            };
        }
    }
}
