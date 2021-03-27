using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Configuration;
using Core;

/// <summary>
/// Summary description for WebFormConSeguridad
/// </summary>
public class WebFormConSeguridad : Page, IBasicPage
{

	public Boolean LoginVisible { get { return true; } }
	public Control ControlFocus { get { return null; } }
	
	public WebFormConSeguridad()
	{        
	}
    protected override void OnLoadComplete(EventArgs e)
    {
        if (Session["Usuario"] == null)
           Response.Redirect("Index2.aspx");
        
        base.OnLoadComplete(e);
    }
}
