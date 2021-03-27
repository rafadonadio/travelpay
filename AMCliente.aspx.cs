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

public partial class AMCliente : WebFormConSeguridad
{
	Cliente cliente = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
		{
			CargarCliente();
		}
	}

	private void CargarCliente()
	{
		if (Request.QueryString["IdCliente"] != null)
		{
			int idCliente = Convert.ToInt32(Request.QueryString["IdCliente"]);
			cliente = FacadeDao.GetCliente(idCliente);
			if (cliente != null)
			{
				txtIdCliente.Value = cliente.Id.ToString();
				txtNombre.Text = cliente.Nombre;
				txtApellido.Text = cliente.Apellido;
				txtDNI.Text = cliente.CuitDni;
				txtDomicilio.Text = cliente.Domicilio;
				txtEmail.Text = cliente.Email;
				txtTelefonos.Text = cliente.Telefonos;
			}
		}
	}

    protected void btnAceptar_Click(object sender, EventArgs e)
    {
		Page.Validate();
		if (Page.IsValid)
		{
			try
			{
				if (CaptchaControl1.IsValid)
				{
					string pass = txtNombre.Text.Trim().ToLower() + DateTime.Now.GetHashCode().ToString().Replace("-", "").Trim();
					Cliente cli = new Cliente();
					cli.Nombre = txtNombre.Text.Trim();
					cli.Apellido = txtApellido.Text.Trim();
					cli.CuitDni = txtDNI.Text.Trim();
					cli.Proveedor = new Proveedor();
					cli.Proveedor.Id = ((Proveedor)(Session["Proveedor"])).Id;
					cli.Domicilio = txtDomicilio.Text.Trim();
					cli.Email = txtEmail.Text.Trim();
					cli.Telefonos = txtTelefonos.Text.Trim();
					Usuario usu = new Usuario();
					usu.Nombre = cli.CuitDni;
					usu.Password = pass;
					usu.Email = cli.Email;
					if (!String.IsNullOrEmpty(txtIdCliente.Value))
					{
						cli.Id = Convert.ToInt32(txtIdCliente.Value);
						int result = FacadeDao.ActualizarCliente(cli);
						if (result == 1)
						{
							Response.Redirect("MisClientes.aspx");
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
							throw (new Exception("Ocurrió un error en la actualización de clientes"));
						}
					}
					else
					{
						int result = FacadeDao.CrearCliente(cli, usu);
						if (result == 1)
						{
							FacadeDao.EnviarMail(cli.Email, "Alta de usuario en TravelPay", "Datos de ingreso a TravelPay web:<br><br>Usuario: " + usu.Nombre + "<br>Password: " + usu.Password, Session["Logo"], true);
							Response.Redirect("MisClientes.aspx");
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
							throw (new Exception("Ocurrió un error en el alta de clientes"));
						}
					}
				}
				else
					throw (new Exception(CaptchaControl1.ErrorMessage));
			}
			catch (Exception ex)
			{
				//Logger.EscribirEventLog(ex);
				string script = "<script>alert('" + ex.Message + "');</script>";
				ClientScript.RegisterStartupScript(this.GetType(), DateTime.Now.ToFileTime().ToString(), script);
			}
		}
    }
}
