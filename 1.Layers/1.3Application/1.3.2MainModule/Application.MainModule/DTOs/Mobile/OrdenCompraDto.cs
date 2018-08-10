using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.DTOs.Mobile
{
    public class OrdenCompraDTO
    {
        public int IdOrdenCompra { get; set; }
        public string ProveedorNombreComercial { get; set; }
        public string ProveedorEstadoProvincia { get; set; }
        public string ProveedorMunicipio { get; set; }
        public string ProveedorCodigoPostal { get; set; }
        public string ProveedorColonia { get; set; }
        public string ProveedorCalle { get; set; }
        public string ProveedorNumExt { get; set; }
        public string ProveedorNumInt { get; set; }
        public string ProveedorRfc { get; set; }
        public string ProveedorTelefono1 { get; set; }
        public string ProveedorTelefono2 { get; set; }
        public string ProveedorTelefono3 { get; set; }
        public string ProveedorCelular1 { get; set; }
        public string ProveedorCelular2 { get; set; }
        public string ProveedorCelular3 { get; set; }
        public string NumOrdenCompra { get; set; }
        public DateTime FechaRequisicion { get; set; }
        public Nullable<decimal> SubtotalSinIva { get; set; }
        public Nullable<decimal> Iva { get; set; }
        public Nullable<decimal> Ieps { get; set; }
        public Nullable<decimal> Total { get; set; }
        public List<ProductoDTO> Productos { get; set; }
    }
}
