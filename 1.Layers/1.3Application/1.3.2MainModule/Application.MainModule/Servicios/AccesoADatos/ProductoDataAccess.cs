using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.UnitOfWork;
using Sagas.MainModule.Entidades;

namespace Application.MainModule.Servicios.AccesoADatos
{
    public class ProductoDataAccess
    {
        private SagasDataUow uow;

        public ProductoDataAccess()
        {
            uow = new SagasDataUow();
        }

        public List<Producto> ListaProductos(short idEmpresa)
        {
            return uow.Repository<Producto>().Get(x => x.IdEmpresa.Equals(idEmpresa)).ToList();
        }
    }
}
