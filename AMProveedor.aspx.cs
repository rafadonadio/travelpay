using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using Core;

public partial class AMProveedor : WebFormConSeguridad
{
	Proveedor proveedor = null;
	readonly string gatewayNPS = ConfigurationManager.AppSettings["GatewayNPS"];
	readonly string gatewayEPayments = ConfigurationManager.AppSettings["GatewayEPayments"];
	
	protected void Page_Load(object sender, EventArgs e)
    {
		cboGateway.Attributes.Add("onchange", "EnableCreditsCards('" + gatewayNPS + "', '" + gatewayEPayments + "')");
		SetOnLoadFunction();
		txtFechaAlta.Enabled = false;
		txtDiasVencimiento.Text = ConfigurationManager.AppSettings["DiasVencimiento"];
		if (!IsPostBack)
        {
			CargarGateways();
			CargarProveedor();
			BindLista();
		}
		btnCrearUsuario.Visible = (proveedor != null);
		lblBtnUsuario.Text = ((Request.QueryString["IdProveedor"] != null) ? "" : "Para crear usuarios primero debe grabar la empresa");

	}

	private void SetOnLoadFunction()
	{
		/*<script language=JavaScript for='window' event='onload'>
	EnableCreditsCards();
</script>*/
		string sScript;
		sScript = "<script language=JavaScript for='window' event='onload'>\n";
		sScript += "   EnableCreditsCards('" + gatewayNPS + "', '" + gatewayEPayments + "');\n";
		sScript += "</script>";
		Page.RegisterClientScriptBlock("Onload_For_Window", sScript);
	}

	private void CargarGateways()
	{
		cboGateway.Items.Clear();
		cboGateway.Items.Add(new ListItem("Seleccionar..."));
		cboGateway.Items.Add(new ListItem(gatewayEPayments));
		cboGateway.Items.Add(new ListItem(gatewayNPS));
	}

	private void BindLista()
	{
		StringBuilder sql = new StringBuilder();
		try
		{
			List<Usuario> usuarios = new List<Usuario>();
			int? idProveedor = null;
			if (Request.QueryString["IdProveedor"] != null)
			{
				idProveedor = Convert.ToInt32(Request.QueryString["IdProveedor"]);
				usuarios = FacadeDao.GetUsuarios(idProveedor);
			}
			grilla.DataSource = usuarios;
			grilla.DataBind();
			
		}
		catch (Exception ex)
		{
			//Logger.EscribirEventLog(ex);
			throw ex;
		}
	}
	private void CargarProveedor()
	{
		if (Request.QueryString["IdProveedor"] != null)
		{
			int idProveedor = Convert.ToInt32(Request.QueryString["IdProveedor"]);
			proveedor = FacadeDao.GetProveedor(idProveedor);
			if (proveedor != null)
			{
				txtIdProveedor.Value = proveedor.Id.ToString();
				txtEmail.Text = proveedor.Email;
				txtCuit.Text = proveedor.CUIT;
				txtIIBB.Text = proveedor.IIBB;
				txtRazonsocial.Text = proveedor.RazonSocial;
				txtFechaAlta.Text = proveedor.Alta.ToShortDateString();
				cboGateway.SelectedIndex = cboGateway.Items.IndexOf(cboGateway.Items.FindByValue(proveedor.Gateway));
				txtIdGateway.Text = proveedor.IdGateway.ToString();
				txtNombreResponsable.Text = proveedor.NombreResp;
				txtApellidoResponsable.Text = proveedor.ApellidoResp;
				txtDNIResponsable.Text = proveedor.DNIResp;
				txtCargoResponsable.Text = proveedor.CargoResp;
				txtCntTransacciones.Text = proveedor.CntTransacciones.ToString();
				txtMontoTransacciones.Text = proveedor.MontoTransacciones.ToString("F2");
				txtImagen.Text = proveedor.UrlImagen;
				txtTelefonos.Text = proveedor.Telefonos;
				txtClaveEncNPS.Text = proveedor.ClaveEncNPS;
				cboDevNPS.SelectedIndex = cboDevNPS.Items.IndexOf(cboDevNPS.Items.FindByValue(proveedor.DevNPS.ToString()));
				txtDiasVencimiento.Text = proveedor.DiasVencimiento.ToString();
				chkAmerican.Checked = proveedor.TieneAmericanExpress;
				chkCabal.Checked = proveedor.TieneCabal;
				chkDiners.Checked = proveedor.TieneDiners;
				chkMastercard.Checked = proveedor.TieneMastercard;
				chkNaranja.Checked = proveedor.TieneNaranja;
				chkNevada.Checked = proveedor.TieneNevada;
				chkVisa.Checked = proveedor.TieneVisa;
			}
		}
		else {
			txtFechaAlta.Text = DateTime.Today.ToShortDateString();
		}
	}


	protected void btnAgregar_Click(object sender, EventArgs e)
	{
		Page.Validate();
		if (Page.IsValid)
		{
			try
			{
				if (CaptchaControl1.IsValid)
				{
					Proveedor prov = new Proveedor();
					if (!String.IsNullOrEmpty(txtIdProveedor.Value))
						prov.Id = Convert.ToInt32(txtIdProveedor.Value);
					prov.Email = txtEmail.Text.Trim();
					prov.CUIT = txtCuit.Text.Trim();
					prov.IIBB = txtIIBB.Text.Trim();
					prov.RazonSocial = txtRazonsocial.Text.Trim();
					prov.Alta = Convert.ToDateTime(txtFechaAlta.Text.Trim());
					prov.Gateway = cboGateway.SelectedValue;
					prov.IdGateway = txtIdGateway.Text.Trim();
					prov.NombreResp = txtNombreResponsable.Text.Trim();
					prov.ApellidoResp = txtApellidoResponsable.Text.Trim();
					prov.DNIResp = txtDNIResponsable.Text.Trim();
					prov.CargoResp = txtCargoResponsable.Text.Trim();
					prov.UrlImagen = (txtImagen.Text.Trim() == "") ? "Images/Logos/travelpay.png" : txtImagen.Text.Trim();
					prov.Telefonos = txtTelefonos.Text.Trim();
					prov.ClaveEncNPS = txtClaveEncNPS.Text.Trim();
					prov.DevNPS = Convert.ToBoolean(cboDevNPS.SelectedValue);
					prov.DiasVencimiento = Convert.ToInt32(txtDiasVencimiento.Text.Trim());

					#region Tarjetas
					if (chkAmerican.Checked)
					{
						prov.SetAmericanExpress = prov.GetCodeCard(chkAmerican.Attributes["description"]);//chkAmerican.Value;
					}
					if (chkCabal.Checked)
					{
						prov.SetCabal = prov.GetCodeCard(chkCabal.Attributes["description"]);//chkCabal.Value;
					}
					if (chkDiners.Checked)
					{
						prov.SetDiners = prov.GetCodeCard(chkDiners.Attributes["description"]);//chkDiners.Value;
					}
					if (chkMastercard.Checked)
					{
						prov.SetMastercard = prov.GetCodeCard(chkMastercard.Attributes["description"]);//chkMastercard.Value;
					}
					if (chkNaranja.Checked)
					{
						prov.SetNaranja = prov.GetCodeCard(chkNaranja.Attributes["description"]);//chkNaranja.Value;
					}
					if (chkNevada.Checked)
					{
						prov.SetNevada = prov.GetCodeCard(chkNevada.Attributes["description"]);//chkNevada.Value;
					}
					if (chkVisa.Checked)
					{
						prov.SetVisa = prov.GetCodeCard(chkVisa.Attributes["description"]);//chkVisa.Value;
					}

					#endregion Tarjetas

					int result;
					if (prov.Id == 0)
						result = FacadeDao.CrearProveedor(prov);
					else
						result = FacadeDao.ActualizarProveedor(prov);
					if (result == 1)
					{
						Response.Redirect("MisProveedores.aspx");
					}
					else
					{
						throw (new Exception("Ocurrió un error en el alta / actualización de la empresa"));
					}
				}
				else
					throw (new Exception(CaptchaControl1.ErrorMessage));
			}
			catch (Exception ex)
			{
				//Logger.EscribirEventLog(ex);
				string script = "alert('" + ex.Message + "');";
				ClientScript.RegisterStartupScript(this.GetType(), DateTime.Now.ToFileTime().ToString(), script);

			}
		}
	}

	protected void BorrarUsuario(object sender, EventArgs e)
	{
		string idUsuario = ((ImageButton)sender).CommandArgument;
		FacadeDao.BorrarUsuario(Convert.ToInt32(idUsuario));
		BindLista();
	}
	

	public void grilla_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		ImageButton ibElim;
		string nombre="",  script="";
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			if (e.Row.DataItem!= null)
			{
				ibElim = (ImageButton)e.Row.FindControl("imgBorrar");

				if ( !((Usuario)Session["Usuario"]).IsSuper && ((Usuario)e.Row.DataItem).RolName.ToLower()=="administrador")
				{
					ibElim.Visible = false;
				}
				else
				{
					ibElim.Visible = true;
					if (grilla.EditIndex < 0)
					{
						nombre = ((Usuario)e.Row.DataItem).Nombre;
						script = "return eliminarUsuario('" + nombre + "', '" + ((Usuario)e.Row.DataItem).RolName.ToLower() + "' );";
					}
					ibElim.Attributes.Add("onclick", script);
				}
			}
		}
	}
	
	protected void grilla_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		grilla.PageIndex = e.NewPageIndex;
		BindLista();
	}
	protected void btnCrearUsuario_Click(object sender, EventArgs e)
	{
		Response.Redirect("AMUsuario.aspx?Back=AMProveedor&IdProveedor=" + txtIdProveedor.Value);
	}
}
