using Application.MainModule.AdaptadoresDTO.Mobile;
using Application.MainModule.DTOs.Mobile;
using Application.MainModule.Servicios.AccesoADatos;
using Exceptions.MainModule.Validaciones;
using Sagas.MainModule.ObjetosValor.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Servicios.Mobile
{
   public static class MobileOrdenesCompraServicio
    {
        public static RespuestaOrdenesCompraDTO Consultar(short idEmpresa, bool EsGas, bool EsActivoVenta, bool EsTransporteGas)
        {
            var ordenCompraDataAccess = new OrdenCompraDataAccess();
            var ordenes = OrdenesCompraAdapter.ToDTO(ordenCompraDataAccess.BuscarOrdenDescargas(idEmpresa, OrdenCompraEstatusEnum.Proceso_compra,EsGas,EsActivoVenta,EsTransporteGas)).OrderByDescending(x=>x.IdOrdenCompra).ToList();
            if(ordenes != null)
                if(ordenes.Count > 0)
                    return new RespuestaOrdenesCompraDTO()
                    {
                        Exito = true,
                        Mensaje = "OK",
                        OrdenesCompra = ordenes,
                    };

            return new RespuestaOrdenesCompraDTO()
            {
                Exito = false,
                Mensaje = string.Format(Error.M0001, "ordenes de compra"),
                OrdenesCompra = ordenes,
            };
        }

        public static int Consultar(int IdOrdenCompra)
        {
            var ordenCompraDataAccess = new OrdenCompraDataAccess();
            var ordenes = ordenCompraDataAccess.Buscar(IdOrdenCompra);
            var OCalternativa = ordenes.Requisicion.OrdenesCompra.FirstOrDefault(x => !x.IdOrdenCompra.Equals(IdOrdenCompra));
            

            if (OCalternativa != null)
                return OCalternativa.IdOrdenCompra;

            return 0;               

           
        }

    }
}
