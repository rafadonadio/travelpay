<%@ WebHandler Language="C#" Class="GetDataTest" %>

using System;
using System.Web;
using System.Configuration;
using System.Text;
using Core;


public class GetDataTest : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Write(DataNPS(context));
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }
	private string DataNPS(HttpContext context)
	{
		bool existIdIventure = false;
		string sIdIventure = null;
		if (!String.IsNullOrEmpty(context.Request["IdIventure"])) {
			existIdIventure = true;
			sIdIventure = context.Request["IdIventure"];
		}

		if (existIdIventure)
		{
			int idIventure = Convert.ToInt32(sIdIventure);
			Core.Iventure iventure = FacadeDao.GetIventure(idIventure);
			if (iventure != null)
			{
				StringBuilder msg = new StringBuilder();
				//msg.Append("egp_MerchantID=").Append(iventure.Proveedor.IdGateway);
				msg.Append("egp_MerchantID=").Append("test_travelpay");
				msg.Append("|egp_TrnType=").Append(ConfigurationManager.AppSettings["TrnType"]);
				//msg.Append("|egp_Payments=").Append(ConfigurationManager.AppSettings["Payments"]);
				msg.Append("|egp_Payments=").Append(iventure.CntCuotas.ToString().PadLeft(2,'0'));
				msg.Append("|egp_Plan=").Append(ConfigurationManager.AppSettings["Plan"]);
				msg.Append("|egp_Currency=").Append(ConfigurationManager.AppSettings["Moneda"]);
				msg.Append("|egp_Amount=").Append(iventure.ImporteTotal.ToString("F2").Replace(",", "."));
				msg.Append("|egp_UserID=").Append(iventure.Cliente.Email);
				msg.Append("|egp_OrderID=").Append(iventure.Id);
				//msg.Append("|egp_UrlOk=").Append(ConfigurationManager.AppSettings["UrlOkNPS"] + "?IdIventure=" + sIdIventure);
				msg.Append("|egp_UrlOk=").Append("http://www.TravelPay.com.ar/PagoAceptadoNPSTest.aspx?IdIventure=" + sIdIventure);
				
				msg.Append("|egp_UrlError=").Append(ConfigurationManager.AppSettings["UrlErrorNPS"] + "?IdIventure=" + sIdIventure);
				msg.Append("|egp_MailCom=").Append(iventure.Proveedor.Email);
				msg.Append("|egp_UrlBack=").Append(ConfigurationManager.AppSettings["UrlBackNPS"] + "?IdIventure=" + sIdIventure);
				msg.Append("|egp_CardId=").Append(context.Request["CardId"]);
				//return Core.NpsEncripterHelper.Encrypt(msg.ToString(), ConfigurationManager.AppSettings["Key"]);
				//return Core.NpsEncripterHelper.Encrypt(msg.ToString(), iventure.Proveedor.ClaveEncNPS);
				return Core.NpsEncripterHelper.Encrypt(msg.ToString(), "298887645747869742751484443145062435617099358761");
				//context.Session["ClaveEncNPS"] = iventure.Proveedor.ClaveEncNPS;
			}
			else
			{
				return "";
			}
		}
		else
		{
			return "";
		}
	}
}