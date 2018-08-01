using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

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
        public Model.RequisicionProductoGridDTO GenerarProductoGrid( DropDownList _tipoProducto ,DropDownList _producto, DropDownList _centroCosto, string _aplicacion, decimal _cantidad)
        {
            return new Model.RequisicionProductoGridDTO
            {
                IdTipoProducto = int.Parse(_tipoProducto.SelectedItem.Value),
                TipoProducto = _tipoProducto.SelectedItem.Text,
                IdProducto = int.Parse(_producto.SelectedItem.Value),
                Producto = _producto.SelectedItem.Text,
                IdCentroCosto = int.Parse(_centroCosto.SelectedItem.Value),
                CentroCosto = _centroCosto.SelectedItem.Text,
                Cantidad = _cantidad,
                Aplicacion =_aplicacion,
                IdUnidad = 1, //Falta metodo para buscar la unidad con el ID del prodcuto
                Unidad = "Unidad" //Falta metodo para buscar la unidad con el ID del prodcuto
            };
        }
        public List<Model.RequisicionProductoGridDTO> GenerarListaGrid(List<Model.RequisicionProductoGridDTO> LProductos, Model.RequisicionProductoGridDTO Producto)
        {
            LProductos.Add(Producto);
            return LProductos;
        }

        public bool GuardarRequisicion(Model.RequisicionDTO Req, List<Model.RequisicionProductoDTO> lProdcutos)
        {
            return true;
        }        
    }
}