using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using Core;

public partial class MisUsuarios : WebFormConSeguridad
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			BindLista();
		}
	}

	private void BindLista()
	{
		StringBuilder sql = new StringBuilder();
		try
		{
			List<Usuario> usuarios = new List<Usuario>();
			int? idProveedor = null;
			if (Session["Proveedor"] != null)
			{

				idProveedor = ((Proveedor)(Session["Proveedor"])).Id;
			}
			usuarios = FacadeDao.GetUsuarios(idProveedor);
			grilla.DataSource = usuarios;
			grilla.DataBind();
			grilla.Visible = true;
		}
		catch
		{
			//Logger.EscribirEventLog(ex);
		}
	}
	public void grilla_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		int idUsuActual = ((Usuario)(Session["Usuario"])).Id.Value;
		ImageButton ibElim;
		string nombre = "", script = "";
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			if (e.Row.DataItem != null)
			{
				ibElim = (ImageButton)e.Row.FindControl("imgBorrar");

				if (((Usuario)e.Row.DataItem).RolName.ToLower() == "administrador" || ((Usuario)e.Row.DataItem).Id.Value == idUsuActual || ((Usuario)e.Row.DataItem).CantidadIventures>0)
				{
					ibElim.Visible = false;
				}
				else
				{
					ibElim.Visible = true;
					if (grilla.EditIndex < 0)
					{
						nombre = ((Usuario)e.Row.DataItem).Nombre;
						script = "return eliminarUsuario('" + nombre + "' );";
					}
					ibElim.Attributes.Add("onclick", script);
				}
			}
		}
	}
	protected void BorrarUsuario(object sender, EventArgs e)
	{
		string idUsuario = ((ImageButton)sender).CommandArgument;
		FacadeDao.BorrarUsuario(Convert.ToInt32(idUsuario));
		BindLista();
	}
	protected void BtnReenviar(object sender, EventArgs e)
	{
		string nombreUsuario = ((ImageButton)sender).CommandArgument;
		Usuario usu = null;
		try
		{
			usu = FacadeDao.RecuperarPassword(nombreUsuario.Trim(), null);
			if (usu != null)
			{
				if (Core.FacadeDao.EnviarMail(usu.Email, "Travel Pay - recuperación de contraseña", "Su usuario es: " + usu.Nombre + "<br>Su contraseña es: " + usu.Password, Session["Logo"],true))
				{
					string script = "alert('Se envió un mail a la casilla " + usu.Email + " donde recibirá los datos de ingreso al sistema');";
					ClientScript.RegisterStartupScript(this.GetType(), DateTime.Now.ToFileTime().ToString(), script);
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
		catch (Exception ex)
		{
			string script = "alert('" + ex.Message + "');";
			ClientScript.RegisterStartupScript(this.GetType(), "error3", script);
		}

	}
	protected void btnCrearUsuario_Click(object sender, EventArgs e)
	{
		Response.Redirect("AMUsuario.aspx");
	}
	protected void grilla_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		grilla.PageIndex = e.NewPageIndex;
		BindLista();
	}

}
