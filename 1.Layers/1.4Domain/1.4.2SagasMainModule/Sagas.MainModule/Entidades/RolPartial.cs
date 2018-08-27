using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagas.MainModule.Entidades
{
    public partial class Rol
    {
        public bool CatInsertarCentroCosto { get; set; }
        public bool CatModificarCentroCosto { get; set; }
        public bool CatEliminarCentroCosto { get; set; }
        public bool CatConsultarCentroCosto { get; set; }
    }
}
