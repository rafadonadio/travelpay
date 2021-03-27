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
using System.Text;

public partial class IventureTest : Page, IBasicPage
{
	Core.Iventure iventure = null;
	System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("es-ar");
	Boolean loginVisible = true;
	public Boolean LoginVisible { get { return loginVisible ; } }
	public Control ControlFocus { get { return txtDetalle; } }
	protected void Page_Load(object sender, EventArgs e)
	{
		btnImprimir.Attributes.Add("onclick", "Imprimir();");
		imgBuscarCuit.Attributes.Add("onclick", "Buscar('cuit');");
		imgBuscarEmail.Attributes.Add("onclick", "Buscar('email');");
		rgvFechaHasta.MaximumValue = DateTime.Today.AddYears(10).ToShortDateString();
		rgvFechaHasta.MinimumValue = DateTime.Today.AddYears(-10).ToShortDateString();
		rgvFechaHasta.ErrorMessage = "Vencimiento Inválido. Valores entre " + rgvFechaHasta.MinimumValue + " y " + rgvFechaHasta.MaximumValue;
		if (!IsPostBack)
		{
			CargarDataGateway();
			CargarIventure();
			CargarVendedores();
			CargarTarjetas();
			CargarCliente();
			HabilitarControles();
		}
	}

	private void CargarVendedores()
	{
		Proveedor prov = null;
		if (Session["Proveedor"] != null)
		{
			prov = (Proveedor)(Session["Proveedor"]);
		}
		else
		{
			if (iventure != null)
				prov = iventure.Proveedor;
		}
		if (prov != null)
		{
			cboVendedores.Items.Clear();
			cboVendedores.DataSource = FacadeDao.GetVendedores(prov.Id);
			cboVendedores.DataTextField = "Nombre";
			cboVendedores.DataValueField = "Id";
			cboVendedores.DataBind();
			if (Session["Proveedor"] != null)
			{
				prov = (Proveedor)(Session["Proveedor"]);
				if (iventure != null)
					cboVendedores.SelectedIndex = cboVendedores.Items.IndexOf(cboVendedores.Items.FindByValue(iventure.IdVendedor.ToString()));
				else
					cboVendedores.SelectedIndex = cboVendedores.Items.IndexOf(cboVendedores.Items.FindByValue(((Usuario)Session["Usuario"]).Id.ToString()));
			}
			else
			{
				cboVendedores.SelectedIndex = cboVendedores.Items.IndexOf(cboVendedores.Items.FindByValue(iventure.IdVendedor.ToString()));
			}
		}
	}
	private void CargarTarjetas()
	{
		Proveedor prov = null;
		cboTarjeta.Items.Clear();
		cboTarjeta.Items.Add(new ListItem("[Seleccione una tarjeta de crédito...]", ""));
		if (Session["Proveedor"] != null)
		{
			prov = (Proveedor)(Session["Proveedor"]);
		}
		else
		{
			if (iventure != null)
				prov = iventure.Proveedor;
		}
		if (prov != null)
		{
			if (prov.TieneAmericanExpress)
			{
				if (prov.Gateway == "NPS - Sub1")
					cboTarjeta.Items.Add(new ListItem(CodigosNPS.GetTarjeta(prov.AmericanExpress), prov.AmericanExpress));
				else if (prov.Gateway == "ePayments")
					cboTarjeta.Items.Add(new ListItem(CodigosEPAY.GetTarjeta(prov.AmericanExpress), prov.AmericanExpress));
				else
					throw new Exception("Nombre de prov.Gateway incorrecto.");
			}
			if (prov.TieneCabal)
			{
				if (prov.Gateway == "NPS - Sub1")
					cboTarjeta.Items.Add(new ListItem(CodigosNPS.GetTarjeta(prov.Cabal), prov.Cabal));
				else if (prov.Gateway == "ePayments")
					cboTarjeta.Items.Add(new ListItem(CodigosEPAY.GetTarjeta(prov.Cabal), prov.Cabal));
				else
					throw new Exception("Nombre de prov.Gateway incorrecto.");
			}
			if (prov.TieneDiners)
			{
				if (prov.Gateway == "NPS - Sub1")
					cboTarjeta.Items.Add(new ListItem(CodigosNPS.GetTarjeta(prov.Diners), prov.Diners));
				else if (prov.Gateway == "ePayments")
					cboTarjeta.Items.Add(new ListItem(CodigosEPAY.GetTarjeta(prov.Diners), prov.Diners));
				else
					throw new Exception("Nombre de prov.Gateway incorrecto.");
			}
			if (prov.TieneMastercard)
			{
				if (prov.Gateway == "NPS - Sub1")
					cboTarjeta.Items.Add(new ListItem(CodigosNPS.GetTarjeta(prov.Mastercard), prov.Mastercard));
				else if (prov.Gateway == "ePayments")
					cboTarjeta.Items.Add(new ListItem(CodigosEPAY.GetTarjeta(prov.Mastercard), prov.Mastercard));
				else
					throw new Exception("Nombre de prov.Gateway incorrecto.");
			}
			if (prov.TieneNaranja)
			{
				if (prov.Gateway == "NPS - Sub1")
					cboTarjeta.Items.Add(new ListItem(CodigosNPS.GetTarjeta(prov.Naranja), prov.Naranja));
				else if (prov.Gateway == "ePayments")
					cboTarjeta.Items.Add(new ListItem(CodigosEPAY.GetTarjeta(prov.Naranja), prov.Naranja));
				else
					throw new Exception("Nombre de prov.Gateway incorrecto.");
			}
			if (prov.TieneNevada)
			{
				if (prov.Gateway == "NPS - Sub1")
					cboTarjeta.Items.Add(new ListItem(CodigosNPS.GetTarjeta(prov.Nevada), prov.Nevada));
				else if (prov.Gateway == "ePayments")
					cboTarjeta.Items.Add(new ListItem(CodigosEPAY.GetTarjeta(prov.Nevada), prov.Nevada));
				else
					throw new Exception("Nombre de prov.Gateway incorrecto.");
			}
			if (prov.TieneVisa)
			{
				if (prov.Gateway == "NPS - Sub1")
					cboTarjeta.Items.Add(new ListItem(CodigosNPS.GetTarjeta(prov.Visa), prov.Visa));
				else if (prov.Gateway == "ePayments")
					cboTarjeta.Items.Add(new ListItem(CodigosEPAY.GetTarjeta(prov.Visa), prov.Visa));
				else
					throw new Exception("Nombre de prov.Gateway incorrecto.");
			}
		}
	}
	private void CargarCliente()
	{
		if (Request.QueryString["IdCliente"] != null)
		{
			int idCliente = Convert.ToInt32(Request.QueryString["IdCliente"]);
			Cliente cliente = FacadeDao.GetCliente(idCliente);
			txtNombre.Text = cliente.Nombre;
			txtApellido.Text = cliente.Apellido;
			txtCUITDNI.Text = cliente.CuitDni;
			txtDomicilio.Text = cliente.Domicilio;
			txtEmail.Text = cliente.Email;
			txtTelefonos.Text = cliente.Telefonos;
			hdnIdCliente.Value = idCliente.ToString();
			txtNombre.Enabled = false;
			txtApellido.Enabled = false;
			txtCUITDNI.Enabled = false;
			txtDomicilio.Enabled = false;
			txtEmail.Enabled = false;
			txtTelefonos.Enabled = false;


		}
	}
	private void HabilitarControles()
	{
		txtImporte.Enabled = false;
		cboCntCuotas.Enabled = false;
		txtIdIventure.Enabled = false;
		txtProveedor.Enabled = false;
		cboVendedores.Enabled = false;
		txtFechaHasta.Enabled = false; 
		btnActualizar.Visible = false;
		btnPagar.Visible = false;
		btnDarDeAlta.Visible = false;
		btnMail.Visible = false;
		btnImprimir.Visible = false;
		cboTarjeta.Visible = false;
		lblTarjeta.Visible = false;
		lblSeleccioneTarjeta.Visible = false;
		
		imgBuscarCuit.Visible = false;
		imgBuscarEmail.Visible = false;
		imgCalendar.Visible = false;

		Usuario usu = (Usuario)(Session["Usuario"]);
		if (usu == null) {
			loginVisible = false;
		}
		if (usu == null || usu.IsSuper || usu.IsCliente || (iventure != null && iventure.Estado != Util.PENDIENTE ))
		{
			txtDetalle.Enabled = false;
			txtNombre.Enabled = false;
			txtApellido.Enabled = false;
			txtCUITDNI.Enabled = false;
			txtDomicilio.Enabled = false;
			txtEmail.Enabled = false;
			txtTelefonos.Enabled = false;
			CaptchaControl1.Visible = false;
			
		}
		if (usu == null || (usu.IsCliente && (iventure != null && iventure.Estado == Util.PENDIENTE)))
		{
			txtDetalle.Enabled = false;
			btnPagar.Visible = true;
			cboTarjeta.Visible = true;
			lblTarjeta.Visible = true;
			lblSeleccioneTarjeta.Visible = true;
			btnImprimir.Visible = true;
		}
		if ((usu == null || usu.IsCliente) && (iventure != null && iventure.Estado == Util.VENCIDO))
		{
			txtDetalle.Enabled = false;
			btnPagar.Visible = false;
			cboTarjeta.Enabled = false;
			lblTarjeta.Visible = true;
			lblSeleccioneTarjeta.Visible = true;
			btnImprimir.Visible = false;
			lblAviso.Text = "La solicitud se encuentra vencida.<br>Por favor comuniquese con la empresa para actualizar<br>el estado de la solicitud.";
	
		}
		if ((usu != null && (usu.IsAdminProveedor || usu.IsVendedor)) && (iventure != null && iventure.Estado == Util.PENDIENTE ))
		{
			btnActualizar.Visible = true;
			txtImporte.Enabled = true;
			txtFechaHasta.Enabled = true;
			cboCntCuotas.Enabled = true;
			btnMail.Visible = true;
			txtNombre.Enabled = false;
			txtApellido.Enabled = false;
			txtCUITDNI.Enabled = false;
			txtDomicilio.Enabled = false;
			txtEmail.Enabled = false;
			txtTelefonos.Enabled = false;
			imgBuscarCuit.Visible = true;
			imgBuscarEmail.Visible = true;
			imgCalendar.Visible = true;
		}
		if ((usu != null && usu.IsAdminProveedor) && (iventure != null && iventure.Estado == Util.PENDIENTE))
		{
			cboVendedores.Enabled = true;
			imgCalendar.Visible = true;
		}
		if ((usu != null && (usu.IsAdminProveedor || usu.IsVendedor)) && iventure == null)
		{
			imgBuscarCuit.Visible = true;
			imgBuscarEmail.Visible = true;
			btnDarDeAlta.Visible = true;
			txtImporte.Enabled = true;
			txtFechaHasta.Enabled = true;
			cboCntCuotas.Enabled = true;
			txtProveedor.Text = ((Proveedor)(Session["Proveedor"])).RazonSocial;
			cboVendedores.SelectedIndex = cboVendedores.Items.IndexOf(cboVendedores.Items.FindByValue(((Usuario)(Session["Usuario"])).Id.Value.ToString()));
			imgCalendar.Visible = true;
		}
		if ((usu != null && (usu.IsAdminProveedor || usu.IsVendedor)) && iventure != null && iventure.Estado== Util.VENCIDO)
		{
			txtFechaHasta.Enabled = true;
			btnActualizar.Visible = true;
			CaptchaControl1.Visible = true;
			imgCalendar.Visible = true;
		}
	}
	private void CargarIventure()
	{
		bool existIdIventure = false;
		string sIdIventure = null;
		if (Request.QueryString["IdCliente"] == null && Request.Path.Length < Request.RawUrl.Length)
		{
			string parameters = Request.RawUrl.Substring(Request.Path.Length + 1);
			string queryString = Cryptography.Desencriptar(parameters);
			string[] values = queryString.Split("=".ToCharArray());
			for (int i = 0; i < values.Length; i++)
			{
				if (values[i].ToLower() == "idiventure")
				{
					existIdIventure = true;
					if (i + 1 < values.Length)
					{
						sIdIventure = values[i + 1];
					}
					break;
				}
			}
		}
		if (existIdIventure)
		{
			int idIventure = Convert.ToInt32(sIdIventure);
			iventure = FacadeDao.GetIventure(idIventure);
			if (iventure != null)
			{
				txtIdIventure.Text = iventure.Id.ToString();
				txtIdIventure.Enabled = false;
				txtProveedor.Text = iventure.Proveedor.RazonSocial;
				egp_MerchantID_NoPost.Value = "test_travelpay";//iventure.Proveedor.IdGateway;
				EPAY_ShopCode_NoPost.Value = iventure.Proveedor.IdGateway;
				dev_NoPost.Value = "1";//(iventure.Proveedor.DevNPS) ? "1" : "0";
				/*txtVendedor.Text = iventure.Vendedor;
				hdnIdVendedor.Value = iventure.IdVendedor.ToString();*/
				cboVendedores.SelectedIndex = cboVendedores.Items.IndexOf(cboVendedores.Items.FindByValue(iventure.IdVendedor.ToString()));
				txtDetalle.Text = iventure.DetalleAereo;
				txtImporte.Text = iventure.ImporteTotal.ToString();
				txtFechaHasta.Text = iventure.Vencimiento.HasValue ? iventure.Vencimiento.Value.ToString("dd/MM/yyyy") : "";
				cboCntCuotas.SelectedIndex = cboCntCuotas.Items.IndexOf(cboCntCuotas.Items.FindByValue(iventure.CntCuotas.ToString()));
				hdnIdCliente.Value = iventure.Cliente.Id.ToString();
				txtNombre.Text = iventure.Cliente.Nombre;
				txtApellido.Text = iventure.Cliente.Apellido;
				txtCUITDNI.Text = iventure.Cliente.CuitDni;
				txtDomicilio.Text = iventure.Cliente.Domicilio;
				txtEmail.Text = iventure.Cliente.Email;
				txtTelefonos.Text = iventure.Cliente.Telefonos;
				Session["Logo"] = iventure.Proveedor.UrlImagen;
				if (iventure.Proveedor.Gateway == "NPS - Sub1")
					btnPagar.Attributes.Add("onclick", "SubmitNPS();");
				else if (iventure.Proveedor.Gateway == "ePayments")
					btnPagar.Attributes.Add("onclick", "SubmitEPayments();");
				else if (iventure.Proveedor.Gateway == "PagoFacil")
					btnPagar.Attributes.Add("onclick", "SubmitPagoFacil();");
			}
			else
			{
				btnPagar.Enabled = false;
			}
		}
		else {
			Proveedor prov = null;
			if (Session["Proveedor"] != null)
			{
				prov = (Proveedor)(Session["Proveedor"]);
				txtFechaHasta.Text = DateTime.Today.AddDays(prov.DiasVencimiento).ToString("dd/MM/yyyy");
			}
				
		}
	}
	private void CargarDataGateway()
	{
		//se carga cuando se carga el iventure
		//dev_NoPost.Value = ConfigurationManager.AppSettings["devNPS"];
		PagoFacil_Id_NoPost.Value = ConfigurationManager.AppSettings["PagoFacil_Id"];
		PagoFacil_Vigencia_NoPost.Value = ConfigurationManager.AppSettings["PagoFacil_Vigencia"];
		PagoFacil_Url_NoPost.Value = ConfigurationManager.AppSettings["PagoFacil_Url"];
		//egp_MerchantID_NoPost se carga cuando se carga el iventure
	}
	private int Grabar()
	{
		int result = -1;
		try
		{
			if (CaptchaControl1.IsValid)
			{
				Core.Iventure ivent = new Core.Iventure();
				ivent.Id = Convert.ToInt32(txtIdIventure.Text.Trim());
				ivent.Proveedor = new Proveedor();
				ivent.Proveedor.Id = ((Proveedor)(Session["Proveedor"])).Id;
				ivent.DetalleAereo = txtDetalle.Text.Trim();
				ivent.ImporteTotal = Convert.ToDouble(txtImporte.Text.Trim());
				ivent.Vencimiento= (txtFechaHasta.Text.Trim()=="")?null:(DateTime?)Convert.ToDateTime(txtFechaHasta.Text.Trim(),ci);
				ivent.CntCuotas = Convert.ToInt32(cboCntCuotas.SelectedValue);
				ivent.IdVendedor = Convert.ToInt32(cboVendedores.SelectedValue);
				result = FacadeDao.ActualizarIventure(ivent);
				if (result != 1)
				{
					throw (new Exception("Ocurrió un error en la actualización de la solicitud"));
				}
			}
			else
			{
				throw (new Exception(CaptchaControl1.ErrorMessage));
			}
			return result;
		}
		catch (Exception ex)
		{
			//Logger.EscribirEventLog(ex);
			string script = "alert('" + ex.Message + "');";
			ClientScript.RegisterStartupScript(this.GetType(), DateTime.Now.ToFileTime().ToString(), script);
			return result;
		}
	}

	protected void btnDarDeAlta_Click(object sender, EventArgs e)
	{
		Page.Validate();
		if (Page.IsValid)
		{
			try
			{
				if (Session["Proveedor"] != null)
				{
					if (CaptchaControl1.IsValid)
					{
						Core.Iventure ivent = new Core.Iventure();
						ivent.Proveedor = new Proveedor();
						ivent.Proveedor.Id = ((Proveedor)(Session["Proveedor"])).Id;
						//ivent.IdVendedor = Convert.ToInt32(hdnIdVendedor.Value);
						ivent.IdVendedor = Convert.ToInt32(cboVendedores.SelectedValue);
						ivent.ImporteTotal = Convert.ToDouble(txtImporte.Text.Trim());
						ivent.Vencimiento = (txtFechaHasta.Text.Trim() == "") ? null : (DateTime?)Convert.ToDateTime(txtFechaHasta.Text.Trim(), ci);
						ivent.CntCuotas = Convert.ToInt32(cboCntCuotas.SelectedValue);
						ivent.DetalleAereo = txtDetalle.Text.Trim();
						Cliente cliente = new Cliente();
						if (!String.IsNullOrEmpty(hdnIdCliente.Value))
						{
							cliente.Id = Convert.ToInt32(hdnIdCliente.Value);
						}
						ivent.Cliente = cliente;
						ivent.EsDonacion = false;
						cliente.Nombre = txtNombre.Text.Trim();
						cliente.Apellido = txtApellido.Text.Trim();
						cliente.CuitDni = txtCUITDNI.Text.Trim();
						cliente.Domicilio = txtDomicilio.Text.Trim();
						cliente.Email = txtEmail.Text.Trim();
						cliente.Telefonos = txtTelefonos.Text.Trim();
						string pass = cliente.Nombre.ToLower() + DateTime.Now.GetHashCode().ToString().Replace("-", "").Trim();
						Usuario usu = new Usuario();
						usu.Nombre = cliente.CuitDni;
						usu.Password = pass;
						usu.Email = cliente.Email;
						int result = FacadeDao.CrearIventure(ref ivent, usu, Session["Logo"]);
						if (result == 1)
						{
							Response.Redirect("MisIventures.aspx");
						}
						else if (result == -1 || result == -4)
						{
							throw (new Exception("Email de cliente existente"));
						}
						else if (result == -2 || result == -3)
						{
							throw (new Exception("Cuit de cliente ya registrado"));
						}
						else
						{
							throw (new Exception("Ocurrió un error en el alta de la solicitud"));
						}
					}
					else
						throw (new Exception(CaptchaControl1.ErrorMessage));
				}
			}
			catch (Exception ex)
			{
				//Logger.EscribirEventLog(ex);
				string script = "alert('" + ex.Message + "');";
				ClientScript.RegisterStartupScript(this.GetType(), DateTime.Now.ToFileTime().ToString(), script);
			}
		}
	}
	protected void btnGrabar_Click(object sender, EventArgs e)
	{
		Page.Validate();
		if (Page.IsValid)
		{
			int result = Grabar();
			if (result == 1)
			{
				Response.Redirect("MisIventures.aspx");
			}
		}
	}
	protected void btnMail_Click(object sender, EventArgs e)
	{
		Page.Validate();
		if (Page.IsValid)
		{
			if (Grabar() == 1)
			{
				try
				{
					FacadeDao.EnviarIventureEmail(Convert.ToInt32(txtIdIventure.Text.Trim()), Session["Logo"]);
					string script = "alert('Se ha enviado el mail correctamente');";
					ClientScript.RegisterStartupScript(this.GetType(), DateTime.Now.ToFileTime().ToString(), script);
				}
				catch
				{
					string script = "alert('Hubo un problema al enviar el mail');";
					ClientScript.RegisterStartupScript(this.GetType(), DateTime.Now.ToFileTime().ToString(), script);
				}
			}
		}
	}
	
}
