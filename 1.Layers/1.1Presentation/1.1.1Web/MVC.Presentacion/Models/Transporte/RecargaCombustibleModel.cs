using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Presentacion.Models
{
    public class RecargaCombustibleModel
    {
        public int Id_DetalleRecargaComb { get; set; }
        public int Id_Vehiculo { get; set; }
        public bool EsCamioneta { get; set; }
        public bool EsPipa { get; set; }
        public bool EsUtilitario { get; set; }
        public int KilometrajeActual { get; set; }
        public decimal KilometrajeRecorrido { get; set; }
        public decimal LitrosRecargados { get; set; }
        public int IdTipoCombustible { get; set; }
        public string Vehiculo { get; set; }
        public string Chofer { get; set; }
        public DateTime FechaRecarga { get; set; }
        public decimal Monto { get; set; }
        public int IdCuentaContable { get; set; }
    }
}
