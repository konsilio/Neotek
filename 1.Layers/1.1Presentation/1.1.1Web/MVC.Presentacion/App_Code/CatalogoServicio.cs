using MVC.Presentacion.Agente;
using MVC.Presentacion.Models.Catalogos;
using MVC.Presentacion.Models.Seguridad;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace MVC.Presentacion.App_Code
{
    public class CatalogoServicio
    {
      
        #region Empresas
        //public bool create(EmpresaDTO Objemp)
        //{
        //    try
        //    {
        //        //DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(EmpresaDTO));
        //        //MemoryStream mem = new MemoryStream();
        //        //ser.WriteObject(mem, Objemp);
        //        //string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
        //        //WebClient webClient = new WebClient();
        //        //webClient.Headers["Content-type"] = "application/json";
        //        //webClient.Encoding = Encoding.UTF8;
        //        //webClient.UploadString(_URL + "create", "POST", data);

        //        //return true;
        //        var agente = new AgenteServicio();
        //        agente.GuardarEmpresaNueva(Objemp);

        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        public RespuestaDTO create(EmpresaModel cc, string tkn)
        {            
            var agente = new AgenteServicio();
            agente.GuardarEmpresaNueva(cc, tkn);
            return agente._respuestaDTO;
        }

        public List<PaisModel> GetPaises(string tkn)
        {
            var agente = new AgenteServicio();
            agente.BuscarPaises(tkn);
            return agente._listaPaises;
        }
        

        #endregion
    }
}