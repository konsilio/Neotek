using Application.MainModule.UnitOfWork;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.AccesoADatos
{
    public class RolDataAccess
    {
        private SagasDataUow uow;

        public RolDataAccess()
        {
            uow = new SagasDataUow();
        }

        public Rol Buscar(short idEmpresa, short idRol)
        {
            return uow.Repository<Rol>().GetSingle(x => x.IdEmpresa.Equals(idEmpresa)
                                                         && x.IdRol.Equals(idRol)
                                                         && x.Activo);
        }
        public List<Rol> Buscar(short idEmpresa)
        {
            return uow.Repository<Rol>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                         && x.Activo).ToList();
        }
    }
}
