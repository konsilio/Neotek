using Application.MainModule.UnitOfWork;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.AccesoADatos
{
   public class CajaGeneralDataAccess
    {
        private SagasDataUow uow;

        public CajaGeneralDataAccess()
        {
            uow = new SagasDataUow();
        }
        public List<VentaMovimiento> BuscarTodos()
        {
            return uow.Repository<VentaMovimiento>().Get().ToList();
        }
      
        public List<VentaMovimiento> Buscar(short idEmpresa)
        {
            return uow.Repository<VentaMovimiento>().Get(x => x.IdEmpresa.Equals(idEmpresa)
                                                         ).ToList();
        }

        public VentaCajaGeneral BuscarGralPorCve(string cve)
        {
            return uow.Repository<VentaCajaGeneral>().GetSingle(x => x.FolioOperacionDia.Equals(cve));
        }

        public List<VentaPuntoDeVenta> BuscarPorCve(string cve)
        {
            return uow.Repository<VentaPuntoDeVenta>().Get(x => x.FolioOperacionDia.Equals(cve)
                                                         ).ToList();
        }
    }
}
