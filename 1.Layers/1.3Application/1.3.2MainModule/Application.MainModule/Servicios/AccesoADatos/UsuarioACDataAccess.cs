using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.UnitOfWork;
using Sagas.MainModule.Entidades;

namespace Application.MainModule.Servicios.AccesoADatos
{
    public class UsuarioACDataAccess
    {
        private SagasDataUow uow;

        public UsuarioAC Buscar(int idUsuario)
        {
            return uow.Repository<UsuarioAC>().GetSingle(x => x.IdAdministracionCentral.Equals(1)
                                                         && x.IdUsuarioAC.Equals(idUsuario)
                                                         && x.Activo);
        }

        public UsuarioAC Buscar(string NombreUsuario, string password)
        {
            return uow.Repository<UsuarioAC>().GetSingle(x => x.IdAdministracionCentral.Equals(1)
                                                         && x.NombreUsuario.Equals(NombreUsuario)
                                                         && x.Password.Equals(password)
                                                         && x.Activo);
        }
    }
}