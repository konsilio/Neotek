using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagas.MainModule
{
    public partial class Usuario
    {
        public Usuario()
        {        
            this.Roles = new HashSet<Rol>();         
        }

        public int IdUsuario { get; set; }
        public int IdRol { get; set; }

        public virtual ICollection<Rol> Roles { get; set; }
     
    }
}
