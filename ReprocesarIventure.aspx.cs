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
using System.Xml;
using System.Text;
using Core;
public partial class ReprocesarIventure : Page, IBasicPage
{

	public Boolean LoginVisible { get { return true; } }
	public Control ControlFocus { get { return null; } }
	protected void Page_Load(object sender, EventArgs e)
    {
		int idIventure = Convert.ToInt32(Request["IdIventure"]);
		Core.Iventure iventure = FacadeDao.GetIventure(idIventure);
			
		if (!IsPostBack && Request["EncData"] != null && iventure!=null)
		{
			string encData = "";
			try
			{
				encData = Core.NpsEncripterHelper.Decrypt(Request["EncData"], iventure.Proveedor.ClaveEncNPS);
				encData = encData.Remove(encData.IndexOf("</TRANSACTIONS>") + "</TRANSACTIONS>".Length, encData.Length - (encData.IndexOf("</TRANSACTIONS>") + "</TRANSACTIONS>".Length));
				ReprocesarPago(encData);
			}
			catch
			{
				Label2.Text = encData;
			}
		}
		Response.Redirect("MisIventures.aspx");
    }

	private void ReprocesarPago(string xml)
	{
		XmlDocument xmlDoc = new XmlDocument();
		xmlDoc.LoadXml(xml);
		XmlNode nodeCharge = xmlDoc.SelectSingleNode("//CHARGE");
		string encData = nodeCharge.Attributes["ENC_DATA"].InnerText;
		string date = nodeCharge.Attributes["DATE"].InnerText;
		string idIventure = nodeCharge.Attributes["ORDER_ID"].InnerText;
		string codigo = nodeCharge.Attributes["RESPONSE_CODE"].InnerText;
		string data = encData;
		string[] campos = data.Split("|".ToCharArray());
		string sAmount = "-1";
		for (int i = 0; i < campos.Length; i++)
		{
			if (campos[i].StartsWith("egp_Amount="))
			{
				sAmount = campos[i].Substring("egp_Amount=".Length);
			}
		}
		System.Globalization.CultureInfo cultureEN_US = new System.Globalization.CultureInfo("en-US");
		try
		{
			FacadeDao.ProcesarPago(Convert.ToInt32(idIventure), Convert.ToDouble(sAmount, cultureEN_US), Convert.ToDateTime(date,cultureEN_US));
			Response.Redirect("MisIventures.aspx");
		}
		catch (Exception ex) {
			Label3.Text = ex.Message;
		}
	}
}
