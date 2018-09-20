using Application.MainModule.AdaptadoresDTO.Seguridad;
using Application.MainModule.DTOs.Catalogo;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.Servicios.Seguridad;
using Exceptions.MainModule.Validaciones;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Catalogos
{
    public static class CentroCostoServicio
    {
        public static RespuestaDto RegistrarCentroCosto(CentroCosto cCosto)
        {   
            return new CentroCostoDataAccess().Insertar(cCosto);
        }

        public static RespuestaDto ModificarCentroCosto(CentroCosto cCosto)
        {
            return new CentroCostoDataAccess().Actualizar(cCosto);
        }

        public static List<CentroCosto> Obtener()
        {
            var empresa = EmpresaServicio.Obtener(TokenServicio.ObtenerIdEmpresa());

            if (empresa.EsAdministracionCentral)
                return new CentroCostoDataAccess().BuscarTodos();
            else
                return new CentroCostoDataAccess().BuscarTodos(empresa.IdEmpresa);
        }

        public static CentroCosto Obtener(int IdCentroCosto)
        {
            return new CentroCostoDataAccess().Buscar(IdCentroCosto);
        }

        public static bool Existe(string numero, string descripcion)
        {
            var ccDAccess = new CentroCostoDataAccess();
            var idEmpresa = TokenServicio.ObtenerIdEmpresa();

            var centro = ccDAccess.BuscarNumero(idEmpresa, numero);
            if (centro != null) return true;

            centro = ccDAccess.BuscarDescripcion(idEmpresa, descripcion);
            if (centro != null) return true;

            return false;
        }

        public static RespuestaDto NoExiste()
        {
            string mensaje = string.Format(Error.NoExiste, "El Centro de Costo");

            return new RespuestaDto()
            {
                ModeloValido = true,
                Mensaje = mensaje,
                MensajesError = new List<string>() { mensaje },
            };
        }
    }
}
