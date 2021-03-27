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

public partial class Pago : Page, IBasicPage
{
	public Boolean LoginVisible { get { return true; } }
	public Control ControlFocus { get { return null; } }
	protected void Page_Load(object sender, EventArgs e)
    {
		gatewayData.Value = "campo=valor|campo1=valor1|campo2=valor2|campo3=valor3";
		/*btnOK.Attributes.Add("onclick", "Submit('PagoAceptado.aspx');");
		btnError.Attributes.Add("onclick", "Submit('PagoRechazado.aspx');");
		Cancel.Attributes.Add("onclick", "Submit('PagoCancelado.aspx');");*/
		if (!IsPostBack && Request["DatosEnc"] != null)
		{
			lblDatosEnc.Text = Request["DatosEnc"];
		}

    }
	/*protected void btnOK_Click(object sender, EventArgs e)
	{
		Response.Redirect(ConfigurationManager.AppSettings["UrlOk"]);
	}
	protected void btnError_Click(object sender, EventArgs e)
	{
		Response.Redirect(ConfigurationManager.AppSettings["UrlError"]);
	}
	protected void Cancel_Click(object sender, EventArgs e)
	{
		Response.Redirect(ConfigurationManager.AppSettings["UrlBack"]);
	}*/
}
