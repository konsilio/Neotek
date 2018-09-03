using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.MainModule.OrdenCompra.Model
{
    public enum OrdenCompraEstatus
    {
        //Proceso_compra = 1,
        Espera_de_Autorización = 2,
        Proceso_de_Compra = 3,
        Compra_Exitosa = 4,
        Compra_Cancelada = 5
    }
}