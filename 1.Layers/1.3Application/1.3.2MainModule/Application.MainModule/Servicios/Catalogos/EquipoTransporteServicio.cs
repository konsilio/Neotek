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
            EquipoTransporteDTO Vehiculo = AdaptadoresDTO.Catalogo.EquipoTransporteAdapter.toDTO(new EquipoTransporteDataAccess().Buscar(idVehiculo));
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

        public static string ObtenerNumero(short idEmpresa, short idCAlmacenGas)
        {
            if (idEmpresa != 0)
                return new EquipoTransporteDataAccess().BuscarUnidades(idEmpresa, idCAlmacenGas).Numero;

            return null;
        }
        public static string ObtenerNombre(EquipoTransporte qt)
        {
            if (qt.IdCamioneta != null)
            {
                if (qt.Camionetas != null)
                    return qt.Camionetas.Nombre + " "+ qt.Camionetas.Numero;
                else
                    return new EquipoTransporteDataAccess().BuscarCamioneta(qt.IdCamioneta.Value).Nombre + " " + qt.Camionetas.Numero;
            }
            if (qt.IdPipa != null)
            {
                if (qt.Pipas != null)
                    return qt.Pipas.Nombre + " " + qt.Pipas.Numero;
                else
                    return new EquipoTransporteDataAccess().BuscarPipa(qt.IdPipa.Value).Nombre + " " + qt.Pipas.Numero;
            }
            //if (qt.Vehiculo != null)
            //    return qt.Vehiculo.Nombre;
            //else
            //    return new EquipoTransporteDataAccess().BuscarVehiculo(qt.Vehiculo.Value).Nombre; 
            return null;

        }
        public static List<EquipoTransporte> BuscarEquipoTransporte()
        {
            return new EquipoTransporteDataAccess().BuscarEquipoTransporte();
        }
        public static List<EquipoTransporte> BuscarEquipoTransporte(short IdEmpresa)
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
        public static RespuestaDto Alta(EquipoTransporte _VehiculoDto)
        {
            return new EquipoTransporteDataAccess().Insertar(_VehiculoDto);
        }     
        public static RespuestaDto Modificar(EquipoTransporte _VehiculoDto)
        {
            return new EquipoTransporteDataAccess().Actualizar(_VehiculoDto);
        }
        public static PuntoVenta GenerarAsignacion(UnidadAlmacenGas almacen, TransporteDTO transporte)
        {
            return new PuntoVenta()
            {
                IdEmpresa = TokenServicio.ObtenerIdEmpresa(),
                IdCAlmacenGas = almacen.IdCAlmacenGas,
                IdOperadorChofer = transporte.IdChofer,
                Activo = true,
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
    }
}
