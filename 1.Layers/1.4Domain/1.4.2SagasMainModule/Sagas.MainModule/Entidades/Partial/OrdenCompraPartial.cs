using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagas.MainModule.Entidades.Partial
{
    public partial class OrdenCompra
    {
        public int IdUsuarioGenerador { get; set; }
        public int IdUsuarioAutorizador { get; set; }
        public DateTime FechaAutorizacion  { get; set; }
    }
}
