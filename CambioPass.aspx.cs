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

public partial class CambioPass : WebFormConSeguridad
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usuario"] == null)
            Response.Write("<script>alert('Debe estar logueado para acceder a esta sección.');window.location.href='index2.aspx';</script>");

    }

    protected void btnCambiar_Click(object sender, EventArgs e)
    {
        if (Validar())
        {
            try
            {
				Core.FacadeDao.EjecutarSQL("UPDATE Usuario SET Password='" + Usuario.Encriptar(txtPassNuevo.Text) + "' WHERE Nombre='" + ((Usuario)(Session["usuario"])).Nombre + "'");
                MagicAjax.AjaxCallHelper.Write("alert('Contraseña cambiada con éxito');window.location.href='Index2.aspx';");
            }
			catch
			{
				//Logger.EscribirEventLog(ex);
				string script = "alert('Ocurrió un error al cambiar su contraseña, contacte al administrador');";
				ClientScript.RegisterStartupScript(this.GetType(), DateTime.Now.ToFileTime().ToString(), script);

            }
        }
    }

    private bool Validar()
    {
        if (txtPassNuevo.Text != txtPassNuevoRepeat.Text)
        {
			string script = "alert('El password nuevo es diferente al password nuevo de confirmación');";
			ClientScript.RegisterStartupScript(this.GetType(), DateTime.Now.ToFileTime().ToString(), script);
			return false;
        }
        if (txtPassNuevo.Text.Trim().Length <5)
        {
			string script = "alert('El password nuevo debe contener como mínimo 5 caracteres');";
			ClientScript.RegisterStartupScript(this.GetType(), DateTime.Now.ToFileTime().ToString(), script);
			return false;
        }
        if (!CaptchaControl1.IsValid)
        {
            string script = "alert('" + CaptchaControl1.ErrorMessage + "');";
			ClientScript.RegisterStartupScript(this.GetType(), DateTime.Now.ToFileTime().ToString(), script);
			return false;
        }
		if (Session["Usuario"]!=null && txtPassViejo.Text.Trim().ToLower() != Usuario.Desencriptar(Core.FacadeDao.DevolverUnValor("SELECT Password FROM Usuario WHERE nombre='" + ((Usuario)(Session["Usuario"])).Nombre + "'").ToString()).Trim().ToLower())
		{
			string script = "alert('El password antiguo es incorrecto, ingrese el password con el que ingreso al sistema');";
			ClientScript.RegisterStartupScript(this.GetType(), DateTime.Now.ToFileTime().ToString(), script);
			return false;
		}
		return true;

    }
}
