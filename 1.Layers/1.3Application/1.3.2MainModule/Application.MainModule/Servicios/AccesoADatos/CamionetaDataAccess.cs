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

namespace Application.MainModule.Servicios.AccesoADatos
{
    public class CamionetaDataAccess
    {
        private SagasDataUow uow;

        public CamionetaDataAccess()
        {
            uow = new SagasDataUow();
        }
        public RespuestaDto Insertar(Camioneta _cc)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Camioneta>().Insert(_cc);
                    uow.SaveChanges();
                    _respuesta.Id = _cc.IdCamioneta;
                    _respuesta.EsInsercion = true;
                    _respuesta.Exito = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0002, "del centro de costo");
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public RespuestaDto Actualizar(Camioneta _pro)
        {
            RespuestaDto _respuesta = new RespuestaDto();
            using (uow)
            {
                try
                {
                    uow.Repository<Camioneta>().Update(_pro);
                    uow.SaveChanges();
                    _respuesta.Id = _pro.IdCamioneta;
                    _respuesta.Exito = true;
                    _respuesta.EsActulizacion = true;
                    _respuesta.ModeloValido = true;
                    _respuesta.Mensaje = Exito.OK;
                }
                catch (Exception ex)
                {
                    _respuesta.Exito = false;
                    _respuesta.Mensaje = string.Format(Error.C0003, "del punto de venta"); ;
                    _respuesta.MensajesError = CatchInnerException.Obtener(ex);
                }
            }
            return _respuesta;
        }
        public Camioneta ObtenerCamioneta(int IdP)
        {
            return uow.Repository<Camioneta>().GetSingle(
                x => x.IdCamioneta.Equals(IdP) && x.Activo
                );
        }
        public List<Camioneta> ObtenerCamionetas(short idEmpresa)
        {
            return uow.Repository<Camioneta>().Get(x => x.IdEmpresa.Equals(idEmpresa) && x.Activo).ToList();
        }
        public List<Camioneta> ObtenerCamionetas()
        {
            return uow.Repository<Camioneta>().Get(x => x.Activo).ToList();
        }
    }
}