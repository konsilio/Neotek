using Application.MainModule.AdaptadoresDTO.Catalogo;
using Application.MainModule.DTOs;
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
    public static class EquipoTransporteServicio
    {
        public static List<EquipoTransporteDTO> Obtener(short idempresa)
        {
            List<EquipoTransporteDTO> lPedidos = AdaptadoresDTO.EqTransporte.EquipoTransporteAdapter.ToDTO(new EqTransporteDataAccess().BuscarTodos(idempresa));
            return lPedidos;
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

            if (qt.IdPipa != null)
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
        public static List<EquipoTransporteDTO> Obtener(short idempresa)
        {
            List<EquipoTransporteDTO> lPedidos = EquipoTransporteAdapter.toDTO(new EqTransporteDataAccess().BuscarTodos(idempresa));
            return lPedidos;
        }

        public static List<EquipoTransporteDTO> ObtenerTransportes(List<PipaDTO> pipas, List<CamionetaDTO> camionetas, List<EstacionCarburacion> estaciones) //,List<Utilitario> utilitariso)
        {
            List<EquipoTransporteDTO> parqueVehicular = new List<EquipoTransporteDTO>();
            foreach (var pip in pipas)
                parqueVehicular.Add(new EquipoTransporteDTO { IdPipa = pip.IdPipa, Vehiculo = pip.Nombre, IdEmpresa = pip.IdEmpresa });
            foreach (var est in estaciones)
                parqueVehicular.Add(new EquipoTransporteDTO { IdEstacion = est.IdEstacionCarburacion, Vehiculo = est.Nombre, IdEmpresa = est.IdEmpresa });
            foreach (var cam in camionetas)
                parqueVehicular.Add(new EquipoTransporteDTO { IdCamioneta = cam.IdCamioneta, Vehiculo = cam.Nombre, IdEmpresa = cam.IdEmpresa });
            //foreach (var util in utilitarios)
            //{
            //parqueVehicular.Add(new EquipoTransporteDTO { IdVehiculoUtilitario = util.IdUtilitario, Vehiculo = util.Nombre, IdEmpresa = util.IdEmpresa });
            //}
            return parqueVehicular;
        }
    }
}
