using Application.MainModule.UnitOfWork;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.AccesoADatos
{
   public class RegimenDataAccess
    {
        private SagasDataUow uow;

        public RegimenDataAccess()
        {
            uow = new SagasDataUow();
        }

        public List<RegimenFiscalDataAccess> ListaRegimen()
        {
            return uow.Repository<RegimenFiscalDataAccess>().GetAll().ToList();
        }
    }
}
