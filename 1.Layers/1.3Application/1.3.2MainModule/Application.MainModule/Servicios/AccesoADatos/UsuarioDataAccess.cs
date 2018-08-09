using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.UnitOfWork;
using Sagas.MainModule.Entidades;

namespace Application.MainModule.Servicios.AccesoADatos
{
    public class UsuarioDataAccess
    {
        private SagasDataUow uow;

        public UsuarioDataAccess()
        {
            uow = new SagasDataUow();
        }

        public Usuario Buscar(short idEmpresa, int idUsuario)
        {
            return uow.Repository<Usuario>().GetSingle(x => x.IdEmpresa.Equals(idEmpresa)
                                                         && x.IdUsuario.Equals(idUsuario)
                                                         && x.Activo);
        }

        public Usuario Buscar(short idEmpresa, string NombreUsuario, string password)
        {
            return uow.Repository<Usuario>().GetSingle(x => x.IdEmpresa.Equals(idEmpresa)
                                                         && x.NombreUsuario.Equals(NombreUsuario)
                                                         && x.Password.Equals(password)
                                                         && x.Activo);
        }
        public Usuario Buscar(int idUsuario)
        {
            return uow.Repository<Usuario>().GetSingle(x => x.IdUsuario.Equals(idUsuario)
                                                         && x.Activo);
        }
    }
}
