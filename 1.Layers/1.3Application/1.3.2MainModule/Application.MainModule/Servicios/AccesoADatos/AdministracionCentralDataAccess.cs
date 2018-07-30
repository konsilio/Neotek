using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sagas.MainModule.Entidades;
using Application.MainModule.UnitOfWork;

namespace Application.MainModule.Servicios.AccesoADatos
{
    public class AdministracionCentralDataAccess
    {
        private SagasDataUow uow;

        public AdministracionCentralDataAccess()
        {
            uow = new SagasDataUow();
        }
        public AdministracionCentral Buscar(short id)
        {
            return uow.Repository<AdministracionCentral>().GetSingle(x => x.IdAdministracionCentral.Equals(id));
        }
        public AdministracionCentral Buscar()
        {
            return BuscarTodos().FirstOrDefault();
        }
        public List<AdministracionCentral> BuscarTodos()
        {
            return uow.Repository<AdministracionCentral>().GetAll().ToList();
        }
    }
}
