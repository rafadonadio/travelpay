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

public partial class Controles_Redirect : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
	{
		if(Session["Usuario"]==null)
			Response.Redirect("Index2.aspx");
    }
}
