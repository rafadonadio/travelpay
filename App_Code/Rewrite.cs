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
{/// <summary>
	/// Summary description for Rewrite
	/// </summary>
	public class Rewrite : System.Web.IHttpModule
	{



		/// <summary>

		/// Init is required from the IHttpModule interface

		/// </summary>

		/// <param name="Appl"></param>

		public void Init(System.Web.HttpApplication Appl)
		{

			//make sure to wire up to BeginRequest

			Appl.BeginRequest += new System.EventHandler(Rewrite_BeginRequest);

		}



		/// <summary>

		/// Dispose is required from the IHttpModule interface

		/// </summary>

		public void Dispose()
		{

			//make sure you clean up after yourself

		}



		/// <summary>

		/// To handle the starting of the incoming request

		/// </summary>

		/// <param name="sender"></param>

		/// <param name="args"></param>

		public void Rewrite_BeginRequest(object sender, System.EventArgs args)
		{

			//process rules here
			//process rules here



			//cast the sender to an HttpApplication object

			System.Web.HttpApplication Appl = (System.Web.HttpApplication)sender;



			//load the settings in

			//System.Collections.Specialized.NameValueCollection SimpleSettings = (System.Collections.Specialized.NameValueCollection)System.Configuration.ConfigurationSettings.GetConfig("Rewrite.NET/SimpleSettings");
			System.Collections.Specialized.NameValueCollection SimpleSettings = (System.Collections.Specialized.NameValueCollection)System.Configuration.ConfigurationSettings.GetConfig("SimpleSettings");



			//see if we have a match

			for (int x = 0; x < SimpleSettings.Count; x++)
			{

				string source = SimpleSettings.GetKey(x);

				string dest = SimpleSettings.Get(x);

				if (Appl.Request.Path.ToLower() == source.ToLower())
				{

					SendToNewUrl(dest, Appl);

					break;

				}

			}


		}
		public void SendToNewUrl(string url, System.Web.HttpApplication Appl)
		{

			Appl.Context.RewritePath(url);

		}



	}

}

