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

                    if (rol.AppCompraGasFinalizarDescarga && !_AppCompraGasFinalizarDescarga)
                    { 
                        lista.Add(ObtenerDatosMenu(ConfigurationManager.AppSettings["AppCompraGasFinalizarDescarga"]));
                        _AppCompraGasFinalizarDescarga = true;
                    }

                    if (rol.AppCompraGasIniciarDescarga && !_AppCompraGasIniciarDescarga)
                    {
                        lista.Add(ObtenerDatosMenu(ConfigurationManager.AppSettings["AppCompraGasIniciarDescarga"]));
                        _AppCompraGasIniciarDescarga = true;
                    }

                    if (rol.AppCompraVerOCompra && !_AppCompraVerOCompra)
                    {
                        lista.Add(ObtenerDatosMenu(ConfigurationManager.AppSettings["AppCompraVerOCompra"]));
                        _AppCompraVerOCompra = true;
                    }
                  

                }                
                return lista;
            }
            else return lista;
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
    }
}
