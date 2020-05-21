using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.DTOs;
using Application.MainModule.DTOs.Catalogo;
using Application.MainModule.Servicios.AccesoADatos;
using Exceptions.MainModule.Validaciones;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.DTOs.Transporte;
using Application.MainModule.Servicios.Seguridad;
using Sagas.MainModule.ObjetosValor.Enum;
using Application.MainModule.Servicios.Almacenes;
using Application.MainModule.Servicios.Equipo;

namespace Application.MainModule.Servicios.Catalogos
{
    public static class EquipoTransporteServicio
    {
        public static List<EquipoTransporteDTO> Obtener(short idempresa)
        {
            List<EquipoTransporteDTO> lVehiculos = AdaptadoresDTO.Catalogo.EquipoTransporteAdapter.toDTO(new EquipoTransporteDataAccess().Buscar(idempresa));
            return lVehiculos;
        }
        public static EquipoTransporteDTO Obtener(int idVehiculo)
        {
            var entidad = new EquipoTransporteDataAccess().Buscar(TokenServicio.ObtenerIdEmpresa());
            EquipoTransporteDTO Vehiculo = AdaptadoresDTO.Catalogo.EquipoTransporteAdapter.toDTO(entidad.SingleOrDefault(x => x.IdEquipoTransporteDetalle.Equals(idVehiculo)));
            return Vehiculo;
        }
        public static string ObtenerNombre(UnidadAlmacenGas uAG)
        {
            if (uAG.IdCamioneta != null)
                return new EquipoTransporteDataAccess().BuscarCamioneta(uAG).Nombre;
            if (uAG.IdPipa != null)
                return new EquipoTransporteDataAccess().BuscarPipa(uAG).Nombre;
            if (uAG.IdEstacionCarburacion != null)
                return new EquipoTransporteDataAccess().BuscarEstacion(uAG).Nombre;
            return null;
        }
        public static string ObtenerNombre(Pedido entidad)
        {

            if (entidad.IdCamioneta != null && entidad.IdCamioneta != 0)
            {

                var calmacen = AlmacenGasServicio.ObtenerPorCamioneta(entidad.IdCamioneta.Value);
                return calmacen != null ? calmacen.Numero : "No encontrada";
            }
            if (entidad.IdPipa != null && entidad.IdPipa != 0)
            {
                var calmacen = AlmacenGasServicio.ObtenerPorPipa(entidad.IdPipa.Value);
                return calmacen != null ? calmacen.Numero : "No encontrada";
            }

            return null;
        }
        public static DateTime ObtenerFechaRegistro(CDetalleEquipoTransporte entidad)
        {
            if (entidad.IdCamioneta != null)
                return entidad.CCamioneta.FechaRegistro;
            if (entidad.IdPipa != null)
                return entidad.CPipa.FechaRegistro;
            if (entidad.IdUtilitario != null)
                return entidad.CUtilitario.FechaRegistro;
            return DateTime.MinValue;
        }
        public static bool ObtenerForaneo(CDetalleEquipoTransporte entidad)
        {
            if (entidad.IdCamioneta != null)
                return entidad.CCamioneta.EsForaneo;
            if (entidad.IdPipa != null)
                return entidad.CPipa.EsForaneo;
            if (entidad.IdUtilitario != null)
                return entidad.CUtilitario.EsForaneo;
            return false;
        }
        public static bool ObtenerActivo(CDetalleEquipoTransporte entidad)
        {
            if (entidad.IdCamioneta != null)
                return entidad.CCamioneta.Activo;
            if (entidad.IdPipa != null)
                return entidad.CPipa.Activo;
            if (entidad.IdUtilitario != null)
                return entidad.CUtilitario.Activo;
            return false;
        }
        public static int ObtenerId(CDetalleEquipoTransporte entidad)
        {
            if (entidad.IdCamioneta != null)
                return entidad.CCamioneta.IdCamioneta;
            if (entidad.IdPipa != null)
                return entidad.CPipa.IdPipa;
            if (entidad.IdUtilitario != null)
                return entidad.CUtilitario.IdUtilitario;
            return 0;
        }
        public static string ObtenerNumero(short idEmpresa, short idCAlmacenGas)
        {
            if (idEmpresa != 0)
                return new EquipoTransporteDataAccess().BuscarUnidades(idEmpresa, idCAlmacenGas).Numero;

            return null;
        }
        public static string ObtenerNombre(CDetalleEquipoTransporte entidad)
        {
            if (entidad.IdCamioneta != null)
                return entidad.CCamioneta.Nombre;
            if (entidad.IdPipa != null)
                return entidad.CPipa.Nombre;
            if (entidad.IdUtilitario != null)
                return entidad.CUtilitario.Nombre;
            return null;
        }

        public static string ObtenerNombreTipo(CDetalleEquipoTransporte entidad)
        {
            if (entidad.IdCamioneta != null)
                return "Camioneta";
            if (entidad.IdPipa != null)
                return "Pipa";
            if (entidad.IdUtilitario != null)
                return "Utilitario";
            return null;
        }

        public static string ObtenerNombre(DetalleRecargaCombustible qt)
        {
            var eq = new EquipoTransporteDataAccess().Buscar(qt.Id_Vehiculo);
            if (eq.IdCamioneta != null)
                return new EquipoTransporteDataAccess().BuscarCamioneta(eq.IdCamioneta.Value).Nombre;
            if (eq.IdPipa != null)
                return new EquipoTransporteDataAccess().BuscarPipa(eq.IdPipa.Value).Nombre;
            if (eq.IdUtilitario != null)
                return new EquipoTransporteDataAccess().BuscarUtilitario(eq.IdUtilitario.Value).Nombre;
            return null;
        }
        public static string ObtenerNombre(DetalleMantenimiento qt)
        {
            try
            {
                var eq = new EquipoTransporteDataAccess().Buscar(qt.id_vehiculo);
                if (qt.EsCamioneta)
                    return new EquipoTransporteDataAccess().BuscarCamioneta(eq.IdCamioneta.Value).Nombre;
                if (qt.EsPipa)
                    return new EquipoTransporteDataAccess().BuscarPipa(eq.IdPipa.Value).Nombre;
                if (qt.EsUtilitario)
                    return new EquipoTransporteDataAccess().BuscarUtilitario(eq.IdUtilitario.Value).Nombre;
                return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }

        }
        public static List<CDetalleEquipoTransporte> BuscarEquipoTransporte()
        {
            return new EquipoTransporteDataAccess().BuscarEquipoTransporte();
        }
        public static List<CDetalleEquipoTransporte> BuscarEquipoTransporte(short IdEmpresa)
        {
            return new EquipoTransporteDataAccess().BuscarEquipoTransporte(IdEmpresa);
        }
        public static List<EquipoTransporteDTO> ObtenerTransportes(List<PipaDTO> pipas, List<CamionetaDTO> camionetas, List<EstacionCarburacion> estaciones) //,List<Utilitario> utilitariso)
        {
            List<EquipoTransporteDTO> parqueVehicular = new List<EquipoTransporteDTO>();
            foreach (var pip in pipas)
                parqueVehicular.Add(new EquipoTransporteDTO { IdPipa = pip.IdPipa, DescVehiculo = pip.Nombre, IdEmpresa = pip.IdEmpresa });
            foreach (var est in estaciones)
                parqueVehicular.Add(new EquipoTransporteDTO { IdEstacion = est.IdEstacionCarburacion, DescVehiculo = est.Nombre, IdEmpresa = est.IdEmpresa });
            foreach (var cam in camionetas)
                parqueVehicular.Add(new EquipoTransporteDTO { IdCamioneta = cam.IdCamioneta, DescVehiculo = cam.Nombre, IdEmpresa = cam.IdEmpresa });
            //foreach (var util in utilitarios)
            //{
            //parqueVehicular.Add(new EquipoTransporteDTO { IdVehiculoUtilitario = util.IdUtilitario, Vehiculo = util.Nombre, IdEmpresa = util.IdEmpresa });
            //}
            return parqueVehicular;
        }
        public static RespuestaDto Alta(CDetalleEquipoTransporte _VehiculoDto)
        {
            return new EquipoTransporteDataAccess().Insertar(_VehiculoDto);
        }
        public static RespuestaDto Modificar(CDetalleEquipoTransporte _VehiculoDto)
        {
            return new EquipoTransporteDataAccess().Actualizar(_VehiculoDto);
        }
        public static RespuestaDto Modificar(CDetalleEquipoTransporte _VehiculoDto, CDetalleEquipoTransporte vehiculo)
        {
            return new EquipoTransporteDataAccess().Actualizar(_VehiculoDto, vehiculo);
        }
        public static PuntoVenta GenerarAsignacion(UnidadAlmacenGas almacen, TransporteDTO transporte)
        {
            return new PuntoVenta()
            {
                IdEmpresa = TokenServicio.ObtenerIdEmpresa(),
                IdCAlmacenGas = almacen.IdCAlmacenGas,
                IdOperadorChofer = transporte.IdChofer,
                FechaRegistro = DateTime.Now,
                FechaModificacion = DateTime.Now,
                Activo = true,
            };
        }
        public static RespuestaDto BorrarVehiculoPorEdicion(CDetalleEquipoTransporte ec)
        {
            if (ec.IdPipa != null)
            {
                return PipaServicio.Borrar(ec.IdPipa.Value);
            }
            else if (ec.IdUtilitario != null)
            {
                return VehiculoUtilitarioServicio.Borrar(ec.IdUtilitario.Value);
            }
            else if (ec.IdCamioneta != null)
            {
                return CamionetaServicio.Borrar(ec.IdCamioneta.Value);
            }
            return new RespuestaDto();
        }
        public static AsignacionUtilitarios GenerarAsignacion(TransporteDTO transporte)
        {
            return new AsignacionUtilitarios()
            {
                IdEmpresa = TokenServicio.ObtenerIdEmpresa(),
                IdChoferOperador = transporte.IdChofer,
                IdUtilitario = transporte.IdVehiculo,
                Activo = true,
                FechaCreacion = DateTime.Today,
                FechaMdidificacion = DateTime.Today
            };
        }
        public static RespuestaDto NoExiste()
        {
            string mensaje = string.Format(Error.NoExiste, "El equipo transporte");

            return new RespuestaDto()
            {
                ModeloValido = true,
                Mensaje = mensaje,
                MensajesError = new List<string>() { mensaje },
            };
        }
        public static int ObtenerTipo(CDetalleEquipoTransporte ec)
        {
            if (ec.IdCamioneta != null) { return TipoUnidadEqTransporteEnum.Camioneta; }
            if (ec.IdPipa != null) { return TipoUnidadEqTransporteEnum.Pipa; }
            if (ec.IdUtilitario != null) { return TipoUnidadEqTransporteEnum.Utilitario; }
            return 0;
        }
        public static decimal ObtenerCapacidadKg(CDetalleEquipoTransporte ec)
        {
            try
            {
                if (ec.IdCamioneta != null) { return AlmacenGasServicio.ObtenerPorCamioneta(ec.IdCamioneta.Value).CapacidadTanqueKg ?? 0; }
                if (ec.IdPipa != null) { return AlmacenGasServicio.ObtenerPorPipa(ec.IdPipa.Value).CapacidadTanqueKg ?? 0; }
                return 0;

            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static decimal ObtenerCapacidadLt(CDetalleEquipoTransporte ec)
        {
            try
            {
                if (ec.IdCamioneta != null) { return AlmacenGasServicio.ObtenerPorCamioneta(ec.IdCamioneta.Value).CapacidadTanqueLt ?? 0; }
                if (ec.IdPipa != null) { return AlmacenGasServicio.ObtenerPorPipa(ec.IdPipa.Value).CapacidadTanqueLt ?? 0; }
                return 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static string ObtenerAlias(CDetalleEquipoTransporte ec)
        {
            return ec.Marca + " " + "Color" + " " + ec.Color;
        }
        public static string ObtenerNombreChofer(CDetalleEquipoTransporte ec)
        {
            try
            {
                Usuario usu = new Usuario { Nombre = "No", Apellido1 = "asignado" };
                if (ec.IdCamioneta != null)
                    usu = PuntoVentaServicio.Obtener(ec.CCamioneta.UnidadAlmacenGas.FirstOrDefault().IdCAlmacenGas).OperadorChofer.Usuario;
                if (ec.IdPipa != null)
                    usu = PuntoVentaServicio.Obtener(ec.CPipa.UnidadAlmacenGas.FirstOrDefault().IdCAlmacenGas).OperadorChofer.Usuario;
                if (ec.IdUtilitario != null)
                    usu = AsignacionUtilitarioServicio.BuscarPorUtilitario(ec.IdUtilitario.Value).Usuario;

                return string.Concat(usu.Nombre, " ", usu.Apellido1);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        public static int ObtenerIdVehiculo(CDetalleEquipoTransporte ec)
        {
            if (ec.IdCamioneta != null) { return ec.IdCamioneta.Value; }
            if (ec.IdPipa != null) { return ec.IdPipa.Value; }
            if (ec.IdUtilitario != null) { return ec.IdUtilitario.Value; }
            return 0;
        }
        public static decimal ObtenerRendimento(DetalleRecargaCombustible entidad)
        {
            var recargaAnterior = RecargaCombustibleServicio.BuscarAnterior(entidad);
            if (recargaAnterior != null)
                return CalculosEquipoTrasporte.CalcularRendimientoVehicular(entidad);
            else
                return 0;
        }
        public static int ObtenerIdCentroCosto(int Id_Vehiculo)
        {
            try
            {
                var vehiculo = Obtener(Id_Vehiculo);
                if (vehiculo.IdCamioneta != null)
                    return CamionetaServicio.Obtener(vehiculo.IdCamioneta.Value).CCentroCosto.FirstOrDefault().IdCentroCosto;
                if (vehiculo.IdPipa != null)
                    return PipaServicio.Obtener(vehiculo.IdPipa.Value).CentroCosto.FirstOrDefault().IdCentroCosto;
                if (vehiculo.EsUtilitario != null)
                    return VehiculoUtilitarioServicio.Obtener(vehiculo.IdVehiculoUtilitario.Value).CCentroCosto.FirstOrDefault().IdCentroCosto;
                return 0;
            }
            catch (Exception)
            {
                return CentroCostoServicio.Obtener().FirstOrDefault().IdCentroCosto;
            }
        }
    }
}
