using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Security.MainModule.Token_Service;
using System.Security.Claims;

namespace Web.MainModule.Requisicion.Vista
{
    public partial class Requisicion : Page
    {
        string _tok = string.Empty;
        List<Model.RequisicionProductoDTO> LProductos;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["StringToken"] != null)
                {
                    _tok = Session["StringToken"].ToString();
                    Claim _autenticado = TokenGenerator.GetClaimsIdentityFromJwtSecurityToken(_tok, "Autenticado");
                    if (Convert.ToBoolean(_autenticado.Value))
                    {
                        dgListaproductos.DataBind();
                        //Habilitar opciones segun el rol
                    }
                    else
                        Salir();
                }
                else
                    Salir();
            }
            else
            {

            }
        }
        private void Salir()
        {
            Response.Redirect("../../Login.aspx");
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            //LProductos = new Serivicio.RequsicionServicio().GenerarLista(LProductos, new Serivicio.RequsicionServicio().CrearProductoLocal())
        }

        protected void BtnCrear_Click(object sender, EventArgs e)
        {

        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {

        }
    }
}