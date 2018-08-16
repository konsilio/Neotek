using Application.MainModule.UnitOfWork;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.AccesoADatos
{
    public class CentroCostoDataAccess
    {
        private SagasDataUow uow;

        public CentroCostoDataAccess()
        {
            uow = new SagasDataUow();
        }
        public List<CentroCosto> BuscarCentroCosto()
        {
            return uow.Repository<CentroCosto>().GetAll().ToList();
        }
    }
}
