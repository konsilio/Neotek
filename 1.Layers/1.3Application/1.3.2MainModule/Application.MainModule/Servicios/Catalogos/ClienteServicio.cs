using Application.MainModule.DTOs.Catalogo;
using Application.MainModule.Servicios.AccesoADatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.DTOs.Respuesta;
using Sagas.MainModule.Entidades;
using Exceptions.MainModule.Validaciones;
using Application.MainModule.DTOs.Mobile;
using Application.MainModule.Servicios.Seguridad;

namespace Application.MainModule.Servicios.Catalogos
{
    public static class ClienteServicio
    {
        public static List<ClientesDto> ListaClientes()
        {
            List<ClientesDto> lClientes = AdaptadoresDTO.Seguridad.ClientesAdapter.ToDTO(new ClientesDataAccess().Buscar());
            return lClientes;
        }
        public static RespuestaDto AltaCliente(Cliente cte)
        {
            return new ClientesDataAccess().Insertar(cte);
        }
        public static RespuestaDto AltaClienteL(ClienteLocacion cte)
        {
            return new ClientesDataAccess().Insertar(cte);
        }
        public static int BuscarRazon(ClienteDTO cliente)
        {
            var resultado = new ClientesDataAccess().BuscarRazonSocial(cliente, TokenServicio.ObtenerIdEmpresa());
            if (resultado != null)
                return resultado.IdCliente;
            else
                return 0;
        }
        public static List<ClientesDto> BuscarRfcyTel(ClientesDto cliente)
        {
            if (cliente.Telefono1.Equals("1"))
                cliente.Telefono1 = null;
            if (cliente.Rfc.Equals("1"))
                cliente.Rfc = null;
            List<ClientesDto> lClientes = AdaptadoresDTO.Seguridad.ClientesAdapter.ToDTO(new ClientesDataAccess().BuscarRfcTel(cliente, TokenServicio.ObtenerIdEmpresa()));
            return lClientes;

        }
        public static int BuscarCliente(ClienteDTO cliente)
        {
            var resultado = new ClientesDataAccess().Buscar(cliente);
            if (resultado != null)
                return cliente.IdCliente;
            else
                return 0;
        }
        public static Cliente Obtener(int IdCliente)
        {
            return new ClientesDataAccess().Buscar(IdCliente);
        }
        public static string ObtenerNomreCliente(int idCliente)
        {
            var us = Obtener(idCliente);
            string nom = "";
            string apell = "";
            string apell2 = "";
            if (us.RepresentanteLegal != null && !us.RepresentanteLegal.Equals(string.Empty))
            {
                if (us.RepresentanteLegal.Split(' ').Count() == 4)
                {
                    nom = us.RepresentanteLegal.Split(' ')[0] + " " + us.RepresentanteLegal.Split(' ')[1];
                    apell = us.RepresentanteLegal.Split(' ')[2];
                    apell2 = us.RepresentanteLegal.Split(' ')[3];
                }
                else if (us.RepresentanteLegal.Split(' ').Count() == 3)
                {
                    nom = us.RepresentanteLegal.Split(' ')[0];
                    apell = us.RepresentanteLegal.Split(' ')[1];
                    apell2 = us.RepresentanteLegal.Split(' ')[2];
                }
                else if (us.RepresentanteLegal.Split(' ').Count() == 2)
                {
                    nom = us.RepresentanteLegal.Split(' ')[0];
                    apell = us.RepresentanteLegal.Split(' ')[1];
                }
            }
            else
            {
                nom = us.Nombre;
                apell = us.Apellido1;
                apell2 = us.Apellido2;
            }
            return string.Concat(nom, " ", apell, " ", apell2);
        }
        public static string ObtenerNomreCliente(Cliente us)
        {            
            string nom = "";
            string apell = "";
            string apell2 = "";
            if (us.RepresentanteLegal != null && !us.RepresentanteLegal.Equals(string.Empty))
            {
                if (us.RepresentanteLegal.Split(' ').Count() == 4)
                {
                    nom = us.RepresentanteLegal.Split(' ')[0] + " " + us.RepresentanteLegal.Split(' ')[1];
                    apell = us.RepresentanteLegal.Split(' ')[2];
                    apell2 = us.RepresentanteLegal.Split(' ')[3];
                }
                else if (us.RepresentanteLegal.Split(' ').Count() == 3)
                {
                    nom = us.RepresentanteLegal.Split(' ')[0];
                    apell = us.RepresentanteLegal.Split(' ')[1];
                    apell2 = us.RepresentanteLegal.Split(' ')[2];
                }
                else if (us.RepresentanteLegal.Split(' ').Count() == 2)
                {
                    nom = us.RepresentanteLegal.Split(' ')[0];
                    apell = us.RepresentanteLegal.Split(' ')[1];
                }
            }
            else
            {
                nom = us.Nombre;
                apell = us.Apellido1;
                apell2 = us.Apellido2;
            }
            return string.Concat(nom, " ", apell, " ", apell2);
        }
        public static ClienteLocacion ObtenerCL(int IdCliente, short Orden)
        {
            return new ClientesDataAccess().BuscarLocacionId(IdCliente, Orden);
        }
        public static List<Cliente> BuscadorClientes(string criterio)
        {
            return new ClientesDataAccess().BuscadorClientes(criterio, TokenServicio.ObtenerIdEmpresa());
        }
        public static List<ClienteLocacionDTO> ObtenerLoc(int IdCliente)
        {
            List<ClienteLocacionDTO> lClientes = AdaptadoresDTO.Seguridad.ClientesAdapter.ToDTOLoc(new ClientesDataAccess().BuscarLocacion(IdCliente));
            return lClientes;
        }
        public static short ObtenerUltimoOrdenLocacion(int IdCliente)
        {

            List<ClienteLocacionDTO> lista = ObtenerLoc(IdCliente);
            if (lista.Count != 0)
                return lista.OrderByDescending(x => x.Orden).FirstOrDefault().Orden;
            else
                return (short)0;
        }
        public static RespuestaDto Eliminar(ClienteLocacion cteLoc)
        {
            return new ClientesDataAccess().Eliminar(cteLoc);
        }
        public static RespuestaDto ModificarCL(ClienteLocacion cte)
        {
            return new ClientesDataAccess().Actualizar(cte);
        }
        public static RespuestaDto Modificar(Cliente cte)
        {
            return new ClientesDataAccess().Actualizar(cte);
        }
        public static RespuestaDto NoExiste()
        {
            string mensaje = string.Format(Error.NoExiste, "El cliente");

            return new RespuestaDto()
            {
                ModeloValido = true,
                Mensaje = mensaje,
                MensajesError = new List<string>() { mensaje },
            };
        }
        public static RespuestaDto YaExiste()
        {
            string mensaje = string.Format(Error.SiExiste, "El RFC");

            return new RespuestaDto()
            {
                ModeloValido = true,
                Mensaje = mensaje,
                MensajesError = new List<string>() { mensaje },
            };
        }
        public static Cliente BuscarClientePorRFC(string rfc)
        {
            return new ClientesDataAccess().Buscar(rfc);
        }
        public static RespuestaDto ModificarCredito(Cliente cliente)
        {
            return new ClientesDataAccess().ActualizarCredito(cliente);
        }
        public static Cliente ObtenerPublicoEnGeneral()
        {
            return BuscarClientePorRFC("XAXX010101000");
        }
    }
}
