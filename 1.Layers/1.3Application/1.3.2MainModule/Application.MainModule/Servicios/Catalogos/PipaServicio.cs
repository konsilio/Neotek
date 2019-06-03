using Application.MainModule.AdaptadoresDTO.Catalogo;
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
    public static class PipaServicio
    {
        public static RespuestaDto Registrar(Pipa entidad)
        {
            return new PipaDataAccess().Insertar(entidad);
        }
        public static RespuestaDto Modificar(Pipa entidad)
        {
            return new PipaDataAccess().Actualizar(entidad);
        }
        public static RespuestaDto Borrar(int id)
        {
            var pip = Obtener(id);
            return new PipaDataAccess().Borrar(PipaAdapter.FromEntity(pip));
        }
        public static List<Pipa> Obtener()
        {
            var empresa = EmpresaServicio.Obtener(TokenServicio.ObtenerIdEmpresa());

            if (empresa.EsAdministracionCentral)
                return new PipaDataAccess().ObtenerPipas();
            else
                return new PipaDataAccess().ObtenerPipas(empresa.IdEmpresa);
        }
        public static List<Pipa> Obtener(short idEmpresa)
        {
            return new PipaDataAccess().ObtenerPipas(idEmpresa).ToList();
        }
        public static Pipa Obtener(short idEmpresa, string nombre, string numero)
        {
            return new PipaDataAccess().ObtenerPipa(idEmpresa, nombre, numero);
        }
        public static Pipa Obtener(int id)
        {
            return new PipaDataAccess().ObtenerPipa(id);
        }
        public static List<Pipa> Obtener(List<PipaDTO> pipas)
        {
            List<Pipa> lista = new List<Pipa>();
            foreach (var item in pipas)
                if (item.Activo && Obtener(item.IdPipa) != null)
                    lista.Add(Obtener(item.IdPipa));
            return lista;
        }
        public static Pipa Obtener(int id, bool activo)
        {
            return new PipaDataAccess().ObtenerPipa(id, activo);
        }
        public static RespuestaDto NoExiste()
        {
            string mensaje = string.Format(Error.NoExiste, "La pipa");

            return new RespuestaDto()
            {
                ModeloValido = true,
                Mensaje = mensaje,
                MensajesError = new List<string>() { mensaje },
            };
        }
    }
}
