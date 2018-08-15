using Application.MainModule.DTOs.Compras;
using Application.MainModule.Servicios.Compras;
using Application.MainModule.Servicios.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Flujos
{
    public class Compras
    {
        public RespuestaCompraDto ComprarGas()
        {
            UsuarioAplicacionServicio.Obtener();

            return new RespuestaCompraDto()
            {
                Exito = true
            };
        }
        public RequisicionOCDTO BuscarRequisicion(int idRequisicion)
        {
            return OrdenCompraServicio.BuscarRequisicion(idRequisicion);
        }
    }
}
