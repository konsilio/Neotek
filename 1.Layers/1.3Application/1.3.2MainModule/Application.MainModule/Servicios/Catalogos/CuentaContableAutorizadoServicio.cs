using Application.MainModule.AccesoADatos;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.AccesoADatos;
using Exceptions.MainModule.Validaciones;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios
{
    public static class CuentaContableAutorizadoServicio
    {
        public static RespuestaDto RegistrarCuentaContableAutorizado(CuentaContableAutorizado provee)
        {
            return new CuentaContableAutorizadoDataAcces().Insertar(provee);
        }
        public static CuentaContableAutorizado RegistrarCuentaContableAutorizado(int idCuentaContable)
        {
            var autorizadas = new CuentaContableAutorizadoDataAcces().BuscarTodos(idCuentaContable);
            CuentaContableAutorizado ultima = new CuentaContableAutorizado();
            if (autorizadas.Count.Equals(0))
            {
                ultima.IdCuentaContable = idCuentaContable;
                ultima.Autorizado = 0;
                ultima.Fecha = DateTime.Parse(DateTime.Now.ToShortDateString());                
            }
            else            
                ultima = autorizadas.OrderByDescending(x => x.Fecha).FirstOrDefault();
            

            CuentaContableAutorizado entidad = new CuentaContableAutorizado();
            entidad.IdCuentaContable = idCuentaContable;
            entidad.Autorizado = ultima.Autorizado;
            entidad.Fecha = DateTime.Parse(DateTime.Now.ToShortDateString());

            var respuesta = new CuentaContableAutorizadoDataAcces().Insertar(entidad);
            if (respuesta.Exito)
            {
                entidad.IdCuentaContableAutorizado = respuesta.Id;
            }

            return entidad;
        }
        public static RespuestaDto ModificarCuentaContableAutorizado(CuentaContableAutorizado provee)
        {
            return new CuentaContableAutorizadoDataAcces().Actualizar(provee);
        }
        public static List<CuentaContableAutorizado> Obtener()
        {
            var empresa = new EmpresaDataAccess().Buscar(Seguridad.TokenServicio.ObtenerIdEmpresa());

            if (empresa.EsAdministracionCentral)
                return new CuentaContableAutorizadoDataAcces().BuscarTodos();
            else
                return new CuentaContableAutorizadoDataAcces().BuscarTodos(empresa.IdEmpresa);
        }
        public static CuentaContableAutorizado Obtener(int id)
        {
            return new CuentaContableAutorizadoDataAcces().Buscar(id);
        }
        public static CuentaContableAutorizado Obtener(int id, DateTime fecha)
        {
            return new CuentaContableAutorizadoDataAcces().Buscar(id, fecha);
        }
        public static RespuestaDto NoExiste()
        {
            string mensaje = string.Format(Error.NoExiste, "La Cuenta Contable");

            return new RespuestaDto()
            {
                ModeloValido = true,
                Mensaje = mensaje,
                MensajesError = new List<string>() { mensaje },
            };
        }
    }
}
