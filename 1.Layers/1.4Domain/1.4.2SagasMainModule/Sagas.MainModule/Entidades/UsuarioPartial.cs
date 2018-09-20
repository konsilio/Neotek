using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagas.MainModule.Entidades
{
    public partial class Usuario
    {
        // Colocar en el constructor de la entidad autogenerada HASTA EL FINAL
        // using System.Linq;
        //this.Roles = this.UsuarioRoles.Select(x => x.Roles).ToList();
        public virtual ICollection<Rol> Roles { get; set; }
    }
}
