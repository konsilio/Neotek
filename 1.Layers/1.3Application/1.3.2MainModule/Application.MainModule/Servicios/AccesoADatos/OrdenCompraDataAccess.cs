using Application.MainModule.UnitOfWork;

using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.AccesoADatos
{
    public class OrdenCompraDataAccess
    {
        private SagasDataUow uow;

        public OrdenCompraDataAccess()
        {
            uow = new SagasDataUow();
        }
        
        public List<OrdenCompra> Buscar(short idEmpresa, byte idOrdenComprEstatus)
        {
            var productos = uow.Repository<OrdenCompraProducto>().Get(x => x.EsGas && x.EsActivoVenta);


            //productos = uow.Repository<OrdenCompraProducto>().Get(x => x.EsGas && x.EsActivoVenta 
            //                                                        && x.OrdenCompra.Activo
            //                                                        && x.OrdenCompra.IdEmpresa.Equals(idEmpresa)
            //                                                        && x.OrdenCompra.OrdenCompraEstatus.Equals(idOrdenComprEstatus));

            List<OrdenCompra> ordencompra = new List<OrdenCompra>();
            foreach (OrdenCompraProducto o in productos)
            {
                ordencompra.Add(o.OrdenCompra);
            }
            return ordencompra;
        }

        public List<OrdenCompra> BuscarTodos()
        {
            return uow.Repository<OrdenCompra>().GetAll().ToList();
        }
    }
}
