﻿using Application.MainModule.DTOs.Seguridad;
using Application.MainModule.Servicios.AccesoADatos;
using Exceptions.MainModule.Validaciones;
using Utilities.MainModule;
using Security.MainModule.Token_Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.Servicios.Mobile;
using Application.MainModule.Servicios.Catalogos;
using Application.MainModule.DTOs;

namespace Application.MainModule.Servicios.Seguridad
{
    public static class AutenticarServicio
    {
        public static RespuestaAutenticacionDto AutenticarUsuario(AutenticacionDto autDto)
        {
            if (autDto.IdEmpresa < 1 || string.IsNullOrEmpty(autDto.Usuario) || string.IsNullOrEmpty(autDto.Password))
                return new RespuestaAutenticacionDto()
                {
                    Exito = false,
                    Mensaje = Error.S0003,
                    token = string.Empty
                };

            UsuarioAplicacionDto usuario;
            // Validamos si es un usuario de la administración central
            // y buscamos la existencia del usuario, validando su contraseña
            usuario = AutenticarUsuarioDeEmpresa(autDto);
            List<MenuDto> listMnu = new List<MenuDto>();
            if (usuario.autenticado)
            {
                MenuDto menu = RolServicio.CrearMenu(usuario.IdUsuario);
                var claims = new[]
                {
                    new Claim(TokenEtiquetasEnum.Autenticado, usuario.autenticado.ToString()),
                    new Claim(TokenEtiquetasEnum.NombreUsuario, autDto.Usuario),
                    new Claim(TokenEtiquetasEnum.IdUsuario, usuario.IdUsuario.ToString()),
                    new Claim(TokenEtiquetasEnum.IdEmpresa, usuario.IdEmpresa.ToString()),
                    new Claim(TokenEtiquetasEnum.EsAdminCentral, usuario.AdminCentral ? "true": "false"),
                    new Claim(TokenEtiquetasEnum.EsSuperUsuario, usuario.SuperUsuario ? "true": "false"),
                };

                var min = Math.Truncate(FechasFunciones.ObtenerMinutosEntreDosFechas(DateTime.Now, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59)));
                var us = UsuarioServicio.Obtener(usuario.IdUsuario);
                return new RespuestaAutenticacionDto()
                {
                    IdUsuario = usuario.IdUsuario,
                    Exito = true,
                    Mensaje = string.Concat(us.Empresa.UrlLogotipo180px, "|", us.Nombre, " ", us.Apellido1, "|", us.NombreUsuario),
                    token = TokenGenerator.GenerateTokenJwt(claims, autDto.Password, Convert.ToInt32(min).ToString()),
                    LstRoles = menu
                };
            }
            else
                return new RespuestaAutenticacionDto()
                {
                    IdUsuario = 0,
                    Exito = false,
                    Mensaje = Error.S0003,
                    token = string.Empty

                };
        }

        public static RespuestaAutenticacionMobileDto AutenticarUsuarioMobile(DTOs.Mobile.LoginFbDTO autDto)
        {
            var aut = AutenticarUsuario(autDto);
            var usuario = new UsuarioDataAccess().Buscar(aut.IdUsuario);
            bool esChofer = false, esEstacion = false, hayLectura = false, hayLecturaFinal = false;
            List<DTOs.Mobile.MenuDto> menu = new List<DTOs.Mobile.MenuDto>();
            if (usuario != null)
            {
                if (usuario.OperadoresChoferes != null && usuario.OperadoresChoferes.Count != 0)
                {
                    esChofer = true;
                    var operadorDTO = PuntoVentaServicio.ObtenerOperador(usuario.IdUsuario);
                    var operador = usuario.OperadoresChoferes.FirstOrDefault(x => x.Activo);
                    //var puntoVenta = PuntoVentaServicio.Obtener(operador.IdOperadorChofer);
                    var puntoVenta = PuntoVentaServicio.Obtener(operador);

                    if (puntoVenta != null)
                    {

                        var unidadAlmacen = puntoVenta.UnidadesAlmacen;
                        if (unidadAlmacen.IdEstacionCarburacion != null && unidadAlmacen.IdEstacionCarburacion != 0)
                            esEstacion = true;

                        var ca = ControlAsistenciaServicio.CalcularEntrada(usuario, autDto, puntoVenta, esEstacion);
                        if (!ca.Exito)
                            return ca;

                        var lecturaFinal = LecturaGasServicio.ObtenerUltimaLecturaFinal(unidadAlmacen.IdCAlmacenGas, DateTime.Now);
                        if (lecturaFinal != null && !esEstacion)
                            return new RespuestaAutenticacionMobileDto()
                            {
                                IdUsuario = 0,
                                Exito = false,
                                Mensaje = Error.S0006,
                                token = string.Empty,
                                listMenu = new List<DTOs.Mobile.MenuDto>(),
                            };
                        var ultimaLectura = LecturaGasServicio.ObtenerUltimaLecturaInicial(unidadAlmacen.IdCAlmacenGas, DateTime.Now);
                        if (ultimaLectura != null)
                            hayLectura = true;
                        else
                        {
                            if (!esEstacion)
                                return new RespuestaAutenticacionMobileDto()
                                {
                                    IdUsuario = 0,
                                    Exito = false,
                                    Mensaje = Error.S0007,
                                    token = string.Empty,
                                    listMenu = new List<DTOs.Mobile.MenuDto>(),
                                };
                        }
                        if (unidadAlmacen.EsGeneral)
                        {
                            hayLectura = true;
                            esChofer = false;
                        }
                    }
                    else
                    {
                        var ca = ControlAsistenciaServicio.CalcularEntrada(usuario, autDto);
                        if (!ca.Exito)
                            return ca;

                    }
                    menu = MenuServicio.Crear(usuario, hayLectura, esEstacion, esChofer);
                }
                else
                {
                    hayLectura = true;
                    var ca = ControlAsistenciaServicio.CalcularEntrada(usuario, autDto);
                    if (!ca.Exito)
                        return ca;
                }
                menu = MenuServicio.Crear(usuario, hayLectura, esEstacion, esChofer);
            }
            else
            {
                return new RespuestaAutenticacionMobileDto()
                {
                    IdUsuario = 0,
                    Exito = true,
                    Mensaje = Error.S0003,
                    token = string.Empty,
                    listMenu = new List<DTOs.Mobile.MenuDto>(),
                };
            }
            return new RespuestaAutenticacionMobileDto()
            {
                IdUsuario = aut.IdUsuario,
                Exito = aut.Exito,
                Mensaje = menu.Count.Equals(0) ? Error.S0005 : aut.Mensaje,
                token = aut.token,
                listMenu = menu,
            };
        }

        private static UsuarioAplicacionDto AutenticarUsuarioDeEmpresa(AutenticacionDto autDto)
        {
            var usuario = new UsuarioDataAccess().Buscar(autDto.IdEmpresa, autDto.Usuario, autDto.Password);
            if (usuario != null)
            {
                var autUsuario = new UsuarioAplicacionDto()
                {
                    autenticado = true,
                    SuperUsuario = usuario.EsSuperAdmin,
                    IdEmpresa = usuario.IdEmpresa,
                    IdUsuario = usuario.IdUsuario,
                    UrlImg = usuario.Empresa.UrlLogotipo180px,
                };

                if (usuario.EsAdministracionCentral)
                    autUsuario.AdminCentral = true;
                else
                    autUsuario.AdminCentral = false;

                return autUsuario;
            }
            else
                return new UsuarioAplicacionDto()
                {
                    autenticado = false,
                };
        }
        public static double CalcularDistancia(CoordenadasDTO point1, CoordenadasDTO point2)
        {
            //Retorna Km
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
            //Retorna Km
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
