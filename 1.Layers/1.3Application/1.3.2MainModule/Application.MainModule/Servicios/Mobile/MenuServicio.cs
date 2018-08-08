using Application.MainModule.DTOs.Mobile;
using Application.MainModule.Servicios.AccesoADatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.MainModule.Servicios.Mobile
{
    public static class MenuServicio
    {
        public static List<MenuDto> Crear(int idUsuario)
        {
            List<MenuDto> lista = new List<MenuDto>();
            var usuario = new UsuarioDataAccess().Buscar(idUsuario);
            if (usuario.Rol != null)
            {
                if (usuario.Rol.CompraCapRequisicion)
                {
                    lista.Add(new MenuDto()
                    {
                        headerMenu = "Compra - Captura Requisicion",
                        name = "Iniciar descarga",
                        imageRef = "ic_iniciar_descarga",
                    });

                }
                return lista;
            }
            else return lista;
        }
    }
}
