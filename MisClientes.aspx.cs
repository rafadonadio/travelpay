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

public partial class MisClientes : WebFormConSeguridad
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
			int idProveedor = ((Proveedor)(Session["Proveedor"])).Id;
			List<Cliente> Clientes = FacadeDao.GetClientes(idProveedor);
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
	protected void BtnCrearIventure(object sender, EventArgs e)
	{
		string idCliente = ((ImageButton)sender).CommandArgument;
		Response.Redirect("Iventure.aspx?IdCliente=" + idCliente);

	}
	protected void BtnVer(object sender, EventArgs e)
	{
		string idCliente = ((ImageButton)sender).CommandArgument;
		Response.Redirect("AMCliente.aspx?IdCliente=" + idCliente);

	}
	protected void btnCrearCliente_Click(object sender, EventArgs e)
	{
		Response.Redirect("AMCliente.aspx");
	}
	protected void EliminarCliente(object sender, EventArgs e)
	{
		string idCliente = ((ImageButton)sender).CommandArgument;
		FacadeDao.BorrarCliente(Convert.ToInt32(idCliente));
		BindLista();
	}
	protected void grilla_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		ImageButton ibElim;
		string cuitDni = "", script = "";
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			if (e.Row.DataItem != null)
			{
				ibElim = (ImageButton)e.Row.FindControl("imgEliminar");
				if (  (((Usuario)Session["Usuario"]).IsAdminProveedor) && !(((Core.Cliente)e.Row.DataItem).SoloLectura)  )
				{
					ibElim.Visible = true;
					cuitDni = ((Core.Cliente)e.Row.DataItem).CuitDni;
					script = "return EliminarCliente('" + cuitDni + "' );";
					ibElim.Attributes.Add("onclick", script);
				}
				else
				{
					ibElim.Visible = false;
				}
			}
		}

	}
	
	protected void grilla_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		grilla.PageIndex = e.NewPageIndex;
		BindLista();
	}

}
