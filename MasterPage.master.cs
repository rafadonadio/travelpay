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

public partial class MasterPageTravel : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Expires = -1;
		if (((IBasicPage)Page).LoginVisible)
		{
			Login1.Visible = true;
			celdaLogin.Visible = true;
		}
		else
		{
			Login1.Visible = false;
			celdaLogin.Visible = false;
		}
		if (!IsPostBack)
        {
			if (Session["Logo"] == null)
				Session["Logo"] = Proveedor.logoDefault;
			imgLogo.Src = (string)Session["Logo"]; 
			lblFecha.Text = ponerFecha();
            lblTitulo.Text = this.Page.Title;
			
            if (Request.Url.AbsoluteUri.ToLower().IndexOf("index2.aspx") >= 0)
            {
                lblTitulo.Visible = false;
            }
            else
            {
                lblTitulo.Visible = true;
            }
			if (((IBasicPage)Page).ControlFocus != null)
			{
				((IBasicPage)Page).ControlFocus.Focus();
			}
			else { Login1.ControlFocus.Focus(); }
        }
    }
	public Controles_Login BloqueLogin
	{
		get { return Login1; }
	}

    private string ponerFecha()
    {
        string fecha = "";
        switch (DateTime.Now.Month)
        { 
            case 1:
                fecha = "Enero ";
                break;
            case 2:
                fecha = "Febrero ";
                break;
            case 3:
                fecha = "Marzo ";
                break;
            case 4:
                fecha = "Abril ";
                break;
            case 5:
                fecha = "Mayo ";
                break;
            case 6:
                fecha = "Junio ";
                break;
            case 7:
                fecha = "Julio ";
                break;
            case 8:
                fecha = "Agosto ";
                break;
            case 9:
                fecha = "Septiembre ";
                break;
            case 10:
                fecha = "Octubre ";
                break;
            case 11:
                fecha = "Noviembre ";
                break;
            case 12:
                fecha = "Diciembre ";
                break;
        }
        fecha += DateTime.Now.Day.ToString() + ", " + DateTime.Now.Year.ToString();
        return fecha;
    }

}
