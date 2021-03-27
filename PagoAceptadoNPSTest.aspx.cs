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

public partial class PagoAceptadoNPSTest : Page, IBasicPage
{
	public Boolean LoginVisible { get { return false; } }
	public Control ControlFocus { get { return null; } }
	protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack) {
			ProcesarPago();
		}
    }

	private void ProcesarPago()
	{
		if (Request["egp_data"] != null && Request.QueryString["IdIventure"] != null)
		{
			Core.Iventure iventure = FacadeDao.GetIventure(Convert.ToInt32(Request.QueryString["IdIventure"]));
			if (iventure != null)
			{
				//string data = Core.NpsEncripterHelper.Decrypt(Request["egp_data"], ConfigurationManager.AppSettings["Key"]);
				//string data = Core.NpsEncripterHelper.Decrypt(Request["egp_data"], iventure.Proveedor.ClaveEncNPS);
				string data = Core.NpsEncripterHelper.Decrypt(Request["egp_data"], "298887645747869742751484443145062435617099358761");
				//"298887645747869742751484443145062435617099358761"
				string[] campos = data.Split("|".ToCharArray());
				string codigo = "-11111";
				string sOrderId = "-1";
				string sAmount = "-1";
				string sFecha = "", sHora = "", sTrans = "", sAuthorizCode="";
				DateTime fechaPago;
				for (int i = 0; i < campos.Length; i++)
				{
					if (campos[i].StartsWith("egp_ResponseCode="))
					{
						codigo = campos[i].Substring("egp_ResponseCode=".Length);
					}
					else if (campos[i].StartsWith("egp_OrderID="))
					{
						sOrderId = campos[i].Substring("egp_OrderID=".Length);
					}
					else if (campos[i].StartsWith("egp_Amount="))
					{
						sAmount = campos[i].Substring("egp_Amount=".Length);
					}
					else if (campos[i].StartsWith("egp_FechaTrn_srv="))
					{
						sFecha = campos[i].Substring("egp_FechaTrn_srv=".Length);

					}
					else if (campos[i].StartsWith("egp_HoraTrn_srv="))
					{
						sHora = campos[i].Substring("egp_HoraTrn_srv=".Length);
					}
					else if (campos[i].StartsWith("egp_TrnID="))
					{
						sTrans = campos[i].Substring("egp_TrnID=".Length);
					}
					else if (campos[i].StartsWith("egp_AuthorizCode="))
					{
						sAuthorizCode = campos[i].Substring("egp_AuthorizCode=".Length);
					}
				}
				fechaPago = GetFechaPago(sFecha, sHora);

				string sufijo = "";
				string detalle = iventure.DetalleAereo;
				if (iventure.DetalleAereo.Length > 30)
				{
					sufijo = " ...";
					detalle = iventure.DetalleAereo.Substring(0, 30);
				}
				lblDescripcion.Text = detalle + sufijo;
				lblFecha.Text = fechaPago.ToShortDateString();
				lblHora.Text = fechaPago.ToShortTimeString(); 
				lblTrans.Text = sTrans;
				lblIdIventure.Text = sOrderId;
				lblCodAut.Text = sAuthorizCode;
				imgEmpresa.ImageUrl = iventure.Proveedor.UrlImagen;
				int orderId, authorizCode;
				Int64 trnID;
				double amount;
				
				System.Globalization.CultureInfo cultureEN_US = new System.Globalization.CultureInfo("en-US");
				if (codigo == "-1")
				{
					amount = Convert.ToDouble(sAmount, cultureEN_US);
					lblImporte.Text = amount.ToString(new System.Globalization.CultureInfo("es-ar"));
					orderId = Convert.ToInt32(sOrderId);
					authorizCode = Convert.ToInt32(sAuthorizCode);
					trnID = Convert.ToInt64(sTrans);
					FacadeDao.ProcesarPago(orderId, amount, fechaPago,trnID, authorizCode);
				}
			}
		}
		else if (Request["egp_data"] == null){
			lblData.Text = "No se encontraron valores en egp_data";
		}
		else
		{ //Request.QueryString["IdIventure"] != null
			lblData.Text = "No se encontraron valores en IdIventure";
		} 
		
	}

	private DateTime GetFechaPago(string fecha, string hora)
	{
		int anio = 0, mes = 0, dia = 0, _hora = 0, minuto = 0, segundo = 0;
		if (fecha.Length == 4)
		{
			anio = DateTime.Today.Year;
			mes = Convert.ToInt32(fecha.Substring(0, 2));
			dia = Convert.ToInt32(fecha.Substring(2, 2));
		} else if (fecha.Length == 8)
		{
			anio = Convert.ToInt32(fecha.Substring(0, 4));
			mes = Convert.ToInt32(fecha.Substring(4, 2));
			dia = Convert.ToInt32(fecha.Substring(6, 2));
		}
		if (hora.Length >= 6)
		{
			_hora = Convert.ToInt32(hora.Substring(0, 2));
			minuto = Convert.ToInt32(hora.Substring(2, 2));
			segundo = Convert.ToInt32(hora.Substring(4, 2));
		}
		return new DateTime(anio, mes, dia, _hora, minuto, segundo);
	}
}
