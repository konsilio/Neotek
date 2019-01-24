using Application.MainModule.DTOs;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.AccesoADatos;
using Exceptions.MainModule.Validaciones;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    return qt.Camionetas.Nombre;
                else
                    return new EquipoTransporteDataAccess().BuscarCamioneta(qt.IdCamioneta.Value).Nombre; 
            }

            if (qt.IdPipa!= null)
            {
                if (qt.Pipas != null)
                    return qt.Pipas.Nombre;
                else
                    return new EquipoTransporteDataAccess().BuscarPipa(qt.IdPipa.Value).Nombre; 
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

        public static RespuestaDto Alta(EquipoTransporte _VehiculoDto)
        {
            return new EquipoTransporteDataAccess().Insertar(_VehiculoDto);
        }     
        public static RespuestaDto Modificar(EquipoTransporte _VehiculoDto)
        {
            return new EquipoTransporteDataAccess().Actualizar(_VehiculoDto);
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
