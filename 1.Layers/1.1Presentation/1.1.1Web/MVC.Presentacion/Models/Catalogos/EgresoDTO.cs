using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Presentacion.Models
{
    public class EgresoDTO
    {
        public int IdEgreso { get; set; }
        public short IdEmpresa { get; set; }
        public System.DateTime FechaRegistro { get; set; }
        public int IdCentroCosto { get; set; }
        public int IdCuentaContable { get; set; }
        public decimal Monto { get; set; }
        public short IdTipoEgreso { get; set; }
        public string Descripcion { get; set; }
        public bool EsExterno { get; set; }
        public bool Activo { get; set; }
        public bool GastoMensual { get; set; }
        public bool EsFiscal { get; set; }

    }
}