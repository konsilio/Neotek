using System;
using System.DirectoryServices;

namespace Security.MainModule
{
    public class DirectorioINFOTEC
    {
        /*No es necesario descomentar, la configuración está en Web.config y WebApiConfig de los servicios distribuidos
        //Esta IP es para probar localmente
        //DirectoryEntry entry = new DirectoryEntry("LDAP:" + @"//192.168.97.22:389/DC=INFOTEC,DC=mx", "sigaif.infotec", "c9o!e7Q*S6");
        //Esta IP es para publicar
        //DirectoryEntry entry = new DirectoryEntry("LDAP:" + @"//207.249.28.247:389/DC=INFOTEC,DC=mx", "sigaif.infotec", "c9o!e7Q*S6");
        */
        DirectoryEntry entry;

        public UsuarioAD GetDataUser(string ldap, string User, string Password)
        {
            UsuarioAD us = new UsuarioAD();
            try
            {
                entry = new DirectoryEntry(ldap, User, Password);
                DirectorySearcher Dsearch = new DirectorySearcher(entry);
                Dsearch.Filter = "(&(objectClass=user)(sAMAccountname=" + User + "))";
                int i = 0;
                foreach (SearchResult sResultSet in Dsearch.FindAll())
                {
                    us.NombreCompleto = GetProperty(sResultSet, "cn");
                    us.Nombre = GetProperty(sResultSet, "givenName");
                    us.Apellido = GetProperty(sResultSet, "sn");
                    us.Usuario = GetProperty(sResultSet, "sAMAccountname");
                    us.Area = GetProperty(sResultSet, "department");
                    us.Correo = GetProperty(sResultSet, "mail");
                    us.Puesto = GetProperty(sResultSet, "title");
                    us.ResponsableInmedieato = GetProperty(sResultSet, "manager");

                    i++;
                }

                if (i > 0)
                {
                    us.Existe = true;
                }
                else
                {
                    us.Existe = false;
                    us.Mensaje = "No se encontró el usuario";
                }
            }
            catch(Exception es)
            {
                us.Existe = false;
                us.Mensaje = es.Message;
            }
            return us;
        }

        public bool UserValild(string Usuario)
        {
            try
            {
                DirectorySearcher Dsearch = new DirectorySearcher(entry);
                Dsearch.Filter = "(&(objectClass=user)(sAMAccountname=" + Usuario + "))";
                int Count = 0;

                foreach (SearchResult sResultSet in Dsearch.FindAll())
                {
                    Count++;
                }

                return Count > 0 ? true : false;
            }
            catch
            {
                return false;
            }
        }

        public UsuarioAD GetUser(string Usuario)
        {
            UsuarioAD usr = new UsuarioAD();
            try
            {
                DirectorySearcher Dsearch = new DirectorySearcher(entry);
                Dsearch.Filter = "(&(objectClass=user)(sAMAccountname=" + Usuario + "))";
                int Count = 0;

                foreach (SearchResult sResultSet in Dsearch.FindAll())
                {
                    usr.NombreCompleto = GetProperty(sResultSet, "cn");
                    usr.Nombre = GetProperty(sResultSet, "givenName");
                    usr.Apellido = GetProperty(sResultSet, "sn");
                    usr.Usuario = GetProperty(sResultSet, "sAMAccountname");
                    usr.Area = GetProperty(sResultSet, "department");
                    usr.Correo = GetProperty(sResultSet, "mail");
                    usr.Puesto = GetProperty(sResultSet, "title");
                    usr.ResponsableInmedieato = GetProperty(sResultSet, "manager");

                    Count++;
                }
                if (Count == 0)
                {
                    usr.NombreCompleto = "No se encontro el usuario " + Usuario;
                    usr.Nombre = "No se encontro ";
                    usr.Apellido = "el usuario"; 
                    usr.Area = "";
                    usr.Correo = "";
                    usr.Puesto = "";
                    usr.ResponsableInmedieato = "";
                }
                else
                {
                    usr.Existe = true;
                }
            }
            catch (Exception e)
            {
                usr.Existe = false;
                usr.NombreCompleto = e.Message;
            }
            return usr;
        }

        public string GetMail(string Usuario)
        {
            string Correo = "";
            try
            {
                DirectorySearcher Dsearch = new DirectorySearcher(entry);
                Dsearch.Filter = "(&(objectClass=user)(sAMAccountname=" + Usuario + "))";
                int Count = 0;

                foreach (SearchResult sResultSet in Dsearch.FindAll())
                {
                    Correo = GetProperty(sResultSet, "mail");

                    Count++;
                }
                if (Count == 0)
                {
                    throw new Exception("No se encontro el usuario ");
                }
            }
            catch (Exception e)
            {
                Correo = e.Message;
            }
            return Correo;
        }

        public string GetManager(string Usuario)
        {
            string Superior = "";
            try
            {
                DirectorySearcher Dsearch = new DirectorySearcher(entry);
                Dsearch.Filter = "(&(objectClass=user)(sAMAccountname=" + Usuario + "))";
                int Count = 0;

                foreach (SearchResult sResultSet in Dsearch.FindAll())
                {
                    Superior = GetProperty(sResultSet, "mail");
                    Count++;
                }
                if (Count == 0)
                {
                    throw new Exception("No se encontro el usuario");
                }
            }
            catch (Exception e)
            {
                Superior = e.Message;
            }
            return Superior;
        }

        public static string GetProperty(SearchResult searchResult, string PropertyName)
        {
            if (searchResult.Properties.Contains(PropertyName))
            {
                return searchResult.Properties[PropertyName][0].ToString();
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
