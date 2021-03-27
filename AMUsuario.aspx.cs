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

public partial class AMUsuario : WebFormConSeguridad
{
	Usuario usuario = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
		{
			CargarUsuario();
		}
	}

	private void CargarUsuario()
	{
		if (Request.QueryString["IdUsuario"] != null)
		{
			int idUsuario = Convert.ToInt32(Request.QueryString["IdUsuario"]);
			usuario = FacadeDao.GetUsuario(idUsuario);
			if (usuario != null)
			{
				txtNombre.Text = usuario.Nombre;
				cboRol.SelectedIndex = cboRol.Items.IndexOf(cboRol.Items.FindByValue(usuario.Rol.ToString()));
				txtEmail.Text = usuario.Email;
				txtNombre.Enabled = false;
				cboRol.Enabled = false;
				btnDarDeAlta.Visible = false;
				CaptchaControl1.Visible = false;
			}
		}
		else {
			txtNombre.Enabled = true;
			cboRol.Enabled = true;
			btnDarDeAlta.Visible = true;
			CaptchaControl1.Visible = true;
		}
	}

    protected void btnDarDeAlta_Click(object sender, EventArgs e)
    {
		Page.Validate();
		if (Page.IsValid)
		{
			try
			{
				if (CaptchaControl1.IsValid)
				{
					Usuario usu = new Usuario();
					int idProveedor;
					string redirect = "MisUsuarios.aspx";
					if (Request.QueryString["Back"] == null)
					{
						idProveedor = ((Proveedor)(Session["Proveedor"])).Id;
					}
					else
					{
						redirect = Request.QueryString["Back"] + ".aspx?IdProveedor=" + Request.QueryString["IdProveedor"];
						idProveedor = Convert.ToInt32(Request.QueryString["IdProveedor"]);
					}
					string pass = txtNombre.Text.Trim().ToLower() + DateTime.Now.GetHashCode().ToString().Replace("-", "").Trim();
					usu.Nombre = txtNombre.Text.Trim();
					usu.Password = pass;
					usu.Email = txtEmail.Text.Trim();
					usu.Rol = Convert.ToInt32(cboRol.SelectedValue);
					usu.IdProveedor = idProveedor;
					int result = FacadeDao.CrearUsuario(usu);
					if (result == 1)
					{
						FacadeDao.EnviarMail(usu.Email, "Alta de usuario en TravelPay", "Datos de ingreso a TravelPay web:<br><br>Usuario: " + usu.Nombre + "<br>Password: " + usu.Password, Session["Logo"], true);
						if (Request.QueryString["Back"] == null)
							Response.Redirect(redirect);
						else
							Response.Redirect(redirect);
					}
					else if (result == -1 || result == -4)
					{
						throw (new Exception("Email existente"));
					}
					else if (result == -2 || result == -3)
					{
						throw (new Exception("Cuit ya registrado"));
					}
					else
					{
						throw (new Exception("Ocurrió un error en el alta de usuarios"));
					}
				}
				else
					throw (new Exception(CaptchaControl1.ErrorMessage));
			}
			catch (Exception ex)
			{
				string script = "<script>alert('" + ex.Message + "');</script>";
				ClientScript.RegisterStartupScript(this.GetType(), DateTime.Now.ToFileTime().ToString(), script);
				//Logger.EscribirEventLog(ex);
			}
		}
    }
}
