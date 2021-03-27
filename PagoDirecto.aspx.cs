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

public partial class PagoDirecto : Page, IBasicPage
{
	Core.Iventure iventure = null;
	System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("es-ar");

	public Boolean LoginVisible { get { return false; } }
	public Control ControlFocus { get{return txtApellido;} }
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			CargarProveedor();
			CargarImporte();
			CargarDataGateway();
			CargarTarjetas();
		}
	}

	private void CargarProveedor() {
		Proveedor prov = FacadeDao.GetProveedor(Request.QueryString["IdGateway"]);
		Session["Proveedor"] = prov;

		txtProveedor.Text = prov.RazonSocial;
		txtProveedor.Enabled = false;
		egp_MerchantID_NoPost.Value = prov.IdGateway;
		EPAY_ShopCode_NoPost.Value = prov.IdGateway;
		dev_NoPost.Value = (prov.DevNPS) ? "1" : "0";
		Session["scriptPago"] = "";
		
		if (prov.Gateway == "NPS - Sub1")
			Session["scriptPago"] = "SubmitNPS();";
		else if (prov.Gateway == "ePayments")
			Session["scriptPago"] = "SubmitEPayments();";
		else if (prov.Gateway == "PagoFacil")
			Session["scriptPago"] = "SubmitPagoFacil();";
	}
	private void CargarImporte()
	{
		string monto = "";
		string fijo = "";
		if (!String.IsNullOrEmpty(Request.QueryString["Monto"])) {
			monto = Request.QueryString["Monto"];
			txtImporte.Text = monto;
			if (!String.IsNullOrEmpty(Request.QueryString["Fijo"]))
			{
				fijo = Request.QueryString["Fijo"];
				if (fijo.ToLower() == "si" || fijo.ToLower() == "yes")
				{
					txtImporte.Enabled = false;
				}
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
	
	private int GrabarDonacion()
	{
		int result = -1;
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
						ivent.EsDonacion = true;
						ivent.Proveedor = new Proveedor();
						ivent.Proveedor.Id = ((Proveedor)(Session["Proveedor"])).Id;
						ivent.ImporteTotal = Convert.ToDouble(txtImporte.Text.Trim());
						ivent.CntCuotas = 1;
						ivent.DetalleAereo = String.Empty;
						Cliente cliente = new Cliente();
						if (!String.IsNullOrEmpty(hdnIdCliente.Value))
						{
							cliente.Id = Convert.ToInt32(hdnIdCliente.Value);
						}
						ivent.Cliente = cliente;
						cliente.Nombre = txtNombre.Text.Trim();
						cliente.Apellido = txtApellido.Text.Trim();
						cliente.CuitDni = String.Empty;
						cliente.Domicilio = String.Empty;
						cliente.Email = txtEmail.Text.Trim();
						string pass = cliente.Nombre.ToLower() + DateTime.Now.GetHashCode().ToString().Replace("-", "").Trim();
						Usuario usu = new Usuario();
						usu.Nombre = cliente.CuitDni;
						usu.Password = pass;
						usu.Email = cliente.Email;
						result = FacadeDao.CrearIventure(ref ivent, usu, Session["Logo"]);
						if (result == 1)
						{
							//Response.Redirect("GraciasPorLaDonacion.aspx");
							txtIdIventure.Value = ivent.Id.ToString();
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
							throw (new Exception("Ocurrió un error en el alta de la donación"));
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
		return result;
	}
	protected void btnDonar_Click(object sender, EventArgs e)
	{
		Page.Validate();
		if (Page.IsValid)
		{
			if (GrabarDonacion() == 1)
			{
				string scriptPago = (string)Session["scriptPago"];
				ClientScript.RegisterStartupScript(this.GetType(), DateTime.Now.ToFileTime().ToString(), scriptPago);
			}
			else
			{
				string script = "alert('Hubo un problema al grabar la donación');";
				ClientScript.RegisterStartupScript(this.GetType(), DateTime.Now.ToFileTime().ToString(), script);
			}

		}
	}

	
}
