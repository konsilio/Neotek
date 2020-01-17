using System.Collections.Generic;
using System.Linq;
using Sagas.MainModule.Entidades;
using Application.MainModule.DTOs.Mobile;
using System;
using Sagas.MainModule.ObjetosValor.Enum;
using Application.MainModule.Servicios.Catalogos;
using Application.MainModule.Servicios.Seguridad;
using Application.MainModule.DTOs.Mobile.Cortes;

namespace Application.MainModule.AdaptadoresDTO.Mobile
{
    public class AnticiposCortesAdapter
    {
        public static DatosAnticiposCorteDto ToDTO(List<EstacionCarburacion> estaciones, List<UnidadAlmacenGas> unidades)
        {
            return new DatosAnticiposCorteDto()
            {
                // estaciones = estaciones.Select(x => ToDTO(x, unidades)).ToList(),
            };
        }

        private static EstacionesDto ToDTO(EstacionCarburacion estacion, List<UnidadAlmacenGas> unidades)
        {
            var unidadEstacion = unidades.Single(x => x.IdEstacionCarburacion.Value.Equals(estacion.IdEstacionCarburacion));
            var lecturaInicial = unidadEstacion.TomasLectura.Where(
                x => x.IdTipoEvento.Equals(TipoEventoEnum.Inicial)
                ).OrderBy(x => x.FechaRegistro).Last();
            return new EstacionesDto()
            {
                Medidor = TipoMedidorAdapter.ToDto(TipoMedidorGasServicio.Obtener(unidadEstacion.IdTipoMedidor.Value)),
                IdTipoMedidor = unidadEstacion.IdTipoMedidor.Value,
                IdAlmacenGas = (short)estacion.IdEstacionCarburacion,
                NombreAlmacen = estacion.Nombre,
                P5000Inicial = lecturaInicial.P5000.Value,
                P5000Final = unidadEstacion.P5000Actual.Value,
                AnticiposEstacion = ToDTO(unidadEstacion)
            };
        }
        public static EstacionesDto ToDTO(EstacionCarburacion estacion, UnidadAlmacenGas unidad)
        {
            var lecturaInicial = unidad.TomasLectura.Where(x => x.IdTipoEvento.Equals(TipoEventoEnum.Inicial)).OrderBy(x => x.FechaRegistro).Last();
            var lecturaFinal = unidad.TomasLectura.Where(x => x.IdTipoEvento.Equals(TipoEventoEnum.Final)).OrderBy(x => x.FechaRegistro).Last();
            return new EstacionesDto()
            {
                Medidor = TipoMedidorAdapter.ToDto(TipoMedidorGasServicio.Obtener(unidad.IdTipoMedidor.Value)),
                IdTipoMedidor = unidad.IdTipoMedidor.Value,
                IdAlmacenGas = (short)estacion.IdEstacionCarburacion,
                NombreAlmacen = estacion.Nombre,
                P5000Inicial = lecturaInicial != null ? lecturaInicial.P5000 ?? 0 : 0,
                P5000Final = lecturaFinal != null ? lecturaFinal.P5000 ?? 0 : 0,
                AnticiposEstacion = ToDTO(unidad),
            };
        }
        public static CamionetaDto ToDTO(Camioneta camioneta, UnidadAlmacenGas unidad)
        {

            var lecturaInicial = unidad.TomasLectura.Where(
                x => x.IdTipoEvento.Equals(TipoEventoEnum.Inicial)
                ).OrderBy(x => x.FechaRegistro).Last();
            return new CamionetaDto()
            {
                Medidor = TipoMedidorAdapter.ToDto(TipoMedidorGasServicio.Obtener(unidad.IdTipoMedidor.Value)),
                IdTipoMedidor = unidad.IdTipoMedidor.Value,
                IdAlmacenGas = (short)camioneta.IdCamioneta,
                IdCAlmacen = (short)camioneta.IdCamioneta,
                Numero = camioneta.Nombre,
                NombreAlmacen = camioneta.Nombre,
                //P5000Inicial = lecturaInicial.P5000.Value,
                //P5000Final = unidad.P5000Actual.Value,
                //AnticiposEstacion = ToDTO(unidad)
            };
        }
        public static PipaDto ToDTO(Pipa pipa, UnidadAlmacenGas unidad)
        {

            var lecturaInicial = unidad.TomasLectura.Where(
                x => x.IdTipoEvento.Equals(TipoEventoEnum.Inicial)
                ).OrderBy(x => x.FechaRegistro).Last();
            return new PipaDto()
            {
                Medidor = TipoMedidorAdapter.ToDto(TipoMedidorGasServicio.Obtener(unidad.IdTipoMedidor ?? 0)),
                IdTipoMedidor = unidad.IdTipoMedidor ?? 0,
                IdAlmacenGas = (short)pipa.IdPipa,
                NombreAlmacen = pipa.Nombre,
                CantidadP5000 = lecturaInicial.P5000 ?? 0,
                P5000Final = unidad.P5000Actual ?? 0,
                AnticiposEstacion = ToDTO(unidad)
            };
        }
        public static AnticiposEstacionDTO ToDTO(UnidadAlmacenGas unidad)
        {
            var anticiposEstacion = PuntoVentaServicio.ObtenerAnticipos(unidad).FindAll(x => x.DatosProcesados.Equals(false));
            decimal suma = anticiposEstacion.Sum(x => x.TotalAnticipado);
            return new AnticiposEstacionDTO()
            {
                //IdCAlmacenGas = (unidad.IdCAlmacenGas != null) ? unidad.IdCAlmacenGas : 0,
                IdCAlmacenGas = unidad.IdCAlmacenGas,
                IdEstacion = unidad.IdEstacionCarburacion != null ? unidad.IdEstacionCarburacion.Value : 0,
                Anticipos = /*ToDTO(anticiposEstacion)*/null,
                Total = suma
            };
        }

        public static List<AnticipoDto> ToDTO(List<VentaCorteAnticipoEC> anticiposEstacion)
        {
            List<AnticipoDto> anticipos = new List<AnticipoDto>();
            foreach (var anticipoEstacion in anticiposEstacion)
            {
                anticipos.Add(new AnticipoDto()
                {
                    ClaveOperacion = anticipoEstacion.FolioOperacion,
                    Fecha = anticipoEstacion.FechaRegistro,
                    Monto = anticipoEstacion.TotalAnticipado,
                    Total = anticipoEstacion.TotalVenta,
                    IdCAlmacenGas = (anticipoEstacion.CAlmacenGas.IdEstacionCarburacion != null) ? (short)anticipoEstacion.CAlmacenGas.IdEstacionCarburacion.Value : (short)0,

                });
            }
            return anticipos;
        }

        public static VentaCorteAnticipoEC FromDto(AnticipoDto dto, short idEmpresa, int idUsario, PuntoVenta puntoventa)
        {
            return new VentaCorteAnticipoEC()
            {
                IdEmpresa = idEmpresa,
                TotalAnticipado = dto.Monto,
                MontoRecortadoAnticipado = dto.Monto,
                FolioOperacion = dto.ClaveOperacion,
                FolioOperacionDia = dto.ClaveOperacion,
                TotalVenta = dto.Total,
                IdUsuarioRecibe = idUsario,
                IdOperadorChofer = puntoventa.IdOperadorChofer,
                IdPuntoVenta = puntoventa.IdPuntoVenta,
                UsuarioRecibe = dto.Recibe,
            };
        }

        public static VentaCorteAnticipoEC FromDto(CorteDto dto, short idEmpresa, int idUsuario, PuntoVenta puntoVenta)
        {
            return new VentaCorteAnticipoEC()
            {
                IdEmpresa = idEmpresa,
                TotalAnticipado = dto.Monto,
                MontoRecortadoAnticipado = dto.Monto,
                FolioOperacion = dto.ClaveOperacion,
                FolioOperacionDia = dto.ClaveOperacion,
                TotalVenta = dto.Total,
                IdUsuarioRecibe = dto.IdRecibe,
                IdOperadorChofer = puntoVenta.IdOperadorChofer,
                IdPuntoVenta = puntoVenta.IdPuntoVenta,
                UsuarioRecibe = dto.Recibe
            };
        }

        public static VentaCajaGeneral FromDTO(AnticipoDto dto, short idEmpresa, Usuario usuario, PuntoVenta puntoVenta, OperadorChofer operador,
            Usuario entrega, List<VentaPuntoDeVenta> deContado, List<VentaPuntoDeVenta> creditos)
        {
            decimal deContadoTotal = 0, creditoTotal = 0;
            decimal descuentoContado = 0, descuentoCredito = 0, descuentoTotal = 0;
            foreach (var contado in deContado)
            {
                deContadoTotal += contado.Total;
                descuentoContado += contado.Descuento;
                descuentoTotal += contado.Descuento;
            }
            foreach (var credito in creditos)
            {
                creditoTotal += credito.Total;
                descuentoCredito += credito.Descuento;
                descuentoTotal += credito.Descuento;
            }


            return new VentaCajaGeneral()
            {
                IdCAlmacenGas = puntoVenta.UnidadesAlmacen.IdCAlmacenGas,
                Year = (short)dto.Fecha.Year,
                Mes = (byte)dto.Fecha.Month,
                Dia = (byte)dto.Fecha.Day,
                IdEmpresa = idEmpresa,
                IdPuntoVenta = puntoVenta.IdPuntoVenta,
                IdOperadorChofer = puntoVenta.IdOperadorChofer,
                IdUsuarioRecibe = usuario.IdUsuario,
                IdUsuarioEntrega = entrega.IdUsuario,
                FolioOperacionDia = dto.ClaveOperacion,
                VentaTotal = dto.Total,
                VentaTotalContado = deContadoTotal,
                VentaTotalCredito = creditoTotal,
                OtrasVentas = 0,//Cambiar
                DescuentoOtrasVentas = 0,
                DescuentoCredito = descuentoCredito,
                DescuentoContado = descuentoContado,
                DescuentoTotal = 0,
                TodoCorrecto = false,
                PuntoVenta = puntoVenta.UnidadesAlmacen.Numero,
                OperadorChofer = operador.Usuario.Nombre + " " + operador.Usuario.Apellido1 + " " + operador.Usuario.Apellido2,
                UsuarioEntrega = entrega.Nombre + " " + entrega.Apellido1 + " " + entrega.Apellido2,
                UsuarioRecibe = usuario.Nombre + " " + usuario.Apellido1 + " " + usuario.Apellido2,
            };
        }

        public static DatosAnticiposCorteDto ToDTO(List<VentaPuntoDeVenta> ventas, List<VentaCorteAnticipoEC> anticipos, UnidadAlmacenGas almacen, bool esAnticipos = false)
        {

            if (esAnticipos)
                return new DatosAnticiposCorteDto()
                {
                    anticipos = ToDTOAnticipo(ventas, almacen)
                };
            else
                return new DatosAnticiposCorteDto()
                {
                    cortes = ToDTOCortes(ventas, almacen),
                    fechasCorte = EstraerFechas(ventas),
                    TotalAnticiposCorte = anticipos.Sum(x => x.TotalAnticipado)
                };
        }

        public static List<DateTime> EstraerFechas(List<VentaPuntoDeVenta> ventas)
        {
            List<DateTime> list = new List<DateTime>();
            foreach (var venta in ventas)
            {
                if (!list.Contains(venta.FechaAplicacion.Value)) list.Add(venta.FechaAplicacion.Value);
            }
            return list;
        }

        public static List<CorteDto> ToDTOCortes(List<VentaPuntoDeVenta> ventas, UnidadAlmacenGas almacen)
        {

            return ventas.Select(x => ToDTO(x, almacen)).ToList();
        }

        public static CorteDto ToDTO(VentaPuntoDeVenta venta, UnidadAlmacenGas almacen)
        {
            var recibe = TokenServicio.ObtenerUsuarioAplicacion();
            return new CorteDto()
            {
                ClaveOperacion = venta.FolioVenta,
                Tiket = venta.FolioVenta,
                Fecha = DateTime.Parse(venta.Year + "-" + venta.Mes + "-" + venta.Dia),
                IdCorte = (short)venta.IdPuntoVenta,
                IdCAlmacenGas = almacen.IdCAlmacenGas,
                Monto = venta.Total,
                Total = venta.Total,
                Recibe = recibe.Nombre + " " + recibe.Apellido1 + " " + recibe.Apellido2
            };
        }

        public static List<AnticipoDto> ToDTOAnticipo(List<VentaPuntoDeVenta> ventas, UnidadAlmacenGas almacen)
        {
            return ventas.Select(x => ToDTOAn(x, almacen)).ToList();
        }

        public static AnticipoDto ToDTOAn(VentaPuntoDeVenta venta, UnidadAlmacenGas almacen)
        {
            var recibe = TokenServicio.ObtenerUsuarioAplicacion();
            return new AnticipoDto()
            {
                Tiket = venta.FolioVenta,
                Fecha = DateTime.Parse(venta.Year + "-" + venta.Mes + "-" + venta.Dia),
                IdAnticipo = (short)venta.IdPuntoVenta,
                Recibe = recibe.Nombre + " " + recibe.Apellido1 + " " + recibe.Apellido2,
                Total = venta.Total,
                Monto = venta.Total,
                IdCAlmacenGas = almacen.IdCAlmacenGas,
                ClaveOperacion = venta.FolioVenta,
                FechaAnticipo = DateTime.Parse(venta.Year + "-" + venta.Mes + "-" + venta.Dia)
            };
        }

        public static VentaCajaGeneral FromDTO(CorteDto dto, short idEmpresa, Usuario usuario, PuntoVenta puntoVenta, OperadorChofer operador, Usuario entrega, List<VentaPuntoDeVenta> deContado, List<VentaPuntoDeVenta> creditos)
        {
            decimal deContadoTotal = 0, creditoTotal = 0;
            decimal descuentoContado = 0, descuentoCredito = 0, descuentoTotal = 0;
            foreach (var contado in deContado)
            {
                deContadoTotal += contado.Total;
                descuentoContado += contado.Descuento;
                descuentoTotal += contado.Descuento;
            }
            foreach (var credito in creditos)
            {
                creditoTotal += credito.Total;
                descuentoCredito += credito.Descuento;
                descuentoTotal += credito.Descuento;
            }
            return new VentaCajaGeneral()
            {
                IdCAlmacenGas = puntoVenta.UnidadesAlmacen.IdCAlmacenGas,
                Year = (short)dto.Fecha.Year,
                Mes = (byte)dto.Fecha.Month,
                Dia = (byte)dto.Fecha.Day,
                IdEmpresa = idEmpresa,
                IdPuntoVenta = puntoVenta.IdPuntoVenta,
                IdOperadorChofer = puntoVenta.IdOperadorChofer,
                IdUsuarioRecibe = dto.IdRecibe,
                IdUsuarioEntrega = dto.IdEntrega,
                FolioOperacionDia = dto.ClaveOperacion,
                VentaTotal = dto.Total,
                VentaTotalContado = creditoTotal,
                VentaTotalCredito = deContadoTotal,
                OtrasVentas = dto.Total,//Cambiar
                DescuentoOtrasVentas = 0,
                DescuentoCredito = descuentoCredito,
                DescuentoContado = descuentoContado,
                DescuentoTotal = descuentoTotal,
                TodoCorrecto = false,
                PuntoVenta = puntoVenta.UnidadesAlmacen.Numero,
                OperadorChofer = operador.Usuario.Nombre + " " + operador.Usuario.Apellido1 + " " + operador.Usuario.Apellido2,
                UsuarioEntrega = entrega.Nombre + " " + entrega.Apellido1 + " " + entrega.Apellido2,
                UsuarioRecibe = usuario.Nombre + " " + usuario.Apellido1 + " " + usuario.Apellido2,
            };
        }

        public static DatosAnticiposCorteDto ToDTOPipa(List<VentaPuntoDeVenta> ventas, List<VentaCorteAnticipoEC> anticipos, UnidadAlmacenGas unidadAlmacen, bool esAnticipos)
        {

            if (esAnticipos)
            {
                decimal cantidadVentas = ventas.Count > 0 ? ventas.Sum(x => x.Total) : 0;

                return new DatosAnticiposCorteDto()
                {
                    anticipos = ToDTOAnticipo(ventas, unidadAlmacen),
                    fechasCorte = EstraerFechas(ventas),
                    TotalAnticiposCorte = cantidadVentas
                };
            }
            else
            {
                decimal cantidadAnticipos = 0;
                if (unidadAlmacen.IdPipa != null || unidadAlmacen.IdPipa != 0 || unidadAlmacen.IdCamioneta != null || unidadAlmacen.IdPipa != 0)
                    cantidadAnticipos = ventas.Count > 0 ? ventas.Sum(x => x.Total) : 0;
                else
                    cantidadAnticipos = ventas.Count > 0 ? anticipos.Sum(x => x.TotalAnticipado) : 0;
                return new DatosAnticiposCorteDto()
                {

                    cortes = ToDTOCortes(ventas, unidadAlmacen),
                    fechasCorte = EstraerFechas(ventas),
                    TotalAnticiposCorte = cantidadAnticipos
                };
            }
        }
        #region Adaptador de DatosBusquedaCortesDTO para las pipas
        /// <summary>
        /// ToDTOBuscador
        /// Permite generar el dto para el buscador de fecvha para la tabla de cortes y anticipos,
        /// toma de parametros una entidad de UnidadAlmacenGas, los anticipos de esta , los cortes realizados,
        /// las ventas realizadas, la pipa que tiene asignada el almacen y si es anticipo o corte de caja 
        /// </summary>
        /// <param name="unidadAlmacen">Entidad de UnidadAlmacenGas con los datos de este </param>
        /// <param name="anticipos">Lista de anticipos encontrados en la fecha indicada </param>
        /// <param name="cortes">Lista de cortes encontrados en la fecha </param>
        /// <param name="ventasSinCorte">Ventas que de momento no cuentan con un corte</param>
        /// <param name="pipa">Entidad con los datos de la pipa</param>
        /// <param name="esAnticipos">Bandera que determina si se esta realizando un corte o anticipo</param>
        /// <returns>Objeto de tipo DatosBusquedaCortesDTO con la respuesta de los datos para realizar el corte o anticipo</returns>
        public static DatosBusquedaCortesDTO ToDTOBuscador(UnidadAlmacenGas unidadAlmacen, List<VentaCorteAnticipoEC> anticipos, List<VentaCorteAnticipoEC> cortes, List<VentaPuntoDeVenta> ventasSinCorte, Pipa pipa, bool esAnticipos, AlmacenGasTomaLectura lectura, AlmacenGasTomaLectura lecturaFinal)
        {
            return new DatosBusquedaCortesDTO()
            {
                anticipo = ToDTOAnticipos(anticipos),
                corte = ToDTOCortes(cortes),
                venta = ToDTOVentas(ventasSinCorte),
                estacion = esAnticipos ? ToDTOPipa(pipa, unidadAlmacen) : ToDTOPipa(pipa, unidadAlmacen, lectura, lecturaFinal)
            };
        }
        #endregion
        #region Adaptador de DatosBusquedaCortesDTO para las estaciones
        /// <summary>
        /// ToDTOBuscador
        /// Permite realizar la adaptación de los datos de cortes y anticipos para las estaciónes 
        /// toma de parametros la unidad de almacen gas , el listado de anticipos de esta , su lista de cortes,
        /// el listado de ventas sin corte, su respectiva entidad de Estación Carburación y si es uncorte o anticipo
        /// </summary>
        /// <param name="unidadAlmacen">Unidad almacen gas de la cual se buscan los datos</param>
        /// <param name="anticipos">Listado de anticipos que tiene esta unidad</param>
        /// <param name="cortes">Listado de cortes que tiene esta unidad</param>
        /// <param name="ventasSinCorte">Lista de ventas que no tienen corte </param>
        /// <param name="estacion">Entidad de Estación Carburación de la unidad almacen</param>
        /// <param name="esAnticipos">Determina si la busqueda es para anticipos o cortes de caja</param>
        /// <returns>Objeto de tipo DatosBusquedaCortesDTO adaptado con los datos enviados </returns>
        public static DatosBusquedaCortesDTO ToDTOBuscador(UnidadAlmacenGas unidadAlmacen, List<VentaCorteAnticipoEC> anticipos, List<VentaCorteAnticipoEC> cortes, List<VentaPuntoDeVenta> ventasSinCorte, EstacionCarburacion estacion, bool esAnticipos, AlmacenGasTomaLectura lectura, AlmacenGasTomaLectura lecturaFinal)
        {
            return new DatosBusquedaCortesDTO()
            {
                anticipo = ToDTOAnticipos(anticipos),
                corte = ToDTOCortes(cortes),
                venta = ToDTOVentas(ventasSinCorte),
                estacion = esAnticipos ? ToDTOEstacion(estacion, unidadAlmacen) : ToDTOEstacion(estacion, unidadAlmacen, lectura, lecturaFinal)
            };
        }
        #endregion
        #region Adaptador de DatosBusquedaCortesDTO para las camionetas
        /// <summary>
        /// ToDTOBuscador
        /// Permite construir el DTO de respuesta de DatosBusquedaCortesDTO para 
        /// una camioneta, se enviaran de parametros la unidad de almacen gas correspondiente, 
        /// el listado de anticipos de la estación, el listado de cortes de la misma , las ventas sin 
        /// corte encontradas, una entidad de tipo Camioneta y finalmente si es anticipo o corte de caja, rertonara 
        /// el objeto construido
        /// </summary>
        /// <param name="unidadAlmacen">Entidad de tipo UnidadAlmacenGas de la camioneta</param>
        /// <param name="anticipos">Lista de anticipos encontrados en la camioneta </param>
        /// <param name="cortes">Lista de cortes encontrados en la camioneta</param>
        /// <param name="ventasSinCorte">Ventas sin corte de la camioneta</param>
        /// <param name="camioneta">Entidad que reprecenta la camioneta de la que se sacan los datos para el anticipo o corte de caja</param>
        /// <param name="esAnticipos">Determina si se esta realizando un anticipo o corte de caja</param>
        /// <returns>Objeto de típo DatosBusquedaCortesDTO adaptado con los datos enviados como parametros</returns>
        public static DatosBusquedaCortesDTO ToDTOBuscador(UnidadAlmacenGas unidadAlmacen, List<VentaCorteAnticipoEC> anticipos, List<VentaCorteAnticipoEC> cortes, List<VentaPuntoDeVenta> ventasSinCorte, Camioneta camioneta, bool esAnticipos)
        {
            return new DatosBusquedaCortesDTO()
            {
                anticipo = ToDTOAnticipos(anticipos),
                corte = ToDTOCortes(cortes),
                venta = ToDTOVentas(ventasSinCorte),
                estacion = ToDTOEstacion(camioneta, unidadAlmacen)
            };
        }
        #endregion

        #region Creación del objeto DTO de AnticipoInfoDTO para cortes y anticipos
        /// <summary>
        /// Permite generar un objeto de tipo AnticipoInfoDTO para ser 
        /// retornado en un objeto de tipo DatosBusquedaCortesDTO para almacenar 
        /// los datos de anticipos relizados en una fecha especifica del  buscador de 
        /// anticipos o cortes 
        /// </summary>
        /// <param name="anticipos">Listado de anticipos encontrados en la busqueda</param>
        /// <returns>Objeto de tipo AnticipoInfoDTO con los datos del anticipo</returns>
        public static AnticipoInfoDTO ToDTOAnticipos(List<VentaCorteAnticipoEC> anticipos)
        {
            return new AnticipoInfoDTO()
            {
                totalAnticipos = (anticipos != null && anticipos.Count > 0) ? anticipos.Sum(x => x.TotalAnticipado) : 0,
                anticipos = anticipos.Select(x => ToDTOAnticiposList(x)).ToList()
            };
        }
        /// <summary>
        /// ToDTOAnticiposList
        /// Permite retornar un objeto de tipo AnticipoDto con los datos de un
        /// anticipo
        /// </summary>
        /// <param name="anticipo">Entidad de tipo VentaCorteAnticipoEC con los datos del anticipo</param>
        /// <returns>Entidad transformada en un DTO con los datos de esta entidad </returns>
        public static AnticipoDto ToDTOAnticiposList(VentaCorteAnticipoEC anticipo)
        {
            return new AnticipoDto()
            {
                ClaveOperacion = anticipo.FolioOperacionDia,
                Fecha = anticipo.FechaAplicacion,
                FechaAnticipo = anticipo.FechaCorteAnticipo,
                //Monto = anticipo.TotalAnticipado,
                IdCAlmacenGas = anticipo.IdCAlmacenGas,
                Tiket = anticipo.FolioOperacionDia,
                Total = anticipo.TotalAnticipado,
                Recibe = anticipo.UsuarioRecibe
            };
        }
        #endregion
        #region Creación del objeto DTO CorteInforDTO para cortes y anticipos
        public static CorteInfoDTO ToDTOCortes(List<VentaCorteAnticipoEC> cortes)
        {
            return new CorteInfoDTO()
            {
                totalCortes = (cortes != null && cortes.Count > 0) ? cortes.Sum(x => x.TotalAnticipado) : 0,
                cortes = cortes.Select(x => ToDTOCorteList(x)).ToList()
            };
        }
        /// <summary>
        /// ToDTOCorteList
        /// Retorna un objeto de tipo CorteDto con los datos de corte 
        /// para el objeto a mostrar en la tabla de cortes y anticipos para mobile
        /// </summary>
        /// <param name="corte">Entidad de tipo VentaCorteAnticipoEC con los datos del corte </param>
        /// <returns>Objeto de tipo CorteDto con los datos adaptados en DTO </returns>
        public static CorteDto ToDTOCorteList(VentaCorteAnticipoEC corte)
        {
            return new CorteDto()
            {
                ClaveOperacion = corte.FolioOperacionDia,
                Fecha = corte.FechaAplicacion,
                FechaCorte = corte.FechaCorteAnticipo,
                IdCAlmacenGas = corte.IdCAlmacenGas,
                Tiket = corte.FolioOperacion,
                Total = corte.TotalAnticipado,
                Recibe = corte.UsuarioRecibe
            };
        }
        #endregion
        #region Creación del objeto DTO de VentasInfoDTO para los cortes y anticipos
        /// <summary>
        /// ToDTOVentas
        /// Permite crear un objeto de tipo VentasInfoDTO con los datos de las 
        /// ventas que se envian como parametros para el apartado de cortes y anticipos 
        /// </summary>
        /// <param name="ventasSinCorte">Listado de entidades de tipo VentaPuntoDeVenta que no tienen cortes </param>
        /// <returns>Objeto de tipo VentasInfoDTO con los datos adaptados</returns>
        public static VentasInfoDTO ToDTOVentas(List<VentaPuntoDeVenta> ventasSinCorte)
        {
            return new VentasInfoDTO()
            {
                totalVentas = (ventasSinCorte != null && ventasSinCorte.Count > 0) ? ventasSinCorte.Sum(x => x.Total) : 0,
                ventas = ventasSinCorte.Select(x => ToDTOVentasList(x)).ToList()
            };
        }
        /// <summary>
        /// ToDTOVentasList
        /// Permite adaptar una entidad de tipo VentaPuntoDeVenta a un 
        /// objeto VentaDTO para el apartado de cortes y anticipos de 
        /// la app mobile
        /// </summary>
        /// <param name="venta">Entidad de tipo VentaPuntoDeVenta la cual se adaptara</param>
        /// <returns>Objeto de tipo VentaDTO con los datos de la entidad</returns>
        public static VentaDTO ToDTOVentasList(VentaPuntoDeVenta venta)
        {
            return new VentaDTO()
            {
                FolioVenta = venta.FolioVenta,
                Total = venta.Total,
                Fecha = venta.FechaRegistro,
                Hora = venta.FechaRegistro.TimeOfDay,
                IdCliente = venta.IdCliente,
                Subtotal = venta.Subtotal,
                Iva = venta.Iva,
                Factura = venta.RequiereFactura,
                Credito = venta.VentaACredito,
                Efectivo = venta.EfectivoRecibido ?? 0,
                Cambio = venta.CambioRegresado ?? 0,

            };
        }
        #endregion
        #region Creación del objeto DTO para los datos de la pipa para el  corte y anticipos
        /// <summary>
        /// Permite generar el Objeto EstacionesDto con los datos que se envian de parametros
        /// </summary>
        /// <param name="pipa">Entidad de tipo Pipa con los datos de la Pipa</param>
        /// <param name="unidadAlmacen">Entidad de tipo UnidadAlmacenGas que le corresponde a la pipa</param>
        /// <returns>Objeto de tipo EstacionesDto con los datos adaptados</returns>
        public static EstacionesDto ToDTOPipa(Pipa pipa, UnidadAlmacenGas unidadAlmacen, AlmacenGasTomaLectura lecturaInicial, AlmacenGasTomaLectura lecturaFinal)
        {
            return new EstacionesDto()
            {
                IdAlmacenGas = unidadAlmacen.IdCAlmacenGas,
                NombreAlmacen = pipa.Nombre,
                NombrePipa = pipa.Nombre,
                PorcentajeMedidor = lecturaInicial.Porcentaje ?? 0,
                P5000Inicial = lecturaInicial.P5000 ?? 0,
                P5000Final = lecturaFinal.P5000 ?? 0

            };
        }
        public static EstacionesDto ToDTOPipa(Pipa pipa, UnidadAlmacenGas unidadAlmacen)
        {
            return new EstacionesDto()
            {
                IdAlmacenGas = unidadAlmacen.IdCAlmacenGas,
                NombreAlmacen = pipa.Nombre,
                NombrePipa = pipa.Nombre,
                PorcentajeMedidor = 0,
                P5000Inicial = 0,
                P5000Final = 0

            };
        }
        #endregion
        #region Creación del objeto DTO para los datos de la Estación de carburación para el corte o anticipo
        /// <summary>
        /// ToDTOEstacion
        /// Permite realizar la adaptación de los datos de la estación de carburación 
        /// en un objeto de tipo EstacionesDto, se envian como parametros una entidad 
        /// de EstacionCarburacion y una de UnidadAlmacenGas, retornara el objeto
        /// adaptado
        /// </summary>
        /// <param name="estacion">Entidad de la estación de carburación para el corte o anticipo</param>
        /// <param name="unidadAlmacen">Entidad de la UnidadAlmacenGas de la estación para el corte o anticipo</param>
        /// <returns>Objeto de tipo EstacionesDto adaptado con los datos </returns>
        public static EstacionesDto ToDTOEstacion(EstacionCarburacion estacion, UnidadAlmacenGas unidadAlmacen, AlmacenGasTomaLectura lecturaInicial, AlmacenGasTomaLectura lecturaFinal)
        {
            return new EstacionesDto()
            {
                IdAlmacenGas = unidadAlmacen.IdCAlmacenGas,
                NombreAlmacen = estacion.Nombre,
                NombrePipa = estacion.Nombre,
                PorcentajeMedidor = lecturaFinal.Porcentaje ?? 0,
                P5000Inicial = lecturaInicial.P5000 ?? 0,
                P5000Final = lecturaFinal.P5000 ?? 0
            };
        }
        public static EstacionesDto ToDTOEstacion(EstacionCarburacion estacion, UnidadAlmacenGas unidadAlmacen)
        {
            return new EstacionesDto()
            {
                IdAlmacenGas = unidadAlmacen.IdCAlmacenGas,
                NombreAlmacen = estacion.Nombre,
                NombrePipa = estacion.Nombre,
                PorcentajeMedidor = 0,
                P5000Inicial = 0,
                P5000Final = 0
            };
        }
        #endregion
        #region Creación del objeto DTO para los datos de la camioneta para el  corte y anticipos
        /// <summary>
        /// ToDTOEstacion
        /// Permite crear un objeto de tipo EstacionesDto adaptando los datos que se envian de parametro,
        /// se toman como parametros una entidad de tipo Camioneta y una de tipo UnidadAlmacenGas de esta
        /// y retorna el objeto adaptado.
        /// </summary>
        /// <param name="camioneta">Entidad de tipo Camioneta de la que se hace corte o anticipo</param>
        /// <param name="unidadAlmacen">Entidad de tipo UnidadAlmacenGas de la que se hace corte o anticipo</param>
        /// <returns>Objeto EstacionesDto adaptado con los datos </returns>
        private static EstacionesDto ToDTOEstacion(Camioneta camioneta, UnidadAlmacenGas unidadAlmacen)
        {
            return new EstacionesDto()
            {
                IdAlmacenGas = unidadAlmacen.IdCAlmacenGas,
                NombreAlmacen = camioneta.Nombre,
                NombrePipa = camioneta.Nombre,
                PorcentajeMedidor = unidadAlmacen.PorcentajeActual
            };
        }
        /// <summary>
        /// ToDTO
        /// Permite realizar la adaptación de los datos del corte 
        /// para ser mostrado en la verificacion del corte 
        /// </summary>
        /// <param name="corte">Entidad de tipo VentaCorteAnticipoEC con los datos a adaptar al dto</param>
        /// <returns>Datos adaptados en un objeto de tipo CorteDto</returns>
        public static CorteDto ToDTO(VentaCorteAnticipoEC corte)
        {
            return new CorteDto()
            {
                ClaveOperacion = corte.FolioOperacionDia,
                Fecha = corte.FechaRegistro,
                FechaCorte = corte.FechaCorteAnticipo,
                IdCAlmacenGas = corte.IdCAlmacenGas,
                Total = corte.TotalAnticipado
            };
        }
        #endregion
    }
}
