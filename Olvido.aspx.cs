using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Core;

public partial class Olvido : Page, IBasicPage
{
	public Boolean LoginVisible { get { return true; } }
	public Control ControlFocus { get { return txtUsuario; } }
	protected void Page_Load(object sender, EventArgs e)
	{

	}
    protected void btnRecuperar_click(object sender, EventArgs e)
    {
        Usuario usu = null;
        try{
			usu = FacadeDao.RecuperarPassword(txtUsuario.Text.Trim(), txtEmail.Text.Trim());
			if (usu != null)
			{
				if (Core.FacadeDao.EnviarMail(usu.Email, "Travel Pay - recuperación de contraseña", "Su usuario es: " + usu.Nombre + "<br>Su contraseña es: " + usu.Password, Session["Logo"], true))
				{
					string script = "alert('Se envió un mail a la casilla "+usu.Email+" donde recibirá los datos de ingreso al sistema');";
					ClientScript.RegisterStartupScript(this.GetType(), DateTime.Now.ToFileTime().ToString(), script);
					txtEmail.Text = "";
				}
				else
				{
					string script = "alert('Ocurrió un error al enviar el email, inténtelo más tarde');";
					ClientScript.RegisterStartupScript(this.GetType(), DateTime.Now.ToFileTime().ToString(), script);
				}
			}
			else
			{
				string script = "alert('El usuario o el email ingresados son inexistentes');";
				ClientScript.RegisterClientScriptBlock(this.GetType(), DateTime.Now.ToFileTime().ToString(), script, false);
			}
        }
        catch(Exception ex){
        	string script = "alert('"+ex.Message+"');";
			ClientScript.RegisterStartupScript(this.GetType(),"error3", script);
        }
    }
}
