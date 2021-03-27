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

public partial class PagoRechazadoEPayments : Page, IBasicPage
{
	public Boolean LoginVisible { get { return false; } }
	public Control ControlFocus { get { return null; } }
	protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			ProcesarRechazo();
		}
	}
	private void ProcesarRechazo()
	{
		if (Request.Form["ErrorId"] != "1")
		{
			lblData.Text = "Nro de Solicitud: " + Request.Form["OrderCode"] + "<br/>Código: " + Request.Form["ErrorId"] + "<br/>Descripción: " + Request.Form["ErrorDescrip"];
		}
		else {
			lblData.Text = "La solicitud nro.  " + Request.Form["OrderCode"] + " ya fue dada de alta en el gateway de pago EPayments.<br/>Por favor, dirijase al mail que ha recibido de EPayments, para terminar de efectuar el pago.<br/>Muchas gracias.";
		}
	}
}
