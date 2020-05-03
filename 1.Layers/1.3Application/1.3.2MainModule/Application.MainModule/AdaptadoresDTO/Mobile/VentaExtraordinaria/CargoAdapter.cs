/**
 * CargoAdapter
 * Permite realizar al adaptación de los datos de la veta
 * para realizar el registro de una venta extraordinaria y 
 * afectar las tablas de:
 * - Cargos
 * - CClienteCondicion
 * @author Jorge Omar Tovar Martínez jorge.tovar@neoteck.com.mx
 * @company Neoteck
 * @date    20/12/2018 
 * @update  20/12/2018
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sagas.MainModule.Entidades;
using Application.MainModule.DTOs.Mobile;

namespace Application.MainModule.AdaptadoresDTO.Mobile.VentaExtraordinaria
{
    public class CargoAdapter
    {
        /// <summary>
        /// Permite adaptar los datos de un modelo DTO  de tipo VentaDTO 
        /// en una de tipo Cargo para las ventas Extraordinarias
        /// </summary>
        /// <param name="venta">Modelo de tipo VentaDTO con los datos la venta</param>
        /// <param name="fechaVencimiento">Fecha de vencimiento para el cargo</param>
        /// <param name="idEmpresa">Id de empresa a la cual se registra el cargo de la venta extraordinaria</param>
        /// <returns>Objeto de tipo Cargo con los datos de este con su venta extraordinaria</returns>
        public static Cargo FromDTO(VentaDTO venta,DateTime fechaVencimiento, short idEmpresa)
        {
            return new Cargo()
            {
                IdCliente = venta.IdCliente,
                IdEmpresa = idEmpresa,
                Ticket = venta.FolioVenta,
                FechaRegistro = DateTime.Now,
                TotalCargo = venta.Total,
                TotalAbonos = 0,
                VentaExtraordinaria = true,
                Activo = true,
                FechaVencimiento = fechaVencimiento
            };
        }
        public static Cargo FromDTO(VentaPuntoDeVenta venta, DateTime fechaVencimiento, short idEmpresa)
        {
            return new Cargo()
            {
                IdCliente = venta.IdCliente,
                IdEmpresa = idEmpresa,
                Ticket = venta.FolioVenta,
                FechaRegistro = DateTime.Now,
                TotalCargo = venta.Total,
                TotalAbonos = 0,
                VentaExtraordinaria = true,
                Activo = true,
                FechaVencimiento = fechaVencimiento
            };
        }
    }
}
