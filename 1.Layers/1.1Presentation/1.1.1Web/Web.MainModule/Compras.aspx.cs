using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Web.MainModule
{
    public partial class Compras : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["RespuestaAutenticacionDto"] != null)
                    if (((Seguridad.Model.RespuestaAutenticacionDto)Session["RespuestaAutenticacionDto"]).Exito)
                    {
                        //lblMensaje.Text = "Token validado correctamente";
                    }
                    else
                    {
                        Session["RespuestaAutenticacionDto"] = null;
                        Response.Redirect("~/Login.aspx");
                    }
            }
        }

        protected void btnCompra_Click(object sender, EventArgs e)
        {
            var respuesta = new Seguridad.Servicio.ComprasServicio().Compra(((Seguridad.Model.RespuestaAutenticacionDto)Session["RespuestaAutenticacionDto"]).token);
            lblMensaje.Text = respuesta.Mensaje;
        }
    }
}