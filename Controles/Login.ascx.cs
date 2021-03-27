using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System;

using Core;

public partial class Controles_Login : System.Web.UI.UserControl//, IBasicPage
{
	string redirect = "";
	//public Boolean LoginVisible { get { return true; } }
	public Control ControlFocus { get { return txtUsuario; } }
	protected void Page_Load(object sender, EventArgs e)
    {
		if (((IBasicPage)Page).LoginVisible)
		{
			this.pnlLogIn.Visible = Session["usuario"] != null;
			this.pnlLogOut.Visible = !this.pnlLogIn.Visible;
		}
		else {
			this.pnlLogIn.Visible = false;
			this.pnlLogOut.Visible = false;
		}
		Usuario usu = null;
		if (Session["Usuario"] != null)
		{
			usu = (Usuario)(Session["Usuario"]);
			lblUsuarioLogueado.Text = usu.Nombre;
			if (usu.IsProveedor)
			{
				lblTipoUsu.Text = "Empresa: ";
				lblNombre.Text = ((Proveedor)(Session["Proveedor"])).RazonSocial;
			}
			else if (usu.IsCliente)
			{
				lblTipoUsu.Text = "Cliente: ";
				lblNombre.Text = ((Cliente)(Session["Cliente"])).Nombre + " " + ((Cliente)(Session["Cliente"])).Apellido;
			}
			else {
				lblTipoUsu.Text = "Administrador: ";
				lblNombre.Text = ((Usuario)(Session["Usuario"])).Nombre;
			}
		}
		/*else {
			txtUsuario.Focus();
		}*/
		GetMenu(usu);
    }

	private void GetMenu(Usuario usu)
	{
		string link = "<input type=\"submit\" name=\"lnk{0}\" value=\"{1}\" onclick=\"window.location.href='{2}';\" class=\"boton\" style=\"color:Black;background-color:Transparent;border-style:None;font-size:8pt;font-weight:bold;width:160px;cursor:hand;\" />";
		string separador = "<br>"; 
		lblMenu.Text = "";
		redirect = "Index2.aspx";
		if (usu != null)
		{
			if (usu.IsSuper)
			{
				lblMenu.Text = String.Format(link, "Proveedores", "Empresas", "MisProveedores.aspx") + separador + String.Format(link, "Transacciones", "Transacciones", "ReporteTransacciones.aspx");//ReporteTransacciones.aspx
				redirect = "MisProveedores.aspx";
			}
			if (usu.IsAdminProveedor)
			{
				lblMenu.Text = String.Format(link, "CrearIventure", "Crear solicitud", "Iventure.aspx") + separador + String.Format(link, "Clientes", "Clientes", "MisClientes.aspx") + separador + String.Format(link, "Usuarios", "Usuarios", "MisUsuarios.aspx") + separador + String.Format(link, "Iventures", "Solicitudes", "MisIventures.aspx") + separador + String.Format(link, "TransOperador", "Trans. x Operador", "ReporteOperador.aspx");
				redirect = "MisClientes.aspx";
			}
			if (usu.IsVendedor)
			{
				lblMenu.Text = String.Format(link, "CrearIventure", "Crear solicitud", "Iventure.aspx") + separador + String.Format(link, "Iventures", "Solicitudes", "MisIventures.aspx");
				redirect = "Iventure.aspx";
			}
			if (usu.IsCliente)
			{
				lblMenu.Text = String.Format(link, "Iventures", "Solicitudes", "MisIventures.aspx");
				redirect = "MisIventures.aspx";
			}
		}
	}

    protected void btnLogOff_Click(object sender, EventArgs e)
    {
		Session.Clear();
		Response.Redirect("Index2.aspx");
    }
    protected void btnselector_Click(object sender, EventArgs e)
    {
        Response.Redirect("selector.aspx");
    }

	protected void btnLogin_Click(object sender, ImageClickEventArgs e)
	{
		if (txtPassword.Text.Trim() != "" && txtUsuario.Text.Trim() != "")
		{
			Usuario usu = FacadeDao.GetUsuario(txtUsuario.Text, txtPassword.Text);
			if (usu != null)
			{
				Session["Usuario"] = usu;
				GetMenu(usu);
				if (usu.IsProveedor)
				{
					Session["Proveedor"] = FacadeDao.GetProveedor(usu.IdProveedor.Value);
				}
				else if (usu.IsCliente)
				{
					Session["Cliente"] = FacadeDao.GetCliente(usu.IdCliente.Value);
				}
				SetLogo();
				Response.Redirect(redirect);
			}
			else
			{
				string script = "alert('Usuario o password equivocado');";
				Page.ClientScript.RegisterStartupScript(this.GetType(), DateTime.Now.ToFileTime().ToString(), script);
			}
		}
		else
		{
			string script = "alert('Complete el usuario y password');";
			Page.ClientScript.RegisterStartupScript(this.GetType(), DateTime.Now.ToFileTime().ToString(), script);
		}
	}

	private void SetLogo()
	{
		int idProveedor = -1;
		if (Session["Cliente"] != null)
		{
			idProveedor = ((Cliente)(Session["Cliente"])).Proveedor.Id;
		}
		else if (Session["Proveedor"] != null)
		{
			idProveedor = ((Proveedor)(Session["Proveedor"])).Id;
		}
		if (idProveedor == -1)
			Session["Logo"]="images/Logos/travelPay.png";
		else {
			Session["Logo"] = FacadeDao.GetLogo(idProveedor);
		}
	}
}
