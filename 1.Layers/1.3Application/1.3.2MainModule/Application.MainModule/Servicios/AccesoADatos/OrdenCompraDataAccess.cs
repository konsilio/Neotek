﻿using Application.MainModule.UnitOfWork;

using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.AccesoADatos
{
    public class OrdenCompraDataAccess
    {
        private SagasDataUow uow;

        public OrdenCompraDataAccess()
        {
            uow = new SagasDataUow();
        }
        
        public List<OrdenCompra> Buscar(short idEmpresa, byte idOrdenComprEstatus, bool EsGas, bool EsActivoVenta, bool EsTransporteGas)
        {
            return uow.Repository<OrdenCompra>().Get(x => x.Activo
                                                                  && x.EsGas.Equals(EsGas)
                                                                  && x.EsActivoVenta.Equals(EsActivoVenta)
                                                                  && x.EsTransporteGas.Equals(EsTransporteGas)
                                                                  && x.IdEmpresa.Equals(idEmpresa)
                                                                  && x.IdOrdenCompraEstatus.Equals(idOrdenComprEstatus))
                                                                  .ToList();

            //return uow.Repository<OrdenCompra>().GetAll().ToList();
        }

        public List<OrdenCompra> BuscarTodos()
        {
            return uow.Repository<OrdenCompra>().GetAll().ToList();
        }
    }
}