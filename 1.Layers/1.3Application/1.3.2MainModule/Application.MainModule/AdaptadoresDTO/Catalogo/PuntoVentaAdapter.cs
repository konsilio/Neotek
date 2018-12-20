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
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.DTOs.Mobile.PuntoVenta;

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

        public static PuntoVentaAsignadoDTO ToDTO(Usuario usuario, OperadorChoferDTO operador, PuntoVenta puntoVenta, UnidadAlmacenGas unidadAlmacen, Pipa pipaAsignada)
        {
            return new PuntoVentaAsignadoDTO()
            {
                 IdCAlmacen = unidadAlmacen.IdCAlmacenGas,
                 IdEstacion = (short) pipaAsignada.IdPipa,
                 IdOperadorChofer = (short) operador.IdOperadorChofer,
                 IdUusario =  (short) usuario.IdUsuario,
                 IdPuntoVenta = (short) puntoVenta.IdPuntoVenta,
                 NombreOperador = usuario.Nombre+" "+usuario.Apellido1+ " "+ usuario.Apellido2,
                 NombrePuntoVenta = pipaAsignada.Nombre
            };
        }

        public static PuntoVentaAsignadoDTO ToDTO(Usuario usuario, OperadorChoferDTO operador, PuntoVenta puntoVenta, UnidadAlmacenGas unidadAlmacen, Camioneta camionetaAsignada)
        {
            return new PuntoVentaAsignadoDTO()
            {
                IdCAlmacen = unidadAlmacen.IdCAlmacenGas,
                IdEstacion = (short)camionetaAsignada.IdCamioneta,
                IdOperadorChofer = (short)operador.IdOperadorChofer,
                IdUusario = (short)usuario.IdUsuario,
                IdPuntoVenta = (short)puntoVenta.IdPuntoVenta,
                NombreOperador = usuario.Nombre + " " + usuario.Apellido1 + " " + usuario.Apellido2,
                NombrePuntoVenta = camionetaAsignada.Nombre
            };
        }
        public static PuntoVentaAsignadoDTO ToDTO(Usuario usuario, OperadorChoferDTO operador, PuntoVenta puntoVenta, UnidadAlmacenGas unidadAlmacen, EstacionCarburacion estacionAsignada)
        {
            return new PuntoVentaAsignadoDTO()
            {
                IdCAlmacen = unidadAlmacen.IdCAlmacenGas,
                IdEstacion = (short)estacionAsignada.IdEstacionCarburacion,
                IdOperadorChofer = (short)operador.IdOperadorChofer,
                IdUusario = (short)usuario.IdUsuario,
                IdPuntoVenta = (short)puntoVenta.IdPuntoVenta,
                NombreOperador = usuario.Nombre + " " + usuario.Apellido1 + " " + usuario.Apellido2,
                NombrePuntoVenta = estacionAsignada.Nombre
            };
        }
        /// <summary>
        /// Permite generar un DTO de respuesta para 
        /// determinar si el cliente tiene permiso de realizar una 
        /// venta extraforanea
        /// </summary>
        /// <param name="cliente">Entidad con los datos del cliente</param>
        /// <param name="extraforaneo">Objeto con los datos de que es extraforaneo </param>
        /// <returns>Objeto DatosVentaExtraforaneaDTO con el resultado de la consulta</returns>
        public static DatosVentaExtraforaneaDTO ToDTO(Cliente cliente, string extraforaneo)
        {
            return new DatosVentaExtraforaneaDTO()
            {
                VentaExtraforanea = false
            };
        }
    }
}
