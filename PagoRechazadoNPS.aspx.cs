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

public partial class PagoRechazadoNPS : Page, IBasicPage
{
	public Boolean LoginVisible { get { return false; } }
	public Control ControlFocus { get { return null; } }
	protected void Page_Load(object sender, EventArgs e)
    {
		if(!IsPostBack){
			ProcesarRechazo();
		}
    }
	private void ProcesarRechazo()
	{
		if (Request["egp_data"] != null && Request.QueryString["IdIventure"] != null)
		{
			Core.Iventure iventure = FacadeDao.GetIventure(Convert.ToInt32(Request.QueryString["IdIventure"]));
			if (iventure != null)
			{
				//string data = Core.NpsEncripterHelper.Decrypt(Request["egp_data"], ConfigurationManager.AppSettings["Key"]);
				string data = Core.NpsEncripterHelper.Decrypt(Request["egp_data"], iventure.Proveedor.ClaveEncNPS);
				string[] campos = data.Split("|".ToCharArray());
				string codigo = "-11111", idIventure = "-11111";
				for (int i = 0; i < campos.Length; i++)
				{
					if (campos[i].StartsWith("egp_ResponseCode="))
					{
						codigo = campos[i].Substring("egp_ResponseCode=".Length);
					}
					else if (campos[i].StartsWith("egp_OrderID="))
					{
						idIventure = campos[i].Substring("egp_OrderID=".Length);
					}

				}
				string descripcion = CodigosNPS.GetDescripcion(Convert.ToInt32(codigo));
				lblData.Text = "Nro de Solicitud: " + idIventure + "<br/>Código: " + codigo + "<br/>Descripción: " + descripcion;
			}
		}
		else if (Request["egp_data"] == null)
		{
			lblData.Text = "No se encontraron valores en egp_data";
		}
		else
		{ //(Request.QueryString["IdIventure"]!=null)
			lblData.Text = "No se encontraron valores en IdIventure";
		} 
	}
}
