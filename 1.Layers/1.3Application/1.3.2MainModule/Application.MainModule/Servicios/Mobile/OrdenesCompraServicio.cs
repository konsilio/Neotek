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
   public static class OrdenesCompraServicio
    {
        public static RespuestaOrdenesCompraDTO Consultar(short idEmpresa, bool EsGas, bool EsActivoVenta, bool EsTransporteGas)
        {
            var ordenCompraDataAccess = new OrdenCompraDataAccess();
            var ordenes = OrdenesCompraAdapter.ToDTO(ordenCompraDataAccess.Buscar(idEmpresa, OrdenCompraEstatusEnum.Proceso_compra,EsGas,EsActivoVenta,EsTransporteGas));
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

    }
}
