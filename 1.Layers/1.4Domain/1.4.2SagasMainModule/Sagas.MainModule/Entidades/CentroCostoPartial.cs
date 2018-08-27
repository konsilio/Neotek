using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagas.MainModule.Entidades
{
    public partial class CentroCosto
    {
        public Nullable<int> IdVehiculoUtilitario { get; set; }
        public Nullable<short> IdCAlmacenGas { get; set; }
        public Nullable<int> IdEstacionCarburacion { get; set; }
        public Nullable<int> IdCamioneta { get; set; }
        public Nullable<int> IdPipa { get; set; }
        public Nullable<int> IdCilindro { get; set; }//CAlmacenGasCilindro
    }
}
