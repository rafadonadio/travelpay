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
public partial class PruebaSendMail : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		double amount = 1.1;
		Label1.Text = amount.ToString(new System.Globalization.CultureInfo("es-ar"));
	}
	protected void Button1_Click(object sender, EventArgs e)
	{
		BL.SendMail("veromassera@yahoo.com", "titulo", "cuerpo", null, true);
	}
}
