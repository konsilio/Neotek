using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Web.MainModule.Requisicion.Serivicio
{
    public class RequisicionServicio
    {
        public Model.RequisicionProductoDTO CrearPorductoRequicision(int idProducto, int idTipoProducto, int idCentroCosto, decimal cantidad, string aplicacion)
        {
            return new Model.RequisicionProductoDTO
            {
                IdProducto = idProducto,
                IdTipoProducto = idTipoProducto,
                IdCentroCosto = idCentroCosto,
                Cantidad = cantidad,
                Aplicacion = aplicacion
            };
        }
        public List<Model.RequisicionProductoDTO> AgregarPRaLista(Model.RequisicionProductoDTO Producto, List<Model.RequisicionProductoDTO> LProdcutos)
        {
            LProdcutos.Add(Producto);
            return LProdcutos;
        }
    }
}