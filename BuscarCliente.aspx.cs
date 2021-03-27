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
using System.Text;
using System.Collections.Generic;
using Core;

public partial class BuscarCliente : Page, IBasicPage
{
	string email = null, cuit = null;
	protected void Page_Load(object sender, EventArgs e)
	{
		Response.Expires = 0;
		if (!IsPostBack)
		{
			if (Request.QueryString["tipo"] != null && Request.QueryString["tipo"] == "email")
			{
				email = Request.QueryString["email"];
				cboTipoDato.SelectedIndex = cboTipoDato.Items.IndexOf(cboTipoDato.Items.FindByValue("email"));
				txtData.Text = email;
			}
			if (Request.QueryString["cuit"] != null && Request.QueryString["tipo"] == "cuit")
			{
				cuit = Request.QueryString["cuit"];
				cboTipoDato.SelectedIndex = cboTipoDato.Items.IndexOf(cboTipoDato.Items.FindByValue("cuit"));
				txtData.Text = cuit;
			}
			if (!string.IsNullOrEmpty(txtData.Text))
				BindLista();
		}
	}
	private void BindLista()
	{
		StringBuilder sql = new StringBuilder();
		try
		{
			int idProveedor = ((Proveedor)(Session["Proveedor"])).Id;
			List<Cliente> Clientes = FacadeDao.GetClientes(idProveedor, cuit, email);
			grilla.DataSource = Clientes;
			grilla.DataBind();
			grilla.Visible = true;
			if (((List<Cliente>)grilla.DataSource).Count == 0)
			{
				grilla.Visible = false;
				lblAviso.Visible = true;
				lblAviso.Text = "No se encontraron clientes disponibles para usted.";
			}
			else
			{
				grilla.Visible = true;
				lblAviso.Visible = false;
			}
		}
		catch
		{
			//Logger.EscribirEventLog(ex);
			lblAviso.Text = "Ocurrio un error al ejecutar la consulta, pongase en contacto con el administrador";
			lblAviso.Visible = true;
		}
	}

	public Boolean LoginVisible { get { return true; } }
	public Control ControlFocus { get { return cboTipoDato; } }
	protected void grilla_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		grilla.PageIndex = e.NewPageIndex;
		BindLista();
	}

	protected void grilla_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		StringBuilder data = new StringBuilder();
		Label lrdx;
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			if (e.Row.DataItem != null)
			{
				lrdx = (Label)e.Row.FindControl("lblRadio");
				data = new StringBuilder();
				data.Append("<input id=\"" + lrdx.ClientID + "\" type=\"radio\" name=\"searchCliente\" value=\"" + ((Cliente)e.Row.DataItem).Id.ToString() + "\" nombre=\"" + ((Cliente)e.Row.DataItem).Nombre + "\"");
				data.Append(" apellido=\"" + ((Cliente)e.Row.DataItem).Apellido.Replace("'", "").Replace("\"", "") + "\"");
				data.Append(" cuitDni=\"" + ((Cliente)e.Row.DataItem).CuitDni.Replace("'", "").Replace("\"", "") + "\"");
				data.Append(" domicilio=\"" + ((Cliente)e.Row.DataItem).Domicilio.Replace("'", "").Replace("\"", "") + "\"");
				data.Append(" email=\"" + ((Cliente)e.Row.DataItem).Email.Replace("'", "").Replace("\"", "") + "\"");
				data.Append(" telefonos=\"" + ((Cliente)e.Row.DataItem).Telefonos.Replace("'", "").Replace("\"", "") + "\"");
				data.Append(" />");
				lrdx.Text = data.ToString();
			}
		}
	}
	protected void btnBuscar_Click(object sender, EventArgs e)
	{
		Page.Validate();
		if (Page.IsValid)
		{
			if (cboTipoDato.SelectedValue.ToLower() == "email")
			{
				email = txtData.Text;
				cuit = null;
			}
			else
			{
				email = null;
				cuit = txtData.Text;
			}
			BindLista();
		}
	}
}
