using Application.MainModule.UnitOfWork;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.AccesoADatos
{
    public class PaisDataAccess
    {
        private SagasDataUow uow;

        public PaisDataAccess()
        {
            uow = new SagasDataUow();
        }

        public List<Pais> ListaPaises()
        {
            return uow.Repository<Pais>().GetAll().ToList();
        }
    }
}
