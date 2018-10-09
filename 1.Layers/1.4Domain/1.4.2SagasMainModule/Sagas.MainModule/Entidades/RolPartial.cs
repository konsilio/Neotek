using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagas.MainModule.Entidades
{
   public partial class Rol
    {
        // Colocar en el constructor de la entidad autogenerada HASTA EL FINAL
        // using System.Linq;
        //this.Usuario = this.UsuariosRoles.Select(x => x.Usuario).ToList();
        public bool Tesoreria { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
