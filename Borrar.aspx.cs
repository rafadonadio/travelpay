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
using System.Net;
using System.IO;

public partial class Borrar : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack) {
			StringBuilder msg = new StringBuilder();
			//msg.Append("MerchantID=").Append(iventure.Proveedor.IdGateway);
			//msg.Append("|OrderId=").Append(iventure.Id);
			msg.Append("MerchantID=").Append("test_travelpay");
			msg.Append("|OrderId=").Append("19");
			msg.Append("|Url=").Append("http://www.travelpay.com.ar/ReprocesarIventure.aspx");
			EncData.Value = Core.NpsEncripterHelper.Encrypt(msg.ToString(), ConfigurationManager.AppSettings["Key"]);

		}
    }
	protected void Button1_Click(object sender, EventArgs e)
	{
		StringBuilder msg = new StringBuilder();
		msg.Append("MerchantID=test_travelpay");
		msg.Append("|OrderId=19"); 
		
		ASCIIEncoding encoding = new ASCIIEncoding();
		string postData = "";
		//postData += "EncData=" + msg.ToString();
		//postData += ("&MerchantID=test_travelpay");
		postData += "&EncData=" + msg.ToString();
		postData += "MerchantID=test_travelpay";
		//postData += "&Url=http://www.travelpay.com.ar/ReprocesarIventure.aspx";
		byte[] data = encoding.GetBytes(postData);

		// Prepare web request...
		HttpWebRequest myRequest =
		  (HttpWebRequest)WebRequest.Create("https://services.nps.com.ar/_ws_res_code_desa.php");
		myRequest.Method = "POST";
		myRequest.ContentType = "application/x-www-form-urlencoded";
		myRequest.ContentLength = data.Length;
		Stream newStream = myRequest.GetRequestStream();
		// Send the data.
		newStream.Write(data, 0, data.Length);
		newStream.Close();

		HttpWebResponse response = (HttpWebResponse)myRequest.GetResponse();

		if (HttpStatusCode.OK == response.StatusCode)
		{
			Stream dataStream = response.GetResponseStream();
			StreamReader reader = new StreamReader(dataStream);
			string responseFromServer = reader.ReadToEnd();
			//File.WriteAllText(@"C:\response.txt", responseFromServer);
			response.Close();
			Label1.Text = responseFromServer;
		} 

	}
}
