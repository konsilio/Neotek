using Application.MainModule.UnitOfWork;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.AccesoADatos
{
    public class EstacionCarburacionDataAccess
    {
        private SagasDataUow uow;

        public EstacionCarburacionDataAccess()
        {
            uow = new SagasDataUow();
        }
        
        public EstacionCarburacion Buscar(int idEstacion)
        {
            return uow.Repository<EstacionCarburacion>().GetSingle(x => x.IdEstacionCarburacion.Equals(idEstacion)
                                                            && x.Activo);
        }

        public EstacionCarburacion BuscarEstacionCarburacion(UnidadAlmacenGas uAG)
        {
            if (uAG.EstacionCarburacion != null)
                return uAG.EstacionCarburacion;
            else
            {
                if (uAG.IdPipa != null && uAG.IdPipa > 0)
                    return Buscar(uAG.IdEstacionCarburacion.Value);
                else
                    return null;
            }
        }

    }
}
