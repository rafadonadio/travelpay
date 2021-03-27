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

public partial class BajarIventure : WebFormConSeguridad
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
			Core.Iventure iventure = FacadeDao.GetIventure(idIventure);
			if (iventure != null)
			{
				txtIdIventure.Text = iventure.Id.ToString();
				//txtIdIventure.Enabled = false;
				txtProveedor.Text = iventure.Proveedor.RazonSocial;
				txtVendedor.Text = iventure.Vendedor;
				txtDetalle.Text = iventure.DetalleAereo;
				txtImporte.Text = iventure.ImporteTotal.ToString("F2");
				cboCntCuotas.SelectedIndex = cboCntCuotas.Items.IndexOf(cboCntCuotas.Items.FindByValue(iventure.CntCuotas.ToString()));
				txtNombre.Text = iventure.Cliente.Nombre;
				txtApellido.Text = iventure.Cliente.Apellido;
				txtCUITDNI.Text = iventure.Cliente.CuitDni;
				txtDomicilio.Text = iventure.Cliente.Domicilio;
				txtEmail.Text = iventure.Cliente.Email;
				txtTelefonos.Text = iventure.Cliente.Telefonos;
				txtImporte.Enabled = false;
				cboCntCuotas.Enabled = false;
				txtIdIventure.Enabled = false;
				txtProveedor.Enabled = false;
				txtVendedor.Enabled = false;
				txtDetalle.Enabled = false;
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
