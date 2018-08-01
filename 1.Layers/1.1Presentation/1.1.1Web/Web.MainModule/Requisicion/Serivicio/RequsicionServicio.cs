using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.MainModule.Requisicion.Serivicio
{
    public class RequsicionServicio
    {
        public List<Model.RequisicionProductoDTO> GenerarLista(List<Model.RequisicionProductoDTO> lP, Model.RequisicionProductoDTO p)
        {
            lP.Add(p);
            return lP;
        }
        public Model.RequisicionProductoDTO CrearProductoLocal(int _idProducto, int _idTipoProducto, int _idCentroCosto, decimal _cantidad, string _aplicacion)
        {
            return new Model.RequisicionProductoDTO
            {
                IdProducto = _idProducto,
                IdTipoProducto =_idTipoProducto,
                IdCentroCosto =_idCentroCosto,
                Cantidad = _cantidad,
                Aplicacion = _aplicacion
            };
        }
    }
}