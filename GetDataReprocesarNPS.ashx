<%@ WebHandler Language="C#" Class="GetDataReprocesarNPS" %>

using System;
using System.Web;
using System.Text;
using System.Configuration;
using Core;

public class GetDataReprocesarNPS : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
		context.Response.Write(DataReprocesarNPS(context));
    }
	private string DataReprocesarNPS(HttpContext context){
		int idIventure = Convert.ToInt32(context.Request.QueryString["IdIventure"]);
		Core.Iventure iventure = FacadeDao.GetIventure(idIventure);
		if (iventure != null)
		{
			StringBuilder msg = new StringBuilder();
			msg.Append("MerchantID=").Append(context.Request.QueryString["MerchantID"]);
			msg.Append("|OrderId=").Append(context.Request.QueryString["IdIventure"]);
			msg.Append("|Url=").Append("http://www.travelpay.com.ar/ReprocesarIventure.aspx?IdIventure=" + context.Request.QueryString["IdIventure"]);
			return Core.NpsEncripterHelper.Encrypt(msg.ToString(), iventure.Proveedor.ClaveEncNPS);
			//return Core.NpsEncripterHelper.Encrypt(msg.ToString(), ConfigurationManager.AppSettings["Key"]);
		}
		else {
			return "No se pudo encontrar la solicitud.";
		}
	}
    public bool IsReusable {
        get {
            return false;
        }
    }

}