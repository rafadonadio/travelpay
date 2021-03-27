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

public partial class DobleConfirmacion : Page, IBasicPage
{
	public Boolean LoginVisible { get { return true; } }
	public Control ControlFocus { get { return null; } }
	protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack) {
			ProcesarConfirmacion();
		}

    }

	private void ProcesarConfirmacion()
	{
		if (Request.QueryString["egp_data"] != null && Request.QueryString["MerchantId"] != null)
		{
			//string data = Core.NpsEncripterHelper.Decrypt(Request.QueryString["egp_data"], ConfigurationManager.AppSettings["Key"]);
			Proveedor prov = null;
			prov = FacadeDao.GetProveedor(Request.QueryString["MerchantId"]);
			if (prov != null)
			{
				string data = Core.NpsEncripterHelper.Decrypt(Request.QueryString["egp_data"], prov.ClaveEncNPS);
				string[] campos = data.Split("|".ToCharArray());
				string codigo = "-11111";
				string sOrderId = "-1";
				string sAmount = "-1";
				for (int i = 0; i < campos.Length; i++)
				{
					if (campos[i].StartsWith("egp_ResponseCode="))
					{
						codigo = campos[i].Substring("egp_ResponseCode=".Length);
					}
					else if (campos[i].StartsWith("egp_OrderID="))
					{
						sOrderId = campos[i].Substring("egp_OrderID=".Length);
					}
					else if (campos[i].StartsWith("egp_Amount="))
					{
						sAmount = campos[i].Substring("egp_Amount=".Length);
					}
				}
				string descripcion = CodigosNPS.GetDescripcion(Convert.ToInt32(codigo));
				int orderId;
				double amount;
				System.Globalization.CultureInfo cultureEN_US = new System.Globalization.CultureInfo("en-US");
				if (codigo == "-1")
				{
					amount = Convert.ToDouble(sAmount, cultureEN_US);
					orderId = Convert.ToInt32(sOrderId);
					if (FacadeDao.IventurePagado(orderId))
					{
						Response.Clear();
						Response.Write("-1");
					}
					else
					{
						FacadeDao.ProcesarPago(orderId, amount);
						Response.Clear();
						Response.Write("1");
					}
				}
			}
			else {
				throw new Exception("No se encontró un proveedor con merchantId = " + Request.QueryString["idGateway"]);
			}
		}
		else if (Request.QueryString["egp_data"] == null)
		{
			throw new Exception("No se encontró el parámetro egp_data");
		}
		else
		{	//Request.QueryString["MerchantId"] == null
			throw new Exception("No se encontró el parámetro idGateway");

		}
	}
}
