using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sagas.MainModule.Entidades;
using Application.MainModule.UnitOfWork;
using Application.MainModule.AdaptadoresDTO.Catalogo;

namespace Application.MainModule.Servicios.AccesoADatos
{
    public class EmpresaDataAccess
    {
        private SagasDataUow uow;

        public EmpresaDataAccess()
        {
            uow = new SagasDataUow();
        }       

        public Empresa Buscar(short idEmpresa)
        {
            return uow.Repository<Empresa>().GetSingle(x => x.IdEmpresa.Equals(idEmpresa));
        }

        public List<Empresa> BuscarTodos()
        {
            return uow.Repository<Empresa>().GetAll().ToList();
        }
    }
}
