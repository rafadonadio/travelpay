using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Core
{
	/// <summary>
	/// Summary description for BasicPage
	/// </summary>
	public interface IBasicPage
	{
		Control ControlFocus { get; }
		Boolean LoginVisible { get; }
	}
}
