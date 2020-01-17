using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.MainModule
{
    public class AuthRepository : IDisposable
    {
        DirectorioINFOTEC dir;

        public AuthRepository()
        {
            dir = new DirectorioINFOTEC();
        }

        public async Task<UsuarioAD> FindUser(string userName, string password)
        {
            UsuarioAD datosUsuario = new UsuarioAD();
            string ldap = "LDAP://192.168.97.22:389/DC=INFOTEC,DC=mx"; // Hardcoded for testing
            datosUsuario = dir.GetDataUser(ldap, userName, password);
            return datosUsuario;
        }

        public void Dispose()
        {

        }
    }
}
