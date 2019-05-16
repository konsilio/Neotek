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

        public List<RegimenFiscal> ListaRegimen()
        {
            return uow.Repository<RegimenFiscal>().GetAll().ToList();
        }
        public RegimenFiscal Regimen(short reg)
        {
            return uow.Repository<RegimenFiscal>().GetSingle(x=>x.IdRegimenFiscal.Equals(reg));
        }
    }
}
