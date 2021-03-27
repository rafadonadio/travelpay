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

public partial class MisIventures : WebFormConSeguridad
{
	readonly string gatewayNPS = ConfigurationManager.AppSettings["GatewayNPS"];
	readonly string gatewayEPayments = ConfigurationManager.AppSettings["GatewayEPayments"];

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			grilla.Visible = false;
			BindLista();
		}
		if (Session["Proveedor"] != null)
		{
			MerchantID_NoPost.Value = ((Proveedor)(Session["Proveedor"])).IdGateway;
			NPS_GatewayData_Url_NoPost.Value = ConfigurationManager.AppSettings["NPS_GatewayData_Url"];
		}
	}

	private void BindLista()
	{
		StringBuilder sql = new StringBuilder();
		try
		{
			int? idCliente = null, idProveedor = null;
			if (Session["Cliente"] != null)
			{
				idCliente = ((Cliente)(Session["Cliente"])).Id;
				idProveedor = ((Cliente)(Session["Cliente"])).Proveedor.Id;
				grilla.Columns[0].Visible = false; //Cliente
				grilla.Columns[1].Visible = true; //Empresa
			}
			else if (Session["Proveedor"] != null)
			{
				idProveedor = ((Proveedor)(Session["Proveedor"])).Id;
				grilla.Columns[0].Visible = true; //Cliente
				grilla.Columns[1].Visible = false; //Empresa
			}
			else
			{
				idProveedor = -1;
			}
			int? idVendedor = null;
			if (((Usuario)(Session["Usuario"])).IsVendedor)
			{
				idVendedor = ((Usuario)(Session["Usuario"])).Id;
			}

			//Se invoca a este metodo par reemplazar al job de actualizacion
			ActualizarEstados();
			List<Core.Iventure> iventures = FacadeDao.GetIventure(idCliente, idProveedor, idVendedor);
			grilla.DataSource = iventures;
			grilla.DataBind();
			grilla.Visible = true;
			if (((List<Core.Iventure>)grilla.DataSource).Count == 0)
			{
				grilla.Visible = false;
				lblAviso.Visible = true;
				lblAviso.Text = "No se encontraron solicitudes disponibles para usted.";
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

	private void ActualizarEstados()
	{
		if (Application["UltimaActualizacionEstados"] == null || ((DateTime)Application["UltimaActualizacionEstados"]) < DateTime.Today)
		{
			FacadeDao.ActualizarEstados();
			Application["UltimaActualizacionEstados"] = DateTime.Today;
		}

	}
	protected void BtnReenviar(object sender, EventArgs e)
	{
		try
		{
			string idIventure = ((ImageButton)sender).CommandArgument;
			FacadeDao.EnviarIventureEmail(Convert.ToInt32(idIventure.Trim()), Session["Logo"]);
			string script = "alert('Se ha enviado el mail correctamente');";
			ClientScript.RegisterStartupScript(this.GetType(), DateTime.Now.ToFileTime().ToString(), script);
		}
		catch
		{
			string script = "alert('Hubo un problema al enviar el mail');";
			ClientScript.RegisterStartupScript(this.GetType(), DateTime.Now.ToFileTime().ToString(), script);
		}

	}
	protected void BtnDownload(object sender, EventArgs e)
	{
		string idIventure = ((ImageButton)sender).CommandArgument;
		MagicAjax.AjaxCallHelper.Write("var w= window.open('BajarIventure.aspx?IdIventure=" + idIventure + "' ,'BajarSolicitud', 'resizable=1, scrollbars=1, top=100, left=200, width=550, height=600'); if(w==null || w=='undefined'){		alert('No se pudo abrir la ventana pop up'); 	} else{ w.focus();} ");
	}
	protected void CancelarIventure(object sender, EventArgs e)
	{
		string idIventure = ((ImageButton)sender).CommandArgument;
		FacadeDao.CancelarIventure(Convert.ToInt32(idIventure));
		BindLista();
	}
	protected void BorrarIventure(object sender, EventArgs e)
	{
		string idIventure = ((ImageButton)sender).CommandArgument;
		FacadeDao.BorrarIventure(Convert.ToInt32(idIventure));
		BindLista();
	}

	protected void BtnVer(object sender, EventArgs e)
	{
		string idIventure = ((ImageButton)sender).CommandArgument;
		//Response.Redirect("Iventure.aspx?IdIventure=" + idIventure);
		string queryString = Cryptography.Encriptar("IdIventure=" + idIventure);
		Response.Redirect("Iventure.aspx?" + queryString);

	}

	protected void grilla_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		ImageButton ibCanc, ibBorr, ibReenv, ibReproc;
		Label lblEsp, lblEspCanc;
		string id = "", script = "";
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			if (e.Row.DataItem != null)
			{
				ibCanc = (ImageButton)e.Row.FindControl("imgCancelar");
				ibBorr = (ImageButton)e.Row.FindControl("imgBorrar");
				ibReenv = (ImageButton)e.Row.FindControl("imgReenviar");
				ibReproc = (ImageButton)e.Row.FindControl("imgReprocesar");
				lblEsp = (Label)e.Row.FindControl("lblEspacios");
				lblEspCanc = (Label)e.Row.FindControl("lblEspCancelar");

				if (!(((Usuario)Session["Usuario"]).IsAdminProveedor) || ((Core.Iventure)e.Row.DataItem).Estado != Util.PENDIENTE)
				{
					ibCanc.Visible = false;
					ibReproc.Visible = false;
					lblEsp.Text = "&nbsp;&nbsp;&nbsp;&nbsp;";
				}

				else
				{
					ibCanc.Visible = true;
					ibReproc.Visible = true;
					if (grilla.EditIndex < 0)
					{
						id = ((Core.Iventure)e.Row.DataItem).Id.ToString();
						script = "return CancelarIventure('" + id + "' );";
						ibCanc.Attributes.Add("onclick", script);
						script = "return BorrarIventure('" + id + "' );";
						ibBorr.Attributes.Add("onclick", script);

						id = ((Core.Iventure)e.Row.DataItem).Id.ToString();
						if (((Proveedor)Session["Proveedor"]).Gateway == gatewayNPS)
							script = "return SubmitReprocesarNPS('" + id + "' );";
						else if (((Proveedor)Session["Proveedor"]).Gateway == gatewayEPayments)
							script = "return SubmitReprocesarEPayments('" + id + "' );";
						else script = "";
						ibReproc.Attributes.Add("onclick", script);
					}
				}
				if ((((Usuario)Session["Usuario"]).IsAdminProveedor) && ((Core.Iventure)e.Row.DataItem).Estado != Util.PENDIENTE)
				{
					lblEsp.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
					lblEspCanc.Visible = true;
				}


				if ((((Usuario)Session["Usuario"]).IsAdminProveedor || ((Usuario)Session["Usuario"]).IsVendedor) && ((Core.Iventure)e.Row.DataItem).Estado == Util.PENDIENTE)
				{
					ibReenv.Visible = true;
					lblEsp.Visible = false;
				}
				else
				{
					ibReenv.Visible = false;
					lblEsp.Visible = true;
				}
				if (((Usuario)Session["Usuario"]).IsCliente)
				{
					lblEsp.Visible = false;
				}
				if (((Usuario)Session["Usuario"]).IsAdminProveedor && ((Core.Iventure)e.Row.DataItem).Estado == Util.CANCELADO)
				{
					ibBorr.Visible = true;
				}
			}
		}

	}
	protected void grilla_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		grilla.PageIndex = e.NewPageIndex;
		BindLista();
	}

	
	protected void grilla_Sorting(object sender, GridViewSortEventArgs e)
	{
		BindLista();
		List<Core.Iventure> iventures = (List<Core.Iventure>)grilla.DataSource;
		SortDirection sortDirection = (Session["sortDirection"] == null) ? SortDirection.Descending : (SortDirection)Session["sortDirection"];
		sortDirection = (sortDirection == SortDirection.Ascending) ? SortDirection.Descending : SortDirection.Ascending;
		Session["sortDirection"] = sortDirection;
		if(e.SortExpression.ToLower() == "cliente")
			iventures.Sort(((IComparer<Core.Iventure>)(new IventuresComparerCliente(sortDirection))));
		else if (e.SortExpression.ToLower() == "estado")
			iventures.Sort(((IComparer<Core.Iventure>)(new IventuresComparerEstado(sortDirection))));
		else if (e.SortExpression.ToLower() == "id")
			iventures.Sort(((IComparer<Core.Iventure>)(new IventuresComparerId(sortDirection))));
		grilla.DataSource = iventures;
		grilla.DataBind();       
	}



	
}
public class IventuresComparerCliente : System.Collections.Generic.IComparer<Core.Iventure>
{
	SortDirection sortDirection = SortDirection.Ascending;
	public IventuresComparerCliente(SortDirection sortDirection)
	{
		this.sortDirection = sortDirection;
	}
	public int Compare(Core.Iventure first, Core.Iventure second)
	{
		if (sortDirection == SortDirection.Ascending)
		{
			if (first == null & second == null) { return 0; }
			else if (first == null) { return -1; }
			else if (second == null) { return 1; }
			else { return first.ApellidoNombreCliente.CompareTo(second.ApellidoNombreCliente); }
		}
		else {
			if (first == null & second == null) { return 0; }
			else if (first == null) { return 1; }
			else if (second == null) { return -1; }
			else { return -1*(first.ApellidoNombreCliente.CompareTo(second.ApellidoNombreCliente)); }
		}
	}
}

public class IventuresComparerEstado : System.Collections.Generic.IComparer<Core.Iventure>
{
	SortDirection sortDirection = SortDirection.Ascending;
	public IventuresComparerEstado(SortDirection sortDirection)
	{
		this.sortDirection = sortDirection;
	}
	public int Compare(Core.Iventure first, Core.Iventure second)
	{
		if (sortDirection == SortDirection.Ascending)
		{
			if (first == null & second == null) { return 0; }
			else if (first == null) { return -1; }
			else if (second == null) { return 1; }
			else { return first.Estado.CompareTo(second.Estado); }
		}
		else
		{
			if (first == null & second == null) { return 0; }
			else if (first == null) { return 1; }
			else if (second == null) { return -1; }
			else { return -1 * (first.Estado.CompareTo(second.Estado)); }
		}
	}
}
public class IventuresComparerId : System.Collections.Generic.IComparer<Core.Iventure>
{
	SortDirection sortDirection = SortDirection.Ascending;
	public IventuresComparerId(SortDirection sortDirection)
	{
		this.sortDirection = sortDirection;
	}
	public int Compare(Core.Iventure first, Core.Iventure second)
	{
		if (sortDirection == SortDirection.Ascending)
		{
			if (first == null & second == null) { return 0; }
			else if (first == null) { return -1; }
			else if (second == null) { return 1; }
			else { return first.Id.CompareTo(second.Id); }
		}
		else
		{
			if (first == null & second == null) { return 0; }
			else if (first == null) { return 1; }
			else if (second == null) { return -1; }
			else { return -1 * (first.Id.CompareTo(second.Id)); }
		}
	}
}
