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

public partial class PagoAceptadoEPayments : Page, IBasicPage
{
	public Boolean LoginVisible { get { return false; } }
	public Control ControlFocus { get { return null; } }
	protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			ProcesarPago();
		}
    }

	private void ProcesarPago()
	{
		if (CodigosEPAY.IsAuthorized(Request.Form["MessageDescrip"])) {
			FacadeDao.ProcesarPago(Convert.ToInt32(Request.Form["OrderCode"]));
		}
	}
}
