using Application.MainModule.AccesoADatos;
using Application.MainModule.AdaptadoresDTO;
using Application.MainModule.DTOs;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.DTOs.Seguridad;
using Exceptions.MainModule.Validaciones;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios
{
    public static class ControlAsistenciaServicio
    {
        public static RespuestaDto Crear(ControlAsistenciaDTO dto)
        {
            var entidad = ControlAsistenciaAdapter.FromDTO(dto);
            return new ControlAsistenciaDataAccess().Insertar(entidad);
        }
        public static RespuestaDto Modificar(ControlAsistenciaDTO dto)
        {
            var entidad = ControlAsistenciaAdapter.FromDTO(dto);
            return new ControlAsistenciaDataAccess().Actualizar(entidad);
        }
        public static ControlAsistenciaDTO ObtenerHoy(int idUsuario)
        {
            var entidad = new ControlAsistenciaDataAccess().Buscar(idUsuario, DateTime.Now);
            if (entidad != null) return ControlAsistenciaAdapter.ToDTO(entidad);
            else return null;
        }
        public static bool ObtenerHoybool(int idUsuario)
        {
            var entidad = new ControlAsistenciaDataAccess().Buscar(idUsuario, DateTime.Now);
            if (entidad != null) return true;
            else return false;
        }
        public static List<ControlAsistencia> Obtener(short idEmpresa, PeriodoDTO p)
        {
            return new ControlAsistenciaDataAccess().Buscar(idEmpresa, p);
            //return ControlAsistenciaAdapter.toDTO(respuesta);
        }
        public static RespuestaAutenticacionMobileDto CalcularEntrada(Usuario usu, AutenticacionDto autDto, PuntoVenta pv = null, bool esEstacion = false)
        {
            RespuestaAutenticacionMobileDto respuesta = new RespuestaAutenticacionMobileDto();
            if (!ObtenerHoybool(usu.IdUsuario))
            {
                if (usu.Empresa.CoordenadaLat != null && usu.Empresa.CoordenadaLong != null)
                {
                    if (!esEstacion)
                    {
                        var distancia = CalcularDistanciaEnMetros(new CoordenadasDTO(usu.Empresa), new CoordenadasDTO(autDto));
                        if (distancia > 40)
                            respuesta = new RespuestaAutenticacionMobileDto()
                            {
                                IdUsuario = 0,
                                Exito = false,
                                Mensaje = Error.S0008,
                                token = string.Empty
                            };
                        else
                        {
                            respuesta = new RespuestaAutenticacionMobileDto()
                            {
                                Exito = true,
                                Mensaje = Exito.S0002,
                            };
                        };
                    }
                    else
                    {
                        if (pv.UnidadesAlmacen.EstacionCarburacion.CoordenadaLat != null && pv.UnidadesAlmacen.EstacionCarburacion.CoordenadaLong != null)
                        {
                            var distancia = CalcularDistanciaEnMetros(new CoordenadasDTO(pv.UnidadesAlmacen.EstacionCarburacion), new CoordenadasDTO(autDto));
                            if (distancia > 20)
                            {
                                respuesta = new RespuestaAutenticacionMobileDto()
                                {
                                    IdUsuario = 0,
                                    Exito = false,
                                    Mensaje = Error.S0008,
                                    token = string.Empty
                                };
                            }
                            else
                            {
                                respuesta = new RespuestaAutenticacionMobileDto()
                                {
                                    Exito = true
                                };
                            }
                        }
                        else
                        {
                            respuesta = new RespuestaAutenticacionMobileDto()
                            {
                                IdUsuario = 0,
                                Exito = false,
                                Mensaje = Error.S0010,
                                token = string.Empty
                            };
                        }
                    }
                }
                else
                {
                    respuesta = new RespuestaAutenticacionMobileDto()
                    {
                        IdUsuario = 0,
                        Exito = false,
                        Mensaje = Error.S0009,
                        token = string.Empty
                    };
                }
                Crear(new ControlAsistenciaDTO() { IdUsuario = usu.IdUsuario, Estatus = respuesta.Exito, Coordenadas = autDto.Coordenadas, IdEmpresa = usu.IdEmpresa });
            }
            else
            {
                respuesta = new RespuestaAutenticacionMobileDto()
                {
                    IdUsuario = 0,
                    Exito = true,
                    Mensaje = Exito.S0001,
                    token = string.Empty
                };
            }
            return respuesta;    
        }
        public static double CalcularDistancia(CoordenadasDTO point1, CoordenadasDTO point2)
        {
            //Retorna Kilometros
            double EarthRadius = 6371;
            double distance = 0;
            double Lat = (point2.Latitud - point1.Latitud) * (Math.PI / 180);
            double Lon = (point2.Longitud - point1.Longitud) * (Math.PI / 180);
            double a = Math.Sin(Lat / 2) * Math.Sin(Lat / 2) + Math.Cos(point1.Latitud * (Math.PI / 180)) * Math.Cos(point2.Latitud * (Math.PI / 180)) * Math.Sin(Lon / 2) * Math.Sin(Lon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            distance = EarthRadius * c;
            return distance;
        }
        public static double CalcularDistanciaEnMetros(CoordenadasDTO point1, CoordenadasDTO point2)
        {
            //Retorna Metros
            double EarthRadius = 6371;
            double distance = 0;
            double Lat = (point2.Latitud - point1.Latitud) * (Math.PI / 180);
            double Lon = (point2.Longitud - point1.Longitud) * (Math.PI / 180);
            double a = Math.Sin(Lat / 2) * Math.Sin(Lat / 2) + Math.Cos(point1.Latitud * (Math.PI / 180)) * Math.Cos(point2.Latitud * (Math.PI / 180)) * Math.Sin(Lon / 2) * Math.Sin(Lon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            distance = EarthRadius * c;
            return distance * 1000;
        }
    }
}
