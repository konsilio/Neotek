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
        public static List<MenuDto> Crear(int idUsuario)
        {
            List<MenuDto> lista = new List<MenuDto>();
            var usuario = new UsuarioDataAccess().Buscar(idUsuario);
            if (usuario.Roles != null)
            {
                foreach (Rol rol in usuario.Roles)
                {
                    if (rol.AppTomaLecturaCamionetaCilindro && !_AppCompraEntraGas)
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
                    if (rol.AppTomaLecturaCamionetaCilindro && _AppTomaLecturaCamionetaCilindro)
                    {
                        lista.Add(ObtenerDatosMenu(ConfigurationManager.AppSettings["AppTomaLecturaCamionetaCilindroInicial"]));
                        lista.Add(ObtenerDatosMenu(ConfigurationManager.AppSettings["AppTomaLecturaCamionetaCilindroFinal"]));
                        _AppTomaLecturaCamionetaCilindro = true;
                    }

                    //Reporte del día 
                    if (rol.AppTomaLecturaReporteDelDia && _AppTomaLecturaReporteDelDia)
                    {
                        lista.Add(ObtenerDatosMenu(ConfigurationManager.AppSettings["AppTomaLecturaReporteDelDia"]));
                        _AppTomaLecturaReporteDelDia = true;
                        
                    }
                    //Auto-consumo Estacion Carb.
                    if (rol.AppAutoconsumoEstacionCarb && _AppAutoconsumoEstacionCarb)
                    {
                        lista.Add(ObtenerDatosMenu(ConfigurationManager.AppSettings["AppAutoconsumoEstacionCarbInicial"]));
                        lista.Add(ObtenerDatosMenu(ConfigurationManager.AppSettings["AppAutoconsumoEstacionCarbFinal"]));
                        _AppAutoconsumoEstacionCarb = true;

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
        }
    }
}
