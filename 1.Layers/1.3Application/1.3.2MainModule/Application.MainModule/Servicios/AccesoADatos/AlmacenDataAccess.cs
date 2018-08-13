using Application.MainModule.UnitOfWork;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.AccesoADatos
{
    public class AlmacenDataAccess
    {
        private SagasDataUow uow;

        public AlmacenDataAccess()
        {
            uow = new SagasDataUow();
        }

        public List<Sagas.MainModule.Entidades.Almacen> ListaProductosAlmacen()
        {
            return uow.Repository<Sagas.MainModule.Entidades.Almacen>().GetAll().ToList();
        }
        public Sagas.MainModule.Entidades.Almacen ProductoAlmacen( int idProducto)
        {
            return uow.Repository<Sagas.MainModule.Entidades.Almacen>().GetSingle(x => x.IdProduto.Equals(idProducto));
        }
    }
}
