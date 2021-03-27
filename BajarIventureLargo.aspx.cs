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
using Core;

public partial class BajarIventureLargo : WebFormConSeguridad
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack) {
			CargarIventure();
			//AbrirWord();
		}
    }

	private void CargarIventure()
	{
		if (Request.QueryString["IdIventure"] != null)
		{
			int idIventure = Convert.ToInt32(Request.QueryString["IdIventure"]);
			Core.Iventure  iventure = FacadeDao.GetIventure(idIventure);
			if (iventure != null)
			{
				txtIdIventure.Text = iventure.Id.ToString();
				//txtIdIventure.Enabled = false;
				txtProveedor.Text = iventure.Proveedor.RazonSocial;
				txtVencimiento.Text = ((iventure.Vencimiento.HasValue) ? iventure.Vencimiento.Value.ToShortDateString() : "");
				txtDetalle.Text = iventure.DetalleAereo;
				cboAereo.SelectedIndex = cboAereo.Items.IndexOf(cboAereo.Items.FindByValue(iventure.TipoAereo));
				txtImporteAereo.Text = iventure.ImporteAereo.ToString();
				txtEstadia.Text = iventure.CiudadEstadia;
				txtPasajeros.Text = iventure.PasajerosEstadia.ToString();
				txtImporteEstadia.Text = iventure.ImporteEstadia.ToString();
				txtDetallesAuto.Text = iventure.DetalleAutomovil;
				txtImporteAuto.Text = iventure.ImporteAutomovil.ToString();
				txtImporteTotal.Text = iventure.ImporteTotal.ToString();
				txtNombre.Text = iventure.Cliente.Nombre;
				txtApellido.Text = iventure.Cliente.Apellido;
				txtCUITDNI.Text = iventure.Cliente.CuitDni;
				txtDomicilio.Text = iventure.Cliente.Domicilio;
				txtEmail.Text = iventure.Cliente.Email;
				txtTelefonos.Text = iventure.Cliente.Telefonos;
				txtImporteTotal.Enabled = false;
				txtIdIventure.Enabled = false;
				txtProveedor.Enabled = false;
				txtVencimiento.Enabled = false;
				txtDetalle.Enabled = false;
				cboAereo.Enabled = false;
				txtImporteAereo.Enabled = false;
				txtEstadia.Enabled = false;
				txtPasajeros.Enabled = false;
				txtImporteEstadia.Enabled = false;
				txtDetallesAuto.Enabled = false;
				txtImporteAuto.Enabled = false;
				txtNombre.Enabled = false;
				txtApellido.Enabled = false;
				txtCUITDNI.Enabled = false;
				txtDomicilio.Enabled = false;
				txtEmail.Enabled = false;
				txtTelefonos.Enabled = false;
			}
		}
	}
	private void AbrirWord()
	{
		/*Response.ContentType = "application/application/msword"; //"application/vnd.word";
		Response.AddHeader("Content-disposition", "attachment;filename=\"iventure.doc\"");
		Response.Clear();
		Response.Buffer = true;
		Response.Expires = -1;
		System.IO.StringWriter sw = new System.IO.StringWriter();
		HtmlTextWriter writer = new HtmlTextWriter(sw);
		tblIventure.RenderControl(writer);
		//txtIdIventure.RenderControl(writer);
		byte[] b = Encoding.UTF8.GetBytes(sw.ToString());
		Response.BinaryWrite(b);
		Response.End();*/
		Response.ContentType = "application/html"; //"application/vnd.word";
		Response.AddHeader("Content-disposition", "attachment;filename=\"Solicitud.html\"");
		Response.End();
	}
	
}
