using Application.MainModule.DTOs.Mobile;
using Application.MainModule.Servicios.AccesoADatos;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.MainModule;

namespace Application.MainModule.Servicios.Mobile
{
    public static class MenuServicio
    {
        private static bool _AppCompraEntraGas = false;
        private static bool _AppCompraGasFinalizarDescarga = false;
        private static bool _AppCompraGasIniciarDescarga = false;
        private static bool _AppCompraVerOCompra = false;
        private static bool _AppTomaLecturaEstacionCarb = false;
        private static bool _AppTomaLecturaAlmacenPral = false;
        private static bool _AppTomaLecturaPipa = false;
        private static bool _AppTomaLecturaCamionetaCilindro = false;
        private static bool _AppTomaLecturaReporteDelDia = false;
        private static bool _AppAutoconsumoEstacionCarb = false;
        private static bool _AppAutoconsumoInventarioGral = false;
        private static bool _AppAutoconsumoPipa = false;
        private static bool _AppCalibracionEstacionCarb = false;
        private static bool _AppCalibracionPipa = false;
        private static bool _AppCalibracionCamionetaCilindro = false;
        private static bool _AppRecargaEstacionCarb = false;
        private static bool _AppRecargaPipa = false;
        private static bool _AppRecargaCamionetaCilindro = false;
        private static bool _AppTraspasoEstacionCarb = false;
        private static bool _AppTraspasoPipa = false;
        private static bool _AppDisposicionEfectivo = false;
        //private static bool _AppPuntoVenta = false;
        private static bool _AppCamionetaPuntoVenta = false;
        private static bool _AppEstacionCarbPuntoVenta = false;
        private static bool _AppPipaPuntoVenta = false;
        public static List<MenuDto> Crear(int idUsuario)
        {
            List<MenuDto> lista = new List<MenuDto>();
            var usuario = new UsuarioDataAccess().Buscar(idUsuario);
            if (usuario.Roles != null)
            {
                foreach (Rol rol in usuario.Roles)
                {
                    if (rol.AppCompraEntraGas && !_AppCompraEntraGas)
                    {
                        lista.Add(ObtenerDatosMenu(ConfigurationManager.AppSettings["AppCompraEntraGas"]));
                        _AppCompraEntraGas = true;
                    }

                    if (rol.AppCompraGasIniciarDescarga && !_AppCompraGasIniciarDescarga)
                    {
                        lista.Add(ObtenerDatosMenu(ConfigurationManager.AppSettings["AppCompraGasIniciarDescarga"]));
                        _AppCompraGasIniciarDescarga = true;
                    }

                    if (rol.AppCompraGasFinalizarDescarga && !_AppCompraGasFinalizarDescarga)
                    {
                        lista.Add(ObtenerDatosMenu(ConfigurationManager.AppSettings["AppCompraGasFinalizarDescarga"]));
                        _AppCompraGasFinalizarDescarga = true;
                    }
                    if(rol.AppCompraVerOCompra && !_AppCompraVerOCompra)
                    {
                        lista.Add(ObtenerDatosMenu(ConfigurationManager.AppSettings["AppCompraVerOCompra"]));
                        _AppCompraVerOCompra = true;
                    }
                                        
                    //Estación Calibacion 
                    if (rol.AppTomaLecturaEstacionCarb && !_AppTomaLecturaEstacionCarb)
                    {
                        lista.Add(ObtenerDatosMenu(ConfigurationManager.AppSettings["AppTomaLecturaEstacionCarbInicial"]));
                        lista.Add(ObtenerDatosMenu(ConfigurationManager.AppSettings["AppTomaLecturaEstacionCarbFinal"]));
                        _AppTomaLecturaEstacionCarb = true;
                    }

                    //Almacen principal 
                    if (rol.AppTomaLecturaCamionetaCilindro && !_AppTomaLecturaAlmacenPral)
                    {
                        lista.Add(ObtenerDatosMenu(ConfigurationManager.AppSettings["AppTomaLecturaAlmacenPralInicial"]));
                        lista.Add(ObtenerDatosMenu(ConfigurationManager.AppSettings["AppTomaLecturaAlmacenPralFinal"]));
                        _AppTomaLecturaAlmacenPral = true;
                    }

                    //Pipa
                    if (rol.AppTomaLecturaPipa && !_AppTomaLecturaPipa)
                    {
                        lista.Add(ObtenerDatosMenu(ConfigurationManager.AppSettings["AppTomaLecturaPipaInicial"]));
                        lista.Add(ObtenerDatosMenu(ConfigurationManager.AppSettings["AppTomaLecturaPipaFinal"]));
                        _AppTomaLecturaPipa = true;
                    }

                    //Camioneta
                    if (rol.AppTomaLecturaCamionetaCilindro && !_AppTomaLecturaCamionetaCilindro)
                    {
                        lista.Add(ObtenerDatosMenu(ConfigurationManager.AppSettings["AppTomaLecturaCamionetaCilindroInicial"]));
                        lista.Add(ObtenerDatosMenu(ConfigurationManager.AppSettings["AppTomaLecturaCamionetaCilindroFinal"]));
                        _AppTomaLecturaCamionetaCilindro = true;
                    }

                    //Reporte del día 
                    if (rol.AppTomaLecturaReporteDelDia && !_AppTomaLecturaReporteDelDia)
                    {
                        lista.Add(ObtenerDatosMenu(ConfigurationManager.AppSettings["AppTomaLecturaReporteDelDia"]));
                        _AppTomaLecturaReporteDelDia = true;                        
                    }
                    //Auto-consumo Estacion Carb.
                    if (rol.AppAutoconsumoEstacionCarb && !_AppAutoconsumoEstacionCarb)
                    {
                        lista.Add(ObtenerDatosMenu(ConfigurationManager.AppSettings["AppAutoconsumoEstacionCarbInicial"]));
                        lista.Add(ObtenerDatosMenu(ConfigurationManager.AppSettings["AppAutoconsumoEstacionCarbFinal"]));
                        _AppAutoconsumoEstacionCarb = true;
                    }
                    //Auto-consumo Inventario Gral.
                    if (rol.AppAutoconsumoInventarioGral && !_AppAutoconsumoInventarioGral)
                    {
                        lista.Add(ObtenerDatosMenu(ConfigurationManager.AppSettings["AppAutoconsumoInventarioGralInicial"]));
                        lista.Add(ObtenerDatosMenu(ConfigurationManager.AppSettings["AppAutoconsumoInventarioGralFinal"]));
                        _AppAutoconsumoInventarioGral = true;
                    }
                    //Auto-consumo Pipa
                    if (rol.AppAutoconsumoPipa && !_AppAutoconsumoPipa)
                    {
                        lista.Add(ObtenerDatosMenu(ConfigurationManager.AppSettings["AppAutoconsumoPipaInicial"]));
                        lista.Add(ObtenerDatosMenu(ConfigurationManager.AppSettings["AppAutoconsumoPipaFinal"]));
                        _AppAutoconsumoPipa = true;
                    }
                    //Calibración Unidad de Gas Estación Carb. 
                    if(rol.AppCalibracionEstacionCarb && !_AppCalibracionEstacionCarb)
                    {
                        lista.Add(ObtenerDatosMenu(ConfigurationManager.AppSettings["AppCalibracionEstacionCarbInicial"]));
                        lista.Add(ObtenerDatosMenu(ConfigurationManager.AppSettings["AppCalibracionEstacionCarbFinal"]));
                        _AppCalibracionEstacionCarb = true;
                    }
                    //Calibración Unidad de Gas Pipa
                    if (rol.AppCalibracionPipa && !_AppCalibracionPipa)
                    {
                        lista.Add(ObtenerDatosMenu(ConfigurationManager.AppSettings["AppCalibracionPipaInicial"]));
                        lista.Add(ObtenerDatosMenu(ConfigurationManager.AppSettings["AppCalibracionPipaFinal"]));
                        _AppCalibracionPipa = true;
                    }
                    //Calibración camioneta cilindro
                    if (rol.AppCalibracionCamionetaCilindro && !_AppCalibracionCamionetaCilindro)
                    {
                        lista.Add(ObtenerDatosMenu(ConfigurationManager.AppSettings["AppCalibracionCamionetaCilindro"]));
                        //lista.Add(ObtenerDatosMenu(ConfigurationManager.AppSettings["AppCalibracionCamionetaCilindroInicial"]));
                        //lista.Add(ObtenerDatosMenu(ConfigurationManager.AppSettings["AppCalibracionCamionetaCilindroFinal"]));
                        _AppCalibracionCamionetaCilindro = true;
                    }
                    //Recarga - Gas Estación Carb.
                    if(rol.AppRecargaEstacionCarb && !_AppRecargaEstacionCarb)
                    {
                        lista.Add(ObtenerDatosMenu(ConfigurationManager.AppSettings["AppRecargaEstacionCarbInicial"]));
                        lista.Add(ObtenerDatosMenu(ConfigurationManager.AppSettings["AppRecargaEstacionCarbFinal"]));
                        _AppRecargaEstacionCarb = true;
                    }
                    //Recarga - Gas Pipa.
                    if (rol.AppRecargaPipa && !_AppRecargaPipa)
                    {
                        lista.Add(ObtenerDatosMenu(ConfigurationManager.AppSettings["AppRecargaPipaInicial"]));
                        lista.Add(ObtenerDatosMenu(ConfigurationManager.AppSettings["AppRecargaPipaFinal"]));
                        _AppRecargaPipa = true;
                    }
                    //Recarga - camioneta cilindro
                    if (rol.AppRecargaCamionetaCilindro && !_AppRecargaCamionetaCilindro)
                    {
                        lista.Add(ObtenerDatosMenu(ConfigurationManager.AppSettings["AppRecargaCamionetaCilindro"]));
                        _AppRecargaCamionetaCilindro = true;
                    }
                    //Traspaso - Gas Estacion Carb.
                    if (rol.AppTraspasoEstacionCarb && !_AppTraspasoEstacionCarb)
                    {
                        lista.Add(ObtenerDatosMenu(ConfigurationManager.AppSettings["AppTraspasoEstacionCarbInicial"]));
                        lista.Add(ObtenerDatosMenu(ConfigurationManager.AppSettings["AppTraspasoEstacionCarbFinal"]));
                        _AppTraspasoEstacionCarb = true;
                    }
                    //Traspaso - Gas Pipa.
                    if (rol.AppTraspasoPipa && !_AppTraspasoPipa)
                    {
                        lista.Add(ObtenerDatosMenu(ConfigurationManager.AppSettings["AppTraspasoPipaInicial"]));
                        lista.Add(ObtenerDatosMenu(ConfigurationManager.AppSettings["AppTraspasoPipaFinal"]));
                        _AppTraspasoPipa = true;
                    }
                    //Diposición de efectivo
                    if(rol.AppDisposicionEfectivo && !_AppDisposicionEfectivo)
                    {
                        lista.Add(ObtenerDatosMenu(ConfigurationManager.AppSettings["AppDisposicionAnticipoEstacionCarb"]));
                        lista.Add(ObtenerDatosMenu(ConfigurationManager.AppSettings["AppDisposicionCorteCajaEstacionCarb"]));
                        _AppDisposicionEfectivo = true;
                    }
                    //Punto de venta - Camioneta
                    if (rol.AppCamionetaPuntoVenta && !_AppCamionetaPuntoVenta)
                    {
                        lista.Add(ObtenerDatosMenu(ConfigurationManager.AppSettings["AppCamionetaPuntoVenta"]));
                        _AppCamionetaPuntoVenta = true;
                    }
                    //Punto de venta - Estacion de carb.
                    if (rol.AppEstacionCarbPuntoVenta && !_AppEstacionCarbPuntoVenta)
                    {
                        lista.Add(ObtenerDatosMenu(ConfigurationManager.AppSettings["AppEstacionCarbPuntoVenta"]));
                        _AppEstacionCarbPuntoVenta = true;
                    }
                    //Punto de venta - Pipa
                    if (rol.AppPipaPuntoVenta && !_AppPipaPuntoVenta)
                    {
                        lista.Add(ObtenerDatosMenu(ConfigurationManager.AppSettings["AppPipaPuntoVenta"]));
                        _AppPipaPuntoVenta = true;
                    }
                }
                setFalse();
                return lista;
            }
            else
            {
                setFalse();
                return lista;
            }
        }

        private static MenuDto ObtenerDatosMenu(string cadena)
        {
            var lista = FilterFunciones.ObtenerFields(cadena);
            return new MenuDto()
            {
                headerMenu = lista.FirstOrDefault(),
                name = lista.ElementAt(1),
                imageRef = lista.LastOrDefault(),
            };
        }

        private static void setFalse()
        {
            _AppCompraEntraGas = false;
            _AppCompraGasFinalizarDescarga = false;
            _AppCompraGasIniciarDescarga = false;
            _AppCompraVerOCompra = false;
            _AppTomaLecturaEstacionCarb = false;
            _AppTomaLecturaAlmacenPral = false;
            _AppTomaLecturaPipa = false;
            _AppTomaLecturaCamionetaCilindro = false;
            _AppTomaLecturaReporteDelDia = false;
            _AppAutoconsumoEstacionCarb = false;
            _AppAutoconsumoInventarioGral = false;
            _AppAutoconsumoPipa = false;
            _AppCalibracionEstacionCarb = false;
            _AppCalibracionPipa = false;
            _AppCalibracionCamionetaCilindro = false;
            _AppRecargaEstacionCarb = false;
            _AppRecargaPipa = false;
            _AppRecargaCamionetaCilindro = false;
            _AppTraspasoEstacionCarb = false;
            _AppTraspasoPipa = false;
            _AppDisposicionEfectivo = false;
            //_AppPuntoVenta = false;
            _AppCamionetaPuntoVenta = false;
            _AppEstacionCarbPuntoVenta = false;
            _AppPipaPuntoVenta = false;
        }
    }
}
