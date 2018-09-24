using Application.MainModule.AdaptadoresDTO.Seguridad;
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
    public static class PaisServicio
    {
        public static List<PaisDTO> ListaPaises()
        {
            List<PaisDTO> lpaises = AdaptadoresDTO.Seguridad.PaisAdapter.ToDTO(new PaisDataAccess().ListaPaises());
            return lpaises;
        }
        public static Pais Obtener(byte IdPais)
        {
            return new PaisDataAccess().BuscarIdPais(IdPais);
        }
    }
}
