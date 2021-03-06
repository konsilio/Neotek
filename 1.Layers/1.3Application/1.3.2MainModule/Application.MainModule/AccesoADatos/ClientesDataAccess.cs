﻿using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.UnitOfWork;
using Exceptions.MainModule;
using Exceptions.MainModule.Validaciones;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.DTOs.Mobile;

namespace Application.MainModule.Servicios.AccesoADatos
{
    public class ClientesDataAccess
    {
        private SagasDataUow uow;

        public ClientesDataAccess()
        {
            uow = new SagasDataUow();
        }

        public List<Cliente> Buscar()
        {
            return uow.Repository<Cliente>().Get(x => x.Activo).ToList();
        }

        public Cliente Buscar(int idCliente)
        {
            return uow.Repository<Cliente>().GetSingle(x => x.IdCliente.Equals(idCliente));
        }

        public List<ClienteLocacion> BuscarLocacion(int idCliente)
        {
            return uow.Repository<ClienteLocacion>().Get(x => x.IdCliente.Equals(idCliente)).ToList();
        }

        public ClienteLocacion BuscarLocacionId(int idCliente, short idOrden)
        {
            return uow.Repository<ClienteLocacion>().GetSingle(x => x.IdCliente.Equals(idCliente) && x.Orden.Equals(idOrden));
        }

        public Cliente BuscarRazonSocial(ClienteDTO cliente, short idEmpresa)
        {
            var consulta = uow.Repository<Cliente>().GetSingle(
                x =>
                (x.RazonSocial != null && x.RazonSocial.Trim().ToLower().Equals(cliente.RazonSocial) && x.RazonSocial.Any()) &&
                x.IdTipoPersona.Equals(cliente.IdTipoPersona) &&
                x.IdRegimenFiscal.Equals(cliente.IdTipoRegimen) &&
                x.IdEmpresa.Equals(idEmpresa)
               );

            return (consulta != null) ? consulta : null;
        }
        public List<Cliente> BuscarRfcTel(DTOs.Catalogo.ClientesDto cliente, short idEmpresa)
        {
            List<Cliente> consulta = new List<Cliente>();
            if (cliente.Rfc != null)
            {
                consulta = uow.Repository<Cliente>().Get(
                                             x =>
                                             (x.Rfc.Trim().Equals(cliente.Rfc)) &&
                                             x.IdEmpresa.Equals(idEmpresa)
                                            ).ToList();
            }
            if (cliente.Telefono1 != null)
            {
                consulta = uow.Repository<Cliente>().Get(
                                     x =>
                                      (x.Telefono1.Trim().Equals(cliente.Telefono1)) &&
                                     x.IdEmpresa.Equals(idEmpresa)
                                    ).ToList();
            }

            return (consulta != null) ? consulta : null;
        }

        public Cliente Buscar(ClienteDTO cliente)
        {

            var respuesta = uow.Repository<Cliente>().GetSingle(
                x =>
                x.Nombre.Equals(cliente.Nombre) &&
                x.Apellido1.ToLower().Equals(cliente.Apellido1) &&
                x.Apellido2.ToLower().Equals(cliente.Apellido2) &&
                x.Telefono.Equals(cliente.TelefonoFijo) &&
                x.Celular.Equals(cliente.Celular)
                && !x.Telefono.Equals(null)
                && !x.Celular.Equals(null)
                && !x.Nombre.Equals(null)
                && !x.Apellido2.Equals(null)
                );
            return respuesta;
        }

        public List<Cliente> BuscadorClientes(string criterio, short idEmpresa)
        {
            return uow.Repository<Cliente>().Get(
                x => ((x.Telefono.Contains(criterio)
                || x.Telefono1.Contains(criterio)
                || x.Telefono2.Contains(criterio)
                || x.Telefono3.Contains(criterio)
                || x.Celular.Contains(criterio)
                || x.Celular1.Contains(criterio)
                || x.Celular2.Contains(criterio)
                || x.Celular3.Contains(criterio)
                || x.Rfc.ToLower().Contains(criterio)))
                && x.IdEmpresa.Equals(idEmpresa)
                && x.Activo
            ).ToList();
        }

        public RespuestaDto Insertar(Cliente cte)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Cliente>().Insert(cte);
                    uow.SaveChanges();
                    _respuesta.Id = cte.IdCliente;
                    _respuesta.EsInsercion = true;
                    _respuesta.Exito = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0002, "del Cliente");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }

        /// <summary>
        /// Realiza al actualización de los datos de credito del cliente,
        /// se envia como parametro los datos del cliente con el credito actualizado
        /// </summary>
        /// <param name="cliente">Entidad cliente con la actualización de datos</param>
        /// <returns>Modelo DTO con la respuesta de la actualización</returns>
        public RespuestaDto ActualizarCredito(Cliente cliente)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    var clienteCredito = uow.Repository<Cliente>().GetSingle(x => x.IdCliente.Equals(cliente.IdCliente));
                    clienteCredito.CreditoDisponibleMonto = cliente.CreditoDisponibleMonto;
                    uow.Repository<Cliente>().Update(clienteCredito);
                    uow.SaveChanges();
                    _respuesta.Id = cliente.IdCliente;
                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0003, " del credito del cliente");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }

        public RespuestaDto Insertar(ClienteLocacion cte)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<ClienteLocacion>().Insert(cte);
                    uow.SaveChanges();
                    _respuesta.Id = cte.IdCliente;
                    _respuesta.EsInsercion = true;
                    _respuesta.Exito = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0002, "del Cliente Locacion");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }

        public RespuestaDto Actualizar(ClienteLocacion _pro)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Sagas.MainModule.Entidades.ClienteLocacion>().Update(_pro);
                    uow.SaveChanges();
                    _respuesta.Id = _pro.IdCliente;
                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0003, "de la locacion del cliente"); ;
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public RespuestaDto Actualizar(Cliente _pro)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Sagas.MainModule.Entidades.Cliente>().Update(_pro);
                    uow.SaveChanges();
                    _respuesta.Id = _pro.IdCliente;
                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0003, "del cliente"); ;
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public RespuestaDto Eliminar(ClienteLocacion cteL)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<ClienteLocacion>().Delete(cteL);
                    uow.SaveChanges();
                    _respuesta.Exito = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.S0004, "Eliminar la locacion de cliente");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public Cliente Buscar(string rfc)
        {
            try
            {
                var consulta = uow.Repository<Cliente>().GetSingle(x => x.Rfc!=null && x.Rfc.Equals(rfc));
                
                return consulta;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
