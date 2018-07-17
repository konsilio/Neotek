using Application.MainModule.DTOs.Compras;
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
            return new RespuestaCompraDto()
            {
                Exito = true
            };
        }
    }
}
