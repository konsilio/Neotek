using Application.MainModule.UnitOfWork;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.AccesoADatos
{
   public class EstadoRDataAccess
    {
        private SagasDataUow uow;

        public EstadoRDataAccess()
        {
            uow = new SagasDataUow();
        }

        public List<EstadosRepublica> ListaEstadosR()
        {
            return uow.Repository<EstadosRepublica>().GetAll().ToList();
        }

        public EstadosRepublica BuscarIdEdo(int idEdo)
        {
            return uow.Repository<EstadosRepublica>().GetSingle(x => x.IdEstadoRep.Equals(idEdo));
        }
    }
}
