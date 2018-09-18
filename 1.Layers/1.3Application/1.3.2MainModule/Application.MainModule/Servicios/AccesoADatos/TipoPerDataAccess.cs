using Application.MainModule.UnitOfWork;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.AccesoADatos
{
    public class TipoPerDataAccess
    {
        private SagasDataUow uow;

        public TipoPerDataAccess()
        {
            uow = new SagasDataUow();
        }

        public List<TipoPersona> ListaTiposPer()
        {
            return uow.Repository<TipoPersona>().GetAll().ToList();
        }
    }
}
