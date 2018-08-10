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
        public static List<MenuDto> Crear(int idUsuario)
        {
            List<MenuDto> lista = new List<MenuDto>();
            var usuario = new UsuarioDataAccess().Buscar(idUsuario);
            if (usuario.Roles != null)
            {
                foreach (Rol rol in usuario.Roles)
                {
                    if(rol.AppCompraEntraGas)
                        lista.Add(ObtenerDatosMenu(ConfigurationManager.AppSettings["AppCompraEntraGas"]));

                    if (rol.AppCompraGasFinalizarDescarga)
                        lista.Add(ObtenerDatosMenu(ConfigurationManager.AppSettings["AppCompraGasFinalizarDescarga"]));

                    if (rol.AppCompraGasIniciarDescarga)
                        lista.Add(ObtenerDatosMenu(ConfigurationManager.AppSettings["AppCompraGasIniciarDescarga"]));

                    if (rol.AppCompraVerOCompra)
                        lista.Add(ObtenerDatosMenu(ConfigurationManager.AppSettings["AppCompraVerOCompra"]));
                  

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
