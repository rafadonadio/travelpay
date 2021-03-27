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

public partial class MisProveedores : WebFormConSeguridad
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
			List<Proveedor> proveedores = FacadeDao.GetProveedores();
			grilla.DataSource = proveedores;
			grilla.DataBind();
			grilla.Visible = true;
			if (((List<Proveedor>)grilla.DataSource).Count == 0)
			{
				grilla.Visible = false;
				lblAviso.Visible = true;
				lblAviso.Text = "No se encontraron empresas disponibles.";
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
			lblAviso.Text = "Ocurrió un error al ejecutar la consulta, pongase en contacto con el administrador";
			lblAviso.Visible = true;
		}
	}
	protected void BtnVer(object sender, EventArgs e)
	{
		string idProveedor = ((ImageButton)sender).CommandArgument;
		//MagicAjax.AjaxCallHelper.Write("window.open('XSLs/DownloadFactura.aspx?id=" + idFactura + "','');");
		Response.Redirect("AMProveedor.aspx?IdProveedor=" + idProveedor);

	}
	protected void btnProveedor_Click(object sender, EventArgs e)
	{
		Response.Redirect("AMProveedor.aspx");
	}
	protected void grilla_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		grilla.PageIndex = e.NewPageIndex;
		BindLista();
	}

}
