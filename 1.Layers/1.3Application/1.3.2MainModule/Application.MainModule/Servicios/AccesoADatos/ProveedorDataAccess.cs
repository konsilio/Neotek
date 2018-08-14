using Application.MainModule.UnitOfWork;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.AccesoADatos
{
    public class ProveedorDataAccess
    {
        private SagasDataUow uow;

        public ProveedorDataAccess()
        {
            uow = new SagasDataUow();
        }



        public List<Proveedor> BuscarTodos()
        {
            return uow.Repository<Proveedor>().Get(x => x.Activo).ToList();
        }

        public List<Proveedor> BuscarTodos(short idEmpresa)
        {
            return uow.Repository<Proveedor>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                         && x.Activo)
                                                         .ToList();
        }

        public Proveedor Buscar(int idProveedor)
        {
            return uow.Repository<Proveedor>().GetSingle(x => x.IdProveedor.Equals(idProveedor)
                                                         && x.Activo);
        }
    }
}
